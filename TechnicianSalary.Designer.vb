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
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvSalary = New System.Windows.Forms.DataGridView()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnIndPay = New System.Windows.Forms.Button()
        Me.btnPayAll = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Crimson
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(-3, 32)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(616, 46)
        Me.Panel1.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(21, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(436, 33)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Technician Salary Management"
        '
        'dgvSalary
        '
        Me.dgvSalary.AllowUserToAddRows = False
        Me.dgvSalary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSalary.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvSalary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSalary.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvSalary.Location = New System.Drawing.Point(23, 123)
        Me.dgvSalary.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.dgvSalary.MultiSelect = False
        Me.dgvSalary.Name = "dgvSalary"
        Me.dgvSalary.ReadOnly = True
        Me.dgvSalary.RowHeadersWidth = 51
        Me.dgvSalary.RowTemplate.Height = 24
        Me.dgvSalary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSalary.Size = New System.Drawing.Size(396, 222)
        Me.dgvSalary.TabIndex = 8
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(23, 100)
        Me.txtName.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(366, 20)
        Me.txtName.TabIndex = 9
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.fdbmsproj.My.Resources.Resources.search
        Me.PictureBox1.Location = New System.Drawing.Point(393, 100)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(26, 18)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'btnIndPay
        '
        Me.btnIndPay.BackColor = System.Drawing.Color.DarkGreen
        Me.btnIndPay.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIndPay.ForeColor = System.Drawing.Color.White
        Me.btnIndPay.Location = New System.Drawing.Point(437, 164)
        Me.btnIndPay.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnIndPay.Name = "btnIndPay"
        Me.btnIndPay.Size = New System.Drawing.Size(151, 36)
        Me.btnIndPay.TabIndex = 11
        Me.btnIndPay.Text = "PAY"
        Me.btnIndPay.UseVisualStyleBackColor = False
        '
        'btnPayAll
        '
        Me.btnPayAll.BackColor = System.Drawing.Color.DarkGreen
        Me.btnPayAll.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPayAll.ForeColor = System.Drawing.Color.White
        Me.btnPayAll.Location = New System.Drawing.Point(437, 204)
        Me.btnPayAll.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnPayAll.Name = "btnPayAll"
        Me.btnPayAll.Size = New System.Drawing.Size(151, 35)
        Me.btnPayAll.TabIndex = 12
        Me.btnPayAll.Text = "PAY ALL"
        Me.btnPayAll.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Crimson
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(486, 6)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 33)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "BACK"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Maroon
        Me.Button2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(437, 243)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(151, 35)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "LOG OUT"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'TechnicianSalary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(611, 366)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnPayAll)
        Me.Controls.Add(Me.btnIndPay)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.dgvSalary)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
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
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
