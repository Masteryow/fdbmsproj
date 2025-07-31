Imports MySql.Data.MySqlClient

Public Class TechnicianPanel
    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim ticketMap As New Dictionary(Of Integer, String)
    Dim ticketId As Integer = 0
    Dim currentStatus As String = ""
    Dim isAccepted As Boolean = False

    Sub ticket_loading()
        cbxSubName.Items.Clear()
        ticketMap.Clear()


        Using conn As New MySqlConnection(strCon)
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                Using getConcern As New MySqlCommand("
                    SELECT st.ticket_id,
                           CONCAT(cust.firstName, ' ', cust.lastName) AS fullName,
                           it.issue_name, it.difficulty_level, it.base_salary,
                           st.description, tt.status
                    FROM ticket_technicians tt
                    JOIN technicians t ON tt.technician_id = t.technician_id
                    JOIN support_tickets st ON tt.ticket_id = st.ticket_id
                    JOIN subscribers s ON st.subscriber_id = s.subscriber_id
                    JOIN users cust ON s.customer_id = cust.user_id
                    JOIN issue_types it ON st.issue_type_id = it.issue_type_id
                    WHERE tt.technician_id = @technician_id", conn)

                    getConcern.Transaction = transaction
                    getConcern.Parameters.AddWithValue("@technician_id", CInt(Session.technicianID))

                    Using reader As MySqlDataReader = getConcern.ExecuteReader()
                        While reader.Read()
                            Dim status = reader.GetString("status").Trim()
                            If status = "Completed" Then Continue While

                            Dim ticketId = reader.GetInt32("ticket_id")
                            Dim fullName = reader.GetString("fullName")

                            If Not ticketMap.ContainsKey(ticketId) Then
                                ticketMap.Add(ticketId, fullName)
                                cbxSubName.Items.Add(New With {.Display = fullName, .Value = ticketId})
                            End If
                        End While
                    End Using
                End Using

                transaction.Commit()
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Error loading tickets: " & ex.Message)
            End Try
        End Using

        ' Set ComboBox display/value (anonymous type workaround)
        cbxSubName.DisplayMember = "Display"
        cbxSubName.ValueMember = "Value"
    End Sub
    Private Sub TechnicianPanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ticket_loading()
    End Sub

    Private Sub cbxSubName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxSubName.SelectedIndexChanged
        If cbxSubName.SelectedItem Is Nothing Then Exit Sub

        If cbxSubName.SelectedIndex <= -1 Then
            btnAccept.Visible = False
            btnClear.Visible = False

        Else

            btnAccept.Visible = True
            btnClear.Visible = True
        End If
        Dim selectedItem = cbxSubName.SelectedItem
        ticketId = CInt(selectedItem.GetType().GetProperty("Value").GetValue(selectedItem, Nothing))

        Using conn As New MySqlConnection(strCon)
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                Using cmd As New MySqlCommand("
                    SELECT CONCAT(cust.firstName, ' ', cust.lastName) AS fullName,
                           it.issue_name, it.difficulty_level, it.base_salary,
                           st.description, st.status
                    FROM ticket_technicians tt
                    JOIN technicians t ON tt.technician_id = t.technician_id
                    JOIN support_tickets st ON tt.ticket_id = st.ticket_id
                    JOIN subscribers s ON st.subscriber_id = s.subscriber_id
                    JOIN users cust ON s.customer_id = cust.user_id
                    JOIN issue_types it ON st.issue_type_id = it.issue_type_id
                    WHERE st.ticket_id = @ticket_id", conn)

                    cmd.Transaction = transaction
                    cmd.Parameters.AddWithValue("@ticket_id", ticketId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtConcern.Text = reader.GetString("issue_name")
                            txtDifficulty.Text = reader.GetString("difficulty_level")
                            txtPrice.Text = "Php " & reader.GetDecimal("base_salary").ToString("F2")
                            txtNotes.Text = reader.GetString("description")

                            Dim statusStr = reader.GetString("status")
                            cbxStatus.Text = statusStr
                            Dim index = cbxStatus.FindStringExact(statusStr)
                            If index >= 0 Then
                                cbxStatus.SelectedIndex = index
                            Else
                                cbxStatus.SelectedIndex = -1
                            End If
                        End If
                    End Using
                End Using

                transaction.Commit()
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Error loading ticket details: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnSave.Click


        Using conn As New MySqlConnection(strCon)

            conn.Open()

            Dim transaction As MySqlTransaction = conn.BeginTransaction

            Try


                Using updateStatus As New MySqlCommand("UPDATE ticket_technicians SET status = @status, remarks = @remarks
                                                        WHERE ticket_id = @ticket_id", conn)

                    updateStatus.Transaction = transaction

                    updateStatus.Parameters.AddWithValue("@ticket_id", CInt(ticketId))
                    updateStatus.Parameters.AddWithValue("@status", currentStatus)
                    updateStatus.Parameters.AddWithValue("@remarks", txtRemarks.Text)

                    updateStatus.ExecuteNonQuery()
                End Using

                transaction.Commit()
                MsgBox("Successfully Updated!")

                If currentStatus = "Completed" Then
                    ' DISABLE the event handler temporarily
                    RemoveHandler cbxSubName.SelectedIndexChanged, AddressOf cbxSubName_SelectedIndexChanged

                    ' Clear ComboBox selection and text
                    cbxSubName.SelectedIndex = -1
                    cbxSubName.Text = ""
                    cbxStatus.SelectedIndex = -1
                    cbxStatus.Text = ""

                    ' Clear ALL ticket detail fields
                    txtConcern.Clear()
                    txtDifficulty.Clear()
                    txtPrice.Clear()
                    txtNotes.Clear()
                    txtRemarks.Clear()

                    ' Reset control states
                    isAccepted = False
                    btnAccept.Visible = False
                    btnClear.Visible = False
                    btnSave.Visible = False
                    cbxStatus.Enabled = False
                    txtRemarks.Enabled = False

                    ' Reset ticket ID
                    ticketId = 0
                    currentStatus = ""

                    ' Reload tickets
                    ticket_loading()

                    ' RE-ENABLE the event handler
                    AddHandler cbxSubName.SelectedIndexChanged, AddressOf cbxSubName_SelectedIndexChanged

                End If


            Catch rollEx As Exception
                transaction.Rollback()
                MessageBox.Show("Rollback failed: " & rollEx.Message)
            End Try




        End Using
    End Sub

    Private Sub cbxStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxStatus.SelectedIndexChanged
        If cbxStatus.SelectedItem IsNot Nothing Then
            currentStatus = cbxStatus.SelectedItem.ToString()
        Else
            currentStatus = ""
        End If
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Click
        If isAccepted = False Then


            MsgBox("Please select a subscriber first", MsgBoxStyle.Exclamation, "Subscriber Selection")
            cbxStatus.Enabled = False
            txtRemarks.Enabled = False


            Return

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAccept.Click


        If isAccepted = True Then
            MsgBox("You already accepted the concern", MsgBoxStyle.Exclamation, "Concern Accepted")

        Else
            isAccepted = True
            MsgBox("Accepted Successfully", MsgBoxStyle.DefaultButton1, "Success!")

            btnSave.Visible = True
            cbxStatus.Enabled = True
            txtRemarks.Enabled = True


        End If

    End Sub

    Private Sub GroupBox2_Enter_1(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub btnDecline_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        cbxSubName.SelectedIndex = -1
        cbxSubName.Text = ""
        txtDifficulty.Clear()
        txtConcern.Clear()
        txtPrice.Clear()
        txtNotes.Clear()
        cbxStatus.SelectedIndex = -1
        cbxStatus.Text = ""
        cbxStatus.Enabled = False
        txtRemarks.Clear()
        btnAccept.Visible = False
        btnClear.Visible = False
        btnSave.Visible = False
        isAccepted = False
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub
End Class
