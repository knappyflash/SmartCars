Public Class QuickTest

    Private geneticAlgorithm As New GeneticAlgorithm(1, 2, 1, 4, 4)
    Private Sub QuickTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Label1.Text = $"Xor:
0, 0 = 0
0, 1 = 1
1, 0 = 1
1, 1 = 0
"
    End Sub
    Private Sub QuickTestButton_Click(sender As Object, e As EventArgs) Handles QuickTestButton.Click
        Me.geneticAlgorithm.inputs(0) = CDbl(Me.TextBox1.Text)
        Me.geneticAlgorithm.inputs(1) = CDbl(Me.TextBox2.Text)
        Me.geneticAlgorithm.Iterate()
        Me.Label2.Text = Me.geneticAlgorithm.outputs(0).ToString("F2")
    End Sub

End Class