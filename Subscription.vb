Imports System.Configuration
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

    Private Sub Subscription_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Interactive_Menu(id)
    End Sub

    Public Sub Interactive_Menu(currentID As Integer)
        If currentID = 1 Then
            pbxPlan.Image = My.Resources.vigan1
            Session.planImage = My.Resources.vigan1
        ElseIf currentID = 2 Then
            pbxPlan.Image = My.Resources.anticorrupt2
            Session.planImage = My.Resources.anticorrupt2
        ElseIf currentID = 3 Then
            pbxPlan.Image = My.Resources.casagua3
            Session.planImage = My.Resources.casagua3
        ElseIf currentID = 4 Then
            pbxPlan.Image = My.Resources.church4
            Session.planImage = My.Resources.church4
        ElseIf currentID = 5 Then
            pbxPlan.Image = My.Resources.church5
            Session.planImage = My.Resources.church5
        ElseIf currentID = 6 Then
            pbxPlan.Image = My.Resources.flag6
            Session.planImage = My.Resources.flag6
        ElseIf currentID = 7 Then
            pbxPlan.Image = My.Resources.fortsantiago7
            Session.planImage = My.Resources.fortsantiago7
        ElseIf currentID = 8 Then
            pbxPlan.Image = My.Resources.philippiness8
            Session.planImage = My.Resources.philippiness8
        ElseIf currentID = 9 Then
            pbxPlan.Image = My.Resources.rizal9
            Session.planImage = My.Resources.rizal9
        ElseIf currentID = 10 Then
            pbxPlan.Image = My.Resources.shrineemilio10
            Session.planImage = My.Resources.shrineemilio10
        End If

        selection(currentID, False)
    End Sub

    Public Sub selection(getID As Integer, popOutReceive As Boolean)
        Using con As New MySqlConnection(strCon)
            con.Open()

            Dim id As Integer = getID
            Dim transaction As MySqlTransaction = con.BeginTransaction
            Try
                Dim cmd As New MySqlCommand()
                cmd.Connection = con
                cmd.Transaction = transaction

                cmd.CommandText = "SELECT plan_id, plan_name, plan_type, price, speed, data_cap FROM internet_plans WHERE plan_id = @plan_id "
                cmd.Parameters.AddWithValue("@plan_id", id)

                Using reader As MySqlDataReader = cmd.ExecuteReader
                    If reader.Read() Then
                        plan_id = reader("plan_id").ToString
                        plan_name = reader("plan_name").ToString
                        plan_type = reader("plan_type").ToString
                        price = reader("price").ToString
                        speed = reader("speed").ToString
                        data_cap = reader("data_cap").ToString
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

                        ' Start new transaction when plan is selected
                        Session.StartTransaction()

                        addons = MsgBox("Do you want an addons?", MsgBoxStyle.YesNo, "Addons")

                        If addons = DialogResult.Yes Then
                            ' Navigate to addon form - keep transaction active
                            Session.fromProduct = False
                            If addons = DialogResult.Yes Then
                                Session.fromProduct = False
                                navigatingAway = True
                                Addon.Show()
                                Me.Close()
                            Else
                                Session.fromProduct = False
                                navigatingAway = True
                                Cart.Show()
                                Me.Close()
                            End If
                            Addon.Show()
                            Me.Close()
                        End If
                    End If
                Else
                    txtSpecs.Text = $"Plan:{plan_name}{vbNewLine}Type: {plan_type}{vbNewLine}Speed: {speed}{vbNewLine}Cap: {data_cap}{vbNewLine}Price: {price} "
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

        Main.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        id -= 1

        If id > 10 Then
            id = 1
        ElseIf id <= 0 Then
            id = 10
        End If

        Interactive_Menu(id)
        TextBox1.Text = id
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        id += 1

        If id > 10 Then
            id = 1
        ElseIf id <= 0 Then
            id = 10
        End If

        Interactive_Menu(id)
        TextBox1.Text = id
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
End Class