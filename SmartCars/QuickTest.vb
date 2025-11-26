Public Class QuickTest

    Private geneticAlgorithm As New GeneticAlgorithm(100, 2, 1, 4, 4)
    Private Sub QuickTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Label1.Text = $"Xor:
0, 0 = 0
0, 1 = 1
1, 0 = 1
1, 1 = 0
"
    End Sub
    Private Sub QuickTestButton_Click(sender As Object, e As EventArgs) Handles QuickTestButton.Click

        For j = 0 To 50
            For i As Integer = 0 To Me.geneticAlgorithm.NeuralNetworks.Count - 1
                Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore = 0
                If Me.geneticAlgorithm.NeuralNetworks(i).PropogateForward({0, 0})(0) < 0.5 Then Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore += 1
                If Me.geneticAlgorithm.NeuralNetworks(i).PropogateForward({0, 1})(0) >= 0.5 Then Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore += 1
                If Me.geneticAlgorithm.NeuralNetworks(i).PropogateForward({1, 0})(0) >= 0.5 Then Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore += 1
                If Me.geneticAlgorithm.NeuralNetworks(i).PropogateForward({1, 1})(0) < 0.5 Then Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore += 1
            Next i
            Me.geneticAlgorithm.NextGeneration()
        Next j

        Me.Label_00.Text = Me.geneticAlgorithm.NeuralNetworks(0).PropogateForward({Me.TextBox_00.Text, Me.TextBox01.Text})(0).ToString("F2")
        Me.Label01.Text = Me.geneticAlgorithm.NeuralNetworks(0).PropogateForward({Me.TextBox10.Text, Me.TextBox11.Text})(0).ToString("F2")
        Me.Label02.Text = Me.geneticAlgorithm.NeuralNetworks(0).PropogateForward({Me.TextBox20.Text, Me.TextBox21.Text})(0).ToString("F2")
        Me.Label03.Text = Me.geneticAlgorithm.NeuralNetworks(0).PropogateForward({Me.TextBox30.Text, Me.TextBox31.Text})(0).ToString("F2")

        If Me.Label_00.Text >= 0.5 Then
            Me.Label_00.Text = 1
        Else
            Me.Label_00.Text = 0
        End If

        If Me.Label01.Text >= 0.5 Then
            Me.Label01.Text = 1
        Else
            Me.Label01.Text = 0
        End If

        If Me.Label02.Text >= 0.5 Then
            Me.Label02.Text = 1
        Else
            Me.Label02.Text = 0
        End If

        If Me.Label03.Text >= 0.5 Then
            Me.Label03.Text = 1
        Else
            Me.Label03.Text = 0
        End If

    End Sub

End Class