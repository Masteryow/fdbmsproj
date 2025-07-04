Imports System.Drawing.Text
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Remoting.Channels
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class CreateAccount


    Private txtBox As New TextBox() 'for username input
    Private txtBox1 As New TextBox() 'for password input
    Private label1 As New Label() 'for username
    Private label2 As New Label() 'for password
    Private label3 As New Label() 'for first restriction
    Private label4 As New Label() 'for second restriction
    Private label5 As New Label 'for third restriction
    Private label6 As New Label 'for fourth restriction
    Private btnTrigger As New Button()
    Private storeTxtBox As New List(Of TextBox)

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click


        Try


            If Not IsNumeric(txtContact.Text) And txtContact.Text <> "Please fill all the fields." _
                And Not String.IsNullOrEmpty(txtContact.Text) Then

                txtContact.ForeColor = Color.Red
                txtContact.Text = "Please Enter a Valid Number"

            End If


            If String.IsNullOrEmpty(txtFirstName.Text) Or String.IsNullOrEmpty(txtLastName.Text) _
           Or String.IsNullOrEmpty(txtAddress.Text) Or String.IsNullOrEmpty(txtEmail.Text) Or
           String.IsNullOrEmpty(txtContact.Text) Or Not IsNumeric(txtContact.Text) Then


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

                If Not IsNumeric(txtContact.Text) Then

                    txtContact.ForeColor = Color.Black
                    txtContact.Clear()


                End If
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
            End If





        Catch ex As Exception

            MsgBox("An Error Has Occured!")
        End Try




    End Sub

    Private Sub CreateAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtBox.Visible = False
        txtBox1.Visible = False
        label1.Visible = False
        label2.Visible = False
        label3.Visible = False
        label4.Visible = False
        label5.Visible = False
        label6.Visible = False
        btnTrigger.Visible = False

        txtBox.Multiline = True
        txtBox.Size = New Size(233, 33)
        txtBox.Location = New Point(487, 100)
        txtBox.Name = "txtUsername"
        txtBox.BorderStyle = BorderStyle.FixedSingle
        txtBox.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)

        Me.Controls.Add(txtBox)



        label1.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        label1.ForeColor = Color.DarkSlateGray
        label1.Location = New Point(560, 70)
        label1.Text = "Username"
        label1.Size = New Size(100, 22)
        label1.Name = "lblUsername"
        Me.Controls.Add(label1)


        label2.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
        label2.ForeColor = Color.DarkSlateGray
        label2.Location = New Point(560, 150)
        label2.Text = "Password"
        label2.Size = New Size(100, 22)
        label2.Name = "lblPassword"
        Me.Controls.Add(label2)


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


        Dim txtbox1 As TextBox = DirectCast(sender, TextBox)
        If Len(txtbox1.Text) >= 8 Then
            label3.ForeColor = Color.DarkGreen
            label3.Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            verifiedCount += 1

        Else
            label3.ForeColor = Color.DarkSlateGray
            label3.Font = New Font("Microsoft Sans Serif", 7, FontStyle.Bold)
        End If
        Dim password As String = txtbox1.Text


        Dim hasUpper As Boolean = False
        Dim hasLower As Boolean = False
        Dim hasDigit As Boolean = False

        For Each ch As Char In txtbox1.Text
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




        For Each int As Char In txtbox1.Text
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


        If hasUpper = True And hasLower = True And Len(txtbox1.Text) > 8 And
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
        btnTrigger.Visible = False
        btnSubmit.Enabled = True
    End Sub


End Class