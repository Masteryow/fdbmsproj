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

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            con.Open()
            ' Modified query to also get the role and active status
            Dim getCredentials As New MySqlCommand("SELECT u.user_id, u.username, u.role, u.is_active, s.subscriber_id, s.status
                                                FROM users u LEFT JOIN subscribers s ON u.user_id = s.customer_id
                                                WHERE username = @username AND password = @password", con)
            getCredentials.Parameters.AddWithValue("@username", txtUsername.Text)
            getCredentials.Parameters.AddWithValue("@password", txtPassword.Text)
            Dim reader As MySqlDataReader = getCredentials.ExecuteReader()

            If reader.Read() Then
                Dim userId As Integer = reader.GetInt32("user_id")
                Dim userName As String = reader("username").ToString
                Dim userRole As String = reader("role").ToString
                Dim isActive As Boolean = reader.GetBoolean("is_active")

                Dim subscriber_id As Integer
                Dim subStatus As String = ""

                If reader.IsDBNull(reader.GetOrdinal("status")) Then
                    Session.subStatus = ""
                Else
                    Session.subStatus = reader.GetString("status")
                End If


                If reader.IsDBNull(reader.GetOrdinal("subscriber_id")) Then
                    subscriber_id = -1
                Else
                    subscriber_id = reader.GetInt32("subscriber_id")
                End If

                reader.Close()

                ' Check if account is active
                If Not isActive Then
                    MessageBox.Show("Your account has been deactivated. Please contact the system administrator for assistance.",
                              "Account Inactive",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning)
                    con.Close()
                    Return
                End If

                ' Store user information in session
                Session.UserId = userId
                Session.UserName = userName
                Session.userRole = userRole
                Session.SubscriberId = subscriber_id
                Session.subStatus = subStatus
                ' Redirect based on role
                Select Case userRole.ToLower()
                    Case "admin"
                        MessageBox.Show("Login Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Admin.Show()
                        Me.Close()

                    Case "technician"
                        Using getTechId As New MySqlCommand("SELECT t.technician_id, u.username FROM technicians t JOIN users u ON t.user_id = u.user_id WHERE u.user_id = @user_id", con)
                            getTechId.Parameters.AddWithValue("@user_id", CInt(Session.UserId))

                            Using fetchTechID As MySqlDataReader = getTechId.ExecuteReader
                                While fetchTechID.Read
                                    Session.technicianID = fetchTechID.GetInt32("technician_id")
                                End While
                            End Using
                        End Using
                        MessageBox.Show("Login Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        TechnicianPanel.Show()
                        Me.Close()

                    Case "subscriber"
                        MessageBox.Show("Login Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        subscribers.Show()
                        Me.Close()

                    Case "customer"
                        MessageBox.Show("Login Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Main.Show()
                        Me.Close()

                    Case "supervisor"
                        MessageBox.Show("Login Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' Add supervisor form here when created
                        ' SupervisorPanel.Show()
                        Me.Close()

                    Case Else
                        MessageBox.Show("Invalid user role. Please contact the system administrator.",
                                  "Access Denied",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error)
                End Select

            Else
                MessageBox.Show("Invalid username or password. Please check your credentials and try again.",
                          "Login Failed",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred during login: " & ex.Message,
                      "Error",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
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
