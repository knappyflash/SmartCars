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
                Me.Img = TrackImages.grassBitmap

            Case TrackImages.TrackTileType.strightVertical
                Me.Img = TrackImages.strightVerticalBitmap

            Case TrackImages.TrackTileType.strightHorizontal
                Me.Img = TrackImages.strightHorizontalBitmap

            Case TrackImages.TrackTileType.turnTopToRight
                Me.Img = TrackImages.turnTopToRightBitmap

            Case TrackImages.TrackTileType.turnTopToLeft
                Me.Img = TrackImages.turnTopToLeftBitmap

            Case TrackImages.TrackTileType.turnBottomToRight
                Me.Img = TrackImages.turnBottomToRightBitmap

            Case TrackImages.TrackTileType.turnBottomToLeft
                Me.Img = TrackImages.turnBottomToLeftBitmap
        End Select
    End Sub

End Class
