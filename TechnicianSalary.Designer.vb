<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TechnicianSalary
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvSalary = New System.Windows.Forms.DataGridView()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnIndPay = New System.Windows.Forms.Button()
        Me.btnPayAll = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Crimson
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(-4, 39)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(822, 56)
        Me.Panel1.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(28, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(535, 40)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Technician Salary Management"
        '
        'dgvSalary
        '
        Me.dgvSalary.AllowUserToAddRows = False
        Me.dgvSalary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSalary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSalary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSalary.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSalary.Location = New System.Drawing.Point(31, 151)
        Me.dgvSalary.MultiSelect = False
        Me.dgvSalary.Name = "dgvSalary"
        Me.dgvSalary.ReadOnly = True
        Me.dgvSalary.RowHeadersWidth = 51
        Me.dgvSalary.RowTemplate.Height = 24
        Me.dgvSalary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSalary.Size = New System.Drawing.Size(528, 273)
        Me.dgvSalary.TabIndex = 8
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(31, 123)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(487, 22)
        Me.txtName.TabIndex = 9
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.fdbmsproj.My.Resources.Resources.search
        Me.PictureBox1.Location = New System.Drawing.Point(524, 123)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(35, 22)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'btnIndPay
        '
        Me.btnIndPay.Location = New System.Drawing.Point(583, 217)
        Me.btnIndPay.Name = "btnIndPay"
        Me.btnIndPay.Size = New System.Drawing.Size(100, 23)
        Me.btnIndPay.TabIndex = 11
        Me.btnIndPay.Text = "Pay"
        Me.btnIndPay.UseVisualStyleBackColor = True
        '
        'btnPayAll
        '
        Me.btnPayAll.Location = New System.Drawing.Point(583, 265)
        Me.btnPayAll.Name = "btnPayAll"
        Me.btnPayAll.Size = New System.Drawing.Size(100, 23)
        Me.btnPayAll.TabIndex = 12
        Me.btnPayAll.Text = "Pay All"
        Me.btnPayAll.UseVisualStyleBackColor = True
        '
        'TechnicianSalary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(815, 450)
        Me.Controls.Add(Me.btnPayAll)
        Me.Controls.Add(Me.btnIndPay)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.dgvSalary)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "TechnicianSalary"
        Me.Text = "TechnicianSalary"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvSalary As DataGridView
    Friend WithEvents txtName As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnIndPay As Button
    Friend WithEvents btnPayAll As Button
End Class
