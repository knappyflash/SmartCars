Public Class SmartCars
    Public GeneticAlgorithm As New GeneticAlgorithm(100, 6, 2, 2, 2)
    Public Cars(99) As Car

    Public inputs(5) As Double
    Public outputs(3) As Double
    Public StillAlive As Boolean


    Public Sub New()
        For i As Integer = 0 To Cars.Length - 1
            Me.Cars(i) = New Car
            Me.Cars(i).BodyColor = Color.FromArgb(255 * (i / 100), 255 * ((100 - i) / 100), 0)
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


            'INPUTS TO OUTPUTS'
            For j As Integer = 0 To Me.Cars(i).sensors.Length - 1
                Me.inputs(j) = Me.Cars(i).sensors(j).SensorValue
            Next
            Me.inputs(5) = Me.Cars(i).GroundSpeed
            Me.outputs = Me.GeneticAlgorithm.NeuralNetworks(i).PropogateForward(inputs)


            'Fitness Evaluation
            If Me.Cars(i).CanReceivePoint Then
                Me.GeneticAlgorithm.NeuralNetworks(i).FitnessScoreValue += (Me.Cars(i).SensorValuesCurrentMin * 0.1) + (Me.Cars(i).GroundSpeed * 0.00001)
            End If

            ''Help Prevent turning around
            For Each tile As TrackTile In TrackMap.HeadEastTiles
                If (Me.Cars(i).posX > tile.Rect.X) And
                        (Me.Cars(i).posX < tile.Rect.X + tile.Rect.Width) And
                        (Me.Cars(i).posY > tile.Rect.Y) And
                        (Me.Cars(i).posY < tile.Rect.Y + tile.Rect.Height) Then
                    Me.Cars(i).ShouldBeHeading = Car.CorrectDirecton.east
                End If
            Next
            For Each tile As TrackTile In TrackMap.HeadSouthTiles
                If (Me.Cars(i).posX > tile.Rect.X) And
                        (Me.Cars(i).posX < tile.Rect.X + tile.Rect.Width) And
                        (Me.Cars(i).posY > tile.Rect.Y) And
                        (Me.Cars(i).posY < tile.Rect.Y + tile.Rect.Height) Then
                    Me.Cars(i).ShouldBeHeading = Car.CorrectDirecton.south
                End If
            Next
            For Each tile As TrackTile In TrackMap.HeadWestTiles
                If (Me.Cars(i).posX > tile.Rect.X) And
                        (Me.Cars(i).posX < tile.Rect.X + tile.Rect.Width) And
                        (Me.Cars(i).posY > tile.Rect.Y) And
                        (Me.Cars(i).posY < tile.Rect.Y + tile.Rect.Height) Then
                    Me.Cars(i).ShouldBeHeading = Car.CorrectDirecton.west
                End If
            Next
            For Each tile As TrackTile In TrackMap.HeadNorthTiles
                If (Me.Cars(i).posX > tile.Rect.X) And
                        (Me.Cars(i).posX < tile.Rect.X + tile.Rect.Width) And
                        (Me.Cars(i).posY > tile.Rect.Y) And
                        (Me.Cars(i).posY < tile.Rect.Y + tile.Rect.Height) Then
                    Me.Cars(i).ShouldBeHeading = Car.CorrectDirecton.north
                End If
            Next

            If ((Me.Cars(i).ShouldBeHeading = Car.CorrectDirecton.east) And (Me.Cars(i).GroundSpeedX < -1.0)) Or
                ((Me.Cars(i).ShouldBeHeading = Car.CorrectDirecton.south) And (Me.Cars(i).GroundSpeedY < -1.0)) Or
                ((Me.Cars(i).ShouldBeHeading = Car.CorrectDirecton.west) And (Me.Cars(i).GroundSpeedX > 1.0)) Or
                ((Me.Cars(i).ShouldBeHeading = Car.CorrectDirecton.north) And (Me.Cars(i).GroundSpeedY > 1.0)) Then
                Me.Cars(i).CanReceivePoint = False
                Me.Cars(i).Crashed = True
            Else
                Me.Cars(i).CanReceivePoint = True
            End If

            If (Me.Cars(i).Odometer < 200) And
                (Me.Cars(i).posX < 300) Then
                Me.Cars(i).CanReceivePoint = False
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
