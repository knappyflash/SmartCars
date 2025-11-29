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
End Class
