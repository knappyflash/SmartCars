Public Class GeneticAlgorithm
    Public NeuralNetworks As New List(Of NeuralNetwork)
    Public PopulationSize As Integer
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
        For i As Integer = 0 To Me.NeuralNetworks.Count - 1
            If Me.NeuralNetworks(i).FitnessScore > Me.NeuralNetworks(i).FitnessScoreBest Then
                Me.NeuralNetworks(i).FitnessScoreBest = Me.NeuralNetworks(i).FitnessScore
            End If
        Next
        Me.SortNeuralNetworksByFitness()
        Me.KillBadPerformers()
        Me.ClonesAndCrossovers()
        Me.Mutations()
    End Sub

    Public Sub SortNeuralNetworksByFitness()
        Dim sortedNeuralNetworks = NeuralNetworks.OrderByDescending(Function(nn) nn.FitnessScoreBest).ToList
        Me.NeuralNetworks = sortedNeuralNetworks
    End Sub

    Public Sub KillBadPerformers()
        Me.NeuralNetworks.RemoveRange(10, 90)
    End Sub

    Public Sub ClonesAndCrossovers()
        For i As Integer = 10 To Me.PopulationSize - 1
            If Maths.RandomInt(0, 1) = 1 Then
                Me.NeuralNetworks.Add(New NeuralNetwork(NeuralNetworks(Maths.RandomInt(0, 9))))
            Else
                Me.NeuralNetworks.Add(New NeuralNetwork(NeuralNetworks(Maths.RandomInt(0, 9)), NeuralNetworks(Maths.RandomInt(0, 9))))
            End If
        Next i
    End Sub

    Public Sub Mutations()
        Dim rndNum As Integer
        For i As Integer = 10 To Me.PopulationSize - 1
            rndNum = Maths.RandomInt(0, 3)
            Select Case rndNum
                Case 0
                    Me.NeuralNetworks(i).MutateIncrease(Maths.RandomDbl(0.01, 0.1))
                Case 1
                    Me.NeuralNetworks(i).MutateDecrease(Maths.RandomDbl(0.01, 0.1))
                Case 2
                    Me.NeuralNetworks(i).MutateRandom(Maths.RandomDbl(0.01, 0.1))
                Case 3
                    Me.NeuralNetworks(i).Randomize()
            End Select
        Next
    End Sub

End Class
