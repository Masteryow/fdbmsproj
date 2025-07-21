Imports MySql.Data.MySqlClient

Public Class Addon
    Private navigatingAway As Boolean = False


    Dim imageRcv As Image = Session.planImage
    Dim planName As String = Session.planName
    Dim planType As String = Session.planType
    Dim planPrice As Decimal = Session.planPrice
    Dim txtValues(4) As Integer
    Dim groupHardware() As TextBox
    Dim prices() As Integer
    Dim totalPriceHardware As Decimal = 0
    Dim total As Decimal = 0
    Dim page As Integer = 1
    Dim productGroup() As GroupBox
    Dim productNames() As String
    Dim priceGroup() As Label
    Dim pageQuantities(2, 4) As Integer
    Dim cartItems As New List(Of cartItem)
    Dim addonIds() As Integer
    Dim addedItemsTotal As Decimal = 0

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

        If page = 1 Then
            lblSpecific.Text = "Hardware"
            productNames = {"5G Modem/Router", "WiFi Extender", "Ethernet Cable (10m)", "External Antenna", "Backup Battey Pack"}
            prices = {7990, 2500, 500, 1200, 3500}
            addonIds = {1, 2, 3, 4, 5}

        ElseIf page = 2 Then
            lblSpecific.Text = "Services"
            productNames = {"Installation Service", "Netflix Subscription (Monthly)", "Landline Service (Monthly)",
                            "Home Network Setup", "Premium Tech Support"}
            prices = {1500, 549, 800, 2000, 500}
            addonIds = {6, 7, 8, 9, 10}

        ElseIf page = 3 Then
            lblSpecific.Text = "Plan Upgrades"
            productNames = {"Speed Boost 100 Mbps", "Speed Boost 200 Mbps", "Data Allowance +100GB",
                           "Data Allowance +50GB", "Priority Support Upgrade"}
            prices = {500, 1000, 500, 300, 400}
            addonIds = {11, 12, 13, 14, 15}
        End If

        For i = 0 To 4
            txtValues(i) = pageQuantities(page - 1, i)
            groupHardware(i).Text = txtValues(i).ToString()
            productGroup(i).Text = productNames(i)
            priceGroup(i).Text = "Price: " & prices(i).ToString("f2")
        Next
    End Sub


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
            Return True
        Catch ex As Exception
            MessageBox.Show("Error adding to cart: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub Addon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Session.CheckTransactionTimeout()

        If Not Session.IsTransactionActive Then
            MessageBox.Show("Your session has expired. Please select a plan again.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Subscription.Show()
            Me.Close()
            Return
        End If

        If Session.fromProduct Then
            pbxPlanImage.Visible = False
            Dim skylinkProduct As New Label()
            Dim cartTotal As Decimal = GetCartTotal(Session.UserId)
            total = cartTotal
            txtTotal.Text = "Php " & total.ToString("F2")
            skylinkProduct.Size = New Size(600, 100)
            skylinkProduct.Font = New Font("Tahoma", 25, FontStyle.Bold)
            skylinkProduct.Text = "SkyLink Products"
            skylinkProduct.Location = New Point(15, 50)
            skylinkProduct.ForeColor = Color.White
            Me.Controls.Add(skylinkProduct)
        Else
            total += planPrice
            txtTotal.Text = "Php " & total.ToString("F2")
            pbxPlanImage.Image = imageRcv
            lblName.Text = "Plan: " & planName
            lblType.Text = "Type: " & planType
            lblPrice.Text = "Price: " & planPrice.ToString("F2")
        End If

        pageHandling()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd1.Click, btnAdd2.Click, btnAdd3.Click, btnAdd4.Click, btnAdd5.Click

        Dim index As Integer = CInt(DirectCast(sender, Button).Tag)

        txtValues(index) += 1
        groupHardware(index).Text = txtValues(index).ToString()

        addedItemsTotal += prices(index)
        total += prices(index)
        txtTotal.Text = "Php " & total.ToString("F2")
        TextBox3.Text = addedItemsTotal
        ' Don't update total here - total should only show base plan price
    End Sub

    Private Sub btnMinus1_Click(sender As Object, e As EventArgs) Handles btnMinus1.Click, btnMinus2.Click, btnMinus3.Click, btnMinus4.Click, btnMinus5.Click

        Dim index As Integer = CInt(DirectCast(sender, Button).Tag)

        txtValues(index) -= 1

        If txtValues(index) < 0 Then
            txtValues(index) = 0
            MessageBox.Show("Quantity cannot be less than 0", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        groupHardware(index).Text = txtValues(index).ToString
        ' Don't update total here - total should only show base plan price

        total -= prices(index)
        addedItemsTotal -= prices(index)
        txtTotal.Text = "Php " & total.ToString("F2")
        TextBox3.Text = addedItemsTotal

    End Sub

    Private Sub btnBuyNow_Click(sender As Object, e As EventArgs) Handles btnBuyNow.Click
        ' Buy now functionality - total is already calculated
        ' Add your buy now logic here
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        For i = 0 To 4
            pageQuantities(page - 1, i) = txtValues(i)
        Next

        page += 1

        If page > 3 Then
            page = 1
        End If
        pageHandling()

    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click

        For i = 0 To 4
            pageQuantities(page - 1, i) = txtValues(i)
        Next

        page -= 1

        If page < 1 Then
            page = 3
        End If
        pageHandling()

    End Sub

    Private Sub btnCart_Click_1(sender As Object, e As EventArgs) Handles btnCart.Click

        Dim hasItems As Boolean = False
        Dim failedItems As New List(Of String)


        ' Check if user is logged in
        If Session.UserId <= 0 Then
            MessageBox.Show("Please log in to add items to cart!", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Add items to database cart and calculate total of added items
        For i = 0 To 4
            If txtValues(i) > 0 Then
                hasItems = True

                ' Add to database cart
                If AddToCartDatabase(Session.UserId, addonIds(i), txtValues(i)) Then

                Else
                    failedItems.Add(productNames(i))
                End If
            End If
        Next

        If hasItems Then
            If failedItems.Count = 0 Then

                ' Reset quantities across ALL pages, not just current page
                For pageNum = 0 To 2
                    For itemIndex = 0 To 4
                        pageQuantities(pageNum, itemIndex) = 0
                    Next
                Next

                ' Reset current page display
                For i = 0 To 4
                    txtValues(i) = 0
                    groupHardware(i).Text = txtValues(i).ToString()
                Next

                ' Update cart count display if you have one
                ' lblCartCount.Text = GetCartCount(Session.UserId).ToString()

                MessageBox.Show("Items added to cart successfully!" & vbCrLf & "Added Php " & addedItemsTotal.ToString("F2") & " to your total.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Some items failed to add to cart: " & String.Join(", ", failedItems),
                       "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("No items selected to add to cart!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub CartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles cartbutton.Click


        ' ADDED: Check transaction timeout before navigation
        Session.CheckTransactionTimeout()

        If Not Session.IsTransactionActive Then
            MessageBox.Show("Your session has expired. Please select a plan again.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Subscription.Show()
            Me.Close()
            Return
        End If

        ' If session is active, proceed to show the cart
        Cart.Show()
        Me.Close()
    End Sub


End Class

' You'll also need a DatabaseHelper class like this:
Public Class DatabaseHelper
    Public Shared ReadOnly Property ConnectionString As String
        Get
            Return "server=localhost; userid=root; database=fdbmsproject"
        End Get
    End Property
End Class