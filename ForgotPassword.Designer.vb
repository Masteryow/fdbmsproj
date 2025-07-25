<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ForgotPassword
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
        Me.txtNewPass = New System.Windows.Forms.TextBox()
        Me.txtNewPassConfirm = New System.Windows.Forms.TextBox()
        Me.userInput = New System.Windows.Forms.TextBox()
        Me.btnPassUpdate = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnResend = New System.Windows.Forms.Button()
        Me.lblSubheader = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnVerify = New System.Windows.Forms.Button()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtNewPass
        '
        Me.txtNewPass.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPass.Location = New System.Drawing.Point(70, 206)
        Me.txtNewPass.Name = "txtNewPass"
        Me.txtNewPass.Size = New System.Drawing.Size(460, 27)
        Me.txtNewPass.TabIndex = 0
        '
        'txtNewPassConfirm
        '
        Me.txtNewPassConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPassConfirm.Location = New System.Drawing.Point(74, 279)
        Me.txtNewPassConfirm.Name = "txtNewPassConfirm"
        Me.txtNewPassConfirm.Size = New System.Drawing.Size(456, 27)
        Me.txtNewPassConfirm.TabIndex = 1
        '
        'userInput
        '
        Me.userInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.userInput.Location = New System.Drawing.Point(70, 150)
        Me.userInput.Margin = New System.Windows.Forms.Padding(2)
        Me.userInput.Name = "userInput"
        Me.userInput.ReadOnly = True
        Me.userInput.Size = New System.Drawing.Size(460, 27)
        Me.userInput.TabIndex = 2
        '
        'btnPassUpdate
        '
        Me.btnPassUpdate.BackColor = System.Drawing.Color.Black
        Me.btnPassUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPassUpdate.ForeColor = System.Drawing.Color.Transparent
        Me.btnPassUpdate.Location = New System.Drawing.Point(236, 314)
        Me.btnPassUpdate.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPassUpdate.Name = "btnPassUpdate"
        Me.btnPassUpdate.Size = New System.Drawing.Size(125, 33)
        Me.btnPassUpdate.TabIndex = 3
        Me.btnPassUpdate.Text = "Update"
        Me.btnPassUpdate.UseVisualStyleBackColor = False
        Me.btnPassUpdate.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(236, 39)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(173, 31)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Reset Password"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(203, 84)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(258, 20)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Enter and confirm your new password"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(68, 132)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Username"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(71, 187)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 20)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Password"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(71, 251)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(132, 20)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Confirm Password"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnResend)
        Me.Panel1.Controls.Add(Me.lblSubheader)
        Me.Panel1.Controls.Add(Me.lblHeader)
        Me.Panel1.Controls.Add(Me.btnVerify)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Location = New System.Drawing.Point(-1, -3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(613, 371)
        Me.Panel1.TabIndex = 12
        '
        'btnResend
        '
        Me.btnResend.BackColor = System.Drawing.Color.Black
        Me.btnResend.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnResend.ForeColor = System.Drawing.Color.Transparent
        Me.btnResend.Location = New System.Drawing.Point(337, 240)
        Me.btnResend.Margin = New System.Windows.Forms.Padding(2)
        Me.btnResend.Name = "btnResend"
        Me.btnResend.Size = New System.Drawing.Size(125, 33)
        Me.btnResend.TabIndex = 19
        Me.btnResend.Text = "Resend"
        Me.btnResend.UseVisualStyleBackColor = False
        '
        'lblSubheader
        '
        Me.lblSubheader.AutoSize = True
        Me.lblSubheader.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubheader.Location = New System.Drawing.Point(244, 152)
        Me.lblSubheader.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSubheader.Name = "lblSubheader"
        Me.lblSubheader.Size = New System.Drawing.Size(123, 20)
        Me.lblSubheader.TabIndex = 17
        Me.lblSubheader.Text = "Code Verification"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI Semibold", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.Location = New System.Drawing.Point(131, 106)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(377, 31)
        Me.lblHeader.TabIndex = 16
        Me.lblHeader.Text = "A code has been sent to your email"
        '
        'btnVerify
        '
        Me.btnVerify.BackColor = System.Drawing.Color.Black
        Me.btnVerify.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVerify.ForeColor = System.Drawing.Color.Transparent
        Me.btnVerify.Location = New System.Drawing.Point(170, 240)
        Me.btnVerify.Margin = New System.Windows.Forms.Padding(2)
        Me.btnVerify.Name = "btnVerify"
        Me.btnVerify.Size = New System.Drawing.Size(125, 33)
        Me.btnVerify.TabIndex = 15
        Me.btnVerify.Text = "Verify"
        Me.btnVerify.UseVisualStyleBackColor = False
        '
        'txtCode
        '
        Me.txtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.Location = New System.Drawing.Point(86, 189)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(456, 27)
        Me.txtCode.TabIndex = 13
        '
        'ForgotPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 366)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnPassUpdate)
        Me.Controls.Add(Me.userInput)
        Me.Controls.Add(Me.txtNewPassConfirm)
        Me.Controls.Add(Me.txtNewPass)
        Me.Name = "ForgotPassword"
        Me.Text = "ForgotPassword"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
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
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblSubheader As Label
    Friend WithEvents lblHeader As Label
    Friend WithEvents btnVerify As Button
    Friend WithEvents txtCode As TextBox
    Friend WithEvents btnResend As Button
End Class
