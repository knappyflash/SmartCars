Public Class SmartCars
    Public GeneticAlgorithm As New GeneticAlgorithm(100, 6, 4, 1, 4)
    Public Cars(99) As Car

    Public inputs(5) As Double
    Public outputs(3) As Double
    Public StillAlive As Boolean



    Public Sub New()
        For i As Integer = 0 To Cars.Length - 1
            Me.Cars(i) = New Car
            Me.Cars(i).BodyColor = Color.FromArgb(Maths.RandomInt(0, 255), Maths.RandomInt(0, 255), Maths.RandomInt(0, 255))
            Me.Cars(i).BodyBrush.Color = Me.Cars(i).BodyColor
        Next
    End Sub

    Public Sub MoveCars()

        Me.StillAlive = False
        For i As Integer = 0 To Me.Cars.Length - 1

            If Cars(i).Crashed Then
                Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScore = Me.Cars(i).Odometer + Me.Cars(i).FitnessScoreValue
                Me.Cars(i).FitnessScoreValue = 0
                Continue For
            End If
            Me.StillAlive = True

            'INPUTS TO OUTPUTS'
            For j As Integer = 0 To Me.Cars(i).sensors.Length - 1
                Me.inputs(j) = Me.Cars(i).sensors(j).SensorValue
            Next
            Me.inputs(5) = Me.Cars(i).GroundSpeed
            Me.outputs = Me.GeneticAlgorithm.NeuralNetworks(i).PropogateForward(inputs)

            Me.Cars(i).FitnessScoreValue += (Me.Cars(i).SensorValuesCurrentMin * 0.01) + (Me.Cars(i).GroundSpeed * 0.001)
            If Me.Cars(i).wheelTurnRight Then Me.Cars(i).FitnessScoreValue += 0.00001

            If Me.outputs(0) > 0.5 Then
                Me.Cars(i).gasPedalPressed = True
            Else
                Me.Cars(i).gasPedalPressed = False
            End If

            If Me.outputs(1) > 0.5 Then
                Me.Cars(i).breakPedalPressed = True
            Else
                Me.Cars(i).breakPedalPressed = False
            End If

            If Me.outputs(2) > 0.5 Then
                Me.Cars(i).wheelTurnLeft = True
            Else
                Me.Cars(i).wheelTurnLeft = False
            End If

            If Me.outputs(3) > 0.5 Then
                Me.Cars(i).wheelTurnRight = True
            Else
                Me.Cars(i).wheelTurnRight = False
            End If

            Me.Cars(i).Move()

        Next
    End Sub
End Class
