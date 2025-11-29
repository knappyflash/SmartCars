Public Class TrackImages
    Public Enum TrackTileType
        grass = -1
        strightVertical = 0
        strightHorizontal = 1
        turnTopToRight = 2
        turnTopToLeft = 3
        turnBottomToRight = 4
        turnBottomToLeft = 5
    End Enum

    Public Shared trackStright As New Bitmap($"{Application.StartupPath}\track\stright.png")
    Public Shared trackTurn As New Bitmap($"{Application.StartupPath}\track\turn.png")
    Public Shared trackGrass As New Bitmap($"{Application.StartupPath}\track\grass.png")

    Public Shared grassBitmap As New Bitmap($"{Application.StartupPath}\track\grass.png")
    Public Shared strightVerticalBitmap As New Bitmap($"{Application.StartupPath}\track\stright.png")
    Public Shared strightHorizontalBitmap As New Bitmap($"{Application.StartupPath}\track\stright.png")
    Public Shared turnTopToRightBitmap As New Bitmap($"{Application.StartupPath}\track\turn.png")
    Public Shared turnTopToLeftBitmap As New Bitmap($"{Application.StartupPath}\track\turn.png")
    Public Shared turnBottomToRightBitmap As New Bitmap($"{Application.StartupPath}\track\turn.png")
    Public Shared turnBottomToLeftBitmap As New Bitmap($"{Application.StartupPath}\track\turn.png")

    Shared Sub New()
        strightVerticalBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone)
        turnTopToRightBitmap.RotateFlip(RotateFlipType.Rotate180FlipNone)
        turnTopToLeftBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone)
        turnBottomToRightBitmap.RotateFlip(RotateFlipType.Rotate270FlipNone)
    End Sub

End Class
