Imports System.Drawing
Imports System.Net.Security
Imports System.Windows.Automation

Public Class TrackMap

    Public car As New Car

    Public TrackBitmap As Bitmap
    Public TrackTiles(,) As TrackTile

    Dim widthTileCount As Integer = 11
    Dim heightTileCount As Integer = 6

    Private Sub track_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = widthTileCount * 160
        Me.Height = heightTileCount * 160
        Me.TrackBitmap = New Bitmap(widthTileCount * 160, widthTileCount * 160)
        Me.Setup()
        Me.Timer1.Start()
    End Sub
    Private Sub track_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Me.MeDraw(e.Graphics)
    End Sub

    Private Sub Setup()
        Me.ClearMap()
        Me.CreateTrack()
        Me.TrackToBitmap()
        Me.car.posX = 90
        Me.car.posY = 90
        car.trackBitmap = Me.TrackBitmap
    End Sub

    Private Sub ClearMap()
        ReDim Me.TrackTiles(widthTileCount - 1, heightTileCount - 1)
        For x As Integer = 0 To widthTileCount - 1
            For y As Integer = 0 To heightTileCount - 1
                Me.TrackTiles(x, y) = New TrackTile(TrackImages.TrackTileType.grass, x * 160, y * 160, 160, 160)
            Next
        Next
    End Sub

    Private Sub CreateTrack()

        Me.TrackTiles(0, 0).ChangeTileType(TrackImages.TrackTileType.turnBottomToRight)

        Dim x As Integer = 0
        Dim y As Integer = 0

        'Head Down and Right
        Do Until (x = widthTileCount - 1) Or (y = heightTileCount - 1)
            If TrackTiles(x, y).TileType = TrackImages.TrackTileType.strightVertical Then
                y += 1
                Select Case Maths.RandomInt(0, 1)
                    Case 0
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                    Case 1
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
                End Select

            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.strightHorizontal Then
                x += 1
                Select Case Maths.RandomInt(0, 1)
                    Case 0
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                    Case 1
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)

                End Select

            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnTopToRight Then
                x += 1
                Select Case Maths.RandomInt(0, 1)
                    Case 0
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToLeft)
                    Case 1
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                End Select

            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnTopToLeft Then
                y -= 1
                Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToRight)


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnBottomToRight Then
                x += 1
                Select Case Maths.RandomInt(0, 1)
                    Case 0
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                    Case 1
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                End Select

            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnBottomToLeft Then
                y += 1
                Select Case Maths.RandomInt(0, 1)
                    Case 0
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                    Case 1
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
                End Select

            End If
        Loop


        'Hit Edge Turn Back
        If x = widthTileCount - 1 Then Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
        Do Until (y = heightTileCount - 1)
            y += 1
            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
        Loop
        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToLeft)



        'Head Back Home
        Do Until x <= 2

            If Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.strightVertical Then
                y -= 1
                If Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                Else
                    Select Case Maths.RandomInt(0, 1)
                        Case 0
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
                        Case 1
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                    End Select
                End If

            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.strightHorizontal Then
                x -= 1
                If Me.TrackTiles(x, y).TileType <> TrackImages.TrackTileType.grass Then
                    x += 1
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToRight)
                    y += 1
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToLeft)


                ElseIf Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                Else
                    Select Case Maths.RandomInt(0, 1)
                        Case 0
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                        Case 1
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                    End Select
                End If

            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnTopToRight Then
                y -= 1
                If Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                Else
                    Select Case Maths.RandomInt(0, 1)
                        Case 0
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
                        Case 1
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                    End Select
                End If

            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnTopToLeft Then
                x -= 1
                If Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                Else
                    Select Case Maths.RandomInt(0, 1)
                        Case 0
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                        Case 1
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                    End Select
                End If


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnBottomToRight Then
                'y += 1
                'TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnBottomToLeft Then
                x -= 1
                If Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                Else
                    Select Case Maths.RandomInt(0, 1)
                        Case 0
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                        Case 1
                            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                    End Select
                End If

            End If
        Loop

        'Creep up to start x0,y0
        Do Until x = 0
            If Me.TrackTiles(x + 1, y).TileType = TrackImages.TrackTileType.grass Then
                Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToLeft)
            Else
                Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)
            End If


            x -= 1
        Loop
        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
        y -= 1
        Do Until y = 0
            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
            y -= 1
        Loop

    End Sub

    Public Sub TrackToBitmap()
        Using g As Graphics = Graphics.FromImage(TrackBitmap)
            For x As Integer = 0 To widthTileCount - 1
                For y As Integer = 0 To heightTileCount - 1
                    g.DrawImage(Me.TrackTiles(x, y).Img, Me.TrackTiles(x, y).Rect.X, Me.TrackTiles(x, y).Rect.Y, Me.TrackTiles(x, y).Rect.Width, Me.TrackTiles(x, y).Rect.Height)
                    If Me.TrackTiles(x, y).ShouldDrawDot Then g.FillEllipse(Brushes.Blue, Me.TrackTiles(x, y).Rect.X, Me.TrackTiles(x, y).Rect.Y, 20, 20)
                Next
            Next
        End Using
    End Sub

    Private Sub MeUpdate()
        Me.car.Move()
        If Me.car.Crashed Then
            Debug.WriteLine($"{Now} Crashed!")
            Me.ClearMap()
            Me.CreateTrack()
            Me.TrackToBitmap()
            Me.car.Reset()
        End If
    End Sub

    Private Sub MeDraw(g As Graphics)

        g.DrawImage(TrackBitmap, 0, 0)
        g.FillPolygon(Brushes.Red, car.Body)
        Me.car.DrawSensors(g)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.MeUpdate()
        Me.Invalidate()
    End Sub

    Private Sub TrackMap_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.D Then car.wheelTurnRight = True
        If e.KeyCode = Keys.A Then car.wheelTurnLeft = True
        If e.KeyCode = Keys.W Then car.gasPedalPressed = True
        If e.KeyCode = Keys.S Then car.breakPedalPressed = True
    End Sub

    Private Sub TrackMap_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.D Then car.wheelTurnRight = False
        If e.KeyCode = Keys.A Then car.wheelTurnLeft = False
        If e.KeyCode = Keys.W Then car.gasPedalPressed = False
        If e.KeyCode = Keys.S Then car.breakPedalPressed = False
    End Sub

    Private Sub TrashCan()
        'Private trackStright As Image = Image.FromFile($"{Application.StartupPath}\track\stright.png")
        'Private trackTurn As Image = Image.FromFile($"{Application.StartupPath}\track\turn.png")
        'Dim x As Integer = 100
        'Dim y As Integer = 100

        'g.FillRectangle(New SolidBrush(Color.FromArgb(0, 0, 255)), New Rectangle(x, y, 134, 134))
        'g.DrawImage(trackStright, x, y, 108, 108)
        'g.DrawImage(trackStright, x + 26, y, 108, 108)

        'If Maths.RandomInt(0, 10) = 1 Then flipFlop = Not flipFlop
        'If flipFlop Then g.DrawImage(trackStright2, x, y, 134, 134)

        'g.FillRectangle(New SolidBrush(Color.FromArgb(0, 0, 255)), New Rectangle(x + 134, y, 134, 134))
        'g.DrawImage(trackTurn, x + 134, 6 + y, 128, 128)

        'If Maths.RandomInt(0, 10) = 1 Then flipFlop = Not flipFlop
        'If flipFlop Then g.DrawImage(trackTurn2, x + 134, y, 134, 134)

        'Dim bmp As New Bitmap(134, 134)
        'Using g As Graphics = Graphics.FromImage(bmp)
        '    Dim x As Integer = 0
        '    Dim y As Integer = 0
        '    g.FillRectangle(New SolidBrush(Color.FromArgb(96, 142, 66)), New Rectangle(x, y, 134, 134))
        '    'g.DrawImage(trackStright, x, y, 108, 108)
        '    'g.DrawImage(trackStright, x + 26, y, 108, 108)

        '    'g.FillRectangle(New SolidBrush(Color.FromArgb(96, 142, 66)), New Rectangle(x + 134, y, 134, 134))
        '    g.DrawImage(trackTurn, x, 6 + y, 128, 128)
        'End Using
        'bmp.Save($"{Application.StartupPath}\track\turn2.png", Imaging.ImageFormat.Png)
        'trackTurn2 = Image.FromFile($"{Application.StartupPath}\track\turn2.png")




        'Dim bmp As New Bitmap(134 + 26, 134 + 26)
        'Using g As Graphics = Graphics.FromImage(bmp)
        '    g.FillRectangle(New SolidBrush(Color.FromArgb(96, 142, 66)), New Rectangle(0, 0, 134 + 26, 134 + 26))
        '    g.DrawImage(tiles(0, 0), 0, 26)
        '    g.DrawImage(tiles(0, 0), 26, 26)
        'End Using
        'bmp.Save($"{Application.StartupPath}\track\stright3.png", Imaging.ImageFormat.Png)
        'trackStright3 = Image.FromFile($"{Application.StartupPath}\track\stright3.png")


        'If Maths.RandomInt(0, 1) = 0 Then
        '    g.FillRectangle(New SolidBrush(Color.Blue), New Rectangle(0, 0, 134 + 26, 134 + 26))
        '    g.DrawImage(tiles(0, 0), 0, 26)
        '    g.DrawImage(tiles(0, 0), 26, 26)
        'Else
        '    g.DrawImage(trackStright3, 0, 0)
        'End If

        'Dim bmp As New Bitmap(160, 160)
        'Using g As Graphics = Graphics.FromImage(bmp)
        '    g.FillRectangle(New SolidBrush(Color.FromArgb(96, 142, 66)), New Rectangle(0, 0, 160, 160))
        '    g.DrawImage(trackTurn2, 0, 26)
        'End Using
        'bmp.Save($"{Application.StartupPath}\track\turn3.png", Imaging.ImageFormat.Png)
        'trackTurn3 = Image.FromFile($"{Application.StartupPath}\track\turn3.png")

    End Sub


End Class