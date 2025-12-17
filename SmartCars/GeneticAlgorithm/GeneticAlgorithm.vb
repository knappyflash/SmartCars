Public Class GeneticAlgorithm
    Public NeuralNetworks As New List(Of NeuralNetwork)
    Public PopulationSize As Integer
    Public Generation As Integer = 0
    Public Sub New(
                  populationSize As Integer,
                  inputCount As Integer,
                  outputCount As Integer,
                  hiddenLayerCount As Integer,
                  NeuronsPerHiddenLayer As Integer)
        Me.PopulationSize = populationSize
        For i = 0 To populationSize - 1
            NeuralNetworks.Add(New NeuralNetwork(inputCount, outputCount, hiddenLayerCount, NeuronsPerHiddenLayer))
        Next
    End Sub

    Public Sub NextGeneration()
        'Update Best Fitness
        For i As Integer = 0 To Me.NeuralNetworks.Count - 1
            If Me.NeuralNetworks(i).FitnessScore > Me.NeuralNetworks(i).FitnessScoreBest Then
                Me.NeuralNetworks(i).FitnessScoreBest = Me.NeuralNetworks(i).FitnessScore
            End If
        Next
        Me.SortNeuralNetworksByFitness()
        ''Decay To Prevent Stagnation
        For i As Integer = 0 To Me.NeuralNetworks.Count - 1
            Me.NeuralNetworks(i).FitnessScoreLastCycle = 0
            Me.NeuralNetworks(i).FitnessScore = 0
            Me.NeuralNetworks(i).FitnessScoreBest -= (Me.NeuralNetworks(i).FitnessScoreBest * 0.1)
        Next

        Me.KillBadPerformers(1, 99)
        Me.Clones(0, 99)
        Me.Mutations(1, 99)
        If Me.NeuralNetworks(0).FitnessScoreBest < 600 Then Me.Randomize(0, 99)
        Me.Generation += 1
    End Sub

    Public Sub SortNeuralNetworksByFitness()
        Dim sortedNeuralNetworks = Me.NeuralNetworks.OrderByDescending(Function(nn) nn.FitnessScoreBest).ToList
        Me.NeuralNetworks = sortedNeuralNetworks
    End Sub

    Public Sub KillBadPerformers(indexStart As Integer, count As Integer)
        Me.NeuralNetworks.RemoveRange(indexStart, count)
    End Sub

    Public Sub Clones(sourceIndex As Integer, count As Integer)
        For i As Integer = 1 To count
            Me.NeuralNetworks.Add(New NeuralNetwork(Me.NeuralNetworks(sourceIndex)))
        Next i
    End Sub

    Public Sub Mutations(indexStart As Integer, indexEnd As Integer)
        For i As Integer = indexStart To indexEnd
            Me.NeuralNetworks(i).MutateOnlyOneThing()
        Next
    End Sub

    Public Sub Randomize(indexStart As Integer, indexEnd As Integer)
        For i As Integer = indexStart To indexEnd
            Me.NeuralNetworks(i).Randomize()
        Next
    End Sub

End Class
