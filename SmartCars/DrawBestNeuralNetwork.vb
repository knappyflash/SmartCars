Public Class DrawBestNeuralNetwork
    Public NeuralNetworkBitmap As New Bitmap(300, 500)
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
            g.Clear(Color.FromArgb(200, 255, 255, 255))
            For Each input As Double In Me.neuralNetwork.NeuronLayers(0).Inputs
                g.DrawString(input.ToString("F0"), myFont, Brushes.Black, x, y, myFormat)
                Me.y += 20
            Next
            g.DrawString("Sensors & Speed", myFont, Brushes.Black, x, y, myFormat)
            Me.y += 20
            g.DrawString("Gas, Break, Left, Right", myFont, Brushes.Black, x, y, myFormat)

            Me.x += 60
            Me.y = 0
            For i As Integer = 1 To Me.neuralNetwork.NeuronLayers.Length - 2
                For j As Integer = 0 To Me.neuralNetwork.NeuronLayers(i).Neurons.Length - 1
                    If Me.neuralNetwork.NeuronLayers(i).Neurons(j).OutputRelu < 0.001 Then
                        g.DrawString(Me.neuralNetwork.NeuronLayers(i).Neurons(j).OutputRelu.ToString("F0"), myFont, Brushes.Black, x, y, myFormat)
                    Else
                        g.DrawString(Me.neuralNetwork.NeuronLayers(i).Neurons(j).OutputRelu.ToString("F0"), myFont, Brushes.Red, x, y, myFormat)
                    End If
                    Me.y += 20
                Next
                Me.x += 60
                Me.y = 0
            Next


            Me.x = Me.NeuralNetworkBitmap.Width - 60
            Me.y = 0
            For Each neuron As Neuron In Me.neuralNetwork.NeuronLayers(Me.neuralNetwork.NeuronLayers.Length - 1).Neurons
                If neuron.OutputLinear > 0.5 Then
                    g.DrawString(neuron.OutputLinear.ToString("F0"), myFont, Brushes.Red, x, y, myFormat)
                Else
                    g.DrawString(neuron.OutputLinear.ToString("F0"), myFont, Brushes.Black, x, y, myFormat)
                End If

                Me.y += 20
            Next

            Me.x = 0
            Me.y = Me.NeuralNetworkBitmap.Height - 20
            Me.y -= 20
            g.DrawString($"Odometer: {Me.car.Odometer.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20

            Me.y -= 20
            g.DrawString($"y: {Me.car.posY.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            g.DrawString($"x: {Me.car.posX.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            Me.y -= 20

            g.DrawString($"y speed: {Me.car.GroundSpeedY.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            g.DrawString($"x speed: {Me.car.GroundSpeedX.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            g.DrawString($"GroundSpeed: {Me.car.GroundSpeed.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            Me.y -= 20


            g.DrawString($"Wall Distance: {Me.car.SensorValuesCurrentMin.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            g.DrawString($"Generation: {Me.geneticAlgorithm.Generation}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            g.DrawString($"CountDown: {TrackMap.countdownTime}", myFont, Brushes.Black, x, y, myFormat)
            Me.y -= 20
            g.DrawString($"Score: {Me.neuralNetwork.FitnessScore.ToString("F2")}", myFont, Brushes.Black, x, y, myFormat)

            Me.y -= 20
            Me.y -= 20
            g.DrawString($"Should Be Headed: {Me.car.ShouldBeHeading}", myFont, Brushes.Black, x, y, myFormat)
        End Using
    End Sub

End Class
