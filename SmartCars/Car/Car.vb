Imports System.Windows
Imports System.Windows.Forms.AxHost
Imports Windows.Win32.System

Public Class Car

    Public ReadOnly Property Crashed As Boolean
        Get
            CrashDetector()
            Return _crashed
        End Get
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
    Public trackBitmap As Bitmap

    Public gasPedalPressed As Boolean
    Public breakPedalPressed As Boolean
    Public wheelTurnLeft As Boolean
    Public wheelTurnRight As Boolean
    Public gear As InGear = InGear.forward
    Public posX As Double
    Public posY As Double
    Public angleDegrees As Double
    Public angleRadians As Double

    Private _crashed As Boolean
    Private BodyRect As New Rectangle(0, 0, 20, 10)

    Public Sub New()
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

    End Sub

    Public Sub CrashDetector()
        If Me.trackBitmap.GetPixel(Me.posX - 5, Me.posY).ToArgb <> Color.FromArgb(77, 77, 77).ToArgb Then
            Me._crashed = True
        Else
            Me._crashed = False
        End If
    End Sub

    Public Sub Reset()
        Me.posX = 90
        Me.posY = 90
        Me.Speed = 0
        Me.angleDegrees = 0
        Me.trackBitmap = Me.trackBitmap
    End Sub

    Public Sub DrawSensors(g As Graphics)
        DrawSensor(g, 0, 0)
        DrawSensor(g, 0, 100)
        'DrawSensor()
        'DrawSensor()
        'DrawSensor()
        'DrawSensor()
        'DrawSensor()
        'DrawSensor()
        'DrawSensor()
        'DrawSensor()
    End Sub
    Public Sub DrawSensor(g As Graphics, x As Integer, y As Integer)
        Dim startX As Integer = Me.BodyRect.X + Me.BodyRect.Width \ 2
        Dim startY As Integer = Me.BodyRect.Y + Me.BodyRect.Height \ 2
        Dim endX As Integer = CInt(startX + Math.Cos(angleRadians + x) * 200)
        Dim endY As Integer = CInt(startY + Math.Sin(angleRadians + y) * 200)
        g.DrawLine(Pens.Blue, CInt(startX), CInt(startY), CInt(endX), CInt(endY))
    End Sub


End Class
