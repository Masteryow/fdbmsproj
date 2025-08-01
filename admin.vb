Imports MySql.Data.MySqlClient
Imports System.IO
Public Class Admin
    Private con As MySqlConnection
    Private currentActiveButton As Button
    Private targetColor As Color
    Private currentColor As Color
    Private animationStep As Integer = 0
    Private animatingButton As Button
    Dim strcon As String = "server=localhost; userid=root; database=fdbmsproject"
    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con = New MySqlConnection(strcon)
        currentActiveButton = btnDashboard
        ShowDashboard()
    End Sub

    ' Button Click Events
    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        AnimateButton(btnDashboard)
        lblTitle.Text = "Dashboard"
        ShowDashboard()
    End Sub

    Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
        AnimateButton(btnUsers)
        lblTitle.Text = "User Management"
        ShowUsersPanel()
    End Sub

    Private Sub btnPlans_Click(sender As Object, e As EventArgs) Handles btnPlans.Click
        AnimateButton(btnPlans)
        lblTitle.Text = "Internet Plans"
        ShowPlansPanel()
    End Sub

    Private Sub btnSubscribers_Click(sender As Object, e As EventArgs) Handles btnSubscribers.Click
        AnimateButton(btnSubscribers)
        lblTitle.Text = "Subscribers"
        ShowSubscribersPanel()
    End Sub

    Private Sub btnTechnicians_Click(sender As Object, e As EventArgs) Handles btnTechnicians.Click
        AnimateButton(btnTechnicians)
        lblTitle.Text = "Technicians"
        ShowTechniciansPanel()
    End Sub

    Private Sub btnAddons_Click(sender As Object, e As EventArgs) Handles btnAddons.Click

        AnimateButton(btnAddons)
        lblTitle.Text = "Add-ons"
        ShowAddonsPanel()
    End Sub

    Private Sub btnBilling_Click(sender As Object, e As EventArgs) Handles btnBilling.Click
        AnimateButton(btnBilling)
        lblTitle.Text = "Billing Management"
        ShowBillingPanel()
    End Sub

    Private Sub btnTickets_Click(sender As Object, e As EventArgs) Handles btnTickets.Click
        AnimateButton(btnTickets)
        lblTitle.Text = "Support Tickets"
        ShowTicketsPanel()
    End Sub

    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        AnimateButton(btnReports)
        lblTitle.Text = "Reports"
        ShowReportsPanel()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub

    ' Animation Methods
    Private Sub AnimateButton(btn As Button)
        If currentActiveButton IsNot Nothing Then
            currentActiveButton.BackColor = Color.Transparent
        End If

        animatingButton = btn
        targetColor = Color.FromArgb(37, 37, 38)
        currentColor = btn.BackColor
        animationStep = 0
        tmrAnimation.Start()

        currentActiveButton = btn
    End Sub

    Private Sub tmrAnimation_Tick(sender As Object, e As EventArgs) Handles tmrAnimation.Tick
        If animatingButton IsNot Nothing AndAlso animationStep < 10 Then
            ' Use Double for calculation to avoid overflow and rounding issues
            Dim r As Integer = CInt(Math.Min(255, Math.Max(0, CDbl(currentColor.R) + (CDbl(targetColor.R) - CDbl(currentColor.R)) * (animationStep / 10.0))))
            Dim g As Integer = CInt(Math.Min(255, Math.Max(0, CDbl(currentColor.G) + (CDbl(targetColor.G) - CDbl(currentColor.G)) * (animationStep / 10.0))))
            Dim b As Integer = CInt(Math.Min(255, Math.Max(0, CDbl(currentColor.B) + (CDbl(targetColor.B) - CDbl(currentColor.B)) * (animationStep / 10.0))))

            animatingButton.BackColor = Color.FromArgb(r, g, b)
            animationStep += 1
        Else
            tmrAnimation.Stop()
            If animatingButton IsNot Nothing Then
                animatingButton.BackColor = targetColor
            End If
        End If
    End Sub


    ' Helper Methods for Dashboard Statistics
    Private Function GetUserCount() As Integer
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM users", con)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            con.Close()
            Return count
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            Return 0
        End Try
    End Function

    Private Function GetPlansCount() As Integer
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM internet_plans", con)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            con.Close()
            Return count
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            Return 0
        End Try
    End Function

    Private Function GetOpenTicketsCount() As Integer
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM support_tickets WHERE status IN ('Open', 'In Progress')", con)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            con.Close()
            Return count
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            Return 0
        End Try
    End Function



    ' Panel Display Methods
    Private Sub ShowDashboard()
        pnlContent.Controls.Clear()

        Dim lblWelcome As New Label With {
            .Text = $"Welcome, {Session.UserName}!",
            .Font = New Font("Segoe UI", 16, FontStyle.Bold),
            .ForeColor = Color.FromArgb(37, 37, 38),
            .AutoSize = True,
            .Location = New Point(20, 20)
        }

        ' Statistics Cards
        Dim cardUsers As New Panel With {.Size = New Size(120, 80), .Location = New Point(20, 60), .BackColor = Color.FromArgb(52, 152, 219)}
        Dim lblUsersCount As New Label With {.Text = GetUserCount().ToString(), .Font = New Font("Segoe UI", 20, FontStyle.Bold), .ForeColor = Color.White, .Location = New Point(10, 10), .AutoSize = True}
        Dim lblUsersLabel As New Label With {.Text = "Total Users", .Font = New Font("Segoe UI", 10), .ForeColor = Color.White, .Location = New Point(10, 45), .AutoSize = True}
        cardUsers.Controls.AddRange({lblUsersCount, lblUsersLabel})

        Dim cardPlans As New Panel With {.Size = New Size(120, 80), .Location = New Point(160, 60), .BackColor = Color.FromArgb(46, 204, 113)}
        Dim lblPlansCount As New Label With {.Text = GetPlansCount().ToString(), .Font = New Font("Segoe UI", 20, FontStyle.Bold), .ForeColor = Color.White, .Location = New Point(10, 10), .AutoSize = True}
        Dim lblPlansLabel As New Label With {.Text = "Active Plans", .Font = New Font("Segoe UI", 10), .ForeColor = Color.White, .Location = New Point(10, 45), .AutoSize = True}
        cardPlans.Controls.AddRange({lblPlansCount, lblPlansLabel})

        Dim cardTickets As New Panel With {.Size = New Size(120, 80), .Location = New Point(300, 60), .BackColor = Color.FromArgb(231, 76, 60)}
        Dim lblTicketsCount As New Label With {.Text = GetOpenTicketsCount().ToString(), .Font = New Font("Segoe UI", 20, FontStyle.Bold), .ForeColor = Color.White, .Location = New Point(10, 10), .AutoSize = True}
        Dim lblTicketsLabel As New Label With {.Text = "Open Tickets", .Font = New Font("Segoe UI", 10), .ForeColor = Color.White, .Location = New Point(10, 45), .AutoSize = True}
        cardTickets.Controls.AddRange({lblTicketsCount, lblTicketsLabel})

        pnlContent.Controls.AddRange({lblWelcome, cardUsers, cardPlans, cardTickets})
    End Sub

    Private Sub ShowUsersPanel()
        pnlContent.Controls.Clear()

        ' Add User Form
        Dim lblAddUser As New Label With {.Text = "Add New User", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 3), .AutoSize = True}

        Dim txtUsername As New TextBox With {.Location = New Point(20, 50), .Size = New Size(100, 23)}
        Dim lblUsername As New Label With {.Text = "Username:", .Location = New Point(20, 30), .AutoSize = True}

        Dim txtPassword As New TextBox With {.Location = New Point(130, 50), .Size = New Size(100, 23), .UseSystemPasswordChar = True}
        Dim lblPassword As New Label With {.Text = "Password:", .Location = New Point(130, 30), .AutoSize = True}

        Dim cmbRole As New ComboBox With {.Location = New Point(240, 50), .Size = New Size(100, 23), .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbRole.Items.AddRange({"admin", "customer", "subscriber", "technician", "supervisor"})
        Dim lblRole As New Label With {.Text = "Role:", .Location = New Point(240, 30), .AutoSize = True}

        Dim txtFirstName As New TextBox With {.Location = New Point(20, 90), .Size = New Size(100, 23)}
        Dim lblFirstName As New Label With {.Text = "First Name:", .Location = New Point(20, 70), .AutoSize = True}

        Dim txtLastName As New TextBox With {.Location = New Point(130, 90), .Size = New Size(100, 23)}
        Dim lblLastName As New Label With {.Text = "Last Name:", .Location = New Point(130, 70), .AutoSize = True}

        Dim txtEmail As New TextBox With {.Location = New Point(240, 90), .Size = New Size(150, 23)}
        Dim lblEmail As New Label With {.Text = "Email:", .Location = New Point(240, 70), .AutoSize = True}

        Dim txtPhone As New TextBox With {.Location = New Point(20, 130), .Size = New Size(120, 23)}
        Dim lblPhone As New Label With {.Text = "Phone:", .Location = New Point(20, 110), .AutoSize = True}

        Dim txtAddress As New TextBox With {.Location = New Point(150, 130), .Size = New Size(200, 23)}
        Dim lblAddress As New Label With {.Text = "Address:", .Location = New Point(150, 110), .AutoSize = True}

        Dim btnAddUser As New Button With {.Text = "Add User", .Location = New Point(360, 130), .Size = New Size(75, 23), .BackColor = Color.FromArgb(52, 152, 219), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnAddUser.FlatAppearance.BorderSize = 0

        AddHandler btnAddUser.Click, Sub()
                                         If String.IsNullOrEmpty(txtUsername.Text) Or String.IsNullOrEmpty(txtPassword.Text) Or cmbRole.SelectedItem Is Nothing Then
                                             MessageBox.Show("Please fill all required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             Return
                                         End If

                                         Try
                                             con.Open()
                                             Dim cmd As New MySqlCommand("INSERT INTO users (username, password, role, firstName, lastName, email, phoneNumber, address, is_active) VALUES (@username, @password, @role, @firstName, @lastName, @email, @phone, @address, 1)", con)
                                             cmd.Parameters.AddWithValue("@username", txtUsername.Text)
                                             cmd.Parameters.AddWithValue("@password", txtPassword.Text)
                                             cmd.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString())
                                             cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text)
                                             cmd.Parameters.AddWithValue("@lastName", txtLastName.Text)
                                             cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                                             cmd.Parameters.AddWithValue("@phone", txtPhone.Text)
                                             cmd.Parameters.AddWithValue("@address", txtAddress.Text)

                                             cmd.ExecuteNonQuery()
                                             con.Close()

                                             MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                             ' Clear form fields
                                             txtUsername.Clear()
                                             txtPassword.Clear()
                                             cmbRole.SelectedIndex = -1
                                             txtFirstName.Clear()
                                             txtLastName.Clear()
                                             txtEmail.Clear()
                                             txtPhone.Clear()
                                             txtAddress.Clear()

                                             ShowUsersPanel() ' Refresh the panel
                                         Catch ex As Exception
                                             If con.State = ConnectionState.Open Then con.Close()
                                             MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                         End Try
                                     End Sub

        ' Users Management Section
        Dim lblManageUsers As New Label With {.Text = "Manage Users", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(20, 160), .AutoSize = True}

        ' Save Changes Button
        Dim btnSaveChanges As New Button With {.Text = "Save Changes", .Location = New Point(20, 185), .Size = New Size(100, 25), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnSaveChanges.FlatAppearance.BorderSize = 0

        ' Users List with editable status
        Dim dgvUsers As New DataGridView With {
        .Location = New Point(20, 215),
        .Size = New Size(400, 140),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    }

        LoadUsersDataEnhanced(dgvUsers)

        ' Save Changes Event Handler
        AddHandler btnSaveChanges.Click, Sub()
                                             Try
                                                 If con Is Nothing Then
                                                     con = New MySqlConnection(strcon)
                                                 End If

                                                 If con.State = ConnectionState.Open Then
                                                     con.Close()
                                                 End If

                                                 con.Open()
                                                 Dim changesCount As Integer = 0

                                                 For Each row As DataGridViewRow In dgvUsers.Rows
                                                     If Not row.IsNewRow Then
                                                         Dim userID As Integer = Convert.ToInt32(row.Cells("user_id").Value)
                                                         Dim isActive As Boolean = Convert.ToBoolean(row.Cells("is_active").Value)

                                                         Dim cmd As New MySqlCommand("UPDATE users SET is_active = @isActive WHERE user_id = @id", con)
                                                         cmd.Parameters.AddWithValue("@isActive", isActive)
                                                         cmd.Parameters.AddWithValue("@id", userID)
                                                         changesCount += cmd.ExecuteNonQuery()
                                                     End If
                                                 Next
                                                 con.Close()

                                                 MessageBox.Show($"Successfully updated {changesCount} user(s) status!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 LoadUsersDataEnhanced(dgvUsers) ' Refresh the data
                                             Catch ex As Exception
                                                 If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        pnlContent.Controls.AddRange({lblAddUser, lblUsername, txtUsername, lblPassword, txtPassword, lblRole, cmbRole, lblFirstName, txtFirstName, lblLastName, txtLastName, lblEmail, txtEmail, lblPhone, txtPhone, lblAddress, txtAddress, btnAddUser, lblManageUsers, btnSaveChanges, dgvUsers})
    End Sub

    Private Sub LoadUsersDataEnhanced(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            Dim cmd As New MySqlCommand("SELECT user_id, username, role, firstName, lastName, email, phoneNumber, is_active FROM users ORDER BY user_id", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("user_id") Then
                dgv.Columns("user_id").ReadOnly = True
                dgv.Columns("user_id").HeaderText = "ID"
                dgv.Columns("user_id").Width = 35
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").ReadOnly = True
                dgv.Columns("username").HeaderText = "Username"
                dgv.Columns("username").Width = 85
            End If

            If dgv.Columns.Contains("role") Then
                dgv.Columns("role").ReadOnly = True
                dgv.Columns("role").HeaderText = "Role"
                dgv.Columns("role").Width = 80
            End If

            If dgv.Columns.Contains("firstName") Then
                dgv.Columns("firstName").ReadOnly = True
                dgv.Columns("firstName").HeaderText = "First Name"
                dgv.Columns("firstName").Width = 85
            End If

            If dgv.Columns.Contains("lastName") Then
                dgv.Columns("lastName").ReadOnly = True
                dgv.Columns("lastName").HeaderText = "Last Name"
                dgv.Columns("lastName").Width = 85
            End If

            If dgv.Columns.Contains("email") Then
                dgv.Columns("email").ReadOnly = True
                dgv.Columns("email").HeaderText = "Email"
                dgv.Columns("email").Width = 120
            End If

            If dgv.Columns.Contains("phoneNumber") Then
                dgv.Columns("phoneNumber").ReadOnly = True
                dgv.Columns("phoneNumber").HeaderText = "Phone"
                dgv.Columns("phoneNumber").Width = 85
            End If

            ' Make is_active column a checkbox column
            If dgv.Columns.Contains("is_active") Then
                Dim activeIndex As Integer = dgv.Columns("is_active").Index
                dgv.Columns.RemoveAt(activeIndex)

                Dim activeColumn As New DataGridViewCheckBoxColumn()
                activeColumn.Name = "is_active"
                activeColumn.HeaderText = "Active"
                activeColumn.DataPropertyName = "is_active"
                activeColumn.Width = 60
                activeColumn.TrueValue = 1
                activeColumn.FalseValue = 0

                dgv.Columns.Insert(activeIndex, activeColumn)
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading users: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowPlansPanel()
        pnlContent.Controls.Clear()

        ' Add Plan Form
        Dim lblAddPlan As New Label With {.Text = "Add New Internet Plan", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 3), .AutoSize = True}

        Dim txtPlanName As New TextBox With {.Location = New Point(20, 50), .Size = New Size(120, 23)}
        Dim lblPlanName As New Label With {.Text = "Plan Name:", .Location = New Point(20, 30), .AutoSize = True}

        Dim txtPlanType As New TextBox With {.Location = New Point(150, 50), .Size = New Size(100, 23)}
        Dim lblPlanType As New Label With {.Text = "Plan Type:", .Location = New Point(150, 30), .AutoSize = True}

        Dim txtPrice As New TextBox With {.Location = New Point(260, 50), .Size = New Size(80, 23)}
        Dim lblPrice As New Label With {.Text = "Price:", .Location = New Point(260, 30), .AutoSize = True}

        Dim txtSpeed As New TextBox With {.Location = New Point(20, 90), .Size = New Size(100, 23)}
        Dim lblSpeed As New Label With {.Text = "Speed:", .Location = New Point(20, 70), .AutoSize = True}

        Dim txtDataCap As New TextBox With {.Location = New Point(130, 90), .Size = New Size(100, 23)}
        Dim lblDataCap As New Label With {.Text = "Data Cap:", .Location = New Point(130, 70), .AutoSize = True}

        ' Plans DataGridView with enhanced functionality (declare early)
        Dim dgvPlans As New DataGridView With {
        .Location = New Point(20, 170),
        .Size = New Size(400, 180),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    }

        ' Image Preview Section - positioned in available space
        Dim lblImagePreview As New Label With {.Text = "Image Preview:", .Font = New Font("Segoe UI", 9, FontStyle.Bold), .Location = New Point(260, 70), .AutoSize = True}
        Dim picPreview As New PictureBox With {
        .Location = New Point(260, 90),
        .Size = New Size(120, 80),
        .BorderStyle = BorderStyle.FixedSingle,
        .SizeMode = PictureBoxSizeMode.Zoom,
        .BackColor = Color.LightGray,
        .Name = "picPreview"
    }
        ' Add placeholder text
        AddHandler picPreview.Paint, Sub(sender, e)
                                         If picPreview.Image Is Nothing Then
                                             Dim g As Graphics = e.Graphics
                                             Dim text As String = "No Image Selected"
                                             Dim font As New Font("Segoe UI", 8)
                                             Dim brush As New SolidBrush(Color.Gray)
                                             Dim rect As Rectangle = picPreview.ClientRectangle
                                             Dim sf As New StringFormat()
                                             sf.Alignment = StringAlignment.Center
                                             sf.LineAlignment = StringAlignment.Center
                                             g.DrawString(text, font, brush, rect, sf)
                                         End If
                                     End Sub

        Dim btnAddPlan As New Button With {.Text = "Add Plan", .Location = New Point(350, 48), .Size = New Size(80, 23), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnAddPlan.FlatAppearance.BorderSize = 0

        AddHandler btnAddPlan.Click, Sub()
                                         If String.IsNullOrEmpty(txtPlanName.Text) OrElse String.IsNullOrEmpty(txtPrice.Text) Then
                                             MessageBox.Show("Please fill all required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             Return
                                         End If

                                         Dim blobId As Integer = Nothing
                                         Dim selectedImagePath As String = ""

                                         ' Step 1: Ask if user wants to attach an image
                                         If MessageBox.Show("Would you like to add an image to this plan?", "Add Image", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                             Using ofd As New OpenFileDialog()
                                                 ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif"
                                                 If ofd.ShowDialog() = DialogResult.OK Then
                                                     selectedImagePath = ofd.FileName
                                                 End If
                                             End Using
                                         End If

                                         Try
                                             con.Open()

                                             ' Step 2: If user selected an image, insert into blobs
                                             If Not String.IsNullOrEmpty(selectedImagePath) AndAlso File.Exists(selectedImagePath) Then
                                                 Dim fileBytes As Byte() = File.ReadAllBytes(selectedImagePath)
                                                 Dim fileName As String = Path.GetFileName(selectedImagePath)
                                                 Dim mimeType As String = "image/" & Path.GetExtension(fileName).TrimStart(".").ToLower()

                                                 Using cmdBlob As New MySqlCommand("INSERT INTO blobs (file_name, mime_type, data) VALUES (@fileName, @mimeType, @data); SELECT LAST_INSERT_ID();", con)
                                                     cmdBlob.Parameters.AddWithValue("@fileName", fileName)
                                                     cmdBlob.Parameters.AddWithValue("@mimeType", mimeType)
                                                     cmdBlob.Parameters.AddWithValue("@data", fileBytes)
                                                     blobId = Convert.ToInt32(cmdBlob.ExecuteScalar())
                                                 End Using
                                             End If

                                             ' Step 3: Insert into internet_plans with blob_id and is_active = True by default
                                             Using cmd As New MySqlCommand("INSERT INTO internet_plans (plan_name, plan_type, price, speed, data_cap, blob_id, is_active) VALUES (@name, @type, @price, @speed, @datacap, @blob_id, @is_active)", con)
                                                 cmd.Parameters.AddWithValue("@name", txtPlanName.Text)
                                                 cmd.Parameters.AddWithValue("@type", txtPlanType.Text)
                                                 cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text))
                                                 cmd.Parameters.AddWithValue("@speed", txtSpeed.Text)
                                                 cmd.Parameters.AddWithValue("@datacap", txtDataCap.Text)
                                                 cmd.Parameters.AddWithValue("@is_active", True) ' New plans are active by default
                                                 If blobId <> Nothing Then
                                                     cmd.Parameters.AddWithValue("@blob_id", blobId)
                                                 Else
                                                     cmd.Parameters.AddWithValue("@blob_id", DBNull.Value)
                                                 End If
                                                 cmd.ExecuteNonQuery()
                                             End Using

                                             con.Close()

                                             MessageBox.Show("Plan added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                             ' Clear fields
                                             txtPlanName.Clear()
                                             txtPlanType.Clear()
                                             txtPrice.Clear()
                                             txtSpeed.Clear()
                                             txtDataCap.Clear()
                                             picPreview.Image = Nothing

                                             ' Reload
                                             LoadPlansDataEnhanced(dgvPlans, picPreview)

                                         Catch ex As Exception
                                             If con.State = ConnectionState.Open Then con.Close()
                                             MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                         End Try
                                     End Sub

        ' Manage Plans Section
        Dim lblManagePlans As New Label With {.Text = "Manage Plans", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 115), .AutoSize = True}

        ' Save Changes Button (Delete button removed)
        Dim btnSaveChanges As New Button With {.Text = "Save Changes", .Location = New Point(20, 140), .Size = New Size(100, 25), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnSaveChanges.FlatAppearance.BorderSize = 0

        LoadPlansDataEnhanced(dgvPlans, picPreview)

        ' DataGridView Selection Changed Event - Update preview image
        AddHandler dgvPlans.SelectionChanged, Sub()
                                                  If dgvPlans.SelectedRows.Count > 0 Then
                                                      LoadImagePreview(dgvPlans.SelectedRows(0), picPreview)
                                                  End If
                                              End Sub

        ' Save Changes Event Handler - Updated to include is_active
        AddHandler btnSaveChanges.Click, Sub()
                                             Try
                                                 If con Is Nothing Then
                                                     con = New MySqlConnection(strcon)
                                                 End If

                                                 If con.State = ConnectionState.Open Then
                                                     con.Close()
                                                 End If

                                                 con.Open()
                                                 Dim changesCount As Integer = 0

                                                 For Each row As DataGridViewRow In dgvPlans.Rows
                                                     If Not row.IsNewRow Then
                                                         Dim planID As Integer = Convert.ToInt32(row.Cells("plan_id").Value)
                                                         Dim planName As String = row.Cells("plan_name").Value.ToString()
                                                         Dim planType As String = row.Cells("plan_type").Value.ToString()
                                                         Dim price As Decimal = Convert.ToDecimal(row.Cells("price").Value)
                                                         Dim speed As String = row.Cells("speed").Value.ToString()
                                                         Dim dataCap As String = row.Cells("data_cap").Value.ToString()
                                                         Dim isActive As Boolean = Convert.ToBoolean(row.Cells("is_active").Value)

                                                         Dim cmd As New MySqlCommand("UPDATE internet_plans SET plan_name = @name, plan_type = @type, price = @price, speed = @speed, data_cap = @datacap, is_active = @is_active WHERE plan_id = @id", con)
                                                         cmd.Parameters.AddWithValue("@name", planName)
                                                         cmd.Parameters.AddWithValue("@type", planType)
                                                         cmd.Parameters.AddWithValue("@price", price)
                                                         cmd.Parameters.AddWithValue("@speed", speed)
                                                         cmd.Parameters.AddWithValue("@datacap", dataCap)
                                                         cmd.Parameters.AddWithValue("@is_active", isActive)
                                                         cmd.Parameters.AddWithValue("@id", planID)
                                                         changesCount += cmd.ExecuteNonQuery()
                                                     End If
                                                 Next
                                                 con.Close()

                                                 MessageBox.Show($"Successfully updated {changesCount} plan(s)!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 LoadPlansDataEnhanced(dgvPlans, picPreview) ' Refresh the data
                                             Catch ex As Exception
                                                 If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        ' Updated controls array - removed btnDeletePlan
        pnlContent.Controls.AddRange({lblAddPlan, lblPlanName, txtPlanName, lblPlanType, txtPlanType, lblPrice, txtPrice, lblSpeed, txtSpeed, lblDataCap, txtDataCap, btnAddPlan, lblImagePreview, picPreview, lblManagePlans, btnSaveChanges, dgvPlans})
    End Sub

    Private Sub LoadPlansDataEnhanced(dgv As DataGridView, picPreview As PictureBox)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            ' Updated SQL query to include is_active column
            Dim cmd As New MySqlCommand("SELECT plan_id, plan_name, plan_type, price, speed, data_cap, is_active, blob_id FROM internet_plans ORDER BY plan_id", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Hide blob_id column
            If dgv.Columns.Contains("blob_id") Then
                dgv.Columns("blob_id").Visible = False
            End If

            ' Configure columns
            If dgv.Columns.Contains("plan_id") Then
                dgv.Columns("plan_id").ReadOnly = True
                dgv.Columns("plan_id").HeaderText = "ID"
                dgv.Columns("plan_id").Width = 50
            End If

            If dgv.Columns.Contains("plan_name") Then
                dgv.Columns("plan_name").ReadOnly = False
                dgv.Columns("plan_name").HeaderText = "Plan Name"
                dgv.Columns("plan_name").Width = 120
            End If

            If dgv.Columns.Contains("plan_type") Then
                dgv.Columns("plan_type").ReadOnly = False
                dgv.Columns("plan_type").HeaderText = "Type"
                dgv.Columns("plan_type").Width = 100
            End If

            If dgv.Columns.Contains("price") Then
                dgv.Columns("price").ReadOnly = False
                dgv.Columns("price").HeaderText = "Price"
                dgv.Columns("price").Width = 80
            End If

            If dgv.Columns.Contains("speed") Then
                dgv.Columns("speed").ReadOnly = False
                dgv.Columns("speed").HeaderText = "Speed"
                dgv.Columns("speed").Width = 100
            End If

            If dgv.Columns.Contains("data_cap") Then
                dgv.Columns("data_cap").ReadOnly = False
                dgv.Columns("data_cap").HeaderText = "Data Cap"
                dgv.Columns("data_cap").Width = 100
            End If

            ' Configure is_active column as checkbox
            If dgv.Columns.Contains("is_active") Then
                dgv.Columns("is_active").ReadOnly = False
                dgv.Columns("is_active").HeaderText = "Active"
                dgv.Columns("is_active").Width = 60
                ' Convert to checkbox column
                Dim checkBoxColumn As New DataGridViewCheckBoxColumn()
                checkBoxColumn.DataPropertyName = "is_active"
                checkBoxColumn.HeaderText = "Active"
                checkBoxColumn.Name = "is_active"
                checkBoxColumn.Width = 60
                checkBoxColumn.ReadOnly = False

                ' Remove the original column and add checkbox column
                Dim columnIndex As Integer = dgv.Columns("is_active").Index
                dgv.Columns.RemoveAt(columnIndex)
                dgv.Columns.Insert(columnIndex, checkBoxColumn)
            End If

            ' Add Browse Image Button Column
            Dim btnColumn As New DataGridViewButtonColumn()
            btnColumn.HeaderText = "Browse Image"
            btnColumn.Text = "Browse"
            btnColumn.UseColumnTextForButtonValue = True
            btnColumn.Width = 100
            btnColumn.Name = "BrowseButton"

            dgv.Columns.Add(btnColumn)

            If dgv.IsHandleCreated Then
                dgv.BeginInvoke(Sub()
                                    dgv.Columns("BrowseButton").DisplayIndex = dgv.Columns.Count - 1
                                End Sub)
            Else
                ' Wait until handle is created before trying BeginInvoke
                AddHandler dgv.HandleCreated, Sub(s, eArgs)
                                                  dgv.BeginInvoke(Sub()
                                                                      dgv.Columns("BrowseButton").DisplayIndex = dgv.Columns.Count - 1
                                                                  End Sub)
                                              End Sub
            End If

            ' Add event handler for button clicks
            RemoveHandler dgv.CellClick, AddressOf HandleBrowseButtonClick
            AddHandler dgv.CellClick, AddressOf HandleBrowseButtonClick

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading plans: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HandleBrowseButtonClick(sender As Object, e As DataGridViewCellEventArgs)
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)

        ' Check if it's the browse button column
        If e.ColumnIndex >= 0 AndAlso dgv.Columns(e.ColumnIndex).Name = "BrowseButton" AndAlso e.RowIndex >= 0 Then
            Dim planId As Integer = Convert.ToInt32(dgv.Rows(e.RowIndex).Cells("plan_id").Value)

            Using ofd As New OpenFileDialog()
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;"
                ofd.Title = "Select Image for Plan"

                If ofd.ShowDialog() = DialogResult.OK Then
                    Try
                        UpdatePlanImage(planId, ofd.FileName)
                        MessageBox.Show("Image updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If dgv.SelectedRows.Count > 0 AndAlso dgv.SelectedRows(0).Index = e.RowIndex Then
                            Dim picPreview As PictureBox = Nothing
                            For Each ctrl As Control In pnlContent.Controls
                                If TypeOf ctrl Is PictureBox AndAlso ctrl.Name = "picPreview" Then
                                    picPreview = DirectCast(ctrl, PictureBox)
                                    Exit For
                                End If
                            Next

                            If picPreview IsNot Nothing Then
                                LoadImagePreview(dgv.SelectedRows(0), picPreview)
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error updating image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End If
    End Sub

    Private Sub UpdatePlanImage(planId As Integer, imagePath As String)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Get current blob_id for this plan
            Dim currentBlobId As Object = Nothing
            Using cmd As New MySqlCommand("SELECT blob_id FROM internet_plans WHERE plan_id = @planId", con)
                cmd.Parameters.AddWithValue("@planId", planId)
                currentBlobId = cmd.ExecuteScalar()
            End Using

            ' Read new image file
            Dim fileBytes As Byte() = File.ReadAllBytes(imagePath)
            Dim fileName As String = Path.GetFileName(imagePath)
            Dim mimeType As String = "image/" & Path.GetExtension(fileName).TrimStart(".").ToLower()

            Dim newBlobId As Integer

            If currentBlobId IsNot Nothing AndAlso Not IsDBNull(currentBlobId) Then
                ' Update existing blob
                Using cmd As New MySqlCommand("UPDATE blobs SET file_name = @fileName, mime_type = @mimeType, data = @data WHERE blob_id = @blobId", con)
                    cmd.Parameters.AddWithValue("@fileName", fileName)
                    cmd.Parameters.AddWithValue("@mimeType", mimeType)
                    cmd.Parameters.AddWithValue("@data", fileBytes)
                    cmd.Parameters.AddWithValue("@blobId", currentBlobId)
                    cmd.ExecuteNonQuery()
                End Using
            Else
                ' Create new blob
                Using cmd As New MySqlCommand("INSERT INTO blobs (file_name, mime_type, data) VALUES (@fileName, @mimeType, @data); SELECT LAST_INSERT_ID();", con)
                    cmd.Parameters.AddWithValue("@fileName", fileName)
                    cmd.Parameters.AddWithValue("@mimeType", mimeType)
                    cmd.Parameters.AddWithValue("@data", fileBytes)
                    newBlobId = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                ' Update plan with new blob_id
                Using cmd As New MySqlCommand("UPDATE internet_plans SET blob_id = @blobId WHERE plan_id = @planId", con)
                    cmd.Parameters.AddWithValue("@blobId", newBlobId)
                    cmd.Parameters.AddWithValue("@planId", planId)
                    cmd.ExecuteNonQuery()
                End Using
            End If

            con.Close()

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            Throw ex
        End Try
    End Sub

    Private Sub LoadImagePreview(row As DataGridViewRow, picPreview As PictureBox)
        Try
            Dim blobId As Object = row.Cells("blob_id").Value

            If blobId IsNot Nothing AndAlso Not IsDBNull(blobId) Then
                If con Is Nothing Then
                    con = New MySqlConnection(strcon)
                End If

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If

                con.Open()
                Using cmd As New MySqlCommand("SELECT data FROM blobs WHERE blob_id = @blobId", con)
                    cmd.Parameters.AddWithValue("@blobId", blobId)
                    Dim imageData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

                    If imageData IsNot Nothing AndAlso imageData.Length > 0 Then
                        Using ms As New MemoryStream(imageData)
                            picPreview.Image = Image.FromStream(ms)
                        End Using
                    Else
                        picPreview.Image = Nothing
                    End If
                End Using
                con.Close()
            Else
                picPreview.Image = Nothing
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            picPreview.Image = Nothing
        End Try
    End Sub

    Private Sub ShowSubscribersPanel()
        pnlContent.Controls.Clear()

        Dim lblSubscribers As New Label With {
    .Text = "Subscriber Management",
    .Font = New Font("Segoe UI", 12, FontStyle.Bold),
    .Location = New Point(0, 0),
    .AutoSize = True
}

        ' Plan Type Filter Section
        Dim lblPlanType As New Label With {
    .Text = "Plan Type:",
    .Font = New Font("Segoe UI", 10, FontStyle.Bold),
    .Location = New Point(270, 30),
    .AutoSize = True
}

        Dim cboPlanType As New ComboBox With {
    .Location = New Point(350, 28),
    .Size = New Size(100, 23),
    .DropDownStyle = ComboBoxStyle.DropDownList
}
        cboPlanType.Items.AddRange({"All", "Prepaid", "Postpaid"})
        cboPlanType.SelectedIndex = 0

        ' Search Section
        Dim lblSearch As New Label With {
    .Text = "Search:",
    .Font = New Font("Segoe UI", 10, FontStyle.Bold),
    .Location = New Point(0, 30),
    .AutoSize = True
}

        Dim txtSearch As New TextBox With {
    .Location = New Point(60, 30),
    .Size = New Size(75, 23)
}

        Dim btnSearch As New Button With {
    .Text = "Search",
    .Location = New Point(142, 27),
    .Size = New Size(60, 23),
    .BackColor = Color.FromArgb(52, 152, 219),
    .ForeColor = Color.White,
    .FlatStyle = FlatStyle.Flat
}
        btnSearch.FlatAppearance.BorderSize = 0

        Dim btnShowAll As New Button With {
    .Text = "Update",
    .Location = New Point(205, 27),
    .Size = New Size(60, 23),
    .BackColor = Color.FromArgb(46, 204, 113),
    .ForeColor = Color.White,
    .FlatStyle = FlatStyle.Flat
}
        btnShowAll.FlatAppearance.BorderSize = 0

        ' Save changes button
        Dim btnSaveChanges As New Button With {
    .Text = "Save Changes",
    .Location = New Point(0, 60),
    .Size = New Size(100, 30),
    .BackColor = Color.FromArgb(46, 204, 113),
    .ForeColor = Color.White,
    .FlatStyle = FlatStyle.Flat
}
        btnSaveChanges.FlatAppearance.BorderSize = 0

        ' DataGridView
        Dim dgvSubscribers As New DataGridView With {
    .Location = New Point(0, 100),
    .Size = New Size(420, 250),
    .AllowUserToAddRows = False,
    .AllowUserToDeleteRows = False,
    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
}

        ' Load subscribers data
        LoadSubscribersDataEnhanced(dgvSubscribers)

        AddHandler dgvSubscribers.DataError, Sub(sender, e)
                                                 If e.Exception.GetType() = GetType(ArgumentException) Then
                                                     ' Handle ComboBox invalid value
                                                     MessageBox.Show("Invalid value detected. Setting to default.", "Data Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                                                     ' Set to first available value in ComboBox
                                                     Dim comboColumn As DataGridViewComboBoxColumn = TryCast(dgvSubscribers.Columns(e.ColumnIndex), DataGridViewComboBoxColumn)
                                                     If comboColumn IsNot Nothing AndAlso comboColumn.Items.Count > 0 Then
                                                         dgvSubscribers.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = comboColumn.Items(0)
                                                     End If

                                                     e.ThrowException = False ' Suppress the error dialog
                                                 End If
                                             End Sub

        ' Plan type filter change handler
        AddHandler cboPlanType.SelectedIndexChanged, Sub()
                                                         LoadSubscribersDataEnhanced(dgvSubscribers, txtSearch.Text.Trim(), cboPlanType.SelectedItem.ToString())
                                                     End Sub

        ' Search event handler
        AddHandler btnSearch.Click, Sub()
                                        LoadSubscribersDataEnhanced(dgvSubscribers, txtSearch.Text.Trim(), cboPlanType.SelectedItem.ToString())
                                    End Sub

        ' Show all event handler
        AddHandler btnShowAll.Click, Sub()
                                         txtSearch.Clear()
                                         cboPlanType.SelectedIndex = 0
                                         LoadSubscribersDataEnhanced(dgvSubscribers)
                                     End Sub

        ' Search on Enter key
        AddHandler txtSearch.KeyDown, Sub(sender, e)
                                          If e.KeyCode = Keys.Enter Then
                                              LoadSubscribersDataEnhanced(dgvSubscribers, txtSearch.Text.Trim(), cboPlanType.SelectedItem.ToString())
                                          End If
                                      End Sub

        ' Save changes event handler
        AddHandler btnSaveChanges.Click, Sub()
                                             Try
                                                 If con Is Nothing Then
                                                     con = New MySqlConnection(strcon)
                                                 End If

                                                 If con.State = ConnectionState.Open Then
                                                     con.Close()
                                                 End If

                                                 con.Open()
                                                 Dim changesCount As Integer = 0

                                                 For Each row As DataGridViewRow In dgvSubscribers.Rows
                                                     If Not row.IsNewRow Then
                                                         Dim subscriberID As Integer = Convert.ToInt32(row.Cells("subscriber_id").Value)
                                                         Dim newStatus As String = row.Cells("status").Value.ToString()

                                                         ' Check if the new plan column has a value
                                                         Dim newPlanID As Integer
                                                         If row.Cells("new_plan_id").Value IsNot Nothing Then
                                                             newPlanID = Convert.ToInt32(row.Cells("new_plan_id").Value)
                                                         Else
                                                             newPlanID = Convert.ToInt32(row.Cells("plan_id").Value)
                                                         End If

                                                         Dim cmd As New MySqlCommand("UPDATE subscribers SET status = @status, plan_id = @planid WHERE subscriber_id = @id", con)
                                                         cmd.Parameters.AddWithValue("@status", newStatus)
                                                         cmd.Parameters.AddWithValue("@planid", newPlanID)
                                                         cmd.Parameters.AddWithValue("@id", subscriberID)
                                                         changesCount += cmd.ExecuteNonQuery()
                                                     End If
                                                 Next
                                                 con.Close()

                                                 MessageBox.Show($"Successfully updated {changesCount} subscriber(s)!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 LoadSubscribersDataEnhanced(dgvSubscribers, txtSearch.Text.Trim(), cboPlanType.SelectedItem.ToString())
                                             Catch ex As Exception
                                                 If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        pnlContent.Controls.AddRange({lblSubscribers, lblPlanType, cboPlanType, lblSearch, txtSearch, btnSearch, btnShowAll, btnSaveChanges, dgvSubscribers})
    End Sub




    Private Sub LoadSubscribersDataEnhanced(dgv As DataGridView, Optional searchTerm As String = "", Optional planTypeFilter As String = "")
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build query with optional search and plan type filter
            Dim query As String = "SELECT s.subscriber_id, u.username, s.customer_id, s.plan_id, p.plan_name, p.plan_type, s.subscription_date, s.status " &
                             "FROM subscribers s " &
                             "INNER JOIN users u ON s.customer_id = u.user_id " &
                             "INNER JOIN internet_plans p ON s.plan_id = p.plan_id"

            Dim whereConditions As New List(Of String)

            If Not String.IsNullOrEmpty(searchTerm) Then
                whereConditions.Add("(u.username LIKE @search OR s.subscriber_id LIKE @search)")
            End If

            If Not String.IsNullOrEmpty(planTypeFilter) And planTypeFilter <> "All" Then
                whereConditions.Add("p.plan_type = @planType")
            End If

            If whereConditions.Count > 0 Then
                query += " WHERE " & String.Join(" AND ", whereConditions)
            End If

            query += " ORDER BY p.plan_type, s.subscriber_id"

            Dim cmd As New MySqlCommand(query, con)
            If Not String.IsNullOrEmpty(searchTerm) Then
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
            End If
            If Not String.IsNullOrEmpty(planTypeFilter) And planTypeFilter <> "All" Then
                cmd.Parameters.AddWithValue("@planType", planTypeFilter)
            End If

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ' Get available plans for dropdown
            Dim plansCmd As New MySqlCommand("SELECT plan_id, plan_name, plan_type FROM internet_plans ORDER BY plan_type, plan_name", con)
            Dim plansAdapter As New MySqlDataAdapter(plansCmd)
            Dim plansTable As New DataTable()
            plansAdapter.Fill(plansTable)

            con.Close()

            ' Check if we have data
            If dt.Rows.Count = 0 Then
                dgv.DataSource = Nothing
                Dim filterText As String = If(String.IsNullOrEmpty(planTypeFilter) Or planTypeFilter = "All", "", $" with {planTypeFilter} plans")
                If String.IsNullOrEmpty(searchTerm) Then
                    MessageBox.Show($"No subscribers found{filterText}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"No subscribers found matching '{searchTerm}'{filterText}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return
            End If

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Hide columns we don't want to show
            If dgv.Columns.Contains("customer_id") Then
                dgv.Columns("customer_id").Visible = False
            End If

            ' Make certain columns read-only
            If dgv.Columns.Contains("subscriber_id") Then
                dgv.Columns("subscriber_id").ReadOnly = True
                dgv.Columns("subscriber_id").HeaderText = "ID"
                dgv.Columns("subscriber_id").Width = 50
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").ReadOnly = True
                dgv.Columns("username").HeaderText = "Username"
                dgv.Columns("username").Width = 80
            End If

            If dgv.Columns.Contains("subscription_date") Then
                dgv.Columns("subscription_date").ReadOnly = True
                dgv.Columns("subscription_date").HeaderText = "Sub. Date"
                dgv.Columns("subscription_date").Width = 80
            End If

            If dgv.Columns.Contains("plan_name") Then
                dgv.Columns("plan_name").ReadOnly = True
                dgv.Columns("plan_name").HeaderText = "Current Plan"
                dgv.Columns("plan_name").Width = 90
            End If

            ' Add plan type column (read-only)
            If dgv.Columns.Contains("plan_type") Then
                dgv.Columns("plan_type").ReadOnly = True
                dgv.Columns("plan_type").HeaderText = "Plan Type"
                dgv.Columns("plan_type").Width = 70
                ' Color code the plan types
                For Each row As DataGridViewRow In dgv.Rows
                    If Not row.IsNewRow Then
                        Dim planType As String = row.Cells("plan_type").Value.ToString()
                        If planType = "Prepaid" Then
                            row.Cells("plan_type").Style.BackColor = Color.LightBlue
                            row.Cells("plan_type").Style.ForeColor = Color.DarkBlue
                        ElseIf planType = "Postpaid" Then
                            row.Cells("plan_type").Style.BackColor = Color.LightGreen
                            row.Cells("plan_type").Style.ForeColor = Color.DarkGreen
                        End If
                    End If
                Next
            End If

            ' Create dropdown for Status
            If dgv.Columns.Contains("status") Then
                Dim statusIndex As Integer = dgv.Columns("status").Index
                dgv.Columns.RemoveAt(statusIndex)

                Dim statusColumn As New DataGridViewComboBoxColumn()
                statusColumn.Name = "status"
                statusColumn.HeaderText = "Status"
                statusColumn.DataPropertyName = "status"
                statusColumn.Items.AddRange({"Active", "Pending", "Inactive"})
                statusColumn.Width = 70

                dgv.Columns.Insert(statusIndex, statusColumn)
            End If

            ' Create dropdown for Plan (hide the plan_id column and add new dropdown)
            If dgv.Columns.Contains("plan_id") Then
                dgv.Columns("plan_id").Visible = False

                Dim planColumn As New DataGridViewComboBoxColumn()
                planColumn.Name = "new_plan_id"
                planColumn.HeaderText = "Change Plan"
                planColumn.DataSource = plansTable
                planColumn.DisplayMember = "plan_name"
                planColumn.ValueMember = "plan_id"
                planColumn.Width = 120

                dgv.Columns.Add(planColumn)

                ' Set the current plan values in the dropdown
                For Each row As DataGridViewRow In dgv.Rows
                    If Not row.IsNewRow Then
                        Try
                            Dim currentPlanId As Integer = Convert.ToInt32(row.Cells("plan_id").Value)
                            Dim planExists As Boolean = False
                            For Each planRow As DataRow In plansTable.Rows
                                If Convert.ToInt32(planRow("plan_id")) = currentPlanId Then
                                    planExists = True
                                    Exit For
                                End If
                            Next

                            If planExists Then
                                row.Cells("new_plan_id").Value = currentPlanId
                            Else
                                If plansTable.Rows.Count > 0 Then
                                    row.Cells("new_plan_id").Value = plansTable.Rows(0)("plan_id")
                                End If
                            End If

                        Catch ex As Exception
                            If plansTable.Rows.Count > 0 Then
                                row.Cells("new_plan_id").Value = plansTable.Rows(0)("plan_id")
                            End If
                        End Try
                    End If
                Next
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading subscribers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowTechniciansPanel()
        pnlContent.Controls.Clear()

        ' Create panels for both pages
        Dim pnlPage1 As New Panel With {.Location = New Point(0, 0), .Size = pnlContent.Size, .Visible = True}
        Dim pnlPage2 As New Panel With {.Location = New Point(0, 0), .Size = pnlContent.Size, .Visible = False}

        ' ===== PAGE 1 - ORIGINAL TECHNICIAN MANAGEMENT =====
        ' Add Technician Form
        Dim lblAddTechnician As New Label With {.Text = "Add Technician Skills", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 3), .AutoSize = True}

        ' Dropdown for technician users who haven't been added to technicians table
        Dim cmbTechnicianUser As New ComboBox With {.Location = New Point(20, 50), .Size = New Size(150, 23), .DropDownStyle = ComboBoxStyle.DropDownList}
        Dim lblTechnicianUser As New Label With {.Text = "Select Technician:", .Location = New Point(20, 30), .AutoSize = True}

        ' Skills dropdown with predefined skills
        Dim cmbSkills As New ComboBox With {.Location = New Point(180, 50), .Size = New Size(150, 23), .DropDownStyle = ComboBoxStyle.DropDownList}
        Dim lblSkills As New Label With {.Text = "Skills:", .Location = New Point(180, 30), .AutoSize = True}

        ' Populate skills dropdown
        cmbSkills.Items.AddRange({
        "Fiber optic installation, Router configuration, Network troubleshooting, WiFi optimization",
        "5G technology, Signal optimization, Hardware replacement, Customer service",
        "Network security, VPN setup, Enterprise solutions, System integration",
        "Cable installation, Line repair, Infrastructure maintenance, Emergency response",
        "Advanced diagnostics, Mesh networks, Port forwarding, DNS configuration",
        "Team management, Technical oversight, Quality assurance, Training",
        "Project coordination, Resource allocation, Performance monitoring, Customer escalation",
        "Client installations, Coverage mapping, Signal testing, Equipment inventory",
        "Wireless networking, Site surveys, Link budget analysis, Tower climbing",
        "Service deployment, Network upgrades, Technical documentation, Remote troubleshooting",
        "Smart home setup, IoT configuration, Customer walkthroughs, Feedback collection",
        "Fiber splicing, Underground cabling, Load balancing, Incident reporting",
        "New subscriber setup, Latency testing, Firmware updates, Field reporting"
    })

        Dim btnAddTechnician As New Button With {.Text = "Add Tech", .Location = New Point(340, 49), .AutoSize = True, .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnAddTechnician.FlatAppearance.BorderSize = 0

        ' Technicians DataGridView with enhanced functionality (declare early for reference)
        Dim dgvTechnicians As New DataGridView With {
        .Location = New Point(20, 100),
        .Size = New Size(400, 200),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    }

        ' Load available technician users (those not in technicians table)
        LoadAvailableTechnicianUsers(cmbTechnicianUser)

        AddHandler btnAddTechnician.Click, Sub()
                                               If cmbTechnicianUser.SelectedValue Is Nothing Or cmbSkills.SelectedItem Is Nothing Then
                                                   MessageBox.Show("Please select both a technician and skills!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                   Return
                                               End If

                                               Try
                                                   con.Open()
                                                   Dim cmd As New MySqlCommand("INSERT INTO technicians (user_id, skills) VALUES (@userid, @skills)", con)
                                                   cmd.Parameters.AddWithValue("@userid", cmbTechnicianUser.SelectedValue)
                                                   cmd.Parameters.AddWithValue("@skills", cmbSkills.SelectedItem.ToString())

                                                   cmd.ExecuteNonQuery()
                                                   con.Close()

                                                   MessageBox.Show("Technician added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                   ' Clear form fields and refresh
                                                   cmbTechnicianUser.SelectedIndex = -1
                                                   cmbSkills.SelectedIndex = -1

                                                   ' Refresh both dropdowns and grid
                                                   LoadAvailableTechnicianUsers(cmbTechnicianUser)
                                                   LoadTechniciansDataEnhanced(dgvTechnicians)
                                               Catch ex As Exception
                                                   If con.State = ConnectionState.Open Then con.Close()
                                                   MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                               End Try
                                           End Sub

        ' Manage Technicians Section
        Dim lblManageTechnicians As New Label With {.Text = "Manage Technicians", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 75), .AutoSize = True}
        Dim btnUpdate As New Button With {.Text = "Update", .Location = New Point(20, 310), .Size = New Size(75, 25), .BackColor = Color.FromArgb(52, 152, 219), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnUpdate.FlatAppearance.BorderSize = 0

        ' Save Changes Button
        Dim btnSaveChanges As New Button With {.Text = "Save Changes", .Location = New Point(100, 310), .Size = New Size(100, 25), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnSaveChanges.FlatAppearance.BorderSize = 0

        ' NEW: Salary Management Button
        Dim btnSalaryManagement As New Button With {.Text = "Salary Management", .Location = New Point(210, 310), .Size = New Size(120, 25), .BackColor = Color.FromArgb(155, 89, 182), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnSalaryManagement.FlatAppearance.BorderSize = 0

        LoadTechniciansDataEnhanced(dgvTechnicians)

        ' Update Button Event Handler
        AddHandler btnUpdate.Click, Sub()
                                        LoadTechniciansDataEnhanced(dgvTechnicians)
                                    End Sub

        ' Save Changes Event Handler
        AddHandler btnSaveChanges.Click, Sub()
                                             Try
                                                 If con Is Nothing Then
                                                     con = New MySqlConnection(strcon)
                                                 End If

                                                 If con.State = ConnectionState.Open Then
                                                     con.Close()
                                                 End If

                                                 con.Open()
                                                 Dim changesCount As Integer = 0

                                                 For Each row As DataGridViewRow In dgvTechnicians.Rows
                                                     If Not row.IsNewRow Then
                                                         Dim technicianID As Integer = Convert.ToInt32(row.Cells("technician_id").Value)
                                                         Dim userID As Integer = Convert.ToInt32(row.Cells("user_id").Value)
                                                         Dim newSkills As String = row.Cells("skills").Value.ToString()
                                                         Dim isActive As Boolean = Convert.ToBoolean(row.Cells("is_active").Value)

                                                         ' Update technician skills
                                                         Dim techCmd As New MySqlCommand("UPDATE technicians SET skills = @skills WHERE technician_id = @id", con)
                                                         techCmd.Parameters.AddWithValue("@skills", newSkills)
                                                         techCmd.Parameters.AddWithValue("@id", technicianID)
                                                         changesCount += techCmd.ExecuteNonQuery()

                                                         ' Update user active status
                                                         Dim userCmd As New MySqlCommand("UPDATE users SET is_active = @isActive WHERE user_id = @userid", con)
                                                         userCmd.Parameters.AddWithValue("@isActive", isActive)
                                                         userCmd.Parameters.AddWithValue("@userid", userID)
                                                         userCmd.ExecuteNonQuery()
                                                     End If
                                                 Next
                                                 con.Close()

                                                 MessageBox.Show($"Successfully updated {changesCount} technician(s)!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 LoadTechniciansDataEnhanced(dgvTechnicians) ' Refresh the data
                                             Catch ex As Exception
                                                 If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        ' Add all Page 1 controls to pnlPage1
        pnlPage1.Controls.AddRange({lblAddTechnician, lblTechnicianUser, cmbTechnicianUser, lblSkills, cmbSkills, btnAddTechnician, lblManageTechnicians, btnSaveChanges, btnUpdate, btnSalaryManagement, dgvTechnicians})

        ' ===== PAGE 2 - SALARY MANAGEMENT =====
        ' Salary Management Title
        Dim lblSalaryManagement As New Label With {.Text = "Technician Salary Management", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 0), .AutoSize = True}

        ' Filter Section
        Dim lblFilterRole As New Label With {.Text = "Filter by Role:", .Location = New Point(20, 25), .AutoSize = True}
        Dim cmbRoleFilter As New ComboBox With {.Location = New Point(110, 23), .Size = New Size(100, 23), .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbRoleFilter.Items.AddRange({"All", "technician", "supervisor"})
        cmbRoleFilter.SelectedIndex = 0

        Dim btnFilterRole As New Button With {.Text = "Filter", .Location = New Point(220, 22), .Size = New Size(60, 25), .BackColor = Color.FromArgb(52, 152, 219), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnFilterRole.FlatAppearance.BorderSize = 0

        ' Payment Status Filter
        Dim lblPaymentFilter As New Label With {.Text = "Payment Status:", .Location = New Point(20, 55), .AutoSize = True}
        Dim cmbPaymentFilter As New ComboBox With {.Location = New Point(120, 53), .Size = New Size(100, 23), .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbPaymentFilter.Items.AddRange({"All", "Paid", "Unpaid"})
        cmbPaymentFilter.SelectedIndex = 0

        Dim btnFilterPayment As New Button With {.Text = "Filter", .Location = New Point(230, 52), .Size = New Size(60, 25), .BackColor = Color.FromArgb(52, 152, 219), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnFilterPayment.FlatAppearance.BorderSize = 0

        ' Salary DataGridView
        Dim dgvSalaries As New DataGridView With {
        .Location = New Point(20, 80),
        .Size = New Size(400, 200),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        .MultiSelect = True
    }



        ' Action Buttons (update Y positions to accommodate new button)
        Dim btnPaySelected As New Button With {.Text = "Pay Selected", .Location = New Point(20, 285), .Size = New Size(100, 25), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnPaySelected.FlatAppearance.BorderSize = 0

        Dim btnPayAll As New Button With {.Text = "Pay All Unpaid", .Location = New Point(125, 285), .Size = New Size(100, 25), .BackColor = Color.FromArgb(231, 76, 60), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnPayAll.FlatAppearance.BorderSize = 0

        Dim btnRefreshSalaries As New Button With {.Text = "Refresh", .Location = New Point(230, 285), .Size = New Size(70, 25), .BackColor = Color.FromArgb(52, 152, 219), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnRefreshSalaries.FlatAppearance.BorderSize = 0

        ' Back Button
        Dim btnBack As New Button With {.Text = "Back to Technicians", .Location = New Point(305, 285), .Size = New Size(115, 25), .BackColor = Color.FromArgb(149, 165, 166), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnBack.FlatAppearance.BorderSize = 0

        ' Variables to track current filters
        Dim currentRoleFilter As String = "All"
        Dim currentPaymentFilter As String = "All"

        ' Load initial salary data
        LoadSalaryData(dgvSalaries)

        ' Event Handlers for Page 2
        AddHandler btnFilterRole.Click, Sub()
                                            currentRoleFilter = cmbRoleFilter.SelectedItem.ToString()
                                            LoadSalaryData(dgvSalaries, currentRoleFilter, currentPaymentFilter)
                                        End Sub

        AddHandler btnFilterPayment.Click, Sub()
                                               currentPaymentFilter = cmbPaymentFilter.SelectedItem.ToString()
                                               LoadSalaryData(dgvSalaries, currentRoleFilter, currentPaymentFilter)
                                           End Sub

        AddHandler btnPaySelected.Click, Sub()
                                             If dgvSalaries.SelectedRows.Count = 0 Then
                                                 MessageBox.Show("Please select at least one salary record to pay!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                 Return
                                             End If

                                             PaySelectedSalaries(dgvSalaries)
                                             LoadSalaryData(dgvSalaries, currentRoleFilter, currentPaymentFilter)
                                         End Sub

        AddHandler btnPayAll.Click, Sub()
                                        If MessageBox.Show("Are you sure you want to pay all unpaid salaries?", "Confirm Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                            PayAllUnpaidSalaries()
                                            LoadSalaryData(dgvSalaries, currentRoleFilter, currentPaymentFilter)
                                        End If
                                    End Sub

        AddHandler btnRefreshSalaries.Click, Sub()
                                                 LoadSalaryData(dgvSalaries, currentRoleFilter, currentPaymentFilter)
                                             End Sub

        ' Add all Page 2 controls to pnlPage2
        pnlPage2.Controls.AddRange({lblSalaryManagement, lblFilterRole, cmbRoleFilter, btnFilterRole, lblPaymentFilter, cmbPaymentFilter, btnFilterPayment, dgvSalaries, btnPaySelected, btnPayAll, btnRefreshSalaries, btnBack})

        ' Page Navigation Event Handlers
        AddHandler btnSalaryManagement.Click, Sub()
                                                  pnlPage1.Visible = False
                                                  pnlPage2.Visible = True
                                                  LoadSalaryData(dgvSalaries)
                                              End Sub

        AddHandler btnBack.Click, Sub()
                                      pnlPage2.Visible = False
                                      pnlPage1.Visible = True
                                  End Sub

        ' Add both panels to main content panel
        pnlContent.Controls.AddRange({pnlPage1, pnlPage2})
    End Sub

    ' Helper method to load salary data from ticket_technicians table with filters
    Private Sub LoadSalaryData(dgv As DataGridView, Optional roleFilter As String = "All", Optional paymentFilter As String = "All")
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build query to get both technician and supervisor salaries
            Dim query As String = ""

            ' Technician salaries from completed tickets
            query = "SELECT " &
                "tt.ticket_id, " &
                "u.username, " &
                "u.firstName, " &
                "u.lastName, " &
                "u.role, " &
                "st.description as ticket_description, " &
                "it.issue_name, " &
                "it.difficulty_level, " &
                "st.task_salary as amount, " &
                "tt.assigned_at, " &
                "tt.status as ticket_status, " &
                "tt.payment_status, " &
                "tt.remarks, " &
                "st.created_at as ticket_created, " &
                "st.resolved_at, " &
                "'Task Payment' as salary_type " &
                "FROM ticket_technicians tt " &
                "JOIN technicians t ON tt.technician_id = t.technician_id " &
                "JOIN users u ON t.user_id = u.user_id " &
                "JOIN support_tickets st ON tt.ticket_id = st.ticket_id " &
                "JOIN issue_types it ON st.issue_type_id = it.issue_type_id " &
                "WHERE tt.status = 'Completed' AND st.task_salary > 0 " &
                "AND u.role = 'technician'"

            ' Add supervisor weekly salary from supervisor_salaries table
            query += " UNION ALL " &
                "SELECT " &
                "ss.salary_id as ticket_id, " &
                "u.username, " &
                "u.firstName, " &
                "u.lastName, " &
                "u.role, " &
                "CONCAT('Weekly Salary: ', DATE_FORMAT(ss.week_start_date, '%Y-%m-%d'), ' to ', DATE_FORMAT(ss.week_end_date, '%Y-%m-%d')) as ticket_description, " &
                "'Supervision' as issue_name, " &
                "'N/A' as difficulty_level, " &
                "ss.amount, " &
                "ss.created_at as assigned_at, " &
                "'Completed' as ticket_status, " &
                "ss.payment_status, " &
                "CONCAT('Weekly supervisor salary for week ', DATE_FORMAT(ss.week_start_date, '%Y-%m-%d')) as remarks, " &
                "ss.created_at as ticket_created, " &
                "ss.created_at as resolved_at, " &
                "'Weekly Salary' as salary_type " &
                "FROM supervisor_salaries ss " &
                "JOIN technicians t ON ss.supervisor_id = t.technician_id " &
                "JOIN users u ON t.user_id = u.user_id " &
                "WHERE u.role = 'supervisor' AND u.is_active = 1"

            ' Apply filters
            Dim whereConditions As String = ""
            If roleFilter <> "All" Then
                whereConditions = " WHERE role = @roleFilter"
            End If
            If paymentFilter <> "All" Then
                If whereConditions = "" Then
                    whereConditions = " WHERE payment_status = @paymentFilter"
                Else
                    whereConditions += " AND payment_status = @paymentFilter"
                End If
            End If

            ' Wrap the UNION query in a subquery to apply filters
            If whereConditions <> "" Then
                query = "SELECT * FROM (" & query & ") as combined_salaries" & whereConditions
            Else
                query = "SELECT * FROM (" & query & ") as combined_salaries"
            End If

            query += " ORDER BY assigned_at DESC"

            Dim cmd As New MySqlCommand(query, con)
            If roleFilter <> "All" Then
                cmd.Parameters.AddWithValue("@roleFilter", roleFilter)
            End If
            If paymentFilter <> "All" Then
                cmd.Parameters.AddWithValue("@paymentFilter", paymentFilter)
            End If

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("ticket_id") Then
                dgv.Columns("ticket_id").HeaderText = "Ticket ID"
                dgv.Columns("ticket_id").Width = 70
                dgv.Columns("ticket_id").ReadOnly = True
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").HeaderText = "Username"
                dgv.Columns("username").Width = 80
                dgv.Columns("username").ReadOnly = True
            End If

            If dgv.Columns.Contains("firstName") Then
                dgv.Columns("firstName").HeaderText = "First Name"
                dgv.Columns("firstName").Width = 80
                dgv.Columns("firstName").ReadOnly = True
            End If

            If dgv.Columns.Contains("lastName") Then
                dgv.Columns("lastName").HeaderText = "Last Name"
                dgv.Columns("lastName").Width = 80
                dgv.Columns("lastName").ReadOnly = True
            End If

            If dgv.Columns.Contains("role") Then
                dgv.Columns("role").HeaderText = "Role"
                dgv.Columns("role").Width = 80
                dgv.Columns("role").ReadOnly = True
            End If

            If dgv.Columns.Contains("issue_name") Then
                dgv.Columns("issue_name").HeaderText = "Issue Type"
                dgv.Columns("issue_name").Width = 120
                dgv.Columns("issue_name").ReadOnly = True
            End If

            If dgv.Columns.Contains("difficulty_level") Then
                dgv.Columns("difficulty_level").HeaderText = "Difficulty"
                dgv.Columns("difficulty_level").Width = 70
                dgv.Columns("difficulty_level").ReadOnly = True
            End If

            If dgv.Columns.Contains("amount") Then
                dgv.Columns("amount").HeaderText = "Salary (PHP)"
                dgv.Columns("amount").Width = 100
                dgv.Columns("amount").ReadOnly = True
                dgv.Columns("amount").DefaultCellStyle.Format = "N2"
            End If

            If dgv.Columns.Contains("ticket_status") Then
                dgv.Columns("ticket_status").HeaderText = "Task Status"
                dgv.Columns("ticket_status").Width = 80
                dgv.Columns("ticket_status").ReadOnly = True
            End If

            If dgv.Columns.Contains("payment_status") Then
                dgv.Columns("payment_status").HeaderText = "Payment"
                dgv.Columns("payment_status").Width = 70
                dgv.Columns("payment_status").ReadOnly = True
            End If

            If dgv.Columns.Contains("salary_type") Then
                dgv.Columns("salary_type").HeaderText = "Type"
                dgv.Columns("salary_type").Width = 90
                dgv.Columns("salary_type").ReadOnly = True
            End If

            If dgv.Columns.Contains("assigned_at") Then
                dgv.Columns("assigned_at").HeaderText = "Date"
                dgv.Columns("assigned_at").Width = 100
                dgv.Columns("assigned_at").ReadOnly = True
            End If

            ' Hide detailed columns that might clutter the view
            If dgv.Columns.Contains("ticket_description") Then
                dgv.Columns("ticket_description").Visible = False
            End If
            If dgv.Columns.Contains("remarks") Then
                dgv.Columns("remarks").Visible = False
            End If
            If dgv.Columns.Contains("ticket_created") Then
                dgv.Columns("ticket_created").Visible = False
            End If
            If dgv.Columns.Contains("resolved_at") Then
                dgv.Columns("resolved_at").Visible = False
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading salary data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    ' Helper method to pay selected salaries
    Private Sub PaySelectedSalaries(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            Dim updatedCount As Integer = 0

            For Each row As DataGridViewRow In dgv.SelectedRows
                If Not row.IsNewRow Then
                    Dim currentStatus As String = row.Cells("payment_status").Value.ToString()
                    Dim salaryType As String = row.Cells("salary_type").Value.ToString()

                    If currentStatus = "Unpaid" Then
                        If salaryType = "Task Payment" Then
                            ' Handle technician task payment
                            Dim ticketID As Integer = Convert.ToInt32(row.Cells("ticket_id").Value)
                            Dim cmd As New MySqlCommand("UPDATE ticket_technicians SET payment_status = 'Paid' WHERE ticket_id = @ticketId", con)
                            cmd.Parameters.AddWithValue("@ticketId", ticketID)
                            updatedCount += cmd.ExecuteNonQuery()
                        ElseIf salaryType = "Weekly Salary" Then
                            ' Handle supervisor weekly salary
                            Dim salaryID As Integer = Convert.ToInt32(row.Cells("ticket_id").Value)
                            Dim cmd As New MySqlCommand("UPDATE supervisor_salaries SET payment_status = 'Paid', paid_at = CURRENT_TIMESTAMP WHERE salary_id = @salaryId", con)
                            cmd.Parameters.AddWithValue("@salaryId", salaryID)
                            updatedCount += cmd.ExecuteNonQuery()
                        End If
                    End If
                End If
            Next

            con.Close()
            If updatedCount > 0 Then
                MessageBox.Show($"Successfully paid {updatedCount} salary record(s)!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error paying selected salaries: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper method to pay all unpaid salaries
    Private Sub PayAllUnpaidSalaries()
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Pay all unpaid technician task salaries
            Dim techCmd As New MySqlCommand("UPDATE ticket_technicians SET payment_status = 'Paid' WHERE payment_status = 'Unpaid' AND status = 'Completed'", con)
            Dim techUpdatedCount As Integer = techCmd.ExecuteNonQuery()

            ' Pay all unpaid supervisor weekly salaries
            Dim supervisorCmd As New MySqlCommand("UPDATE supervisor_salaries SET payment_status = 'Paid', paid_at = CURRENT_TIMESTAMP WHERE payment_status = 'Unpaid'", con)
            Dim supervisorUpdatedCount As Integer = supervisorCmd.ExecuteNonQuery()

            con.Close()

            MessageBox.Show($"Successfully paid:" & vbCrLf &
                        $"- {techUpdatedCount} technician task salary record(s)" & vbCrLf &
                        $"- {supervisorUpdatedCount} supervisor weekly salary record(s)",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error paying all unpaid salaries: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub LoadAvailableTechnicianUsers(cmb As ComboBox)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            ' Get users with role 'technician' who are not in the technicians table
            Dim cmd As New MySqlCommand("SELECT u.user_id, CONCAT(u.firstName, ' ', u.lastName, ' (', u.username, ')') as display_name FROM users u WHERE u.role IN ('technician','supervisor') AND u.user_id NOT IN (SELECT user_id FROM technicians)", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            cmb.DataSource = dt
            cmb.DisplayMember = "display_name"
            cmb.ValueMember = "user_id"
            cmb.SelectedIndex = -1

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading available technicians: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadTechniciansDataBasic(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            Dim cmd As New MySqlCommand("SELECT t.technician_id, t.user_id, u.username, u.firstName, u.lastName, t.skills, u.is_active FROM technicians t JOIN users u ON t.user_id = u.user_id ORDER BY t.technician_id", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure all columns as read-only initially
            If dgv.Columns.Contains("technician_id") Then
                dgv.Columns("technician_id").ReadOnly = True
                dgv.Columns("technician_id").HeaderText = "Tech ID"
                dgv.Columns("technician_id").Width = 60
            End If

            If dgv.Columns.Contains("user_id") Then
                dgv.Columns("user_id").Visible = False ' Hide user_id column
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").ReadOnly = True
                dgv.Columns("username").HeaderText = "Username"
                dgv.Columns("username").Width = 80
            End If

            If dgv.Columns.Contains("firstName") Then
                dgv.Columns("firstName").ReadOnly = True
                dgv.Columns("firstName").HeaderText = "First Name"
                dgv.Columns("firstName").Width = 80
            End If

            If dgv.Columns.Contains("lastName") Then
                dgv.Columns("lastName").ReadOnly = True
                dgv.Columns("lastName").HeaderText = "Last Name"
                dgv.Columns("lastName").Width = 80
            End If

            If dgv.Columns.Contains("skills") Then
                dgv.Columns("skills").ReadOnly = True
                dgv.Columns("skills").HeaderText = "Skills"
                dgv.Columns("skills").Width = 150
            End If

            If dgv.Columns.Contains("is_active") Then
                dgv.Columns("is_active").ReadOnly = True
                dgv.Columns("is_active").HeaderText = "Active"
                dgv.Columns("is_active").Width = 60
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading technicians: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadTechniciansDataEnhanced(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            Dim cmd As New MySqlCommand("SELECT t.technician_id, t.user_id, u.username, u.firstName, u.lastName, t.skills, u.is_active FROM technicians t JOIN users u ON t.user_id = u.user_id ORDER BY t.technician_id", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("technician_id") Then
                dgv.Columns("technician_id").ReadOnly = True
                dgv.Columns("technician_id").HeaderText = "Tech ID"
                dgv.Columns("technician_id").Width = 60
            End If

            If dgv.Columns.Contains("user_id") Then
                dgv.Columns("user_id").Visible = False ' Hide user_id column
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").ReadOnly = True
                dgv.Columns("username").HeaderText = "Username"
                dgv.Columns("username").Width = 80
            End If

            If dgv.Columns.Contains("firstName") Then
                dgv.Columns("firstName").ReadOnly = True
                dgv.Columns("firstName").HeaderText = "First Name"
                dgv.Columns("firstName").Width = 80
            End If

            If dgv.Columns.Contains("lastName") Then
                dgv.Columns("lastName").ReadOnly = True
                dgv.Columns("lastName").HeaderText = "Last Name"
                dgv.Columns("lastName").Width = 80
            End If

            ' Create dropdown for Skills
            If dgv.Columns.Contains("skills") Then
                Dim skillsIndex As Integer = dgv.Columns("skills").Index
                dgv.Columns.RemoveAt(skillsIndex)

                Dim skillsColumn As New DataGridViewComboBoxColumn()
                skillsColumn.Name = "skills"
                skillsColumn.HeaderText = "Skills"
                skillsColumn.DataPropertyName = "skills"
                skillsColumn.Items.AddRange({
                "Fiber optic installation, Router configuration, Network troubleshooting, WiFi optimization",
                "5G technology, Signal optimization, Hardware replacement, Customer service",
                "Network security, VPN setup, Enterprise solutions, System integration",
                "Cable installation, Line repair, Infrastructure maintenance, Emergency response",
                "Advanced diagnostics, Mesh networks, Port forwarding, DNS configuration",
                "Team management, Technical oversight, Quality assurance, Training",
                "Project coordination, Resource allocation, Performance monitoring, Customer escalation",
                "Client installations, Coverage mapping, Signal testing, Equipment inventory",
                "Wireless networking, Site surveys, Link budget analysis, Tower climbing",
                "Service deployment, Network upgrades, Technical documentation, Remote troubleshooting",
                "Smart home setup, IoT configuration, Customer walkthroughs, Feedback collection",
                "Fiber splicing, Underground cabling, Load balancing, Incident reporting",
                "New subscriber setup, Latency testing, Firmware updates, Field reporting"
            })
                skillsColumn.Width = 150

                dgv.Columns.Insert(skillsIndex, skillsColumn)
            End If

            ' Make is_active column a checkbox column
            If dgv.Columns.Contains("is_active") Then
                Dim activeIndex As Integer = dgv.Columns("is_active").Index
                dgv.Columns.RemoveAt(activeIndex)

                Dim activeColumn As New DataGridViewCheckBoxColumn()
                activeColumn.Name = "is_active"
                activeColumn.HeaderText = "Active"
                activeColumn.DataPropertyName = "is_active"
                activeColumn.Width = 60
                activeColumn.TrueValue = 1
                activeColumn.FalseValue = 0

                dgv.Columns.Insert(activeIndex, activeColumn)
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading technicians: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowAddonsPanel()
        pnlContent.Controls.Clear()

        ' Create panels for both pages
        Dim pnlPage1 As New Panel With {.Location = New Point(0, 0), .Size = pnlContent.Size, .Visible = True}
        Dim pnlPage2 As New Panel With {.Location = New Point(0, 0), .Size = pnlContent.Size, .Visible = False}

        ' ===== PAGE 1 - ORIGINAL ADDON MANAGEMENT =====
        ' Add Addon Form
        Dim lblAddAddon As New Label With {.Text = "Add New Addon", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 3), .AutoSize = True}

        Dim txtItemName As New TextBox With {.Location = New Point(20, 50), .Size = New Size(120, 23)}
        Dim lblItemName As New Label With {.Text = "Item Name:", .Location = New Point(20, 30), .AutoSize = True}

        Dim cmbCategory As New ComboBox With {.Location = New Point(150, 50), .Size = New Size(100, 23), .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbCategory.Items.AddRange({"Hardware", "Service", "Plan Upgrade"})
        Dim lblCategory As New Label With {.Text = "Category:", .Location = New Point(150, 30), .AutoSize = True}

        Dim txtPrice As New TextBox With {.Location = New Point(260, 50), .Size = New Size(80, 23)}
        Dim lblPrice As New Label With {.Text = "Price:", .Location = New Point(260, 30), .AutoSize = True}
        ' Addons DataGridView with enhanced functionality (declare early for reference)
        Dim dgvAddons As New DataGridView With {
        .Location = New Point(20, 120),
        .Size = New Size(300, 180),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        .EditMode = DataGridViewEditMode.EditOnEnter
    }

        ' Image Preview Section - keep original positioning
        Dim lblImagePreview As New Label With {.Text = "Image Preview:", .Font = New Font("Segoe UI", 9, FontStyle.Bold), .Location = New Point(325, 120), .AutoSize = True}
        Dim picPreview As New PictureBox With {
        .Location = New Point(330, 135),
        .Size = New Size(120, 80),
        .BorderStyle = BorderStyle.FixedSingle,
        .SizeMode = PictureBoxSizeMode.Zoom,
        .BackColor = Color.LightGray,
        .Name = "picPreview"
    }
        ' Add placeholder text
        AddHandler picPreview.Paint, Sub(sender, e)
                                         If picPreview.Image Is Nothing Then
                                             Dim g As Graphics = e.Graphics
                                             Dim text As String = "No Image Selected"
                                             Dim font As New Font("Segoe UI", 8)
                                             Dim brush As New SolidBrush(Color.Gray)
                                             Dim rect As Rectangle = picPreview.ClientRectangle
                                             Dim sf As New StringFormat()
                                             sf.Alignment = StringAlignment.Center
                                             sf.LineAlignment = StringAlignment.Center
                                             g.DrawString(text, font, brush, rect, sf)
                                         End If
                                     End Sub

        Dim btnAddAddon As New Button With {.Text = "Add Add-on", .Location = New Point(350, 50), .Size = New Size(80, 23), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnAddAddon.FlatAppearance.BorderSize = 0

        AddHandler btnAddAddon.Click, Sub()
                                          If String.IsNullOrEmpty(txtItemName.Text) Or String.IsNullOrEmpty(txtPrice.Text) Or cmbCategory.SelectedItem Is Nothing Then
                                              MessageBox.Show("Please fill all required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              Return
                                          End If

                                          ' Validate price
                                          Dim price As Decimal
                                          If Not Decimal.TryParse(txtPrice.Text, price) OrElse price <= 0 Then
                                              MessageBox.Show("Please enter a valid price!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              Return
                                          End If

                                          Dim blobId As Integer = Nothing
                                          Dim selectedImagePath As String = ""

                                          ' Step 1: Ask if user wants to attach an image
                                          If MessageBox.Show("Would you like to add an image to this addon?", "Add Image", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                              Using ofd As New OpenFileDialog()
                                                  ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif"
                                                  If ofd.ShowDialog() = DialogResult.OK Then
                                                      selectedImagePath = ofd.FileName
                                                  End If
                                              End Using
                                          End If

                                          Try
                                              con.Open()

                                              ' Step 2: If user selected an image, insert into blobs
                                              If Not String.IsNullOrEmpty(selectedImagePath) AndAlso File.Exists(selectedImagePath) Then
                                                  Dim fileBytes As Byte() = File.ReadAllBytes(selectedImagePath)
                                                  Dim fileName As String = Path.GetFileName(selectedImagePath)
                                                  Dim mimeType As String = "image/" & Path.GetExtension(fileName).TrimStart(".").ToLower()

                                                  Using cmdBlob As New MySqlCommand("INSERT INTO blobs (file_name, mime_type, data) VALUES (@fileName, @mimeType, @data); SELECT LAST_INSERT_ID();", con)
                                                      cmdBlob.Parameters.AddWithValue("@fileName", fileName)
                                                      cmdBlob.Parameters.AddWithValue("@mimeType", mimeType)
                                                      cmdBlob.Parameters.AddWithValue("@data", fileBytes)
                                                      blobId = Convert.ToInt32(cmdBlob.ExecuteScalar())
                                                  End Using
                                              End If

                                              ' Step 3: Insert into addons with blob_id and is_active = True by default
                                              Using cmd As New MySqlCommand("INSERT INTO addons (item_name, category, price, blob_id, is_active) VALUES (@itemname, @category, @price, @blob_id, @is_active)", con)
                                                  cmd.Parameters.AddWithValue("@itemname", txtItemName.Text.Trim())
                                                  cmd.Parameters.AddWithValue("@category", cmbCategory.SelectedItem.ToString())
                                                  cmd.Parameters.AddWithValue("@price", price)
                                                  cmd.Parameters.AddWithValue("@is_active", True) ' New addons are active by default
                                                  If blobId <> Nothing Then
                                                      cmd.Parameters.AddWithValue("@blob_id", blobId)
                                                  Else
                                                      cmd.Parameters.AddWithValue("@blob_id", DBNull.Value)
                                                  End If

                                                  cmd.ExecuteNonQuery()

                                                  ' If it's hardware, add to stock table with 0 initial stock
                                                  If cmbCategory.SelectedItem.ToString() = "Hardware" Then
                                                      Dim addonId As Integer = cmd.LastInsertedId
                                                      Dim stockCmd As New MySqlCommand("INSERT INTO hardware_stocks (addon_id, quantity_available) VALUES (@addonid, 0)", con)
                                                      stockCmd.Parameters.AddWithValue("@addonid", addonId)
                                                      stockCmd.ExecuteNonQuery()
                                                  End If
                                              End Using

                                              con.Close()

                                              MessageBox.Show("Add-on added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                              ' Clear form fields
                                              txtItemName.Clear()
                                              cmbCategory.SelectedIndex = -1
                                              txtPrice.Clear()
                                              picPreview.Image = Nothing

                                              ' Refresh the addons list
                                              LoadAddonsDataEnhanced(dgvAddons, picPreview)

                                          Catch ex As Exception
                                              If con.State = ConnectionState.Open Then con.Close()
                                              MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                          End Try
                                      End Sub

        ' Search Section
        Dim lblSearch As New Label With {.Text = "Search:", .Location = New Point(20, 93), .AutoSize = True}
        Dim txtSearch As New TextBox With {.Location = New Point(80, 90), .Size = New Size(150, 23)}
        Dim btnSearch As New Button With {.Text = "Search", .Location = New Point(240, 89), .Size = New Size(60, 23), .BackColor = Color.FromArgb(52, 152, 219), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnSearch.FlatAppearance.BorderSize = 0

        Dim btnShowAll As New Button With {.Text = "Show All", .Location = New Point(310, 89), .Size = New Size(70, 23), .BackColor = Color.FromArgb(52, 152, 219), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnShowAll.FlatAppearance.BorderSize = 0

        ' Manage Addons Section
        Dim lblManageAddons As New Label With {.Text = "Manage Addons", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 70), .AutoSize = True}

        ' Save Changes Button
        Dim btnSaveChanges As New Button With {.Text = "Save Changes", .Location = New Point(20, 310), .Size = New Size(100, 25), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnSaveChanges.FlatAppearance.BorderSize = 0

        Dim btnStockManagement As New Button With {.Text = "Stock Management", .Location = New Point(130, 310), .Size = New Size(120, 25), .BackColor = Color.FromArgb(155, 89, 182), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnStockManagement.FlatAppearance.BorderSize = 0

        LoadAddonsDataEnhanced(dgvAddons, picPreview)

        ' DataGridView Selection Changed Event - Update preview image
        AddHandler dgvAddons.SelectionChanged, Sub()
                                                   If dgvAddons.SelectedRows.Count > 0 Then
                                                       LoadAddonImagePreview(dgvAddons.SelectedRows(0), picPreview)
                                                   End If
                                               End Sub

        ' Search event handlers
        AddHandler btnSearch.Click, Sub()
                                        LoadAddonsDataEnhanced(dgvAddons, picPreview, txtSearch.Text.Trim())
                                    End Sub

        AddHandler btnShowAll.Click, Sub()
                                         txtSearch.Clear()
                                         LoadAddonsDataEnhanced(dgvAddons, picPreview)
                                     End Sub

        ' Search on Enter key
        AddHandler txtSearch.KeyDown, Sub(sender, e)
                                          If e.KeyCode = Keys.Enter Then
                                              LoadAddonsDataEnhanced(dgvAddons, picPreview, txtSearch.Text.Trim())
                                          End If
                                      End Sub

        ' Save Changes Event Handler - Updated to handle name, category, price, and is_active changes
        AddHandler btnSaveChanges.Click, Sub()
                                             Try
                                                 If con Is Nothing Then
                                                     con = New MySqlConnection(strcon)
                                                 End If

                                                 If con.State = ConnectionState.Open Then
                                                     con.Close()
                                                 End If

                                                 con.Open()
                                                 Dim changesCount As Integer = 0

                                                 For Each row As DataGridViewRow In dgvAddons.Rows
                                                     If Not row.IsNewRow Then
                                                         Dim addonID As Integer = Convert.ToInt32(row.Cells("addon_id").Value)
                                                         Dim newItemName As String = row.Cells("item_name").Value.ToString().Trim()
                                                         Dim newCategory As String = row.Cells("category").Value.ToString()
                                                         Dim newPrice As Decimal = Convert.ToDecimal(row.Cells("price").Value)
                                                         Dim isActive As Boolean = Convert.ToBoolean(row.Cells("is_active").Value)

                                                         ' Validate that required fields are not empty
                                                         If String.IsNullOrEmpty(newItemName) Then
                                                             MessageBox.Show($"Item name cannot be empty for ID {addonID}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                             con.Close()
                                                             Return
                                                         End If

                                                         ' Check if category is valid
                                                         If Not {"Hardware", "Service", "Plan Upgrade"}.Contains(newCategory) Then
                                                             MessageBox.Show($"Invalid category '{newCategory}' for ID {addonID}! Valid categories are: Hardware, Service, Plan Upgrade", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                             con.Close()
                                                             Return
                                                         End If

                                                         ' Get the old category to check if we need to manage stock records
                                                         Dim oldCategoryCmd As New MySqlCommand("SELECT category FROM addons WHERE addon_id = @id", con)
                                                         oldCategoryCmd.Parameters.AddWithValue("@id", addonID)
                                                         Dim oldCategory As String = oldCategoryCmd.ExecuteScalar()?.ToString()

                                                         ' Update the addon - now includes is_active
                                                         Dim cmd As New MySqlCommand("UPDATE addons SET item_name = @itemname, category = @category, price = @price, is_active = @is_active WHERE addon_id = @id", con)
                                                         cmd.Parameters.AddWithValue("@itemname", newItemName)
                                                         cmd.Parameters.AddWithValue("@category", newCategory)
                                                         cmd.Parameters.AddWithValue("@price", newPrice)
                                                         cmd.Parameters.AddWithValue("@is_active", isActive)
                                                         cmd.Parameters.AddWithValue("@id", addonID)
                                                         changesCount += cmd.ExecuteNonQuery()

                                                         ' Handle stock record changes when category changes
                                                         If oldCategory <> newCategory Then
                                                             If oldCategory = "Hardware" AndAlso newCategory <> "Hardware" Then
                                                                 ' Remove from stock table if changing from Hardware to non-Hardware
                                                                 Dim deleteStockCmd As New MySqlCommand("DELETE FROM hardware_stocks WHERE addon_id = @id", con)
                                                                 deleteStockCmd.Parameters.AddWithValue("@id", addonID)
                                                                 deleteStockCmd.ExecuteNonQuery()
                                                             ElseIf oldCategory <> "Hardware" AndAlso newCategory = "Hardware" Then
                                                                 ' Add to stock table if changing from non-Hardware to Hardware
                                                                 Dim insertStockCmd As New MySqlCommand("INSERT INTO hardware_stocks (addon_id, quantity_available) VALUES (@id, 0)", con)
                                                                 insertStockCmd.Parameters.AddWithValue("@id", addonID)
                                                                 insertStockCmd.ExecuteNonQuery()
                                                             End If
                                                         End If
                                                     End If
                                                 Next
                                                 con.Close()

                                                 MessageBox.Show($"Successfully updated {changesCount} add-on(s)!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 LoadAddonsDataEnhanced(dgvAddons, picPreview, txtSearch.Text.Trim()) ' Refresh with current search
                                             Catch ex As Exception
                                                 If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        ' Add all Page 1 controls to pnlPage1
        pnlPage1.Controls.AddRange({lblAddAddon, lblItemName, txtItemName, lblCategory, cmbCategory, lblPrice, txtPrice, btnAddAddon, lblImagePreview, picPreview, lblSearch, txtSearch, btnSearch, btnShowAll, lblManageAddons, btnSaveChanges, btnStockManagement, dgvAddons})

        ' ===== PAGE 2 - STOCK MANAGEMENT =====
        ' Stock Management Title
        Dim lblStockManagement As New Label With {.Text = "Hardware Stock Management", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 0), .AutoSize = True}

        ' Add Stock Section
        Dim lblAddStock As New Label With {.Text = "Adjust Stock", .Font = New Font("Segoe UI", 10, FontStyle.Bold), .Location = New Point(5, 19), .AutoSize = True}

        ' Hardware dropdown
        Dim cmbHardware As New ComboBox With {.Location = New Point(20, 60), .Size = New Size(100, 23), .DropDownStyle = ComboBoxStyle.DropDownList}
        Dim lblHardware As New Label With {.Text = "Select Hardware:", .Location = New Point(20, 40), .AutoSize = True}

        ' Quantity input
        Dim txtQuantity As New TextBox With {.Location = New Point(125, 60), .Size = New Size(80, 23)}
        Dim lblQuantity As New Label With {.Text = "Quantity:", .Location = New Point(125, 40), .AutoSize = True}

        ' Add/Remove buttons
        Dim btnAddStock As New Button With {.Text = "Add Stock", .Location = New Point(210, 58), .Size = New Size(80, 25), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnAddStock.FlatAppearance.BorderSize = 0

        Dim btnRemoveStock As New Button With {.Text = "Remove Stock", .Location = New Point(295, 58), .Size = New Size(90, 25), .BackColor = Color.FromArgb(231, 76, 60), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnRemoveStock.FlatAppearance.BorderSize = 0

        ' Stock DataGridView
        Dim dgvStocks As New DataGridView With {
        .Location = New Point(20, 100),
        .Size = New Size(400, 200),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    }

        ' Back Button
        Dim btnBack As New Button With {.Text = "Back to Addons", .Location = New Point(20, 310), .Size = New Size(100, 25), .BackColor = Color.FromArgb(52, 152, 219), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnBack.FlatAppearance.BorderSize = 0

        ' Refresh Button
        Dim btnRefreshStocks As New Button With {.Text = "Refresh", .Location = New Point(130, 310), .Size = New Size(70, 25), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnRefreshStocks.FlatAppearance.BorderSize = 0

        ' Load hardware items and stock data
        LoadHardwareItems(cmbHardware)
        LoadStockData(dgvStocks)

        ' Stock Management Event Handlers
        AddHandler btnAddStock.Click, Sub()
                                          If cmbHardware.SelectedValue Is Nothing OrElse String.IsNullOrEmpty(txtQuantity.Text) Then
                                              MessageBox.Show("Please select hardware and enter quantity!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              Return
                                          End If

                                          Dim quantity As Integer
                                          If Not Integer.TryParse(txtQuantity.Text, quantity) OrElse quantity <= 0 Then
                                              MessageBox.Show("Please enter a valid positive quantity!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              Return
                                          End If

                                          Try
                                              con.Open()
                                              Dim cmd As New MySqlCommand("UPDATE hardware_stocks SET quantity_available = quantity_available + @quantity WHERE addon_id = @addonid", con)
                                              cmd.Parameters.AddWithValue("@quantity", quantity)
                                              cmd.Parameters.AddWithValue("@addonid", cmbHardware.SelectedValue)

                                              Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                                              con.Close()

                                              If rowsAffected > 0 Then
                                                  MessageBox.Show($"Successfully added {quantity} units to stock!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                  txtQuantity.Clear()
                                                  cmbHardware.SelectedIndex = -1
                                                  LoadStockData(dgvStocks)
                                              Else
                                                  MessageBox.Show("No stock record found for this hardware!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              End If

                                          Catch ex As Exception
                                              If con.State = ConnectionState.Open Then con.Close()
                                              MessageBox.Show("Error adding stock: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                          End Try
                                      End Sub

        AddHandler btnRemoveStock.Click, Sub()
                                             If cmbHardware.SelectedValue Is Nothing OrElse String.IsNullOrEmpty(txtQuantity.Text) Then
                                                 MessageBox.Show("Please select hardware and enter quantity!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                 Return
                                             End If

                                             Dim quantity As Integer
                                             If Not Integer.TryParse(txtQuantity.Text, quantity) OrElse quantity <= 0 Then
                                                 MessageBox.Show("Please enter a valid positive quantity!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                 Return
                                             End If

                                             Try
                                                 con.Open()
                                                 ' Check current stock first
                                                 Dim checkCmd As New MySqlCommand("SELECT quantity_available FROM hardware_stocks WHERE addon_id = @addonid", con)
                                                 checkCmd.Parameters.AddWithValue("@addonid", cmbHardware.SelectedValue)
                                                 Dim currentStock As Object = checkCmd.ExecuteScalar()

                                                 If currentStock Is Nothing Then
                                                     con.Close()
                                                     MessageBox.Show("No stock record found for this hardware!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                     Return
                                                 End If

                                                 Dim availableStock As Integer = Convert.ToInt32(currentStock)
                                                 If quantity > availableStock Then
                                                     con.Close()
                                                     MessageBox.Show($"Cannot remove {quantity} units. Only {availableStock} units available in stock!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                     Return
                                                 End If

                                                 Dim cmd As New MySqlCommand("UPDATE hardware_stocks SET quantity_available = quantity_available - @quantity WHERE addon_id = @addonid", con)
                                                 cmd.Parameters.AddWithValue("@quantity", quantity)
                                                 cmd.Parameters.AddWithValue("@addonid", cmbHardware.SelectedValue)

                                                 cmd.ExecuteNonQuery()
                                                 con.Close()

                                                 MessageBox.Show($"Successfully removed {quantity} units from stock!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 txtQuantity.Clear()
                                                 cmbHardware.SelectedIndex = -1
                                                 LoadStockData(dgvStocks)

                                             Catch ex As Exception
                                                 If con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error removing stock: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        AddHandler btnRefreshStocks.Click, Sub()
                                               LoadStockData(dgvStocks)
                                               LoadHardwareItems(cmbHardware)
                                           End Sub

        ' Add all Page 2 controls to pnlPage2
        pnlPage2.Controls.AddRange({lblStockManagement, lblAddStock, lblHardware, cmbHardware, lblQuantity, txtQuantity, btnAddStock, btnRemoveStock, dgvStocks, btnBack, btnRefreshStocks})

        ' Page Navigation Event Handlers
        AddHandler btnStockManagement.Click, Sub()
                                                 InitializeMissingStockRecords()
                                                 pnlPage1.Visible = False
                                                 pnlPage2.Visible = True
                                                 LoadStockData(dgvStocks)
                                                 LoadHardwareItems(cmbHardware)
                                             End Sub

        AddHandler btnBack.Click, Sub()
                                      pnlPage2.Visible = False
                                      pnlPage1.Visible = True
                                  End Sub

        ' Add both panels to main content panel
        pnlContent.Controls.AddRange({pnlPage1, pnlPage2})
    End Sub

    Private Sub InitializeMissingStockRecords()
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' This single query will insert stock records for all hardware items that don't have them
            Dim cmd As New MySqlCommand("INSERT INTO hardware_stocks (addon_id, quantity_available) SELECT addon_id, 0 FROM addons WHERE category = 'Hardware' AND addon_id NOT IN (SELECT addon_id FROM hardware_stocks)", con)
            cmd.ExecuteNonQuery()

            con.Close()

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            ' Handle error silently or log it
        End Try
    End Sub

    Private Sub LoadAddonsDataEnhanced(dgv As DataGridView, picPreview As PictureBox, Optional searchTerm As String = "")
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build query with optional search - Updated to include is_active and blob_id
            Dim query As String = "SELECT addon_id, item_name, category, price, is_active, blob_id FROM addons"

            If Not String.IsNullOrEmpty(searchTerm) Then
                query += " WHERE item_name LIKE @search OR category LIKE @search"
            End If

            query += " ORDER BY addon_id"

            Dim cmd As New MySqlCommand(query, con)
            If Not String.IsNullOrEmpty(searchTerm) Then
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
            End If

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Check if we have data
            If dt.Rows.Count = 0 Then
                dgv.DataSource = Nothing
                If String.IsNullOrEmpty(searchTerm) Then
                    MessageBox.Show("No add-ons found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"No add-ons found matching '{searchTerm}'.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return
            End If

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Hide blob_id column
            If dgv.Columns.Contains("blob_id") Then
                dgv.Columns("blob_id").Visible = False
            End If

            ' Configure columns
            If dgv.Columns.Contains("addon_id") Then
                dgv.Columns("addon_id").ReadOnly = True
                dgv.Columns("addon_id").HeaderText = "ID"
                dgv.Columns("addon_id").Width = 50
            End If

            ' Make item name editable
            If dgv.Columns.Contains("item_name") Then
                dgv.Columns("item_name").ReadOnly = False
                dgv.Columns("item_name").HeaderText = "Item Name"
                dgv.Columns("item_name").Width = 120
            End If

            ' Make category editable with dropdown
            If dgv.Columns.Contains("category") Then
                dgv.Columns("category").ReadOnly = False
                dgv.Columns("category").HeaderText = "Category"
                dgv.Columns("category").Width = 100

                ' Create ComboBox column for category
                Dim categoryComboColumn As New DataGridViewComboBoxColumn()
                categoryComboColumn.Name = "category"
                categoryComboColumn.HeaderText = "Category"
                categoryComboColumn.DataPropertyName = "category"
                categoryComboColumn.Items.AddRange({"Hardware", "Service", "Plan Upgrade"})
                categoryComboColumn.Width = 100
                categoryComboColumn.FlatStyle = FlatStyle.Flat

                ' Remove the original category column and add the ComboBox column
                dgv.Columns.Remove("category")
                dgv.Columns.Insert(2, categoryComboColumn)
            End If

            ' Make price column editable for modifications
            If dgv.Columns.Contains("price") Then
                dgv.Columns("price").ReadOnly = False
                dgv.Columns("price").HeaderText = "Price"
                dgv.Columns("price").Width = 80
            End If

            ' Configure is_active column as checkbox
            If dgv.Columns.Contains("is_active") Then
                dgv.Columns("is_active").ReadOnly = False
                dgv.Columns("is_active").HeaderText = "Active"
                dgv.Columns("is_active").Width = 60
                ' Convert to checkbox column
                Dim checkBoxColumn As New DataGridViewCheckBoxColumn()
                checkBoxColumn.DataPropertyName = "is_active"
                checkBoxColumn.HeaderText = "Active"
                checkBoxColumn.Name = "is_active"
                checkBoxColumn.Width = 60
                checkBoxColumn.ReadOnly = False

                ' Remove the original column and add checkbox column
                Dim columnIndex As Integer = dgv.Columns("is_active").Index
                dgv.Columns.RemoveAt(columnIndex)
                dgv.Columns.Insert(columnIndex, checkBoxColumn)
            End If

            ' Add Browse Image Button Column
            Dim btnColumn As New DataGridViewButtonColumn()
            btnColumn.HeaderText = "Browse Image"
            btnColumn.Text = "Browse"
            btnColumn.UseColumnTextForButtonValue = True
            btnColumn.Width = 100
            btnColumn.Name = "BrowseButton"

            dgv.Columns.Add(btnColumn)

            If dgv.IsHandleCreated Then
                dgv.BeginInvoke(Sub()
                                    dgv.Columns("BrowseButton").DisplayIndex = dgv.Columns.Count - 1
                                End Sub)
            Else
                ' Wait until handle is created before trying BeginInvoke
                AddHandler dgv.HandleCreated, Sub(s, eArgs)
                                                  dgv.BeginInvoke(Sub()
                                                                      dgv.Columns("BrowseButton").DisplayIndex = dgv.Columns.Count - 1
                                                                  End Sub)
                                              End Sub
            End If

            ' Add event handler for button clicks
            RemoveHandler dgv.CellClick, AddressOf HandleAddonBrowseButtonClick
            AddHandler dgv.CellClick, AddressOf HandleAddonBrowseButtonClick

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading add-ons: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HandleAddonBrowseButtonClick(sender As Object, e As DataGridViewCellEventArgs)
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)

        ' Check if it's the browse button column
        If e.ColumnIndex >= 0 AndAlso dgv.Columns(e.ColumnIndex).Name = "BrowseButton" AndAlso e.RowIndex >= 0 Then
            Dim addonId As Integer = Convert.ToInt32(dgv.Rows(e.RowIndex).Cells("addon_id").Value)

            Using ofd As New OpenFileDialog()
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;"
                ofd.Title = "Select Image for Addon"

                If ofd.ShowDialog() = DialogResult.OK Then
                    Try
                        UpdateAddonImage(addonId, ofd.FileName)
                        MessageBox.Show("Image updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If dgv.SelectedRows.Count > 0 AndAlso dgv.SelectedRows(0).Index = e.RowIndex Then
                            Dim picPreview As PictureBox = Nothing
                            For Each ctrl As Control In pnlContent.Controls
                                If TypeOf ctrl Is Panel Then
                                    For Each subCtrl As Control In DirectCast(ctrl, Panel).Controls
                                        If TypeOf subCtrl Is PictureBox AndAlso subCtrl.Name = "picPreview" Then
                                            picPreview = DirectCast(subCtrl, PictureBox)
                                            Exit For
                                        End If
                                    Next
                                    If picPreview IsNot Nothing Then Exit For
                                End If
                            Next

                            If picPreview IsNot Nothing Then
                                LoadAddonImagePreview(dgv.SelectedRows(0), picPreview)
                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error updating image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            End Using
        End If
    End Sub

    Private Sub UpdateAddonImage(addonId As Integer, imagePath As String)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Get current blob_id for this addon
            Dim currentBlobId As Object = Nothing
            Using cmd As New MySqlCommand("SELECT blob_id FROM addons WHERE addon_id = @addonId", con)
                cmd.Parameters.AddWithValue("@addonId", addonId)
                currentBlobId = cmd.ExecuteScalar()
            End Using

            ' Read new image file
            Dim fileBytes As Byte() = File.ReadAllBytes(imagePath)
            Dim fileName As String = Path.GetFileName(imagePath)
            Dim mimeType As String = "image/" & Path.GetExtension(fileName).TrimStart(".").ToLower()

            Dim newBlobId As Integer

            If currentBlobId IsNot Nothing AndAlso Not IsDBNull(currentBlobId) Then
                ' Update existing blob
                Using cmd As New MySqlCommand("UPDATE blobs SET file_name = @fileName, mime_type = @mimeType, data = @data WHERE blob_id = @blobId", con)
                    cmd.Parameters.AddWithValue("@fileName", fileName)
                    cmd.Parameters.AddWithValue("@mimeType", mimeType)
                    cmd.Parameters.AddWithValue("@data", fileBytes)
                    cmd.Parameters.AddWithValue("@blobId", currentBlobId)
                    cmd.ExecuteNonQuery()
                End Using
            Else
                ' Create new blob
                Using cmd As New MySqlCommand("INSERT INTO blobs (file_name, mime_type, data) VALUES (@fileName, @mimeType, @data); SELECT LAST_INSERT_ID();", con)
                    cmd.Parameters.AddWithValue("@fileName", fileName)
                    cmd.Parameters.AddWithValue("@mimeType", mimeType)
                    cmd.Parameters.AddWithValue("@data", fileBytes)
                    newBlobId = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                ' Update addon with new blob_id
                Using cmd As New MySqlCommand("UPDATE addons SET blob_id = @blobId WHERE addon_id = @addonId", con)
                    cmd.Parameters.AddWithValue("@blobId", newBlobId)
                    cmd.Parameters.AddWithValue("@addonId", addonId)
                    cmd.ExecuteNonQuery()
                End Using
            End If

            con.Close()

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            Throw ex
        End Try
    End Sub

    Private Sub LoadAddonImagePreview(row As DataGridViewRow, picPreview As PictureBox)
        Try
            Dim blobId As Object = row.Cells("blob_id").Value

            If blobId IsNot Nothing AndAlso Not IsDBNull(blobId) Then
                If con Is Nothing Then
                    con = New MySqlConnection(strcon)
                End If

                If con.State = ConnectionState.Open Then
                    con.Close()
                End If

                con.Open()
                Using cmd As New MySqlCommand("SELECT data FROM blobs WHERE blob_id = @blobId", con)
                    cmd.Parameters.AddWithValue("@blobId", blobId)
                    Dim imageData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

                    If imageData IsNot Nothing AndAlso imageData.Length > 0 Then
                        Using ms As New MemoryStream(imageData)
                            picPreview.Image = Image.FromStream(ms)
                        End Using
                    Else
                        picPreview.Image = Nothing
                    End If
                End Using
                con.Close()
            Else
                picPreview.Image = Nothing
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            picPreview.Image = Nothing
        End Try
    End Sub

    Private Sub ShowBillingPanel()
        pnlContent.Controls.Clear()

        ' Main title
        Dim lblBilling As New Label With {
        .Text = "Billing Management",
        .Font = New Font("Segoe UI", 14, FontStyle.Bold),
        .Location = New Point(5, 5),
        .AutoSize = True
    }

        ' Tab buttons for switching between views
        Dim btnBillingRecords As New Button With {
        .Text = "Billing Records",
        .Location = New Point(20, 35),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(52, 152, 219),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "billing_records"
    }
        btnBillingRecords.FlatAppearance.BorderSize = 0

        Dim btnCustomerAddons As New Button With {
        .Text = "Customer Addons",
        .Location = New Point(150, 35),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "customer_addons"
    }
        btnCustomerAddons.FlatAppearance.BorderSize = 0

        Dim btnAllTransactions As New Button With {
        .Text = "All Transactions",
        .Location = New Point(280, 35),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "all_transactions"
    }
        btnAllTransactions.FlatAppearance.BorderSize = 0

        ' Search section
        Dim lblSearch As New Label With {
        .Text = "Search:",
        .Location = New Point(20, 75),
        .AutoSize = True
    }

        Dim txtSearch As New TextBox With {
        .Location = New Point(80, 73),
        .Size = New Size(150, 23)
    }

        Dim btnSearch As New Button With {
        .Text = "Search",
        .Location = New Point(240, 72),
        .Size = New Size(60, 25),
        .BackColor = Color.FromArgb(46, 204, 113),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnSearch.FlatAppearance.BorderSize = 0

        Dim btnShowAll As New Button With {
        .Text = "Show All",
        .Location = New Point(310, 72),
        .Size = New Size(70, 25),
        .BackColor = Color.FromArgb(46, 204, 113),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnShowAll.FlatAppearance.BorderSize = 0

        ' Update billing status section (only for billing records)
        Dim pnlUpdateSection As New Panel With {
        .Location = New Point(20, 105),
        .Size = New Size(400, 35),
        .Visible = True
    }

        Dim lblUpdateStatus As New Label With {
        .Text = "Update Status:",
        .Location = New Point(0, 8),
        .AutoSize = True
    }

        Dim cmbStatusFilter As New ComboBox With {
        .Location = New Point(100, 5),
        .Size = New Size(100, 23),
        .DropDownStyle = ComboBoxStyle.DropDownList
    }
        cmbStatusFilter.Items.AddRange({"Unpaid", "Paid", "Overdue"})

        Dim btnUpdateStatus As New Button With {
        .Text = "Update Selected",
        .Location = New Point(210, 4),
        .Size = New Size(100, 25),
        .BackColor = Color.FromArgb(230, 126, 34),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnUpdateStatus.FlatAppearance.BorderSize = 0

        pnlUpdateSection.Controls.AddRange({lblUpdateStatus, cmbStatusFilter, btnUpdateStatus})

        ' DataGridView for displaying data
        Dim dgvBilling As New DataGridView With {
        .Location = New Point(20, 150),
        .Size = New Size(400, 200),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        .MultiSelect = True
    }

        ' Variables to track current view
        Dim currentView As String = "billing_records"
        Dim currentSearchTerm As String = ""

        ' Load initial data
        LoadBillingRecordsEnhanced(dgvBilling)

        ' Tab button click handlers
        AddHandler btnBillingRecords.Click, Sub()
                                                ' Update button colors
                                                btnBillingRecords.BackColor = Color.FromArgb(52, 152, 219)
                                                btnCustomerAddons.BackColor = Color.FromArgb(149, 165, 166)
                                                btnAllTransactions.BackColor = Color.FromArgb(149, 165, 166)

                                                currentView = "billing_records"
                                                pnlUpdateSection.Visible = True
                                                LoadBillingRecordsEnhanced(dgvBilling, currentSearchTerm)
                                            End Sub

        AddHandler btnCustomerAddons.Click, Sub()
                                                ' Update button colors
                                                btnBillingRecords.BackColor = Color.FromArgb(149, 165, 166)
                                                btnCustomerAddons.BackColor = Color.FromArgb(52, 152, 219)
                                                btnAllTransactions.BackColor = Color.FromArgb(149, 165, 166)

                                                currentView = "customer_addons"
                                                pnlUpdateSection.Visible = False
                                                LoadCustomerAddonsEnhanced(dgvBilling, currentSearchTerm)
                                            End Sub

        AddHandler btnAllTransactions.Click, Sub()
                                                 ' Update button colors
                                                 btnBillingRecords.BackColor = Color.FromArgb(149, 165, 166)
                                                 btnCustomerAddons.BackColor = Color.FromArgb(149, 165, 166)
                                                 btnAllTransactions.BackColor = Color.FromArgb(52, 152, 219)

                                                 currentView = "all_transactions"
                                                 pnlUpdateSection.Visible = False
                                                 LoadAllTransactionsEnhanced(dgvBilling, currentSearchTerm)
                                             End Sub

        ' Search handlers
        AddHandler btnSearch.Click, Sub()
                                        currentSearchTerm = txtSearch.Text.Trim()
                                        Select Case currentView
                                            Case "billing_records"
                                                LoadBillingRecordsEnhanced(dgvBilling, currentSearchTerm)
                                            Case "customer_addons"
                                                LoadCustomerAddonsEnhanced(dgvBilling, currentSearchTerm)
                                            Case "all_transactions"
                                                LoadAllTransactionsEnhanced(dgvBilling, currentSearchTerm)
                                        End Select
                                    End Sub

        AddHandler btnShowAll.Click, Sub()
                                         txtSearch.Clear()
                                         currentSearchTerm = ""
                                         Select Case currentView
                                             Case "billing_records"
                                                 LoadBillingRecordsEnhanced(dgvBilling)
                                             Case "customer_addons"
                                                 LoadCustomerAddonsEnhanced(dgvBilling)
                                             Case "all_transactions"
                                                 LoadAllTransactionsEnhanced(dgvBilling)
                                         End Select
                                     End Sub

        ' Search on Enter key
        AddHandler txtSearch.KeyDown, Sub(sender, e)
                                          If e.KeyCode = Keys.Enter Then
                                              currentSearchTerm = txtSearch.Text.Trim()
                                              Select Case currentView
                                                  Case "billing_records"
                                                      LoadBillingRecordsEnhanced(dgvBilling, currentSearchTerm)
                                                  Case "customer_addons"
                                                      LoadCustomerAddonsEnhanced(dgvBilling, currentSearchTerm)
                                                  Case "all_transactions"
                                                      LoadAllTransactionsEnhanced(dgvBilling, currentSearchTerm)
                                              End Select
                                          End If
                                      End Sub

        ' Update status handler (only for billing records)
        AddHandler btnUpdateStatus.Click, Sub()
                                              If currentView <> "billing_records" Then Return
                                              If cmbStatusFilter.SelectedItem Is Nothing Then
                                                  MessageBox.Show("Please select a status to update!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                  Return
                                              End If

                                              If dgvBilling.SelectedRows.Count = 0 Then
                                                  MessageBox.Show("Please select at least one billing record to update!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                  Return
                                              End If

                                              Try
                                                  If con Is Nothing Then
                                                      con = New MySqlConnection(strcon)
                                                  End If

                                                  If con.State = ConnectionState.Open Then
                                                      con.Close()
                                                  End If

                                                  con.Open()
                                                  Dim updatedCount As Integer = 0
                                                  Dim newStatus As String = cmbStatusFilter.SelectedItem.ToString()

                                                  For Each row As DataGridViewRow In dgvBilling.SelectedRows
                                                      If Not row.IsNewRow Then
                                                          Dim billingID As Integer = Convert.ToInt32(row.Cells("billing_id").Value)

                                                          Dim cmd As New MySqlCommand("UPDATE billing_records SET status = @status WHERE billing_id = @id", con)
                                                          cmd.Parameters.AddWithValue("@status", newStatus)
                                                          cmd.Parameters.AddWithValue("@id", billingID)
                                                          updatedCount += cmd.ExecuteNonQuery()
                                                      End If
                                                  Next

                                                  con.Close()
                                                  MessageBox.Show($"Successfully updated {updatedCount} billing record(s) to {newStatus}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                  LoadBillingRecordsEnhanced(dgvBilling, currentSearchTerm)

                                              Catch ex As Exception
                                                  If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                  MessageBox.Show("Error updating billing records: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              End Try
                                          End Sub

        pnlContent.Controls.AddRange({lblBilling, btnBillingRecords, btnCustomerAddons, btnAllTransactions, lblSearch, txtSearch, btnSearch, btnShowAll, pnlUpdateSection, dgvBilling})
    End Sub

    Private Sub ShowTicketsPanel()
        pnlContent.Controls.Clear()

        ' Main title
        Dim lblTickets As New Label With {
        .Text = "Support Tickets Management",
        .Font = New Font("Segoe UI", 14, FontStyle.Bold),
        .Location = New Point(5, 5),
        .AutoSize = True
    }

        ' Search section
        Dim lblSearch As New Label With {
        .Text = "Search:",
        .Location = New Point(20, 35),
        .AutoSize = True
    }

        Dim txtSearch As New TextBox With {
        .Location = New Point(80, 33),
        .Size = New Size(150, 23)
    }

        Dim btnSearch As New Button With {
        .Text = "Search",
        .Location = New Point(240, 32),
        .Size = New Size(60, 25),
        .BackColor = Color.FromArgb(52, 152, 219),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnSearch.FlatAppearance.BorderSize = 0

        Dim btnShowAll As New Button With {
        .Text = "Show All",
        .Location = New Point(310, 32),
        .Size = New Size(70, 25),
        .BackColor = Color.FromArgb(46, 204, 113),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnShowAll.FlatAppearance.BorderSize = 0

        ' Status filter section
        Dim lblStatusFilter As New Label With {
        .Text = "Filter by Status:",
        .Location = New Point(20, 65),
        .AutoSize = True
    }

        Dim cmbStatusFilter As New ComboBox With {
        .Location = New Point(120, 63),
        .Size = New Size(100, 23),
        .DropDownStyle = ComboBoxStyle.DropDownList
    }
        cmbStatusFilter.Items.AddRange({"All", "Open", "In Progress", "Resolved", "Closed"})
        cmbStatusFilter.SelectedIndex = 0 ' Default to "All"

        Dim btnFilter As New Button With {
        .Text = "Filter",
        .Location = New Point(230, 62),
        .Size = New Size(60, 25),
        .BackColor = Color.FromArgb(155, 89, 182),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnFilter.FlatAppearance.BorderSize = 0

        ' Update status section
        Dim lblUpdateStatus As New Label With {
        .Text = "Update Status:",
        .Location = New Point(20, 95),
        .AutoSize = True
    }

        Dim cmbNewStatus As New ComboBox With {
        .Location = New Point(120, 93),
        .Size = New Size(100, 23),
        .DropDownStyle = ComboBoxStyle.DropDownList
    }
        cmbNewStatus.Items.AddRange({"Open", "In Progress", "Resolved", "Closed"})

        Dim btnUpdateStatus As New Button With {
        .Text = "Update Selected",
        .Location = New Point(230, 92),
        .Size = New Size(100, 25),
        .BackColor = Color.FromArgb(230, 126, 34),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnUpdateStatus.FlatAppearance.BorderSize = 0

        ' DataGridView for tickets
        Dim dgvTickets As New DataGridView With {
        .Location = New Point(20, 125),
        .Size = New Size(400, 200),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        .MultiSelect = True
    }

        ' Variables to track current search and filter
        Dim currentSearchTerm As String = ""
        Dim currentStatusFilter As String = "All"

        ' Load initial data
        LoadTicketsDataEnhanced(dgvTickets)

        ' Search event handlers
        AddHandler btnSearch.Click, Sub()
                                        currentSearchTerm = txtSearch.Text.Trim()
                                        LoadTicketsDataEnhanced(dgvTickets, currentSearchTerm, currentStatusFilter)
                                    End Sub

        AddHandler btnShowAll.Click, Sub()
                                         txtSearch.Clear()
                                         currentSearchTerm = ""
                                         LoadTicketsDataEnhanced(dgvTickets, "", currentStatusFilter)
                                     End Sub

        ' Filter event handler
        AddHandler btnFilter.Click, Sub()
                                        currentStatusFilter = cmbStatusFilter.SelectedItem.ToString()
                                        LoadTicketsDataEnhanced(dgvTickets, currentSearchTerm, currentStatusFilter)
                                    End Sub

        ' Search on Enter key
        AddHandler txtSearch.KeyDown, Sub(sender, e)
                                          If e.KeyCode = Keys.Enter Then
                                              currentSearchTerm = txtSearch.Text.Trim()
                                              LoadTicketsDataEnhanced(dgvTickets, currentSearchTerm, currentStatusFilter)
                                          End If
                                      End Sub

        ' Update status event handler
        AddHandler btnUpdateStatus.Click, Sub()
                                              If cmbNewStatus.SelectedItem Is Nothing Then
                                                  MessageBox.Show("Please select a status to update!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                  Return
                                              End If

                                              If dgvTickets.SelectedRows.Count = 0 Then
                                                  MessageBox.Show("Please select at least one ticket to update!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                  Return
                                              End If

                                              Try
                                                  If con Is Nothing Then
                                                      con = New MySqlConnection(strcon)
                                                  End If

                                                  If con.State = ConnectionState.Open Then
                                                      con.Close()
                                                  End If

                                                  con.Open()
                                                  Dim updatedCount As Integer = 0
                                                  Dim newStatus As String = cmbNewStatus.SelectedItem.ToString()

                                                  For Each row As DataGridViewRow In dgvTickets.SelectedRows
                                                      If Not row.IsNewRow Then
                                                          Dim ticketID As Integer = Convert.ToInt32(row.Cells("ticket_id").Value)

                                                          Dim cmd As New MySqlCommand("UPDATE support_tickets SET status = @status WHERE ticket_id = @id", con)
                                                          cmd.Parameters.AddWithValue("@status", newStatus)
                                                          cmd.Parameters.AddWithValue("@id", ticketID)
                                                          updatedCount += cmd.ExecuteNonQuery()
                                                      End If
                                                  Next

                                                  con.Close()
                                                  MessageBox.Show($"Successfully updated {updatedCount} ticket(s) to {newStatus}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                                  ' Clear selection and refresh
                                                  cmbNewStatus.SelectedIndex = -1
                                                  LoadTicketsDataEnhanced(dgvTickets, currentSearchTerm, currentStatusFilter)

                                              Catch ex As Exception
                                                  If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                  MessageBox.Show("Error updating tickets: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                              End Try
                                          End Sub

        pnlContent.Controls.AddRange({lblTickets, lblSearch, txtSearch, btnSearch, btnShowAll, lblStatusFilter, cmbStatusFilter, btnFilter, lblUpdateStatus, cmbNewStatus, btnUpdateStatus, dgvTickets})
    End Sub


    Private Sub ShowReportsPanel()
        pnlContent.Controls.Clear()

        ' Main title
        Dim lblReports As New Label With {
        .Text = "System Reports",
        .Font = New Font("Segoe UI", 14, FontStyle.Bold),
        .Location = New Point(5, 5),
        .AutoSize = True
    }

        ' Date range controls for Income & Expenses
        Dim lblDateFrom As New Label With {
        .Text = "From:",
        .Location = New Point(20, 15),
        .AutoSize = True,
        .Visible = False
    }

        Dim dtpDateFrom As New DateTimePicker With {
        .Location = New Point(60, 12),
        .Size = New Size(100, 25),
        .Format = DateTimePickerFormat.Short,
        .Value = DateTime.Now.AddMonths(-1),
        .Visible = False
    }

        ' Filter dropdown (positioned to the right of dtpDateFrom)
        Dim lblFilter As New Label With {
        .Text = "Filter:",
        .Location = New Point(299, 15),
        .AutoSize = True,
        .Visible = False
    }

        Dim cmbFilter As New ComboBox With {
        .Location = New Point(333, 11),
        .Size = New Size(120, 25),
        .DropDownStyle = ComboBoxStyle.DropDownList,
        .Visible = False
    }

        ' Add filter options
        cmbFilter.Items.AddRange({"All Transactions", "Income Only", "Expenses Only", "Stock Adjustments"})
        cmbFilter.SelectedIndex = 0 ' Default to "All Transactions"

        Dim lblDateTo As New Label With {
        .Text = "To:",
        .Location = New Point(170, 15),
        .AutoSize = True,
        .Visible = False
    }

        Dim dtpDateTo As New DateTimePicker With {
        .Location = New Point(195, 12),
        .Size = New Size(100, 25),
        .Format = DateTimePickerFormat.Short,
        .Value = DateTime.Now,
        .Visible = False
    }

        ' Report type buttons
        Dim btnRevenueReport As New Button With {
        .Text = "Revenue Report",
        .Location = New Point(20, 45),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(52, 152, 219),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "revenue"
    }
        btnRevenueReport.FlatAppearance.BorderSize = 0

        Dim btnIncomeExpensesReport As New Button With {
        .Text = "Income & Expenses",
        .Location = New Point(150, 45),
        .Size = New Size(130, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "incomeexpenses"
    }
        btnIncomeExpensesReport.FlatAppearance.BorderSize = 0

        Dim btnTicketsReport As New Button With {
        .Text = "Tickets Report",
        .Location = New Point(290, 45),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "tickets"
    }
        btnTicketsReport.FlatAppearance.BorderSize = 0

        Dim btnSubscribersReport As New Button With {
        .Text = "Subscribers Report",
        .Location = New Point(20, 85),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "subscribers"
    }
        btnSubscribersReport.FlatAppearance.BorderSize = 0

        Dim btnPlansReport As New Button With {
        .Text = "Plans Report",
        .Location = New Point(150, 85),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "plans"
    }
        btnPlansReport.FlatAppearance.BorderSize = 0

        ' DataGridView for reports
        Dim dgvReports As New DataGridView With {
        .Location = New Point(20, 125),
        .Size = New Size(400, 220),
        .ReadOnly = True,
        .AllowUserToAddRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    }

        ' Track current report type
        Dim currentReportType As String = "revenue"

        ' Button click handlers with color management
        Dim reportButtons() As Button = {btnRevenueReport, btnIncomeExpensesReport, btnTicketsReport, btnSubscribersReport, btnPlansReport}

        For Each btn As Button In reportButtons
            AddHandler btn.Click, Sub(sender As Button, e As EventArgs)
                                      ' Reset all button colors
                                      For Each b As Button In reportButtons
                                          b.BackColor = Color.FromArgb(149, 165, 166)
                                      Next

                                      ' Set active button color
                                      sender.BackColor = Color.FromArgb(52, 152, 219)
                                      currentReportType = sender.Tag.ToString()

                                      ' Load appropriate report
                                      Select Case currentReportType
                                          Case "revenue"
                                              LoadEnhancedRevenueReport(dgvReports)
                                              lblReports.Visible = True
                                              lblDateFrom.Visible = False
                                              lblDateTo.Visible = False
                                              dtpDateFrom.Visible = False
                                              dtpDateTo.Visible = False
                                              lblFilter.Visible = False
                                              cmbFilter.Visible = False
                                          Case "incomeexpenses"
                                              LoadIncomeExpensesReport(dgvReports, dtpDateFrom.Value, dtpDateTo.Value, cmbFilter.SelectedItem.ToString())
                                              lblReports.Visible = False
                                              lblDateFrom.Visible = True
                                              lblDateTo.Visible = True
                                              dtpDateFrom.Visible = True
                                              dtpDateTo.Visible = True
                                              lblFilter.Visible = True
                                              cmbFilter.Visible = True
                                          Case "tickets"
                                              LoadTicketsReport(dgvReports)
                                              lblReports.Visible = True
                                              lblDateFrom.Visible = False
                                              lblDateTo.Visible = False
                                              dtpDateFrom.Visible = False
                                              dtpDateTo.Visible = False
                                              lblFilter.Visible = False
                                              cmbFilter.Visible = False
                                          Case "subscribers"
                                              LoadSubscribersReport(dgvReports)
                                              lblReports.Visible = True
                                              lblDateFrom.Visible = False
                                              lblDateTo.Visible = False
                                              dtpDateFrom.Visible = False
                                              dtpDateTo.Visible = False
                                              lblFilter.Visible = False
                                              cmbFilter.Visible = False
                                          Case "plans"
                                              LoadPlansReport(dgvReports)
                                              lblReports.Visible = True
                                              lblDateFrom.Visible = False
                                              lblDateTo.Visible = False
                                              dtpDateFrom.Visible = False
                                              dtpDateTo.Visible = False
                                              lblFilter.Visible = False
                                              cmbFilter.Visible = False
                                      End Select
                                  End Sub
        Next

        ' Date picker change handlers for Income & Expenses report
        AddHandler dtpDateFrom.ValueChanged, Sub()
                                                 If currentReportType = "incomeexpenses" Then
                                                     LoadIncomeExpensesReport(dgvReports, dtpDateFrom.Value, dtpDateTo.Value, cmbFilter.SelectedItem.ToString())
                                                 End If
                                             End Sub

        AddHandler dtpDateTo.ValueChanged, Sub()
                                               If currentReportType = "incomeexpenses" Then
                                                   LoadIncomeExpensesReport(dgvReports, dtpDateFrom.Value, dtpDateTo.Value, cmbFilter.SelectedItem.ToString())
                                               End If
                                           End Sub

        ' Filter dropdown change handler for automatic update
        AddHandler cmbFilter.SelectedIndexChanged, Sub()
                                                       If currentReportType = "incomeexpenses" Then
                                                           LoadIncomeExpensesReport(dgvReports, dtpDateFrom.Value, dtpDateTo.Value, cmbFilter.SelectedItem.ToString())
                                                       End If
                                                   End Sub

        ' Load default report
        LoadEnhancedRevenueReport(dgvReports)

        pnlContent.Controls.AddRange({lblReports, lblDateFrom, dtpDateFrom, lblFilter, cmbFilter, lblDateTo, dtpDateTo, btnRevenueReport, btnIncomeExpensesReport, btnTicketsReport, btnSubscribersReport, btnPlansReport, dgvReports})
    End Sub

    Private Sub LoadIncomeExpensesReport(dgv As DataGridView, dateFrom As DateTime, dateTo As DateTime, filterType As String)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build the base query with filter conditions
            Dim whereClause As String = ""
            Select Case filterType
                Case "Income Only"
                    whereClause = " AND transaction_type IN ('Plan Payment', 'Addon Purchase')"
                Case "Expenses Only"
                    whereClause = " AND transaction_type IN ('Salary Payment', 'Hardware Expense', 'Stock Adjustment')"
                Case "Stock Adjustments"
                    whereClause = " AND transaction_type = 'Stock Adjustment'"
                Case Else ' "All Transactions"
                    whereClause = ""
            End Select

            ' Query to get income and expenses within date range with filter
            Dim query As String = "
    SELECT 
        transaction_date,
        transaction_type,
        description,
        CASE WHEN transaction_type IN ('Plan Payment', 'Addon Purchase') THEN amount ELSE 0 END as income,
        CASE WHEN transaction_type IN ('Salary Payment', 'Hardware Expense', 'Stock Adjustment') THEN ABS(amount) ELSE 0 END as expense,
        amount
    FROM (
        -- Plan payments (income from billing records)
        SELECT 
            DATE(br.created_at) as transaction_date,
            'Plan Payment' as transaction_type,
            CONCAT('Plan billing - ', u.username, ' (', p.plan_name, ')') as description,
            br.total_amount as amount
        FROM billing_records br
        JOIN subscribers s ON br.subscriber_id = s.subscriber_id
        JOIN users u ON s.customer_id = u.user_id
        JOIN internet_plans p ON s.plan_id = p.plan_id
        WHERE br.status = 'Paid' 
            AND DATE(br.created_at) BETWEEN @dateFrom AND @dateTo
        
        UNION ALL
        
        -- Addon purchases (income from customer addons)
        SELECT 
            DATE(ca.purchase_date) as transaction_date,
            'Addon Purchase' as transaction_type,
            CONCAT('Addon - ', a.item_name, ' x', ca.quantity, ' by ', u.username) as description,
            (a.price * ca.quantity) as amount
        FROM customer_addons ca
        JOIN addons a ON ca.addon_id = a.addon_id
        JOIN users u ON ca.customer_id = u.user_id
        WHERE DATE(ca.purchase_date) BETWEEN @dateFrom AND @dateTo
        
        UNION ALL
        
        -- Salary payments (expenses)
        SELECT 
            sp.payment_date as transaction_date,
            'Salary Payment' as transaction_type,
            CONCAT(sp.employee_type, ' - ', u.firstName, ' ', u.lastName, ' (', sp.payment_type, ') - ', sp.description) as description,
            sp.amount as amount
        FROM salary_payments sp
        JOIN users u ON sp.employee_id = u.user_id
        WHERE sp.payment_date BETWEEN @dateFrom AND @dateTo
        
        UNION ALL
        
        -- Hardware expenses - Stock additions (positive values)
        SELECT 
            he.expense_date as transaction_date,
            'Hardware Expense' as transaction_type,
            CONCAT('Hardware restocking - ', a.item_name, ' (Qty: +', he.quantity_added, ' @ ', FORMAT(he.estimated_cost_per_unit, 2), ' each)') as description,
            he.total_estimated_cost as amount
        FROM hardware_expenses he
        JOIN addons a ON he.addon_id = a.addon_id
        WHERE he.expense_date BETWEEN @dateFrom AND @dateTo
            AND he.quantity_added > 0
        
        UNION ALL
        
        -- Stock adjustments - Stock reductions (negative values shown as adjustments)
        SELECT 
            he.expense_date as transaction_date,
            'Stock Adjustment' as transaction_type,
            CONCAT('Stock correction - ', a.item_name, ' (Qty: ', he.quantity_added, ' @ ', FORMAT(he.estimated_cost_per_unit, 2), ' each) - ', 
                CASE 
                    WHEN he.notes LIKE '%correction%' OR he.notes LIKE '%adjustment%' THEN 'Inventory correction'
                    ELSE 'Stock reduction'
                END) as description,
            he.total_estimated_cost as amount
        FROM hardware_expenses he
        JOIN addons a ON he.addon_id = a.addon_id
        WHERE he.expense_date BETWEEN @dateFrom AND @dateTo
            AND he.quantity_added < 0
    ) as all_transactions
    WHERE 1=1" & whereClause & "
    ORDER BY transaction_date DESC"

            Dim cmd As New MySqlCommand(query, con)
            cmd.Parameters.AddWithValue("@dateFrom", dateFrom.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@dateTo", dateTo.ToString("yyyy-MM-dd"))

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Add summary row - Updated calculation to handle negative adjustments properly
            Dim totalIncome As Decimal = 0
            Dim totalExpense As Decimal = 0

            For Each row As DataRow In dt.Rows
                totalIncome += Convert.ToDecimal(row("income"))
                ' For expenses, use absolute value to ensure positive display
                totalExpense += Convert.ToDecimal(row("expense"))
            Next

            ' Add summary row with filter information
            Dim summaryRow As DataRow = dt.NewRow()
            summaryRow("transaction_date") = DBNull.Value
            summaryRow("transaction_type") = "SUMMARY"
            summaryRow("description") = $"Period: {dateFrom.ToShortDateString()} to {dateTo.ToShortDateString()} | Filter: {filterType}"
            summaryRow("income") = totalIncome
            summaryRow("expense") = totalExpense
            summaryRow("amount") = totalIncome - totalExpense
            dt.Rows.InsertAt(summaryRow, 0)

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("transaction_date") Then
                dgv.Columns("transaction_date").HeaderText = "Date"
                dgv.Columns("transaction_date").Width = 80
            End If

            If dgv.Columns.Contains("transaction_type") Then
                dgv.Columns("transaction_type").HeaderText = "Type"
                dgv.Columns("transaction_type").Width = 100
            End If

            If dgv.Columns.Contains("description") Then
                dgv.Columns("description").HeaderText = "Description"
                dgv.Columns("description").Width = 250
                dgv.Columns("description").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            If dgv.Columns.Contains("income") Then
                dgv.Columns("income").HeaderText = "Income"
                dgv.Columns("income").Width = 80
                dgv.Columns("income").DefaultCellStyle.Format = "C2"
                dgv.Columns("income").DefaultCellStyle.ForeColor = Color.Green
            End If

            If dgv.Columns.Contains("expense") Then
                dgv.Columns("expense").HeaderText = "Expense"
                dgv.Columns("expense").Width = 80
                dgv.Columns("expense").DefaultCellStyle.Format = "C2"
                dgv.Columns("expense").DefaultCellStyle.ForeColor = Color.Red
            End If

            If dgv.Columns.Contains("amount") Then
                dgv.Columns("amount").HeaderText = "Net Amount"
                dgv.Columns("amount").Width = 80
                dgv.Columns("amount").DefaultCellStyle.Format = "C2"
            End If

            ' Highlight summary row and format stock adjustment rows
            If dt.Rows.Count > 0 Then
                dgv.Rows(0).DefaultCellStyle.BackColor = Color.LightGray
                dgv.Rows(0).DefaultCellStyle.Font = New Font(dgv.Font, FontStyle.Bold)

                ' Color code different transaction types
                For i As Integer = 1 To dgv.Rows.Count - 1
                    Select Case dgv.Rows(i).Cells("transaction_type").Value?.ToString()
                        Case "Stock Adjustment"
                            dgv.Rows(i).DefaultCellStyle.BackColor = Color.LightYellow
                        Case "Plan Payment", "Addon Purchase"
                            dgv.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                        Case "Salary Payment", "Hardware Expense"
                            dgv.Rows(i).DefaultCellStyle.BackColor = Color.LightPink
                    End Select
                Next
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading income & expenses report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadPlansData(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT plan_id, plan_name, plan_type, price, speed, data_cap FROM internet_plans", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading plans: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadSubscribersData(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT s.subscriber_id, u.username, p.plan_name, s.subscription_date, s.status FROM subscribers s JOIN users u ON s.customer_id = u.user_id JOIN internet_plans p ON s.plan_id = p.plan_id", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading subscribers: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadTechniciansData(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT t.technician_id, u.username, u.firstName, u.lastName, t.skills FROM technicians t JOIN users u ON t.user_id = u.user_id", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading technicians: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadAddonsData(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT addon_id, item_name, category, price FROM addons", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading addons: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadBillingRecordsEnhanced(dgv As DataGridView, Optional searchTerm As String = "")
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build query with optional search
            Dim query As String = "SELECT b.billing_id, u.username, u.firstName, u.lastName, " &
                             "p.plan_name, b.billing_month, b.total_amount, b.due_date, " &
                             "b.status, b.created_at " &
                             "FROM billing_records b " &
                             "JOIN subscribers s ON b.subscriber_id = s.subscriber_id " &
                             "JOIN users u ON s.customer_id = u.user_id " &
                             "JOIN internet_plans p ON s.plan_id = p.plan_id"

            If Not String.IsNullOrEmpty(searchTerm) Then
                query += " WHERE u.username LIKE @search OR u.firstName LIKE @search OR u.lastName LIKE @search OR b.status LIKE @search"
            End If

            query += " ORDER BY b.created_at DESC"

            Dim cmd As New MySqlCommand(query, con)
            If Not String.IsNullOrEmpty(searchTerm) Then
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
            End If

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Check if we have data
            If dt.Rows.Count = 0 Then
                dgv.DataSource = Nothing
                If String.IsNullOrEmpty(searchTerm) Then
                    MessageBox.Show("No billing records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"No billing records found matching '{searchTerm}'.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return
            End If

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("billing_id") Then
                dgv.Columns("billing_id").HeaderText = "Billing ID"
                dgv.Columns("billing_id").Width = 70
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").HeaderText = "Username"
                dgv.Columns("username").Width = 80
            End If

            If dgv.Columns.Contains("firstName") Then
                dgv.Columns("firstName").HeaderText = "First Name"
                dgv.Columns("firstName").Width = 80
            End If

            If dgv.Columns.Contains("lastName") Then
                dgv.Columns("lastName").HeaderText = "Last Name"
                dgv.Columns("lastName").Width = 80
            End If

            If dgv.Columns.Contains("plan_name") Then
                dgv.Columns("plan_name").HeaderText = "Plan"
                dgv.Columns("plan_name").Width = 100
            End If

            If dgv.Columns.Contains("billing_month") Then
                dgv.Columns("billing_month").HeaderText = "Billing Month"
                dgv.Columns("billing_month").Width = 90
            End If

            If dgv.Columns.Contains("total_amount") Then
                dgv.Columns("total_amount").HeaderText = "Amount"
                dgv.Columns("total_amount").Width = 70
                dgv.Columns("total_amount").DefaultCellStyle.Format = "C2"
            End If

            If dgv.Columns.Contains("due_date") Then
                dgv.Columns("due_date").HeaderText = "Due Date"
                dgv.Columns("due_date").Width = 80
            End If

            If dgv.Columns.Contains("status") Then
                dgv.Columns("status").HeaderText = "Status"
                dgv.Columns("status").Width = 70
            End If

            If dgv.Columns.Contains("created_at") Then
                dgv.Columns("created_at").HeaderText = "Created"
                dgv.Columns("created_at").Width = 90
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading billing records: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadHardwareItems(cmb As ComboBox)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            Dim cmd As New MySqlCommand("SELECT addon_id, item_name FROM addons WHERE category = 'Hardware' ORDER BY item_name", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            cmb.DataSource = dt
            cmb.DisplayMember = "item_name"
            cmb.ValueMember = "addon_id"
            cmb.SelectedIndex = -1

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading hardware items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper method to load stock data
    Private Sub LoadStockData(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            Dim cmd As New MySqlCommand("SELECT hs.stock_id, a.item_name as 'Hardware Name', hs.quantity_available as 'Available Stock', a.price as 'Unit Price' FROM hardware_stocks hs JOIN addons a ON hs.addon_id = a.addon_id ORDER BY a.item_name", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("stock_id") Then
                dgv.Columns("stock_id").Visible = False
            End If

            If dgv.Columns.Contains("Hardware Name") Then
                dgv.Columns("Hardware Name").Width = 150
                dgv.Columns("Hardware Name").ReadOnly = True
            End If

            If dgv.Columns.Contains("Available Stock") Then
                dgv.Columns("Available Stock").Width = 100
                dgv.Columns("Available Stock").ReadOnly = True
            End If

            If dgv.Columns.Contains("Unit Price") Then
                dgv.Columns("Unit Price").Width = 100
                dgv.Columns("Unit Price").ReadOnly = True
                dgv.Columns("Unit Price").DefaultCellStyle.Format = "C2"
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading stock data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCustomerAddonsEnhanced(dgv As DataGridView, Optional searchTerm As String = "")
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build query with optional search
            Dim query As String = "SELECT ca.purchase_id, u.username, u.firstName, u.lastName, " &
                             "a.item_name, a.category, ca.quantity, " &
                             "(a.price * ca.quantity) as total_cost, " &
                             "ca.purchase_date, ca.expires_at, ca.is_recurring " &
                             "FROM customer_addons ca " &
                             "JOIN users u ON ca.customer_id = u.user_id " &
                             "JOIN addons a ON ca.addon_id = a.addon_id"

            If Not String.IsNullOrEmpty(searchTerm) Then
                query += " WHERE u.username LIKE @search OR u.firstName LIKE @search OR u.lastName LIKE @search OR a.item_name LIKE @search OR a.category LIKE @search"
            End If

            query += " ORDER BY ca.purchase_date DESC"

            Dim cmd As New MySqlCommand(query, con)
            If Not String.IsNullOrEmpty(searchTerm) Then
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
            End If

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Check if we have data
            If dt.Rows.Count = 0 Then
                dgv.DataSource = Nothing
                If String.IsNullOrEmpty(searchTerm) Then
                    MessageBox.Show("No customer addon purchases found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"No customer addon purchases found matching '{searchTerm}'.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return
            End If

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("purchase_id") Then
                dgv.Columns("purchase_id").HeaderText = "Purchase ID"
                dgv.Columns("purchase_id").Width = 80
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").HeaderText = "Username"
                dgv.Columns("username").Width = 80
            End If

            If dgv.Columns.Contains("firstName") Then
                dgv.Columns("firstName").HeaderText = "First Name"
                dgv.Columns("firstName").Width = 80
            End If

            If dgv.Columns.Contains("lastName") Then
                dgv.Columns("lastName").HeaderText = "Last Name"
                dgv.Columns("lastName").Width = 80
            End If

            If dgv.Columns.Contains("item_name") Then
                dgv.Columns("item_name").HeaderText = "Item"
                dgv.Columns("item_name").Width = 120
            End If

            If dgv.Columns.Contains("category") Then
                dgv.Columns("category").HeaderText = "Category"
                dgv.Columns("category").Width = 90
            End If

            If dgv.Columns.Contains("quantity") Then
                dgv.Columns("quantity").HeaderText = "Qty"
                dgv.Columns("quantity").Width = 50
            End If

            If dgv.Columns.Contains("total_cost") Then
                dgv.Columns("total_cost").HeaderText = "Total Cost"
                dgv.Columns("total_cost").Width = 80
                dgv.Columns("total_cost").DefaultCellStyle.Format = "C2"
            End If

            If dgv.Columns.Contains("purchase_date") Then
                dgv.Columns("purchase_date").HeaderText = "Purchase Date"
                dgv.Columns("purchase_date").Width = 100
            End If

            If dgv.Columns.Contains("expires_at") Then
                dgv.Columns("expires_at").HeaderText = "Expires"
                dgv.Columns("expires_at").Width = 80
            End If

            If dgv.Columns.Contains("is_recurring") Then
                dgv.Columns("is_recurring").HeaderText = "Recurring"
                dgv.Columns("is_recurring").Width = 70
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading customer addons: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadAllTransactionsEnhanced(dgv As DataGridView, Optional searchTerm As String = "")
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build union query to combine billing records and customer addons
            Dim query As String = "SELECT 'Billing' as transaction_type, " &
                             "CONCAT('BILL-', b.billing_id) as transaction_id, " &
                             "u.username, u.firstName, u.lastName, " &
                             "CONCAT('Plan: ', p.plan_name) as description, " &
                             "b.total_amount as amount, " &
                             "b.created_at as transaction_date, " &
                             "b.status " &
                             "FROM billing_records b " &
                             "JOIN subscribers s ON b.subscriber_id = s.subscriber_id " &
                             "JOIN users u ON s.customer_id = u.user_id " &
                             "JOIN internet_plans p ON s.plan_id = p.plan_id " &
                             "UNION ALL " &
                             "SELECT 'Addon Purchase' as transaction_type, " &
                             "CONCAT('ADDON-', ca.purchase_id) as transaction_id, " &
                             "u.username, u.firstName, u.lastName, " &
                             "CONCAT(a.item_name, ' (', ca.quantity, 'x)') as description, " &
                             "(a.price * ca.quantity) as amount, " &
                             "ca.purchase_date as transaction_date, " &
                             "'Completed' as status " &
                             "FROM customer_addons ca " &
                             "JOIN users u ON ca.customer_id = u.user_id " &
                             "JOIN addons a ON ca.addon_id = a.addon_id"

            If Not String.IsNullOrEmpty(searchTerm) Then
                query = "SELECT * FROM (" & query & ") as all_transactions " &
                   "WHERE username LIKE @search OR firstName LIKE @search OR lastName LIKE @search OR description LIKE @search OR status LIKE @search"
            End If

            query += " ORDER BY transaction_date DESC"

            Dim cmd As New MySqlCommand(query, con)
            If Not String.IsNullOrEmpty(searchTerm) Then
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
            End If

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Check if we have data
            If dt.Rows.Count = 0 Then
                dgv.DataSource = Nothing
                If String.IsNullOrEmpty(searchTerm) Then
                    MessageBox.Show("No transactions found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"No transactions found matching '{searchTerm}'.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return
            End If

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("transaction_type") Then
                dgv.Columns("transaction_type").HeaderText = "Type"
                dgv.Columns("transaction_type").Width = 90
            End If

            If dgv.Columns.Contains("transaction_id") Then
                dgv.Columns("transaction_id").HeaderText = "Transaction ID"
                dgv.Columns("transaction_id").Width = 100
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").HeaderText = "Username"
                dgv.Columns("username").Width = 80
            End If

            If dgv.Columns.Contains("firstName") Then
                dgv.Columns("firstName").HeaderText = "First Name"
                dgv.Columns("firstName").Width = 80
            End If

            If dgv.Columns.Contains("lastName") Then
                dgv.Columns("lastName").HeaderText = "Last Name"
                dgv.Columns("lastName").Width = 80
            End If

            If dgv.Columns.Contains("description") Then
                dgv.Columns("description").HeaderText = "Description"
                dgv.Columns("description").Width = 150
            End If

            If dgv.Columns.Contains("amount") Then
                dgv.Columns("amount").HeaderText = "Amount"
                dgv.Columns("amount").Width = 80
                dgv.Columns("amount").DefaultCellStyle.Format = "C2"
            End If

            If dgv.Columns.Contains("transaction_date") Then
                dgv.Columns("transaction_date").HeaderText = "Date"
                dgv.Columns("transaction_date").Width = 100
            End If

            If dgv.Columns.Contains("status") Then
                dgv.Columns("status").HeaderText = "Status"
                dgv.Columns("status").Width = 80
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading all transactions: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadTicketsDataEnhanced(dgv As DataGridView, Optional searchTerm As String = "", Optional statusFilter As String = "All")
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build query with optional search and status filter
            Dim query As String = "SELECT t.ticket_id, u.username, " &
                         "CONCAT(u.firstName, ' ', u.lastName) as customer_name, " &
                         "i.issue_name, t.description, t.status, " &
                         "t.created_at " &
                         "FROM support_tickets t " &
                         "JOIN users u ON t.customer_id = u.user_id " &
                         "JOIN issue_types i ON t.issue_type_id = i.issue_type_id WHERE 1=1"

            ' Add search condition
            If Not String.IsNullOrEmpty(searchTerm) Then
                query += " AND (u.username LIKE @search OR u.firstName LIKE @search OR u.lastName LIKE @search OR i.issue_name LIKE @search OR t.description LIKE @search)"
            End If

            ' Add status filter condition
            If statusFilter <> "All" Then
                query += " AND t.status = @status"
            End If

            query += " ORDER BY t.created_at DESC"

            Dim cmd As New MySqlCommand(query, con)

            If Not String.IsNullOrEmpty(searchTerm) Then
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
            End If

            If statusFilter <> "All" Then
                cmd.Parameters.AddWithValue("@status", statusFilter)
            End If

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Check if we have data
            If dt.Rows.Count = 0 Then
                dgv.DataSource = Nothing
                Dim message As String = "No tickets found"
                If Not String.IsNullOrEmpty(searchTerm) Then
                    message += $" matching '{searchTerm}'"
                End If
                If statusFilter <> "All" Then
                    message += $" with status '{statusFilter}'"
                End If
                message += "."
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("ticket_id") Then
                dgv.Columns("ticket_id").HeaderText = "Ticket ID"
                dgv.Columns("ticket_id").Width = 70
                dgv.Columns("ticket_id").ReadOnly = True
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").HeaderText = "Username"
                dgv.Columns("username").Width = 80
                dgv.Columns("username").ReadOnly = True
            End If

            If dgv.Columns.Contains("customer_name") Then
                dgv.Columns("customer_name").HeaderText = "Customer Name"
                dgv.Columns("customer_name").Width = 120
                dgv.Columns("customer_name").ReadOnly = True
            End If

            If dgv.Columns.Contains("issue_name") Then
                dgv.Columns("issue_name").HeaderText = "Issue Type"
                dgv.Columns("issue_name").Width = 100
                dgv.Columns("issue_name").ReadOnly = True
            End If

            If dgv.Columns.Contains("description") Then
                dgv.Columns("description").HeaderText = "Description"
                dgv.Columns("description").Width = 150
                dgv.Columns("description").ReadOnly = True
            End If

            If dgv.Columns.Contains("status") Then
                dgv.Columns("status").HeaderText = "Status"
                dgv.Columns("status").Width = 80
                dgv.Columns("status").ReadOnly = True
            End If

            If dgv.Columns.Contains("created_at") Then
                dgv.Columns("created_at").HeaderText = "Created"
                dgv.Columns("created_at").Width = 120
                dgv.Columns("created_at").ReadOnly = True
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading tickets: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadEnhancedRevenueReport(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Combined query for billing records and customer addons revenue
            Dim query As String = "SELECT 
            month_year,
            SUM(billing_revenue) as billing_revenue,
            SUM(addon_revenue) as addon_revenue,
            SUM(billing_revenue + addon_revenue) as total_revenue
        FROM (
            SELECT 
                DATE_FORMAT(billing_month, '%Y-%m') as month_year,
                SUM(total_amount) as billing_revenue,
                0 as addon_revenue
            FROM billing_records 
            WHERE status = 'Paid'
            GROUP BY DATE_FORMAT(billing_month, '%Y-%m')
            
            UNION ALL
            
            SELECT 
                DATE_FORMAT(purchase_date, '%Y-%m') as month_year,
                0 as billing_revenue,
                SUM(a.price * ca.quantity) as addon_revenue
            FROM customer_addons ca
            JOIN addons a ON ca.addon_id = a.addon_id
            GROUP BY DATE_FORMAT(purchase_date, '%Y-%m')
        ) as combined_revenue
        GROUP BY month_year
        ORDER BY month_year DESC"

            Dim cmd As New MySqlCommand(query, con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("month_year") Then
                dgv.Columns("month_year").HeaderText = "Month"
                dgv.Columns("month_year").Width = 80
            End If

            If dgv.Columns.Contains("billing_revenue") Then
                dgv.Columns("billing_revenue").HeaderText = "Plan Revenue"
                dgv.Columns("billing_revenue").Width = 100
                dgv.Columns("billing_revenue").DefaultCellStyle.Format = "C2"
            End If

            If dgv.Columns.Contains("addon_revenue") Then
                dgv.Columns("addon_revenue").HeaderText = "Addon Revenue"
                dgv.Columns("addon_revenue").Width = 100
                dgv.Columns("addon_revenue").DefaultCellStyle.Format = "C2"
            End If

            If dgv.Columns.Contains("total_revenue") Then
                dgv.Columns("total_revenue").HeaderText = "Total Revenue"
                dgv.Columns("total_revenue").Width = 100
                dgv.Columns("total_revenue").DefaultCellStyle.Format = "C2"
                dgv.Columns("total_revenue").DefaultCellStyle.Font = New Font(dgv.Font, FontStyle.Bold)
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading enhanced revenue report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Additional report methods
    Private Sub LoadSubscribersReport(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            Dim cmd As New MySqlCommand("SELECT p.plan_name as 'Plan Name', COUNT(s.subscriber_id) as 'Active Subscribers', s.status as 'Status' FROM subscribers s JOIN internet_plans p ON s.plan_id = p.plan_id GROUP BY p.plan_name, s.status ORDER BY p.plan_name, s.status", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading subscribers report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadPlansReport(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            Dim cmd As New MySqlCommand("SELECT p.plan_name as 'Plan Name', p.plan_type as 'Type', p.price as 'Price', COUNT(s.subscriber_id) as 'Total Subscribers', SUM(CASE WHEN s.status = 'Active' THEN 1 ELSE 0 END) as 'Active Subscribers' FROM internet_plans p LEFT JOIN subscribers s ON p.plan_id = s.plan_id GROUP BY p.plan_id, p.plan_name, p.plan_type, p.price ORDER BY COUNT(s.subscriber_id) DESC", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()

            ' Format price column
            If dgv.Columns.Contains("Price") Then
                dgv.Columns("Price").DefaultCellStyle.Format = "C2"
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading plans report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Report Methods
    Private Sub LoadRevenueReport(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT DATE_FORMAT(billing_month, '%Y-%m') as Month, SUM(total_amount) as Revenue FROM billing_records WHERE status = 'Paid' GROUP BY DATE_FORMAT(billing_month, '%Y-%m') ORDER BY Month DESC", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading revenue report: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadUsersReport(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT role, COUNT(*) as Count FROM users WHERE is_active = 1 GROUP BY role", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading users report: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadTicketsReport(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT status, COUNT(*) as Count FROM support_tickets GROUP BY status", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading tickets report: " & ex.Message)
        End Try
    End Sub


End Class