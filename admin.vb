Imports MySql.Data.MySqlClient

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

        Dim btnAddPlan As New Button With {.Text = "Add Plan", .Location = New Point(260, 90), .Size = New Size(80, 23), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnAddPlan.FlatAppearance.BorderSize = 0

        AddHandler btnAddPlan.Click, Sub()
                                         If String.IsNullOrEmpty(txtPlanName.Text) Or String.IsNullOrEmpty(txtPrice.Text) Then
                                             MessageBox.Show("Please fill all required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             Return
                                         End If

                                         Try
                                             con.Open()
                                             Dim cmd As New MySqlCommand("INSERT INTO internet_plans (plan_name, plan_type, price, speed, data_cap) VALUES (@name, @type, @price, @speed, @datacap)", con)
                                             cmd.Parameters.AddWithValue("@name", txtPlanName.Text)
                                             cmd.Parameters.AddWithValue("@type", txtPlanType.Text)
                                             cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text))
                                             cmd.Parameters.AddWithValue("@speed", txtSpeed.Text)
                                             cmd.Parameters.AddWithValue("@datacap", txtDataCap.Text)

                                             cmd.ExecuteNonQuery()
                                             con.Close()

                                             MessageBox.Show("Plan added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                             ' Clear form fields
                                             txtPlanName.Clear()
                                             txtPlanType.Clear()
                                             txtPrice.Clear()
                                             txtSpeed.Clear()
                                             txtDataCap.Clear()

                                             ' Refresh the plans list
                                             LoadPlansDataEnhanced(dgvPlans)
                                         Catch ex As Exception
                                             If con.State = ConnectionState.Open Then con.Close()
                                             MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                         End Try
                                     End Sub

        ' Manage Plans Section
        Dim lblManagePlans As New Label With {.Text = "Manage Plans", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(20, 115), .AutoSize = True}

        ' Save Changes Button
        Dim btnSaveChanges As New Button With {.Text = "Save Changes", .Location = New Point(20, 140), .Size = New Size(100, 25), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnSaveChanges.FlatAppearance.BorderSize = 0

        LoadPlansDataEnhanced(dgvPlans)

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

                                                 For Each row As DataGridViewRow In dgvPlans.Rows
                                                     If Not row.IsNewRow Then
                                                         Dim planID As Integer = Convert.ToInt32(row.Cells("plan_id").Value)
                                                         Dim price As Decimal = Convert.ToDecimal(row.Cells("price").Value)
                                                         Dim speed As String = row.Cells("speed").Value.ToString()
                                                         Dim dataCap As String = row.Cells("data_cap").Value.ToString()

                                                         Dim cmd As New MySqlCommand("UPDATE internet_plans SET price = @price, speed = @speed, data_cap = @datacap WHERE plan_id = @id", con)
                                                         cmd.Parameters.AddWithValue("@price", price)
                                                         cmd.Parameters.AddWithValue("@speed", speed)
                                                         cmd.Parameters.AddWithValue("@datacap", dataCap)
                                                         cmd.Parameters.AddWithValue("@id", planID)
                                                         changesCount += cmd.ExecuteNonQuery()
                                                     End If
                                                 Next
                                                 con.Close()

                                                 MessageBox.Show($"Successfully updated {changesCount} plan(s)!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 LoadPlansDataEnhanced(dgvPlans) ' Refresh the data
                                             Catch ex As Exception
                                                 If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        pnlContent.Controls.AddRange({lblAddPlan, lblPlanName, txtPlanName, lblPlanType, txtPlanType, lblPrice, txtPrice, lblSpeed, txtSpeed, lblDataCap, txtDataCap, btnAddPlan, lblManagePlans, btnSaveChanges, dgvPlans})
    End Sub

    Private Sub LoadPlansDataEnhanced(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()
            Dim cmd As New MySqlCommand("SELECT plan_id, plan_name, plan_type, price, speed, data_cap FROM internet_plans ORDER BY plan_id", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            con.Close()

            ' Clear existing columns and set data source
            dgv.DataSource = Nothing
            dgv.Columns.Clear()
            dgv.DataSource = dt

            ' Configure columns
            If dgv.Columns.Contains("plan_id") Then
                dgv.Columns("plan_id").ReadOnly = True
                dgv.Columns("plan_id").HeaderText = "ID"
                dgv.Columns("plan_id").Width = 40
            End If

            If dgv.Columns.Contains("plan_name") Then
                dgv.Columns("plan_name").ReadOnly = True
                dgv.Columns("plan_name").HeaderText = "Plan Name"
                dgv.Columns("plan_name").Width = 120
            End If

            If dgv.Columns.Contains("plan_type") Then
                dgv.Columns("plan_type").ReadOnly = True
                dgv.Columns("plan_type").HeaderText = "Type"
                dgv.Columns("plan_type").Width = 100
            End If

            ' Make these columns editable
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

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading plans: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        ' Search Section
        Dim lblSearch As New Label With {
        .Text = "Search:",
        .Font = New Font("Segoe UI", 10, FontStyle.Bold),
        .Location = New Point(0, 30),
        .AutoSize = True
    }

        Dim txtSearch As New TextBox With {
        .Location = New Point(60, 28),
        .Size = New Size(200, 23)
    }

        Dim btnSearch As New Button With {
        .Text = "Search",
        .Location = New Point(270, 28),
        .Size = New Size(60, 23),
        .BackColor = Color.FromArgb(52, 152, 219),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnSearch.FlatAppearance.BorderSize = 0

        Dim btnShowAll As New Button With {
        .Text = "Update",
        .Location = New Point(340, 28),
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
        .Size = New Size(410, 260),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    }

        ' Load subscribers data
        LoadSubscribersDataEnhanced(dgvSubscribers)

        ' Search event handler
        AddHandler btnSearch.Click, Sub()
                                        LoadSubscribersDataEnhanced(dgvSubscribers, txtSearch.Text.Trim())
                                    End Sub

        ' Show all event handler
        AddHandler btnShowAll.Click, Sub()
                                         txtSearch.Clear()
                                         LoadSubscribersDataEnhanced(dgvSubscribers)
                                     End Sub

        ' Search on Enter key
        AddHandler txtSearch.KeyDown, Sub(sender, e)
                                          If e.KeyCode = Keys.Enter Then
                                              LoadSubscribersDataEnhanced(dgvSubscribers, txtSearch.Text.Trim())
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
                                                 LoadSubscribersDataEnhanced(dgvSubscribers, txtSearch.Text.Trim()) ' Refresh with current search
                                             Catch ex As Exception
                                                 If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        pnlContent.Controls.AddRange({lblSubscribers, lblSearch, txtSearch, btnSearch, btnShowAll, btnSaveChanges, dgvSubscribers})
    End Sub




    Private Sub LoadSubscribersDataEnhanced(dgv As DataGridView, Optional searchTerm As String = "")
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build query with optional search
            Dim query As String = "SELECT s.subscriber_id, u.username, s.customer_id, s.plan_id, p.plan_name, s.subscription_date, s.status FROM subscribers s JOIN users u ON s.customer_id = u.user_id JOIN internet_plans p ON s.plan_id = p.plan_id"

            If Not String.IsNullOrEmpty(searchTerm) Then
                query += " WHERE u.username LIKE @search OR s.subscriber_id LIKE @search"
            End If

            query += " ORDER BY s.subscriber_id"

            Dim cmd As New MySqlCommand(query, con)
            If Not String.IsNullOrEmpty(searchTerm) Then
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
            End If

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ' Get available plans for dropdown
            Dim plansCmd As New MySqlCommand("SELECT plan_id, plan_name FROM internet_plans", con)
            Dim plansAdapter As New MySqlDataAdapter(plansCmd)
            Dim plansTable As New DataTable()
            plansAdapter.Fill(plansTable)

            con.Close()

            ' Check if we have data
            If dt.Rows.Count = 0 Then
                dgv.DataSource = Nothing
                If String.IsNullOrEmpty(searchTerm) Then
                    MessageBox.Show("No subscribers found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"No subscribers found matching '{searchTerm}'.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

            ' Create dropdown for Status with only Active/Inactive
            If dgv.Columns.Contains("status") Then
                Dim statusIndex As Integer = dgv.Columns("status").Index
                dgv.Columns.RemoveAt(statusIndex)

                Dim statusColumn As New DataGridViewComboBoxColumn()
                statusColumn.Name = "status"
                statusColumn.HeaderText = "Status"
                statusColumn.DataPropertyName = "status"
                statusColumn.Items.AddRange({"Active", "Inactive"})
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
                planColumn.Width = 100

                dgv.Columns.Add(planColumn)

                ' Set the current plan values in the dropdown
                For Each row As DataGridViewRow In dgv.Rows
                    If Not row.IsNewRow Then
                        row.Cells("new_plan_id").Value = row.Cells("plan_id").Value
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

        pnlContent.Controls.AddRange({lblAddTechnician, lblTechnicianUser, cmbTechnicianUser, lblSkills, cmbSkills, btnAddTechnician, lblManageTechnicians, btnSaveChanges, btnUpdate, dgvTechnicians})
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
            Dim cmd As New MySqlCommand("SELECT u.user_id, CONCAT(u.firstName, ' ', u.lastName, ' (', u.username, ')') as display_name FROM users u WHERE u.role = 'technician' AND u.user_id NOT IN (SELECT user_id FROM technicians)", con)
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
        .Size = New Size(400, 180),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    }

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

                                          Try
                                              con.Open()
                                              Dim cmd As New MySqlCommand("INSERT INTO addons (item_name, category, price) VALUES (@itemname, @category, @price)", con)
                                              cmd.Parameters.AddWithValue("@itemname", txtItemName.Text.Trim())
                                              cmd.Parameters.AddWithValue("@category", cmbCategory.SelectedItem.ToString())
                                              cmd.Parameters.AddWithValue("@price", price)

                                              cmd.ExecuteNonQuery()
                                              con.Close()

                                              MessageBox.Show("Add-on added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                              ' Clear form fields
                                              txtItemName.Clear()
                                              cmbCategory.SelectedIndex = -1
                                              txtPrice.Clear()

                                              ' Refresh the addons list
                                              LoadAddonsDataEnhanced(dgvAddons)
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
        Dim lblManageAddons As New Label With {.Text = "Manage Addons Price", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(5, 70), .AutoSize = True}

        ' Save Changes Button
        Dim btnSaveChanges As New Button With {.Text = "Save Changes", .Location = New Point(20, 310), .Size = New Size(100, 25), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnSaveChanges.FlatAppearance.BorderSize = 0

        LoadAddonsDataEnhanced(dgvAddons)

        ' Search event handlers
        AddHandler btnSearch.Click, Sub()
                                        LoadAddonsDataEnhanced(dgvAddons, txtSearch.Text.Trim())
                                    End Sub

        AddHandler btnShowAll.Click, Sub()
                                         txtSearch.Clear()
                                         LoadAddonsDataEnhanced(dgvAddons)
                                     End Sub

        ' Search on Enter key
        AddHandler txtSearch.KeyDown, Sub(sender, e)
                                          If e.KeyCode = Keys.Enter Then
                                              LoadAddonsDataEnhanced(dgvAddons, txtSearch.Text.Trim())
                                          End If
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

                                                 For Each row As DataGridViewRow In dgvAddons.Rows
                                                     If Not row.IsNewRow Then
                                                         Dim addonID As Integer = Convert.ToInt32(row.Cells("addon_id").Value)
                                                         Dim newPrice As Decimal = Convert.ToDecimal(row.Cells("price").Value)

                                                         Dim cmd As New MySqlCommand("UPDATE addons SET price = @price WHERE addon_id = @id", con)
                                                         cmd.Parameters.AddWithValue("@price", newPrice)
                                                         cmd.Parameters.AddWithValue("@id", addonID)
                                                         changesCount += cmd.ExecuteNonQuery()
                                                     End If
                                                 Next
                                                 con.Close()

                                                 MessageBox.Show($"Successfully updated {changesCount} add-on(s) price!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 LoadAddonsDataEnhanced(dgvAddons, txtSearch.Text.Trim()) ' Refresh with current search
                                             Catch ex As Exception
                                                 If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        pnlContent.Controls.AddRange({lblAddAddon, lblItemName, txtItemName, lblCategory, cmbCategory, lblPrice, txtPrice, btnAddAddon, lblSearch, txtSearch, btnSearch, btnShowAll, lblManageAddons, btnSaveChanges, dgvAddons})
    End Sub

    Private Sub LoadAddonsDataEnhanced(dgv As DataGridView, Optional searchTerm As String = "")
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Build query with optional search
            Dim query As String = "SELECT addon_id, item_name, category, price FROM addons"

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

            ' Configure columns
            If dgv.Columns.Contains("addon_id") Then
                dgv.Columns("addon_id").ReadOnly = True
                dgv.Columns("addon_id").HeaderText = "ID"
                dgv.Columns("addon_id").Width = 50
            End If

            If dgv.Columns.Contains("item_name") Then
                dgv.Columns("item_name").ReadOnly = True
                dgv.Columns("item_name").HeaderText = "Item Name"
                dgv.Columns("item_name").Width = 150
            End If

            If dgv.Columns.Contains("category") Then
                dgv.Columns("category").ReadOnly = True
                dgv.Columns("category").HeaderText = "Category"
                dgv.Columns("category").Width = 100
            End If

            ' Make price column editable for modifications
            If dgv.Columns.Contains("price") Then
                dgv.Columns("price").ReadOnly = False
                dgv.Columns("price").HeaderText = "Price"
                dgv.Columns("price").Width = 80
            End If

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading add-ons: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        ' Report type buttons
        Dim btnRevenueReport As New Button With {
        .Text = "Revenue Report",
        .Location = New Point(20, 40),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(52, 152, 219),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "revenue"
    }
        btnRevenueReport.FlatAppearance.BorderSize = 0

        Dim btnUsersReport As New Button With {
        .Text = "Active Users Report",
        .Location = New Point(150, 40),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "users"
    }
        btnUsersReport.FlatAppearance.BorderSize = 0

        Dim btnTicketsReport As New Button With {
        .Text = "Tickets Report",
        .Location = New Point(280, 40),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "tickets"
    }
        btnTicketsReport.FlatAppearance.BorderSize = 0

        Dim btnSubscribersReport As New Button With {
        .Text = "Subscribers Report",
        .Location = New Point(20, 80),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "subscribers"
    }
        btnSubscribersReport.FlatAppearance.BorderSize = 0

        Dim btnPlansReport As New Button With {
        .Text = "Plans Report",
        .Location = New Point(150, 80),
        .Size = New Size(120, 30),
        .BackColor = Color.FromArgb(149, 165, 166),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Tag = "plans"
    }
        btnPlansReport.FlatAppearance.BorderSize = 0

        ' DataGridView for reports
        Dim dgvReports As New DataGridView With {
        .Location = New Point(20, 120),
        .Size = New Size(400, 220),
        .ReadOnly = True,
        .AllowUserToAddRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    }

        ' Track current report type
        Dim currentReportType As String = "revenue"

        ' Button click handlers with color management
        Dim reportButtons() As Button = {btnRevenueReport, btnUsersReport, btnTicketsReport, btnSubscribersReport, btnPlansReport}

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
                                          Case "users"
                                              LoadUsersReport(dgvReports)
                                          Case "tickets"
                                              LoadTicketsReport(dgvReports)
                                          Case "subscribers"
                                              LoadSubscribersReport(dgvReports)
                                          Case "plans"
                                              LoadPlansReport(dgvReports)
                                      End Select
                                  End Sub
        Next

        ' Load default report
        LoadEnhancedRevenueReport(dgvReports)

        pnlContent.Controls.AddRange({lblReports, btnRevenueReport, btnUsersReport, btnTicketsReport, btnSubscribersReport, btnPlansReport, dgvReports})
    End Sub

    ' Data Loading Methods
    Private Sub LoadUsersData(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT user_id, username, role, firstName, lastName, email, is_active FROM users", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading users: " & ex.Message)
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