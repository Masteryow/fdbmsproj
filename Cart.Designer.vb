<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Cart
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TicketToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.btnupdatequantity = New System.Windows.Forms.Button()
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
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MenuStrip1.Size = New System.Drawing.Size(705, 24)
        Me.MenuStrip1.TabIndex = 23
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HomeToolStripMenuItem
        '
        Me.HomeToolStripMenuItem.ForeColor = System.Drawing.Color.Red
        Me.HomeToolStripMenuItem.Name = "HomeToolStripMenuItem"
        Me.HomeToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.HomeToolStripMenuItem.Text = "Log Out"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TicketToolStripMenuItem})
        Me.HelpToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'TicketToolStripMenuItem
        '
        Me.TicketToolStripMenuItem.Name = "TicketToolStripMenuItem"
        Me.TicketToolStripMenuItem.Size = New System.Drawing.Size(106, 22)
        Me.TicketToolStripMenuItem.Text = "Ticket"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.ForeColor = System.Drawing.Color.White
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(41, 20)
        Me.ToolStripMenuItem1.Text = "Cart"
        '
        'ProductsToolStripMenuItem
        '
        Me.ProductsToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.ProductsToolStripMenuItem.Name = "ProductsToolStripMenuItem"
        Me.ProductsToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
        Me.ProductsToolStripMenuItem.Text = "Products"
        '
        'SubscriptionToolStripMenuItem
        '
        Me.SubscriptionToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.SubscriptionToolStripMenuItem.Name = "SubscriptionToolStripMenuItem"
        Me.SubscriptionToolStripMenuItem.Size = New System.Drawing.Size(85, 20)
        Me.SubscriptionToolStripMenuItem.Text = "Subscription"
        '
        'HomeToolStripMenuItem1
        '
        Me.HomeToolStripMenuItem1.ForeColor = System.Drawing.Color.White
        Me.HomeToolStripMenuItem1.Name = "HomeToolStripMenuItem1"
        Me.HomeToolStripMenuItem1.Size = New System.Drawing.Size(52, 20)
        Me.HomeToolStripMenuItem1.Text = "Home"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 22.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(25, 33)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 36)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "CART"
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(533, 75)
        Me.txtTotal.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(132, 20)
        Me.txtTotal.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Black
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(480, 76)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 17)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Total"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(31, 98)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(634, 169)
        Me.CheckedListBox1.TabIndex = 27
        '
        'btnDeletionMode
        '
        Me.btnDeletionMode.BackColor = System.Drawing.Color.DarkRed
        Me.btnDeletionMode.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeletionMode.ForeColor = System.Drawing.Color.White
        Me.btnDeletionMode.Location = New System.Drawing.Point(307, 280)
        Me.btnDeletionMode.Name = "btnDeletionMode"
        Me.btnDeletionMode.Size = New System.Drawing.Size(147, 50)
        Me.btnDeletionMode.TabIndex = 28
        Me.btnDeletionMode.Text = "DELETION MODE"
        Me.btnDeletionMode.UseVisualStyleBackColor = False
        '
        'btnClearCart
        '
        Me.btnClearCart.BackColor = System.Drawing.Color.DarkRed
        Me.btnClearCart.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearCart.ForeColor = System.Drawing.Color.White
        Me.btnClearCart.Location = New System.Drawing.Point(307, 336)
        Me.btnClearCart.Name = "btnClearCart"
        Me.btnClearCart.Size = New System.Drawing.Size(147, 49)
        Me.btnClearCart.TabIndex = 29
        Me.btnClearCart.Text = "CLEAR CART"
        Me.btnClearCart.UseVisualStyleBackColor = False
        '
        'btnCheckout
        '
        Me.btnCheckout.BackColor = System.Drawing.Color.DarkGreen
        Me.btnCheckout.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheckout.ForeColor = System.Drawing.Color.White
        Me.btnCheckout.Location = New System.Drawing.Point(31, 282)
        Me.btnCheckout.Name = "btnCheckout"
        Me.btnCheckout.Size = New System.Drawing.Size(254, 48)
        Me.btnCheckout.TabIndex = 31
        Me.btnCheckout.Text = "CHECKOUT"
        Me.btnCheckout.UseVisualStyleBackColor = False
        '
        'btnContinueShopping
        '
        Me.btnContinueShopping.BackColor = System.Drawing.Color.Indigo
        Me.btnContinueShopping.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinueShopping.ForeColor = System.Drawing.Color.White
        Me.btnContinueShopping.Location = New System.Drawing.Point(473, 282)
        Me.btnContinueShopping.Name = "btnContinueShopping"
        Me.btnContinueShopping.Size = New System.Drawing.Size(192, 48)
        Me.btnContinueShopping.TabIndex = 32
        Me.btnContinueShopping.Text = "CONTINUE SHOPPING"
        Me.btnContinueShopping.UseVisualStyleBackColor = False
        '
        'btnCancelOrder
        '
        Me.btnCancelOrder.BackColor = System.Drawing.Color.Indigo
        Me.btnCancelOrder.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelOrder.ForeColor = System.Drawing.Color.White
        Me.btnCancelOrder.Location = New System.Drawing.Point(473, 336)
        Me.btnCancelOrder.Name = "btnCancelOrder"
        Me.btnCancelOrder.Size = New System.Drawing.Size(192, 49)
        Me.btnCancelOrder.TabIndex = 33
        Me.btnCancelOrder.Text = "CANCEL ORDER"
        Me.btnCancelOrder.UseVisualStyleBackColor = False
        '
        'lblDeletionMode
        '
        Me.lblDeletionMode.AutoSize = True
        Me.lblDeletionMode.BackColor = System.Drawing.Color.Transparent
        Me.lblDeletionMode.Font = New System.Drawing.Font("Microsoft YaHei UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeletionMode.ForeColor = System.Drawing.Color.Red
        Me.lblDeletionMode.Location = New System.Drawing.Point(27, 74)
        Me.lblDeletionMode.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDeletionMode.Name = "lblDeletionMode"
        Me.lblDeletionMode.Size = New System.Drawing.Size(121, 19)
        Me.lblDeletionMode.TabIndex = 34
        Me.lblDeletionMode.Text = "Deletion Mode"
        Me.lblDeletionMode.Visible = False
        '
        'btnCheck
        '
        Me.btnCheck.BackColor = System.Drawing.Color.DarkRed
        Me.btnCheck.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheck.ForeColor = System.Drawing.Color.White
        Me.btnCheck.Location = New System.Drawing.Point(152, 67)
        Me.btnCheck.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCheck.Name = "btnCheck"
        Me.btnCheck.Size = New System.Drawing.Size(73, 27)
        Me.btnCheck.TabIndex = 35
        Me.btnCheck.Text = "DELETE"
        Me.btnCheck.UseVisualStyleBackColor = False
        '
        'btnDM
        '
        Me.btnDM.BackColor = System.Drawing.Color.Indigo
        Me.btnDM.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDM.ForeColor = System.Drawing.Color.White
        Me.btnDM.Location = New System.Drawing.Point(229, 68)
        Me.btnDM.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDM.Name = "btnDM"
        Me.btnDM.Size = New System.Drawing.Size(146, 26)
        Me.btnDM.TabIndex = 36
        Me.btnDM.Text = "EXIT DELETION MODE"
        Me.btnDM.UseVisualStyleBackColor = False
        '
        'btnupdatequantity
        '
        Me.btnupdatequantity.BackColor = System.Drawing.Color.DarkGreen
        Me.btnupdatequantity.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdatequantity.ForeColor = System.Drawing.Color.White
        Me.btnupdatequantity.Location = New System.Drawing.Point(31, 336)
        Me.btnupdatequantity.Name = "btnupdatequantity"
        Me.btnupdatequantity.Size = New System.Drawing.Size(254, 48)
        Me.btnupdatequantity.TabIndex = 37
        Me.btnupdatequantity.Text = "Update Quantity"
        Me.btnupdatequantity.UseVisualStyleBackColor = False
        '
        'Cart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.fdbmsproj.My.Resources.Resources.black_abstract
        Me.ClientSize = New System.Drawing.Size(705, 418)
        Me.Controls.Add(Me.btnupdatequantity)
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
        Me.Margin = New System.Windows.Forms.Padding(2)
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
    Friend WithEvents btnupdatequantity As Button
End Class
