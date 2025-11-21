Public Class GraphicsPainter
    Public Shared Function InvertColor(inputColor As Color) As Color
        Return Color.FromArgb(inputColor.A,
                          255 - inputColor.R,
                          255 - inputColor.G,
                          255 - inputColor.B)
    End Function

End Class
