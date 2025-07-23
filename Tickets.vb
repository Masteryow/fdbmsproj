Imports System.ComponentModel
Imports System.Transactions
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports ZstdSharp.Unsafe

Public Class Tickets
    Dim con As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim firstDown As Boolean = True
    Dim issueId As Integer = 0
    Dim status As String = ""
    Dim technicianId As Integer = 0
    Public Sub disabled()
        cbxIssueType.Enabled = False
        txtDifficulty.Enabled = False
        txtPrice.Enabled = False
        txtNote.Enabled = False
        cbxTechnician.Enabled = False
        txtExpertise.Enabled = False
        txtStatus.Enabled = False
        btnConfirm.Enabled = False
        btnClear.Enabled = False
        txtTStatus.Enabled = False

    End Sub

    Public Sub activates()
        cbxIssueType.Enabled = True
        txtDifficulty.Enabled = True
        txtPrice.Enabled = True
        txtNote.Enabled = True
        cbxTechnician.Enabled = True
        txtExpertise.Enabled = True
        txtStatus.Enabled = True
        btnConfirm.Enabled = True
        btnClear.Enabled = True
        txtTStatus.Enabled = True

    End Sub


    Public Sub clearing()
        cbxIssueType.SelectedIndex = -1
        cbxIssueType.Text = ""
        txtDifficulty.Clear()
        txtPrice.Clear()
        cbxTechnician.SelectedIndex = -1
        cbxTechnician.Text = ""
        cbxTechnician.Enabled = False
        txtExpertise.Clear()
        txtStatus.Clear()
        btnConfirm.Enabled = False
        txtNote.Clear()
    End Sub
    Private Sub Tickets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbxTechnician.Enabled = False


        Try
            Using conn As New MySqlConnection(con)

                conn.Open()


                Using cmd As New MySqlCommand("SELECT * FROM support_tickets WHERE 
                                               subscriber_id = @subscriber_id", conn)

                    cmd.Parameters.AddWithValue("@subscriber_id", CInt(Session.SubscriberId))

                    Using getStatus As MySqlDataReader = cmd.ExecuteReader
                        While getStatus.Read()

                            status = getStatus.GetString("status")

                            txtTStatus.Text = status

                        End While


                    End Using

                    If status = "Open" OrElse status = "In Progress" OrElse
                            status = "Resolved" OrElse status = "Closed" Then


                        disabled()

                        Using cmd2 As New MySqlCommand("SELECT it.issue_name, st.created_at, it.difficulty_level, it.base_salary,
                                                                st.description, t.technician_id, CONCAT(u.firstName, ' ',
                                                                u.lastName) AS fullName, t.skills, u.is_active 
                                                                From issue_types it JOIN support_tickets st ON it.issue_type_id
                                                                = st.issue_type_id JOIN ticket_technicians tt ON st.ticket_id = 
                                                                tt.ticket_id JOIN technicians t ON tt.technician_id = t.technician_id
                                                                JOIN users u ON t.user_id = u.user_id WHERE subscriber_id
                                                                = @subscriber_id ORDER BY st.created_at DESC", conn)
                            cmd2.Parameters.AddWithValue("@subscriber_id", Session.SubscriberId)

                            Using post As MySqlDataReader = cmd2.ExecuteReader()

                                While post.Read
                                    cbxIssueType.SelectedText = post.GetString("issue_name")
                                    txtDifficulty.Text = post.GetString("difficulty_level")
                                    txtPrice.Text = "Php " & post.GetDecimal("base_salary")
                                    txtNote.Text = post.GetString("description")
                                    cbxTechnician.SelectedText = post.GetString("fullName")
                                    txtExpertise.Text = post.GetString("skills")
                                    txtStatus.Text = post.GetBoolean("is_active")

                                    If txtStatus.Text = "True" Then
                                        txtStatus.Text = "Active"
                                    End If
                                End While



                            End Using


                        End Using

                        If status = "Resolved" Then
                            btnNewTicket.Visible = True

                        End If



                    End If

                End Using


                Using issueType As New MySqlCommand("SELECT * FROM issue_types", conn)


                    Using fetchIssue As MySqlDataReader = issueType.ExecuteReader
                        While fetchIssue.Read()
                            cbxIssueType.Items.Add(fetchIssue.GetString("issue_name").ToString())
                        End While

                    End Using

                End Using


                Using technicians As New MySqlCommand("SELECT CONCAT(u.firstName,' ',u.lastName) AS fullName, u.role,
                                                        u.is_active, t.skills FROM users u JOIN technicians t
                                                        ON u.user_id = t.user_id", conn)
                    Using getName As MySqlDataReader = technicians.ExecuteReader

                        While getName.Read

                            cbxTechnician.Items.Add(getName.GetString("fullName"))
                        End While
                    End Using

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try


    End Sub




    Private Sub cbxIssueType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxIssueType.SelectedIndexChanged

        If cbxIssueType.SelectedItem Is Nothing Then Exit Sub

        Dim issueType As String = cbxIssueType.SelectedItem.ToString()

        Try

            Using conn As New MySqlConnection(con)

                conn.Open()

                Using getDifficulty As New MySqlCommand("SELECT issue_type_id, difficulty_level, base_salary FROM issue_types 
                                                        WHERE issue_name = @issue_name", conn)
                    getDifficulty.Parameters.AddWithValue("@issue_name", issueType)

                    Using fetchDifficulty As MySqlDataReader = getDifficulty.ExecuteReader


                        While fetchDifficulty.Read()

                            txtDifficulty.Text = fetchDifficulty.GetString("difficulty_level")
                            txtPrice.Text = "Php " & fetchDifficulty.GetDecimal("base_salary")
                            issueId = fetchDifficulty.GetInt32("issue_type_id")
                            cbxTechnician.Enabled = True


                        End While

                    End Using


                End Using


            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        clearing()

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles gbxTechnician.Click
        If cbxIssueType.SelectedIndex = -1 AndAlso cbxTechnician.Enabled = False AndAlso (status <>
            "Open" AndAlso status <> "In Progress" AndAlso status <> "Resolved" AndAlso status <> "Closed") Then


            MsgBox("Please select an issue type first", MsgBoxStyle.Exclamation, "Warning!")

            Exit Sub
        End If
    End Sub

    Private Sub cbxTechnician_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxTechnician.SelectedIndexChanged

        If cbxTechnician.SelectedItem Is Nothing Then Exit Sub

        Dim getName As String = cbxTechnician.SelectedItem.ToString()

        Try

            Using conn As New MySqlConnection(con)

                conn.Open()

                Using getSkill As New MySqlCommand("SELECT t.technician_id, t.skills, u.is_active FROM technicians t 
                                                    JOIN users u ON t.user_id = u.user_id WHERE CONCAT
                                                    (u.firstName,' ', u.lastName) = @name", conn)

                    getSkill.Parameters.AddWithValue("@name", getName)

                    Using fetchSkill As MySqlDataReader = getSkill.ExecuteReader

                        While fetchSkill.Read
                            txtExpertise.Text = fetchSkill.GetString("skills")
                            txtStatus.Text = fetchSkill.GetBoolean("is_active")
                            technicianId = fetchSkill.GetInt32("technician_id")

                            If txtStatus.Text = "True" Then
                                txtStatus.Text = "Active"
                            End If

                        End While

                    End Using

                End Using




                btnConfirm.Enabled = True
            End Using


        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

        Dim ticket_id As Integer = 0

        Using conn As New MySqlConnection(con)
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction

            Try
                Dim result As DialogResult = MsgBox("You can only submit 1 ticket at a time, do you want to continue?", MsgBoxStyle.YesNo, "Ticket Confirmation")

                If result = DialogResult.Yes Then

                    Using cmd As New MySqlCommand("INSERT INTO support_tickets (customer_id, subscriber_id, issue_type_id, description, task_salary) VALUES (@customer_id, @subscriber_id, @issue_type_id, @description, @task_salary)", conn)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@customer_id", CInt(Session.UserId))
                        cmd.Parameters.AddWithValue("@subscriber_id", CInt(Session.SubscriberId))
                        cmd.Parameters.AddWithValue("@issue_type_id", CInt(issueId))
                        cmd.Parameters.AddWithValue("@description", txtNote.Text)
                        Dim salaryDecimal As Decimal = Decimal.Parse(txtPrice.Text.Replace("Php", "").Trim())
                        cmd.Parameters.AddWithValue("@task_salary", salaryDecimal)
                        cmd.ExecuteNonQuery()
                    End Using


                    Using status As New MySqlCommand("SELECT status, ticket_id FROM support_tickets WHERE subscriber_id = @subscriber_id ORDER BY created_at DESC LIMIT 1", conn)
                        status.Transaction = transaction
                        status.Parameters.AddWithValue("@subscriber_id", CInt(Session.SubscriberId))

                        Using getStatus As MySqlDataReader = status.ExecuteReader()
                            If getStatus.Read() Then
                                txtTStatus.Text = getStatus.GetString("status")
                                ticket_id = getStatus.GetInt32("ticket_id")
                            End If
                        End Using
                    End Using

                    Using cmd As New MySqlCommand("INSERT INTO ticket_technicians (ticket_id, technician_id)
                                                  VALUES (@ticket_id, @technician_id)", conn)
                        cmd.Transaction = transaction
                        cmd.Parameters.AddWithValue("@ticket_id", ticket_id)
                        cmd.Parameters.AddWithValue("@technician_id", technicianId)
                        cmd.ExecuteNonQuery()




                    End Using

                    transaction.Commit()
                    MsgBox("Ticket Successfully Added!")
                    disabled()
                Else
                    transaction.Rollback()
                End If

            Catch ex As Exception
                transaction.Rollback()
                MsgBox("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub gbxTechnician_Enter(sender As Object, e As EventArgs) Handles gbxTechnician.Enter

    End Sub

    Private Sub btnNewTicket_Click(sender As Object, e As EventArgs) Handles btnNewTicket.Click
        btnNewTicket.Visible = False
        clearing()
        txtTStatus.Clear()
        activates()
        btnConfirm.Enabled = False
        cbxTechnician.Enabled = False
        status = ""
    End Sub
End Class