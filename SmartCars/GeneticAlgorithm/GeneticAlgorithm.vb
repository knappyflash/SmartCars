Public Class GeneticAlgorithm
    Public NeuralNetworks As New List(Of NeuralNetwork)
    Public PopulationSize As Integer
    Public Generation As Integer = 0
    Public GenerationCounter As Integer
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

        If Me.GenerationCounter >= 1 Then
            Me.KillBadPerformers()
            Me.Clones()
            Me.Crossovers()
            Me.Generation += 1
            Me.GenerationCounter = 0
        End If
        Me.Mutations()
        Me.GenerationCounter = Me.GenerationCounter + 1
    End Sub

    Public Sub SortNeuralNetworksByFitness()
        Dim sortedNeuralNetworks = Me.NeuralNetworks.OrderByDescending(Function(nn) nn.FitnessScoreBest).ToList
        Me.NeuralNetworks = sortedNeuralNetworks
    End Sub

    Public Sub KillBadPerformers()
        Me.NeuralNetworks.RemoveRange(10, 90)
    End Sub

    Public Sub Clones()
        For i As Integer = 1 To 45
            Me.NeuralNetworks.Add(New NeuralNetwork(Me.NeuralNetworks(0)))
        Next i
    End Sub

    Public Sub Crossovers()
        For i As Integer = 1 To 45
            Me.NeuralNetworks.Add(New NeuralNetwork(Me.NeuralNetworks(Maths.RandomInt(0, 9)), Me.NeuralNetworks(Maths.RandomInt(0, 9))))
        Next i
    End Sub

    Public Sub Mutations()
        Dim rndNum As Integer
        For i As Integer = 1 To Me.PopulationSize - 1

            rndNum = Maths.RandomInt(0, 100)

            If (rndNum > 0) And (rndNum < 89) Then
                Me.NeuralNetworks(i).MutateOnlyOneThing()
            Else
                Me.NeuralNetworks(i).Randomize()
            End If

            'If (rndNum > 0) And (rndNum < 39) Then
            '    Me.NeuralNetworks(i).MutateIncrease(Maths.RandomDbl(0.01, 1))
            'ElseIf (rndNum > 40) And (rndNum < 79) Then
            '    Me.NeuralNetworks(i).MutateDecrease(Maths.RandomDbl(0.01, 1))
            'ElseIf (rndNum > 80) And (rndNum < 89) Then
            '    Me.NeuralNetworks(i).MutateRandom(Maths.RandomDbl(0.01, 1))
            'ElseIf (rndNum > 90) And (rndNum < 100) Then
            '    Me.NeuralNetworks(i).Randomize()
            'End If

        Next
    End Sub

End Class
