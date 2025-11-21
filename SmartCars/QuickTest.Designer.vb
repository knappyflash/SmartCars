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
        SuspendLayout()
        ' 
        ' QuickTestButton
        ' 
        QuickTestButton.Location = New Point(95, 12)
        QuickTestButton.Name = "QuickTestButton"
        QuickTestButton.Size = New Size(75, 23)
        QuickTestButton.TabIndex = 0
        QuickTestButton.Text = "QuickTest"
        QuickTestButton.UseVisualStyleBackColor = True
        ' 
        ' QuickTest
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(279, 53)
        Controls.Add(QuickTestButton)
        Name = "QuickTest"
        Text = "QuickTest"
        ResumeLayout(False)
    End Sub

    Friend WithEvents QuickTestButton As Button
End Class
