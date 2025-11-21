Public Class Maths

    Public Shared rnd As New Random()
    Public Shared Function RadiansFromDegreesConvert(angleDegrees As Double) As Double
        Return angleDegrees * Math.PI / 180
    End Function

    Public Shared Function DegreesModulo360(angleDegrees As Double) As Double
        Return (angleDegrees + 360) Mod 360
    End Function
    Public Shared Function RandomInt(lowAmt As Integer, HighAmt As Integer) As Integer
        Return rnd.NextDouble() * (HighAmt - lowAmt) + lowAmt
    End Function
    Public Shared Function RandomDbl(lowAmt As Double, HighAmt As Double) As Double
        Return rnd.NextDouble() * (HighAmt - lowAmt) + lowAmt
    End Function
    Public Shared Function Sigmoid(x As Double) As Double
        Return 1 / (1 + Math.Exp(-x))
    End Function

    Public Shared Function SigmoidDerivative(x As Double) As Double
        Return x * (1 - x)
    End Function

    Public Shared Function GetDistance(x1 As Double, y1 As Double, x2 As Double, y2 As Double) As Double
        Dim dx As Double = x2 - x1
        Dim dy As Double = y2 - y1
        Return Math.Sqrt(dx * dx + dy * dy)
    End Function

End Class
