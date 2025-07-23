Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class subscribers
    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim name1 As String = "Welcome To SkyLink, " & Session.UserName & "!"
    Dim increment As Integer = 0
    Public receivID As Integer
    Dim status As String = ""
    Dim planName As String = ""
    Dim planPrice As Decimal = 0
    Dim billAmount As Decimal = 0
    Dim billMonth As String = ""
    Dim billingId As Integer = 0

    Private Sub subscribers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblUser.Text = ""         ' Clear label before animation
        increment = 0             ' Reset increment
        Timer1.Interval = 100     ' Set interval (milliseconds)
        Timer1.Start()            ' Start timer

        ' === Fetch subscriber details from database ===
        Using con As New MySqlConnection(strCon)
            Try
                con.Open()
                Dim query As String = "
                    SELECT s.status, p.plan_name, p.price
                    FROM subscribers s
                    LEFT JOIN internet_plans p ON s.plan_id = p.plan_id
                    WHERE s.subscriber_id = @id
                "
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", receivID)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            status = reader("status").ToString()
                            planName = reader("plan_name").ToString()
                            planPrice = Convert.ToDecimal(reader("price"))
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading subscriber info: " & ex.Message)
            End Try
        End Using

        ' === Determine billing panel visibility based on status ===
        If status = "Active" Then
            panelBilling.Visible = True
        Else
            panelBilling.Visible = False
        End If

        ' === Optional: Load billing record for current month ===
        Using con As New MySqlConnection(strCon)
            Try
                con.Open()
                Dim currentMonth As String = DateTime.Now.ToString("yyyy-MM")
                Dim query As String = "
                    SELECT billing_id, total_amount 
                    FROM billing_records 
                    WHERE subscriber_id = @id AND billing_month = @month
                "
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", receivID)
                    cmd.Parameters.AddWithValue("@month", currentMonth)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            billingId = Convert.ToInt32(reader("billing_id"))
                            billAmount = Convert.ToDecimal(reader("total_amount"))
                            billMonth = currentMonth
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading billing info: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If increment < name1.Length Then
            lblUser.Text &= name1(increment)
            increment += 1
        Else
            Timer1.Stop()
        End If
    End Sub
End Class
