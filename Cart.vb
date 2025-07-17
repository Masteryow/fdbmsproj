Public Class Cart

    Public Sub LoadCart(cartItems As List(Of Addon.cartItem))


        For Each item In cartItems

            CheckedListBox1.Items.Add($"{item.ProductName} {item.Quantity} {item.Price}")



        Next













    End Sub






End Class



