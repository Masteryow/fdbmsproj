<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Admin
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
        Me.pnlSidebar = New System.Windows.Forms.Panel()
        Me.btnDashboard = New System.Windows.Forms.Button()
        Me.btnUsers = New System.Windows.Forms.Button()
        Me.btnPlans = New System.Windows.Forms.Button()
        Me.btnSubscribers = New System.Windows.Forms.Button()
        Me.btnTechnicians = New System.Windows.Forms.Button()
        Me.btnAddons = New System.Windows.Forms.Button()
        Me.btnBilling = New System.Windows.Forms.Button()
        Me.btnTickets = New System.Windows.Forms.Button()
        Me.btnReports = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.tmrAnimation = New System.Windows.Forms.Timer(Me.components)
        Me.pnlSidebar.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSidebar
        '
        Me.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pnlSidebar.Controls.Add(Me.btnLogout)
        Me.pnlSidebar.Controls.Add(Me.btnReports)
        Me.pnlSidebar.Controls.Add(Me.btnTickets)
        Me.pnlSidebar.Controls.Add(Me.btnBilling)
        Me.pnlSidebar.Controls.Add(Me.btnAddons)
        Me.pnlSidebar.Controls.Add(Me.btnTechnicians)
        Me.pnlSidebar.Controls.Add(Me.btnSubscribers)
        Me.pnlSidebar.Controls.Add(Me.btnPlans)
        Me.pnlSidebar.Controls.Add(Me.btnUsers)
        Me.pnlSidebar.Controls.Add(Me.btnDashboard)
        Me.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSidebar.Location = New System.Drawing.Point(0, 0)
        Me.pnlSidebar.Name = "pnlSidebar"
        Me.pnlSidebar.Size = New System.Drawing.Size(180, 405)
        Me.pnlSidebar.TabIndex = 0
        '
        'btnDashboard
        '
        Me.btnDashboard.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnDashboard.FlatAppearance.BorderSize = 0
        Me.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDashboard.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDashboard.ForeColor = System.Drawing.Color.White
        Me.btnDashboard.Location = New System.Drawing.Point(0, 10)
        Me.btnDashboard.Name = "btnDashboard"
        Me.btnDashboard.Size = New System.Drawing.Size(180, 35)
        Me.btnDashboard.TabIndex = 0
        Me.btnDashboard.Text = "📊 Dashboard"
        Me.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDashboard.UseVisualStyleBackColor = False
        '
        'btnUsers
        '
        Me.btnUsers.BackColor = System.Drawing.Color.Transparent
        Me.btnUsers.FlatAppearance.BorderSize = 0
        Me.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUsers.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUsers.ForeColor = System.Drawing.Color.White
        Me.btnUsers.Location = New System.Drawing.Point(0, 50)
        Me.btnUsers.Name = "btnUsers"
        Me.btnUsers.Size = New System.Drawing.Size(180, 35)
        Me.btnUsers.TabIndex = 1
        Me.btnUsers.Text = "👥 Users"
        Me.btnUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUsers.UseVisualStyleBackColor = False
        '
        'btnPlans
        '
        Me.btnPlans.BackColor = System.Drawing.Color.Transparent
        Me.btnPlans.FlatAppearance.BorderSize = 0
        Me.btnPlans.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPlans.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlans.ForeColor = System.Drawing.Color.White
        Me.btnPlans.Location = New System.Drawing.Point(0, 90)
        Me.btnPlans.Name = "btnPlans"
        Me.btnPlans.Size = New System.Drawing.Size(180, 35)
        Me.btnPlans.TabIndex = 2
        Me.btnPlans.Text = "📋 Internet Plans"
        Me.btnPlans.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPlans.UseVisualStyleBackColor = False
        '
        'btnSubscribers
        '
        Me.btnSubscribers.BackColor = System.Drawing.Color.Transparent
        Me.btnSubscribers.FlatAppearance.BorderSize = 0
        Me.btnSubscribers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSubscribers.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubscribers.ForeColor = System.Drawing.Color.White
        Me.btnSubscribers.Location = New System.Drawing.Point(0, 130)
        Me.btnSubscribers.Name = "btnSubscribers"
        Me.btnSubscribers.Size = New System.Drawing.Size(180, 35)
        Me.btnSubscribers.TabIndex = 3
        Me.btnSubscribers.Text = "📡 Subscribers"
        Me.btnSubscribers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSubscribers.UseVisualStyleBackColor = False
        '
        'btnTechnicians
        '
        Me.btnTechnicians.BackColor = System.Drawing.Color.Transparent
        Me.btnTechnicians.FlatAppearance.BorderSize = 0
        Me.btnTechnicians.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTechnicians.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTechnicians.ForeColor = System.Drawing.Color.White
        Me.btnTechnicians.Location = New System.Drawing.Point(0, 170)
        Me.btnTechnicians.Name = "btnTechnicians"
        Me.btnTechnicians.Size = New System.Drawing.Size(180, 35)
        Me.btnTechnicians.TabIndex = 4
        Me.btnTechnicians.Text = "🔧 Technicians"
        Me.btnTechnicians.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTechnicians.UseVisualStyleBackColor = False
        '
        'btnAddons
        '
        Me.btnAddons.BackColor = System.Drawing.Color.Transparent
        Me.btnAddons.FlatAppearance.BorderSize = 0
        Me.btnAddons.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddons.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddons.ForeColor = System.Drawing.Color.White
        Me.btnAddons.Location = New System.Drawing.Point(0, 210)
        Me.btnAddons.Name = "btnAddons"
        Me.btnAddons.Size = New System.Drawing.Size(180, 35)
        Me.btnAddons.TabIndex = 5
        Me.btnAddons.Text = "🛠️ Add-ons"
        Me.btnAddons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddons.UseVisualStyleBackColor = False
        '
        'btnBilling
        '
        Me.btnBilling.BackColor = System.Drawing.Color.Transparent
        Me.btnBilling.FlatAppearance.BorderSize = 0
        Me.btnBilling.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBilling.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBilling.ForeColor = System.Drawing.Color.White
        Me.btnBilling.Location = New System.Drawing.Point(0, 250)
        Me.btnBilling.Name = "btnBilling"
        Me.btnBilling.Size = New System.Drawing.Size(180, 35)
        Me.btnBilling.TabIndex = 6
        Me.btnBilling.Text = "💰 Billing"
        Me.btnBilling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBilling.UseVisualStyleBackColor = False
        '
        'btnTickets
        '
        Me.btnTickets.BackColor = System.Drawing.Color.Transparent
        Me.btnTickets.FlatAppearance.BorderSize = 0
        Me.btnTickets.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTickets.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTickets.ForeColor = System.Drawing.Color.White
        Me.btnTickets.Location = New System.Drawing.Point(0, 290)
        Me.btnTickets.Name = "btnTickets"
        Me.btnTickets.Size = New System.Drawing.Size(180, 35)
        Me.btnTickets.TabIndex = 7
        Me.btnTickets.Text = "🎫 Support Tickets"
        Me.btnTickets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTickets.UseVisualStyleBackColor = False
        '
        'btnReports
        '
        Me.btnReports.BackColor = System.Drawing.Color.Transparent
        Me.btnReports.FlatAppearance.BorderSize = 0
        Me.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReports.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReports.ForeColor = System.Drawing.Color.White
        Me.btnReports.Location = New System.Drawing.Point(0, 330)
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(180, 35)
        Me.btnReports.TabIndex = 8
        Me.btnReports.Text = "📈 Reports"
        Me.btnReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReports.UseVisualStyleBackColor = False
        '
        'btnLogout
        '
        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(0, 370)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(180, 35)
        Me.btnLogout.TabIndex = 9
        Me.btnLogout.Text = "🚪 Logout"
        Me.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(10, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(87, 21)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Dashboard"
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.lblTitle)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(180, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(447, 405)
        Me.pnlMain.TabIndex = 1
        '
        'pnlContent
        '
        Me.pnlContent.BackColor = System.Drawing.Color.White
        Me.pnlContent.Location = New System.Drawing.Point(10, 40)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(427, 355)
        Me.pnlContent.TabIndex = 1
        '
        'tmrAnimation
        '
        Me.tmrAnimation.Interval = 50
        '
        'AdminFormForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(627, 405)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSidebar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "AdminForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Admin Panel - ISP Management System"
        Me.pnlSidebar.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlSidebar As Panel
    Friend WithEvents btnDashboard As Button
    Friend WithEvents btnUsers As Button
    Friend WithEvents btnPlans As Button
    Friend WithEvents btnSubscribers As Button
    Friend WithEvents btnTechnicians As Button
    Friend WithEvents btnAddons As Button
    Friend WithEvents btnBilling As Button
    Friend WithEvents btnTickets As Button
    Friend WithEvents btnReports As Button
    Friend WithEvents btnLogout As Button
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlMain As Panel
    Friend WithEvents pnlContent As Panel
    Friend WithEvents tmrAnimation As Timer
End Class