Public Class GeneticAlgorithm
    Public NeuralNetworks() As NeuralNetwork
    Public inputs() As Double
    Public outputs() As Double
    Public Sub New(
                  populationSize As Integer,
                  inputCount As Integer,
                  outputCount As Integer,
                  hiddenLayerCount As Integer,
                  NeuronsPerHiddenLayer As Integer)
        ReDim NeuralNetworks(populationSize - 1)
        ReDim inputs(inputCount - 1)
        ReDim outputs(outputCount - 1)
        For i = 0 To NeuralNetworks.Length - 1
            NeuralNetworks(i) = New NeuralNetwork(inputCount, outputCount, hiddenLayerCount, NeuronsPerHiddenLayer)
        Next
    End Sub

    Public Sub Iterate()
        For i As Integer = 0 To 0
            outputs = NeuralNetworks(i).PropogateForward(inputs)
        Next
    End Sub

End Class
