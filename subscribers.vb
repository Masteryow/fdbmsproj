Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports iTextSharp.text.pdf.draw
Imports System.Security.Cryptography

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

    Public Sub SendPath()
        Dim pdfPath As String = Application.StartupPath & "User Report.pdf"

        crystal_report(pdfPath)

    End Sub

    Public Sub crystal_report(filePath As String)

        Try
            Dim midnightBlue As New BaseColor(25, 25, 112)
            Dim titleFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18, 1, midnightBlue)
            Dim forStyling = FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.BLACK)
            Dim titles = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, 1, midnightBlue)
            Dim dates = FontFactory.GetFont(FontFactory.COURIER, 12, 1, midnightBlue)
            Dim companyContact = FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, midnightBlue)
            Dim crystalPDF As New Document()
            PdfWriter.GetInstance(crystalPDF, New FileStream(filePath, FileMode.Create))
            crystalPDF.Open()
            crystalPDF.Add(New Paragraph("USER REPORT", titleFont) With {.Alignment = Element.ALIGN_CENTER})

            Dim space As New Paragraph
            space.Add(" ")
            crystalPDF.Add(space)

            Dim reportDate As New Paragraph
            reportDate.Add(New Chunk("Report Date: " & DateTime.Now.ToString("MMMM dd, yyyy"), dates))
            reportDate.Alignment = Element.ALIGN_RIGHT

            crystalPDF.Add(reportDate)

            crystalPDF.Add(space)
            Dim line As New LineSeparator(1.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, -2)

            crystalPDF.Add(line)

            crystalPDF.Add(space)

            Dim quote As New Paragraph()
            quote.Add(New Paragraph("Welcome To SkyLink – Your Trusted Connection To the Digital World.", forStyling) With {.Alignment = Element.ALIGN_CENTER})
            crystalPDF.Add(quote)
            crystalPDF.Add(space)

            Dim proposition As New Paragraph()
            proposition.Add(New Paragraph("    As a valued subscriber, this report provides an overview of your current internet service" &
                                " details. SkyLink is committed to delivering fast, reliable, and secure internet connectivity" &
                                " to power your digital needs — from work and learning to entertainment and communication.") With {.Alignment = Element.ALIGN_JUSTIFIED})
            crystalPDF.Add(proposition)
            crystalPDF.Add(space)
            crystalPDF.Add(line)
            crystalPDF.Add(space)

            Dim subHeader As New Paragraph
            subHeader.Add(New Chunk("Personal Info", titles))
            crystalPDF.Add(subHeader)
            crystalPDF.Add(space)

            Dim subName As New Paragraph()
            subName.Add(New Chunk("Subscriber Name: ", forStyling))
            subName.Add(New Chunk(Session.fullName))
            crystalPDF.Add(subName)


            Dim address As New Paragraph()
            address.Add(New Chunk("Address: ", forStyling))
            address.Add(New Chunk(Session.address))
            crystalPDF.Add(address)

            Dim contactNumber As New Paragraph
            contactNumber.Add(New Chunk("Contact Number: ", forStyling))
            contactNumber.Add(New Chunk(Session.contactNumber))
            crystalPDF.Add(contactNumber)

            Dim emailing As New Paragraph
            emailing.Add(New Chunk("Email: ", forStyling))
            emailing.Add(New Chunk(Session.email))
            crystalPDF.Add(emailing)

            crystalPDF.Add(space)

            Dim subscriptionInfo As New Paragraph
            subscriptionInfo.Add(New Chunk("Subscription Info", titles))
            crystalPDF.Add(subscriptionInfo)
            crystalPDF.Add(space)

            Dim planName1 As New Paragraph()
            planName1.Add(New Chunk("Plan Name: ", forStyling))
            planName1.Add(New Chunk(Session.planName))
            crystalPDF.Add(planName1)

            Dim planType As New Paragraph()
            planType.Add(New Chunk("Plan Type: ", forStyling))
            planType.Add(New Chunk(Session.planType))
            crystalPDF.Add(planType)


            Dim planSpeed As New Paragraph()
            planSpeed.Add(New Chunk("Plan Speed: ", forStyling))
            planSpeed.Add(New Chunk(Session.planSpeed))
            crystalPDF.Add(planSpeed)


            Dim planDataCap As New Paragraph()
            planDataCap.Add(New Chunk("Data Capacity: ", forStyling))
            planDataCap.Add(Session.planDataCap)
            crystalPDF.Add(planDataCap)

            Dim planPrice As New Paragraph()
            planPrice.Add(New Chunk("Price: ", forStyling))
            planPrice.Add("Php " & Session.planPrice.ToString("f2"))
            crystalPDF.Add(planPrice)
            crystalPDF.Add(space)
            crystalPDF.Add(space)

            Dim paymentStatus As New Paragraph
            paymentStatus.Add(New Chunk("Payment Status", titles))
            crystalPDF.Add(paymentStatus)
            crystalPDF.Add(space)

            Dim table As New PdfPTable(4)

            table.WidthPercentage = 100
            table.SetWidths(New Single() {1.0F, 1.0F, 1.0F, 1.0F})


            Dim cell1 As New PdfPCell(New Phrase("Amount to Pay", forStyling))
            cell1.Border = Rectangle.BOX
            cell1.HorizontalAlignment = Element.ALIGN_CENTER
            table.AddCell(cell1)

            Dim cell2 As New PdfPCell(New Phrase("Money Given", forStyling))
            cell2.Border = Rectangle.BOX
            cell2.HorizontalAlignment = Element.ALIGN_CENTER
            table.AddCell(cell2)

            Dim cell3 As New PdfPCell(New Phrase("Change", forStyling))
            cell3.Border = Rectangle.BOX
            cell3.HorizontalAlignment = Element.ALIGN_CENTER
            table.AddCell(cell3)

            Dim cell4 As New PdfPCell(New Phrase("Payment Status", forStyling))
            cell4.Border = Rectangle.BOX
            cell4.HorizontalAlignment = Element.ALIGN_CENTER
            table.AddCell(cell4)

            Dim cell7 As New PdfPCell(New Phrase($"Php {Session.planPrice.ToString("F2")}", forStyling))
            cell7.Border = Rectangle.BOX
            cell7.HorizontalAlignment = Element.ALIGN_CENTER
            table.AddCell(cell7)

            Dim cell5 As New PdfPCell(New Phrase($"Php {Session.cashOnHand.ToString("F2")}", forStyling))
            cell5.Border = Rectangle.BOX
            cell5.HorizontalAlignment = Element.ALIGN_CENTER
            table.AddCell(cell5)

            Dim change As Decimal = Session.cashOnHand - Session.planPrice
            Dim cell6 As New PdfPCell(New Phrase($"Php {change.ToString("F2")}", forStyling))
            cell6.Border = Rectangle.BOX
            cell6.HorizontalAlignment = Element.ALIGN_CENTER
            table.AddCell(cell6)

            Dim cell8 As New PdfPCell(New Phrase("Paid", forStyling))
            cell8.Border = Rectangle.BOX
            cell8.HorizontalAlignment = Element.ALIGN_CENTER
            table.AddCell(cell8)


            crystalPDF.Add(table)

            crystalPDF.Add(space)
            crystalPDF.Add(space)
            crystalPDF.Add(space)
            crystalPDF.Add(line)
            Dim status1 As New Paragraph()
            status1.Add(New Paragraph($"Status: {Session.subStatus} - Please allow 1–2 hours after installation for full activation." &
                "Your service is currently in the setup phase and will be available shortly. Once activated, " &
                "your billing status and usage details will be displayed in this report, and a new email" &
                "containing the updated report will be sent to you automatically.", forStyling) With {.Alignment = Element.ALIGN_JUSTIFIED})


            crystalPDF.Add(status1)
            crystalPDF.Add(space)
            crystalPDF.Add(line)
            crystalPDF.Add(space)


            Dim footer As New Paragraph()
            footer.Add(New Paragraph("This report is confidential and intended solely" &
                        "for the subscriber. Do not share or reproduce without permission.", dates) With {.Alignment = Element.ALIGN_CENTER})
            crystalPDF.Add(footer)

            crystalPDF.Add(space)

            Dim companyContact1 As New Paragraph()
            companyContact1.Add(New Paragraph("SkyLink Internet Services | www.skylink.ph | support@skylink.ph | +63 912 345 6789", companyContact) With {.Alignment = Element.ALIGN_CENTER})
            crystalPDF.Add(companyContact1)

            crystalPDF.Close()

        Catch ex As Exception

            MessageBox.Show("Error generating PDF: " & ex.Message)

        End Try



    End Sub

    Private Sub subscribers_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        receivID = Session.SubscriberId
        TextBox1.Text = receivID
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
                            Session.subStatus = status
                            lblStatus.Text = "Status: " & status
                            lblPlan.Text = "Plan Name: " & planName
                            lblPrice.Text = "Price: " & planPrice.ToString("C")

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
                            lblAmountDue.Text = "Amount Due: " & billAmount.ToString("C")
                            lblBillMonth.Text = "Billing Month: " & billMonth
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading billing info: " & ex.Message)
            End Try
        End Using

        If Session.IsNewSubscription = True Then
            SendPath()
            Session.IsNewSubscription = False
        End If


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If increment < name1.Length Then
            lblUser.Text &= name1(increment)
            increment += 1
        Else
            Timer1.Stop()
        End If
    End Sub

    Private Sub ProductsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsToolStripMenuItem.Click
        Session.fromProduct = False
        Addon.Show()
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Cart.Show()
        Me.Close()
    End Sub

    Private Sub TicketsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TicketsToolStripMenuItem.Click
        Tickets.Show()
        Me.Close()
    End Sub
End Class
