Imports MySql.Data.MySqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

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

    Private skipClosingEvent As Boolean = False
    Private Sub Cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load, Me.VisibleChanged

        planName = Session.planName
        planType = Session.planType
        planSpeed = Session.planSpeed
        data_cap = Session.planDataCap
        price = Session.planPrice
        status = Session.subStatus
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

                ' Join shopping_cart with addons table to get product details - ALL ADDONS
                Dim query As String = "SELECT sc.addon_id, sc.quantity, a.item_name, a.price, a.category " &
                                     "FROM shopping_cart sc " &
                                     "INNER JOIN addons a ON sc.addon_id = a.addon_id " &
                                     "WHERE sc.customer_id = @customerId"

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
                    ' ONLY CHECK STOCK FOR HARDWARE ITEMS
                    If item.Category.ToUpper() = "HARDWARE" Then
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
                                ' No stock record found for hardware - assume out of stock
                                outOfStockItems.Add($"{item.ProductName} (No stock information)")
                            End If
                        End Using
                    End If
                    ' For non-hardware items (Service, Plan Upgrade), skip stock checking entirely
                Next
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking stock: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return outOfStockItems
    End Function

    Private Sub DisplayPlanDetails()
        ' Display plan information if coming from a plan selection (new subscribers only)
        If Session.preSubscriber = True Then
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
        ElseIf Session.subscriberAccess = True OrElse Session.fromProduct = True Then
            startIndex = 0
        Else
            CheckedListBox1.Items.Clear()
        End If

        ' Remove only cart items, not the plan/subscriber info
        For i As Integer = CheckedListBox1.Items.Count - 1 To startIndex Step -1
            CheckedListBox1.Items.RemoveAt(i)
        Next

        ' Add cart items with stock status - ONLY SHOW STOCK INFO FOR HARDWARE
        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                For Each item In cartItems
                    Dim stockStatus As String = ""

                    ' ONLY SHOW STOCK INFO FOR HARDWARE ITEMS
                    If item.Category.ToUpper() = "HARDWARE" Then
                        Dim stockQuery As String = "SELECT hs.quantity_available FROM hardware_stocks hs WHERE hs.addon_id = @addonId"

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
                    End If
                    ' For non-hardware items, stockStatus remains empty (no stock info shown)

                    Dim displayText As String = $"{item.ProductName} - Qty: {item.Quantity} - Php {(item.Price * item.Quantity):F2} [{item.Category}]{stockStatus}"
                    CheckedListBox1.Items.Add(displayText)
                Next
            End Using
        Catch ex As Exception
            ' Fallback to original display if stock check fails
            For Each item In cartItems
                Dim displayText As String = $"{item.ProductName} - Qty: {item.Quantity} - Php {(item.Price * item.Quantity):F2} [{item.Category}]"
                CheckedListBox1.Items.Add(displayText)
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

        ' Add cart items total - ALL ITEMS (Hardware, Service, Plan Upgrade)
        For Each item In cartItems
            total += (item.Price * item.Quantity)
        Next
    End Sub

    Public Function GetCartTotal() As Decimal
        Dim cartTotal As Decimal = 0

        ' Add plan price only for new subscriptions
        If Not Session.fromProduct AndAlso Session.preSubscriber AndAlso Session.planPrice > 0 Then
            cartTotal += Session.planPrice
        End If
        ' For existing subscribers, don't add plan price

        ' Add cart items total - ALL ITEMS (Hardware, Service, Plan Upgrade)
        For Each item In cartItems
            cartTotal += (item.Price * item.Quantity)
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

        ' Get selected cart items - ALL CATEGORIES (Hardware, Service, Plan Upgrade)
        For i As Integer = startIndex To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                Dim cartIndex As Integer = i - startIndex
                If cartIndex >= 0 AndAlso cartIndex < cartItems.Count Then
                    Dim item As CartItem = cartItems(cartIndex)
                    selectedItems.Add(item) ' Include all items regardless of category
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

        ' Add only selected cart items - ALL CATEGORIES (Hardware, Service, Plan Upgrade)
        For i As Integer = startIndex To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                Dim cartIndex As Integer = i - startIndex
                If cartIndex >= 0 AndAlso cartIndex < cartItems.Count Then
                    Dim item As CartItem = cartItems(cartIndex)
                    selectedTotal += (item.Price * item.Quantity) ' Include all items regardless of category
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

        ' Get selected items (skip plan/subscriber info item) - ALL CART ITEMS
        For i As Integer = startIndex To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                ' Additional safety check - don't allow plan deletion
                If Session.preSubscriber = True AndAlso i = 0 Then
                    MessageBox.Show("Plan cannot be deleted!", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Continue For
                End If

                Dim cartIndex As Integer = i - startIndex
                If cartIndex >= 0 AndAlso cartIndex < cartItems.Count Then
                    itemsToRemove.Add(cartIndex)
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

                        ' Remove ANY cart item - no addon_id restrictions
                        Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId AND addon_id = @addonId"
                        Using cmd As New MySqlCommand(deleteQuery, con)
                            cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                            cmd.Parameters.AddWithValue("@addonId", item.AddonId)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' Remove from local list
                        cartItems.RemoveAt(index)
                    End If
                Next
            End Using

            RefreshCartDisplay()
            UpdateTotal()
            MessageBox.Show("Selected items removed from cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error removing items: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub ClearCart()
        Try
            Using con As New MySqlConnection(strCon)
                con.Open()
                ' Clear ALL cart items - no addon_id restrictions
                Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId"
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
            MessageBox.Show("All cart items cleared!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to clear all items from cart?",
                                                "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            ClearCart()
            deletionMode = False
            btnCheck.Visible = False
            btnDM.Visible = False
            lblDeletionMode.Visible = False
            btnClearCart.Visible = False
            btnDeletionMode.Enabled = True

            If Session.userRole = "Subscriber" Then
                For Each form As Form In Application.OpenForms
                    If form.Name = "Addon" Then
                        form.Close()
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click
        ' Check transaction validity before checkout
        If deletionMode = True Then
            MsgBox("Please Exit Deletion Mode First!")
            Return
        End If

        ' Get selected items and validate (all categories)
        Dim selectedItems As List(Of CartItem) = GetSelectedCartItems()

        ' Check if we have items to process
        If selectedItems.Count = 0 AndAlso (Session.fromProduct OrElse (Not Session.preSubscriber AndAlso Session.planPrice = 0)) Then
            MessageBox.Show("Please select items to checkout!", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' **Check stock availability ONLY for selected HARDWARE items**
        Dim hardwareItems As New List(Of CartItem)
        For Each item In selectedItems
            If item.Category.ToUpper() = "HARDWARE" Then
                hardwareItems.Add(item)
            End If
        Next

        Dim outOfStockItems As List(Of String) = CheckStockAvailability(hardwareItems)

        If outOfStockItems.Count > 0 Then
            Dim stockMessage As String = "The following hardware items are out of stock or have insufficient quantity:" & vbCrLf & vbCrLf
            For Each item In outOfStockItems
                stockMessage += "• " & item & vbCrLf
            Next
            stockMessage += vbCrLf & "Please remove these items from your selection or reduce the quantities to proceed with checkout."

            MessageBox.Show(stockMessage, "Stock Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Calculate total for selected items only (all categories)
        Dim checkoutTotal As Decimal = GetSelectedItemsTotal()

        ' If no total (no plan and no selected items), show error
        If checkoutTotal = 0 Then
            MessageBox.Show("Please select items to checkout!", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        ' Process payment based on purchase type - using selected items only (all categories)
        Dim purchaseSuccess As Boolean = False
        If Session.fromProduct = True Then
            ' Direct product purchase - selected items only (all categories)
            purchaseSuccess = ProcessSelectedDirectPurchase(selectedItems)
        ElseIf Session.preSubscriber = True Then
            ' Plan with selected addons purchase (all categories)
            purchaseSuccess = ProcessPlanWithSelectedAddonsPurchase(selectedItems)
            Session.userRole = "Subscriber"
        Else
            ' Existing subscriber buying selected addons (all categories)
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

            Dim newMoneyGiven As Decimal = paymentAmount - Session.planPrice
            Dim newTotal As Decimal = checkoutTotal - Session.planPrice
            Dim withPlanChange As Decimal = newMoneyGiven - newTotal



            Try

                If Session.preSubscriber = True Then
                    GenerateSimplePDFReceipt(selectedItems, newTotal, newMoneyGiven, withPlanChange)

                Else
                    GenerateSimplePDFReceipt(selectedItems, checkoutTotal, paymentAmount, change)
                End If

            Catch ex As Exception
                MessageBox.Show("Receipt generated successfully, but PDF creation failed: " & ex.Message, "PDF Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try


            ' Payment successful - complete transaction and remove only purchased items
            ' Remove only the items that were actually purchased
            RemovePurchasedItemsFromCart(selectedItems)

            ' Refresh display and recalculate total
            RefreshCartDisplay()
            UpdateTotal()

            If Session.preSubscriber = True Then
                Session.planName = planName
                Session.planType = planType
                Session.planSpeed = planSpeed
                Session.planDataCap = data_cap
                Session.planPrice = price
                Session.subStatus = status

                subscribers.Show()
                cartItems.Clear()


            ElseIf Session.subscriberAccess = True Then
                subscribers.Show()


            ElseIf Session.userRole = "Customer" Then
                Main.Show()
            End If
            Session.EndTransaction(True)
            Me.Close()
        Else
            MessageBox.Show("Purchase failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub GenerateSimplePDFReceipt(selectedItems As List(Of CartItem), totalAmount As Decimal, paymentAmount As Decimal, change As Decimal)
        Try
            ' Create PDF folder
            Dim pdfPath As String = Application.StartupPath
            Dim pdfFolder As String = Path.Combine(pdfPath, "Receipts")

            If Not Directory.Exists(pdfFolder) Then
                Directory.CreateDirectory(pdfFolder)
            End If

            ' Create filename
            Dim timestamp As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")
            Dim receiptFileName As String = $"Receipt_{timestamp}.pdf"
            Dim pdfFilePath As String = Path.Combine(pdfFolder, receiptFileName)

            ' Create PDF
            Dim receipt As New Document(PageSize.A4)
            PdfWriter.GetInstance(receipt, New FileStream(pdfFilePath, FileMode.Create))
            receipt.Open()

            ' Simple fonts
            Dim titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)
            Dim normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12)
            Dim boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)

            ' Title
            Dim title As New Paragraph("SkyLink Receipt", titleFont)
            title.Alignment = Element.ALIGN_CENTER
            receipt.Add(title)
            receipt.Add(New Paragraph(" "))

            ' Date and Time
            receipt.Add(New Paragraph($"Date: {DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")}", normalFont))
            receipt.Add(New Paragraph(" "))

            ' Plan (if new subscription)
            If Session.preSubscriber AndAlso Session.planPrice > 0 Then
                receipt.Add(New Paragraph("PLAN:", boldFont))
                receipt.Add(New Paragraph($"{Session.planName} - {Session.planType} - Php {Session.planPrice:F2}", normalFont))
                receipt.Add(New Paragraph(" "))
            End If

            ' Items purchased
            If selectedItems.Count > 0 Then
                receipt.Add(New Paragraph("ITEMS ORDERED:", boldFont))
                receipt.Add(New Paragraph(" "))

                ' Simple table for items
                Dim itemsTable As New PdfPTable(4)
                itemsTable.WidthPercentage = 100
                itemsTable.SetWidths(New Single() {3, 1, 1, 1})

                ' Headers
                itemsTable.AddCell(New PdfPCell(New Phrase("Item", boldFont)))
                itemsTable.AddCell(New PdfPCell(New Phrase("Qty", boldFont)))
                itemsTable.AddCell(New PdfPCell(New Phrase("Price", boldFont)))
                itemsTable.AddCell(New PdfPCell(New Phrase("Total", boldFont)))

                ' Add each item
                For Each item In selectedItems
                    Dim itemTotal As Decimal = item.Price * item.Quantity
                    itemsTable.AddCell(New PdfPCell(New Phrase(item.ProductName, normalFont)))
                    itemsTable.AddCell(New PdfPCell(New Phrase(item.Quantity.ToString(), normalFont)))
                    itemsTable.AddCell(New PdfPCell(New Phrase($"Php {item.Price:F2}", normalFont)))
                    itemsTable.AddCell(New PdfPCell(New Phrase($"Php {itemTotal:F2}", normalFont)))
                Next

                receipt.Add(itemsTable)
                receipt.Add(New Paragraph(" "))
            End If

            ' Payment summary
            receipt.Add(New Paragraph("PAYMENT SUMMARY:", boldFont))
            receipt.Add(New Paragraph($"Total Amount: Php {totalAmount:F2}", normalFont))
            receipt.Add(New Paragraph($"Money Given: Php {paymentAmount:F2}", normalFont))
            receipt.Add(New Paragraph($"Change: Php {change:F2}", normalFont))
            receipt.Add(New Paragraph(" "))

            If Session.preSubscriber = True Then

                Dim note As New Paragraph
                note.Add(New Paragraph("Note: Your payment was divided into TWO separate receipt, the receipt for your plan subscription was sent via email. Hence, the amount you gave was also divided. You may see the exact amount of plan price was substracted on the money you gave as it was transferred in another receipt", normalFont))
                receipt.Add(note)
                receipt.Add("")
            End If
            ' Footer
            receipt.Add(New Paragraph("Thank you for your purchase!", normalFont))

            receipt.Close()



        Catch ex As Exception
            Throw New Exception("Error creating PDF receipt: " & ex.Message)
        End Try
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

        If deletionMode = True Then
            MsgBox("Please exit from Deletion Mode first.", MsgBoxStyle.Exclamation, "Notice")
            Exit Sub
        End If

        ' Return to addon selection - keep transaction active
        If Session.userRole = "Customer" AndAlso Session.fromProduct = True Then
            Session.preSubscriber = False
            Session.fromProduct = True
            Session.planName = ""
            Session.planPrice = 0
            Session.planType = ""

        End If

        products.Show()
        CloseProgrammatically() 'Me.Hide
    End Sub

    Private Sub btnCancelOrder_Click(sender As Object, e As EventArgs) Handles btnCancelOrder.Click
        ' Explicitly cancel the transaction
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel your order?",
                                   "Cancel Order",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ClearCart()

            For Each form As Form In Application.OpenForms
                If form.Name = "Addon" Then
                    form.Close()
                    Exit For
                End If
            Next
            Session.EndTransaction(False)
            ' Clear the database cart as well
            ReturnToPlanSelection()
        End If
    End Sub



    Public Sub delete()
        If Session.preSubscriber AndAlso Session.subscriberAccess = False Then
            Try
                Using con As New MySqlConnection(strCon)
                    con.Open()
                    ' Clear ALL cart items - no addon_id restrictions
                    Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId"
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

        If skipClosingEvent Then
            ' Skip all processing when closing programmatically
            Return
        End If


        ' Only handle user closing (X button click)
        If e.CloseReason = CloseReason.UserClosing Then

            delete()
            ' Your code here for X button click
        End If



    End Sub

    Public Sub CloseProgrammatically()
        skipClosingEvent = True
        Me.Close()
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
        ' Additional safety check - don't allow plan deletion
        If Session.preSubscriber = True Then
            If CheckedListBox1.SelectedIndex = 0 Then
                MsgBox("Plan cannot be deleted")
                Return
            End If
        End If

        If CheckedListBox1.CheckedItems.Count = 1 AndAlso Session.preSubscriber = True Then
            MessageBox.Show("Please select items to remove!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        ElseIf CheckedListBox1.CheckedItems.Count = 0 AndAlso (Session.fromProduct = True OrElse Session.subscriberAccess = True) Then
            MessageBox.Show("Please select items to remove!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to remove selected items?",
                                                "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            RemoveSelectedItems()

            If Session.userRole = "Subscriber" Then
                For Each form As Form In Application.OpenForms
                    If form.Name = "Addon" Then
                        form.Close()
                        Exit For
                    End If
                Next
            End If
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

        'added12
        If deletionMode = True Then
            MsgBox("Please exit from Deletion Mode first.", MsgBoxStyle.Exclamation, "Notice")
            Exit Sub
        End If

        If Session.preSubscriber = True Then 'customer who added something on cart with plan


            Dim result As DialogResult = MsgBox("This action will clear your cart due to incomplete transaction, do you want to continue?", MsgBoxStyle.YesNo)

            If result = DialogResult.Yes Then

                delete()
                Session.preSubscriber = False

                Main.Show()
                Me.Close()
            Else

                Exit Sub
            End If

        ElseIf Session.fromProduct = True AndAlso Session.subscriberAccess = False Then 'ordinary customer

            Main.Show()
            Session.fromProduct = False

            Me.Close()

        End If

        If Session.userRole = "Subscriber" Then
            subscribers.Show()
        ElseIf Session.userRole = "Customer" Then
            Session.preSubscriber = False
            Session.fromProduct = True
            Session.planName = ""
            Session.planPrice = 0
            Session.planType = ""


            Main.Show()
        End If

        Me.Close()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubscriptionToolStripMenuItem.Click
        If deletionMode = True Then
            MsgBox("Please exit from Deletion Mode first.", MsgBoxStyle.Exclamation, "Notice")
            Exit Sub
        End If
           If Session.preSubscriber = True Then 'customer who added something on cart with plan


            Dim result As DialogResult = MsgBox("This action will clear your cart due to incomplete transaction, do you want to continue?", MsgBoxStyle.YesNo)

            If result = DialogResult.Yes Then

                delete()
                Session.preSubscriber = False

                Subscription.Show()
                Me.Close()
            Else

                Exit Sub
            End If


        ElseIf Session.fromProduct = True AndAlso Session.subscriberAccess = False Then 'ordinary customer

            Subscription.Show()
            Session.fromProduct = False

            Me.Close()

        End If





        If Session.userRole = "Customer" Then
            Session.preSubscriber = False
            Session.fromProduct = True
            Session.planName = ""
            Session.planPrice = 0
            Session.planType = ""


            Subscription.Show()
            Me.Close()

        End If



    End Sub

    Private Sub ProductsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsToolStripMenuItem.Click
        If deletionMode = True Then
            MsgBox("Please exit from Deletion Mode first.", MsgBoxStyle.Exclamation, "Notice")
            Exit Sub
        End If

        products.Show()
        Me.Hide()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

    End Sub

    Private Sub TicketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TicketToolStripMenuItem.Click
        If deletionMode = True Then
            MsgBox("Please exit from Deletion Mode first.", MsgBoxStyle.Exclamation, "Notice")
            Exit Sub
        End If

        Tickets.Show()
        Me.Close()
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub HelpToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

    End Sub

    Private Sub btnupdatequantity_Click(sender As Object, e As EventArgs) Handles btnupdatequantity.Click
        ' Check if in deletion mode
        If deletionMode = True Then
            MsgBox("Please exit from Deletion Mode first.", MsgBoxStyle.Exclamation, "Notice")
            Return
        End If

        ' Check if any items are selected
        Dim hasSelectedItems As Boolean = False
        Dim startIndex As Integer = 0

        ' Skip plan/subscriber info item if it exists
        If Session.preSubscriber = True Then
            startIndex = 1
        End If

        ' Check for selected items
        For i As Integer = startIndex To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                hasSelectedItems = True
                Exit For
            End If
        Next

        If Not hasSelectedItems Then
            MessageBox.Show("Please select items to update quantity!", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                ' Process each selected item
                For i As Integer = startIndex To CheckedListBox1.Items.Count - 1
                    If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                        ' Additional safety check - don't allow plan updates
                        If Session.preSubscriber = True AndAlso i = 0 Then
                            MessageBox.Show("Plan quantities cannot be updated!", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Continue For
                        End If

                        Dim cartIndex As Integer = i - startIndex

                        ' Make sure we have a valid cart item
                        If cartIndex >= 0 AndAlso cartIndex < cartItems.Count Then
                            Dim currentItem As CartItem = cartItems(cartIndex)

                            ' Get current quantity info
                            Dim currentQuantity As Integer = currentItem.Quantity
                            Dim itemName As String = currentItem.ProductName

                            ' Check stock availability for hardware items
                            Dim maxAllowed As Integer = Integer.MaxValue
                            If currentItem.Category.ToUpper() = "HARDWARE" Then
                                Dim stockQuery As String = "SELECT hs.quantity_available FROM hardware_stocks hs WHERE hs.addon_id = @addonId"
                                Using stockCmd As New MySqlCommand(stockQuery, con)
                                    stockCmd.Parameters.AddWithValue("@addonId", currentItem.AddonId)
                                    Dim stockResult = stockCmd.ExecuteScalar()
                                    If stockResult IsNot Nothing Then
                                        maxAllowed = Convert.ToInt32(stockResult)
                                    End If
                                End Using
                            End If

                            ' Prompt for new quantity with stock info
                            Dim promptMessage As String = $"Update quantity for: {itemName}" & vbCrLf &
                                                    $"Current quantity: {currentQuantity}" & vbCrLf

                            If currentItem.Category.ToUpper() = "HARDWARE" AndAlso maxAllowed <> Integer.MaxValue Then
                                promptMessage += $"Available stock: {maxAllowed}" & vbCrLf
                            End If

                            promptMessage += "Enter new quantity:"

                            Dim input As String = InputBox(promptMessage, "Update Quantity", currentQuantity.ToString())

                            ' Check if user cancelled
                            If String.IsNullOrEmpty(input) Then
                                Continue For ' Skip this item, continue with others
                            End If

                            Dim newQuantity As Integer
                            If Integer.TryParse(input, newQuantity) AndAlso newQuantity > 0 Then
                                ' Check stock limits for hardware items
                                If currentItem.Category.ToUpper() = "HARDWARE" AndAlso newQuantity > maxAllowed Then
                                    MessageBox.Show($"Insufficient stock for {itemName}!" & vbCrLf &
                                              $"Requested: {newQuantity}, Available: {maxAllowed}",
                                              "Stock Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Continue For
                                End If

                                ' Update the database
                                Dim updateQuery As String = "UPDATE shopping_cart SET quantity = @quantity WHERE customer_id = @customerId AND addon_id = @addonId"
                                Using cmd As New MySqlCommand(updateQuery, con)
                                    cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                                    cmd.Parameters.AddWithValue("@addonId", currentItem.AddonId)
                                    cmd.Parameters.AddWithValue("@quantity", newQuantity)
                                    cmd.ExecuteNonQuery()
                                End Using

                                MessageBox.Show("Quantities updated successfully!", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                currentItem.Quantity = newQuantity

                            Else
                                MessageBox.Show($"Invalid quantity entered for {itemName}!" & vbCrLf &
                                          "Please enter a positive number greater than 0." & vbCrLf &
                                          "To remove items, use Deletion Mode instead.",
                                          "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If
                        End If
                    End If
                Next

                ' Refresh the display after all updates
                LoadCartFromDatabase() ' Reload from database to ensure consistency
                RefreshCartDisplay()
                UpdateTotal()
                RefreshCart()


            End Using

        Catch ex As Exception
            MessageBox.Show("Error updating quantities: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class