Imports System.Transactions
Imports MySql.Data.MySqlClient

Public Class TechnicianSalary
    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim salary As Decimal = 0
    Dim techName As String = ""
    Dim ticket_id As Integer = 0
    Dim total As Decimal = 0
    Private Sub RefreshSalaryData()
        Try
            Using conn As New MySqlConnection(strCon)
                conn.Open()

                Using cmd As New MySqlCommand("SELECT tt.technician_id AS `Technician ID`, CONCAT(u.firstName,' ', u.lastName) AS `Technician Name`, 
                                              st.ticket_id AS `Ticket ID`, st.task_salary AS `Task Salary`, tt.status AS `Ticket Status`, tt.payment_status AS `Payment Status`
                                              FROM users u 
                                              JOIN technicians t ON u.user_id = t.user_id 
                                              JOIN ticket_technicians tt ON t.technician_id = tt.technician_id 
                                              JOIN support_tickets st ON tt.ticket_id = st.ticket_id WHERE tt.status = @status AND tt.payment_status = @payment_status", conn)

                    cmd.Parameters.AddWithValue("@status", "Completed")
                    cmd.Parameters.AddWithValue("@payment_status", "Unpaid")

                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)

                    dgvSalary.DataSource = table
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub TechnicianSalary_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Try
            Using conn As New MySqlConnection(strCon)

                conn.Open()

                Using cmd As New MySqlCommand("SELECT tt.technician_id AS `Technician ID`, CONCAT(u.firstName,' ', u.lastName) AS `Technician Name`, 
                                              st.ticket_id AS `Ticket ID`, st.task_salary AS `Task Salary`, tt.status AS `Ticket Status`, tt.payment_status AS `Payment Status`,
                                              tt.remarks AS Remarks FROM users u 
                                              JOIN technicians t ON u.user_id = t.user_id 
                                              JOIN ticket_technicians tt ON t.technician_id = tt.technician_id 
                                              JOIN support_tickets st ON tt.ticket_id = st.ticket_id WHERE tt.status = @status AND tt.payment_status = @payment_status", conn)
                    cmd.Parameters.AddWithValue("@status", "Completed")
                    cmd.Parameters.AddWithValue("@payment_status", "Unpaid")

                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable
                    adapter.Fill(table)

                    dgvSalary.DataSource = table



                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub btnIndPay_Click(sender As Object, e As EventArgs) Handles btnIndPay.Click

        If dgvSalary.Rows.Count = 0 Then
            MessageBox.Show("No unpaid tasks to process.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try



            Using conn As New MySqlConnection(strCon)

                    conn.Open()

                    Using transaction As MySqlTransaction = conn.BeginTransaction

                    Dim confirm = MessageBox.Show($"Are you sure you want to pay {techName} Php {salary.ToString("F2")}?",
                          "Confirm Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If confirm = DialogResult.Yes Then

                        MsgBox($"{techName} has been marked as paid")

                        Using updateStatus As New MySqlCommand("UPDATE ticket_technicians SET payment_status = @payment_status
                                                               WHERE ticket_id = @ticket_id", conn, transaction)

                            updateStatus.Parameters.AddWithValue("@payment_status", "Paid")
                            updateStatus.Parameters.AddWithValue("@ticket_id", ticket_id)

                            updateStatus.ExecuteNonQuery()

                            transaction.Commit()

                            RefreshSalaryData()
                        End Using


                    Else

                            MsgBox($"Payment has been cancelled successfully!")
                            transaction.Rollback()

                    End If

                End Using


            End Using

        Catch ex As Exception

            MsgBox("Error: " & ex.Message)
        End Try



    End Sub

    Private Sub dgvSalary_CellContentClick(sender As Object, e As EventArgs) Handles dgvSalary.SelectionChanged

        If dgvSalary.SelectedRows.Count > 0 Then

            Dim currentRow As DataGridViewRow = dgvSalary.SelectedRows(0)

            salary = CInt(currentRow.Cells("Task Salary").Value)
            techName = currentRow.Cells("Technician Name").Value.ToString
            ticket_id = CInt(currentRow.Cells("Ticket ID").Value)


        End If



    End Sub

    Private Sub btnPayAll_Click(sender As Object, e As EventArgs) Handles btnPayAll.Click

        If dgvSalary.Rows.Count = 0 Then
            MessageBox.Show("No unpaid tasks to process.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        Try
            Using conn As New MySqlConnection(strCon)
                conn.Open()
                Using transaction As MySqlTransaction = conn.BeginTransaction()

                    total = 0

                    Using getTotal As New MySqlCommand("SELECT SUM(st.task_salary) AS `Task Salary`
                                                                    FROM ticket_technicians tt
                                                                    JOIN support_tickets st ON tt.ticket_id = st.ticket_id
                                                                    WHERE tt.payment_status = @payment_status AND tt.status = @status", conn, transaction)

                        getTotal.Parameters.AddWithValue("@payment_status", "Unpaid")
                        getTotal.Parameters.AddWithValue("@status", "Completed")
                        Dim result As Decimal = getTotal.ExecuteScalar

                        total = result.ToString("f2")


                    End Using


                    Dim confirm = MessageBox.Show($"Are you sure you want to mark all listed tasks as Paid with a total of Php {total.ToString("f2")}",
                                      "Confirm Bulk Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If confirm = DialogResult.No Then Exit Sub

                    Try
                        For Each row As DataGridViewRow In dgvSalary.Rows
                            If row.Cells("Payment Status").Value.ToString() = "Unpaid" Then

                                ticket_id = CInt(row.Cells("Ticket ID").Value)

                                Using updateCmd As New MySqlCommand("UPDATE ticket_technicians 
                                                                 SET payment_status = 'Paid' 
                                                                 WHERE ticket_id = @ticket_id", conn, transaction)


                                    updateCmd.Parameters.AddWithValue("@ticket_id", ticket_id)

                                    updateCmd.ExecuteNonQuery()
                                End Using
                            End If
                        Next



                        transaction.Commit()
                        MessageBox.Show($"All applicable technician tasks have been marked as Paid.{vbNewLine}Total: Php {total.ToString("f2")} ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        RefreshSalaryData()

                    Catch ex As Exception
                        transaction.Rollback()
                        MessageBox.Show("An error occurred while processing: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

        Dim searchValue As String = txtName.Text.Trim().ToLower()

        dgvSalary.ClearSelection()
        dgvSalary.CurrentCell = Nothing

        For Each row As DataGridViewRow In dgvSalary.Rows
            Dim technicianName As String = row.Cells("Technician Name").Value.ToString.ToLower


            row.Visible = technicianName.Contains(searchValue)

        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Supervisor_Panel.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub
End Class