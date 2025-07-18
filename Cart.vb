Imports fdbmsproj.Addon
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Cart

    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"

    Private Shared cartItems As New List(Of Addon.cartItem)

    Public Sub LoadCart(newItems As List(Of Addon.cartItem))

        For Each newItem In newItems
            Dim existingItem = cartItems.FirstOrDefault(Function(x) x.ProductName = newItem.ProductName)

            If existingItem IsNot Nothing Then

                existingItem.Quantity += newItem.Quantity
            Else

                cartItems.Add(newItem)
            End If
        Next


        RefreshCartDisplay()

        ' Try

        ' Using con As New MySqlConnection(strCon)


        ' con.Open()

        ' Dim joinUserProduct As New MySqlCommand("SELECT u.user_id c.productName, c.category, c.quantity, c.price FROM 
        '           users u INNER JOIN cart c ON u.user_id = c.user_id")

        '      Dim getId As New MySqlCommand("SELECT COUNT(*) FROM cart WHERE user_id = @user_id")



        '     Dim cmd As New MySqlCommand("INSERT INTO cart (productName, category, quantity, price, user_id) VALUES
        '                     (productName = @productName, category = @category, quantity = @quantity, user_id = @user_id)")






        ' End Using


        ' Catch ex As Exception

        ' End Try

        'fix recording the cart


    End Sub

    Private Sub RefreshCartDisplay()

        CheckedListBox1.Items.Clear()

        For Each item In cartItems
            CheckedListBox1.Items.Add($"{item.ProductName} - Qty: {item.Quantity} - Price: Php {item.Price:F2}")
        Next
    End Sub


    Public Function GetCartTotal() As Decimal
        Dim total As Decimal = 0
        For Each item In cartItems
            total += (item.Price * item.Quantity)
        Next
        Return total
    End Function


    Public Sub ClearCart()
        cartItems.Clear()
        RefreshCartDisplay()
    End Sub


    Public Sub RemoveItem(productName As String)
        cartItems.RemoveAll(Function(x) x.ProductName = productName)
        RefreshCartDisplay()
    End Sub

    Private Sub Cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshCartDisplay()
    End Sub
End Class

