Imports MySql.Data.MySqlClient

Public Class Supervisor_Panel
    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim ticketDict As New Dictionary(Of String, Integer)
    Dim techDict As New Dictionary(Of String, Integer)
    Dim hasChosen As Boolean = False
    Dim technicianId As Integer = 0
    '
    Sub loadingInfo()
        Try
            Using conn As New MySqlConnection(strCon)
                conn.Open()

                ' Load Subscriber Tickets
                Using getInfo As New MySqlCommand("SELECT u.user_id, s.subscriber_id, 
                            CONCAT(u.firstName, ' ', u.lastName) AS fullName, it.issue_name, it.difficulty_level, 
                            it.base_salary, st.description, st.ticket_id, st.status AS ticket_status, tt.status AS 
                            technician_status FROM ticket_technicians tt
                            JOIN support_tickets st ON tt.ticket_id = st.ticket_id
                            JOIN subscribers s ON st.subscriber_id = s.subscriber_id
                            JOIN users u ON s.customer_id = u.user_id
                            JOIN issue_types it ON st.issue_type_id = it.issue_type_id
                            WHERE tt.status = 'Unassigned' ", conn)

                    Using getOpen As MySqlDataReader = getInfo.ExecuteReader()
                        ticketDict.Clear()
                        cbxSubName.Items.Clear()
                        While getOpen.Read()
                            Dim fullName As String = getOpen.GetString("fullName")
                            Dim ticketId As Integer = getOpen.GetInt32("ticket_id")
                            Dim displayName As String = fullName & " (Ticket #" & ticketId & ")"

                            ' Use displayName as key for uniqueness
                            If Not ticketDict.ContainsKey(displayName) Then
                                ticketDict.Add(displayName, ticketId)
                                cbxSubName.Items.Add(displayName)
                            End If
                        End While

                    End Using
                End Using

                ' Load Technician Names into ComboBox (but don’t open list until user clicks)
                Using getTechInfo As New MySqlCommand("
                    SELECT CONCAT(u.firstName, ' ', u.lastName) AS fullName,
                           t.technician_id
                    FROM users u
                    JOIN technicians t ON u.user_id = t.user_id", conn)

                    Using fetchInfo As MySqlDataReader = getTechInfo.ExecuteReader()
                        techDict.Clear()
                        cbxTechName.Items.Clear()

                        While fetchInfo.Read()
                            Dim fullName As String = fetchInfo.GetString("fullName")
                            Dim techId As Integer = fetchInfo.GetInt32("technician_id")

                            If Not techDict.ContainsKey(fullName) Then
                                techDict.Add(fullName, techId)
                                cbxTechName.Items.Add(fullName)
                            End If
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub
    Private Sub Supervisor_Panel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadingInfo()
    End Sub

    Private Sub cbxSubName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxSubName.SelectedIndexChanged
        If cbxSubName.SelectedItem Is Nothing Then Exit Sub

        Dim selectedName As String = cbxSubName.SelectedItem.ToString()

        If Not ticketDict.ContainsKey(selectedName) Then
            MessageBox.Show("Ticket not found.")
            Exit Sub
        End If

        btnChoose.Visible = True
        btnClear.Visible = True

        Dim ticketId As Integer = ticketDict(selectedName)

        Try
            Using conn As New MySqlConnection(strCon)
                conn.Open()

                Using getInfo As New MySqlCommand("
                    SELECT it.issue_name, it.difficulty_level, it.base_salary, st.description 
                    FROM support_tickets st
                    JOIN issue_types it ON st.issue_type_id = it.issue_type_id
                    WHERE st.ticket_id = @ticket_id", conn)

                    getInfo.Parameters.AddWithValue("@ticket_id", ticketId)

                    Using reader As MySqlDataReader = getInfo.ExecuteReader()
                        If reader.Read() Then
                            txtConcern.Text = reader.GetString("issue_name")
                            txtDifficulty.Text = reader.GetString("difficulty_level")
                            txtPrice.Text = "Php " & reader.GetDecimal("base_salary").ToString("F2")
                            txtNotes.Text = reader.GetString("description")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading ticket details: " & ex.Message)
        End Try
    End Sub

    Private Sub cbxTechName_DropDown(sender As Object, e As EventArgs) Handles cbxTechName.DropDown
        ' Only fetch when user opens dropdown
    End Sub

    Private Sub cbxTechName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxTechName.SelectedIndexChanged
        If cbxTechName.SelectedItem Is Nothing Then Exit Sub

        Dim selectedTech As String = cbxTechName.SelectedItem.ToString()

        If Not techDict.ContainsKey(selectedTech) Then
            MessageBox.Show("Technician not found.")
            Exit Sub
        End If

        technicianId = techDict(selectedTech)

        Try
            Using conn As New MySqlConnection(strCon)
                conn.Open()

                ' Load Technician Skills & Status
                Using getSkills As New MySqlCommand("
                    SELECT t.skills, u.is_active 
                    FROM technicians t
                    JOIN users u ON u.user_id = t.user_id
                    WHERE t.technician_id = @technician_id", conn)

                    getSkills.Parameters.AddWithValue("@technician_id", technicianId)

                    Using reader As MySqlDataReader = getSkills.ExecuteReader()
                        If reader.Read() Then
                            txtSkills.Text = reader.GetString("skills")
                            txtStatus.Text = If(reader.GetBoolean("is_active"), "Active", "Inactive")
                        End If
                    End Using
                End Using

                ' Load Pending Tickets
                cbxPendingTix.Items.Clear()
                Using getPending As New MySqlCommand("
                    SELECT ticket_id FROM ticket_technicians 
                    WHERE technician_id = @technician_id 
                    AND status IN ('Assigned', 'In Progress')", conn)

                    getPending.Parameters.AddWithValue("@technician_id", technicianId)

                    Using fetchPending As MySqlDataReader = getPending.ExecuteReader()
                        While fetchPending.Read()
                            cbxPendingTix.Items.Add(fetchPending.GetInt32("ticket_id"))
                        End While
                    End Using
                End Using

                ' Load Completed Tickets
                cbxCompletedTix.Items.Clear()
                Using getCompleted As New MySqlCommand("
                    SELECT ticket_id FROM ticket_technicians 
                    WHERE technician_id = @technician_id 
                    AND status = 'Completed'", conn)

                    getCompleted.Parameters.AddWithValue("@technician_id", technicianId)

                    Using fetchCompleted As MySqlDataReader = getCompleted.ExecuteReader()
                        While fetchCompleted.Read()
                            cbxCompletedTix.Items.Add(fetchCompleted.GetInt32("ticket_id"))
                        End While
                    End Using
                End Using

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading technician details: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        cbxSubName.SelectedIndex = -1
        txtConcern.Clear()
        txtDifficulty.Clear()
        txtNotes.Clear()
        txtPrice.Clear()
        btnChoose.Visible = False
        btnClear.Visible = False
        hasChosen = False
        MsgBox("Cleared Successfully!")
    End Sub

    Private Sub btnChoose_Click(sender As Object, e As EventArgs) Handles btnChoose.Click
        hasChosen = True
        btnAssign.Visible = True
        MsgBox("Subscriber chosen successfully")
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub btnAssign_Click(sender As Object, e As EventArgs) Handles btnAssign.Click
        If hasChosen = True AndAlso cbxTechName.SelectedIndex <> -1 AndAlso cbxSubName.SelectedIndex <> -1 Then
            Dim selectedTicketKey As String = cbxSubName.SelectedItem.ToString()
            Dim ticketID As Integer = ticketDict(selectedTicketKey)

            Try
                Using conn As New MySqlConnection(strCon)
                    conn.Open()

                    ' Check if ticket_technicians record exists for this ticket
                    Using checkCmd As New MySqlCommand("SELECT COUNT(*) FROM ticket_technicians WHERE ticket_id = @ticket_id", conn)
                        checkCmd.Parameters.AddWithValue("@ticket_id", ticketID)
                        Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                        If exists > 0 Then
                            ' Update status and technician_id
                            Using updateCmd As New MySqlCommand("
                            UPDATE ticket_technicians 
                            SET technician_id = @technician_id, status = @status, assigned_at = CURRENT_TIMESTAMP 
                            WHERE ticket_id = @ticket_id", conn)

                                updateCmd.Parameters.AddWithValue("@technician_id", technicianId)
                                updateCmd.Parameters.AddWithValue("@status", "Assigned")
                                updateCmd.Parameters.AddWithValue("@ticket_id", ticketID)

                                Dim rowsAffected As Integer = updateCmd.ExecuteNonQuery()
                                If rowsAffected > 0 Then
                                    MsgBox("Ticket assigned and status updated successfully!")
                                    loadingInfo()
                                Else
                                    MsgBox("Failed to update the ticket assignment.")
                                End If
                            End Using
                        End If
                    End Using
                End Using

                cbxSubName.SelectedIndex = -1
                cbxSubName.Text = ""
                txtConcern.Clear()
                txtDifficulty.Clear()
                txtNotes.Clear()
                txtPrice.Clear()
                txtSkills.Clear()
                txtStatus.Clear()
                btnAssign.Visible = False
                btnChoose.Visible = False
                btnClear.Visible = False
                cbxTechName.SelectedIndex = -1
                cbxTechName.Text = ""
                cbxPendingTix.SelectedIndex = -1
                cbxPendingTix.Text = ""
                cbxCompletedTix.SelectedIndex = -1
                cbxCompletedTix.Text = ""




            Catch ex As Exception
                MessageBox.Show("Error assigning ticket: " & ex.Message)
            End Try
        Else
            MsgBox("Please select both a ticket and a technician first!")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TechnicianSalary.Show()
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub
End Class
