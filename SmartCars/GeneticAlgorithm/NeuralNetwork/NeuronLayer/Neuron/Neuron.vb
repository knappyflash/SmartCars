Public Class Neuron
    Public ParentLayerId As Integer
    Public MyLayerId As Integer
    Public ChildLayerId As Integer
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

    Public Sub Backup()
        Me.BiasBackup = Me.Bias
        For i As Integer = 0 To Me.InputWeights.Length - 1
            Me.WeightsBackup(i) = Me.InputWeights(i)
        Next
    End Sub

    Public Sub Restore()
        Me.Bias = Me.BiasBackup
        For i As Integer = 0 To Me.InputWeights.Length - 1
            Me.InputWeights(i) = Me.WeightsBackup(i)
        Next
    End Sub

End Class
