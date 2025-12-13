Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Forms.AxHost
Imports Windows.Win32.System

Public Class Car

    Public Property TrackBitmap As Bitmap
        Get
            Return _trackBitmap
        End Get
        Set(value As Bitmap)
            _trackBitmap = value
            For i As Integer = 0 To Me.sensors.Length - 1
                Me.sensors(i).mapBitmap = _trackBitmap
            Next
        End Set
    End Property
    Public Enum InGear
        neutral = 0
        forward = 1
        reverse = 2
    End Enum

    Public Enum CorrectDirecton
        north = 0
        south = 1
        east = 2
        west = 3
    End Enum

    Public BodyColor As Color = Color.Red
    Public BodyBrush As New SolidBrush(BodyColor)
    Public AccelerationValue As Double
    Public Odometer As Double
    Public Body As PointF()
    Public _trackBitmap As Bitmap
    Public gasPedalPressed As Boolean
    Public breakPedalPressed As Boolean
    Public wheelTurnLeft As Boolean
    Public wheelTurnRight As Boolean
    Public gear As InGear = InGear.forward
    Public posX As Double
    Public posY As Double
    Public angleDegrees As Double
    Public angleRadians As Double
    Public maxSpeed As Double = 5

    Public BodyRect As New Rectangle(0, 0, 20, 10)
    Public sensors(4) As Sensor
    Public SensorValuesCurrentMin As Double
    Public sensorVisible As Boolean = False

    Public CanReceivePoint As Boolean

    Public ShouldBeHeading As CorrectDirecton = CorrectDirecton.east
    Public MadeFullLoop As Integer = 0

    Public GroundSpeed As Double
    Public GroundSpeedX As Double
    Public GroundSpeedY As Double
    Public Crashed As Boolean
    Public justReset As Boolean = False

    Private OdometerXY1 As New Point()
    Private OdometerXY2 As New Point()

    Private OdometerLastLoopSave As Double

    Public Sub New()
        For i As Integer = 0 To sensors.Length - 1
            sensors(i) = New Sensor
        Next
        Move()
    End Sub

    Public Sub Move()

        If gear = InGear.forward Then
            If gasPedalPressed Then
                AccelerationValue += 0.01
            End If
            If breakPedalPressed Then
                If AccelerationValue > 0 Then
                    AccelerationValue -= 0.05
                Else
                    AccelerationValue = 0
                End If
            End If
            If AccelerationValue > maxSpeed Then AccelerationValue = maxSpeed
        End If

        If wheelTurnLeft Then angleDegrees -= 1.5
        If wheelTurnRight Then angleDegrees += 1.5

        angleDegrees = Maths.DegreesModulo360(angleDegrees)

        Dim center As New PointF(BodyRect.X + BodyRect.Width / 2, BodyRect.Y + BodyRect.Height / 2)
        Dim dx As Double
        Dim dy As Double


        Body = {
            New PointF(BodyRect.Left, BodyRect.Top),
            New PointF(BodyRect.Right, BodyRect.Top),
            New PointF(BodyRect.Right, BodyRect.Bottom),
            New PointF(BodyRect.Left, BodyRect.Bottom)
        }
        For i As Integer = 0 To Body.Length - 1
            angleRadians = angleDegrees * Math.PI / 180
            Dim cosA As Double = Math.Cos(angleRadians)
            Dim sinA As Double = Math.Sin(angleRadians)

            dx = Body(i).X - center.X
            dy = Body(i).Y - center.Y

            Dim xNew As Double = center.X + (dx * cosA - dy * sinA)
            Dim yNew As Double = center.Y + (dx * sinA + dy * cosA)
            Body(i) = New PointF(CSng(xNew), CSng(yNew))
        Next

        dx = Math.Cos(angleRadians) * AccelerationValue
        dy = Math.Sin(angleRadians) * AccelerationValue

        Me.posX += dx
        Me.posY += dy

        Me.BodyRect.X = CInt(Me.posX)
        Me.BodyRect.Y = CInt(Me.posY)

        If (Me.ShouldBeHeading = CorrectDirecton.north) Then
            MadeFullLoop = 1
        ElseIf ((MadeFullLoop = 1) AndAlso (Me.ShouldBeHeading = CorrectDirecton.east)) Then
            MadeFullLoop = 2
        End If

        SetOdometer()
        killCounterDo()
        CrashDetector()

    End Sub

    Public Sub CrashDetector()
        If TrackBitmap Is Nothing Then Exit Sub
        If Me.TrackBitmap.GetPixel(Me.posX + 10, Me.posY + 5).ToArgb <> Color.FromArgb(77, 77, 77).ToArgb Then
            Me.Crashed = True
            Me.AccelerationValue = 0
        End If
    End Sub

    Public Sub Reset()
        Me.justReset = True
        Me.CanReceivePoint = False
        Me.Crashed = False
        Me.GroundSpeed = 0
        Me.GroundSpeedX = 0
        Me.GroundSpeedY = 0
        Me.posX = 90
        Me.posY = 90
        Me.AccelerationValue = 0
        Me.Odometer = 0
        Me.killCounter = 0
        Me.angleDegrees = 0
        Me.TrackBitmap = Me.TrackBitmap
    End Sub

    Public Sub DrawSensors(g As Graphics, isVisible As Boolean)
        Me.SensorValuesCurrentMin = 10000
        Dim myAngle As Double = -90
        For i As Integer = 0 To sensors.Length - 1
            sensors(i).DrawSensor(g, BodyRect, angleDegrees, Maths.DegreesModulo360(myAngle), isVisible)
            myAngle += 45
            If Me.sensors(i).SensorValue < Me.SensorValuesCurrentMin Then Me.SensorValuesCurrentMin = Me.sensors(i).SensorValue
        Next
    End Sub


    Private Sub SetOdometer()
        Static counter As Integer
        counter += 1
        If counter >= 100 Then
            Me.OdometerXY1.X = posX
            Me.OdometerXY1.Y = posY

            Me.GroundSpeedX = Me.OdometerXY1.X - Me.OdometerXY2.X
            Me.GroundSpeedY = Me.OdometerXY1.Y - Me.OdometerXY2.Y
            Me.GroundSpeed = Maths.GetDistance(Me.OdometerXY1.X, Me.OdometerXY1.Y, Me.OdometerXY2.X, Me.OdometerXY2.Y) * 100

            Me.OdometerXY2.X = Me.OdometerXY1.X
            Me.OdometerXY2.Y = Me.OdometerXY1.Y
            Me.Odometer += Me.GroundSpeed * 0.001

            If Me.justReset Then
                Me.justReset = False
                Me.GroundSpeedX = 0
                Me.GroundSpeedY = 0
                Me.GroundSpeed = 0
            End If
        End If
    End Sub

    Private killCounter As Integer
    Private Sub killCounterDo()
        If Me.GroundSpeed >= 80 Then
            Me.killCounter = 0
        End If
        If Me.killCounter > 200 Then
            Me.GroundSpeed = 0
            Me.Crashed = True
        End If
        Me.killCounter += 1
    End Sub


End Class
