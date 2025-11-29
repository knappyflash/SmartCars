Public Class Sensor
    Public SensorValue As Integer
    Public sensorHead As New Rectangle(0, 0, 4, 4)
    Public minLength As Integer = 20
    Public length As Integer
    Public angleRadians As Double
    Public startX As Integer
    Public startY As Integer
    Public endX As Integer
    Public endY As Integer
    Public mapBitmap As Bitmap
    Public Sub DrawSensor(g As Graphics, BodyRect As Rectangle, angleDegrees As Double, angleOffsetDegrees As Double, isVisible As Boolean)
        length = minLength
        Me.angleRadians = (angleDegrees + angleOffsetDegrees) * Math.PI / 180
        Me.startX = BodyRect.X + BodyRect.Width \ 2
        Me.startY = BodyRect.Y + BodyRect.Height \ 2
        endX = CInt(startX + Math.Cos(angleRadians) * length)
        endY = CInt(startY + Math.Sin(angleRadians) * length)
        sensorHead.X = endX - 2
        sensorHead.Y = endY - 2

        If sensorHead.X < 0 Then sensorHead.X = 0
        If sensorHead.Y < 0 Then sensorHead.Y = 0

        Do Until Me.mapBitmap.GetPixel(sensorHead.X, sensorHead.Y).ToArgb <> -11711155
            Me.angleRadians = (angleDegrees + angleOffsetDegrees) * Math.PI / 180
            Me.startX = BodyRect.X + BodyRect.Width \ 2
            Me.startY = BodyRect.Y + BodyRect.Height \ 2
            endX = CInt(startX + Math.Cos(angleRadians) * length)
            endY = CInt(startY + Math.Sin(angleRadians) * length)
            sensorHead.X = endX - 2
            sensorHead.Y = endY - 2
            length += 2
        Loop

        If isVisible Then
            g.DrawLine(Pens.Blue, startX, startY, endX, endY)
            g.FillEllipse(Brushes.Yellow, sensorHead.X, sensorHead.Y, 4, 4)
        End If

        SensorValue = length
    End Sub
End Class
