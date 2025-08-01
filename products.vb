Imports System.IO
Imports MySql.Data.MySqlClient




Public Class products
    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim stocksCount As Integer = 0
    Dim price As Decimal = 0
    Dim id As Integer = 0
    Dim accessCategory As String = ""
    Dim itemDisplayList As New List(Of ItemDisplay)
    Dim productImage As Byte()
    Dim Quantity As Integer = 1
    Dim selectedItem As ItemDisplay
    Dim total As Decimal = 0
    Dim planName As String = Session.planName
    Dim planPrice As Decimal = Session.planPrice
    Dim planType As String = Session.planType
    Dim planID As Integer = Session.PlanId
    Dim planImage As Image = Session.planImage

    Public Sub delete()

        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId"
                Using cmd As New MySqlCommand(deleteQuery, con)
                    cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using



        Catch ex As Exception
            MessageBox.Show("Error clearing cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub itemCategoryBased(selectedCategory As String)
        cbxItems.Items.Clear()
        itemDisplayList.Clear()

        Try
            Using conn As New MySqlConnection(strCon)
                conn.Open()

                Dim query As String = "SELECT a.addon_id, a.item_name, a.category, a.price, 
                                      IFNULL(hs.quantity_available, 0) AS quantity, 
                                      b.blob_id, b.file_name, b.data 
                                      FROM addons a 
                                      LEFT JOIN hardware_stocks hs ON a.addon_id = hs.addon_id 
                                      JOIN blobs b ON a.blob_id = b.blob_id"


                If selectedCategory <> "All" Then
                        query &= " WHERE a.category = @category"
                    End If



                Using cmd As New MySqlCommand(query, conn)
                    If selectedCategory <> "All" Then
                        cmd.Parameters.AddWithValue("@category", selectedCategory)
                    End If

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()


                            Dim addonId As Integer = reader.GetInt32("addon_id")
                            Dim name As String = reader.GetString("item_name")
                            Dim itemPrice As Decimal = reader.GetDecimal("price")

                            Dim quantity As Integer = reader.GetInt32("quantity")

                            Dim displayText As String = name


                            Dim item As New ItemDisplay With {
                                .AddonId = addonId,
                                .DisplayText = displayText
                            }

                            cbxItems.Items.Add(item)
                            itemDisplayList.Add(item)
                        End While
                    End Using
                End Using

                If cbxItems.Items.Count > 0 Then
                    cbxItems.SelectedIndex = 0
                End If
            End Using

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Public Sub getItemInfo(fetchID As Integer)
        Try
            Dim selectedCategory As String = ""
            Dim query As String = ""

            Using conn As New MySqlConnection(strCon)
                conn.Open()

                Using getCategory As New MySqlCommand("SELECT category FROM addons WHERE addon_id = @addon_id", conn)
                    getCategory.Parameters.AddWithValue("@addon_id", fetchID)
                    selectedCategory = Convert.ToString(getCategory.ExecuteScalar())
                End Using

                accessCategory = selectedCategory

                If selectedCategory = "Hardware" Then
                    query = "SELECT a.item_name, a.category, a.price, hs.quantity_available, b.blob_id, b.file_name, b.data 
                             FROM addons a 
                             JOIN blobs b ON a.blob_id = b.blob_id 
                             JOIN hardware_stocks hs ON hs.addon_id = a.addon_id 
                             WHERE a.addon_id = @addon_id"
                Else
                    query = "SELECT a.item_name, a.category, a.price, b.blob_id, b.file_name, b.data 
                             FROM addons a 
                             JOIN blobs b ON a.blob_id = b.blob_id 
                             WHERE a.addon_id = @addon_id"
                End If

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@addon_id", fetchID)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            price = reader.GetDecimal("price")
                            txtPrice.Text = price.ToString("f2")
                            productImage = CType(reader("data"), Byte())

                            Using imageConversion As New MemoryStream(productImage)

                                pbxItem.Image = Image.FromStream(imageConversion)

                            End Using
                            If selectedCategory = "Hardware" Then
                                stocksCount = reader.GetInt32("quantity_available")
                                txtStocks.Text = stocksCount.ToString()
                                lblStocks.Visible = True
                                txtStocks.Visible = True
                            Else
                                txtStocks.Text = ""
                                lblStocks.Visible = False
                                txtStocks.Visible = False
                            End If
                        End While
                    End Using
                End Using



            End Using

        Catch ex As Exception
            MsgBox("Error fetching item info: " & ex.Message)
        End Try
    End Sub

    Private Sub products_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Session.preSubscriber = True Then
            pbxPlanImage.Visible = True
            lblPlanName.Visible = True
            lblPlanType.Visible = True
            lblPlanPrice.Visible = True

        ElseIf Session.fromProduct = True Then
            pbxPlanImage.Visible = False
            lblPlanName.Visible = False
            lblPlanType.Visible = False
            lblPlanPrice.Visible = False

            Dim lblHeader As New Label()

            lblHeader.Size = New Size(400, 50)
            lblHeader.Location = New Point(40, 35)
            lblHeader.Font = New Font("Times New Roman", 35, FontStyle.Bold)
            lblHeader.Text = "SkyLink Products"
            lblHeader.ForeColor = Color.SlateBlue

            Me.Controls.Add(lblHeader)

            Dim lblSubHeader As New Label

            lblSubHeader.Size = New Size(400, 50)
            lblSubHeader.Location = New Point(120, 100)
            lblSubHeader.Font = New Font("Tahoma", 9, FontStyle.Bold)
            lblSubHeader.Text = "Enjoy ultra-fast download and upload speeds without buffering or lag—day or night."
            lblSubHeader.ForeColor = Color.White

            Me.Controls.Add(lblSubHeader)




        End If
        pbxPlanImage.Image = planImage
        lblPlanName.Text = planName
        lblPlanType.Text = planType
        lblPlanPrice.Text = "₱ " & planPrice.ToString("f2")

        cbxFilter.SelectedIndex = 0

        Dim currentCategory1 As String

        If Session.fromProduct = True Then
            cbxFilter.SelectedIndex = 1
            cbxFilter.Enabled = False

            currentCategory1 = "Hardware"
        Else
            currentCategory1 = cbxFilter.SelectedItem.ToString()
        End If

        itemCategoryBased(currentCategory1)



        total = Quantity * price
        txtTotal.Text = total

        If Session.preSubscriber Then
            lblChangeOfMind.Visible = True

        ElseIf Session.fromProduct = True Then
            lblChangeOfMind.Visible = False

        End If

    End Sub

    Private Sub cbxItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxItems.SelectedIndexChanged
        If cbxItems.SelectedItem IsNot Nothing Then
            selectedItem = CType(cbxItems.SelectedItem, ItemDisplay)
            getItemInfo(selectedItem.AddonId)
        End If


        Quantity = 1
        txtQuantity.Text = Quantity
        total = Quantity * price
        txtTotal.Text = total



    End Sub

    Private Sub cbxFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxFilter.SelectedIndexChanged
        cbxItems.Items.Clear()
        cbxItems.Text = ""
        txtPrice.Text = ""
        txtStocks.Text = ""

        Dim currentCategory As String = cbxFilter.SelectedItem.ToString
        itemCategoryBased(currentCategory)

        If cbxItems.Items.Count > 0 Then
            cbxItems.SelectedIndex = 0
        End If
    End Sub

    Private Sub pbxAdd_Click(sender As Object, e As EventArgs) Handles pbxAdd.Click
        Quantity += 1
        txtQuantity.Text = Quantity

        total = Quantity * price
        txtTotal.Text = total

    End Sub

    Private Sub pbxMinus_Click(sender As Object, e As EventArgs) Handles pbxMinus.Click
        Quantity -= 1

        If Quantity < 1 Then
            MsgBox("Quantity cannot be less than 0", MsgBoxStyle.Exclamation)
            Quantity = 1
        End If

        txtQuantity.Text = Quantity
        total = Quantity * price
        txtTotal.Text = total
    End Sub

    Private Sub pbxClear_Click(sender As Object, e As EventArgs) Handles pbxClear.Click

        If Session.fromProduct = True AndAlso Session.subscriberAccess = False Then
            cbxItems.SelectedIndex = 0

        Else
            cbxFilter.SelectedIndex = 0
            cbxItems.SelectedIndex = 0
        End If

        Quantity = 1
        txtQuantity.Text = Quantity

    End Sub

    Public Sub planOnly()

        Try

            Dim money As Decimal = 0
            Dim moneyValidation As String = InputBox($"Please Enter Your Money: {vbNewLine}Plan Name: {planName}{vbNewLine}Plan Type: {planType}{vbNewLine}" &
                                                    $"Plan Price: {planPrice.ToString("f2")}", "Payment")

            If Decimal.TryParse(moneyValidation, money) Then

                Dim change As Decimal = money - planPrice

                Session.cashOnHand = money

                If money > planPrice Then
                    MsgBox($"Thank you for purchasing! Here is your change {change.ToString("f2")}")

                ElseIf money = planPrice Then
                    MsgBox("Thank you for purchasing!")

                ElseIf money < planPrice Then
                    MsgBox("Insufficient money, please try again!")
                    Exit Sub
                Else

                    MsgBox("Invalid Input")
                    Exit Sub
                End If
            Else
                Exit Sub

            End If

            Using conn As New MySqlConnection(strCon)

                conn.Open()

                Using transaction As MySqlTransaction = conn.BeginTransaction

                    Try

                        Using cmd As New MySqlCommand("INSERT INTO subscribers (customer_id, plan_id) VALUES (@customer_id, @plan_id)", conn, transaction)

                            cmd.Parameters.AddWithValue("@customer_id", Session.UserId)
                            cmd.Parameters.AddWithValue("@plan_id", planID)

                            cmd.ExecuteNonQuery()

                        End Using

                        Using getSubId As New MySqlCommand("SELECT u.user_id, u.username, s.subscriber_id FROM subscribers s JOIN
                                                    users u ON u.user_id = s.customer_id WHERE u.user_id = @user_id", conn)
                            getSubId.Parameters.AddWithValue("@user_id", Session.UserId)

                            Using fetchSubID As MySqlDataReader = getSubId.ExecuteReader

                                While fetchSubID.Read
                                    Session.SubscriberId = fetchSubID.GetInt32("subscriber_id")
                                End While

                            End Using


                        End Using

                        Dim billingQuery As String = "INSERT INTO billing_records (subscriber_id, billing_month, total_amount, due_date, status) " &
            "VALUES (@subscriber, CURDATE(), @planAmount, DATE_ADD(CURDATE(), INTERVAL 1 MONTH), 'Paid')"
                        Dim billingId As Integer = 0

                        Using billing As New MySqlCommand(billingQuery, conn, transaction)
                            billing.Parameters.AddWithValue("@subscriber", Session.SubscriberId)
                            billing.Parameters.AddWithValue("@planAmount", planPrice)
                            billing.ExecuteNonQuery()
                            billingId = billing.LastInsertedId
                        End Using

                        ' Insert payment record
                        Dim paymentQuery As String = "INSERT INTO payments (billing_id, amount, payment_date) " &
            "VALUES (@billingId, @amount, CURDATE())"
                        Using cmd As New MySqlCommand(paymentQuery, conn, transaction)
                            cmd.Parameters.AddWithValue("@billingId", billingId)
                            cmd.Parameters.AddWithValue("@amount", planPrice)
                            cmd.ExecuteNonQuery()
                        End Using

                        Using updateRole As New MySqlCommand("UPDATE users SET role = @role WHERE user_id = @user_id", conn, transaction)

                            updateRole.Parameters.AddWithValue("@user_id", Session.UserId)
                            updateRole.Parameters.AddWithValue("@role", "Subscriber")

                            updateRole.ExecuteNonQuery()
                        End Using

                        transaction.Commit()
                    Catch ex As Exception

                        transaction.Rollback()

                    End Try



                End Using

            End Using

            delete()

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try


    End Sub


    Public Sub ordinaryPurchases()
        Try

            Dim money As Decimal = 0
            Dim moneyValidation As String = InputBox($"Please Enter Your Money: {vbNewLine}Item: {cbxItems.SelectedItem.ToString}{vbNewLine}Quantity: {Quantity}{vbNewLine}" &
                                                    $"To Pay: {total.ToString("f2")}", "Payment")

            If Decimal.TryParse(moneyValidation, money) Then

                Dim change As Decimal = money - total
                If money > total Then
                    MsgBox($"Thank you for purchasing! Here is your change {change.ToString("f2")}")

                ElseIf money = total Then
                    MsgBox("Thank you for purchasing!")

                ElseIf money < total Then
                    MsgBox("Insufficient money, please try again!")
                    Exit Sub
                Else

                    MsgBox("Invalid Input")
                    Exit Sub
                End If
            Else
                Exit Sub

            End If


            Using conn As New MySqlConnection(strCon)
                conn.Open()

                Using transaction As MySqlTransaction = conn.BeginTransaction()
                    Try
                        Using cmd As New MySqlCommand("INSERT INTO customer_addons (customer_id, addon_id, quantity) 
                                                   VALUES (@customer_id, @addon_id, @quantity)", conn, transaction)

                            cmd.Parameters.AddWithValue("@customer_id", Session.UserId)
                            cmd.Parameters.AddWithValue("@addon_id", selectedItem.AddonId)
                            cmd.Parameters.AddWithValue("@quantity", txtQuantity.Text)

                            cmd.ExecuteNonQuery()
                        End Using


                        If accessCategory = "Hardware" Then

                            Using updateStocks As New MySqlCommand("UPDATE hardware_stocks hs JOIN addons a ON hs.addon_id = a.addon_id
                                                                SET quantity_available = @quant WHERE a.addon_id = @currentID", conn, transaction)
                                Dim availableItem As Integer = stocksCount - Quantity

                                updateStocks.Parameters.AddWithValue("@quant", availableItem)
                                updateStocks.Parameters.AddWithValue("@currentID", selectedItem.AddonId)

                                updateStocks.ExecuteNonQuery()

                            End Using

                        End If



                        transaction.Commit()

                    Catch ex As Exception
                        transaction.Rollback()
                        MsgBox("Transaction failed: " & ex.Message)
                    End Try
                End Using



            End Using

            Session.IsNewSubscription = True

            subscribers.Show()
            Me.Close()

        Catch ex As Exception
            MsgBox("Please select an item first")
        End Try

    End Sub
    Private Sub pbxBuyNow_Click(sender As Object, e As EventArgs)



    End Sub

    Private Sub pbxBuyNow_Click_1(sender As Object, e As EventArgs) Handles pbxBuyNow.Click


        If Session.preSubscriber Then
            ' This is a new subscription purchase (plan + addons)
            Dim result As DialogResult = MsgBox($"Are you sure you want to purchase PLAN ONLY?{vbNewLine}" &
                "Your current cart will be cleared if you proceed", MsgBoxStyle.YesNo)

            If result = DialogResult.Yes Then
                planOnly()
            Else

                Exit Sub

            End If

        ElseIf Session.fromProduct = True Then
            ' Buying addons directly (from Products tab)
            ordinaryPurchases()

        End If

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class

Public Class ItemDisplay
    Public Property AddonId As Integer
    Public Property DisplayText As String

    Public Overrides Function ToString() As String
        Return DisplayText
    End Function
End Class
