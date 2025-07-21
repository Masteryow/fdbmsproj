Imports System.Diagnostics.Eventing
Imports System.Diagnostics.Tracing
Imports System.Drawing.Drawing2D
Imports MySql.Data.MySqlClient

Public Class Form1

    Dim strAnimation As String = "Fast, Reliable, Secure, Connected, Skylink"
    Dim eachWord As String() = strAnimation.Split(","c)
    Dim currentIndex As Integer = 0
    Dim isShowing As Boolean = False
    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim con As New MySqlConnection(strCon)


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 500
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

            If Label1.Text = "Fast" Then

                Me.BackColor = Color.WhiteSmoke
                Label1.ForeColor = Color.Black
            ElseIf Label1.Text = "Reliable" Then

                Me.BackColor = Color.Black
                Label1.ForeColor = Color.WhiteSmoke

            ElseIf Label1.Text = "Secure" Then
                Label1.ForeColor = Color.Black
                Me.BackColor = Color.WhiteSmoke


            ElseIf Label1.Text = "Connected" Then

                Me.BackColor = Color.Black

                Label1.ForeColor = Color.WhiteSmoke

            ElseIf Label1.Text = "Skylink" Then
                Label1.ForeColor = Color.Black
                Me.BackColor = Color.WhiteSmoke
            End If
            isShowing = False




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

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click


        con.Open()
        ' Modified query to also get the role
        Dim getCredentials As New MySqlCommand("SELECT user_id, username, role FROM users WHERE username = @username AND password = @password", con)
        getCredentials.Parameters.AddWithValue("@username", txtUsername.Text)
        getCredentials.Parameters.AddWithValue("@password", txtPassword.Text)
        Dim reader As MySqlDataReader = getCredentials.ExecuteReader()

        If reader.Read() Then
            Dim userId As Integer = reader.GetInt32("user_id")
            Dim userName As String = reader("username").ToString
            Dim userRole As String = reader("role").ToString

            ' Store user information in session
            Session.UserId = userId
            Session.UserName = userName
            Session.UserRole = userRole ' You might want to add this to your Session class



            ' Redirect based on role
            Select Case userRole.ToLower()
                Case "admin"
                    MsgBox("Login Successful")
                    admin.Show()
                    Me.Close()
                'Case "employee"
                    ' employeeForm.Show()
                Case "customer"
                    MsgBox("Login Successful")
                    Main.Show()
                    Me.Close()
            End Select

            Me.Close()
        Else
            MsgBox("Invalid username or password.")
        End If

        reader.Close()
        con.Close()
    End Sub



    Private Sub lblForgotPass_Click(sender As Object, e As EventArgs) Handles lblForgotPass.Click

        Using con As New MySqlConnection(strCon)

            con.Open()

            Dim getUser As New MySqlCommand("SELECT COUNT(*) FROM users WHERE username = @username", con)

            getUser.Parameters.AddWithValue("@username", txtUsername.Text)

            getUser.ExecuteNonQuery()



            Dim isFound As Integer = CInt(getUser.ExecuteScalar())

            If isFound = 1 Then

                ForgotPassword.Show()
                Me.Close()


            Else
                MsgBox("Invalid Credentials")
            End If
        End Using
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        Session.UserName = txtUsername.Text
    End Sub
End Class
