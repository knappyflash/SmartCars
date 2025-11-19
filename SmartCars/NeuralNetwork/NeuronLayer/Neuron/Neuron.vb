Public Class Neuron
    Public id As Integer
    Public ParentLayer As NeuronLayer
    Public ChildLayer As NeuronLayer
    Public Bias As Double
    Public Inputs() As Double
    Public InputWeights() As Double
    Public BiasBackup As Double
    Public WeightsBackup() As Double
    Public OutputLinear As Double
    Public OutputRelu As Double
    Public OutputSoftMax As Double

    Public Sub ActivationFunction()
        For i = 0 To Me.Inputs.Length - 1
            OutputLinear += Me.Inputs(i) * InputWeights(i)
        Next
        OutputLinear += Bias
        OutputRelu = Math.Max(0, OutputLinear)
    End Sub

End Class
