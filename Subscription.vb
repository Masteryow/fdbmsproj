Imports System.Numerics
Imports System.Transactions
Imports MySql.Data.MySqlClient
Public Class Subscription

    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim id As Integer = 0
    Private Sub Subscription_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub selection(getID As Integer)
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

                        MsgBox($"Plan: {plan_name} | Type: {plan_type} | Price: {price} | Speed: {speed} | Cap: {data_cap}")

                    Else

                        MsgBox("ID not Found")
                    End If

                End Using

                transaction.Commit()
            Catch ex As Exception
                Transaction.Rollback()
                MsgBox("Transaction failed: " & ex.Message)
            End Try



        End Using
    End Sub

    Private Sub btnSub1_Click(sender As Object, e As EventArgs) Handles btnSub1.Click

        id = 1

        selection(id)

    End Sub

    Private Sub btnSub2_Click(sender As Object, e As EventArgs) Handles btnSub2.Click
        id = 2
        selection(id)
    End Sub

    Private Sub btnSub3_Click(sender As Object, e As EventArgs) Handles btnSub3.Click
        id = 3

        selection(id)
    End Sub

    Private Sub btnSub4_Click(sender As Object, e As EventArgs) Handles btnSub4.Click
        id = 4

        selection(id)
    End Sub

    Private Sub btnSub5_Click(sender As Object, e As EventArgs) Handles btnSub5.Click
        id = 5

        selection(id)
    End Sub

    Private Sub btnSub6_Click(sender As Object, e As EventArgs) Handles btnSub6.Click
        id = 6

        selection(id)
    End Sub

    Private Sub btnSub7_Click(sender As Object, e As EventArgs) Handles btnSub7.Click
        id = 7

        selection(id)
    End Sub

    Private Sub btnSub8_Click(sender As Object, e As EventArgs) Handles btnSub8.Click
        id = 8

        selection(id)
    End Sub

    Private Sub btnSub9_Click(sender As Object, e As EventArgs) Handles btnSub9.Click
        id = 9

        selection(id)
    End Sub

    Private Sub btnSub10_Click(sender As Object, e As EventArgs) Handles btnSub10.Click
        id = 10

        selection(id)
    End Sub

    Private Sub btnSub11_Click(sender As Object, e As EventArgs) Handles btnSub11.Click
        id = 11

        selection(id)
    End Sub

    Private Sub HomeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem1.Click
        Main.Show()
        Me.Close()

    End Sub
End Class