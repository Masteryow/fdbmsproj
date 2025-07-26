Imports MySql.Data.MySqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Cart
    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Private cartItems As New List(Of CartItem)

    ' Create a CartItem class for this form
    Public Class CartItem
        Public Property ProductName As String
        Public Property Quantity As Integer
        Public Property Price As Decimal
        Public Property AddonId As Integer
        Public Property Category As String
    End Class

    Dim total As Decimal
    Dim itemTotal As Decimal
    Dim deletionMode As Boolean = False
    Dim planName As String = Session.planName
    Dim planType As String = Session.planType
    Dim planSpeed As String = Session.planSpeed
    Dim data_cap As String = Session.planDataCap
    Dim price As Decimal = Session.planPrice
    Dim status As String = Session.subStatus
    Private Sub Cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load, Me.VisibleChanged




        TextBox1.Text = Session.planName
        TextBox2.Text = Session.planType
        TextBox3.Text = Session.planPrice
        If Session.userRole <> "Subscriber" OrElse Session.subStatus Is DBNull.Value OrElse Session.subStatus.ToString() = "" Then
            HelpToolStripMenuItem.Visible = False

        Else
            SubscriptionToolStripMenuItem.Visible = False
        End If

        If Session.preSubscriber = True Then
            DisplayPlanDetails()
        End If
        ' Show selected plan if exists
        LoadCartFromDatabase()          ' Load actual cart items from DB (hardware only)
        RefreshCartDisplay()            ' Display them in CheckedListBox
        UpdateTotal()
        RefreshCart() ' Show total including plan
        btnCheck.Visible = False
        btnDM.Visible = False
        btnClearCart.Visible = False

        ' Handle different user types
        If Session.fromProduct = False AndAlso Session.preSubscriber = False Then
            ' Existing subscriber accessing cart
            btnCancelOrder.Visible = False ' Don't show cancel for existing subscribers
        ElseIf Session.fromProduct = False Then
            ' New subscription flow
            btnCancelOrder.Visible = True
        Else
            ' Products tab
            btnCancelOrder.Visible = False
        End If
    End Sub

    Public Sub RefreshCart()
        ' Clear current cart display
        ' Load items from database for Session.UserId
        ' Update cart item list and total label

        If Session.fromProduct = True Then
            total = 0
        Else
            total = Session.planPrice
        End If

        txtTotal.Text = "Php " & total.ToString("F2")
        ' Update cart display controls here
    End Sub

    Private Sub LoadCartFromDatabase()
        cartItems.Clear()

        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                ' Join shopping_cart with addons table to get product details - ONLY HARDWARE ADDONS (ID 1-5)
                Dim query As String = "SELECT sc.addon_id, sc.quantity, a.item_name, a.price, a.category " &
                                     "FROM shopping_cart sc " &
                                     "INNER JOIN addons a ON sc.addon_id = a.addon_id " &
                                     "WHERE sc.customer_id = @customerId AND sc.addon_id BETWEEN 1 AND 15"

                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@customerId", Session.UserId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim item As New CartItem()
                            item.AddonId = Convert.ToInt32(reader("addon_id"))
                            item.Quantity = Convert.ToInt32(reader("quantity"))
                            item.ProductName = reader("item_name").ToString()
                            item.Price = Convert.ToDecimal(reader("price"))
                            item.Category = reader("category").ToString()
                            cartItems.Add(item)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Stock is automatically updated by database trigger when items are inserted into customer_addons
    ' No manual stock update needed

    Private Function CheckStockAvailability(cartItems As List(Of CartItem)) As List(Of String)
        Dim outOfStockItems As New List(Of String)

        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                For Each item In cartItems
                    ' Only check stock for hardware addons (ID 1-5)
                    If item.AddonId >= 1 AndAlso item.AddonId <= 5 Then
                        Dim stockQuery As String = "SELECT hs.quantity_available FROM hardware_stocks hs " &
                                             "WHERE hs.addon_id = @addonId"

                        Using cmd As New MySqlCommand(stockQuery, con)
                            cmd.Parameters.AddWithValue("@addonId", item.AddonId)

                            Dim result = cmd.ExecuteScalar()
                            If result IsNot Nothing Then
                                Dim availableStock As Integer = Convert.ToInt32(result)

                                ' Check if requested quantity exceeds available stock
                                If item.Quantity > availableStock Then
                                    If availableStock = 0 Then
                                        outOfStockItems.Add($"{item.ProductName} (Out of Stock)")
                                    Else
                                        outOfStockItems.Add($"{item.ProductName} (Only {availableStock} available, you requested {item.Quantity})")
                                    End If
                                End If
                            Else
                                ' No stock record found - assume out of stock
                                outOfStockItems.Add($"{item.ProductName} (No stock information)")
                            End If
                        End Using
                    End If
                Next
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking stock: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return outOfStockItems
    End Function

    Private Sub DisplayPlanDetails()
        ' Display plan information if coming from a plan selection (new subscribers only)
        If Not Session.fromProduct AndAlso Session.preSubscriber AndAlso Not String.IsNullOrEmpty(Session.planName) Then
            Dim planInfo As String = $"Selected Plan: {Session.planName} - {Session.planType} - Php {Session.planPrice:F2}"
            CheckedListBox1.Items.Add($"PLAN: {Session.planName} - {Session.planType} - Php {Session.planPrice:F2}")
            CheckedListBox1.SetItemCheckState(0, CheckState.Indeterminate) ' Make it non-selectable
        ElseIf Not Session.fromProduct AndAlso Not Session.preSubscriber AndAlso Session.SubscriberId > 0 Then
            ' Existing subscriber - show their current plan info
            Try
                Using con As New MySqlConnection(strCon)
                    con.Open()
                    Dim query As String = "SELECT ip.plan_name, ip.plan_type, ip.price " &
                                     "FROM subscribers s " &
                                     "JOIN internet_plans ip ON s.plan_id = ip.plan_id " &
                                     "WHERE s.subscriber_id = @subscriberId"
                    Using cmd As New MySqlCommand(query, con)
                        cmd.Parameters.AddWithValue("@subscriberId", Session.SubscriberId)
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                Dim currentPlan As String = $"Current Plan: {reader("plan_name")} - {reader("plan_type")} (Hardware Add-ons Only)"
                                CheckedListBox1.Items.Add(currentPlan)
                                CheckedListBox1.SetItemCheckState(0, CheckState.Indeterminate)
                            End If
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                ' If can't load plan info, just show generic message
                CheckedListBox1.Items.Add("Current Subscriber - Hardware Add-ons Only")
                CheckedListBox1.SetItemCheckState(0, CheckState.Indeterminate)
            End Try
        End If
    End Sub

    Private Sub RefreshCartDisplay()
        ' Clear only cart items, keep plan/subscriber info if it exists
        Dim startIndex As Integer = 0
        If Session.preSubscriber = True Then
            startIndex = 1 ' Keep the plan/subscriber info item

        ElseIf Session.subscriberAccess = True Then

            startIndex = 0
        Else
            CheckedListBox1.Items.Clear()
        End If

        ' Remove only cart items, not the plan/subscriber info
        For i As Integer = CheckedListBox1.Items.Count - 1 To startIndex Step -1
            CheckedListBox1.Items.RemoveAt(i)
        Next

        ' Add cart items with stock status - ONLY HARDWARE ADDONS (ID 1-5)
        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                For Each item In cartItems
                    ' Double check that item is hardware addon (ID 1-5)
                    If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                        Dim stockQuery As String = "SELECT hs.quantity_available FROM hardware_stocks hs WHERE hs.addon_id = @addonId"
                        Dim stockStatus As String = ""

                        Using cmd As New MySqlCommand(stockQuery, con)
                            cmd.Parameters.AddWithValue("@addonId", item.AddonId)
                            Dim result = cmd.ExecuteScalar()

                            If result IsNot Nothing Then
                                Dim availableStock As Integer = Convert.ToInt32(result)
                                If availableStock = 0 Then
                                    stockStatus = " [OUT OF STOCK]"
                                ElseIf item.Quantity > availableStock Then
                                    stockStatus = $" [INSUFFICIENT STOCK - Only {availableStock} available]"
                                Else
                                    stockStatus = $" [In Stock: {availableStock}]"
                                End If
                            Else
                                stockStatus = " [NO STOCK INFO]"
                            End If
                        End Using

                        Dim displayText As String = $"{item.ProductName} - Qty: {item.Quantity} - Php {(item.Price * item.Quantity):F2} [{item.Category}]{stockStatus}"
                        CheckedListBox1.Items.Add(displayText)
                    End If
                Next
            End Using
        Catch ex As Exception
            ' Fallback to original display if stock check fails
            For Each item In cartItems
                If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                    Dim displayText As String = $"{item.ProductName} - Qty: {item.Quantity} - Php {(item.Price * item.Quantity):F2} [{item.Category}]"
                    CheckedListBox1.Items.Add(displayText)
                End If
            Next
            MessageBox.Show("Could not load stock information: " & ex.Message, "Stock Check Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub UpdateTotal()
        Dim total As Decimal = 0

        ' Add plan price only for new subscriptions
        If Not Session.fromProduct AndAlso Session.preSubscriber AndAlso Session.planPrice > 0 Then
            total += Session.planPrice
        End If
        ' For existing subscribers, don't add plan price - they're only buying addons

        ' Add cart items total - ONLY HARDWARE ADDONS (ID 1-5)
        For Each item In cartItems
            If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                total += (item.Price * item.Quantity)
            End If
        Next


    End Sub

    Public Function GetCartTotal() As Decimal
        Dim cartTotal As Decimal = 0

        ' Add plan price only for new subscriptions
        If Not Session.fromProduct AndAlso Session.preSubscriber AndAlso Session.planPrice > 0 Then
            cartTotal += Session.planPrice
        End If
        ' For existing subscribers, don't add plan price

        ' Add cart items total - ONLY HARDWARE ADDONS (ID 1-5)
        For Each item In cartItems
            If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                cartTotal += (item.Price * item.Quantity)
            End If
        Next

        Return cartTotal
    End Function

    ' Helper method to get only selected cart items (hardware only)
    Private Function GetSelectedCartItems() As List(Of CartItem)
        Dim selectedItems As New List(Of CartItem)
        Dim startIndex As Integer = 0

        ' Skip plan/subscriber info item if it exists
        If Session.preSubscriber = True Then
            startIndex = 1

        ElseIf Session.subscriberAccess = True Then

            startIndex = 0
        End If

        ' Get selected cart items - ONLY HARDWARE ADDONS (ID 1-5)
        For i As Integer = startIndex To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                Dim cartIndex As Integer = i - startIndex
                If cartIndex >= 0 AndAlso cartIndex < cartItems.Count Then
                    Dim item As CartItem = cartItems(cartIndex)
                    ' Only include hardware addons (ID 1-5)
                    If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                        selectedItems.Add(item)
                    End If
                End If
            End If
        Next

        Return selectedItems
    End Function

    ' Calculate total for selected items only (hardware only)
    Private Function GetSelectedItemsTotal() As Decimal
        Dim selectedTotal As Decimal = 0
        Dim startIndex As Integer = 0

        ' Add plan price only for new subscriptions
        If Not Session.fromProduct AndAlso Session.preSubscriber AndAlso Session.planPrice > 0 Then
            selectedTotal += Session.planPrice
        End If

        ' Skip plan/subscriber info item if it exists
        If Session.preSubscriber = True Then
            startIndex = 1
        Else
            startIndex = 0
        End If

        ' Add only selected cart items - ONLY HARDWARE ADDONS (ID 1-5)
        For i As Integer = startIndex To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                Dim cartIndex As Integer = i - startIndex
                If cartIndex >= 0 AndAlso cartIndex < cartItems.Count Then
                    Dim item As CartItem = cartItems(cartIndex)
                    ' Only include hardware addons (ID 1-5)
                    If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                        selectedTotal += (item.Price * item.Quantity)
                    End If
                End If
            End If
        Next

        Return selectedTotal
    End Function

    Private Sub RemoveSelectedItems()
        ' Check transaction validity only for new subscriptions
        If Session.preSubscriber Then
            Session.CheckTransactionTimeout()
            If Not Session.IsTransactionActive Then
                MessageBox.Show("Session expired. Please select a plan again.")
                ReturnToPlanSelection()
                Return
            End If
        End If

        Dim itemsToRemove As New List(Of Integer)
        Dim startIndex As Integer = 0

        ' Skip plan/subscriber info item if it exists
        If Session.preSubscriber = True Then
            startIndex = 1

        ElseIf Session.subscriberAccess = True Then
            startIndex = 0

        End If

        ' Get selected items (skip plan/subscriber info item) - ONLY HARDWARE ADDONS
        For i As Integer = startIndex To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                Dim cartIndex As Integer = i - startIndex
                If cartIndex >= 0 AndAlso cartIndex < cartItems.Count Then
                    Dim item As CartItem = cartItems(cartIndex)
                    ' Only remove hardware addons (ID 1-5)
                    If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                        itemsToRemove.Add(cartIndex)
                    End If
                End If
            End If
        Next

        ' Remove from database and local list
        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                For Each index In itemsToRemove.OrderByDescending(Function(x) x)
                    If index >= 0 AndAlso index < cartItems.Count Then
                        Dim item As CartItem = cartItems(index)
                        ' Only remove hardware addons (ID 1-5)
                        If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                            ' Remove from database
                            Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId AND addon_id = @addonId"
                            Using cmd As New MySqlCommand(deleteQuery, con)
                                cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                                cmd.Parameters.AddWithValue("@addonId", item.AddonId)
                                cmd.ExecuteNonQuery()
                            End Using

                            ' Remove from local list
                            cartItems.RemoveAt(index)
                        End If
                    End If
                Next
            End Using

            RefreshCartDisplay()


            UpdateTotal()
            MessageBox.Show("Selected hardware items removed from cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error removing items: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ClearCart()
        ' Check transaction validity only for new subscriptions
        '     If Session.IsNewSubscription Then
        ' Session.CheckTransactionTimeout()
        ' If Not Session.IsTransactionActive Then
        ' MessageBox.Show("Session expired. Please select a plan again.")
        '  ReturnToPlanSelection()
        '  Return
        '  End If
        '  End If

        Try
            Using con As New MySqlConnection(strCon)
                con.Open()
                ' Only clear hardware addons (ID 1-5) from cart
                Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId AND addon_id BETWEEN 1 AND 15"
                Using cmd As New MySqlCommand(deleteQuery, con)
                    cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            cartItems.Clear()
            CheckedListBox1.Items.Clear()
            If Session.preSubscriber = True Then
                DisplayPlanDetails()
            End If
            ' Re-add plan/subscriber info if applicable
            UpdateTotal()
            MessageBox.Show("Hardware cart items cleared!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error clearing cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Button event handlers
    Private Sub btnRemoveSelected_Click(sender As Object, e As EventArgs) Handles btnDeletionMode.Click

        If Session.preSubscriber = True Then
            If CheckedListBox1.Items.Count = 1 Then
                MsgBox("There is no item to delete")
                Return

            End If

        ElseIf Session.subscriberAccess = True OrElse Session.fromProduct = True Then

            If CheckedListBox1.Items.Count = 0 Then
                MsgBox("There is no item to delete")
                Return

            End If

        End If


        btnCheck.Visible = True
        btnDM.Visible = True
        deletionMode = True
        btnClearCart.Visible = True
        lblDeletionMode.Visible = True
        btnDeletionMode.Enabled = False
        btnDM.Enabled = True

        If Session.preSubscriber = True Then
            For i As Integer = 1 To CheckedListBox1.Items.Count - 1
                CheckedListBox1.SetItemChecked(i, False)
            Next
            total = Session.planPrice
        ElseIf Session.fromProduct = True OrElse Session.subscriberAccess = True Then

            For i As Integer = 0 To CheckedListBox1.Items.Count - 1
                CheckedListBox1.SetItemChecked(i, False)
            Next
            total = 0
        End If

        txtTotal.Text = "Php " & total.ToString("F2")
    End Sub

    Private Sub btnClearCart_Click(sender As Object, e As EventArgs) Handles btnClearCart.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to clear all hardware items from cart?",
                                                    "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            ClearCart()
            deletionMode = False
            btnCheck.Visible = False
            btnDM.Visible = False
            lblDeletionMode.Visible = False
            btnClearCart.Visible = False
            btnDeletionMode.Enabled = True
        End If
    End Sub

    Private Sub btnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click
        ' Check transaction validity before checkout
        If deletionMode = True Then
            MsgBox("Please Exit Deletion Mode First!")
            Return
        End If

        ' Get selected items and validate (hardware only)
        Dim selectedItems As List(Of CartItem) = GetSelectedCartItems()

        ' Check if we have items to process
        If selectedItems.Count = 0 AndAlso (Session.fromProduct OrElse (Not Session.preSubscriber AndAlso Session.planPrice = 0)) Then
            MessageBox.Show("Please select hardware items to checkout!", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' **Check stock availability for selected hardware items**
        Dim outOfStockItems As List(Of String) = CheckStockAvailability(selectedItems)

        If outOfStockItems.Count > 0 Then
            Dim stockMessage As String = "The following hardware items are out of stock or have insufficient quantity:" & vbCrLf & vbCrLf
            For Each item In outOfStockItems
                stockMessage += "• " & item & vbCrLf
            Next
            stockMessage += vbCrLf & "Please remove these items from your selection or reduce the quantities to proceed with checkout."

            MessageBox.Show(stockMessage, "Stock Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Calculate total for selected items only
        Dim checkoutTotal As Decimal = GetSelectedItemsTotal()

        ' If no total (no plan and no selected items), show error
        If checkoutTotal = 0 Then
            MessageBox.Show("Please select hardware items to checkout!", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim message As String = $"Total amount: Php {checkoutTotal:F2}" & vbCrLf & "Enter payment amount:"

        Dim paymentInput As String = InputBox(message, "Payment")

        ' Check if user cancelled or entered empty value
        If String.IsNullOrEmpty(paymentInput) Then
            Return ' User cancelled, keep transaction active
        End If

        Dim paymentAmount As Decimal
        If Not Decimal.TryParse(paymentInput, paymentAmount) Then
            MessageBox.Show("Invalid payment amount entered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Keep transaction active for retry
        End If

        If paymentAmount < checkoutTotal Then
            MessageBox.Show("Insufficient payment amount!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Keep transaction active for retry
        End If

        ' Process payment based on purchase type - using selected items only
        Dim purchaseSuccess As Boolean = False
        If Session.fromProduct = True Then
            ' Direct product purchase - selected items only
            purchaseSuccess = ProcessSelectedDirectPurchase(selectedItems)
        ElseIf Session.preSubscriber Then
            ' Plan with selected addons purchase
            purchaseSuccess = ProcessPlanWithSelectedAddonsPurchase(selectedItems)
            Session.userRole = "Subscriber"

        Else
            ' Existing subscriber buying selected addons
            purchaseSuccess = ProcessSelectedAddonsForExistingSubscriber(selectedItems)
        End If

        If purchaseSuccess Then
            Dim change As Decimal = paymentAmount - checkoutTotal
            Dim resultMessage As String = "Payment successful!" & vbCrLf &
                                 $"Total: Php {checkoutTotal:F2}" & vbCrLf &
                                 $"Payment: Php {paymentAmount:F2}" & vbCrLf &
                                 $"Change: Php {change:F2}" & vbCrLf &
                                 "Thank you for your purchase!"

            MessageBox.Show(resultMessage, "Payment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Payment successful - complete transaction and remove only purchased items
            If Session.preSubscriber Then
                Session.EndTransaction(True)
            End If

            ' Remove only the items that were actually purchased
            RemovePurchasedItemsFromCart(selectedItems)

            ' Refresh display and recalculate total
            RefreshCartDisplay()
            UpdateTotal()

            If Session.IsNewSubscription = True Then

                Session.planName = planName
                Session.planType = planType
                Session.planSpeed = planSpeed
                Session.planDataCap = data_cap
                Session.planPrice = price
                Session.subStatus = status
                cartItems.Clear()

                For Each form As Form In Application.OpenForms
                    If form.Name = "Addon" Then
                        form.Close()
                        Exit For
                    End If
                Next

                subscribers.Show()

            Else
                Main.Show()
            End If

            Me.Close()
        Else
            MessageBox.Show("Purchase failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Process selected direct purchases only (hardware items)
    Private Function ProcessSelectedDirectPurchase(selectedItems As List(Of CartItem)) As Boolean
        Dim success As Boolean = False
        Dim con As New MySqlConnection(strCon)
        Dim trans As MySqlTransaction = Nothing

        Try
            con.Open()
            trans = con.BeginTransaction()

            ' Insert only selected hardware cart items into customer_addons
            For Each item In selectedItems
                If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                    Dim insertQuery As String = "INSERT INTO customer_addons (customer_id, addon_id, quantity, purchase_date) " &
                                               "VALUES (@customerId, @addonId, @quantity, NOW())"
                    Using cmd As New MySqlCommand(insertQuery, con, trans)
                        cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                        cmd.Parameters.AddWithValue("@addonId", item.AddonId)
                        cmd.Parameters.AddWithValue("@quantity", item.Quantity)
                        cmd.ExecuteNonQuery()
                    End Using
                End If
            Next

            trans.Commit()
            success = True

        Catch ex As Exception
            MessageBox.Show("Error during direct purchase: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    ' Process selected addons for existing subscriber (hardware only)
    Private Function ProcessSelectedAddonsForExistingSubscriber(selectedItems As List(Of CartItem)) As Boolean
        Dim success As Boolean = False
        Dim con As New MySqlConnection(strCon)
        Dim trans As MySqlTransaction = Nothing

        Try
            con.Open()
            trans = con.BeginTransaction()

            ' Insert only selected hardware cart items into customer_addons for existing subscriber
            For Each item In selectedItems
                If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                    Dim insertQuery As String = "INSERT INTO customer_addons (customer_id, addon_id, quantity, purchase_date) " &
                                           "VALUES (@customerId, @addonId, @quantity, NOW())"
                    Using cmd As New MySqlCommand(insertQuery, con, trans)
                        cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                        cmd.Parameters.AddWithValue("@addonId", item.AddonId)
                        cmd.Parameters.AddWithValue("@quantity", item.Quantity)
                        cmd.ExecuteNonQuery()
                    End Using
                End If
            Next

            trans.Commit()
            success = True

        Catch ex As Exception
            MessageBox.Show("Error during addon purchase: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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


    ' Process plan with selected addons (hardware only) - FIXED VERSION
    Private Function ProcessPlanWithSelectedAddonsPurchase(selectedItems As List(Of CartItem)) As Boolean
        Dim success As Boolean = False
        Dim con As New MySqlConnection(strCon)
        Dim trans As MySqlTransaction = Nothing

        Try
            con.Open()
            trans = con.BeginTransaction()

            ' Insert subscription record
            Dim insertSubQuery As String = "INSERT INTO subscribers (customer_id, plan_id, subscription_date, status) " &
                                      "VALUES (@customerId, @planId, NOW(), 'Pending')"
            Dim subscriberId As Integer
            Using cmd As New MySqlCommand(insertSubQuery, con, trans)
                cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                cmd.Parameters.AddWithValue("@planId", Session.PlanId)
                cmd.ExecuteNonQuery()
                subscriberId = CInt(cmd.LastInsertedId)
            End Using

            Using getId As New MySqlCommand("SELECT u.user_id, s.subscriber_id FROM users u JOIN subscribers s ON u.user_id = s.customer_id WHERE u.user_id = @user_id", con, trans)
                getId.Parameters.AddWithValue("@user_id", Session.UserId)
                Using fetchId As MySqlDataReader = getId.ExecuteReader
                    While fetchId.Read
                        Session.SubscriberId = fetchId.GetInt32("subscriber_id")
                    End While
                End Using
            End Using

            ' Insert all selected addon items (1-15) into customer_addons
            For Each item In selectedItems
                If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                    Dim insertAddonQuery As String = "INSERT INTO customer_addons (customer_id, addon_id, quantity, purchase_date) " &
                                               "VALUES (@customerId, @addonId, @quantity, NOW())"
                    Using cmd As New MySqlCommand(insertAddonQuery, con, trans)
                        cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                        cmd.Parameters.AddWithValue("@addonId", item.AddonId)
                        cmd.Parameters.AddWithValue("@quantity", item.Quantity)
                        cmd.ExecuteNonQuery()
                    End Using
                End If
            Next

            ' Create billing record for PLAN ONLY (not including addons)
            Dim billingQuery As String = "INSERT INTO billing_records (subscriber_id, billing_month, total_amount, due_date, status) " &
            "VALUES (@subscriber_id, CURDATE(), @totalAmount, DATE_ADD(NOW(), INTERVAL 1 MONTH), 'Paid')"
            Dim billingId As Integer
            Using cmd As New MySqlCommand(billingQuery, con, trans)
                cmd.Parameters.AddWithValue("@subscriber_id", subscriberId)
                cmd.Parameters.AddWithValue("@totalAmount", Session.planPrice) ' ONLY plan price, not total
                cmd.ExecuteNonQuery()
                billingId = CInt(cmd.LastInsertedId)
            End Using

            ' Create payment record for PLAN ONLY
            Dim paymentQuery As String = "INSERT INTO payments (billing_id, amount, payment_date) " &
            "VALUES (@billing_id, @amount, CURDATE())"
            Using cmd As New MySqlCommand(paymentQuery, con, trans)
                cmd.Parameters.AddWithValue("@billing_id", billingId)
                cmd.Parameters.AddWithValue("@amount", GetSelectedItemsTotal()) ' ONLY plan price, not total
                cmd.ExecuteNonQuery()
            End Using

            ' All addons (1-15) are already inserted into customer_addons table above
            ' No separate billing records needed for any addons - they're one-time purchases

            trans.Commit()
            success = True

        Catch ex As Exception
            MessageBox.Show("Error during plan purchase: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        Session.IsNewSubscription = True
        Session.cashOnHand = Session.planPrice ' This should only be the plan price

        Return success
    End Function

    ' Remove only the purchased items from cart and database (hardware only)
    Private Sub RemovePurchasedItemsFromCart(purchasedItems As List(Of CartItem))
        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                ' Remove purchased hardware items from database
                For Each item In purchasedItems
                    If item.AddonId >= 1 AndAlso item.AddonId <= 15 Then
                        Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId AND addon_id = @addonId"
                        Using cmd As New MySqlCommand(deleteQuery, con)
                            cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                            cmd.Parameters.AddWithValue("@addonId", item.AddonId)
                            cmd.ExecuteNonQuery()
                        End Using
                    End If
                Next
            End Using

            ' Remove from local cart items list
            For Each purchasedItem In purchasedItems
                If purchasedItem.AddonId >= 1 AndAlso purchasedItem.AddonId <= 5 Then
                    For i As Integer = cartItems.Count - 1 To 0 Step -1
                        If cartItems(i).AddonId = purchasedItem.AddonId Then
                            cartItems.RemoveAt(i)
                            Exit For
                        End If
                    Next
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Error removing purchased items: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Add navigation methods
    Private Sub btnContinueShopping_Click(sender As Object, e As EventArgs) Handles btnContinueShopping.Click
        ' Return to addon selection - keep transaction active
        If Session.userRole = "Customer" AndAlso Session.fromProduct = True Then
            Session.preSubscriber = False
            Session.fromProduct = True
            Session.planName = ""
            Session.planPrice = 0
            Session.planType = ""


        End If

        Addon.Show()
        Me.Hide() 'Me.Hide
    End Sub

    Private Sub btnCancelOrder_Click(sender As Object, e As EventArgs) Handles btnCancelOrder.Click
        ' Explicitly cancel the transaction
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel your order?",
                                   "Cancel Order",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ClearCart()
            Session.EndTransaction(False)
            ' Clear the database cart as well
            ReturnToPlanSelection()
        End If
    End Sub

    ' Add this field to your form class
    Private isProgrammaticClose As Boolean = False

    Public Sub delete()
        If Session.preSubscriber AndAlso Session.subscriberAccess = False Then
            Try
                Using con As New MySqlConnection(strCon)
                    con.Open()
                    ' Only clear hardware addons (ID 1-5) from cart
                    Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId AND addon_id BETWEEN 1 AND 15"
                    Using cmd As New MySqlCommand(deleteQuery, con)
                        cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                cartItems.Clear()
                CheckedListBox1.Items.Clear()
                DisplayPlanDetails() ' Re-add plan if applicable
                UpdateTotal()

                Session.EndTransaction(False)

            Catch ex As Exception
                MessageBox.Show("Error clearing cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub form_closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Only clear cart and end transaction for new subscriptions
        delete()
    End Sub

    Private Sub ReturnToPlanSelection()
        Subscription.Show()
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtTotal.TextChanged
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        If deletionMode = False Then
            Dim fullText As String = CheckedListBox1.Items(e.Index).ToString()
            Dim itemChanging As String = fullText.Split("-"c)(0).Trim()

            Dim cartItems As New List(Of CartItem)
            Dim itemName As String = ""

            Using con As New MySqlConnection(DatabaseHelper.ConnectionString)
                con.Open()

                ' Only check hardware addons (ID 1-5)
                Dim query As String = "SELECT sc.addon_id, a.item_name, sc.quantity, a.price FROM shopping_cart sc JOIN 
                                    addons a ON sc.addon_id = a.addon_id WHERE sc.customer_id = @customerId AND item_name = @itemName AND sc.addon_id BETWEEN 1 AND 15"
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                    cmd.Parameters.AddWithValue("@itemName", itemChanging)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            itemTotal = reader.GetInt32("quantity") * reader.GetDecimal("price")

                            If e.NewValue = CheckState.Checked Then
                                total += itemTotal
                            ElseIf e.NewValue = CheckState.Unchecked Then
                                total -= itemTotal
                            End If
                        End While
                    End Using
                End Using
            End Using
            txtTotal.Text = "Php " & total.ToString("F2")
        End If
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        If Session.preSubscriber = True Then
            If CheckedListBox1.SelectedIndex = 0 Then
                MsgBox("Plan cannot be deleted")
                Return
            End If
        End If

        If CheckedListBox1.CheckedItems.Count = 1 AndAlso Session.preSubscriber = True Then
            MessageBox.Show("Please select hardware items to remove!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        ElseIf CheckedListBox1.CheckedItems.Count = 0 AndAlso (Session.fromProduct = True OrElse Session.subscriberAccess = True) Then
            MessageBox.Show("Please select hardware items to remove!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to remove selected hardware items?",
                                                    "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            RemoveSelectedItems()
        End If
    End Sub

    Private Sub btnDM_Click(sender As Object, e As EventArgs) Handles btnDM.Click
        deletionMode = False
        btnCheck.Visible = False
        btnDM.Visible = False
        lblDeletionMode.Visible = False
        btnClearCart.Visible = False
        btnDeletionMode.Enabled = True

        RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck

        If Session.preSubscriber = True Then
            For i As Integer = 1 To CheckedListBox1.Items.Count - 1
                CheckedListBox1.SetItemChecked(i, False)
            Next
        ElseIf Session.subscriberAccess = True OrElse Session.fromProduct = True Then
            For i As Integer = 0 To CheckedListBox1.Items.Count - 1
                CheckedListBox1.SetItemChecked(i, False)
            Next
        End If

        AddHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
    End Sub

    Private Sub HomeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem1.Click


        If Session.userRole = "Subscriber" Then
            subscribers.Show()
        ElseIf Session.userRole = "Customer" Then
            Session.preSubscriber = False
                Session.fromProduct = True
                Session.planName = ""
                Session.planPrice = 0
                Session.planType = ""

            For Each form As Form In Application.OpenForms
                    If form.Name = "Addon" Then
                        form.Close()
                        Exit For
                    End If
                Next
                Main.Show()
        End If

        Me.Close()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubscriptionToolStripMenuItem.Click



        If Session.userRole = "Customer" Then
            Session.preSubscriber = False
            Session.fromProduct = True
            Session.planName = ""
            Session.planPrice = 0
            Session.planType = ""

            For Each form As Form In Application.OpenForms
                If form.Name = "Addon" Then
                    form.Close()
                    Exit For
                End If
            Next

            Subscription.Show()
            Me.Close()

        End If



    End Sub

    Private Sub ProductsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsToolStripMenuItem.Click
        Addon.Show()
        Me.Hide()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

    End Sub

    Private Sub TicketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TicketToolStripMenuItem.Click
        Tickets.Show()
        Me.Close()
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub
End Class