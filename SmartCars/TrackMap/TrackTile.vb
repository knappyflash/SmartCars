Public Class TrackTile

    Public Img As Bitmap
    Public TileType As TrackImages.TrackTileType
    Public Rect As Rectangle
    Public ShouldDrawDot As Boolean = False

    Public Sub New(trackTileType As TrackImages.TrackTileType, x As Integer, y As Integer, Optional width As Integer = 160, Optional height As Integer = 160)
        SetTile(trackTileType, x, y, width, height)
    End Sub

    Public Sub SetTile(trackTileType As TrackImages.TrackTileType, x As Integer, y As Integer, Optional width As Integer = 160, Optional height As Integer = 160)
        Me.Rect.X = x
        Me.Rect.Y = y
        Me.Rect.Width = width
        Me.Rect.Height = height

        ChangeTileType(trackTileType)

    End Sub

    Public Sub ChangeTileType(trackTileType As TrackImages.TrackTileType)
        Me.TileType = trackTileType
        Select Case Me.TileType
            Case TrackImages.TrackTileType.grass
                Me.Img = TrackImages.trackGrass.Clone

            Case TrackImages.TrackTileType.strightVertical
                Me.Img = TrackImages.trackStright.Clone
                Me.Img.RotateFlip(RotateFlipType.Rotate90FlipNone)

            Case TrackImages.TrackTileType.strightHorizontal
                Me.Img = TrackImages.trackStright.Clone

            Case TrackImages.TrackTileType.turnTopToRight
                Me.Img = TrackImages.trackTurn.Clone
                Me.Img.RotateFlip(RotateFlipType.Rotate180FlipNone)

            Case TrackImages.TrackTileType.turnTopToLeft
                Me.Img = TrackImages.trackTurn.Clone
                Me.Img.RotateFlip(RotateFlipType.Rotate90FlipNone)

            Case TrackImages.TrackTileType.turnBottomToRight
                Me.Img = TrackImages.trackTurn.Clone
                Me.Img.RotateFlip(RotateFlipType.Rotate270FlipNone)

            Case TrackImages.TrackTileType.turnBottomToLeft
                Me.Img = TrackImages.trackTurn.Clone
        End Select
    End Sub

End Class
