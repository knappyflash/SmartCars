Public Class GeneticAlgorithm
    Public NeuralNetworks() As NeuralNetwork
    Public Sub New(
                  populationSize As Integer,
                  inputCount As Integer,
                  outputCount As Integer,
                  hiddenLayerCount As Integer,
                  NeuronsPerHiddenLayer As Integer)
        ReDim NeuralNetworks(populationSize - 1)
        For i = 0 To NeuralNetworks.Length - 1
            NeuralNetworks(i) = New NeuralNetwork(inputCount, outputCount, hiddenLayerCount, NeuronsPerHiddenLayer)
        Next
        JsonParser.SaveToFile(Me.NeuralNetworks, $"{Application.StartupPath}\NeuralNetworksSave.json")
    End Sub



End Class
