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

        Dim cmbRole As New ComboBox With {.Location = New Point(240, 50), .Size = New Size(80, 23), .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbRole.Items.AddRange({"Admin", "Employee", "Customer"})
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
                                             Dim cmd As New MySqlCommand("INSERT INTO users (username, password, role, firstName, lastName, email, phoneNumber, address) VALUES (@username, @password, @role, @firstName, @lastName, @email, @phone, @address)", con)
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
                                             ShowUsersPanel() ' Refresh the panel
                                         Catch ex As Exception
                                             If con.State = ConnectionState.Open Then con.Close()
                                             MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                         End Try
                                     End Sub

        ' Users List
        Dim dgvUsers As New DataGridView With {.Location = New Point(20, 170), .Size = New Size(400, 160), .ReadOnly = True, .AllowUserToAddRows = False}
        LoadUsersData(dgvUsers)

        pnlContent.Controls.AddRange({lblAddUser, lblUsername, txtUsername, lblPassword, txtPassword, lblRole, cmbRole, lblFirstName, txtFirstName, lblLastName, txtLastName, lblEmail, txtEmail, lblPhone, txtPhone, lblAddress, txtAddress, btnAddUser, dgvUsers})
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

        Dim btnAddPlan As New Button With {.Text = "Add Plan", .Location = New Point(350, 90), .Size = New Size(75, 23), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
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
                                             ShowPlansPanel()
                                         Catch ex As Exception
                                             If con.State = ConnectionState.Open Then con.Close()
                                             MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                         End Try
                                     End Sub

        Dim dgvPlans As New DataGridView With {.Location = New Point(20, 130), .Size = New Size(400, 200), .ReadOnly = True, .AllowUserToAddRows = False}
        LoadPlansData(dgvPlans)

        pnlContent.Controls.AddRange({lblAddPlan, lblPlanName, txtPlanName, lblPlanType, txtPlanType, lblPrice, txtPrice, lblSpeed, txtSpeed, lblDataCap, txtDataCap, btnAddPlan, dgvPlans})
    End Sub

    Private Sub ShowSubscribersPanel()
        pnlContent.Controls.Clear()

        Dim lblSubscribers As New Label With {
        .Text = "Subscriber Management",
        .Font = New Font("Segoe UI", 12, FontStyle.Bold),
        .Location = New Point(0, 0),
        .AutoSize = True
    }

        ' Delete subscriber section
        Dim lblDeleteSubscriber As New Label With {
        .Text = "Delete Subscriber:",
        .Font = New Font("Segoe UI", 10, FontStyle.Bold),
        .Location = New Point(0, 30),
        .AutoSize = True
    }

        Dim txtSubscriberID As New TextBox With {
        .Location = New Point(0, 50),
        .Size = New Size(100, 23)
    }

        Dim btnDeleteSubscriber As New Button With {
        .Text = "Delete",
        .Location = New Point(110, 50),
        .Size = New Size(75, 23),
        .BackColor = Color.FromArgb(231, 76, 60),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnDeleteSubscriber.FlatAppearance.BorderSize = 0

        ' Save changes button
        Dim btnSaveChanges As New Button With {
        .Text = "Save Changes",
        .Location = New Point(0, 80),
        .Size = New Size(100, 30),
        .BackColor = Color.FromArgb(46, 204, 113),
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat
    }
        btnSaveChanges.FlatAppearance.BorderSize = 0

        ' DataGridView with custom columns
        Dim dgvSubscribers As New DataGridView With {
        .Location = New Point(0, 120),
        .Size = New Size(410, 240),
        .AllowUserToAddRows = False,
        .AllowUserToDeleteRows = False,
        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    }

        ' Load subscribers data with enhanced functionality
        LoadSubscribersDataEnhanced(dgvSubscribers)

        ' Delete subscriber event handler
        AddHandler btnDeleteSubscriber.Click, Sub()
                                                  If String.IsNullOrEmpty(txtSubscriberID.Text) Then
                                                      MessageBox.Show("Please enter a Subscriber ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                      Return
                                                  End If

                                                  If MessageBox.Show($"Are you sure you want to delete subscriber with ID {txtSubscriberID.Text}?",
                          "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                                      Try
                                                          con.Open()
                                                          Dim cmd As New MySqlCommand("DELETE FROM subscribers WHERE subscriber_id = @id", con)
                                                          cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtSubscriberID.Text))

                                                          Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                                                          con.Close()

                                                          If rowsAffected > 0 Then
                                                              MessageBox.Show("Subscriber deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                              txtSubscriberID.Clear()
                                                              LoadSubscribersDataEnhanced(dgvSubscribers) ' Refresh the grid
                                                          Else
                                                              MessageBox.Show("No subscriber found with that ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                          End If
                                                      Catch ex As Exception
                                                          If con.State = ConnectionState.Open Then con.Close()
                                                          MessageBox.Show("Error deleting subscriber: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                      End Try
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
                                                         cmd.ExecuteNonQuery()
                                                     End If
                                                 Next
                                                 con.Close()

                                                 MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 LoadSubscribersDataEnhanced(dgvSubscribers) ' Refresh to show updated data
                                             Catch ex As Exception
                                                 If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
                                                 MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                             End Try
                                         End Sub

        pnlContent.Controls.AddRange({lblSubscribers, lblDeleteSubscriber, txtSubscriberID, btnDeleteSubscriber, btnSaveChanges, dgvSubscribers})
    End Sub

    Private Sub LoadSubscribersDataEnhanced(dgv As DataGridView)
        Try
            If con Is Nothing Then
                con = New MySqlConnection(strcon)
            End If

            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Load subscriber data with plan names
            Dim cmd As New MySqlCommand("SELECT s.subscriber_id, u.username, s.customer_id, s.plan_id, p.plan_name, s.subscription_date, s.status FROM subscribers s JOIN users u ON s.customer_id = u.user_id JOIN internet_plans p ON s.plan_id = p.plan_id", con)
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
                MessageBox.Show("No subscribers found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            End If

            If dgv.Columns.Contains("username") Then
                dgv.Columns("username").ReadOnly = True
                dgv.Columns("username").HeaderText = "Username"
            End If

            If dgv.Columns.Contains("subscription_date") Then
                dgv.Columns("subscription_date").ReadOnly = True
                dgv.Columns("subscription_date").HeaderText = "Subscription Date"
            End If

            If dgv.Columns.Contains("plan_name") Then
                dgv.Columns("plan_name").ReadOnly = True
                dgv.Columns("plan_name").HeaderText = "Current Plan"
            End If

            ' Create dropdown for Status
            If dgv.Columns.Contains("status") Then
                Dim statusIndex As Integer = dgv.Columns("status").Index
                dgv.Columns.RemoveAt(statusIndex)

                Dim statusColumn As New DataGridViewComboBoxColumn()
                statusColumn.Name = "status"
                statusColumn.HeaderText = "Status"
                statusColumn.DataPropertyName = "status"
                statusColumn.Items.AddRange({"Active", "Suspended", "Cancelled", "Pending"})

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

                dgv.Columns.Add(planColumn)

                ' Set the current plan values in the dropdown
                For Each row As DataGridViewRow In dgv.Rows
                    If Not row.IsNewRow Then
                        row.Cells("new_plan_id").Value = row.Cells("plan_id").Value
                    End If
                Next
            End If

            ' Auto-resize columns
            dgv.AutoResizeColumns()

        Catch ex As Exception
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading subscribers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowTechniciansPanel()
        pnlContent.Controls.Clear()

        Dim lblTechnicians As New Label With {.Text = "Technician Management", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(20, 20), .AutoSize = True}

        Dim dgvTechnicians As New DataGridView With {.Location = New Point(20, 50), .Size = New Size(400, 280), .ReadOnly = True, .AllowUserToAddRows = False}
        LoadTechniciansData(dgvTechnicians)

        pnlContent.Controls.AddRange({lblTechnicians, dgvTechnicians})
    End Sub

    Private Sub ShowAddonsPanel()
        pnlContent.Controls.Clear()

        Dim lblAddons As New Label With {.Text = "Add-on Management", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(20, 20), .AutoSize = True}

        Dim dgvAddons As New DataGridView With {.Location = New Point(20, 50), .Size = New Size(400, 280), .ReadOnly = True, .AllowUserToAddRows = False}
        LoadAddonsData(dgvAddons)

        pnlContent.Controls.AddRange({lblAddons, dgvAddons})
    End Sub

    Private Sub ShowBillingPanel()
        pnlContent.Controls.Clear()

        Dim lblBilling As New Label With {.Text = "Billing Management", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(20, 20), .AutoSize = True}

        Dim dgvBilling As New DataGridView With {.Location = New Point(20, 50), .Size = New Size(400, 280), .ReadOnly = True, .AllowUserToAddRows = False}
        LoadBillingData(dgvBilling)

        pnlContent.Controls.AddRange({lblBilling, dgvBilling})
    End Sub

    Private Sub ShowTicketsPanel()
        pnlContent.Controls.Clear()

        Dim lblTickets As New Label With {.Text = "Support Tickets", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(20, 20), .AutoSize = True}

        Dim dgvTickets As New DataGridView With {.Location = New Point(20, 50), .Size = New Size(400, 280), .ReadOnly = True, .AllowUserToAddRows = False}
        LoadTicketsData(dgvTickets)

        pnlContent.Controls.AddRange({lblTickets, dgvTickets})
    End Sub

    Private Sub ShowReportsPanel()
        pnlContent.Controls.Clear()

        Dim lblReports As New Label With {.Text = "System Reports", .Font = New Font("Segoe UI", 12, FontStyle.Bold), .Location = New Point(20, 20), .AutoSize = True}

        ' Revenue Report
        Dim btnRevenueReport As New Button With {.Text = "Revenue Report", .Location = New Point(20, 60), .Size = New Size(120, 30), .BackColor = Color.FromArgb(52, 152, 219), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnRevenueReport.FlatAppearance.BorderSize = 0

        ' Active Users Report
        Dim btnUsersReport As New Button With {.Text = "Active Users Report", .Location = New Point(150, 60), .Size = New Size(120, 30), .BackColor = Color.FromArgb(46, 204, 113), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnUsersReport.FlatAppearance.BorderSize = 0

        ' Tickets Report
        Dim btnTicketsReport As New Button With {.Text = "Tickets Report", .Location = New Point(280, 60), .Size = New Size(120, 30), .BackColor = Color.FromArgb(231, 76, 60), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnTicketsReport.FlatAppearance.BorderSize = 0

        Dim dgvReports As New DataGridView With {.Location = New Point(20, 100), .Size = New Size(400, 230), .ReadOnly = True, .AllowUserToAddRows = False}

        AddHandler btnRevenueReport.Click, Sub() LoadRevenueReport(dgvReports)
        AddHandler btnUsersReport.Click, Sub() LoadUsersReport(dgvReports)
        AddHandler btnTicketsReport.Click, Sub() LoadTicketsReport(dgvReports)

        pnlContent.Controls.AddRange({lblReports, btnRevenueReport, btnUsersReport, btnTicketsReport, dgvReports})
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

    Private Sub LoadBillingData(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT b.billing_id, u.username, b.billing_month, b.total_amount, b.due_date, b.status FROM billing_records b JOIN subscribers s ON b.subscriber_id = s.subscriber_id JOIN users u ON s.customer_id = u.user_id", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading billing data: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadTicketsData(dgv As DataGridView)
        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT t.ticket_id, u.username, i.issue_name, t.description, t.status, t.created_at FROM support_tickets t JOIN users u ON t.customer_id = u.user_id JOIN issue_types i ON t.issue_type_id = i.issue_type_id", con)
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgv.DataSource = dt
            con.Close()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then con.Close()
            MessageBox.Show("Error loading tickets: " & ex.Message)
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