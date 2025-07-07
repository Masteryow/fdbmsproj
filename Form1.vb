Imports System.Diagnostics.Tracing
Imports System.Drawing.Drawing2D

Public Class Form1

    Dim strAnimation As String = "Fast, Reliable, Secure, Connected, Skylink"
    Dim eachWord As String() = strAnimation.Split(","c)
    Dim currentIndex As Integer = 0
    Dim isShowing As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 1000
        Timer1.Start()

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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If isShowing Then
            Label1.Text = eachWord(currentIndex).Trim()
            isShowing = False
            If Label1.Text = "Fast" Then

                Me.BackColor = Color.WhiteSmoke
                Label1.ForeColor = Color.Black
            ElseIf Label1.Text = "Reliable" Then

                Me.BackColor = Color.Black
                Label1.ForeColor = Color.WhiteSmoke

            ElseIf Label1.text = "Secure" Then
                Label1.ForeColor = Color.Black
                Me.BackColor = Color.WhiteSmoke


            ElseIf Label1.text = "Connected" Then

                Me.BackColor = Color.Black

                Label1.ForeColor = Color.WhiteSmoke

            ElseIf Label1.Text = "Skylink" Then
                Label1.ForeColor = Color.Black
                Me.BackColor = Color.WhiteSmoke
            End If




        Else
                Label1.Text = ""
            currentIndex += 1


            If currentIndex >= eachWord.Length Then
                currentIndex = 0
            End If

            isShowing = True

        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
