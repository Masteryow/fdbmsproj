<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cart
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SubscriptionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HomeToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.btnDeletionMode = New System.Windows.Forms.Button()
        Me.btnClearCart = New System.Windows.Forms.Button()
        Me.btnCheckout = New System.Windows.Forms.Button()
        Me.btnContinueShopping = New System.Windows.Forms.Button()
        Me.btnCancelOrder = New System.Windows.Forms.Button()
        Me.lblDeletionMode = New System.Windows.Forms.Label()
        Me.btnCheck = New System.Windows.Forms.Button()
        Me.btnDM = New System.Windows.Forms.Button()
        Me.TicketToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HomeToolStripMenuItem, Me.HelpToolStripMenuItem, Me.ToolStripMenuItem1, Me.ProductsToolStripMenuItem, Me.SubscriptionToolStripMenuItem, Me.HomeToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(5, 2, 0, 2)
        Me.MenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 28)
        Me.MenuStrip1.TabIndex = 23
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.ForeColor = System.Drawing.Color.White
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(50, 24)
        Me.ToolStripMenuItem1.Text = "Cart"
        '
        'HomeToolStripMenuItem
        '
        Me.HomeToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.HomeToolStripMenuItem.Name = "HomeToolStripMenuItem"
        Me.HomeToolStripMenuItem.Size = New System.Drawing.Size(84, 24)
        Me.HomeToolStripMenuItem.Text = "About Us"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TicketToolStripMenuItem})
        Me.HelpToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(55, 24)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'ProductsToolStripMenuItem
        '
        Me.ProductsToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.ProductsToolStripMenuItem.Name = "ProductsToolStripMenuItem"
        Me.ProductsToolStripMenuItem.Size = New System.Drawing.Size(80, 24)
        Me.ProductsToolStripMenuItem.Text = "Products"
        '
        'SubscriptionToolStripMenuItem
        '
        Me.SubscriptionToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.SubscriptionToolStripMenuItem.Name = "SubscriptionToolStripMenuItem"
        Me.SubscriptionToolStripMenuItem.Size = New System.Drawing.Size(105, 24)
        Me.SubscriptionToolStripMenuItem.Text = "Subscription"
        '
        'HomeToolStripMenuItem1
        '
        Me.HomeToolStripMenuItem1.ForeColor = System.Drawing.Color.White
        Me.HomeToolStripMenuItem1.Name = "HomeToolStripMenuItem1"
        Me.HomeToolStripMenuItem1.Size = New System.Drawing.Size(64, 24)
        Me.HomeToolStripMenuItem1.Text = "Home"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 22.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(33, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 45)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Cart"
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(563, 66)
        Me.txtTotal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(215, 22)
        Me.txtTotal.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(504, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 21)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Total"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(41, 122)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(281, 276)
        Me.CheckedListBox1.TabIndex = 27
        '
        'btnDeletionMode
        '
        Me.btnDeletionMode.Location = New System.Drawing.Point(385, 304)
        Me.btnDeletionMode.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDeletionMode.Name = "btnDeletionMode"
        Me.btnDeletionMode.Size = New System.Drawing.Size(100, 28)
        Me.btnDeletionMode.TabIndex = 28
        Me.btnDeletionMode.Text = "Deletion Mode"
        Me.btnDeletionMode.UseVisualStyleBackColor = True
        '
        'btnClearCart
        '
        Me.btnClearCart.Location = New System.Drawing.Point(508, 304)
        Me.btnClearCart.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearCart.Name = "btnClearCart"
        Me.btnClearCart.Size = New System.Drawing.Size(100, 28)
        Me.btnClearCart.TabIndex = 29
        Me.btnClearCart.Text = "Clear Cart"
        Me.btnClearCart.UseVisualStyleBackColor = True
        '
        'btnCheckout
        '
        Me.btnCheckout.Location = New System.Drawing.Point(385, 358)
        Me.btnCheckout.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCheckout.Name = "btnCheckout"
        Me.btnCheckout.Size = New System.Drawing.Size(100, 28)
        Me.btnCheckout.TabIndex = 31
        Me.btnCheckout.Text = "Checkout"
        Me.btnCheckout.UseVisualStyleBackColor = True
        '
        'btnContinueShopping
        '
        Me.btnContinueShopping.Location = New System.Drawing.Point(508, 358)
        Me.btnContinueShopping.Margin = New System.Windows.Forms.Padding(4)
        Me.btnContinueShopping.Name = "btnContinueShopping"
        Me.btnContinueShopping.Size = New System.Drawing.Size(224, 28)
        Me.btnContinueShopping.TabIndex = 32
        Me.btnContinueShopping.Text = "Continue Shopping"
        Me.btnContinueShopping.UseVisualStyleBackColor = True
        '
        'btnCancelOrder
        '
        Me.btnCancelOrder.Location = New System.Drawing.Point(508, 394)
        Me.btnCancelOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancelOrder.Name = "btnCancelOrder"
        Me.btnCancelOrder.Size = New System.Drawing.Size(224, 28)
        Me.btnCancelOrder.TabIndex = 33
        Me.btnCancelOrder.Text = "Cancel Order"
        Me.btnCancelOrder.UseVisualStyleBackColor = True
        '
        'lblDeletionMode
        '
        Me.lblDeletionMode.AutoSize = True
        Me.lblDeletionMode.ForeColor = System.Drawing.Color.Red
        Me.lblDeletionMode.Location = New System.Drawing.Point(41, 13)
        Me.lblDeletionMode.Name = "lblDeletionMode"
        Me.lblDeletionMode.Size = New System.Drawing.Size(95, 16)
        Me.lblDeletionMode.TabIndex = 34
        Me.lblDeletionMode.Text = "Deletion Mode"
        Me.lblDeletionMode.Visible = False
        '
        'btnCheck
        '
        Me.btnCheck.Location = New System.Drawing.Point(142, 10)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(75, 23)
        Me.btnCheck.TabIndex = 35
        Me.btnCheck.Text = "Check"
        Me.btnCheck.UseVisualStyleBackColor = True
        '
        'btnDM
        '
        Me.btnDM.Location = New System.Drawing.Point(223, 10)
        Me.btnDM.Name = "btnDM"
        Me.btnDM.Size = New System.Drawing.Size(75, 23)
        Me.btnDM.TabIndex = 36
        Me.btnDM.Text = "Exit DM"
        Me.btnDM.UseVisualStyleBackColor = True
        '
        'TicketToolStripMenuItem
        '
        Me.TicketToolStripMenuItem.Name = "TicketToolStripMenuItem"
        Me.TicketToolStripMenuItem.Size = New System.Drawing.Size(224, 26)
        Me.TicketToolStripMenuItem.Text = "Ticket"
        '
        'Cart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnDM)
        Me.Controls.Add(Me.btnCheck)
        Me.Controls.Add(Me.lblDeletionMode)
        Me.Controls.Add(Me.btnCancelOrder)
        Me.Controls.Add(Me.btnContinueShopping)
        Me.Controls.Add(Me.btnCheckout)
        Me.Controls.Add(Me.btnClearCart)
        Me.Controls.Add(Me.btnDeletionMode)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Cart"
        Me.Text = "Cart"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents HomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProductsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SubscriptionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HomeToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTotal As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents btnDeletionMode As Button
    Friend WithEvents btnClearCart As Button
    Friend WithEvents btnCheckout As Button
    Friend WithEvents btnContinueShopping As Button
    Friend WithEvents btnCancelOrder As Button
    Friend WithEvents lblDeletionMode As Label
    Friend WithEvents btnCheck As Button
    Friend WithEvents btnDM As Button
    Friend WithEvents TicketToolStripMenuItem As ToolStripMenuItem
End Class
