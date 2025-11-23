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
        Dim v1 As Double = CDbl(Me.TextBox1.Text)
        Dim v2 As Double = CDbl(Me.TextBox2.Text)
        Dim inputs() As Double = {v1, CDbl(v2)}

        For i As Integer = 0 To Me.geneticAlgorithm.NeuralNetworks.Count - 1
            Dim score As Double = 0
            Dim outputs() As Double = Me.geneticAlgorithm.NeuralNetworks(i).PropogateForward(inputs)

            If i = 0 Then

                Me.Label2.Text = CInt(outputs(0))
            End If

            If (v1 = 0) And (v2 = 0) And (CInt(outputs(0)) = 0) Then score = score + 1
            If (v1 = 0) And (v2 = 1) And (CInt(outputs(0)) = 1) Then score = score + 1
            If (v1 = 1) And (v2 = 0) And (CInt(outputs(0)) = 1) Then score = score + 1
            If (v1 = 0) And (v2 = 0) And (CInt(outputs(0)) = 0) Then score = score + 1
            Me.geneticAlgorithm.NeuralNetworks(i).FitnessScore = score
            Debug.WriteLine($"{v1} {v2} {CInt(outputs(0))} {score}")
        Next

        Me.geneticAlgorithm.NextGeneration()

    End Sub

End Class