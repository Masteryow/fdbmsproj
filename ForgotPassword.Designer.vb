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
        Me.SuspendLayout()
        '
        'txtNewPass
        '
        Me.txtNewPass.Location = New System.Drawing.Point(309, 205)
        Me.txtNewPass.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNewPass.Name = "txtNewPass"
        Me.txtNewPass.Size = New System.Drawing.Size(178, 22)
        Me.txtNewPass.TabIndex = 0
        '
        'txtNewPassConfirm
        '
        Me.txtNewPassConfirm.Location = New System.Drawing.Point(309, 286)
        Me.txtNewPassConfirm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNewPassConfirm.Name = "txtNewPassConfirm"
        Me.txtNewPassConfirm.Size = New System.Drawing.Size(178, 22)
        Me.txtNewPassConfirm.TabIndex = 1
        '
        'userInput
        '
        Me.userInput.Location = New System.Drawing.Point(309, 135)
        Me.userInput.Name = "userInput"
        Me.userInput.ReadOnly = True
        Me.userInput.Size = New System.Drawing.Size(178, 22)
        Me.userInput.TabIndex = 2
        '
        'btnPassUpdate
        '
        Me.btnPassUpdate.Location = New System.Drawing.Point(360, 353)
        Me.btnPassUpdate.Name = "btnPassUpdate"
        Me.btnPassUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnPassUpdate.TabIndex = 3
        Me.btnPassUpdate.Text = "Update"
        Me.btnPassUpdate.UseVisualStyleBackColor = True
        Me.btnPassUpdate.Visible = False
        '
        'ForgotPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnPassUpdate)
        Me.Controls.Add(Me.userInput)
        Me.Controls.Add(Me.txtNewPassConfirm)
        Me.Controls.Add(Me.txtNewPass)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "ForgotPassword"
        Me.Text = "ForgotPassword"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtNewPass As TextBox
    Friend WithEvents txtNewPassConfirm As TextBox
    Friend WithEvents userInput As TextBox
    Friend WithEvents btnPassUpdate As Button
End Class
