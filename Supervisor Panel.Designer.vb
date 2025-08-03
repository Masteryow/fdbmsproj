<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Supervisor_Panel
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
        Me.MySqlCommand1 = New MySql.Data.MySqlClient.MySqlCommand()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnChoose = New System.Windows.Forms.Button()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDifficulty = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtConcern = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbxSubName = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnAssign = New System.Windows.Forms.Button()
        Me.cbxCompletedTix = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbxPendingTix = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSkills = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbxTechName = New System.Windows.Forms.ComboBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MySqlCommand1
        '
        Me.MySqlCommand1.CacheAge = 0
        Me.MySqlCommand1.Connection = Nothing
        Me.MySqlCommand1.EnableCaching = False
        Me.MySqlCommand1.Transaction = Nothing
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.DarkSlateBlue
        Me.Panel1.Controls.Add(Me.btnLogout)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Location = New System.Drawing.Point(-8, 44)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(912, 66)
        Me.Panel1.TabIndex = 7
        '
        'btnLogout
        '
        Me.btnLogout.BackColor = System.Drawing.Color.DarkBlue
        Me.btnLogout.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(719, 19)
        Me.btnLogout.Margin = New System.Windows.Forms.Padding(2)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(143, 33)
        Me.btnLogout.TabIndex = 4
        Me.btnLogout.Text = "LOG OUT"
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(24, 13)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(287, 39)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Supervisor Panel"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.MidnightBlue
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(543, 19)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(143, 33)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Salary Assignment"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.btnClear)
        Me.GroupBox1.Controls.Add(Me.btnChoose)
        Me.GroupBox1.Controls.Add(Me.txtNotes)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtPrice)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtDifficulty)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtConcern)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbxSubName)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(23, 147)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(442, 364)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Subscriber Concern"
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.MidnightBlue
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Location = New System.Drawing.Point(261, 293)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(2)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(140, 42)
        Me.btnClear.TabIndex = 15
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        Me.btnClear.Visible = False
        '
        'btnChoose
        '
        Me.btnChoose.BackColor = System.Drawing.Color.MidnightBlue
        Me.btnChoose.ForeColor = System.Drawing.Color.White
        Me.btnChoose.Location = New System.Drawing.Point(73, 293)
        Me.btnChoose.Margin = New System.Windows.Forms.Padding(2)
        Me.btnChoose.Name = "btnChoose"
        Me.btnChoose.Size = New System.Drawing.Size(140, 42)
        Me.btnChoose.TabIndex = 14
        Me.btnChoose.Text = "Choose"
        Me.btnChoose.UseVisualStyleBackColor = False
        Me.btnChoose.Visible = False
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(16, 207)
        Me.txtNotes.Margin = New System.Windows.Forms.Padding(2)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ReadOnly = True
        Me.txtNotes.Size = New System.Drawing.Size(403, 64)
        Me.txtNotes.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(199, 183)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 19)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Notes"
        '
        'txtPrice
        '
        Me.txtPrice.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrice.Location = New System.Drawing.Point(252, 124)
        Me.txtPrice.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.ReadOnly = True
        Me.txtPrice.Size = New System.Drawing.Size(167, 27)
        Me.txtPrice.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(205, 134)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 19)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Price"
        '
        'txtDifficulty
        '
        Me.txtDifficulty.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDifficulty.Location = New System.Drawing.Point(88, 128)
        Me.txtDifficulty.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDifficulty.Name = "txtDifficulty"
        Me.txtDifficulty.ReadOnly = True
        Me.txtDifficulty.Size = New System.Drawing.Size(108, 23)
        Me.txtDifficulty.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 134)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 19)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Difficulty"
        '
        'txtConcern
        '
        Me.txtConcern.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConcern.Location = New System.Drawing.Point(94, 79)
        Me.txtConcern.Margin = New System.Windows.Forms.Padding(2)
        Me.txtConcern.Name = "txtConcern"
        Me.txtConcern.ReadOnly = True
        Me.txtConcern.Size = New System.Drawing.Size(325, 27)
        Me.txtConcern.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 82)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 19)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Concern"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 37)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 19)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Subscriber Name"
        '
        'cbxSubName
        '
        Me.cbxSubName.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxSubName.FormattingEnabled = True
        Me.cbxSubName.Location = New System.Drawing.Point(146, 35)
        Me.cbxSubName.Margin = New System.Windows.Forms.Padding(2)
        Me.cbxSubName.Name = "cbxSubName"
        Me.cbxSubName.Size = New System.Drawing.Size(273, 27)
        Me.cbxSubName.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.btnAssign)
        Me.GroupBox2.Controls.Add(Me.cbxCompletedTix)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.cbxPendingTix)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtStatus)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtSkills)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.cbxTechName)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(487, 147)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(376, 364)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Technician Assignment"
        '
        'btnAssign
        '
        Me.btnAssign.BackColor = System.Drawing.Color.MidnightBlue
        Me.btnAssign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAssign.ForeColor = System.Drawing.Color.White
        Me.btnAssign.Location = New System.Drawing.Point(75, 293)
        Me.btnAssign.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAssign.Name = "btnAssign"
        Me.btnAssign.Size = New System.Drawing.Size(182, 42)
        Me.btnAssign.TabIndex = 16
        Me.btnAssign.Text = "Assign"
        Me.btnAssign.UseVisualStyleBackColor = False
        Me.btnAssign.Visible = False
        '
        'cbxCompletedTix
        '
        Me.cbxCompletedTix.FormattingEnabled = True
        Me.cbxCompletedTix.Location = New System.Drawing.Point(245, 238)
        Me.cbxCompletedTix.Margin = New System.Windows.Forms.Padding(2)
        Me.cbxCompletedTix.Name = "cbxCompletedTix"
        Me.cbxCompletedTix.Size = New System.Drawing.Size(95, 33)
        Me.cbxCompletedTix.TabIndex = 21
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(251, 215)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 19)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Completed"
        '
        'cbxPendingTix
        '
        Me.cbxPendingTix.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxPendingTix.FormattingEnabled = True
        Me.cbxPendingTix.Location = New System.Drawing.Point(118, 240)
        Me.cbxPendingTix.Margin = New System.Windows.Forms.Padding(2)
        Me.cbxPendingTix.Name = "cbxPendingTix"
        Me.cbxPendingTix.Size = New System.Drawing.Size(116, 31)
        Me.cbxPendingTix.TabIndex = 19
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(114, 217)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 19)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Pending Tickets"
        '
        'txtStatus
        '
        Me.txtStatus.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(19, 238)
        Me.txtStatus.Margin = New System.Windows.Forms.Padding(2)
        Me.txtStatus.Multiline = True
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(78, 31)
        Me.txtStatus.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(31, 217)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 19)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Status"
        '
        'txtSkills
        '
        Me.txtSkills.Location = New System.Drawing.Point(19, 128)
        Me.txtSkills.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSkills.Multiline = True
        Me.txtSkills.Name = "txtSkills"
        Me.txtSkills.ReadOnly = True
        Me.txtSkills.Size = New System.Drawing.Size(331, 75)
        Me.txtSkills.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(153, 89)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 23)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Skills"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 43)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(153, 19)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = " Technician Name"
        '
        'cbxTechName
        '
        Me.cbxTechName.FormattingEnabled = True
        Me.cbxTechName.Location = New System.Drawing.Point(191, 35)
        Me.cbxTechName.Margin = New System.Windows.Forms.Padding(2)
        Me.cbxTechName.Name = "cbxTechName"
        Me.cbxTechName.Size = New System.Drawing.Size(159, 33)
        Me.cbxTechName.TabIndex = 4
        '
        'Supervisor_Panel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.fdbmsproj.My.Resources.Resources.darkBLUEBG
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(903, 522)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Supervisor_Panel"
        Me.Text = "Supervisor_Panel"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MySqlCommand1 As MySql.Data.MySqlClient.MySqlCommand
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnClear As Button
    Friend WithEvents btnChoose As Button
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtPrice As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtDifficulty As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtConcern As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cbxSubName As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtSkills As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents cbxTechName As ComboBox
    Friend WithEvents cbxCompletedTix As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents cbxPendingTix As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents btnAssign As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btnLogout As Button
End Class
