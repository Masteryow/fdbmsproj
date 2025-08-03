Imports System.Diagnostics.Eventing.Reader
Imports System.Drawing.Text
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Remoting.Channels
Imports System.Text.RegularExpressions
Imports System.Threading
Imports MySql.Data.MySqlClient

Public Class CreateAccount

    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Private txtBox As New TextBox() 'for username input
    Private txtBox1 As New TextBox() 'for password input
    Private txtPassConfirm As New TextBox() 'for password verification
    Private label1 As New Label() 'for username
    Private label2 As New Label() 'for password
    Private label3 As New Label() 'for first restriction
    Private label4 As New Label() 'for second restriction
    Private label5 As New Label 'for third restriction
    Private label6 As New Label 'for fourth restriction
    Private label7 As New Label 'for password verification
    Private btnTrigger As New Button()
    Private storeTxtBox As New List(Of TextBox)
    Private storePass As New List(Of Char) 'for password verification
    Private initialPass As String = ""
    Dim strAnimation As String = "Register!, Be One, Of Us!, Skylink!"
    Dim eachWord As String() = strAnimation.Split(","c)
    Dim currentIndex As Integer = 0
    Dim isShowing As Boolean = False
    Private userEmpty = False

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles lblContact.Click

    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click



        'for checking if contact textbox is not empty but filled with invalid values


        If Not IsNumeric(txtContact.Text) And txtContact.Text <> "Please fill all the fields." _
                And Not String.IsNullOrEmpty(txtContact.Text) Then

            txtContact.ForeColor = Color.Red
            txtContact.Text = "Please Enter a Valid Number"

        ElseIf txtContact.Text.Length < 11 Then

            txtContact.ForeColor = Color.Red
            txtContact.Text = "Contact must be 11-digit"


        End If

        If String.IsNullOrEmpty(txtFirstName.Text) Or String.IsNullOrEmpty(txtLastName.Text) _
            Or String.IsNullOrEmpty(txtAddress.Text) Or String.IsNullOrEmpty(txtEmail.Text) Or
            String.IsNullOrEmpty(txtContact.Text) Or Not IsNumeric(txtContact.Text) Then

                'for making the textboxes red if they are empty or not filled properly

                For Each txt As Control In Panel2.Controls
                    If TypeOf txt Is TextBox Then
                        Dim convertedTxt As TextBox = DirectCast(txt, TextBox)
                        If String.IsNullOrEmpty(convertedTxt.Text) Then
                            convertedTxt.ForeColor = Color.Red
                            convertedTxt.Text = "Please fill all the fields."
                            storeTxtBox.Add(convertedTxt)
                        End If
                    End If
                Next



                Await Task.Delay(1000)

                'for clearing invalid input of contact textbox

                If Not IsNumeric(txtContact.Text) Then

                    txtContact.ForeColor = Color.Black
                    txtContact.Clear()


                End If

                'for clearing the textboxes if they are filled with "Please fill all the fields." (empty) text

                For Each txt1 As TextBox In storeTxtBox

                    If txt1.Text = "Please fill all the fields." Then
                        txt1.Clear()
                        txt1.ForeColor = Color.Black
                        btnSubmit.Enabled = True
                        btnClear.Enabled = True

                    End If

                Next

            Else
            'for making username and password creation visible upon submitting infos
            Try

                Using con As New MySqlConnection(strCon)

                    con.Open()

                    Dim getEmail As New MySqlCommand("SELECT COUNT(*) FROM users where email = @email", con)

                    getEmail.Parameters.AddWithValue("@email", txtEmail.Text)

                    Dim emailFound As Integer = CInt(getEmail.ExecuteScalar())

                    Dim getContact As New MySqlCommand("SELECT COUNT(*) FROM users where phoneNumber = @contact", con)

                    getContact.Parameters.AddWithValue("@contact", txtContact.Text)

                    Dim contactFound As Integer = CInt(getContact.ExecuteScalar())


                    If emailFound = 1 Or contactFound = 1 Then

                        MessageBox.Show("Email or Contact already exists!", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)


                    Else

                        Timer1.Stop()
                        Me.BackColor = Color.WhiteSmoke
                        Label8.Visible = False 'for text animation

                        txtBox.Visible = True
                        txtBox1.Visible = True
                        label1.Visible = True
                        label2.Visible = True
                        label3.Visible = True
                        label4.Visible = True
                        label5.Visible = True
                        label6.Visible = True
                        btnTrigger.Visible = True
                        btnSubmit.Enabled = False
                        txtBox1.Enabled = False

                    End If

                End Using


            Catch ex As MySqlException
                MessageBox.Show("ERROR: " & ex.Message, "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try





        End If









    End Sub

    Private Sub CreateAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 500
        Timer1.Start()
        'true for a while (debugging mode)
        txtBox.Visible = False
        txtBox1.Visible = False
        label1.Visible = False
        label2.Visible = False
        label3.Visible = False
        label4.Visible = False
        label5.Visible = False
        label6.Visible = False
        btnTrigger.Visible = False
        txtPassConfirm.PasswordChar = "*"
        txtBox1.PasswordChar = "*"
        txtBox.Multiline = False
        txtBox.Size = New Size(233, 33)
        txtBox.Location = New Point(320, 100)
        txtBox.Name = "txtUsername"
        txtBox.Anchor = AnchorStyles.None
        txtBox.BorderStyle = BorderStyle.FixedSingle
        txtBox.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Regular)
        AddHandler txtBox.TextChanged, AddressOf txtUserAndPassword_TextChanged
        Me.Controls.Add(txtBox)



        label1.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        label1.ForeColor = Color.DarkSlateGray
        label1.Location = New Point(400, 70)
        label1.Text = "Username"
        label1.Size = New Size(100, 22)
        label1.Name = "lblUsername"
        label1.Anchor = AnchorStyles.None
        Me.Controls.Add(label1)


        label2.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        label2.ForeColor = Color.DarkSlateGray
        label2.Location = New Point(400, 150)
        label2.Text = "Password"
        label2.Size = New Size(100, 22)
        label2.Name = "lblPassword"
        label2.Anchor = AnchorStyles.None
        Me.Controls.Add(label2)


        txtBox1.Multiline = False
        txtBox1.Size = New Size(233, 33)
        txtBox1.Location = New Point(320, 180)
        txtBox1.Name = "txtPassword"
        txtBox1.BorderStyle = BorderStyle.FixedSingle
        txtBox1.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Regular)
        txtBox1.PasswordChar = "*"
        txtBox1.Anchor = AnchorStyles.None
        txtBox.Anchor = AnchorStyles.None
        AddHandler txtBox1.TextChanged, AddressOf txtUserAndPassword_TextChanged
        Me.Controls.Add(txtBox1)

        label3.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label3.ForeColor = Color.DarkSlateGray
        label3.Location = New Point(323, 230)
        label3.Text = "• Must be at least 8 characters"
        label3.Size = New Size(500, 22)
        label3.Name = "lbl3"
        label3.Anchor = AnchorStyles.None
        Me.Controls.Add(label3)

        label4.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label4.ForeColor = Color.DarkSlateGray
        label4.Location = New Point(323, 250)
        label4.Text = "• Use both uppercase and lowercase 
                         letters"
        label4.Size = New Size(500, 15)
        label4.Name = "lbl4"
        label4.Anchor = AnchorStyles.None
        Me.Controls.Add(label4)

        label5.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label5.ForeColor = Color.DarkSlateGray
        label5.Location = New Point(323, 270)
        label5.Text = "• Include at least one number"
        label5.Size = New Size(500, 15)
        label5.Name = "lbl5"
        label5.Anchor = AnchorStyles.None
        Me.Controls.Add(label5)


        label6.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        label6.ForeColor = Color.DarkSlateGray
        label6.Location = New Point(323, 290)
        label6.Text = "• Add at least one special character"
        label6.Size = New Size(500, 15)
        label6.Name = "lbl6"
        label6.Anchor = AnchorStyles.None
        Me.Controls.Add(label6)



    End Sub

    Dim verifiedCount As Integer = 0

    Private Sub txtUserAndPassword_TextChanged(sender As Object, e As EventArgs)

        If String.IsNullOrEmpty(txtBox.Text) Then

            txtBox1.Enabled = False

            txtPassConfirm.Visible = False
            btnTrigger.Visible = False
            label7.Visible = False
        Else

            txtBox1.Enabled = True
            label3.Visible = True
            label4.Visible = True
            label5.Visible = True
            label6.Visible = True
            label7.Visible = True

            Dim strPassword As String = txtBox1.Text
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




            'possible to remove this logic since it will be textbox to textbox comparison
            If strPassword.Length > initialPass.Length Then

                storePass.Add(strPassword.Last())

            ElseIf strPassword.Length < initialPass.Length AndAlso storePass.Count > 0 Then

                Dim removedIndex As Integer = -1
                Dim minLength As Integer = Math.Min(strPassword.Length, initialPass.Length)

                For i As Integer = 0 To minLength - 1
                    If strPassword(i) <> initialPass(i) Then
                        removedIndex = i
                        Exit For
                    End If
                Next

                If removedIndex = -1 Then
                    removedIndex = initialPass.Length - 1
                End If

                If removedIndex >= 0 AndAlso removedIndex < storePass.Count Then
                    storePass.RemoveAt(removedIndex)

                End If

            End If

            initialPass = strPassword
            txtPassConfirm.Visible = False


            'for password verification section

            If Not String.IsNullOrEmpty(txtBox.Text) AndAlso hasUpper = True AndAlso hasLower = True AndAlso Len(strPassword) >= 8 AndAlso
               hasDigit = True AndAlso hasSpecial = True Then


                label3.Visible = False
                label4.Visible = False
                label5.Visible = False
                label6.Visible = False

                label7.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                label7.ForeColor = Color.DarkSlateGray
                label7.Location = New Point(360, 230)
                label7.Text = "Password Verification"
                label7.Size = New Size(200, 22)
                label7.Name = "lbl7"
                label7.Anchor = AnchorStyles.None
                Me.Controls.Add(label7)

                txtPassConfirm.Multiline = False
                txtPassConfirm.Size = New Size(233, 33)
                txtPassConfirm.Location = New Point(320, 260)
                txtPassConfirm.Name = "txtPconfirmation"
                txtPassConfirm.BorderStyle = BorderStyle.FixedSingle
                txtPassConfirm.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Regular)
                txtPassConfirm.PasswordChar = "*"
                txtPassConfirm.Visible = True
                txtPassConfirm.Anchor = AnchorStyles.None
                Me.Controls.Add(txtPassConfirm)


                btnTrigger.Visible = True
                btnTrigger.Text = "Submit"
                btnTrigger.Size = New Size(113, 34)
                btnTrigger.Location = New Point(388, 305)
                btnTrigger.BackColor = Color.WhiteSmoke
                btnTrigger.Anchor = AnchorStyles.None
                btnTrigger.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)

                RemoveHandler btnTrigger.Click, AddressOf btnTrigger_click
                AddHandler btnTrigger.Click, AddressOf btnTrigger_click


                Me.Controls.Add(btnTrigger)


                If String.IsNullOrEmpty(txtBox.Text) Then
                    userEmpty = True
                End If

            Else

                btnTrigger.Visible = False

                If userEmpty = True Then

                End If

            End If

        End If

    End Sub

    Private Async Sub btnTrigger_click(sender As Object, e As EventArgs)

        'possible to add restriction about fields cannot be empty

        If String.IsNullOrEmpty(txtPassConfirm.Text) Then

            txtPassConfirm.ForeColor = Color.Red
            txtPassConfirm.PasswordChar = ""
            txtPassConfirm.Text = "Please Enter Valid Values"

            Await Task.Delay(1000)
            txtPassConfirm.PasswordChar = "*"
            txtPassConfirm.Clear()
            txtPassConfirm.ForeColor = Color.Black
            Exit Sub
        End If

        If txtPassConfirm.Text <> txtBox1.Text Then

            txtPassConfirm.ForeColor = Color.Red
            txtPassConfirm.PasswordChar = ""
            txtPassConfirm.Text = "Passwords Do Not Match"

            Await Task.Delay(1000)
            txtPassConfirm.PasswordChar = "*"
            txtPassConfirm.Clear()
            txtPassConfirm.ForeColor = Color.Black

            Exit Sub
        End If




        Using con As New MySqlConnection(strCon)

            con.Open()

            Dim checkCmd As New MySqlCommand("SELECT COUNT(*) FROM users WHERE username = @username", con)
            checkCmd.Parameters.AddWithValue("@username", txtBox.Text)

            Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
            If exists = 1 Then
                MessageBox.Show("Username already exists. Please choose a different username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim cmd As New MySqlCommand("INSERT INTO users (username, password, firstName, lastName,
                                             email, phoneNumber, address) VALUES (@username, @password,
                                             @firstName, @lastName, @email, @phoneNumber, @address)", con)

            cmd.Parameters.AddWithValue("@username", txtBox.Text)
            cmd.Parameters.AddWithValue("@password", txtBox1.Text)
            cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text)
            cmd.Parameters.AddWithValue("@lastName", txtLastName.Text)
            cmd.Parameters.AddWithValue("@email", txtEmail.Text)
            cmd.Parameters.AddWithValue("@phoneNumber", txtContact.Text)
            cmd.Parameters.AddWithValue("@address", txtAddress.Text)

            cmd.ExecuteNonQuery()

        End Using

        MessageBox.Show("Account successfully created!")
        Form1.Show()
        Me.Close()


    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        txtFirstName.Clear()
        txtLastName.Clear()
        txtAddress.Clear()
        txtEmail.Clear()
        txtContact.Clear()

        txtBox.Clear()
        txtBox1.Clear()
        txtBox.Visible = False
        txtBox1.Visible = False
        label1.Visible = False
        label2.Visible = False
        label3.Visible = False
        label4.Visible = False
        label5.Visible = False
        label6.Visible = False
        label7.Visible = False
        txtPassConfirm.Visible = False
        btnTrigger.Visible = False
        btnSubmit.Enabled = True

        Timer1.Start()
        Label8.Visible = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Close()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If isShowing Then



            Label8.Text = eachWord(currentIndex).Trim()

            If Label8.Text = "Register!" Then

                Me.BackColor = Color.WhiteSmoke
                Label8.ForeColor = Color.Black
            ElseIf Label8.Text = "Be One" Then

                Me.BackColor = Color.Black
                Label8.ForeColor = Color.WhiteSmoke

            ElseIf Label8.Text = "Of Us!" Then
                Label8.ForeColor = Color.Black
                Me.BackColor = Color.WhiteSmoke


            ElseIf Label8.Text = "Skylink!" Then

                Me.BackColor = Color.Black

                Label8.ForeColor = Color.WhiteSmoke


            End If
            isShowing = False




        Else
            Label8.Text = ""
            currentIndex += 1


            If currentIndex >= eachWord.Length Then
                currentIndex = 0
            End If

            isShowing = True

        End If
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class