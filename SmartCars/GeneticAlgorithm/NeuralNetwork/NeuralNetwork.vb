Public Class NeuralNetwork

    Public NeuronLayers() As NeuronLayer
    Public InputCount As Integer
    Public OutputCount As Integer
    Public HiddenLayerCount As Integer
    Public NeuronsPerHiddenLayer As Integer
    Public FitnessScore As Double
    Public FitnessScoreBest As Double

    'Setup a Brand New NeuralNetwork
    Public Sub New()

    End Sub
    Public Sub New(inputCount As Integer, outputCount As Integer, hiddenLayerCount As Integer, NeuronsPerHiddenLayer As Integer)
        Me.InputCount = inputCount
        Me.OutputCount = outputCount
        Me.HiddenLayerCount = hiddenLayerCount
        Me.NeuronsPerHiddenLayer = NeuronsPerHiddenLayer
        Me.CreateNewNeuralNetwork()
        Me.Randomize()
        Me.Backup()
    End Sub

    'Create a New Copy of an Existing NeuralNetwork
    Public Sub New(neuralNetwork As NeuralNetwork)
        Me.InputCount = neuralNetwork.InputCount
        Me.OutputCount = neuralNetwork.OutputCount
        Me.HiddenLayerCount = neuralNetwork.HiddenLayerCount
        Me.NeuronsPerHiddenLayer = neuralNetwork.NeuronsPerHiddenLayer
        Me.CreateNewNeuralNetwork()
    End Sub

    'Create a New Crossover NeuralNetwork From Two Existing Parents
    Public Sub New(neuralNetwork1 As NeuralNetwork, neuralNetwork2 As NeuralNetwork)
        Me.InputCount = neuralNetwork1.InputCount
        Me.OutputCount = neuralNetwork1.OutputCount
        Me.HiddenLayerCount = neuralNetwork1.HiddenLayerCount
        Me.NeuronsPerHiddenLayer = neuralNetwork1.NeuronsPerHiddenLayer
        Me.CreateNewNeuralNetwork()
        Me.Crossover(neuralNetwork2)
    End Sub

    Public Sub CreateNewNeuralNetwork()
        'Create NeuronLayers
        ReDim Me.NeuronLayers((Me.HiddenLayerCount - 1) + 2)
        For i As Integer = 0 To Me.NeuronLayers.Length - 1
            Me.NeuronLayers(i) = New NeuronLayer
        Next

        'Setup Input Layer
        ReDim Me.NeuronLayers(0).Inputs(Me.InputCount - 1)
        Me.NeuronLayers(0).ParentLayerId = -1
        Me.NeuronLayers(0).ChildLayerId = 1

        'Setup Hidden Layers
        For i As Integer = 1 To Me.NeuronLayers.Length - 2
            Me.NeuronLayers(i).ParentLayerId = i - 1
            Me.NeuronLayers(i).ChildLayerId = i + 1
            ReDim Me.NeuronLayers(i).Neurons(Me.NeuronsPerHiddenLayer - 1)
            For j = 0 To Me.NeuronLayers(i).Neurons.Length - 1
                Me.NeuronLayers(i).Neurons(j) = New Neuron
                Me.NeuronLayers(i).Neurons(j).ParentLayerId = i - 1
                Me.NeuronLayers(i).Neurons(j).ChildLayerId = i + 1
                If i = 1 Then
                    ReDim Me.NeuronLayers(i).Neurons(j).Inputs(Me.NeuronLayers(0).Inputs.Length - 1)
                    ReDim Me.NeuronLayers(i).Neurons(j).InputWeights(Me.NeuronLayers(0).Inputs.Length - 1)
                    ReDim Me.NeuronLayers(i).Neurons(j).WeightsBackup(Me.NeuronLayers(0).Inputs.Length - 1)
                Else
                    ReDim Me.NeuronLayers(i).Neurons(j).Inputs(Me.NeuronLayers(Me.NeuronLayers(i).Neurons(j).ParentLayerId).Neurons.Length - 1)
                    ReDim Me.NeuronLayers(i).Neurons(j).InputWeights(Me.NeuronLayers(Me.NeuronLayers(i).Neurons(j).ParentLayerId).Neurons.Length - 1)
                    ReDim Me.NeuronLayers(i).Neurons(j).WeightsBackup(Me.NeuronLayers(Me.NeuronLayers(i).Neurons(j).ParentLayerId).Neurons.Length - 1)
                End If
            Next
        Next

        'Setup Output Layer
        ReDim Me.NeuronLayers(Me.NeuronLayers.Length - 1).Neurons(Me.OutputCount - 1)
        Me.NeuronLayers(Me.NeuronLayers.Length - 1).ParentLayerId = Me.NeuronLayers.Length - 2
        Me.NeuronLayers(Me.NeuronLayers.Length - 1).ChildLayerId = -1
        For i As Integer = 0 To Me.NeuronLayers(Me.NeuronLayers.Length - 1).Neurons.Length - 1
            Me.NeuronLayers(Me.NeuronLayers.Length - 1).Neurons(i) = New Neuron
            Me.NeuronLayers(Me.NeuronLayers.Length - 1).Neurons(i).ParentLayerId = Me.NeuronLayers.Length - 2
            Me.NeuronLayers(Me.NeuronLayers.Length - 1).Neurons(i).ChildLayerId = -1
            ReDim Me.NeuronLayers(Me.NeuronLayers.Length - 1).Neurons(i).Inputs(Me.NeuronLayers(Me.NeuronLayers.Length - 2).Neurons.Length - 1)
            ReDim Me.NeuronLayers(Me.NeuronLayers.Length - 1).Neurons(i).InputWeights(Me.NeuronLayers(Me.NeuronLayers.Length - 2).Neurons.Length - 1)
            ReDim Me.NeuronLayers(Me.NeuronLayers.Length - 1).Neurons(i).WeightsBackup(Me.NeuronLayers(Me.NeuronLayers.Length - 2).Neurons.Length - 1)
        Next
    End Sub

    Public Sub Crossover(neuralNetwork2 As NeuralNetwork)
        ' Crossover whole nerons bias and weights togther
        For i As Integer = 1 To Me.NeuronLayers.Length - 1
            For j As Integer = 0 To Me.NeuronLayers(i).Neurons.Length - 1
                If Maths.RandomInt(0, 1) = 1 Then Me.NeuronLayers(i).Neurons(j).Bias = neuralNetwork2.NeuronLayers(i).Neurons(j).Bias
                For k As Integer = 0 To Me.NeuronLayers(i).Neurons(j).InputWeights.Length - 1
                    If Maths.RandomInt(0, 1) = 1 Then Me.NeuronLayers(i).Neurons(j).InputWeights(k) = neuralNetwork2.NeuronLayers(i).Neurons(j).InputWeights(k)
                Next
            Next
        Next
    End Sub

    Public Sub MutateRandom(percentage As Double)
        For i As Integer = 1 To Me.NeuronLayers.Length - 1
            For j As Integer = 0 To Me.NeuronLayers(i).Neurons.Length - 1
                If Maths.RandomDbl(0, 1) < percentage Then
                    Me.NeuronLayers(i).Neurons(j).Bias = Maths.RandomDbl(-1, 1)
                End If
                For k As Integer = 0 To Me.NeuronLayers(i).Neurons(j).InputWeights.Length - 1
                    If Maths.RandomDbl(0, 1) < percentage Then
                        Me.NeuronLayers(i).Neurons(j).InputWeights(k) = Maths.RandomDbl(-1, 1)
                    End If
                Next
            Next
        Next
    End Sub

    Public Sub MutateIncrease(percentage As Double)
        For i As Integer = 1 To Me.NeuronLayers.Length - 1
            For j As Integer = 0 To Me.NeuronLayers(i).Neurons.Length - 1
                If Maths.RandomDbl(0, 1) < percentage Then
                    Me.NeuronLayers(i).Neurons(j).Bias += Maths.RandomDbl(0.001, 1)
                End If
                For k As Integer = 0 To Me.NeuronLayers(i).Neurons(j).InputWeights.Length - 1
                    If Maths.RandomDbl(0, 1) < percentage Then
                        Me.NeuronLayers(i).Neurons(j).InputWeights(k) += Maths.RandomDbl(0.001, 1)
                    End If
                Next
            Next
        Next
    End Sub

    Public Sub MutateDecrease(percentage As Double)
        For i As Integer = 1 To Me.NeuronLayers.Length - 1
            For j As Integer = 0 To Me.NeuronLayers(i).Neurons.Length - 1
                If Maths.RandomDbl(0, 1) < percentage Then
                    Me.NeuronLayers(i).Neurons(j).Bias -= Maths.RandomDbl(0.001, 1)
                End If
                For k As Integer = 0 To Me.NeuronLayers(i).Neurons(j).InputWeights.Length - 1
                    If Maths.RandomDbl(0, 1) < percentage Then
                        Me.NeuronLayers(i).Neurons(j).InputWeights(k) -= Maths.RandomDbl(0.001, 1)
                    End If
                Next
            Next
        Next
    End Sub

    Public Sub Randomize()
        For i As Integer = 1 To Me.NeuronLayers.Length - 1
            For j As Integer = 0 To Me.NeuronLayers(i).Neurons.Length - 1
                Me.NeuronLayers(i).Neurons(j).Bias = Maths.RandomDbl(-1, 1)
                For k As Integer = 0 To Me.NeuronLayers(i).Neurons(j).InputWeights.Length - 1
                    Me.NeuronLayers(i).Neurons(j).InputWeights(k) = Maths.RandomDbl(-1, 1)
                Next
            Next
        Next
    End Sub

    Public Sub Backup()
        For i As Integer = 1 To Me.NeuronLayers.Length - 1
            For j As Integer = 0 To Me.NeuronLayers(i).Neurons.Length - 1
                Me.NeuronLayers(i).Neurons(j).Backup()
            Next
        Next
    End Sub

    Public Sub Restore()
        For i As Integer = 1 To Me.NeuronLayers.Length - 1
            For j As Integer = 0 To Me.NeuronLayers(i).Neurons.Length - 1
                Me.NeuronLayers(i).Neurons(j).Restore()
            Next
        Next
    End Sub

    Public Function PropogateForward(inputs() As Double) As Double()
        Dim outputs(Me.OutputCount - 1) As Double

        'inputs
        For i As Integer = 0 To Me.InputCount - 1
            Me.NeuronLayers(0).Inputs(i) = inputs(i)
        Next

        'layers
        For i As Integer = 1 To Me.NeuronLayers.Length - 1
            For j As Integer = 0 To Me.NeuronLayers(i).Neurons.Length - 1
                For k As Integer = 0 To Me.NeuronLayers(i).Neurons(j).Inputs.Length - 1
                    If i = 1 Then
                        Me.NeuronLayers(1).Neurons(j).Inputs(k) = Me.NeuronLayers(0).Inputs(k)
                    Else
                        Me.NeuronLayers(i).Neurons(j).Inputs(k) = Me.NeuronLayers(i - 1).Neurons(k).OutputRelu
                    End If
                Next
                Me.NeuronLayers(i).Neurons(j).ActivationFunction()
            Next
        Next

        'outputs
        For i As Integer = 0 To Me.OutputCount - 1
            outputs(i) = Me.NeuronLayers((Me.HiddenLayerCount - 1) + 2).Neurons(i).OutputLinear
        Next

        Return outputs
    End Function

End Class
