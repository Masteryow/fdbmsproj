<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class subscribers
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
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SubscriptionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TicketsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HomeToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblPlan = New System.Windows.Forms.Label()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.panelBilling = New System.Windows.Forms.Panel()
        Me.btnPay = New System.Windows.Forms.Button()
        Me.lblAmountDue = New System.Windows.Forms.Label()
        Me.lblBillMonth = New System.Windows.Forms.Label()
        Me.MySqlCommand1 = New MySql.Data.MySqlClient.MySqlCommand()
        Me.MenuStrip1.SuspendLayout()
        Me.panelBilling.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HomeToolStripMenuItem, Me.SubscriptionToolStripMenuItem, Me.ToolStripMenuItem1, Me.ProductsToolStripMenuItem, Me.HomeToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MenuStrip1.Size = New System.Drawing.Size(688, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'HomeToolStripMenuItem
        '
        Me.HomeToolStripMenuItem.ForeColor = System.Drawing.Color.Red
        Me.HomeToolStripMenuItem.Name = "HomeToolStripMenuItem"
        Me.HomeToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.HomeToolStripMenuItem.Text = "Log Out"
        '
        'SubscriptionToolStripMenuItem
        '
        Me.SubscriptionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TicketsToolStripMenuItem})
        Me.SubscriptionToolStripMenuItem.ForeColor = System.Drawing.Color.White
        Me.SubscriptionToolStripMenuItem.Name = "SubscriptionToolStripMenuItem"
        Me.SubscriptionToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.SubscriptionToolStripMenuItem.Text = "Help"
        '
        'TicketsToolStripMenuItem
        '
        Me.TicketsToolStripMenuItem.Name = "TicketsToolStripMenuItem"
        Me.TicketsToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.TicketsToolStripMenuItem.Text = "Tickets"
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
        'HomeToolStripMenuItem1
        '
        Me.HomeToolStripMenuItem1.ForeColor = System.Drawing.Color.White
        Me.HomeToolStripMenuItem1.Name = "HomeToolStripMenuItem1"
        Me.HomeToolStripMenuItem1.Size = New System.Drawing.Size(52, 20)
        Me.HomeToolStripMenuItem1.Text = "Home"
        '
        'lblUser
        '
        Me.lblUser.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblUser.AutoSize = True
        Me.lblUser.BackColor = System.Drawing.Color.Transparent
        Me.lblUser.Font = New System.Drawing.Font("Palatino Linotype", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.ForeColor = System.Drawing.Color.White
        Me.lblUser.Location = New System.Drawing.Point(45, 50)
        Me.lblUser.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(191, 44)
        Me.lblUser.TabIndex = 3
        Me.lblUser.Text = "placeholder"
        '
        'Timer1
        '
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Font = New System.Drawing.Font("Palatino Linotype", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.White
        Me.lblStatus.Location = New System.Drawing.Point(58, 94)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(78, 28)
        Me.lblStatus.TabIndex = 4
        Me.lblStatus.Text = "Status:"
        '
        'lblPlan
        '
        Me.lblPlan.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPlan.AutoSize = True
        Me.lblPlan.BackColor = System.Drawing.Color.Transparent
        Me.lblPlan.Font = New System.Drawing.Font("Palatino Linotype", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlan.ForeColor = System.Drawing.Color.White
        Me.lblPlan.Location = New System.Drawing.Point(58, 124)
        Me.lblPlan.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPlan.Name = "lblPlan"
        Me.lblPlan.Size = New System.Drawing.Size(61, 28)
        Me.lblPlan.TabIndex = 5
        Me.lblPlan.Text = "Plan:"
        '
        'lblPrice
        '
        Me.lblPrice.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPrice.AutoSize = True
        Me.lblPrice.BackColor = System.Drawing.Color.Transparent
        Me.lblPrice.Font = New System.Drawing.Font("Palatino Linotype", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrice.ForeColor = System.Drawing.Color.White
        Me.lblPrice.Location = New System.Drawing.Point(58, 154)
        Me.lblPrice.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(66, 28)
        Me.lblPrice.TabIndex = 6
        Me.lblPrice.Text = "Price:"
        '
        'panelBilling
        '
        Me.panelBilling.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.panelBilling.BackColor = System.Drawing.Color.Transparent
        Me.panelBilling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelBilling.Controls.Add(Me.btnPay)
        Me.panelBilling.Controls.Add(Me.lblAmountDue)
        Me.panelBilling.Controls.Add(Me.lblBillMonth)
        Me.panelBilling.Location = New System.Drawing.Point(53, 200)
        Me.panelBilling.Name = "panelBilling"
        Me.panelBilling.Size = New System.Drawing.Size(595, 212)
        Me.panelBilling.TabIndex = 7
        '
        'btnPay
        '
        Me.btnPay.BackColor = System.Drawing.Color.Transparent
        Me.btnPay.Font = New System.Drawing.Font("Palatino Linotype", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPay.Location = New System.Drawing.Point(172, 133)
        Me.btnPay.Name = "btnPay"
        Me.btnPay.Size = New System.Drawing.Size(248, 46)
        Me.btnPay.TabIndex = 10
        Me.btnPay.Text = "Pay now"
        Me.btnPay.UseVisualStyleBackColor = False
        '
        'lblAmountDue
        '
        Me.lblAmountDue.AutoSize = True
        Me.lblAmountDue.Font = New System.Drawing.Font("Palatino Linotype", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountDue.ForeColor = System.Drawing.Color.Transparent
        Me.lblAmountDue.Location = New System.Drawing.Point(5, 41)
        Me.lblAmountDue.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblAmountDue.Name = "lblAmountDue"
        Me.lblAmountDue.Size = New System.Drawing.Size(111, 22)
        Me.lblAmountDue.TabIndex = 9
        Me.lblAmountDue.Text = "Amount Due:"
        '
        'lblBillMonth
        '
        Me.lblBillMonth.AutoSize = True
        Me.lblBillMonth.Font = New System.Drawing.Font("Palatino Linotype", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillMonth.ForeColor = System.Drawing.Color.Transparent
        Me.lblBillMonth.Location = New System.Drawing.Point(5, 9)
        Me.lblBillMonth.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblBillMonth.Name = "lblBillMonth"
        Me.lblBillMonth.Size = New System.Drawing.Size(119, 22)
        Me.lblBillMonth.TabIndex = 8
        Me.lblBillMonth.Text = "Billing Month:"
        '
        'MySqlCommand1
        '
        Me.MySqlCommand1.CacheAge = 0
        Me.MySqlCommand1.Connection = Nothing
        Me.MySqlCommand1.EnableCaching = False
        Me.MySqlCommand1.Transaction = Nothing
        '
        'subscribers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.fdbmsproj.My.Resources.Resources.black_abstract
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(688, 440)
        Me.Controls.Add(Me.panelBilling)
        Me.Controls.Add(Me.lblPrice)
        Me.Controls.Add(Me.lblPlan)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.lblUser)
        Me.DoubleBuffered = True
        Me.Name = "subscribers"
        Me.Text = "subscribers"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.panelBilling.ResumeLayout(False)
        Me.panelBilling.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SubscriptionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TicketsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ProductsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HomeToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents lblUser As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblPlan As Label
    Friend WithEvents lblPrice As Label
    Friend WithEvents panelBilling As Panel
    Friend WithEvents btnPay As Button
    Friend WithEvents lblAmountDue As Label
    Friend WithEvents lblBillMonth As Label
    Friend WithEvents MySqlCommand1 As MySql.Data.MySqlClient.MySqlCommand
End Class
