Imports MySql.Data.MySqlClient
Imports Mysqlx.Crud

Public Class Addon
    Private addOnData As New List(Of Dictionary(Of String, Object))
    Private navigatingAway As Boolean = False
    Dim selectedQuantities(14) As Integer ' Holds quantity for all 15 addons (0-14 indexed)
    Dim imageRcv As Image = Session.planImage
    Dim planName As String = Session.planName
    Dim planType As String = Session.planType
    Dim planPrice As Decimal = Session.planPrice

    Dim txtValues(4) As Integer ' Current page display values
    Dim groupHardware() As TextBox
    Dim prices() As Integer
    Dim totalPriceHardware As Decimal = 0
    Dim total As Decimal = 0
    Dim page As Integer = 1
    Dim productGroup() As GroupBox
    Dim productNames() As String
    Dim priceGroup() As Label
    Dim cartItems As New List(Of cartItem)
    Dim addonIds() As Integer
    Dim addedItemsTotal As Decimal = 0
    Dim addonPrice1 As Decimal = 0
    Dim addonPrice2 As Decimal = 0
    Dim addonPrice3 As Decimal = 0
    Dim addonPrice4 As Decimal = 0
    Dim addonPrice5 As Decimal = 0
    Dim addonPrice6 As Decimal = 0
    Dim addonPrice7 As Decimal = 0
    Dim addonPrice8 As Decimal = 0
    Dim addonPrice9 As Decimal = 0
    Dim addonPrice10 As Decimal = 0
    Dim addonPrice11 As Decimal = 0
    Dim addonPrice12 As Decimal = 0
    Dim addonPrice13 As Decimal = 0
    Dim addonPrice14 As Decimal = 0
    Dim addonPrice15 As Decimal = 0
    Dim productName1 As String = ""
    Dim productName2 As String = ""
    Dim productName3 As String = ""
    Dim productName4 As String = ""
    Dim productName5 As String = ""
    Dim productName6 As String = ""
    Dim productName7 As String = ""
    Dim productName8 As String = ""
    Dim productName9 As String = ""
    Dim productName10 As String = ""
    Dim productName11 As String = ""
    Dim productName12 As String = ""
    Dim productName13 As String = ""
    Dim productName14 As String = ""
    Dim productName15 As String = ""
    Dim index As Integer = 1

    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"


    Public Class cartItem
        Public Property ProductName As String
        Public Property Quantity As Integer
        Public Property Price As Decimal
        Public Property AddonId As Integer
    End Class

    Public Sub pageHandling()
        priceGroup = {lblPrice1, lblPrice2, lblPrice3, lblPrice4, lblPrice5}
        productGroup = {gbxProduct1, gbxProduct2, gbxProduct3, gbxProduct4, gbxProduct5}
        groupHardware = {txtHardware1, txtHardware2, txtHardware3, txtHardware4, txtHardware5}

        Dim baseIndex As Integer = (page - 1) * 5

        gbxProduct5.Font = New Font(gbxProduct5.Font.FontFamily, 8, gbxProduct5.Font.Style)
        If page = 1 Then
            lblSpecific.Text = "Hardware"
            productNames = {productName1, productName2, productName3, productName4, productName5}
            prices = {addonPrice1, addonPrice2, addonPrice3, addonPrice4, addonPrice5}
            addonIds = {1, 2, 3, 4, 5}

            ' Set images for hardware page
            PictureBox2.Image = My.Resources._5gmodemrouter ' 5G Modem/Router -> PictureBox2
            PictureBox3.Image = My.Resources.wifiextender ' WiFi Extender -> PictureBox3  
            PictureBox4.Image = My.Resources.ethernetcable ' Ethernet Cable -> PictureBox4
            PictureBox5.Image = My.Resources.external_antenna ' External Antenna -> PictureBox5
            PictureBox6.Image = My.Resources.backupbatterypack ' Backup Battery Pack -> PictureBox6



        ElseIf page = 2 Then
            lblSpecific.Text = "Services"
            productNames = {productName6, productName7, productName8, productName9, productName10}
            prices = {addonPrice6, addonPrice7, addonPrice8, addonPrice9, addonPrice10}
            addonIds = {6, 7, 8, 9, 10}

            ' Set images for services page
            PictureBox2.Image = My.Resources.Installationservice ' Installation Service -> PictureBox2
            PictureBox3.Image = My.Resources.netflixsubscription ' Netflix -> PictureBox3
            PictureBox4.Image = My.Resources.landlineservice ' Landline Service -> PictureBox4
            PictureBox5.Image = My.Resources.homenetworksetup ' Home Network Setup -> PictureBox5
            PictureBox6.Image = My.Resources.premiumtechsupport ' Premium Tech Support -> PictureBox6



        ElseIf page = 3 Then
            lblSpecific.Text = "Plan Upgrades"
            productNames = {productName11, productName12, productName13, productName14, productName15}
            prices = {addonPrice11, addonPrice12, addonPrice13, addonPrice14, addonPrice15}
            addonIds = {11, 12, 13, 14, 15}

            ' Set images for plan upgrades page
            PictureBox2.Image = My.Resources.speedboost100mbps ' Speed Boost 100 Mbps -> PictureBox2
            PictureBox3.Image = My.Resources.speedboost200mbps ' Speed Boost 200 Mbps -> PictureBox3
            PictureBox4.Image = My.Resources.databoost100gb ' Data Allowance +100GB -> PictureBox4
            PictureBox5.Image = My.Resources.databoost50gb ' Data Allowance +50GB -> PictureBox5
            PictureBox6.Image = My.Resources.prioritysupportupgrade ' Priority Support Upgrade -> PictureBox6
            gbxProduct5.Font = New Font(gbxProduct5.Font.FontFamily, 7, gbxProduct5.Font.Style)

        Else
        End If

        ' Load saved quantities from selectedQuantities array into txtValues and display
        For i As Integer = 0 To 4
            txtValues(i) = selectedQuantities(baseIndex + i)
            priceGroup(i).Text = "₱" & prices(i).ToString("N2")
            productGroup(i).Text = productNames(i)
            groupHardware(i).Text = txtValues(i).ToString()
        Next

        ' Recalculate total based on all selected items
        RecalculateTotal()
    End Sub

    Private Sub SavePageQuantities()
        ' Save current page quantities to the selectedQuantities array
        Dim baseIndex As Integer = (page - 1) * 5
        For i As Integer = 0 To 4
            selectedQuantities(baseIndex + i) = txtValues(i)
        Next
    End Sub

    ' Modified RecalculateTotal method to handle existing subscribers properly
    Private Sub RecalculateTotal()



        ' Add all addon quantities across all pages
        addedItemsTotal = 0
        For pageNum As Integer = 1 To 3
            Dim baseIdx As Integer = (pageNum - 1) * 5
            For itemIdx As Integer = 0 To 4
                Dim quantity As Integer = selectedQuantities(baseIdx + itemIdx)
                If quantity > 0 Then
                    Dim itemPrice As Decimal = GetPriceForAddon(baseIdx + itemIdx + 1) ' addon IDs are 1-based
                    addedItemsTotal += itemPrice * quantity
                End If
            Next
        Next

        ' Calculate total based on user type and context


        Dim cartTotal As Decimal = GetCartTotal(Session.UserId)
        If Session.fromProduct = True Then
            ' From Products tab - show cart total from database

            total = cartTotal
        ElseIf Session.preSubscriber Then
            ' New subscription - include plan price + addons

            total = planPrice + cartTotal

        ElseIf Session.userRole = "Subscriber" Then

            total = cartTotal
            ' Existing subscriber viewing addons - start with 0 (no plan price)

        End If

        TextBox3.Text = "Php " & (planPrice + addedItemsTotal).ToString("F2")


    End Sub

    Private Function CheckStockAvailability() As List(Of String)
        Dim outOfStockItems As New List(Of String)

        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                ' Check stock ONLY for hardware items (addon IDs 1-5)
                For addonIndex As Integer = 0 To 4 ' 0-4 for hardware addons 1-5
                    Dim quantity As Integer = selectedQuantities(addonIndex)
                    If quantity > 0 Then
                        Dim actualAddonId As Integer = addonIndex + 1 ' addon_ids 1-5 (hardware only)

                        Dim stockQuery As String = "SELECT hs.quantity_available FROM hardware_stocks hs " &
                                             "WHERE hs.addon_id = @addonId"

                        Using cmd As New MySqlCommand(stockQuery, con)
                            cmd.Parameters.AddWithValue("@addonId", actualAddonId)

                            Dim result = cmd.ExecuteScalar()
                            If result IsNot Nothing Then
                                Dim availableStock As Integer = Convert.ToInt32(result)

                                ' Check if requested quantity exceeds available stock
                                If quantity > availableStock Then
                                    Dim productName As String = GetProductNameByAddonId(actualAddonId)
                                    If availableStock = 0 Then
                                        outOfStockItems.Add($"{productName} (Out of Stock)")
                                    Else
                                        outOfStockItems.Add($"{productName} (Only {availableStock} available, you selected {quantity})")
                                    End If
                                End If
                            Else
                                ' No stock record found - assume out of stock for hardware
                                Dim productName As String = GetProductNameByAddonId(actualAddonId)
                                outOfStockItems.Add($"{productName} (No stock information)")
                            End If
                        End Using
                    End If
                Next
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking stock: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return outOfStockItems
    End Function

    Private Function CheckStockForCart(addonId As Integer, requestedQuantity As Integer) As Boolean
        ' Only check stock for hardware items (addon IDs 1-5)
        If addonId < 1 Or addonId > 5 Then
            Return True ' Non-hardware items don't need stock checking
        End If

        Try
            Using con As New MySqlConnection(strCon)
                con.Open()

                ' Get current cart quantity for this item
                Dim cartQuery As String = "SELECT COALESCE(SUM(quantity), 0) FROM shopping_cart " &
                                    "WHERE customer_id = @customerId AND addon_id = @addonId"
                Dim currentCartQuantity As Integer = 0

                Using cartCmd As New MySqlCommand(cartQuery, con)
                    cartCmd.Parameters.AddWithValue("@customerId", Session.UserId)
                    cartCmd.Parameters.AddWithValue("@addonId", addonId)
                    Dim cartResult = cartCmd.ExecuteScalar()
                    If cartResult IsNot Nothing Then
                        currentCartQuantity = Convert.ToInt32(cartResult)
                    End If
                End Using

                ' Check available stock for hardware items
                Dim stockQuery As String = "SELECT hs.quantity_available FROM hardware_stocks hs " &
                                     "WHERE hs.addon_id = @addonId"

                Using cmd As New MySqlCommand(stockQuery, con)
                    cmd.Parameters.AddWithValue("@addonId", addonId)

                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing Then
                        Dim availableStock As Integer = Convert.ToInt32(result)
                        Dim totalRequested As Integer = currentCartQuantity + requestedQuantity

                        If totalRequested > availableStock Then
                            Dim productName As String = GetProductNameByAddonId(addonId)
                            If availableStock = 0 Then
                                MessageBox.Show($"{productName} is out of stock and cannot be added to cart.",
                                          "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Else
                                Dim canAdd As Integer = availableStock - currentCartQuantity
                                If canAdd <= 0 Then
                                    MessageBox.Show($"You already have the maximum available quantity of {productName} in your cart.",
                                              "Maximum Quantity Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Else
                                    MessageBox.Show($"{productName}: Only {availableStock} available in stock. " &
                                              $"You have {currentCartQuantity} in cart. You can add {canAdd} more.",
                                              "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If
                            End If
                            Return False
                        End If
                    Else
                        ' No stock record found for hardware item
                        Dim productName As String = GetProductNameByAddonId(addonId)
                        MessageBox.Show($"{productName} has no stock information and cannot be added to cart.",
                                  "No Stock Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error checking stock: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return True
    End Function

    Private Function GetPriceForAddon(addonId As Integer) As Decimal
        ' Get price based on addon ID (1-15)
        Select Case addonId
            Case 1 : Return addonPrice1  ' 5G Modem/Router
            Case 2 : Return addonPrice2  ' WiFi Extender
            Case 3 : Return addonPrice3   ' Ethernet Cable
            Case 4 : Return addonPrice4  ' External Antenna
            Case 5 : Return addonPrice5  ' Backup Battery Pack
            Case 6 : Return addonPrice6  ' Installation Service
            Case 7 : Return addonPrice7   ' Netflix Subscription
            Case 8 : Return addonPrice8   ' Landline Service
            Case 9 : Return addonPrice9  ' Home Network Setup
            Case 10 : Return addonPrice10  ' Premium Tech Support
            Case 11 : Return addonPrice11  ' Speed Boost 100 Mbps
            Case 12 : Return addonPrice12 ' Speed Boost 200 Mbps
            Case 13 : Return addonPrice13  ' Data Allowance +100GB
            Case 14 : Return addonPrice14  ' Data Allowance +50GB
            Case 15 : Return addonPrice15  ' Priority Support Upgrade
            Case Else : Return 0D
        End Select
    End Function

    Private Function GetCartTotal(customerId As Integer) As Decimal
        Dim total As Decimal = 0
        Using con As New MySqlConnection(DatabaseHelper.ConnectionString)
            con.Open()
            Dim query As String = "
            SELECT SUM(a.price * sc.quantity) AS total
            FROM shopping_cart sc
            JOIN addons a ON sc.addon_id = a.addon_id
            WHERE sc.customer_id = @customerId;"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@customerId", customerId)
                Dim result = cmd.ExecuteScalar()
                If result IsNot DBNull.Value Then
                    total = Convert.ToDecimal(result)
                End If
            End Using
        End Using
        Return total
    End Function

    Private Function AddToCartDatabase(customerId As Integer, addonId As Integer, quantity As Integer) As Boolean
        Try
            Using connection As New MySqlConnection(DatabaseHelper.ConnectionString)
                connection.Open()
                Dim query As String = "INSERT INTO shopping_cart (customer_id, addon_id, quantity) " &
                                         "VALUES (@customerId, @addonId, @quantity) " &
                                         "ON DUPLICATE KEY UPDATE quantity = quantity + @quantity, added_at = CURRENT_TIMESTAMP"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@customerId", customerId)
                    cmd.Parameters.AddWithValue("@addonId", addonId)
                    cmd.Parameters.AddWithValue("@quantity", quantity)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            RecalculateTotal()
            Return True
        Catch ex As Exception
            MessageBox.Show("Error adding to cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function PurchaseAddonsDirectly() As Boolean
        Dim success As Boolean = False
        Dim conStr As String = "server=localhost; userid=root; database=fdbmsproject"
        Dim con As New MySqlConnection(conStr)
        Dim trans As MySqlTransaction = Nothing

        Dim decDPayment As Decimal = 0

        Dim strDPayment As String = InputBox($"PleaseL enter your money: To Pay - {addedItemsTotal.ToString("f2")} ", "Payment", "0.00")



        If Decimal.TryParse(strDPayment, decDPayment) Then

            If decDPayment > addedItemsTotal Then

                Dim dChange As Decimal = decDPayment - addedItemsTotal

                MsgBox($"Thank your for purchasing! Here is your change: Php {dChange.ToString("f2")}")

            ElseIf decDPayment = addedItemsTotal Then

                MsgBox($"Thank your for purchasing!")

            ElseIf decDPayment < addedItemsTotal Then

                MsgBox("Insufficient money, please try again!")
                success = False
                Return success

            End If
            Try
                con.Open()
                trans = con.BeginTransaction()

                ' Process all selected items across all pages
                For addonIndex As Integer = 0 To 14 ' 0-14 for addons 1-15
                    Dim quantity As Integer = selectedQuantities(addonIndex)
                    If quantity > 0 Then
                        Dim actualAddonId As Integer = addonIndex + 1 ' addon_ids are 1-15

                        ' Get the addon price from database
                        Dim priceQuery As String = "SELECT price FROM addons WHERE addon_id = @addonId"
                        Dim priceCmd As New MySqlCommand(priceQuery, con, trans)
                        priceCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        Dim addonPrice As Decimal = Convert.ToDecimal(priceCmd.ExecuteScalar())

                        ' Insert addon purchase
                        Dim insertAddonQuery As String = "
                INSERT INTO customer_addons (customer_id, addon_id, quantity, purchase_date) 
                VALUES (@custId, @addonId, @qty, NOW())"
                        Dim addonCmd As New MySqlCommand(insertAddonQuery, con, trans)
                        addonCmd.Parameters.AddWithValue("@custId", Session.UserId)
                        addonCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        addonCmd.Parameters.AddWithValue("@qty", quantity)
                        addonCmd.ExecuteNonQuery()

                        Console.WriteLine($"Inserted addon: ID={actualAddonId}, Qty={quantity}, Price={addonPrice}")
                    End If
                Next

                trans.Commit()
                success = True
            Catch ex As Exception
                MessageBox.Show("Error during addon purchase: " & ex.Message)
                If trans IsNot Nothing Then
                    Try
                        trans.Rollback()
                    Catch rollEx As Exception
                        MessageBox.Show("Rollback failed: " & rollEx.Message)
                    End Try
                End If
            Finally
                con.Close()
            End Try
            Return success

        Else


            MsgBox("Please Enter A Valid Amount")
            success = False
            Return success

        End If




    End Function

    Private Sub ClearAllQuantities()


        ' Reset all quantities
        For i As Integer = 0 To 14
            selectedQuantities(i) = 0
        Next

        ' Reset current page display
        For i = 0 To 4
            txtValues(i) = 0
            groupHardware(i).Text = txtValues(i).ToString()
        Next

        ' Reset totals

        If Session.fromProduct = False Then

            total = planPrice

        Else
            total = 0
        End If
        ' txtTotal.Text = "Php " & total.ToString("F2")
        TextBox3.Text = "Php " & total.ToString("F2")
    End Sub

    Private Sub form_closing(sender As Object, e As EventArgs) Handles MyBase.FormClosing

        If Session.preSubscriber = True AndAlso Session.subscriberAccess = False Then
            Try
                Using con As New MySqlConnection(strCon)
                    con.Open()
                    ' Only clear hardware addons (ID 1-5) from cart
                    Dim deleteQuery As String = "DELETE FROM shopping_cart WHERE customer_id = @customerId AND addon_id BETWEEN 1 AND 15"
                    Using cmd As New MySqlCommand(deleteQuery, con)
                        cmd.Parameters.AddWithValue("@customerId", Session.UserId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                cartItems.Clear()

                Session.EndTransaction(False)

            Catch ex As Exception
                MessageBox.Show("Error clearing cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub Addon_Load(sender As Object, e As EventArgs) Handles MyBase.Load, Me.VisibleChanged

        Using conn As New MySqlConnection(strCon)

            conn.Open()

            Using getInfo As New MySqlCommand("SELECT * FROM addons LIMIT 15", conn)

                Using fetchInfo As MySqlDataReader = getInfo.ExecuteReader

                    While fetchInfo.Read

                        If index > 15 Then
                            Exit While
                        End If
                        Dim price As Decimal = fetchInfo.GetDecimal("price")
                        Dim productName As String = fetchInfo.GetString("item_name")
                        Select Case index
                            Case 1 : addonPrice1 = price : productName1 = productName
                            Case 2 : addonPrice2 = price : productName2 = productName
                            Case 3 : addonPrice3 = price : productName3 = productName
                            Case 4 : addonPrice4 = price : productName4 = productName
                            Case 5 : addonPrice5 = price : productName5 = productName
                            Case 6 : addonPrice6 = price : productName6 = productName
                            Case 7 : addonPrice7 = price : productName7 = productName
                            Case 8 : addonPrice8 = price : productName8 = productName
                            Case 9 : addonPrice9 = price : productName9 = productName
                            Case 10 : addonPrice10 = price : productName10 = productName
                            Case 11 : addonPrice11 = price : productName11 = productName
                            Case 12 : addonPrice12 = price : productName12 = productName
                            Case 13 : addonPrice13 = price : productName13 = productName
                            Case 14 : addonPrice14 = price : productName14 = productName
                            Case 15 : addonPrice15 = price : productName15 = productName

                        End Select

                        index += 1



                    End While


                End Using

            End Using


        End Using



        If Session.userRole <> "Subscriber" OrElse Session.subStatus Is DBNull.Value OrElse Session.subStatus.ToString() = "" Then
            HelpToolStripMenuItem.Visible = False

        Else

            SubscriptionToolStripMenuItem.Visible = False
        End If

        Session.CheckTransactionTimeout()

        '    If Not Session.IsTransactionActive AndAlso Session.IsNewSubscription Then
        ' Check if user is an existing subscriber
        '  If Session.SubscriberId > 0 Then
        ' Existing subscriber - allow them to view addons

        '  Else
        ' No active session and not a subscriber
        'MessageBox.Show("Your session has expired. Please select a plan again.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Subscription.Show()
        'Me.Close()
        'Return
        ' End If
        ' End If


        Dim cartTotal As Decimal = GetCartTotal(Session.UserId)
        If Session.fromProduct = True AndAlso Session.subscriberAccess = False Then
            pbxPlanImage.Visible = False
            btnPrevious.Visible = False
            lblName.Visible = False
            lblType.Visible = False
            lblPrice.Visible = False
            btnNext.Visible = False
            Dim skylinkProduct As New Label()
            total = cartTotal
            txtTotal.Text = "Php " & total.ToString("F2")
            skylinkProduct.Size = New Size(600, 100)
            skylinkProduct.Font = New Font("Tahoma", 25, FontStyle.Bold)
            skylinkProduct.Text = "SkyLink Products"
            skylinkProduct.BackColor = Color.Transparent
            skylinkProduct.Location = New Point(15, 50)
            skylinkProduct.ForeColor = Color.White
            Me.Controls.Add(skylinkProduct)
        ElseIf Session.preSubscriber = True AndAlso Session.subscriberAccess = False Then
            total = planPrice + cartTotal
            txtTotal.Text = "Php " & total.ToString("F2")
            TextBox3.Text = "Php " & total.ToString("F2")
            pbxPlanImage.Image = imageRcv
            lblName.Text = "Plan: " & planName
            lblType.Text = "Type: " & planType
            lblPrice.Text = "Price: " & planPrice.ToString("F2")

        ElseIf Session.subscriberAccess = True Then

            lblName.Visible = False
            lblType.Visible = False
            lblPrice.Visible = False
            pbxPlanImage.Visible = False
            btnPrevious.Visible = True
            btnNext.Visible = True
            TextBox3.Text = "Php " & total.ToString("f2")
            txtTotal.Text = "Php " & total.ToString("F2")
            Dim skylinkProduct As New Label()
            skylinkProduct.Size = New Size(600, 100)
            skylinkProduct.Font = New Font("Tahoma", 25, FontStyle.Bold)
            skylinkProduct.Text = "SkyLink Add-ons"
            skylinkProduct.Location = New Point(15, 50)
            skylinkProduct.BackColor = Color.Transparent
            skylinkProduct.ForeColor = Color.White
            Me.Controls.Add(skylinkProduct)

            ' ElseIf Session.userRole = "Subscriber" Then
            '  total = cartTotal
            '  btnPrevious.Visible = True
            ' btnNext.Visible = True
            ' txtTotal.Text = "Php " & total.ToString("F2")

        End If

        pageHandling()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd1.Click, btnAdd2.Click, btnAdd3.Click, btnAdd4.Click, btnAdd5.Click
        Dim index As Integer = CInt(DirectCast(sender, Button).Tag)

        txtValues(index) += 1
        groupHardware(index).Text = txtValues(index).ToString()

        ' Update the selectedQuantities array immediately
        Dim baseIndex As Integer = (page - 1) * 5
        selectedQuantities(baseIndex + index) = txtValues(index)

        ' Recalculate totals
        RecalculateTotal()

    End Sub

    Private Sub btnMinus1_Click(sender As Object, e As EventArgs) Handles btnMinus1.Click, btnMinus2.Click, btnMinus3.Click, btnMinus4.Click, btnMinus5.Click
        Dim index As Integer = CInt(DirectCast(sender, Button).Tag)

        If txtValues(index) <= 0 Then
            MessageBox.Show("Quantity cannot be less than 0", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        txtValues(index) -= 1
        groupHardware(index).Text = txtValues(index).ToString

        ' Update the selectedQuantities array immediately
        Dim baseIndex As Integer = (page - 1) * 5
        selectedQuantities(baseIndex + index) = txtValues(index)

        ' Recalculate totals
        RecalculateTotal()
    End Sub

    Private Function PurchaseAddonsForExistingSubscriber() As Boolean
        Dim success As Boolean = False
        Dim conStr As String = "server=localhost; userid=root; database=fdbmsproject"
        Dim con As New MySqlConnection(conStr)
        Dim trans As MySqlTransaction = Nothing

        Dim decPayment As Decimal = 0
        Dim strPayment As String = InputBox($"Please enter your money: To Pay - Php {total.ToString("f2")}", "Payment", "0.00")

        If Decimal.TryParse(strPayment, decPayment) Then
            If decPayment > addedItemsTotal Then
                Dim change As Decimal = decPayment - addedItemsTotal
                MsgBox($"Thank you for purchasing! Here is your change: Php {change.ToString("f2")}")
            ElseIf decPayment = addedItemsTotal Then
                MsgBox($"Thank you for purchasing!")
            ElseIf decPayment < addedItemsTotal Then
                MsgBox("Insufficient money, please try again!")
                Return False
            End If

            Try
                con.Open()
                trans = con.BeginTransaction()

                ' Process all selected addon items - only insert into customer_addons
                For addonIndex As Integer = 0 To 14 ' 0-14 for addons 1-15
                    Dim quantity As Integer = selectedQuantities(addonIndex)
                    If quantity > 0 Then
                        Dim actualAddonId As Integer = addonIndex + 1 ' addon_ids are 1-15

                        ' Insert addon purchase for existing subscriber
                        Dim insertAddonQuery As String = "
                INSERT INTO customer_addons (customer_id, addon_id, quantity, purchase_date) 
                VALUES (@custId, @addonId, @qty, NOW())"
                        Dim addonCmd As New MySqlCommand(insertAddonQuery, con, trans)
                        addonCmd.Parameters.AddWithValue("@custId", Session.UserId)
                        addonCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        addonCmd.Parameters.AddWithValue("@qty", quantity)
                        addonCmd.ExecuteNonQuery()

                        Console.WriteLine($"Inserted addon for existing subscriber: ID={actualAddonId}, Qty={quantity}")
                    End If
                Next

                trans.Commit()
                success = True

            Catch ex As Exception
                MessageBox.Show("Error during addon purchase: " & ex.Message)
                If trans IsNot Nothing Then
                    Try
                        trans.Rollback()
                    Catch rollEx As Exception
                        MessageBox.Show("Rollback failed: " & rollEx.Message)
                    End Try
                End If
            Finally
                con.Close()
            End Try

            Return success
        Else
            MsgBox("Please Enter A Valid Amount")
            Return False
        End If
    End Function

    Private Sub btnBuyNow_Click(sender As Object, e As EventArgs) Handles btnBuyNow.Click
        ' Save current page quantities first
        SavePageQuantities()

        ' Check if user is logged in
        If Session.UserId <= 0 Then
            MessageBox.Show("Please log in to make a purchase!", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if any items are selected across all pages
        Dim hasSelectedItems As Boolean = False
        For i As Integer = 0 To 14
            If selectedQuantities(i) > 0 Then
                hasSelectedItems = True
                Exit For
            End If
        Next

        If Not hasSelectedItems AndAlso Not Session.preSubscriber Then
            MessageBox.Show("Please select at least one item to purchase!", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Check stock availability for all selected items
        Dim outOfStockItems As List(Of String) = CheckStockAvailability()

        If outOfStockItems.Count > 0 Then
            Dim stockMessage As String = "The following items are out of stock or have insufficient quantity:" & vbCrLf & vbCrLf
            For Each item In outOfStockItems
                stockMessage += "• " & item & vbCrLf
            Next
            stockMessage += vbCrLf & "Please reduce the quantities or remove these items to proceed with purchase."

            MessageBox.Show(stockMessage, "Stock Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm purchase
        Dim confirmResult As DialogResult

        If Session.fromProduct = True Then
            confirmResult = MessageBox.Show($"Confirm purchase of items totaling Php {addedItemsTotal:F2}?",
                                          "Confirm Purchase",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question)
        Else
            confirmResult = MessageBox.Show($"Confirm purchase of items totaling Php {planPrice + addedItemsTotal:F2}?",
                                          "Confirm Purchase",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question)
        End If

        If confirmResult = DialogResult.Yes Then
            Dim purchaseSuccess As Boolean = False

            If Session.preSubscriber Then
                ' This is a new subscription purchase (plan + addons)
                purchaseSuccess = PurchaseSubscriptionWithAddons()

            ElseIf Session.fromProduct = True Then
                ' Buying addons directly (from Products tab)
                purchaseSuccess = PurchaseAddonsDirectly()
            Else
                ' This is an existing subscriber buying addons from the subscription flow
                purchaseSuccess = PurchaseAddonsForExistingSubscriber()
            End If

            If purchaseSuccess AndAlso Session.preSubscriber = True Then
                Session.userRole = "Subscriber"
                subscribers.Show()

                ClearAllQuantities()




                Me.Close()

            Else

                ' Stock will be automatically reduced by the database trigger
                ClearAllQuantities() ' Clear after successful purchase
            End If
        End If
    End Sub

    Private Function PurchaseSubscriptionWithAddons() As Boolean

        Dim success As Boolean = False
        Dim conStr As String = "server=localhost; userid=root; database=fdbmsproject"
        Dim con As New MySqlConnection(conStr)
        Dim trans As MySqlTransaction = Nothing

        total = planPrice + addedItemsTotal

        Dim decPayment As Decimal = 0

        Dim strPayment As String = InputBox($"Please enter your money: To Pay - Php {total.ToString("f2")} ", "Payment", "0.00")

        If Decimal.TryParse(strPayment, decPayment) Then

            If decPayment > total Then
                Dim change As Decimal = decPayment - total

                MsgBox($"Thank your for purchasing! Here is your change: Php {change.ToString("f2")}")
            ElseIf decPayment = total Then
                MsgBox($"Thank your for purchasing!")
            ElseIf decPayment < total Then
                MsgBox($"Insufficient money, please try again!")
                success = False

                Return success


            End If
            Try
                con.Open()
                trans = con.BeginTransaction()

                ' Insert into subscribers table
                Dim insertSubQuery As String = "
        INSERT INTO subscribers (customer_id, plan_id, subscription_date)
        VALUES (@customerId, @planId, NOW())"
                Dim subCmd As New MySqlCommand(insertSubQuery, con, trans)
                subCmd.Parameters.AddWithValue("@customerId", Session.UserId)
                subCmd.Parameters.AddWithValue("@planId", Session.PlanId)
                subCmd.ExecuteNonQuery()

                ' Now insert selected addons with purchase_date
                For addonIndex As Integer = 0 To 14
                    Dim quantity As Integer = selectedQuantities(addonIndex)
                    If quantity > 0 Then
                        Dim actualAddonId As Integer = addonIndex + 1

                        ' Check if this addon is recurring to handle it properly
                        Dim checkRecurringQuery As String = "SELECT is_recurring FROM addons WHERE addon_id = @addonId"
                        Dim checkCmd As New MySqlCommand(checkRecurringQuery, con, trans)
                        checkCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        Dim isRecurring As Boolean = Convert.ToBoolean(checkCmd.ExecuteScalar())

                        ' Insert addon purchase with purchase_date
                        Dim insertAddonQuery As String = "
                INSERT INTO customer_addons (customer_id, addon_id, quantity, purchase_date) 
                VALUES (@custId, @addonId, @qty, NOW())"
                        Dim addonCmd As New MySqlCommand(insertAddonQuery, con, trans)
                        addonCmd.Parameters.AddWithValue("@custId", Session.UserId)
                        addonCmd.Parameters.AddWithValue("@addonId", actualAddonId)
                        addonCmd.Parameters.AddWithValue("@qty", quantity)
                        addonCmd.ExecuteNonQuery()

                        Console.WriteLine($"Inserted addon: ID={actualAddonId}, Qty={quantity}, IsRecurring={isRecurring}")


                    End If
                Next

                'getting the subscriber id of newly subscribed customer for the ticket to be available immediately
                Using getSubId As New MySqlCommand("SELECT u.user_id, u.username, s.subscriber_id FROM subscribers s JOIN
                                                    users u ON u.user_id = s.customer_id WHERE u.user_id = @user_id", con)
                    getSubId.Parameters.AddWithValue("@user_id", Session.UserId)

                    Using fetchSubID As MySqlDataReader = getSubId.ExecuteReader

                        While fetchSubID.Read
                            Session.SubscriberId = fetchSubID.GetInt32("subscriber_id")
                        End While

                    End Using


                End Using

                Dim billingQuery As String = "INSERT INTO billing_records (subscriber_id, billing_month, total_amount, due_date, status) " &
            "VALUES (@subscriber, CURDATE(), @planAmount, DATE_ADD(CURDATE(), INTERVAL 1 MONTH), 'Paid')"
                Dim billingId As Integer = 0

                Using cmd As New MySqlCommand(billingQuery, con, trans)
                    cmd.Parameters.AddWithValue("@subscriber", Session.SubscriberId)
                    cmd.Parameters.AddWithValue("@planAmount", planPrice)
                    cmd.ExecuteNonQuery()
                    billingId = cmd.LastInsertedId
                End Using

                ' Insert payment record
                Dim paymentQuery As String = "INSERT INTO payments (billing_id, amount, payment_date) " &
            "VALUES (@billingId, @amount, CURDATE())"
                Using cmd As New MySqlCommand(paymentQuery, con, trans)
                    cmd.Parameters.AddWithValue("@billingId", billingId)
                    cmd.Parameters.AddWithValue("@amount", total)
                    cmd.ExecuteNonQuery()
                End Using




                trans.Commit()
                success = True

            Catch ex As Exception
                MessageBox.Show("Error during subscription purchase: " & ex.Message)
                If trans IsNot Nothing Then
                    Try
                        trans.Rollback()
                    Catch rollEx As Exception
                        MessageBox.Show("Rollback failed: " & rollEx.Message)
                    End Try
                End If
            Finally
                con.Close()
            End Try




            Session.IsNewSubscription = True
            Session.cashOnHand = planPrice
            Return success

        Else

            MsgBox("Please Enter A Valid Amount")
            addedItemsTotal = 0
            success = False
            Return success

        End If

    End Function


    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        SavePageQuantities()
        page += 1
        If page > 3 Then
            page = 1
        End If
        pageHandling()
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        SavePageQuantities()
        page -= 1
        If page < 1 Then
            page = 3
        End If
        pageHandling()
    End Sub

    Private Sub btnCart_Click_1(sender As Object, e As EventArgs) Handles btnCart.Click
        ' Save current page quantities first
        SavePageQuantities()

        Dim hasItems As Boolean = False
        Dim failedItems As New List(Of String)
        Dim successCount As Integer = 0

        ' Check if user is logged in
        If Session.UserId <= 0 Then
            MessageBox.Show("Please log in to add items to cart!", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Process all selected items across all pages
        For addonIndex As Integer = 0 To 14 ' 0-14 for addons 1-15
            Dim quantity As Integer = selectedQuantities(addonIndex)
            If quantity > 0 Then
                hasItems = True
                Dim actualAddonId As Integer = addonIndex + 1 ' addon_ids are 1-15

                ' Check stock before adding to cart
                If CheckStockForCart(actualAddonId, quantity) Then
                    If AddToCartDatabase(Session.UserId, actualAddonId, quantity) Then
                        successCount += 1
                    Else
                        ' Get product name for failed item
                        Dim productName As String = GetProductNameByAddonId(actualAddonId)
                        failedItems.Add(productName)
                    End If
                Else
                    ' Stock check failed - item not added
                    Dim productName As String = GetProductNameByAddonId(actualAddonId)
                    failedItems.Add(productName & " (Stock issue)")
                End If
            End If
        Next

        If hasItems Then
            If failedItems.Count = 0 Then
                MessageBox.Show($"All {successCount} items added to cart successfully!" & vbCrLf & "Added Php " & addedItemsTotal.ToString("F2") & " to your total.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                txtTotal.Text = "Php " & total.ToString("F2")
                ClearAllQuantities() ' Clear after successful cart addition
            Else
                If successCount > 0 Then
                    MessageBox.Show($"{successCount} items added successfully. Some items failed to add to cart: " & String.Join(", ", failedItems),
                           "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("No items were added to cart due to stock issues: " & String.Join(", ", failedItems),
                           "Add to Cart Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Else
            MessageBox.Show("No items selected to add to cart!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        addedItemsTotal = 0

    End Sub

    Private Function GetProductNameByAddonId(addonId As Integer) As String
        Select Case addonId
            Case 1 : Return productName1
            Case 2 : Return productName2
            Case 3 : Return productName3
            Case 4 : Return productName4
            Case 5 : Return productName5
            Case 6 : Return productName6
            Case 7 : Return productName7
            Case 8 : Return productName8
            Case 9 : Return productName9
            Case 10 : Return productName10
            Case 11 : Return productName11
            Case 12 : Return productName12
            Case 13 : Return productName13
            Case 14 : Return productName14
            Case 15 : Return productName15
            Case Else : Return "Unknown Product"
        End Select
    End Function

    Private Sub CartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles cartbutton.Click

        Cart.Show()
        Me.Hide()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubscriptionToolStripMenuItem.Click
        Subscription.Show()
        Me.Close()
    End Sub

    Private Sub HomeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem1.Click


        If Session.userRole = "Subscriber" Then

            subscribers.Show()
        ElseIf Session.userRole = "Customer" AndAlso Session.preSubscriber = True Then

            Session.preSubscriber = False
            Session.fromProduct = True
            Session.planName = ""
            Session.planPrice = 0
            Session.planType = ""

            Main.Show()


        Else
            Main.Show()
        End If

        Me.Close()

    End Sub

    Private Sub TicketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TicketToolStripMenuItem.Click
        Tickets.Show()
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub HelpToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click


    End Sub

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub
End Class

Public Class DatabaseHelper
    Public Shared ReadOnly Property ConnectionString As String
        Get
            Return "server=localhost; userid=root; database=fdbmsproject"
        End Get
    End Property
End Class