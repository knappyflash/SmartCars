Public Class SmartCars
    Public GeneticAlgorithm As New GeneticAlgorithm(100, 12, 4, 10, 4)
    Public Cars(99) As Car

    Public inputs(11) As Double
    Public outputs(3) As Double
    Public StillAlive As Boolean

    Public Sub New()
        For i As Integer = 0 To Cars.Length - 1
            Cars(i) = New Car
        Next
    End Sub

    Public Sub MoveCars()

        StillAlive = False
        For i As Integer = 0 To Me.GeneticAlgorithm.NeuralNetworks.Count - 1

            If Cars(i).Crashed Then
                Continue For
            End If
            StillAlive = True

            'INPUTS TO OUTPUTS'
            For j As Integer = 0 To Cars(i).sensors.Length - 1
                inputs(j) = Cars(i).sensors(j).SensorValue
            Next
            inputs(11) = Cars(i).Speed
            outputs = Me.GeneticAlgorithm.NeuralNetworks(i).PropogateForward(inputs)

            Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScore = Me.Cars(i).Odometer

            If outputs(0) > 0.5 Then
                Cars(i).gasPedalPressed = True
            Else
                Cars(i).gasPedalPressed = False
            End If

            'If outputs(1) > 0.5 Then
            '    Cars(i).breakPedalPressed = True
            'Else
            '    Cars(i).breakPedalPressed = False
            'End If

            If outputs(2) > 0.5 Then
                Cars(i).wheelTurnLeft = True
            Else
                Cars(i).wheelTurnLeft = False
            End If

            If outputs(3) > 0.5 Then
                Cars(i).wheelTurnRight = True
            Else
                Cars(i).wheelTurnRight = False
            End If

            Me.Cars(i).Move()

        Next
    End Sub
End Class
