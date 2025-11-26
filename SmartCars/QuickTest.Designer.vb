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
        TextBox01 = New TextBox()
        TextBox11 = New TextBox()
        TextBox10 = New TextBox()
        Label01 = New Label()
        TextBox21 = New TextBox()
        TextBox20 = New TextBox()
        Label02 = New Label()
        TextBox31 = New TextBox()
        TextBox30 = New TextBox()
        Label03 = New Label()
        TextBox_00 = New TextBox()
        Label_00 = New Label()
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
        ' TextBox01
        ' 
        TextBox01.Location = New Point(132, 12)
        TextBox01.Name = "TextBox01"
        TextBox01.Size = New Size(36, 23)
        TextBox01.TabIndex = 4
        TextBox01.Text = "0"
        ' 
        ' TextBox11
        ' 
        TextBox11.Location = New Point(132, 41)
        TextBox11.Name = "TextBox11"
        TextBox11.Size = New Size(36, 23)
        TextBox11.TabIndex = 7
        TextBox11.Text = "1"
        ' 
        ' TextBox10
        ' 
        TextBox10.Location = New Point(93, 41)
        TextBox10.Name = "TextBox10"
        TextBox10.Size = New Size(33, 23)
        TextBox10.TabIndex = 6
        TextBox10.Text = "0"
        ' 
        ' Label01
        ' 
        Label01.AutoSize = True
        Label01.Location = New Point(183, 44)
        Label01.Name = "Label01"
        Label01.Size = New Size(41, 15)
        Label01.TabIndex = 5
        Label01.Text = "Label3"
        ' 
        ' TextBox21
        ' 
        TextBox21.Location = New Point(132, 70)
        TextBox21.Name = "TextBox21"
        TextBox21.Size = New Size(36, 23)
        TextBox21.TabIndex = 10
        TextBox21.Text = "0"
        ' 
        ' TextBox20
        ' 
        TextBox20.Location = New Point(93, 70)
        TextBox20.Name = "TextBox20"
        TextBox20.Size = New Size(33, 23)
        TextBox20.TabIndex = 9
        TextBox20.Text = "1"
        ' 
        ' Label02
        ' 
        Label02.AutoSize = True
        Label02.Location = New Point(183, 73)
        Label02.Name = "Label02"
        Label02.Size = New Size(41, 15)
        Label02.TabIndex = 8
        Label02.Text = "Label4"
        ' 
        ' TextBox31
        ' 
        TextBox31.Location = New Point(132, 99)
        TextBox31.Name = "TextBox31"
        TextBox31.Size = New Size(36, 23)
        TextBox31.TabIndex = 13
        TextBox31.Text = "1"
        ' 
        ' TextBox30
        ' 
        TextBox30.Location = New Point(93, 99)
        TextBox30.Name = "TextBox30"
        TextBox30.Size = New Size(33, 23)
        TextBox30.TabIndex = 12
        TextBox30.Text = "1"
        ' 
        ' Label03
        ' 
        Label03.AutoSize = True
        Label03.Location = New Point(183, 102)
        Label03.Name = "Label03"
        Label03.Size = New Size(41, 15)
        Label03.TabIndex = 11
        Label03.Text = "Label5"
        ' 
        ' TextBox_00
        ' 
        TextBox_00.Location = New Point(93, 12)
        TextBox_00.Name = "TextBox_00"
        TextBox_00.Size = New Size(36, 23)
        TextBox_00.TabIndex = 14
        TextBox_00.Text = "0"
        ' 
        ' Label_00
        ' 
        Label_00.AutoSize = True
        Label_00.Location = New Point(183, 16)
        Label_00.Name = "Label_00"
        Label_00.Size = New Size(41, 15)
        Label_00.TabIndex = 15
        Label_00.Text = "Label3"
        ' 
        ' QuickTest
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(462, 333)
        Controls.Add(Label_00)
        Controls.Add(TextBox_00)
        Controls.Add(TextBox31)
        Controls.Add(TextBox30)
        Controls.Add(Label03)
        Controls.Add(TextBox21)
        Controls.Add(TextBox20)
        Controls.Add(Label02)
        Controls.Add(TextBox11)
        Controls.Add(TextBox10)
        Controls.Add(Label01)
        Controls.Add(TextBox01)
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
    Friend WithEvents TextBox01 As TextBox
    Friend WithEvents TextBox11 As TextBox
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents Label01 As Label
    Friend WithEvents TextBox21 As TextBox
    Friend WithEvents TextBox20 As TextBox
    Friend WithEvents Label02 As Label
    Friend WithEvents TextBox31 As TextBox
    Friend WithEvents TextBox30 As TextBox
    Friend WithEvents Label03 As Label
    Friend WithEvents TextBox_00 As TextBox
    Friend WithEvents Label_00 As Label
End Class
