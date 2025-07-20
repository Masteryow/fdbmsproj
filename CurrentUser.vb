Public Class Session
    ' Existing properties
    Public Shared Property UserId As Integer
    Public Shared Property UserName As String
    Public Shared Property PlanId As Integer
    Public Shared Property planImage As Image
    Public Shared Property planName As String
    Public Shared Property planType As String
    Public Shared Property planPrice As Decimal
    Public Shared Property userRole As String
    Public Shared Property fromProduct As Boolean

    ' Transaction Management Properties
    Public Shared Property TransactionId As String
    Public Shared Property TransactionStartTime As DateTime
    Public Shared Property IsTransactionActive As Boolean
    Public Shared Property TransactionTimeout As Integer = 30 ' minutes

    ' Cart Management Properties (ADDED)
    Public Shared Property SelectedPlanForCart As Boolean = False
    Public Shared Property PendingCartNavigation As Boolean = False

    ' Modified CartItems property to be more robust
    Private Shared _cartItems As List(Of CartItem)
    Public Shared Property CartItems As List(Of CartItem)
        Get
            If _cartItems Is Nothing Then
                _cartItems = New List(Of CartItem)
            End If
            Return _cartItems
        End Get
        Set(value As List(Of CartItem))
            _cartItems = value
        End Set
    End Property

    ' Transaction Management Methods
    Public Shared Sub StartTransaction()
        TransactionId = Guid.NewGuid().ToString()
        TransactionStartTime = DateTime.Now
        IsTransactionActive = True
        CartItems.Clear()

        Console.WriteLine($"Transaction Started: {TransactionId} at {TransactionStartTime}")
    End Sub

    Public Shared Sub EndTransaction(completed As Boolean)
        If completed Then
            Console.WriteLine($"Transaction Completed: {TransactionId}")
        Else
            Console.WriteLine($"Transaction Cancelled: {TransactionId}")
        End If

        ' Clear transaction data
        TransactionId = String.Empty
        TransactionStartTime = DateTime.MinValue
        IsTransactionActive = False
        CartItems.Clear()

        ' Clear plan data on transaction end (ADDED)
        ClearPlanSelection()
    End Sub

    Public Shared Function IsTransactionExpired() As Boolean
        If Not IsTransactionActive Then Return False
        Return DateTime.Now.Subtract(TransactionStartTime).TotalMinutes > TransactionTimeout
    End Function

    Public Shared Sub CheckTransactionTimeout()
        If IsTransactionExpired() Then
            Console.WriteLine($"Transaction Timeout: {TransactionId}")
            EndTransaction(False)
        End If
    End Sub

    Public Shared Sub AddToCart(item As CartItem)
        CheckTransactionTimeout()
        If IsTransactionActive Then
            CartItems.Add(item)
            Console.WriteLine($"Added to cart: {item.Name} - ${item.Price}")
        End If
    End Sub

    ' ADDED method for adding items to session cart
    Public Shared Sub AddToSessionCart(item As CartItem)
        CheckTransactionTimeout()
        If IsTransactionActive Then
            CartItems.Add(item)
            Console.WriteLine($"Added to session cart: {item.Name} - ${item.Price}")
        End If
    End Sub

    Public Shared Sub RemoveFromCart(itemId As Integer)
        CheckTransactionTimeout()
        If IsTransactionActive Then
            Dim item = CartItems.FirstOrDefault(Function(x) x.Id = itemId)
            If item IsNot Nothing Then
                CartItems.Remove(item)
                Console.WriteLine($"Removed from cart: {item.Name}")
            End If
        End If
    End Sub

    Public Shared Function GetCartTotal() As Decimal
        CheckTransactionTimeout()
        If IsTransactionActive Then
            Return CartItems.Sum(Function(x) x.Price * x.Quantity)
        End If
        Return 0
    End Function

    Public Shared Function GetCartItemCount() As Integer
        CheckTransactionTimeout()
        If IsTransactionActive Then
            Return CartItems.Sum(Function(x) x.Quantity)
        End If
        Return 0
    End Function

    ' ADDED method to check if user has selected a plan for cart
    Public Shared Function HasPlanInSession() As Boolean
        Return PlanId > 0 AndAlso Not String.IsNullOrEmpty(planName)
    End Function

    ' ADDED method to clear plan selection
    Public Shared Sub ClearPlanSelection()
        PlanId = 0
        planName = ""
        planType = ""
        planPrice = 0
        planImage = Nothing
        SelectedPlanForCart = False
        PendingCartNavigation = False
    End Sub
End Class

' Cart Item class to represent items in the shopping cart
Public Class CartItem
    Public Property Id As Integer
    Public Property Name As String
    Public Property Price As Decimal
    Public Property Quantity As Integer
    Public Property Category As String ' "plan" or "addon"
    Public Property PlanId As Integer ' For tracking which plan this belongs to

    Public Sub New(id As Integer, name As String, price As Decimal, quantity As Integer, category As String, Optional planId As Integer = 0)
        Me.Id = id
        Me.Name = name
        Me.Price = price
        Me.Quantity = quantity
        Me.Category = category
        Me.PlanId = planId
    End Sub

    Public ReadOnly Property TotalPrice As Decimal
        Get
            Return Price * Quantity
        End Get
    End Property
End Class