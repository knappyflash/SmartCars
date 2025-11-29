Imports System.Windows
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

    Public BodyColor As Color
    Public Speed As Double
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
    Public maxSpeed As Double = 2

    Public BodyRect As New Rectangle(0, 0, 20, 10)
    Public sensors(10) As Sensor

    Public GroundSpeed As Double
    Public Crashed As Boolean

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
                Speed += 0.01
            End If
            If breakPedalPressed Then
                If Speed > 0 Then
                    Speed -= 0.05
                Else
                    Speed = 0
                End If
            End If
            If Speed > maxSpeed Then Speed = maxSpeed
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

        dx = Math.Cos(angleRadians) * Speed
        dy = Math.Sin(angleRadians) * Speed

        Me.posX += dx
        Me.posY += dy

        Me.BodyRect.X = CInt(Me.posX)
        Me.BodyRect.Y = CInt(Me.posY)

        SetOdometer()
        killCounterDo()
        CrashDetector()

    End Sub

    Public Sub CrashDetector()
        If TrackBitmap Is Nothing Then Exit Sub
        If Me.TrackBitmap.GetPixel(Me.posX + 10, Me.posY + 5).ToArgb <> Color.FromArgb(77, 77, 77).ToArgb Then
            Me.Crashed = True
            Me.Speed = 0
        End If
    End Sub

    Public Sub Reset()
        Me.Crashed = False
        Me.posX = 90
        Me.posY = 90
        Me.Speed = 0
        Me.Odometer = 0
        Me.killCounter = 0
        Me.angleDegrees = 0
        Me.TrackBitmap = Me.TrackBitmap
    End Sub

    Public Sub DrawSensors(g As Graphics, isVisible As Boolean)
        Dim myAngle As Double = -100
        For i As Integer = 0 To sensors.Length - 1
            sensors(i).DrawSensor(g, BodyRect, angleDegrees, Maths.DegreesModulo360(myAngle), isVisible)
            myAngle += 20
        Next
    End Sub


    Private Sub SetOdometer()
        Static counter As Integer
        counter += 1
        If counter >= 100 Then
            Me.OdometerXY1.X = posX
            Me.OdometerXY1.Y = posY
            Me.GroundSpeed = Maths.GetDistance(Me.OdometerXY1.X, Me.OdometerXY1.Y, Me.OdometerXY2.X, Me.OdometerXY2.Y)
            Me.OdometerXY2.X = Me.OdometerXY1.X
            Me.OdometerXY2.Y = Me.OdometerXY1.Y
            Me.Odometer += Me.GroundSpeed
        End If
    End Sub

    Private killCounter As Integer
    Private Sub killCounterDo()
        If Me.GroundSpeed >= 0.8 Then
            Me.killCounter = 0
        End If
        If Me.killCounter > 200 Then
            Me.GroundSpeed = 0
            Me.Crashed = True
        End If
        Me.killCounter += 1
        If Me.Odometer > 4000 Then Me.Crashed = True
    End Sub


End Class
