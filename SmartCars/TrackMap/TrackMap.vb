Imports System.ComponentModel
Imports System.Drawing
Imports System.Net.Security
Imports System.Windows.Automation

Public Class TrackMap

    Public SmartCars As New SmartCars

    Public TrackBitmap As Bitmap
    Public TrackTiles(,) As TrackTile

    Public drawNn As New DrawBestNeuralNetwork

    Dim widthTileCount As Integer = 11
    Dim heightTileCount As Integer = 6

    Private Sub track_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = widthTileCount * 160
        Me.Height = heightTileCount * 160
        Me.BackColor = Color.FromArgb(96, 142, 66)
        ReDim Me.TrackTiles(widthTileCount - 1, heightTileCount - 1)
        Me.TrackBitmap = New Bitmap(widthTileCount * 160, widthTileCount * 160)
        Me.Setup()

        Dim answer As MsgBoxResult = MsgBox("Do you want to load your save file?", MsgBoxStyle.YesNo)
        If answer = MsgBoxResult.Yes Then Me.SmartCars.GeneticAlgorithm = JsonParser.LoadFromFile(Of GeneticAlgorithm)($"{Application.StartupPath}\save.json")

        drawNn.car = Me.SmartCars.Cars(0)
        drawNn.neuralNetwork = Me.SmartCars.GeneticAlgorithm.NeuralNetworks(0)
        drawNn.geneticAlgorithm = Me.SmartCars.GeneticAlgorithm

        Me.Timer1.Start()

        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.WindowState = WindowState.Normal
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = WindowState.Maximized
    End Sub
    Private Sub track_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Me.MeDraw(e.Graphics)
    End Sub

    Private Sub Setup()
        Me.ClearMap()
        Me.CreateTrack()
        Me.TrackToBitmap()

        For i As Integer = 0 To Me.SmartCars.Cars.Length - 1
            Me.SmartCars.Cars(i).posX = 90
            Me.SmartCars.Cars(i).posY = 90
            Me.SmartCars.Cars(i).TrackBitmap = Me.TrackBitmap
        Next

    End Sub

    Private Sub ClearMap()
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

        Me.SmartCars.MoveCars()

        If Not Me.SmartCars.StillAlive Then
            Me.Reset()
        End If
    End Sub

    Private Sub MeDraw(g As Graphics)
        Dim sensorVisible As Boolean = False
        g.DrawImage(TrackBitmap, 0, 0)
        For i As Integer = 0 To Me.SmartCars.Cars.Length - 1

            g.FillPolygon(Brushes.Red, Me.SmartCars.Cars(i).Body)
            g.FillEllipse(Brushes.Blue, CInt(Me.SmartCars.Cars(i).posX) + 10, CInt(Me.SmartCars.Cars(i).posY) + 5, 3, 3)

            If i = 0 Then
                sensorVisible = True
            Else
                sensorVisible = False
            End If

            Me.SmartCars.Cars(i).DrawSensors(g, sensorVisible)
        Next

        drawNn.NeuralNetworkToBitmap()
        g.DrawImage(drawNn.NeuralNetworkBitmap, Me.Width - drawNn.NeuralNetworkBitmap.Width, 0, drawNn.NeuralNetworkBitmap.Width, drawNn.NeuralNetworkBitmap.Height)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.MeUpdate()
        Me.Invalidate()
    End Sub

    Private Sub Reset()
        Me.SmartCars.GeneticAlgorithm.NextGeneration()
        Me.ClearMap()
        Me.CreateTrack()
        Me.TrackToBitmap()
        For i As Integer = 0 To Me.SmartCars.Cars.Length - 1
            Me.SmartCars.Cars(i).Reset()
        Next
        Me.drawNn.neuralNetwork = Me.SmartCars.GeneticAlgorithm.NeuralNetworks(0)
    End Sub

    Private Sub TrackMap_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim answer As MsgBoxResult = MsgBox("Do you want to override you save?", MsgBoxStyle.YesNo)
        If answer = MsgBoxResult.Yes Then JsonParser.SaveToFile(Me.SmartCars.GeneticAlgorithm, $"{Application.StartupPath}\save.json")
        End
    End Sub

    'Private Sub TrackMap_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

    'End Sub

    Private Sub TrackMap_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            If Me.FormBorderStyle = FormBorderStyle.None Then
                Me.FormBorderStyle = FormBorderStyle.Sizable
                Me.WindowState = WindowState.Normal
            Else
                Me.FormBorderStyle = FormBorderStyle.None
                Me.WindowState = WindowState.Maximized
            End If
        End If
    End Sub




End Class