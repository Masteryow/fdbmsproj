Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports iTextSharp.text.pdf.draw
Imports System.Security.Cryptography
Imports System.Net
Imports System.Net.Mail

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


    Public Sub pdfReport(reportPath As String)

        Dim sendPdf As New MailMessage
        sendPdf.From = New MailAddress("skylink.solutions2@gmail.com")
        sendPdf.To.Add(Session.email)
        sendPdf.Subject = "User Report"
        sendPdf.Body = $"Dear {Session.fullName}," & vbCrLf & vbCrLf &
"Thank you for choosing SkyLink as your internet service provider. We are delighted to welcome you to a community that values seamless connectivity, modern technology, and customer-first service. Your account has been successfully registered in our system, and we are preparing everything to bring you online as soon as possible." & vbCrLf & vbCrLf &
"At this time, your internet service status is marked as PENDING ACTIVATION. This status means that some final steps are still underway before your connection becomes fully active. This could involve technical installation, final account verification, address validation, or activation scheduling. Rest assured that our team is closely monitoring your account and working diligently to ensure everything is processed smoothly and efficiently." & vbCrLf & vbCrLf &
"Attached to this email is your official User Report. It contains helpful reference information related to your account — including plan selection, and SkyLink’s support contact details. We encourage you to keep this document on file for your future reference." & vbCrLf & vbCrLf &
"Our goal is to deliver not just internet access, but a reliable, long-term digital experience you can depend on for work, entertainment, education, and staying connected to the world. SkyLink customers enjoy continuous infrastructure upgrades, rapid support response times, and growing service coverage in both rural and urban areas." & vbCrLf & vbCrLf &
"Once your service has been activated, you will receive a follow-up email confirming that you are now connected and ready to go. That message will also include helpful information for getting started with your connection and managing your account online." & vbCrLf & vbCrLf &
"If you have any questions regarding your activation status, want to confirm a schedule, or simply wish to speak with one of our representatives, our support team is available 24/7 via phone, chat, or email. Your satisfaction and convenience are our top priorities." & vbCrLf & vbCrLf &
"We truly appreciate your patience as we complete your setup. Thank you for trusting SkyLink to provide the connectivity you need in today’s fast-moving digital world. We’re honored to serve you — and we look forward to getting you connected very soon." & vbCrLf & vbCrLf &
"Best regards," & vbCrLf &
"The SkyLink Customer Success Team"


        If File.Exists(reportPath) Then
            sendPdf.Attachments.Add(New Attachment(reportPath))
        End If
        Dim smtp As New SmtpClient("smtp.gmail.com", 587)
        smtp.Credentials = New NetworkCredential("skylink.solutions2@gmail.com", "hptu svfb rfas uttx")
        smtp.EnableSsl = True

        Try
            smtp.Send(sendPdf)

        Catch ex As Exception
            MessageBox.Show("Failed to send email: " & ex.Message)
        End Try


    End Sub
    Public Sub SendPath()
        Dim pdfPath As String = Path.Combine(Application.StartupPath, "User Report" & Guid.NewGuid().ToString() & ".pdf")

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

            Dim planPrice1 As New Paragraph()
            planPrice1.Add(New Chunk("Price: ", forStyling))
            planPrice1.Add("Php " & Session.planPrice.ToString("f2"))
            crystalPDF.Add(planPrice1)
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
                        " for the subscriber. Do not share or reproduce without permission.", dates) With {.Alignment = Element.ALIGN_CENTER})
            crystalPDF.Add(footer)

            crystalPDF.Add(space)

            Dim companyContact1 As New Paragraph()
            companyContact1.Add(New Paragraph("SkyLink Internet Services | www.skylink.ph | support@skylink.ph | +63 912 345 6789", companyContact) With {.Alignment = Element.ALIGN_CENTER})
            crystalPDF.Add(companyContact1)

            crystalPDF.Close()
            pdfReport(filePath)

        Catch ex As Exception

            MessageBox.Show("Error generating PDF: " & ex.Message)

        End Try



    End Sub

    Private Sub subscribers_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Session.subscriberAccess = True
        Session.preSubscriber = False


        receivID = Session.SubscriberId
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

        ' === Load billing record for current month ===
        LoadBillingInfo()

        If Session.IsNewSubscription = True Then
            SendPath()
            Session.IsNewSubscription = False
            Session.planPrice = 0
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
        Session.fromProduct = True
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

    Private Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        ' Check if already paid first
        If GetPaymentStatus() = "PAID" Then
            MessageBox.Show("This month's bill has already been paid!", "Already Paid", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Check if there's a billing record to pay
        If billingId = 0 Then
            MessageBox.Show("No billing record found for this month.", "No Bill Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Process payment
        ProcessPayment()
    End Sub

    Private Sub ProcessPayment()
        Using con As New MySqlConnection(strCon)
            Try
                con.Open()
                Dim transaction As MySqlTransaction = con.BeginTransaction()

                Try
                    ' Get the amount to pay
                    Dim amountToPay As Decimal = If(billAmount > 0, billAmount, planPrice)

                    ' Ask for payment amount
                    Dim paymentInput As String = InputBox($"Bill Amount: Php {amountToPay.ToString("F2")}{vbNewLine}Please enter your payment amount:", "Payment Required")

                    If String.IsNullOrEmpty(paymentInput) Then
                        transaction.Rollback()
                        Return ' User cancelled
                    End If

                    Dim paymentAmount As Decimal = 0
                    If Not Decimal.TryParse(paymentInput, paymentAmount) Then
                        MessageBox.Show("Please enter a valid payment amount.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        transaction.Rollback()
                        Return
                    End If

                    If paymentAmount < amountToPay Then
                        MessageBox.Show($"Insufficient payment! You need to pay at least Php {amountToPay.ToString("F2")}", "Insufficient Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        transaction.Rollback()
                        Return
                    End If

                    ' Store payment info in Session for the crystal report
                    Session.cashOnHand = paymentAmount

                    ' Insert payment record
                    Using paymentCmd As New MySqlCommand("INSERT INTO payments (billing_id, amount, payment_date) VALUES (@bid, @amount, @payDate)", con)
                        paymentCmd.Transaction = transaction
                        paymentCmd.Parameters.AddWithValue("@bid", billingId)
                        paymentCmd.Parameters.AddWithValue("@amount", paymentAmount)
                        paymentCmd.Parameters.AddWithValue("@payDate", Date.Today)
                        paymentCmd.ExecuteNonQuery()
                    End Using

                    ' The trigger will automatically update the billing_records status to 'Paid'
                    ' and update the user role to 'Subscriber' if needed

                    transaction.Commit()

                    ' Calculate change
                    Dim change As Decimal = paymentAmount - amountToPay
                    If change > 0 Then
                        MessageBox.Show($"Payment successful!{vbNewLine}Amount Paid: Php {paymentAmount.ToString("F2")}{vbNewLine}Change: Php {change.ToString("F2")}", "Payment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show($"Payment successful!{vbNewLine}Amount Paid: Php {paymentAmount.ToString("F2")}", "Payment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    ' Refresh the billing information
                    LoadBillingInfo()

                    ' Generate and send crystal report using the same path pattern as SendPath()
                    SendPaymentPath()

                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show("Payment failed: " & ex.Message, "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Catch ex As Exception
                MessageBox.Show("Database connection error: " & ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub LoadBillingInfo()
        ' === Load billing record for current month ===
        Using con As New MySqlConnection(strCon)
            Try
                con.Open()
                ' Get the current month in the format your database uses (first day of month)
                Dim currentMonthStart As String = DateTime.Now.ToString("yyyy-MM") & "-01"
                Dim query As String = "
                SELECT billing_id, total_amount, billing_month, status 
                FROM billing_records 
                WHERE subscriber_id = @id AND DATE_FORMAT(billing_month, '%Y-%m-01') = @month
                ORDER BY billing_id DESC
                LIMIT 1
            "
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", receivID)
                    cmd.Parameters.AddWithValue("@month", currentMonthStart)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            billingId = Convert.ToInt32(reader("billing_id"))
                            billAmount = Convert.ToDecimal(reader("total_amount"))
                            Dim billingMonthDate As DateTime = Convert.ToDateTime(reader("billing_month"))
                            billMonth = billingMonthDate.ToString("MMMM yyyy")
                            Dim billingStatus As String = reader("status").ToString()

                            lblAmountDue.Text = "Amount Due: " & billAmount.ToString("C")
                            lblBillMonth.Text = "Billing Month: " & billMonth

                            ' Update status display if needed
                            If billingStatus <> status Then
                                lblStatus.Text = "Status: " & status & " | Bill: " & billingStatus
                            End If
                        Else
                            ' No billing record found, use plan price as default
                            billAmount = planPrice
                            billMonth = DateTime.Now.ToString("MMMM yyyy")
                            lblAmountDue.Text = "Amount Due: " & billAmount.ToString("C")
                            lblBillMonth.Text = "Billing Month: " & billMonth
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading billing info: " & ex.Message)
            End Try
        End Using
    End Sub

    Public Sub SendPaymentPath()
        ' Use the same file path pattern as SendPath() with GUID for unique naming
        Dim pdfPath As String = Path.Combine(Application.StartupPath, "User Report" & Guid.NewGuid().ToString() & ".pdf")

        ' Generate the billing PDF (the one from pay now)
        CreateBillingPDF(pdfPath)
    End Sub


    Public Sub forBilling(billingReportPath As String)

        Dim billingPDF As New MailMessage
        billingPDF.From = New MailAddress("skylink.solutions2@gmail.com")
        billingPDF.To.Add(Session.email)
        billingPDF.Subject = "Your Monthly SkyLink Billing Statement"
        billingPDF.Body = "Dear " & Session.fullName & "," & vbCrLf & vbCrLf &
"We hope you're enjoying reliable and high-speed internet with SkyLink." & vbCrLf & vbCrLf &
"Attached is your monthly billing statement for the period of " & billMonth & ". It contains a summary of your service plan, billing amount, payment status, and the due date. Please review the details carefully." & vbCrLf & vbCrLf &
"If payment is still pending, we kindly encourage you to settle your balance before the due date to avoid any service interruptions." & vbCrLf & vbCrLf &
"You may pay online, via authorized payment centers, or by visiting our local office. For any questions or concerns regarding your statement, feel free to reach out to our support team." & vbCrLf & vbCrLf &
"Thank you for choosing SkyLink Internet Services. We value your trust and commitment to staying connected." & vbCrLf & vbCrLf &
"Best regards," & vbCrLf &
"SkyLink Billing Department"


        If File.Exists(billingReportPath) Then
            billingPDF.Attachments.Add(New Attachment(billingReportPath))
        End If
        Dim smtp As New SmtpClient("smtp.gmail.com", 587)
        smtp.Credentials = New NetworkCredential("skylink.solutions2@gmail.com", "hptu svfb rfas uttx")
        smtp.EnableSsl = True

        Try
            smtp.Send(billingPDF)

        Catch ex As Exception
            MessageBox.Show("Failed to send email: " & ex.Message)
        End Try


    End Sub


    Public Sub CreateBillingPDF(billingPath As String)
        Try
            ' Define colors and fonts
            Dim midnightBlue As New BaseColor(25, 25, 112)
            Dim darkRed As New BaseColor(139, 0, 0)
            Dim darkGreen As New BaseColor(0, 100, 0)
            Dim titleFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18, 1, midnightBlue)
            Dim forStyling = FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.BLACK)
            Dim titles = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, 1, midnightBlue)
            Dim dates = FontFactory.GetFont(FontFactory.COURIER, 12, 1, midnightBlue)
            Dim companyContact = FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, midnightBlue)

            ' Create PDF document
            Dim billingPDF As New Document()
            PdfWriter.GetInstance(billingPDF, New FileStream(billingPath, FileMode.Create))
            billingPDF.Open()

            ' Title
            billingPDF.Add(New Paragraph("BILLING REPORT", titleFont) With {.Alignment = Element.ALIGN_CENTER})

            ' Add space
            Dim space As New Paragraph
            space.Add(" ")
            billingPDF.Add(space)

            ' Report date
            Dim reportDate As New Paragraph
            reportDate.Add(New Chunk("Report Generated: " & DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt"), dates))
            reportDate.Alignment = Element.ALIGN_RIGHT
            billingPDF.Add(reportDate)

            billingPDF.Add(space)

            ' Line separator
            Dim line As New LineSeparator(1.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, -2)
            billingPDF.Add(line)
            billingPDF.Add(space)

            ' Company intro
            Dim quote As New Paragraph()
            quote.Add(New Paragraph("SkyLink Internet Services - Monthly Billing Statement", forStyling) With {.Alignment = Element.ALIGN_CENTER})
            billingPDF.Add(quote)
            billingPDF.Add(space)

            ' Customer Information Section
            Dim customerHeader As New Paragraph
            customerHeader.Add(New Chunk("Customer Information", titles))
            billingPDF.Add(customerHeader)
            billingPDF.Add(space)

            ' Customer details
            Dim custName As New Paragraph()
            custName.Add(New Chunk("Customer Name: ", forStyling))
            custName.Add(New Chunk(Session.fullName))
            billingPDF.Add(custName)

            Dim custId As New Paragraph()
            custId.Add(New Chunk("Customer ID: ", forStyling))
            custId.Add(New Chunk(Session.SubscriberId.ToString()))
            billingPDF.Add(custId)

            Dim custAddress As New Paragraph()
            custAddress.Add(New Chunk("Service Address: ", forStyling))
            custAddress.Add(New Chunk(Session.address))
            billingPDF.Add(custAddress)

            billingPDF.Add(space)
            billingPDF.Add(line)
            billingPDF.Add(space)

            ' Billing Information Section
            Dim billingHeader As New Paragraph
            billingHeader.Add(New Chunk("Billing Information", titles))
            billingPDF.Add(billingHeader)
            billingPDF.Add(space)

            ' Service details
            Dim servicePlan As New Paragraph()
            servicePlan.Add(New Chunk("Service Plan: ", forStyling))
            servicePlan.Add(New Chunk(planName))
            billingPDF.Add(servicePlan)

            Dim monthlyRate As New Paragraph()
            monthlyRate.Add(New Chunk("Monthly Rate: ", forStyling))
            monthlyRate.Add(New Chunk("Php " & planPrice.ToString("F2")))
            billingPDF.Add(monthlyRate)

            ' Billing month
            Dim billingMonthPara As New Paragraph()
            billingMonthPara.Add(New Chunk("Billing Month: ", forStyling))
            If String.IsNullOrEmpty(billMonth) Then
                billingMonthPara.Add(New Chunk(DateTime.Now.ToString("MMMM yyyy")))
            Else
                billingMonthPara.Add(New Chunk(billMonth))
            End If
            billingPDF.Add(billingMonthPara)

            billingPDF.Add(space)

            ' Payment Status Table
            Dim paymentHeader As New Paragraph
            paymentHeader.Add(New Chunk("Payment Details", titles))
            billingPDF.Add(paymentHeader)
            billingPDF.Add(space)

            ' Create payment status table
            Dim paymentTable As New PdfPTable(2)
            paymentTable.WidthPercentage = 100
            paymentTable.SetWidths(New Single() {1.0F, 1.0F})

            ' Table headers
            Dim headerCell1 As New PdfPCell(New Phrase("Description", forStyling))
            headerCell1.Border = Rectangle.BOX
            headerCell1.HorizontalAlignment = Element.ALIGN_CENTER
            headerCell1.BackgroundColor = BaseColor.LIGHT_GRAY
            paymentTable.AddCell(headerCell1)

            Dim headerCell2 As New PdfPCell(New Phrase("Amount/Status", forStyling))
            headerCell2.Border = Rectangle.BOX
            headerCell2.HorizontalAlignment = Element.ALIGN_CENTER
            headerCell2.BackgroundColor = BaseColor.LIGHT_GRAY
            paymentTable.AddCell(headerCell2)

            ' Amount Due row
            Dim amountDueCell As New PdfPCell(New Phrase("Amount Due", forStyling))
            amountDueCell.Border = Rectangle.BOX
            amountDueCell.HorizontalAlignment = Element.ALIGN_LEFT
            paymentTable.AddCell(amountDueCell)

            Dim amountValueCell As New PdfPCell(New Phrase("Php " & If(billAmount > 0, billAmount.ToString("F2"), planPrice.ToString("F2")), forStyling))
            amountValueCell.Border = Rectangle.BOX
            amountValueCell.HorizontalAlignment = Element.ALIGN_RIGHT
            paymentTable.AddCell(amountValueCell)

            ' Payment Status row
            Dim statusCell As New PdfPCell(New Phrase("Payment Status", forStyling))
            statusCell.Border = Rectangle.BOX
            statusCell.HorizontalAlignment = Element.ALIGN_LEFT
            paymentTable.AddCell(statusCell)

            ' Determine payment status and color
            Dim paymentStatus As String = GetPaymentStatus()
            Dim statusFont As Font = forStyling

            Select Case paymentStatus.ToUpper()
                Case "PAID"
                    statusFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, darkGreen)
                Case "OVERDUE"
                    statusFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, darkRed)
                Case "UNPAID"
                    statusFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, 1, BaseColor.ORANGE)
            End Select

            Dim paymentStatusCell As New PdfPCell(New Phrase(paymentStatus, statusFont))
            paymentStatusCell.Border = Rectangle.BOX
            paymentStatusCell.HorizontalAlignment = Element.ALIGN_RIGHT
            paymentTable.AddCell(paymentStatusCell)

            ' Due Date row
            Dim dueDateCell As New PdfPCell(New Phrase("Due Date", forStyling))
            dueDateCell.Border = Rectangle.BOX
            dueDateCell.HorizontalAlignment = Element.ALIGN_LEFT
            paymentTable.AddCell(dueDateCell)

            Dim dueDate As String = GetDueDate()
            Dim dueDateValueCell As New PdfPCell(New Phrase(dueDate, forStyling))
            dueDateValueCell.Border = Rectangle.BOX
            dueDateValueCell.HorizontalAlignment = Element.ALIGN_RIGHT
            paymentTable.AddCell(dueDateValueCell)

            billingPDF.Add(paymentTable)
            billingPDF.Add(space)
            billingPDF.Add(space)

            ' Payment Instructions
            If paymentStatus <> "PAID" Then
                billingPDF.Add(line)
                billingPDF.Add(space)

                Dim paymentInstructions As New Paragraph
                paymentInstructions.Add(New Chunk("Payment Instructions", titles))
                billingPDF.Add(paymentInstructions)
                billingPDF.Add(space)

                Dim instructions As New Paragraph()
                instructions.Add(New Paragraph("Please ensure payment is made by the due date to avoid service interruption. " &
                "You can pay through our online portal, authorized payment centers, or visit our office directly. " &
                "For questions regarding your bill, please contact our customer service.", forStyling) With {.Alignment = Element.ALIGN_JUSTIFIED})
                billingPDF.Add(instructions)
                billingPDF.Add(space)
            End If

            billingPDF.Add(line)
            billingPDF.Add(space)

            ' Footer
            Dim footer As New Paragraph()
            footer.Add(New Paragraph("This is a computer-generated billing statement. " &
                    "Keep this document for your records.", dates) With {.Alignment = Element.ALIGN_CENTER})
            billingPDF.Add(footer)

            billingPDF.Add(space)

            Dim companyContact1 As New Paragraph()
            companyContact1.Add(New Paragraph("SkyLink Internet Services | www.skylink.ph | billing@skylink.ph | +63 912 345 6789", companyContact) With {.Alignment = Element.ALIGN_CENTER})
            billingPDF.Add(companyContact1)

            billingPDF.Close()
            forBilling(billingPath)
        Catch ex As Exception
            MessageBox.Show("Error generating billing PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function GetPaymentStatus() As String
        ' This function determines the payment status based on your database structure

        Using con As New MySqlConnection(strCon)
            Try
                con.Open()
                ' Get the current month in the format your database uses (first day of month)
                Dim currentMonthStart As String = DateTime.Now.ToString("yyyy-MM") & "-01"
                Dim query As String = "
                SELECT status, due_date 
                FROM billing_records 
                WHERE subscriber_id = @id AND DATE_FORMAT(billing_month, '%Y-%m-01') = @month
                ORDER BY billing_id DESC
                LIMIT 1
            "
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", receivID)
                    cmd.Parameters.AddWithValue("@month", currentMonthStart)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim paymentStatus As String = reader("status").ToString()
                            Dim dueDate As DateTime = Convert.ToDateTime(reader("due_date"))

                            ' Return the status from database (triggers handle overdue automatically)
                            Return paymentStatus.ToUpper()
                        Else
                            ' No billing record found, assume unpaid for current month
                            Return "UNPAID"
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error getting payment status: " & ex.Message)
                Return "UNKNOWN"
            End Try
        End Using
    End Function

    Private Function GetDueDate() As String
        Using con As New MySqlConnection(strCon)
            Try
                con.Open()
                ' Get the current month in the format your database uses (first day of month)
                Dim currentMonthStart As String = DateTime.Now.ToString("yyyy-MM") & "-01"
                Dim query As String = "
                SELECT due_date 
                FROM billing_records 
                WHERE subscriber_id = @id AND DATE_FORMAT(billing_month, '%Y-%m-01') = @month
                ORDER BY billing_id DESC
                LIMIT 1
            "
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", receivID)
                    cmd.Parameters.AddWithValue("@month", currentMonthStart)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim dueDate As DateTime = Convert.ToDateTime(reader("due_date"))
                            Return dueDate.ToString("MMMM dd, yyyy")
                        Else
                            ' If no billing record, set due date to end of current month
                            Dim endOfMonth As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                            Return endOfMonth.ToString("MMMM dd, yyyy")
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error getting due date: " & ex.Message)
                Return "N/A"
            End Try
        End Using
    End Function

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub
End Class
