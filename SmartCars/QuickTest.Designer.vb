<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QuickTest
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
        QuickTestButton = New Button()
        Label1 = New Label()
        Label2 = New Label()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        SuspendLayout()
        ' 
        ' QuickTestButton
        ' 
        QuickTestButton.Location = New Point(12, 12)
        QuickTestButton.Name = "QuickTestButton"
        QuickTestButton.Size = New Size(75, 23)
        QuickTestButton.TabIndex = 0
        QuickTestButton.Text = "QuickTest"
        QuickTestButton.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 38)
        Label1.Name = "Label1"
        Label1.Size = New Size(28, 15)
        Label1.TabIndex = 1
        Label1.Text = "Xor:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(214, 38)
        Label2.Name = "Label2"
        Label2.Size = New Size(41, 15)
        Label2.TabIndex = 2
        Label2.Text = "Label2"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(93, 12)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(33, 23)
        TextBox1.TabIndex = 3
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(132, 12)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(36, 23)
        TextBox2.TabIndex = 4
        ' 
        ' QuickTest
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(462, 333)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(QuickTestButton)
        Name = "QuickTest"
        Text = "QuickTest"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents QuickTestButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
End Class
