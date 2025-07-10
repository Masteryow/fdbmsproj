<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ForgotPassword
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
        Me.txtNewPass = New System.Windows.Forms.TextBox()
        Me.txtNewPassConfirm = New System.Windows.Forms.TextBox()
        Me.userInput = New System.Windows.Forms.TextBox()
        Me.btnPassUpdate = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtNewPass
        '
        Me.txtNewPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPass.Location = New System.Drawing.Point(94, 254)
        Me.txtNewPass.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNewPass.Name = "txtNewPass"
        Me.txtNewPass.Size = New System.Drawing.Size(612, 27)
        Me.txtNewPass.TabIndex = 0
        '
        'txtNewPassConfirm
        '
        Me.txtNewPassConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPassConfirm.Location = New System.Drawing.Point(99, 343)
        Me.txtNewPassConfirm.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNewPassConfirm.Name = "txtNewPassConfirm"
        Me.txtNewPassConfirm.Size = New System.Drawing.Size(607, 27)
        Me.txtNewPassConfirm.TabIndex = 1
        '
        'userInput
        '
        Me.userInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.userInput.Location = New System.Drawing.Point(94, 185)
        Me.userInput.Name = "userInput"
        Me.userInput.ReadOnly = True
        Me.userInput.Size = New System.Drawing.Size(612, 27)
        Me.userInput.TabIndex = 2
        '
        'btnPassUpdate
        '
        Me.btnPassUpdate.BackColor = System.Drawing.Color.Black
        Me.btnPassUpdate.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPassUpdate.ForeColor = System.Drawing.Color.Transparent
        Me.btnPassUpdate.Location = New System.Drawing.Point(315, 387)
        Me.btnPassUpdate.Name = "btnPassUpdate"
        Me.btnPassUpdate.Size = New System.Drawing.Size(167, 41)
        Me.btnPassUpdate.TabIndex = 3
        Me.btnPassUpdate.Text = "Update"
        Me.btnPassUpdate.UseVisualStyleBackColor = False
        Me.btnPassUpdate.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(314, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(173, 31)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Reset Password"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(271, 104)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(258, 20)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Enter and confirm your new password"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(90, 162)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Username"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(95, 230)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 20)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Password"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(95, 309)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(132, 20)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Confirm Password"
        '
        'ForgotPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnPassUpdate)
        Me.Controls.Add(Me.userInput)
        Me.Controls.Add(Me.txtNewPassConfirm)
        Me.Controls.Add(Me.txtNewPass)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ForgotPassword"
        Me.Text = "ForgotPassword"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtNewPass As TextBox
    Friend WithEvents txtNewPassConfirm As TextBox
    Friend WithEvents userInput As TextBox
    Friend WithEvents btnPassUpdate As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label7 As Label
End Class
