Public Class SmartCars
    Public GeneticAlgorithm As New GeneticAlgorithm(100, 6, 2, 1, 2)
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
                If Me.Cars(i).CanReceivePoint Then
                    Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScoreLastCycle = Me.Cars(i).Odometer + Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScoreValue
                    Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScore = Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScoreLastCycle
                End If
                Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScoreValue = 0
                Continue For
            End If
            Me.StillAlive = True


            If Not Me.Cars(i).CanReceivePoint Then
                If (Me.Cars(i).posX > 270) And (Me.Cars(i).posY < 270) Then
                    Me.Cars(i).CanReceivePoint = True
                End If
            End If

            'INPUTS TO OUTPUTS'
            For j As Integer = 0 To Me.Cars(i).sensors.Length - 1
                Me.inputs(j) = Me.Cars(i).sensors(j).SensorValue
            Next
            Me.inputs(5) = Me.Cars(i).GroundSpeed
            Me.outputs = Me.GeneticAlgorithm.NeuralNetworks(i).PropogateForward(inputs)


            'Fitness Evaluation
            If Me.Cars(i).CanReceivePoint Then
                Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScoreValue += (Me.Cars(i).SensorValuesCurrentMin * 0.01) + (Me.Cars(i).GroundSpeed * 0.001)
                If TrackMap.countdownTime <= 1 Then Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScoreValue += 1000
            End If

            If Me.outputs(0) > 0.5 Then
                Me.Cars(i).gasPedalPressed = True
                Me.Cars(i).breakPedalPressed = False
            Else
                Me.Cars(i).breakPedalPressed = True
                Me.Cars(i).gasPedalPressed = False
            End If

            If Me.outputs(1) > 0.5 Then
                Me.Cars(i).wheelTurnLeft = True
                Me.Cars(i).wheelTurnRight = False
            Else
                Me.Cars(i).wheelTurnRight = True
                Me.Cars(i).wheelTurnLeft = False
            End If


            Me.Cars(i).Move()

        Next
    End Sub
End Class
