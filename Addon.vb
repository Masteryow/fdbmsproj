Public Class Addon

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

    Public Class cartItem

        Public Property ProductName As String
        Public Property Quantity As Integer
        Public Property Price As Decimal

    End Class

    Public Sub pageHandling()

        priceGroup = {lblPrice1, lblPrice2, lblPrice3, lblPrice4, lblPrice5}
        productGroup = {gbxProduct1, gbxProduct2, gbxProduct3, gbxProduct4, gbxProduct5}
        groupHardware = {txtHardware1, txtHardware2, txtHardware3, txtHardware4, txtHardware5}

        If page = 1 Then

            lblSpecific.Text = "Hardware"
            productNames = {"5G Modem/Router", "WiFi Extender", "Ethernet Cable (10m)", "External Antenna", "Backup Battey Pack"}
            prices = {7990, 2500, 500, 1200, 3500}

        ElseIf page = 2 Then

            lblSpecific.Text = "Services"
            productNames = {"Installation Service", "Netflix Subscription (Monthly)", "Landline Service (Monthly)",
                            "Home Network Setup", "Premium Tech Support"}
            prices = {1500, 549, 800, 2000, 500}

        ElseIf page = 3 Then

            lblSpecific.Text = "Plan Upgrades"
            productNames = {"Speed Boost 100 Mbps", "Speed Boost 200 Mbps", "Data Allowance +100GB",
                           "Data Allowance +50GB", "Priority Support Upgrade"}
            prices = {500, 1000, 500, 300, 400}


        End If

        For i = 0 To 4
            txtValues(i) = pageQuantities(page - 1, i)
            groupHardware(i).Text = txtValues(i).ToString()
            productGroup(i).Text = productNames(i)
            priceGroup(i).Text = "Price: " & prices(i).ToString("f2")


        Next

    End Sub

    Private Sub Addon_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        If Session.fromProduct = True Then
            pbxPlanImage.Visible = False
            Dim skylinkProduct As New Label()

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
            lblPrice.Text = "Price: " & planPrice




        End If

        pageHandling()

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd1.Click, btnAdd2.Click, btnAdd3.Click, btnAdd4.Click, btnAdd5.Click

        Dim index As Integer = CInt(DirectCast(sender, Button).Tag)

        txtValues(index) += 1

        groupHardware(index).Text = txtValues(index).ToString()

        total += prices(index)

        txtTotal.Text = "Php " & total.ToString("f2")
    End Sub

    Private Sub btnMinus1_Click(sender As Object, e As EventArgs) Handles btnMinus1.Click, btnMinus2.Click, btnMinus3.Click, btnMinus4.Click, btnMinus5.Click

        Dim index As Integer = CInt(DirectCast(sender, Button).Tag)

        txtValues(index) -= 1

        If txtValues(index) < 0 Then
            txtValues(index) = 0
            MsgBox("Quantity cannot be less than 0")
            Return
        End If

        groupHardware(index).Text = txtValues(index).ToString

        total -= prices(index)

        txtTotal.Text = "Php " & total.ToString("f2")


    End Sub

    Private Sub btnBuyNow_Click(sender As Object, e As EventArgs) Handles btnBuyNow.Click

        total = planPrice + totalPriceHardware


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

    Private Sub btnCart_Click(sender As Object, e As EventArgs)






    End Sub

    Private Sub btnCart_Click_1(sender As Object, e As EventArgs) Handles btnCart.Click

        For i = 0 To 4


            If txtValues(i) > 0 Then

                Dim item As New cartItem()

                item.ProductName = productNames(i)
                item.Quantity = txtValues(i)
                item.Price = prices(i)


                cartItems.Add(item)




            End If

        Next

        Cart.LoadCart(cartItems)




        For i = 0 To 4
            txtValues(i) = 0
            groupHardware(i).Text = txtValues(i)
        Next


        MsgBox("Added to Cart Successfully!")

        cartItems.Clear
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Cart.Show()
        Me.Close()
    End Sub
End Class