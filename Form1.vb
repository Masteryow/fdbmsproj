Imports System.Drawing.Drawing2D

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Create a new TextBox

    End Sub

    Private Sub btn_create(sender As Object, e As EventArgs) Handles btnCreate.Click
        CreateAccount.Show()
        Me.Close()




    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub lblForgotPass_MouseHover(sender As Object, e As EventArgs) Handles lblForgotPass.MouseHover
        lblForgotPass.ForeColor = Color.DarkBlue
        lblForgotPass.Font = New Font(lblForgotPass.Font, lblForgotPass.Font.Style Or FontStyle.Bold)
    End Sub

    Private Sub lblForgotPass_MouseLeave(sender As Object, e As EventArgs) Handles lblForgotPass.MouseLeave
        lblForgotPass.ForeColor = Color.DarkSlateGray

        lblForgotPass.Font = New Font(lblForgotPass.Font, FontStyle.Underline)
    End Sub
End Class
