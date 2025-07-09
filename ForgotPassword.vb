Imports MySql.Data.MySqlClient

Public Class ForgotPassword

    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim connect As New MySqlConnection(strCon)

    Private Sub ForgotPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class