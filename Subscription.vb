Imports System.Numerics
Imports System.Transactions
Imports MySql.Data.MySqlClient
Public Class Subscription

    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim id As Integer = 1
    Dim popOut As Boolean = False
    Private Sub Subscription_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Interactive_Menu(id)

    End Sub

    Public Sub Interactive_Menu(currentID As Integer)


        If currentID = 1 Then

            pbxPlan.Image = My.Resources.vigan1

        ElseIf currentID = 2 Then


            pbxPlan.Image = My.Resources.anticorrupt2

        ElseIf currentID = 3 Then

            pbxPlan.Image = My.Resources.casagua3

        ElseIf currentID = 4 Then
            pbxPlan.Image = My.Resources.church4

        ElseIf currentID = 5 Then

            pbxPlan.Image = My.Resources.church5

        ElseIf currentID = 6 Then
            pbxPlan.Image = My.Resources.flag6

        ElseIf currentID = 7 Then

            pbxPlan.Image = My.Resources.fortsantiago7

        ElseIf currentID = 8 Then

            pbxPlan.Image = My.Resources.philippiness8

        ElseIf currentID = 9 Then

            pbxPlan.Image = My.Resources.rizal9

        ElseIf currentID = 10 Then

            pbxPlan.Image = My.Resources.shrineemilio10

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

                cmd.CommandText = "SELECT plan_name, plan_type, price, speed, data_cap FROM internet_plans WHERE plan_id = @plan_id "
                cmd.Parameters.AddWithValue("@plan_id", id)

                Using reader As MySqlDataReader = cmd.ExecuteReader
                    If reader.Read() Then
                        Dim plan_name As String = reader("plan_name").ToString
                        Dim plan_type As String = reader("plan_type").ToString
                        Dim price As String = reader("price").ToString
                        Dim speed As String = reader("speed").ToString
                        Dim data_cap As String = reader("data_cap").ToString

                        If popOutReceive = True Then

                            MsgBox($"Plan: {plan_name} | Type: {plan_type} | Price: {price} | Speed: {speed} | Cap: {data_cap}")

                        Else
                            txtSpecs.Text = $"Plan:{plan_name}{vbNewLine}Type: {plan_type}{vbNewLine}Speed: {speed}{vbNewLine}Cap: {data_cap}{vbNewLine}Price: {price} "
                        End If

                    Else

                        MsgBox("ID not Found")
                    End If

                End Using

                transaction.Commit()
            Catch ex As Exception
                transaction.Rollback()
                MsgBox("Transaction failed: " & ex.Message)
            End Try



        End Using
    End Sub


    Private Sub HomeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem1.Click
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
End Class