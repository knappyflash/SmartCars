Public Enum NeuronLayerType
    input = 0
    hidden = 1
    output = 2
End Enum
Public Class NeuronLayer
    Public NeuronLayerType As NeuronLayerType
    Public ParentLayerId As Integer
    Public ChildLayerId As Integer
    Public Inputs() As Double
    Public Neurons() As Neuron

End Class
