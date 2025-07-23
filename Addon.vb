Imports MySql.Data.MySqlClient
Imports Mysqlx.Crud

Public Class Addon
    Private addOnData As New List(Of Dictionary(Of String, Object))
    Private navigatingAway As Boolean = False
    Dim selectedQuantities(14) As Integer ' Holds quantity for all 15 addons (0-14 indexed)
    Dim imageRcv As Image = Session.planImage
    Dim planName As String = Session.planName
    Dim planType As String = Session.planType
    Dim planPrice As Decimal = Session.planPrice
    Dim txtValues(4) As Integer ' Current page display values
    Dim groupHardware() As TextBox
    Dim prices() As Integer
    Dim totalPriceHardware As Decimal = 0
    Dim total As Decimal = 0
    Dim page As Integer = 1
    Dim productGroup() As GroupBox
    Dim productNames() As String
    Dim priceGroup() As Label
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
        Else

        End If

        ' Load saved quantities from selectedQuantities array into txtValues and display
        For i As Integer = 0 To 4
            txtValues(i) = selectedQuantities(baseIndex + i)
            priceGroup(i).Text = "₱" & prices(i).ToString("N2")
            productGroup(i).Text = productNames(i)
            groupHardware(i).Text = txtValues(i).ToString()
        Next

        ' Recalculate total based on all selected items
        RecalculateTotal()
    End Sub

    Private Sub SavePageQuantities()
        ' Save current page quantities to the selectedQuantities array
        Dim baseIndex As Integer = (page - 1) * 5
        For i As Integer = 0 To 4
            selectedQuantities(baseIndex + i) = txtValues(i)
        Next
    End Sub

    Private Sub RecalculateTotal()
        ' Calculate total from base plan price and all selected quantities
        If Session.fromProduct Then
            Dim cartTotal As Decimal = GetCartTotal(Session.UserId)
            total = cartTotal
        Else
            total = planPrice
        End If

        ' Add all addon quantities across all pages
        addedItemsTotal = 0
        For pageNum As Integer = 1 To 3
            Dim baseIdx As Integer = (pageNum - 1) * 5
            For itemIdx As Integer = 0 To 4
                Dim quantity As Integer = selectedQuantities(baseIdx + itemIdx)
                If quantity > 0 Then
                    Dim itemPrice As Decimal = GetPriceForAddon(baseIdx + itemIdx + 1) ' addon IDs are 1-based
                    addedItemsTotal += itemPrice * quantity
                End If
            Next
        Next

        total += addedItemsTotal
        txtTotal.Text = "Php " & total.ToString("F2")
        TextBox3.Text = "Php " & addedItemsTotal.ToString("F2")
    End Sub

    Private Function GetPriceForAddon(addonId As Integer) As Decimal
        ' Get price based on addon ID (1-15)
        Select Case addonId
            Case 1 : Return 7990D  ' 5G Modem/Router
            Case 2 : Return 2500D  ' WiFi Extender
            Case 3 : Return 500D   ' Ethernet Cable
            Case 4 : Return 1200D  ' External Antenna
            Case 5 : Return 3500D  ' Backup Battery Pack
            Case 6 : Return 1500D  ' Installation Service
            Case 7 : Return 549D   ' Netflix Subscription
            Case 8 : Return 800D   ' Landline Service
            Case 9 : Return 2000D  ' Home Network Setup
            Case 10 : Return 500D  ' Premium Tech Support
            Case 11 : Return 500D  ' Speed Boost 100 Mbps
            Case 12 : Return 1000D ' Speed Boost 200 Mbps
            Case 13 : Return 500D  ' Data Allowance +100GB
            Case 14 : Return 300D  ' Data Allowance +50GB
            Case 15 : Return 400D  ' Priority Support Upgrade
            Case Else : Return 0D
        End Select
    End Function

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
        Dim trans As MySqlTransaction = Nothing

        Dim decDPayment As Decimal = 0

        Dim strDPayment As String = InputBox("Please enter your money:", "Payment", "0.00")

        If Decimal.TryParse(strDPayment, decDPayment) Then

            If decDPayment > total Then

                Dim dChange As Decimal = decDPayment - total

                MsgBox($"Thank your for purchasing! Here is your change: Php {dChange.ToString("f2")}")

            ElseIf decDPayment = total Then

                MsgBox($"Thank your for purchasing!")

            ElseIf decDPayment < total Then

                MsgBox("Insufficient money, please try again!")
                success = False
                Return success

            End If
            Try
                con.Open()
                trans = con.BeginTransaction()

                ' Process all selected items across all pages
                For addonIndex As Integer = 0 To 14 ' 0-14 for addons 1-15
                    Dim quantity As Integer = selectedQuantities(addonIndex)
                    If quantity > 0 Then
                        Dim actualAddonId As Integer = addonIndex + 1 ' addon_ids are 1-15

                        ' Get the addon price from database
                        Dim priceQuery As String = "SELECT price FROM addons WHERE addon_id = @addonId"
                        Dim priceCmd As New MySqlCommand(priceQuery, con, trans)
                        priceCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        Dim addonPrice As Decimal = Convert.ToDecimal(priceCmd.ExecuteScalar())

                        ' Insert addon purchase
                        Dim insertAddonQuery As String = "
                INSERT INTO customer_addons (customer_id, addon_id, quantity, purchase_date) 
                VALUES (@custId, @addonId, @qty, NOW())"
                        Dim addonCmd As New MySqlCommand(insertAddonQuery, con, trans)
                        addonCmd.Parameters.AddWithValue("@custId", Session.UserId)
                        addonCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        addonCmd.Parameters.AddWithValue("@qty", quantity)
                        addonCmd.ExecuteNonQuery()

                        Console.WriteLine($"Inserted addon: ID={actualAddonId}, Qty={quantity}, Price={addonPrice}")
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

        Else


            MsgBox("Please Enter A Valid Amount")
            success = False
            Return success

        End If




    End Function

    Private Sub ClearAllQuantities()
        ' Reset all quantities
        For i As Integer = 0 To 14
            selectedQuantities(i) = 0
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
            btnPrevious.Visible = False
            btnNext.Visible = False
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

        ' Update the selectedQuantities array immediately
        Dim baseIndex As Integer = (page - 1) * 5
        selectedQuantities(baseIndex + index) = txtValues(index)

        ' Recalculate totals
        RecalculateTotal()
    End Sub

    Private Sub btnMinus1_Click(sender As Object, e As EventArgs) Handles btnMinus1.Click, btnMinus2.Click, btnMinus3.Click, btnMinus4.Click, btnMinus5.Click
        Dim index As Integer = CInt(DirectCast(sender, Button).Tag)

        If txtValues(index) <= 0 Then
            MessageBox.Show("Quantity cannot be less than 0", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        txtValues(index) -= 1
        groupHardware(index).Text = txtValues(index).ToString

        ' Update the selectedQuantities array immediately
        Dim baseIndex As Integer = (page - 1) * 5
        selectedQuantities(baseIndex + index) = txtValues(index)

        ' Recalculate totals
        RecalculateTotal()
    End Sub

    Private Sub btnBuyNow_Click(sender As Object, e As EventArgs) Handles btnBuyNow.Click
        ' Save current page quantities first
        SavePageQuantities()

        ' Check if user is logged in
        If Session.UserId <= 0 Then
            MessageBox.Show("Please log in to make a purchase!", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if any items are selected across all pages
        Dim hasSelectedItems As Boolean = False
        For i As Integer = 0 To 14
            If selectedQuantities(i) > 0 Then
                hasSelectedItems = True
                Exit For
            End If
        Next

        If Not hasSelectedItems AndAlso Not Session.IsNewSubscription Then
            MessageBox.Show("Please select at least one item to purchase!", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Confirm purchase
        Dim confirmResult As DialogResult = MessageBox.Show($"Confirm purchase of items totaling Php {total:F2}?",
                                                       "Confirm Purchase",
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question)


        If confirmResult = DialogResult.Yes Then
            Dim purchaseSuccess As Boolean = False

            If Session.IsNewSubscription Then
                ' This is a new subscription purchase (plan + addons)
                purchaseSuccess = PurchaseSubscriptionWithAddons()



            ElseIf Session.fromProduct = True Then
                ' Buying addons directly (from Products tab)
                purchaseSuccess = PurchaseAddonsDirectly()
            End If

            If purchaseSuccess Then
                ClearAllQuantities() ' Clear after successful purchase
            End If
        End If
    End Sub

    Private Function PurchaseSubscriptionWithAddons() As Boolean

        Dim success As Boolean = False
        Dim conStr As String = "server=localhost; userid=root; database=fdbmsproject"
        Dim con As New MySqlConnection(conStr)
        Dim trans As MySqlTransaction = Nothing



        Dim decPayment As Decimal = 0

        Dim strPayment As String = InputBox("Please enter your money:", "Payment", "0.00")

        If Decimal.TryParse(strPayment, decPayment) Then

            If decPayment > total Then
                Dim change As Decimal = decPayment - total

                MsgBox($"Thank your for purchasing! Here is your change: Php {change.ToString("f2")}")
            ElseIf decPayment = total Then
                MsgBox($"Thank your for purchasing!")
            ElseIf decPayment < total Then
                MsgBox($"Insufficient money, please try again!")
                success = False
                Return success
            End If
            Try
                con.Open()
                trans = con.BeginTransaction()

                ' Insert into subscribers table
                Dim insertSubQuery As String = "
        INSERT INTO subscribers (customer_id, plan_id, subscription_date)
        VALUES (@customerId, @planId, NOW())"
                Dim subCmd As New MySqlCommand(insertSubQuery, con, trans)
                subCmd.Parameters.AddWithValue("@customerId", Session.UserId)
                subCmd.Parameters.AddWithValue("@planId", Session.PlanId)
                subCmd.ExecuteNonQuery()

                ' Now insert selected addons with purchase_date
                For addonIndex As Integer = 0 To 14
                    Dim quantity As Integer = selectedQuantities(addonIndex)
                    If quantity > 0 Then
                        Dim actualAddonId As Integer = addonIndex + 1

                        ' Check if this addon is recurring to handle it properly
                        Dim checkRecurringQuery As String = "SELECT is_recurring FROM addons WHERE addon_id = @addonId"
                        Dim checkCmd As New MySqlCommand(checkRecurringQuery, con, trans)
                        checkCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        Dim isRecurring As Boolean = Convert.ToBoolean(checkCmd.ExecuteScalar())

                        ' Insert addon purchase with purchase_date
                        Dim insertAddonQuery As String = "
                INSERT INTO customer_addons (customer_id, addon_id, quantity, purchase_date) 
                VALUES (@custId, @addonId, @qty, NOW())"
                        Dim addonCmd As New MySqlCommand(insertAddonQuery, con, trans)
                        addonCmd.Parameters.AddWithValue("@custId", Session.UserId)
                        addonCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        addonCmd.Parameters.AddWithValue("@qty", quantity)
                        addonCmd.ExecuteNonQuery()

                        Console.WriteLine($"Inserted addon: ID={actualAddonId}, Qty={quantity}, IsRecurring={isRecurring}")


                    End If
                Next

                'getting the subscriber id of newly subscribed customer for the ticket to be available immediately
                Using getSubId As New MySqlCommand("SELECT u.user_id, u.username, s.subscriber_id FROM subscribers s JOIN
                                                    users u ON u.user_id = s.customer_id WHERE u.user_id = @user_id", con)
                    getSubId.Parameters.AddWithValue("@user_id", Session.UserId)

                    Using fetchSubID As MySqlDataReader = getSubId.ExecuteReader

                        While fetchSubID.Read
                            Session.SubscriberId = fetchSubID.GetInt32("subscriber_id")
                        End While

                    End Using


                End Using



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

        Else

            MsgBox("Please Enter A Valid Amount")
            success = False
            Return success

        End If

    End Function


    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        SavePageQuantities()
        page += 1
        If page > 3 Then
            page = 1
        End If
        pageHandling()
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        SavePageQuantities()
        page -= 1
        If page < 1 Then
            page = 3
        End If
        pageHandling()
    End Sub

    Private Sub btnCart_Click_1(sender As Object, e As EventArgs) Handles btnCart.Click
        ' Save current page quantities first
        SavePageQuantities()

        Dim hasItems As Boolean = False
        Dim failedItems As New List(Of String)
        Dim successCount As Integer = 0

        ' Check if user is logged in
        If Session.UserId <= 0 Then
            MessageBox.Show("Please log in to add items to cart!", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Process all selected items across all pages
        For addonIndex As Integer = 0 To 14 ' 0-14 for addons 1-15
            Dim quantity As Integer = selectedQuantities(addonIndex)
            If quantity > 0 Then
                hasItems = True
                Dim actualAddonId As Integer = addonIndex + 1 ' addon_ids are 1-15

                If AddToCartDatabase(Session.UserId, actualAddonId, quantity) Then
                    successCount += 1
                Else
                    ' Get product name for failed item
                    Dim productName As String = GetProductNameByAddonId(actualAddonId)
                    failedItems.Add(productName)
                End If
            End If
        Next

        If hasItems Then
            If failedItems.Count = 0 Then
                MessageBox.Show($"All {successCount} items added to cart successfully!" & vbCrLf & "Added Php " & addedItemsTotal.ToString("F2") & " to your total.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearAllQuantities() ' Clear after successful cart addition
            Else
                MessageBox.Show("Some items failed to add to cart: " & String.Join(", ", failedItems),
                           "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("No items selected to add to cart!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Function GetProductNameByAddonId(addonId As Integer) As String
        Select Case addonId
            Case 1 : Return "5G Modem/Router"
            Case 2 : Return "WiFi Extender"
            Case 3 : Return "Ethernet Cable (10m)"
            Case 4 : Return "External Antenna"
            Case 5 : Return "Backup Battery Pack"
            Case 6 : Return "Installation Service"
            Case 7 : Return "Netflix Subscription (Monthly)"
            Case 8 : Return "Landline Service (Monthly)"
            Case 9 : Return "Home Network Setup"
            Case 10 : Return "Premium Tech Support"
            Case 11 : Return "Speed Boost 100 Mbps"
            Case 12 : Return "Speed Boost 200 Mbps"
            Case 13 : Return "Data Allowance +100GB"
            Case 14 : Return "Data Allowance +50GB"
            Case 15 : Return "Priority Support Upgrade"
            Case Else : Return "Unknown Product"
        End Select
    End Function

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