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
    Dim deletionMode = False
    ' In Cart.vb
    Private Sub Cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load, Me.VisibleChanged
        DisplayPlanDetails()             ' Show selected plan if exists
        LoadCartFromDatabase()          ' Load actual cart items from DB
        RefreshCartDisplay()            ' Display them in CheckedListBox
        UpdateTotal()
        RefreshCart() ' Show total including plan
        btnCheck.Visible = False
        btnDM.Visible = False
        btnClearCart.Visible = False

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

                ' Join shopping_cart with addons table to get product details
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

    Private Sub DisplayPlanDetails()
        ' Display plan information if coming from a plan selection
        If Not Session.fromProduct AndAlso Not String.IsNullOrEmpty(Session.planName) Then
            ' You can add plan details to a label or at the top of the CheckedListBox
            Dim planInfo As String = $"Selected Plan: {Session.planName} - {Session.planType} - Php {Session.planPrice:F2}"

            ' If you have a label for plan details, uncomment this:
            ' lblPlanDetails.Text = planInfo

            ' Or add it as the first item in CheckedListBox (non-selectable)
            CheckedListBox1.Items.Add($"PLAN: {Session.planName} - {Session.planType} - Php {Session.planPrice:F2}")
            CheckedListBox1.SetItemCheckState(0, CheckState.Indeterminate) ' Make it non-selectable
        End If
    End Sub

    Private Sub RefreshCartDisplay()
        ' Clear only cart items, keep plan if it exists
        Dim startIndex As Integer = 0
        If Not Session.fromProduct AndAlso Not String.IsNullOrEmpty(Session.planName) Then
            startIndex = 1 ' Keep the plan item
        Else
            CheckedListBox1.Items.Clear()
        End If

        ' Remove only cart items, not the plan
        For i As Integer = CheckedListBox1.Items.Count - 1 To startIndex Step -1
            CheckedListBox1.Items.RemoveAt(i)
        Next

        ' Add cart items
        For Each item In cartItems
            Dim displayText As String = $"{item.ProductName} - Qty: {item.Quantity} - Php {(item.Price * item.Quantity):F2} [{item.Category}]"
            CheckedListBox1.Items.Add(displayText)
        Next
    End Sub

    Private Sub UpdateTotal()
        Dim total As Decimal = 0

        ' Add plan price if applicable
        If Not Session.fromProduct AndAlso Session.planPrice > 0 Then
            total += Session.planPrice
        End If

        ' Display total in TextBox1
        txtTotal.Text = "Php " & total.ToString("F2")
    End Sub

    Public Function GetCartTotal() As Decimal


        If Not Session.fromProduct AndAlso Session.planPrice > 0 Then

            Return total

        Else

            Return ItemTotal


        End If



    End Function

    Private Sub RemoveSelectedItems()
        ' Check transaction validity before modifying cart
        Session.CheckTransactionTimeout()
        If Not Session.IsTransactionActive Then
            MessageBox.Show("Session expired. Please select a plan again.")
            ReturnToPlanSelection()
            Return
        End If

        Dim itemsToRemove As New List(Of Integer)
        Dim startIndex As Integer = 0

        ' Skip plan item if it exists
        If Not Session.fromProduct AndAlso Not String.IsNullOrEmpty(Session.planName) Then
            startIndex = 1
        End If

        ' Get selected items (skip plan item)
        For i As Integer = startIndex To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemCheckState(i) = CheckState.Checked Then
                itemsToRemove.Add(i - startIndex) ' Adjust index for cartItems list
            End If
        Next

        ' Remove from database and local list
        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                For Each index In itemsToRemove.OrderByDescending(Function(x) x)
                    If index >= 0 AndAlso index < cartItems.Count Then
                        ' Remove from database
                        Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId AND addon_id = @addonId"
                        Using cmd As New MySqlCommand(deleteQuery, con)
                            cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                            cmd.Parameters.AddWithValue("@addonId", cartItems(index).AddonId)
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
        ' Check transaction validity before clearing cart
        Session.CheckTransactionTimeout()
        If Not Session.IsTransactionActive Then
            MessageBox.Show("Session expired. Please select a plan again.")
            ReturnToPlanSelection()
            Return
        End If

        Try
            Using con As New MySqlConnection(strCon)
                con.Open()
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
            MessageBox.Show("Cart cleared!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error clearing cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Button event handlers (add these buttons to your form if they don't exist)
    Private Sub btnRemoveSelected_Click(sender As Object, e As EventArgs) Handles btnDeletionMode.Click


        If CheckedListBox1.Items.Count = 1 AndAlso Session.fromProduct = False Then
            MsgBox("There is nothing to delete")
            Return
        End If

        btnCheck.Visible = True
        btnDM.Visible = True
        deletionMode = True
        btnClearCart.Visible = True
        lblDeletionMode.Visible = True
        btnDeletionMode.Enabled = False
        btnDM.Enabled = True


        For i As Integer = 1 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SetItemChecked(i, False)
        Next

        total = Session.planPrice
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
        End If
    End Sub



    Private Sub btnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click

        ' Check transaction validity before checkout

        If deletionMode = True Then
            MsgBox("Please Exit Deletion Mode First!")
            Return
        End If

        Session.CheckTransactionTimeout()
        If Not Session.IsTransactionActive Then
            MessageBox.Show("Session expired. Please select a plan again.")
            ReturnToPlanSelection()
            Return
        End If

        If cartItems.Count = 0 AndAlso (Session.fromProduct OrElse Session.planPrice = 0) Then
            MessageBox.Show("Your cart is empty!", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim total As Decimal = GetCartTotal()
        Dim message As String = $"Total amount: Php {total:F2}" & vbCrLf & "Enter payment amount:"

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

        If paymentAmount < total Then
            MessageBox.Show("Insufficient payment amount!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Keep transaction active for retry
        End If

        ' Process payment
        Dim change As Decimal = paymentAmount - total
        Dim resultMessage As String = "Payment successful!" & vbCrLf &
                                     $"Total: Php {total:F2}" & vbCrLf &
                                     $"Payment: Php {paymentAmount:F2}" & vbCrLf &
                                     $"Change: Php {change:F2}" & vbCrLf &
                                     "Thank you for your purchase!"

        MessageBox.Show(resultMessage, "Payment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Payment successful - complete transaction and clear cart
        Session.EndTransaction(True)
        ClearCart()

        ' Navigate back to main menu or close
        Me.Close()
    End Sub

    ' Add navigation methods
    Private Sub btnContinueShopping_Click(sender As Object, e As EventArgs) Handles btnContinueShopping.Click
        ' Return to addon selection - keep transaction active
        Dim addonForm As New Addon()
        addonForm.Show()
        Me.Hide()
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

    Private Sub form_closing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Explicitly cancel the transaction

        Try
            Using con As New MySqlConnection(strCon)
                con.Open()
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


                Dim query As String = "SELECT sc.addon_id, a.item_name, sc.quantity, a.price FROM shopping_cart sc JOIN 
                                    addons a ON sc.addon_id = a.addon_id WHERE sc.customer_id = @customerId AND item_name = @itemName"
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

        If Session.fromProduct = False Then
            If CheckedListBox1.SelectedIndex = 0 Then
                MsgBox("Plan cannot be deleted")
                Return
            End If
        End If
        If CheckedListBox1.CheckedItems.Count = 1 And Session.fromProduct = False Then
            MessageBox.Show("Please select items to remove!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to remove selected items?",
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
    End Sub
End Class