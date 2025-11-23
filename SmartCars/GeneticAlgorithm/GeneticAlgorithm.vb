Public Class GeneticAlgorithm
    Public NeuralNetworks As New List(Of NeuralNetwork)
    Public inputs() As Double
    Public outputs() As Double
    Public Sub New(
                  populationSize As Integer,
                  inputCount As Integer,
                  outputCount As Integer,
                  hiddenLayerCount As Integer,
                  NeuronsPerHiddenLayer As Integer)
        ReDim inputs(inputCount - 1)
        ReDim outputs(outputCount - 1)
        For i = 0 To populationSize - 1
            NeuralNetworks.Add(New NeuralNetwork(inputCount, outputCount, hiddenLayerCount, NeuronsPerHiddenLayer))
        Next
    End Sub

    Public Sub Iterate()
        SortNeuralNetworksByFitness()
        KillBadPerformers()
        PerformCrossover()
        RunGeneration()
    End Sub

    Public Sub SortNeuralNetworksByFitness()
        Dim sortedNeuralNetworks = NeuralNetworks.OrderByDescending(Function(nn) nn.FitnessScore).ToList
        Me.NeuralNetworks = sortedNeuralNetworks
    End Sub

    Public Sub KillBadPerformers()
        Me.NeuralNetworks.RemoveRange(10, 90)
    End Sub

    Public Sub PerformCrossover()
        ' 18 × 5 = 90 offspring
        For i As Integer = 1 To 18
            For j As Integer = 0 To 8 Step 2
                NeuralNetworks.Add(New NeuralNetwork(NeuralNetworks(j), NeuralNetworks(j + 1)))
            Next
        Next
    End Sub

    Public Sub MutateIncreaseSome()
        For i As Integer = 60 To 69
            Me.NeuralNetworks(i).MutateIncrease(0.01)
        Next
    End Sub

    Public Sub MutateDecreaseSome()
        For i As Integer = 70 To 79
            Me.NeuralNetworks(i).MutateDecrease(0.01)
        Next
    End Sub

    Public Sub MutateRandomSome()
        For i As Integer = 80 To 94
            Me.NeuralNetworks(i).MutateRandom(0.01)
        Next
    End Sub

    Public Sub RandomizeSome()
        For i As Integer = 95 To 99
            Me.NeuralNetworks(i).Randomize()
        Next
    End Sub

    Public Sub RunGeneration()
        For i As Integer = 0 To NeuralNetworks.Count - 1
            outputs = NeuralNetworks(i).PropogateForward(inputs)
        Next
    End Sub

End Class
