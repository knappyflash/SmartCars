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

        Dim v1 As Double
        Dim v2 As Double
        Dim inputs() As Double
        Dim outputs() As Double
        Dim scoreA(3) As Integer

        For i As Integer = 0 To Me.geneticAlgorithm.NeuralNetworks.Count - 1
            Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore = 0
        Next

        For j = 0 To 1000

            v1 = CDbl(Maths.RandomInt(0, 1))
            v2 = CDbl(Maths.RandomInt(0, 1))
            inputs = {v1, CDbl(v2)}

            For i As Integer = 0 To Me.geneticAlgorithm.NeuralNetworks.Count - 1

                outputs = Me.geneticAlgorithm.NeuralNetworks(i).PropogateForward(inputs)

                If (v1 = 0) And (v2 = 0) And (CInt(outputs(0)) = 0) Then scoreA(0) = 1
                If (v1 = 0) And (v2 = 1) And (CInt(outputs(0)) = 1) Then scoreA(1) = 1
                If (v1 = 1) And (v2 = 0) And (CInt(outputs(0)) = 1) Then scoreA(2) = 1
                If (v1 = 0) And (v2 = 0) And (CInt(outputs(0)) = 0) Then scoreA(3) = 1

                Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore = scoreA(0) + scoreA(1) + scoreA(2) + scoreA(3)

                If Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore > Me.geneticAlgorithm.NeuralNetworks(i).FitnessScoreBest Then
                    Me.geneticAlgorithm.NeuralNetworks(i).FitnessScoreBest = Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore
                End If

            Next i

        Next j

        Me.geneticAlgorithm.NextGeneration()

        v1 = CDbl(Maths.RandomInt(0, 1))
        v2 = CDbl(Maths.RandomInt(0, 1))
        inputs = {v1, CDbl(v2)}
        Me.TextBox1.Text = v1
        Me.TextBox2.Text = v2
        outputs = Me.geneticAlgorithm.NeuralNetworks(0).PropogateForward(inputs)
        Me.Label2.Text = CInt(outputs(0))

    End Sub

End Class