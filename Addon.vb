Imports MySql.Data.MySqlClient
Imports Mysqlx.Crud

Public Class Addon
    Private addOnData As New List(Of Dictionary(Of String, Object))
    Private navigatingAway As Boolean = False
    Dim selectedQuantities(14) As Integer ' Holds quantity for all 15 addons
    Dim imageRcv As Image = Session.planImage
    Dim planName As String = Session.planName
    Dim planType As String = Session.planType
    Dim planPrice As Decimal = Session.planPrice
    Dim txtValues(4) As Integer
    Dim groupHardware() As TextBox
    Dim prices() As Integer
    Dim totalPriceHardware As Decimal = 0
    Dim total As Decimal = 0
    Dim page As Integer = 1
    Dim productGroup() As GroupBox
    Dim productNames() As String
    Dim priceGroup() As Label
    Dim pageQuantities(2, 4) As Integer
    Dim cartItems As New List(Of cartItem)
    Dim addonIds() As Integer
    Dim addedItemsTotal As Decimal = 0

    Public Class cartItem
        Public Property ProductName As String
        Public Property Quantity As Integer
        Public Property Price As Decimal
        Public Property AddonId As Integer
    End Class

    Public Sub pageHandling()
        priceGroup = {lblPrice1, lblPrice2, lblPrice3, lblPrice4, lblPrice5}
        productGroup = {gbxProduct1, gbxProduct2, gbxProduct3, gbxProduct4, gbxProduct5}
        groupHardware = {txtHardware1, txtHardware2, txtHardware3, txtHardware4, txtHardware5}

        Dim baseIndex As Integer = (page - 1) * 5

        If page = 1 Then
            lblSpecific.Text = "Hardware"
            productNames = {"5G Modem/Router", "WiFi Extender", "Ethernet Cable (10m)", "External Antenna", "Backup Battey Pack"}
            prices = {7990D, 2500D, 500D, 1200D, 3500D}
            addonIds = {1, 2, 3, 4, 5}
        ElseIf page = 2 Then
            lblSpecific.Text = "Services"
            productNames = {"Installation Service", "Netflix Subscription (Monthly)", "Landline Service (Monthly)",
                        "Home Network Setup", "Premium Tech Support"}
            prices = {1500D, 549D, 800D, 2000D, 500D}
            addonIds = {6, 7, 8, 9, 10}
        ElseIf page = 3 Then
            lblSpecific.Text = "Plan Upgrades"
            productNames = {"Speed Boost 100 Mbps", "Speed Boost 200 Mbps", "Data Allowance +100GB",
                        "Data Allowance +50GB", "Priority Support Upgrade"}
            prices = {500D, 1000D, 500D, 300D, 400D}
            addonIds = {11, 12, 13, 14, 15}
        End If

        ' Populate UI
        For i As Integer = 0 To 4
            priceGroup(i).Text = "₱" & prices(i).ToString("N2")
            productGroup(i).Text = productNames(i)
            groupHardware(i).Text = selectedQuantities(baseIndex + i).ToString()
        Next
    End Sub

    Private Sub SavePageQuantities()
        Dim baseIndex As Integer = (page - 1) * 5
        For i As Integer = 0 To 4
            Dim val As Integer = 0
            Integer.TryParse(groupHardware(i).Text, val)
            selectedQuantities(baseIndex + i) = val
        Next
    End Sub


    Private Function GetCartTotal(customerId As Integer) As Decimal
        Dim total As Decimal = 0
        Using con As New MySqlConnection(DatabaseHelper.ConnectionString)
            con.Open()
            Dim query As String = "
            SELECT SUM(a.price * sc.quantity) AS total
            FROM shopping_cart sc
            JOIN addons a ON sc.addon_id = a.addon_id
            WHERE sc.customer_id = @customerId;"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@customerId", customerId)
                Dim result = cmd.ExecuteScalar()
                If result IsNot DBNull.Value Then
                    total = Convert.ToDecimal(result)
                End If
            End Using
        End Using
        Return total
    End Function

    Private Function AddToCartDatabase(customerId As Integer, addonId As Integer, quantity As Integer) As Boolean
        Try
            Using connection As New MySqlConnection(DatabaseHelper.ConnectionString)
                connection.Open()
                Dim query As String = "INSERT INTO shopping_cart (customer_id, addon_id, quantity) " &
                                         "VALUES (@customerId, @addonId, @quantity) " &
                                         "ON DUPLICATE KEY UPDATE quantity = quantity + @quantity, added_at = CURRENT_TIMESTAMP"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@customerId", customerId)
                    cmd.Parameters.AddWithValue("@addonId", addonId)
                    cmd.Parameters.AddWithValue("@quantity", quantity)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("Error adding to cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function PurchaseAddonsDirectly() As Boolean
        Dim success As Boolean = False
        Dim conStr As String = "server=localhost; userid=root; database=fdbmsproject"
        Dim con As New MySqlConnection(conStr)
        Dim trans As MySqlTransaction = Nothing ' Declare outside Try to be accessible in Catch

        Try
            con.Open()
            trans = con.BeginTransaction()

            ' Insert transaction record
            Dim insertTxnQuery As String = "INSERT INTO addon_purchases (user_id, created_at, total_price)
                                        VALUES (@userId, NOW(), @total)"
            Dim txnCmd As New MySqlCommand(insertTxnQuery, con, trans)
            txnCmd.Parameters.AddWithValue("@userId", Session.UserId)
            txnCmd.Parameters.AddWithValue("@total", addedItemsTotal)
            txnCmd.ExecuteNonQuery()

            Dim purchaseId As Long = txnCmd.LastInsertedId

            ' Insert each selected addon
            For i = 0 To 4
                If txtValues(i) > 0 AndAlso i < addOnData.Count Then
                    Dim addon = addOnData(i)
                    Dim insertAddonQuery As String = "
                    INSERT INTO customer_addons (customer_id, addon_id, quantity, total_price, purchase_date)
                    VALUES (@customerId, @addonId, @qty, @total, NOW())"

                    Using itemCmd As New MySqlCommand(insertAddonQuery, con, trans)
                        itemCmd.Parameters.AddWithValue("@customerId", Session.UserId)
                        itemCmd.Parameters.AddWithValue("@addonId", addon("addon_id"))
                        itemCmd.Parameters.AddWithValue("@qty", txtValues(i))
                        itemCmd.Parameters.AddWithValue("@total", addon("price") * txtValues(i))
                        itemCmd.ExecuteNonQuery()
                    End Using
                End If
            Next

            trans.Commit()
            success = True
        Catch ex As Exception
            MessageBox.Show("Error during addon purchase: " & ex.Message)
            If trans IsNot Nothing Then
                Try
                    trans.Rollback()
                Catch rollEx As Exception
                    MessageBox.Show("Rollback failed: " & rollEx.Message)
                End Try
            End If
        Finally
            con.Close()
        End Try

        Return success
    End Function



    Private Function CreateOrUpdateBillingRecord(connection As MySqlConnection, transaction As MySqlTransaction, subscriberId As Integer, totalAmount As Decimal) As Integer
        Dim billingId As Integer = 0

        ' Handle different cases:
        ' 1. New subscriber (just selected plan + addons)
        ' 2. Existing subscriber adding new addons
        ' 3. Standalone product purchase

        Dim currentMonthStart As Date = New Date(Date.Now.Year, Date.Now.Month, 1)
        Dim dueDate As Date = Date.Now.AddDays(30)

        If subscriberId > 0 Then
            ' Check for existing billing record
            Dim checkQuery As String = "SELECT billing_id FROM billing_records WHERE subscriber_id = @subscriberId AND billing_month = @billingMonth"
            Using cmd As New MySqlCommand(checkQuery, connection, transaction)
                cmd.Parameters.AddWithValue("@subscriberId", subscriberId)
                cmd.Parameters.AddWithValue("@billingMonth", currentMonthStart)
                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    billingId = Convert.ToInt32(result)
                    ' Update existing record
                    Dim updateQuery As String = "UPDATE billing_records SET total_amount = total_amount + @amount WHERE billing_id = @billingId"
                    Using updateCmd As New MySqlCommand(updateQuery, connection, transaction)
                        updateCmd.Parameters.AddWithValue("@amount", totalAmount)
                        updateCmd.Parameters.AddWithValue("@billingId", billingId)
                        updateCmd.ExecuteNonQuery()
                    End Using
                    Return billingId
                End If
            End Using
        End If

        ' Create new billing record
        Dim insertQuery As String = "INSERT INTO billing_records (subscriber_id, billing_month, total_amount, due_date, status) " &
                                   "VALUES (@subscriberId, @billingMonth, @totalAmount, @dueDate, 'Pending'); SELECT LAST_INSERT_ID();"

        Using cmd As New MySqlCommand(insertQuery, connection, transaction)
            cmd.Parameters.AddWithValue("@subscriberId", If(subscriberId > 0, subscriberId, DBNull.Value))
            cmd.Parameters.AddWithValue("@billingMonth", currentMonthStart)
            cmd.Parameters.AddWithValue("@totalAmount", totalAmount)
            cmd.Parameters.AddWithValue("@dueDate", dueDate)
            billingId = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        Return billingId
    End Function

    Private Function CreateStandaloneBillingRecord(connection As MySqlConnection, transaction As MySqlTransaction, addonTotal As Decimal) As Integer
        ' For customers without active subscriptions, create a standalone billing record
        Dim currentMonthStart As Date = New Date(Date.Now.Year, Date.Now.Month, 1)
        Dim dueDate As Date = Date.Now.AddDays(30) ' 30 days from now

        ' Create a temporary subscriber record or use a special subscriber_id for addon-only purchases
        ' For simplicity, we'll use subscriber_id = 0 or create a special handling
        Dim insertQuery As String = "INSERT INTO billing_records (subscriber_id, billing_month, total_amount, due_date) VALUES (@subscriberId, @billingMonth, @totalAmount, @dueDate)"
        Using cmd As New MySqlCommand(insertQuery, connection, transaction)
            cmd.Parameters.AddWithValue("@subscriberId", DBNull.Value) ' This might need adjustment based on your business logic
            cmd.Parameters.AddWithValue("@billingMonth", currentMonthStart)
            cmd.Parameters.AddWithValue("@totalAmount", addonTotal)
            cmd.Parameters.AddWithValue("@dueDate", dueDate)
            cmd.ExecuteNonQuery()
            Return cmd.LastInsertedId
        End Using
    End Function

    Private Function GetActiveSubscriberId(connection As MySqlConnection, transaction As MySqlTransaction) As Integer
        Dim query As String = "SELECT subscriber_id FROM subscribers WHERE customer_id = @customerId AND status = 'Active' LIMIT 1"
        Using cmd As New MySqlCommand(query, connection, transaction)
            cmd.Parameters.AddWithValue("@customerId", Session.UserId)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                Return Convert.ToInt32(result)
            End If
        End Using
        Return 0
    End Function

    Private Function GetCurrentPlanPrice(connection As MySqlConnection, transaction As MySqlTransaction, subscriberId As Integer) As Decimal
        Dim query As String = "SELECT ip.price FROM subscribers s JOIN internet_plans ip ON s.plan_id = ip.plan_id WHERE s.subscriber_id = @subscriberId"
        Using cmd As New MySqlCommand(query, connection, transaction)
            cmd.Parameters.AddWithValue("@subscriberId", subscriberId)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                Return Convert.ToDecimal(result)
            End If
        End Using
        Return 0
    End Function

    Private Sub CreatePaymentRecord(connection As MySqlConnection, transaction As MySqlTransaction, billingId As Integer, amount As Decimal)
        Dim insertQuery As String = "INSERT INTO payments (billing_id, amount, payment_date) VALUES (@billingId, @amount, @paymentDate)"
        Using cmd As New MySqlCommand(insertQuery, connection, transaction)
            cmd.Parameters.AddWithValue("@billingId", billingId)
            cmd.Parameters.AddWithValue("@amount", amount)
            cmd.Parameters.AddWithValue("@paymentDate", Date.Now.Date)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub ClearAllQuantities()
        ' Reset quantities across ALL pages
        For pageNum = 0 To 2
            For itemIndex = 0 To 4
                pageQuantities(pageNum, itemIndex) = 0
            Next
        Next

        ' Reset current page display
        For i = 0 To 4
            txtValues(i) = 0
            groupHardware(i).Text = txtValues(i).ToString()
        Next

        ' Reset totals
        addedItemsTotal = 0
        If Session.fromProduct Then
            total = 0
        Else
            total = planPrice
        End If
        txtTotal.Text = "Php " & total.ToString("F2")
        TextBox3.Text = "Php " & total.ToString("F2")
    End Sub

    Private Sub Addon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Session.CheckTransactionTimeout()

        If Not Session.IsTransactionActive AndAlso Session.fromProduct = False Then
            MessageBox.Show("Your session has expired. Please select a plan again.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Subscription.Show()
            Me.Close()
            Return
        End If

        If Session.fromProduct Then
            pbxPlanImage.Visible = False
            Dim skylinkProduct As New Label()
            Dim cartTotal As Decimal = GetCartTotal(Session.UserId)
            total = cartTotal
            txtTotal.Text = "Php " & total.ToString("F2")
            skylinkProduct.Size = New Size(600, 100)
            skylinkProduct.Font = New Font("Tahoma", 25, FontStyle.Bold)
            skylinkProduct.Text = "SkyLink Products"
            skylinkProduct.Location = New Point(15, 50)
            skylinkProduct.ForeColor = Color.White
            Me.Controls.Add(skylinkProduct)
        Else
            total += planPrice
            txtTotal.Text = "Php " & total.ToString("F2")
            pbxPlanImage.Image = imageRcv
            lblName.Text = "Plan: " & planName
            lblType.Text = "Type: " & planType
            lblPrice.Text = "Price: " & planPrice.ToString("F2")
        End If

        pageHandling()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd1.Click, btnAdd2.Click, btnAdd3.Click, btnAdd4.Click, btnAdd5.Click
        Dim index As Integer = CInt(DirectCast(sender, Button).Tag)

        txtValues(index) += 1
        groupHardware(index).Text = txtValues(index).ToString()

        addedItemsTotal += prices(index)
        total += prices(index)
        txtTotal.Text = "Php " & total.ToString("F2")
        TextBox3.Text = addedItemsTotal
    End Sub

    Private Sub btnMinus1_Click(sender As Object, e As EventArgs) Handles btnMinus1.Click, btnMinus2.Click, btnMinus3.Click, btnMinus4.Click, btnMinus5.Click
        Dim index As Integer = CInt(DirectCast(sender, Button).Tag)

        txtValues(index) -= 1

        If txtValues(index) < 0 Then
            txtValues(index) = 0
            MessageBox.Show("Quantity cannot be less than 0", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        groupHardware(index).Text = txtValues(index).ToString

        total -= prices(index)
        addedItemsTotal -= prices(index)
        txtTotal.Text = "Php " & total.ToString("F2")
        TextBox3.Text = addedItemsTotal
    End Sub

    Private Sub btnBuyNow_Click(sender As Object, e As EventArgs) Handles btnBuyNow.Click
        ' Check if user is logged in
        If Session.UserId <= 0 Then
            MessageBox.Show("Please log in to make a purchase!", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if any items are selected
        Dim hasSelectedItems As Boolean = False
        For pageNum = 0 To 2
            For itemIndex = 0 To 4
                If pageQuantities(pageNum, itemIndex) > 0 Then
                    hasSelectedItems = True
                    Exit For
                End If
            Next
            If hasSelectedItems Then Exit For
        Next

        ' Include current page items
        For i = 0 To 4
            If txtValues(i) > 0 Then
                hasSelectedItems = True
                Exit For
            End If
        Next

        If Not hasSelectedItems AndAlso Not Session.IsNewSubscription Then
            MessageBox.Show("Please select at least one item to purchase!", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Compute total including plan if it's from subscription
        Dim totalPurchaseAmount As Decimal = 0
        If Session.IsNewSubscription Then
            totalPurchaseAmount += Session.planPrice
        End If
        totalPurchaseAmount += addedItemsTotal ' Added items total computed from UI selection

        ' Confirm purchase
        Dim confirmResult As DialogResult = MessageBox.Show($"Confirm purchase of items totaling Php {totalPurchaseAmount:F2}?",
                                                       "Confirm Purchase",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question)

        If confirmResult = DialogResult.Yes Then
            Dim purchaseSuccess As Boolean = False

            If Session.IsNewSubscription Then
                ' This is a new subscription purchase (plan + addons)
                purchaseSuccess = PurchaseSubscriptionWithAddons()
            ElseIf Session.fromProduct Then
                ' Buying addons directly (from Products tab)
                purchaseSuccess = PurchaseAddonsDirectly()
            End If

            If purchaseSuccess Then
                ' Add user to subscribers table with status 'Pending' (if not already a subscriber)

                MessageBox.Show("Purchase successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Purchase failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Function PurchaseSubscriptionWithAddons() As Boolean
        Dim success As Boolean = False
        Dim conStr As String = "server=localhost; userid=root; database=fdbmsproject"
        Dim con As New MySqlConnection(conStr)
        Dim trans As MySqlTransaction = Nothing

        Try
            con.Open()
            trans = con.BeginTransaction()

            ' Insert subscription record
            Dim insertSubQuery As String = "
        INSERT INTO subscribers (customer_id, plan_id, subscription_date)
        VALUES (@customerId, @planId, NOW())"
            Dim subCmd As New MySqlCommand(insertSubQuery, con, trans)
            subCmd.Parameters.AddWithValue("@customerId", Session.UserId)
            subCmd.Parameters.AddWithValue("@planId", Session.PlanId)
            subCmd.ExecuteNonQuery()

            Dim subscriptionId As Integer = CInt(subCmd.LastInsertedId)

            ' Process all pages for addons
            For pageNum As Integer = 0 To 2 ' Pages 1-3 (0-based indexing)
                For itemIndex As Integer = 0 To 4 ' 5 items per page
                    Dim quantity As Integer = 0

                    ' Get quantity from the appropriate source
                    If pageNum = page - 1 Then
                        ' Current page - get from txtValues
                        quantity = txtValues(itemIndex)
                    Else
                        ' Other pages - get from pageQuantities
                        quantity = pageQuantities(pageNum, itemIndex)
                    End If

                    If quantity > 0 Then
                        ' Calculate the actual addon_id based on page and item position
                        Dim actualAddonId As Integer = (pageNum * 5) + itemIndex + 1 ' addon_ids are 1-15

                        ' Get the addon price from database
                        Dim priceQuery As String = "SELECT price FROM addons WHERE addon_id = @addonId"
                        Dim priceCmd As New MySqlCommand(priceQuery, con, trans)
                        priceCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        Dim addonPrice As Decimal = Convert.ToDecimal(priceCmd.ExecuteScalar())

                        ' Insert addon purchase
                        Dim insertAddonQuery As String = "
                    INSERT INTO customer_addons (customer_id, addon_id, quantity, total_price, purchase_date) 
                    VALUES (@custId, @addonId, @qty, @total, NOW())"

                        Dim addonCmd As New MySqlCommand(insertAddonQuery, con, trans)
                        addonCmd.Parameters.AddWithValue("@custId", Session.UserId)
                        addonCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        addonCmd.Parameters.AddWithValue("@qty", quantity)
                        addonCmd.Parameters.AddWithValue("@total", addonPrice * quantity)
                        addonCmd.ExecuteNonQuery()

                        ' Debug output
                        Console.WriteLine($"Inserted addon: ID={actualAddonId}, Qty={quantity}, Price={addonPrice}")
                    End If
                Next
            Next

            trans.Commit()
            success = True

        Catch ex As Exception
            MessageBox.Show("Error during subscription purchase: " & ex.Message)
            If trans IsNot Nothing Then
                Try
                    trans.Rollback()
                Catch rollEx As Exception
                    MessageBox.Show("Rollback failed: " & rollEx.Message)
                End Try
            End If
        Finally
            con.Close()
        End Try

        Return success
    End Function


    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        SavePageQuantities()
        If page < 3 Then
            page += 1
            pageHandling()
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        SavePageQuantities()
        If page > 1 Then
            page -= 1
            pageHandling()
        End If
    End Sub


    Private Sub btnCart_Click_1(sender As Object, e As EventArgs) Handles btnCart.Click
        For i = 0 To 4
            pageQuantities(page - 1, i) = txtValues(i)
        Next

        Dim hasItems As Boolean = False
        Dim failedItems As New List(Of String)

        ' Check if user is logged in
        If Session.UserId <= 0 Then
            MessageBox.Show("Please log in to add items to cart!", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim currentPageBeforeProcessing As Integer = page
        Dim successfullyAddedTotal As Decimal = 0

        ' Process all pages (1-3) and all items (0-4)
        For currentPage = 1 To 3
            page = currentPage
            pageHandling()

            For itemIndex = 0 To 4
                Dim quantity = pageQuantities(currentPage - 1, itemIndex)
                If quantity > 0 Then
                    hasItems = True
                    If AddToCartDatabase(Session.UserId, addonIds(itemIndex), quantity) Then
                    Else
                        failedItems.Add(productNames(itemIndex))
                    End If
                End If
            Next
        Next

        ' Restore the original page
        page = currentPageBeforeProcessing
        pageHandling()

        If hasItems Then
            If failedItems.Count = 0 Then
                ' Reset quantities across ALL pages
                For pageNum = 0 To 2
                    For itemIndex = 0 To 4
                        pageQuantities(pageNum, itemIndex) = 0
                    Next
                Next

                ' Reset current page display
                For i = 0 To 4
                    txtValues(i) = 0
                    groupHardware(i).Text = txtValues(i).ToString()
                Next

                MessageBox.Show("Items added to cart successfully!" & vbCrLf & "Added Php " & addedItemsTotal.ToString("F2") & " to your total.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Some items failed to add to cart: " & String.Join(", ", failedItems),
                           "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("No items selected to add to cart!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub CartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles cartbutton.Click
        Session.CheckTransactionTimeout()

        If Not Session.IsTransactionActive AndAlso Session.fromProduct = False Then
            MessageBox.Show("Your session has expired. Please select a plan again.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Subscription.Show()
            Me.Close()
            Return
        End If

        Cart.Show()
        Me.Close()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Subscription.Show()
        Me.Close()
    End Sub
End Class

Public Class DatabaseHelper
    Public Shared ReadOnly Property ConnectionString As String
        Get
            Return "server=localhost; userid=root; database=fdbmsproject"
        End Get
    End Property
End Class