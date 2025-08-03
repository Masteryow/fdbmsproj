Imports System.Configuration
Imports System.Diagnostics.Metrics
Imports System.IO
Imports System.Numerics
Imports System.Transactions
Imports MySql.Data.MySqlClient


Public Class Subscription
    Private navigatingAway As Boolean = False
    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim id As Integer = 1
    Dim popOut As Boolean = False
    Dim currentUserID As Integer = Session.UserId
    Dim currentPlanID As Integer = 0
    Dim plan_id As Integer = 0
    Dim plan_name As String = ""
    Dim plan_type As String = ""
    Dim price As Decimal = 0
    Dim speed As String = ""
    Dim data_cap As String = ""
    Dim getImage As Byte() = Nothing
    Function planCount()

        Dim count As Integer = 0
        Using conn As New MySqlConnection(strCon)

            conn.Open()

            Using counter As New MySqlCommand("SELECT COUNT(*) FROM internet_plans", conn)

                count = Convert.ToInt32(counter.ExecuteScalar())



            End Using
        End Using

        Return count

    End Function





    Public Sub fetchPlans(planID As Integer)
        Using conn As New MySqlConnection(strCon)
            conn.Open()
            Using cmd As New MySqlCommand("SELECT ip.plan_id, ip.plan_name, ip.plan_type, ip.price, ip.speed, ip.data_cap FROM internet_plans ip JOIN
                                   blobs b on ip.blob_id = b.blob_id WHERE plan_id = @plan_id")

                cmd.Parameters.AddWithValue("@plan_id", planID)
                Using reader As MySqlDataReader = cmd.ExecuteReader
                End Using
            End Using
        End Using
    End Sub











    Public Sub delete()

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



        Catch ex As Exception
            MessageBox.Show("Error clearing cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Subscription_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Session.userRole <> "Subscriber" OrElse Session.subStatus Is DBNull.Value OrElse Session.subStatus.ToString() = "" Then
            HelpToolStripMenuItem.Visible = False
        End If

        Session.fromProduct = True
        Session.PlanId = 0
        Session.planName = ""
        Session.planType = ""
        Session.planPrice = 0
        Session.preSubscriber = False

        Session.subscriberAccess = False



        Using conn As New MySqlConnection(strCon)
            conn.Open()


            Using cmd As New MySqlCommand("SELECT * FROM internet_plans", conn)

                Using reader As MySqlDataReader = cmd.ExecuteReader

                    While reader.Read

                        cbxPlans.Items.Add(reader.GetString("plan_name"))

                    End While


                End Using
            End Using


        End Using


        cbxPlans.SelectedIndex = 0

        selection(id, False)

    End Sub



    Public Sub selection(getID As Integer, popOutReceive As Boolean)
        Using con As New MySqlConnection(strCon)
            con.Open()


            Dim transaction As MySqlTransaction = con.BeginTransaction

            Try
                Dim cmd As New MySqlCommand()
                cmd.Connection = con
                cmd.Transaction = transaction

                cmd.CommandText = "SELECT ip.plan_id, ip.plan_name, ip.plan_type, ip.price, ip.speed, ip.data_cap, b.data FROM internet_plans ip JOIN
                                   blobs b on ip.blob_id = b.blob_id WHERE plan_id = @plan_id "
                cmd.Parameters.AddWithValue("@plan_id", getID)

                Using reader As MySqlDataReader = cmd.ExecuteReader
                    If reader.Read() Then
                        plan_id = reader("plan_id").ToString
                        plan_name = reader("plan_name").ToString
                        plan_type = reader("plan_type").ToString
                        price = reader("price").ToString
                        speed = reader("speed").ToString
                        data_cap = reader("data_cap").ToString
                        getImage = CType(reader("data"), Byte())



                    End If
                End Using

                If popOutReceive = True Then
                    Dim result As DialogResult
                    Dim addons As DialogResult
                    result = MsgBox($"Plan: {plan_name}{vbNewLine}Type: {plan_type}{vbNewLine}Price: {price}{vbNewLine}Speed: {speed}{vbNewLine}Cap: {data_cap}{vbNewLine}", MsgBoxStyle.YesNo, "Subscribe?")

                    If result = DialogResult.Yes Then
                        ' Check for existing subscription first
                        Dim forSub As New MySqlCommand()
                        forSub.Connection = con
                        forSub.Transaction = transaction

                        forSub.CommandText = "SELECT sub.customer_id, sub.status, sub.plan_id, ip.plan_name FROM subscribers sub JOIN internet_plans ip ON sub.plan_id = ip.plan_id WHERE sub.customer_id = @customer_id"
                        forSub.Parameters.AddWithValue("@customer_id", currentUserID)
                        forSub.ExecuteNonQuery()

                        Using getStatus As MySqlDataReader = forSub.ExecuteReader
                            If getStatus.Read Then
                                Dim statusValidator As String = getStatus.GetString("status")
                                Dim planValidator As String = getStatus.GetString("plan_name")

                                If statusValidator = "Active" Then
                                    MsgBox($"Already Subscribed to: {planValidator}. Multiple subscription can't be processed.")
                                    Exit Sub
                                End If
                            End If
                        End Using

                        ' START TRANSACTION MANAGEMENT HERE
                        ' Set plan details in session
                        ' START TRANSACTION MANAGEMENT HERE
                        ' Set plan details in session
                        Session.PlanId = plan_id
                        Session.planName = plan_name
                        Session.planType = plan_type
                        Session.planPrice = price
                        Session.planSpeed = speed
                        Session.planDataCap = data_cap

                        ' Start new transaction when plan is selected
                        Session.StartTransaction()


                        addons = MsgBox("Do you want an addons?", MsgBoxStyle.YesNo, "Addons")

                        If addons = DialogResult.Yes Then
                            ' Navigate to addon form - keep transaction active
                            Session.preSubscriber = True
                            Session.fromProduct = False
                                Session.subscriberAccess = False
                                navigatingAway = True
                                delete()
                                products.Show()

                                Me.Close()

                        Else


                            Session.fromProduct = False

                            Dim convertedPayment As Decimal = 0

                            Dim payment As String = InputBox($"Selected Plan: {plan_name}{vbNewLine}To Pay: {price.ToString("f2")}{vbNewLine}Please enter your money: ", "Payment")

                            If Decimal.TryParse(payment, convertedPayment) Then



                                If convertedPayment > price Then

                                    Dim change As Decimal = convertedPayment - price
                                    MsgBox($"Thank you for purchasing! Here is your change {change.ToString("f2")} ")
                                    Session.IsNewSubscription = True
                                ElseIf convertedPayment = price Then

                                    MsgBox("Thank you for purchasing!", MsgBoxStyle.DefaultButton1, "Purchased Successfully")
                                ElseIf convertedPayment < price Then
                                    Session.IsNewSubscription = True
                                    MsgBox("Insufficient money!", MsgBoxStyle.Exclamation, "Insufficient Amount")

                                    'Added1
                                    Session.PlanId = 0
                                    Session.planName = ""
                                    Session.planType = ""
                                    Session.planPrice = 0
                                    Session.preSubscriber = False
                                    Session.fromProduct = False
                                    Return
                                End If

                                Session.cashOnHand = convertedPayment

                                Using insertSub As New MySqlCommand("INSERT INTO subscribers (customer_id, plan_id) 
                                                                    VALUES (@customer_id, @plan_id)", con)

                                    insertSub.Transaction = transaction
                                    insertSub.Parameters.AddWithValue("@customer_id", CInt(Session.UserId))
                                    insertSub.Parameters.AddWithValue("@plan_id", CInt(Session.PlanId))

                                    insertSub.ExecuteNonQuery()
                                End Using

                                Using getSubID As New MySqlCommand("SELECT * FROM subscribers WHERE customer_id = @customer_id", con)
                                    getSubID.Parameters.AddWithValue("@customer_id", Session.UserId)

                                    getSubID.Transaction = transaction
                                    Using fetchSubId As MySqlDataReader = getSubID.ExecuteReader

                                        While fetchSubId.Read
                                            Session.SubscriberId = fetchSubId.GetInt32("subscriber_id")
                                        End While
                                    End Using

                                    ' Insert billing record
                                    Dim billingMonth As Date = Date.Today
                                    Dim dueDate As Date = billingMonth.AddDays(30)
                                    Dim totalAmount As Decimal = Decimal.Parse(price)

                                    Using billingCmd As New MySqlCommand("INSERT INTO billing_records (subscriber_id, billing_month, total_amount, due_date)
                                      VALUES (@sid, @month, @amount, @due)", con)
                                        billingCmd.Transaction = transaction
                                        billingCmd.Parameters.AddWithValue("@sid", Session.SubscriberId)
                                        billingCmd.Parameters.AddWithValue("@month", billingMonth)
                                        billingCmd.Parameters.AddWithValue("@amount", totalAmount)
                                        billingCmd.Parameters.AddWithValue("@due", dueDate)
                                        billingCmd.ExecuteNonQuery()
                                    End Using

                                    Dim billingId As Integer
                                    Using getBillingId As New MySqlCommand("SELECT LAST_INSERT_ID()", con)
                                        getBillingId.Transaction = transaction
                                        billingId = CInt(getBillingId.ExecuteScalar())
                                    End Using

                                    ' Insert payment
                                    Using paymentCmd As New MySqlCommand("INSERT INTO payments (billing_id, amount, payment_date)
                                      VALUES (@bid, @amount, @payDate)", con)
                                        paymentCmd.Transaction = transaction
                                        paymentCmd.Parameters.AddWithValue("@bid", billingId)
                                        paymentCmd.Parameters.AddWithValue("@amount", totalAmount)
                                        paymentCmd.Parameters.AddWithValue("@payDate", Date.Today)
                                        paymentCmd.ExecuteNonQuery()
                                    End Using
                                End Using

                                Session.userRole = "Subscriber"

                                transaction.Commit()

                                subscribers.Show()
                                Me.Close()
                            Else
                                MsgBox("Please enter a valid amount", MsgBoxStyle.Exclamation, "Invalid Amount")
                                Session.PlanId = 0
                                Session.planName = ""
                                Session.planType = ""
                                Session.planPrice = 0
                                Session.preSubscriber = False
                                Session.fromProduct = False
                                transaction.Rollback()
                            End If

                        End If
                    End If
                Else
                    txtSpecs.Text = $"Plan:{plan_name}{vbNewLine}Type: {plan_type}{vbNewLine}Speed: {speed}{vbNewLine}Cap: {data_cap}{vbNewLine}Price: {price} "
                    Using fetchImage As New MemoryStream(getImage)
                        pbxPlan.Image = Image.FromStream(fetchImage)
                        Session.planImage = pbxPlan.Image
                    End Using

                End If


            Catch ex As Exception
                transaction.Rollback()
                MsgBox("Transaction failed: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ProcessDirectPayment(con As MySqlConnection, transaction As MySqlTransaction)
        ' Handle direct payment for plan only (no addons)
        Dim insertCmd As New MySqlCommand()
        insertCmd.Connection = con
        insertCmd.Transaction = transaction

        Dim test As String = InputBox($"Please Enter Your Payment - To pay: {price.ToString("f2")}", "Payment")
        Dim payment As Decimal = 0

        If Decimal.TryParse(test, payment) Then
            Dim change As Decimal = payment - price

            If payment >= price Then
                Dim withChange As Boolean = (payment > price)
                currentPlanID = plan_id

                insertCmd.CommandText = "INSERT INTO subscribers(customer_id, plan_id, status) VALUES (@customer_id, @plan_id, @status)"
                insertCmd.Parameters.AddWithValue("@customer_id", currentUserID)
                insertCmd.Parameters.AddWithValue("@plan_id", plan_id)
                insertCmd.Parameters.AddWithValue("@status", "Active")

                insertCmd.ExecuteNonQuery()

                ' Complete the transaction successfully
                Session.EndTransaction(True)

                If withChange Then
                    MsgBox($"Subscribed! Here is your change: Php {change.ToString("f2")}")
                Else
                    MsgBox($"Subscribed Successfully!")
                End If
            Else
                ' Payment failed - cancel transaction
                Session.EndTransaction(False)
                MsgBox("Not Enough Money")
            End If
        Else
            ' Invalid payment - cancel transaction
            Session.EndTransaction(False)
            MsgBox("Please Enter A Valid Values")
        End If
    End Sub

    Private Sub HomeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem1.Click
        ' Cancel any active transaction when going home
        If Session.IsTransactionActive Then
            Session.EndTransaction(False)
        End If

        If Session.userRole = "Subscriber" Then
            subscribers.Show()
            Me.Close()

        ElseIf Session.userRole = "Customer" Then

            'Added1
            Session.PlanId = 0
            Session.planName = ""
            Session.planType = ""
            Session.planPrice = 0
            Session.preSubscriber = False
            Session.fromProduct = False

            Session.planSpeed = ""
            Session.planDataCap = ""
            Main.Show()
            Me.Close()
        End If

    End Sub






    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

        Dim limit As Integer = planCount()
        id -= 1

        If id > limit Then
            id = 1
        ElseIf id <= 0 Then
            id = limit
        End If

        selection(id, False)
    End Sub




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        Dim limit As Integer = planCount()
        id += 1

        If id > limit Then
            id = 1
        ElseIf id <= 0 Then

            id = limit
        End If

        selection(id, False)
    End Sub






    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnSubscribe.Click
        selection(id, True)
    End Sub

    ' Optional: Handle form closing to clean up transactions
    Private Sub Subscription_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Only end transaction if not navigating away
        If Not navigatingAway AndAlso Session.IsTransactionActive Then
            Session.EndTransaction(False)
        End If
    End Sub

    Private Sub btnCart_Click(sender As Object, e As EventArgs) Handles btnCart.Click
        Cart.Show()
        Me.Close()
    End Sub

    Private Sub ProductsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsToolStripMenuItem.Click

        Session.fromProduct = True
        products.Show()
        Me.Close()

    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

    End Sub

    Private Sub TicketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TicketToolStripMenuItem.Click
        Tickets.Show()
    End Sub



    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxPlans.SelectedIndexChanged

        id = cbxPlans.SelectedIndex + 1

        selection(id, False)

    End Sub

    Private Sub txtSpecs_TextChanged(sender As Object, e As EventArgs) Handles txtSpecs.TextChanged

    End Sub
End Class