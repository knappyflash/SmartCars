Public Class DrawBestNeuralNetwork
    Public NeuralNetworkBitmap As New Bitmap(225, 370)
    Public car As Car
    Public neuralNetwork As NeuralNetwork
    Public geneticAlgorithm As GeneticAlgorithm
    Public x As Integer = 0
    Public y As Integer = 0
    Public myFont As New Font("Arial", 12, FontStyle.Regular)
    Public myFormat As New StringFormat()   ' optional, for alignment
    Public Sub NeuralNetworkToBitmap()
        Me.x = 0
        Me.y = 0
        Using g As Graphics = Graphics.FromImage(Me.NeuralNetworkBitmap)
            g.Clear(Color.FromArgb(50, 200, 255, 200))
            For Each input As Double In Me.neuralNetwork.NeuronLayers(0).Inputs
                g.DrawString(input.ToString("F2"), myFont, Brushes.Black, x, y, myFormat)
                Me.y += 20
            Next
            g.DrawString("Sensors & Speed", myFont, Brushes.Black, x, y, myFormat)
            Me.y += 20
            g.DrawString("Gas, Break, Left, Right", myFont, Brushes.Black, x, y, myFormat)

            Me.x += 60
            Me.y = 0
            For i As Integer = 1 To Me.neuralNetwork.NeuronLayers.Length - 2
                For j As Integer = 0 To Me.neuralNetwork.NeuronLayers(i).Neurons.Length - 1
                    g.DrawString(Me.neuralNetwork.NeuronLayers(i).Neurons(j).OutputRelu.ToString("F2"), myFont, Brushes.Black, x, y, myFormat)
                    Me.y += 20
                Next
                Me.x += 60
                Me.y = 0
            Next


            Me.x = Me.NeuralNetworkBitmap.Width - 60
            Me.y = 0
            For Each neuron As Neuron In Me.neuralNetwork.NeuronLayers(Me.neuralNetwork.NeuronLayers.Length - 1).Neurons
                g.DrawString(neuron.OutputLinear.ToString("F2"), myFont, Brushes.Red, x, y, myFormat)
                Me.y += 20
            Next

            Me.x = 0
            Me.y = Me.NeuralNetworkBitmap.Height - 20
            g.DrawString($"FitnessScoreBest: {Me.neuralNetwork.FitnessScoreBest.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            g.DrawString($"Odometer: {Me.car.Odometer.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            g.DrawString($"GroundSpeed: {Me.car.GroundSpeed.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            g.DrawString($"Generation: {Me.geneticAlgorithm.Generation}:{Me.geneticAlgorithm.GenerationCounter}", myFont, Brushes.Black, x, y, myFormat)
        End Using
    End Sub

End Class
