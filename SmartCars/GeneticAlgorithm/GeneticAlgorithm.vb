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
            Me.NeuralNetworks(i).FitnessScoreBest -= (Me.NeuralNetworks(i).FitnessScoreBest * 0.001)
        Next

        Me.KillBadPerformers()
        Clones()
        Me.Mutations()
        If Me.NeuralNetworks(0).FitnessScoreBest < 400 Then Me.Randomize()
        Me.Generation += 1
    End Sub

    Public Sub SortNeuralNetworksByFitness()
        Dim sortedNeuralNetworks = Me.NeuralNetworks.OrderByDescending(Function(nn) nn.FitnessScoreBest).ToList
        Me.NeuralNetworks = sortedNeuralNetworks
    End Sub

    Public Sub KillBadPerformers()
        Me.NeuralNetworks.RemoveRange(9, 90)
    End Sub

    Public Sub Clones()
        For i As Integer = 10 To 99
            Me.NeuralNetworks.Add(New NeuralNetwork(Me.NeuralNetworks(0)))
        Next i
    End Sub

    Public Sub Mutations()
        For i As Integer = 10 To Me.PopulationSize - 1
            Me.NeuralNetworks(i).MutateOnlyOneThing()
        Next
    End Sub

    Public Sub Randomize()
        For i As Integer = 1 To Me.PopulationSize - 1
            Me.NeuralNetworks(i).Randomize()
        Next
    End Sub

End Class
