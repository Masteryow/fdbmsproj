<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class products
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
        Me.cbxItems = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtStocks = New System.Windows.Forms.TextBox()
        Me.pbxAdd = New System.Windows.Forms.PictureBox()
        Me.pbxMinus = New System.Windows.Forms.PictureBox()
        Me.lblStocks = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.cbxFilter = New System.Windows.Forms.ComboBox()
        Me.lblQuantity = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pbxClear = New System.Windows.Forms.PictureBox()
        Me.pbxAddToCart = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.pbxItem = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.lblPlanName = New System.Windows.Forms.Label()
        Me.lblPlanType = New System.Windows.Forms.Label()
        Me.lblPlanPrice = New System.Windows.Forms.Label()
        Me.lblChangeOfMind = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pbxBuyNow = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.HomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TicketToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cartbutton = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SubscriptionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HomeToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtCartTotal = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pbxPlanImage = New System.Windows.Forms.PictureBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pbxAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxMinus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.pbxClear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxAddToCart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.pbxItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.pbxBuyNow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.pbxPlanImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbxItems
        '
        Me.cbxItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxItems.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxItems.FormattingEnabled = True
        Me.cbxItems.Location = New System.Drawing.Point(91, 54)
        Me.cbxItems.Name = "cbxItems"
        Me.cbxItems.Size = New System.Drawing.Size(247, 24)
        Me.cbxItems.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.txtPrice)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtStocks)
        Me.GroupBox1.Controls.Add(Me.pbxAdd)
        Me.GroupBox1.Controls.Add(Me.pbxMinus)
        Me.GroupBox1.Controls.Add(Me.lblStocks)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtQuantity)
        Me.GroupBox1.Controls.Add(Me.cbxFilter)
        Me.GroupBox1.Controls.Add(Me.lblQuantity)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbxItems)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(61, 164)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(550, 216)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Addon Selection"
        '
        'txtPrice
        '
        Me.txtPrice.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrice.Location = New System.Drawing.Point(126, 114)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.ReadOnly = True
        Me.txtPrice.Size = New System.Drawing.Size(212, 23)
        Me.txtPrice.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 19)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Base Price"
        '
        'txtStocks
        '
        Me.txtStocks.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStocks.Location = New System.Drawing.Point(424, 113)
        Me.txtStocks.Name = "txtStocks"
        Me.txtStocks.ReadOnly = True
        Me.txtStocks.Size = New System.Drawing.Size(103, 23)
        Me.txtStocks.TabIndex = 7
        '
        'pbxAdd
        '
        Me.pbxAdd.BackColor = System.Drawing.Color.White
        Me.pbxAdd.Location = New System.Drawing.Point(298, 177)
        Me.pbxAdd.Name = "pbxAdd"
        Me.pbxAdd.Size = New System.Drawing.Size(40, 22)
        Me.pbxAdd.TabIndex = 11
        Me.pbxAdd.TabStop = False
        '
        'pbxMinus
        '
        Me.pbxMinus.BackColor = System.Drawing.Color.White
        Me.pbxMinus.Location = New System.Drawing.Point(247, 177)
        Me.pbxMinus.Name = "pbxMinus"
        Me.pbxMinus.Size = New System.Drawing.Size(40, 22)
        Me.pbxMinus.TabIndex = 12
        Me.pbxMinus.TabStop = False
        '
        'lblStocks
        '
        Me.lblStocks.AutoSize = True
        Me.lblStocks.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStocks.Location = New System.Drawing.Point(357, 116)
        Me.lblStocks.Name = "lblStocks"
        Me.lblStocks.Size = New System.Drawing.Size(62, 19)
        Me.lblStocks.TabIndex = 6
        Me.lblStocks.Text = "Stocks"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 19)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Addon"
        '
        'txtQuantity
        '
        Me.txtQuantity.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantity.Location = New System.Drawing.Point(126, 177)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(111, 23)
        Me.txtQuantity.TabIndex = 10
        Me.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbxFilter
        '
        Me.cbxFilter.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxFilter.FormattingEnabled = True
        Me.cbxFilter.Items.AddRange(New Object() {"All", "Hardware", "Service", "Plan Upgrade"})
        Me.cbxFilter.Location = New System.Drawing.Point(413, 54)
        Me.cbxFilter.Name = "cbxFilter"
        Me.cbxFilter.Size = New System.Drawing.Size(114, 24)
        Me.cbxFilter.TabIndex = 2
        '
        'lblQuantity
        '
        Me.lblQuantity.AutoSize = True
        Me.lblQuantity.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuantity.ForeColor = System.Drawing.Color.White
        Me.lblQuantity.Location = New System.Drawing.Point(24, 178)
        Me.lblQuantity.Name = "lblQuantity"
        Me.lblQuantity.Size = New System.Drawing.Size(79, 19)
        Me.lblQuantity.TabIndex = 10
        Me.lblQuantity.Text = "Quantity"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(355, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 19)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Filter"
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.pbxClear)
        Me.Panel1.Controls.Add(Me.pbxAddToCart)
        Me.Panel1.Location = New System.Drawing.Point(61, 386)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(550, 102)
        Me.Panel1.TabIndex = 11
        '
        'pbxClear
        '
        Me.pbxClear.BackColor = System.Drawing.Color.Transparent
        Me.pbxClear.Image = Global.fdbmsproj.My.Resources.Resources.reset
        Me.pbxClear.Location = New System.Drawing.Point(326, 36)
        Me.pbxClear.Name = "pbxClear"
        Me.pbxClear.Size = New System.Drawing.Size(135, 35)
        Me.pbxClear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxClear.TabIndex = 15
        Me.pbxClear.TabStop = False
        '
        'pbxAddToCart
        '
        Me.pbxAddToCart.BackColor = System.Drawing.Color.Transparent
        Me.pbxAddToCart.Image = Global.fdbmsproj.My.Resources.Resources.addtocart
        Me.pbxAddToCart.InitialImage = Nothing
        Me.pbxAddToCart.Location = New System.Drawing.Point(78, 36)
        Me.pbxAddToCart.Name = "pbxAddToCart"
        Me.pbxAddToCart.Size = New System.Drawing.Size(159, 36)
        Me.pbxAddToCart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxAddToCart.TabIndex = 13
        Me.pbxAddToCart.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.pbxItem)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(617, 164)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(231, 216)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Item Preview"
        '
        'pbxItem
        '
        Me.pbxItem.BackColor = System.Drawing.Color.Transparent
        Me.pbxItem.Location = New System.Drawing.Point(16, 54)
        Me.pbxItem.Name = "pbxItem"
        Me.pbxItem.Size = New System.Drawing.Size(197, 145)
        Me.pbxItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxItem.TabIndex = 5
        Me.pbxItem.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(355, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(0, 19)
        Me.Label7.TabIndex = 4
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTotal.AutoSize = True
        Me.lblTotal.BackColor = System.Drawing.Color.Transparent
        Me.lblTotal.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.White
        Me.lblTotal.Location = New System.Drawing.Point(613, 125)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(52, 19)
        Me.lblTotal.TabIndex = 13
        Me.lblTotal.Text = "Total"
        '
        'txtTotal
        '
        Me.txtTotal.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTotal.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(720, 121)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(128, 23)
        Me.txtTotal.TabIndex = 13
        '
        'lblPlanName
        '
        Me.lblPlanName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPlanName.AutoSize = True
        Me.lblPlanName.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlanName.ForeColor = System.Drawing.Color.White
        Me.lblPlanName.Location = New System.Drawing.Point(185, 45)
        Me.lblPlanName.Name = "lblPlanName"
        Me.lblPlanName.Size = New System.Drawing.Size(0, 19)
        Me.lblPlanName.TabIndex = 28
        '
        'lblPlanType
        '
        Me.lblPlanType.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPlanType.AutoSize = True
        Me.lblPlanType.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlanType.ForeColor = System.Drawing.Color.White
        Me.lblPlanType.Location = New System.Drawing.Point(185, 85)
        Me.lblPlanType.Name = "lblPlanType"
        Me.lblPlanType.Size = New System.Drawing.Size(0, 19)
        Me.lblPlanType.TabIndex = 29
        '
        'lblPlanPrice
        '
        Me.lblPlanPrice.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPlanPrice.AutoSize = True
        Me.lblPlanPrice.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlanPrice.ForeColor = System.Drawing.Color.White
        Me.lblPlanPrice.Location = New System.Drawing.Point(185, 121)
        Me.lblPlanPrice.Name = "lblPlanPrice"
        Me.lblPlanPrice.Size = New System.Drawing.Size(0, 19)
        Me.lblPlanPrice.TabIndex = 30
        '
        'lblChangeOfMind
        '
        Me.lblChangeOfMind.AutoSize = True
        Me.lblChangeOfMind.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangeOfMind.ForeColor = System.Drawing.Color.White
        Me.lblChangeOfMind.Location = New System.Drawing.Point(13, 9)
        Me.lblChangeOfMind.Name = "lblChangeOfMind"
        Me.lblChangeOfMind.Size = New System.Drawing.Size(209, 16)
        Me.lblChangeOfMind.TabIndex = 31
        Me.lblChangeOfMind.Text = "Change of Mind? Buy Plan Only!"
        Me.lblChangeOfMind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblChangeOfMind)
        Me.Panel2.Controls.Add(Me.pbxBuyNow)
        Me.Panel2.Location = New System.Drawing.Point(617, 386)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(231, 102)
        Me.Panel2.TabIndex = 16
        '
        'pbxBuyNow
        '
        Me.pbxBuyNow.BackColor = System.Drawing.Color.Transparent
        Me.pbxBuyNow.Image = Global.fdbmsproj.My.Resources.Resources.buynow
        Me.pbxBuyNow.Location = New System.Drawing.Point(34, 37)
        Me.pbxBuyNow.Name = "pbxBuyNow"
        Me.pbxBuyNow.Size = New System.Drawing.Size(159, 35)
        Me.pbxBuyNow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxBuyNow.TabIndex = 32
        Me.pbxBuyNow.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HomeToolStripMenuItem, Me.HelpToolStripMenuItem, Me.cartbutton, Me.ProductsToolStripMenuItem, Me.SubscriptionToolStripMenuItem, Me.HomeToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MenuStrip1.Size = New System.Drawing.Size(903, 24)
        Me.MenuStrip1.TabIndex = 31
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
        'cartbutton
        '
        Me.cartbutton.ForeColor = System.Drawing.Color.White
        Me.cartbutton.Name = "cartbutton"
        Me.cartbutton.Size = New System.Drawing.Size(41, 20)
        Me.cartbutton.Text = "Cart"
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
        'txtCartTotal
        '
        Me.txtCartTotal.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtCartTotal.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCartTotal.Location = New System.Drawing.Point(720, 85)
        Me.txtCartTotal.Name = "txtCartTotal"
        Me.txtCartTotal.ReadOnly = True
        Me.txtCartTotal.Size = New System.Drawing.Size(128, 23)
        Me.txtCartTotal.TabIndex = 32
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(613, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 19)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Cart Total"
        '
        'pbxPlanImage
        '
        Me.pbxPlanImage.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbxPlanImage.BackColor = System.Drawing.Color.Transparent
        Me.pbxPlanImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbxPlanImage.Location = New System.Drawing.Point(61, 45)
        Me.pbxPlanImage.Name = "pbxPlanImage"
        Me.pbxPlanImage.Size = New System.Drawing.Size(118, 95)
        Me.pbxPlanImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxPlanImage.TabIndex = 27
        Me.pbxPlanImage.TabStop = False
        '
        'products
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.fdbmsproj.My.Resources.Resources.backgroundprod
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(903, 522)
        Me.Controls.Add(Me.txtCartTotal)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.lblPlanPrice)
        Me.Controls.Add(Me.lblPlanType)
        Me.Controls.Add(Me.lblPlanName)
        Me.Controls.Add(Me.pbxPlanImage)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.DoubleBuffered = True
        Me.Name = "products"
        Me.Text = "products"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pbxAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxMinus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.pbxClear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxAddToCart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.pbxItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.pbxBuyNow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.pbxPlanImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbxItems As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cbxFilter As ComboBox
    Friend WithEvents txtStocks As TextBox
    Friend WithEvents lblStocks As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPrice As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lblQuantity As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents pbxAddToCart As PictureBox
    Friend WithEvents pbxMinus As PictureBox
    Friend WithEvents pbxAdd As PictureBox
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents pbxClear As PictureBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents pbxItem As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents txtTotal As TextBox
    Friend WithEvents pbxPlanImage As PictureBox
    Friend WithEvents lblPlanName As Label
    Friend WithEvents lblPlanType As Label
    Friend WithEvents lblPlanPrice As Label
    Friend WithEvents lblChangeOfMind As Label
    Friend WithEvents pbxBuyNow As PictureBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents HomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TicketToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cartbutton As ToolStripMenuItem
    Friend WithEvents ProductsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SubscriptionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HomeToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents txtCartTotal As TextBox
    Friend WithEvents Label5 As Label
End Class
