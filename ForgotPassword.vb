Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Net.Mail
Imports System.Threading.Tasks

Public Class ForgotPassword

    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim connect As New MySqlConnection(strCon)
    Dim receiveUser As String = ""
    Private label3 As New Label
    Private label4 As New Label
    Private label5 As New Label
    Private label6 As New Label
    Private verifiedCount As Integer = 0
    Dim randomCode As String = ""

    Sub email_verification()

        Dim sendCode As New MailMessage
        sendCode.From = New MailAddress("yohasakura200519@gmail.com")
        sendCode.To.Add(Session.email)
        sendCode.Subject = "Email Verification"
        sendCode.Body = "Your Code is " & randomCode.ToString


        Dim smtp As New SmtpClient("smtp.gmail.com", 587)
        smtp.Credentials = New NetworkCredential("yohasakura200519@gmail.com", "rwxe oxyt ocnc dfmx")
        smtp.EnableSsl = True

        Try
            smtp.Send(sendCode)

        Catch ex As Exception
            MessageBox.Show("Failed to send email: " & ex.Message)
        End Try


    End Sub

    Sub random_number()

        randomCode = ""
        Dim random As New Random


        For i As Integer = 1 To 4
            randomCode &= random.Next(0, 10).ToString

        Next

    End Sub
    Private Async Sub ForgotPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Panel1.Visible = True
        receiveUser = Session.UserName

        userInput.Text = receiveUser
        txtNewPassConfirm.Visible = False
        Label7.Visible = False
        txtNewPass.PasswordChar = "*"
        txtNewPassConfirm.PasswordChar = "*"

        random_number()


        Await Task.Run(Sub() email_verification())
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtNewPass.TextChanged

        btnPassUpdate.Visible = False
        label3.Visible = True
        label4.Visible = True
        label5.Visible = True
        label6.Visible = True
        label3.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label3.ForeColor = Color.DarkSlateGray
        label3.Location = New Point(200, 260)
        label3.Text = "• Must be at least 8 characters"
        label3.Size = New Size(500, 22)
        label3.Name = "lbl3"
        Me.Controls.Add(label3)

        label4.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label4.ForeColor = Color.DarkSlateGray
        label4.Location = New Point(200, 280)
        label4.Text = "• Use both uppercase and lowercase 
                         letters"
        label4.Size = New Size(500, 15)
        label4.Name = "lbl4"
        Me.Controls.Add(label4)

        label5.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label5.ForeColor = Color.DarkSlateGray
        label5.Location = New Point(200, 300)
        label5.Text = "• Include at least one number"
        label5.Size = New Size(500, 15)
        label5.Name = "lbl5"
        Me.Controls.Add(label5)


        label6.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label6.ForeColor = Color.DarkSlateGray
        label6.Location = New Point(200, 320)
        label6.Text = "• Add at least one special character"
        label6.Size = New Size(500, 15)
        label6.Name = "lbl6"
        Me.Controls.Add(label6)



        Dim strPassword As String = txtNewPass.Text
        If Len(strPassword) >= 8 Then
            label3.ForeColor = Color.DarkGreen
            label3.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            verifiedCount += 1

        Else
            label3.ForeColor = Color.DarkSlateGray
            label3.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        End If
        Dim password As String = strPassword


        Dim hasUpper As Boolean = False
        Dim hasLower As Boolean = False
        Dim hasDigit As Boolean = False

        For Each ch As Char In strPassword
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




        For Each int As Char In strPassword
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


        If Not String.IsNullOrEmpty(txtNewPass.Text) AndAlso hasUpper = True AndAlso hasLower = True AndAlso Len(strPassword) >= 8 AndAlso
               hasDigit = True AndAlso hasSpecial = True Then

            label3.Visible = False
            label4.Visible = False
            label5.Visible = False
            label6.Visible = False
            Label7.Visible = True

            txtNewPassConfirm.Visible = True
            btnPassUpdate.Visible = True
        End If


    End Sub

    Private Sub btnPassUpdate_Click(sender As Object, e As EventArgs) Handles btnPassUpdate.Click

        If txtNewPass.Text <> txtNewPassConfirm.Text Then

            MsgBox("Password does not match, please try again", MsgBoxStyle.Critical, "Error!")

        Else
            Using con As New MySqlConnection(strCon)

                con.Open()

                Dim cmd As New MySqlCommand("UPDATE users SET password = @password WHERE username = @username", con)

                cmd.Parameters.AddWithValue("@password", txtNewPass.Text)
                cmd.Parameters.AddWithValue("@username", userInput.Text)

                cmd.ExecuteNonQuery()


                MsgBox("Updated Successfully!")

                Form1.Show()
                Me.Close()
            End Using
        End If

    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnVerify.Click

        If txtCode.Text <> randomCode Then
            MsgBox("Invalid code, please try again")

        Else

            MsgBox("Code verified!")
            Panel1.Visible = False
            btnVerify.Visible = False
            btnResend.Visible = False
            txtCode.Visible = False
            lblHeader.Visible = False
            lblSubheader.Visible = False
        End If




    End Sub

    Private Sub btnResend_Click(sender As Object, e As EventArgs) Handles btnResend.Click
        MsgBox("Resend Successfully!")
        random_number()
        email_verification()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class