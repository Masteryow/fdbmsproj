Imports System.Drawing.Text
Imports System.Runtime.Remoting.Channels
Imports System.Text.RegularExpressions

Public Class CreateAccount
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click



    End Sub

    Private label3 As New Label() 'for first restriction
    Private label4 As New Label() 'for second restriction
    Private label5 As New Label 'for third restriction
    Private label6 As New Label 'for fourth restriction
    Private btnTrigger As New Button()
    Private Sub CreateAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim txtBox As New TextBox

        txtBox.Multiline = True
        txtBox.Size = New Size(233, 33)
        txtBox.Location = New Point(487, 100)
        txtBox.Name = "txtUsername"
        txtBox.BorderStyle = BorderStyle.FixedSingle
        txtBox.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)

        Me.Controls.Add(txtBox)

        Dim label1 As New Label()

        label1.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        label1.ForeColor = Color.DarkSlateGray
        label1.Location = New Point(560, 70)
        label1.Text = "Username"
        label1.Size = New Size(100, 22)
        label1.Name = "lblUsername"
        Me.Controls.Add(label1)


        Dim label2 As New Label()

        label2.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        label2.ForeColor = Color.DarkSlateGray
        label2.Location = New Point(560, 150)
        label2.Text = "Password"
        label2.Size = New Size(100, 22)
        label2.Name = "lblPassword"
        Me.Controls.Add(label2)

        Dim txtBox1 As New TextBox

        txtBox1.Multiline = True
        txtBox1.Size = New Size(233, 33)
        txtBox1.Location = New Point(487, 180)
        txtBox1.Name = "txtPassword"
        txtBox1.BorderStyle = BorderStyle.FixedSingle
        txtBox1.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)

        AddHandler txtBox1.TextChanged, AddressOf txtPassword_TextChanged

        Me.Controls.Add(txtBox1)



        label3.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label3.ForeColor = Color.DarkSlateGray
        label3.Location = New Point(490, 230)
        label3.Text = "• Must be at least 8 characters"
        label3.Size = New Size(500, 22)
        label3.Name = "lbl3"


        Me.Controls.Add(label3)



        label4.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label4.ForeColor = Color.DarkSlateGray
        label4.Location = New Point(490, 250)
        label4.Text = "• Use both uppercase and lowercase 
                         letters"
        label4.Size = New Size(500, 15)
        label4.Name = "lbl4"
        Me.Controls.Add(label4)
        label5.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label5.ForeColor = Color.DarkSlateGray
        label5.Location = New Point(490, 270)
        label5.Text = "• Include at least one number"
        label5.Size = New Size(500, 15)
        label5.Name = "lbl5"

        Me.Controls.Add(label5)


        label6.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label6.ForeColor = Color.DarkSlateGray
        label6.Location = New Point(490, 290)
        label6.Text = "• Add at least one special character"
        label6.Size = New Size(500, 15)
        label6.Name = "lbl6"

        Me.Controls.Add(label6)
    End Sub

    Dim verifiedCount As Integer = 0

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs)



        Dim txtBox As TextBox = DirectCast(sender, TextBox)
        If Len(txtBox.Text) >= 8 Then
            label3.ForeColor = Color.DarkGreen
            label3.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            verifiedCount += 1

        Else
            label3.ForeColor = Color.DarkSlateGray
            label3.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        End If
        Dim password As String = txtBox.Text


        Dim hasUpper As Boolean = False
        Dim hasLower As Boolean = False
        Dim hasDigit As Boolean = False

        For Each ch As Char In txtBox.Text
            If Char.IsUpper(ch) Then
                hasUpper = True

            End If

            If Char.IsLower(ch) Then
                hasLower = True
            End If
        Next


        If hasUpper And hasLower = True Then
            label4.ForeColor = Color.DarkGreen
            label4.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            verifiedCount += 1
        Else
            label4.ForeColor = Color.DarkSlateGray
            label4.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        End If




        For Each int As Char In txtBox.Text
            If Char.IsDigit(int) Then

                hasDigit = True

            End If

        Next



        If hasDigit = True Then
            label5.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            label5.ForeColor = Color.DarkGreen
        Else
            label5.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
            label5.ForeColor = Color.DarkSlateGray
        End If




        Dim hasSpecial As Boolean = Regex.IsMatch(password, "[^a-zA-Z0-9\s]")

        If hasSpecial Then
            label6.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            label6.ForeColor = Color.DarkGreen
        Else
            label6.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
            label6.ForeColor = Color.DarkSlateGray
        End If


        If hasUpper = True And hasLower = True And Len(txtBox.Text) > 8 And
           hasDigit = True And hasSpecial = True Then



            btnTrigger.Visible = True
            btnTrigger.Text = "Submit"
            btnTrigger.Size = New Size(113, 34)
            btnTrigger.Location = New Point(540, 330)
            btnTrigger.BackColor = Color.WhiteSmoke
            btnTrigger.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)



            Me.Controls.Add(btnTrigger)
        Else

            btnTrigger.Visible = False
        End If
    End Sub

End Class