Imports System.ComponentModel
Imports System.Drawing
Imports System.Net.Security
Imports System.Windows.Automation

Public Class TrackMap

    Public SmartCars As New SmartCars

    Public ScoreGoal As Integer = 1000000
    Public GoalReached As Boolean = False

    Public TrackBitmap As Bitmap
    Public TrackTiles(,) As TrackTile
    Public drawNn As New DrawBestNeuralNetwork
    Public widthTileCount As Integer = 11
    Public heightTileCount As Integer = 6

    Public HeadEastTiles As New List(Of TrackTile)
    Public HeadSouthTiles As New List(Of TrackTile)
    Public HeadWestTiles As New List(Of TrackTile)
    Public HeadNorthTiles As New List(Of TrackTile)

    Public ShouldChangeTrack As Boolean = False
    Public HeroSurvivedCounter As Integer = 0
    Public DrawHeroOnly As Boolean = False

    Private useTimer As Boolean = False
    Private fastForwardSpeed As Integer = 10
    Private fastForwardCounter As Integer = 0

    Private loadedCounter As Integer = 0


    Private Sub track_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TotalReset()
        If Dir($"{Application.StartupPath}\save.json") <> "" Then
            Dim answer As MsgBoxResult = MsgBox("Do you want to load your save file?", MsgBoxStyle.YesNo)
            If answer = MsgBoxResult.Yes Then Me.SmartCars.GeneticAlgorithm = JsonParser.LoadFromFile(Of GeneticAlgorithm)($"{Application.StartupPath}\save.json")
        End If
    End Sub
    Private Sub TotalReset()
        Me.Width = widthTileCount * 160
        Me.Height = heightTileCount * 160
        Me.BackColor = Color.FromArgb(96, 142, 66)
        ReDim Me.TrackTiles(widthTileCount - 1, heightTileCount - 1)
        Me.TrackBitmap = New Bitmap(widthTileCount * 160, widthTileCount * 160)
        Me.Setup()

        drawNn.car = Me.SmartCars.Cars(0)
        drawNn.neuralNetwork = Me.SmartCars.GeneticAlgorithm.NeuralNetworks(0)
        drawNn.geneticAlgorithm = Me.SmartCars.GeneticAlgorithm

        Me.ShowInTaskbar = True
        Me.ShowIcon = True

        Me.Timer1.Start()
        CountDownTimer.Start()

        Me.GoalReached = False
        Me.SmartCars.GeneticAlgorithm.Generation = 0
        For Each nn As NeuralNetwork In Me.SmartCars.GeneticAlgorithm.NeuralNetworks
            nn.Randomize()
        Next
        Me.Reset()
    End Sub

    Private Sub Reset()

        If Not DrawHeroOnly Then
            Me.SmartCars.GeneticAlgorithm.NextGeneration()
        End If

        If ShouldChangeTrack Then
            Me.ClearMap()
            Me.CreateTrack()
            Me.TrackToBitmap()
        End If

        Me.countdownTime = 40

        For i As Integer = 0 To Me.SmartCars.Cars.Length - 1
            Me.SmartCars.Cars(i).Reset()
            If i = 0 Then
                Me.SmartCars.Cars(i).sensorVisible = True
            Else
                Me.SmartCars.Cars(i).sensorVisible = False
            End If

        Next

        drawNn.car = Me.SmartCars.Cars(0)
        drawNn.neuralNetwork = Me.SmartCars.GeneticAlgorithm.NeuralNetworks(0)
        drawNn.geneticAlgorithm = Me.SmartCars.GeneticAlgorithm
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

        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim chncFrStrtTrk As Integer = 10

        HeadEastTiles.Clear()
        HeadSouthTiles.Clear()
        HeadWestTiles.Clear()
        HeadNorthTiles.Clear()

        HeadEastTiles.Add(Me.TrackTiles(0, 0))

        'Head Down and Right
        Do Until (x = widthTileCount - 1) Or (y = heightTileCount - 1)

            If x = 0 And y = 0 Then
                Me.TrackTiles(0, 0).ChangeTileType(TrackImages.TrackTileType.turnBottomToRight)
                x = 1

            ElseIf x = 1 And y = 0 Then
                Me.TrackTiles(1, 0).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                x = 1
                y = 1


            ElseIf x = 1 And y = 1 Then
                Me.TrackTiles(1, 1).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                x = 2
                y = 1


            ElseIf x = 2 And y = 1 Then
                Me.TrackTiles(2, 1).ChangeTileType(TrackImages.TrackTileType.turnTopToLeft)
                x = 2
                y = 0


            ElseIf x = 2 And y = 0 Then
                Me.TrackTiles(2, 0).ChangeTileType(TrackImages.TrackTileType.turnBottomToRight)

            End If

            If TrackTiles(x, y).TileType = TrackImages.TrackTileType.strightVertical Then
                y += 1
                'HeadEastTiles.Add(Me.TrackTiles(x, y))
                If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
                Else
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                End If


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.strightHorizontal Then
                x += 1
                'HeadEastTiles.Add(Me.TrackTiles(x, y))
                If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)
                Else
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                End If


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnTopToRight Then
                x += 1
                'HeadEastTiles.Add(Me.TrackTiles(x, y))
                If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)
                Else
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToLeft)
                End If


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnTopToLeft Then
                y -= 1
                'HeadEastTiles.Add(Me.TrackTiles(x, y))
                Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToRight)


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnBottomToRight Then
                x += 1
                'HeadEastTiles.Add(Me.TrackTiles(x, y))
                If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)
                Else
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                End If


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnBottomToLeft Then
                y += 1
                'HeadEastTiles.Add(Me.TrackTiles(x, y))
                If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
                Else
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                End If
            End If
        Loop


        'Hit Edge Turn Back
        If x = widthTileCount - 1 Then Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
        Do Until (y = heightTileCount - 1)
            y += 1
            HeadSouthTiles.Add(Me.TrackTiles(x, y))
            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
        Loop
        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToLeft)



        'Head Back Home
        Do Until x <= 2

            If Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.strightVertical Then
                y -= 1
                HeadWestTiles.Add(Me.TrackTiles(x, y))
                If Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                Else
                    If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
                    Else
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                    End If
                End If


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.strightHorizontal Then
                x -= 1
                HeadWestTiles.Add(Me.TrackTiles(x, y))
                If Me.TrackTiles(x, y).TileType <> TrackImages.TrackTileType.grass Then
                    x += 1
                    HeadWestTiles.Add(Me.TrackTiles(x, y))
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToRight)
                    y += 1
                    HeadWestTiles.Add(Me.TrackTiles(x, y))
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToLeft)


                ElseIf Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                Else
                    If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)
                    Else
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                    End If
                End If


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnTopToRight Then
                y -= 1
                HeadWestTiles.Add(Me.TrackTiles(x, y))
                If Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                Else
                    If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
                    Else
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnBottomToLeft)
                    End If
                End If

            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnTopToLeft Then
                x -= 1
                HeadWestTiles.Add(Me.TrackTiles(x, y))
                If Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                Else
                    If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)
                    Else
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                    End If
                End If


            ElseIf Me.TrackTiles(x, y).TileType = TrackImages.TrackTileType.turnBottomToLeft Then
                x -= 1
                HeadWestTiles.Add(Me.TrackTiles(x, y))
                If Me.TrackTiles(x, y - 1).TileType <> TrackImages.TrackTileType.grass Or Me.TrackTiles(x - 1, y - 1).TileType <> TrackImages.TrackTileType.grass Then
                    Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)


                Else
                    If Maths.RandomInt(0, 100) < chncFrStrtTrk Then
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightHorizontal)
                    Else
                        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
                    End If
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

            HeadWestTiles.Add(Me.TrackTiles(x, y))
            x -= 1
        Loop
        Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.turnTopToRight)
        y -= 1
        HeadNorthTiles.Add(Me.TrackTiles(x, y))
        Do Until y = 0
            Me.TrackTiles(x, y).ChangeTileType(TrackImages.TrackTileType.strightVertical)
            HeadNorthTiles.Add(Me.TrackTiles(x, y))
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
            If Me.SmartCars.GeneticAlgorithm.Generation >= 1000 Then Me.GoalReached = True
            If Me.GoalReached Then Me.TotalReset()
        End If


        If Me.loadedCounter = 3 Then
            Me.FormBorderStyle = FormBorderStyle.None
            Me.WindowState = WindowState.Maximized
            loadedCounter += 1
        Else
            loadedCounter += 1
        End If


    End Sub



    Private Sub MeDraw(g As Graphics)
        g.DrawImage(TrackBitmap, 0, 0)

        If DrawHeroOnly Then
            Me.SmartCars.Cars(0).DrawSensors(g, False)
            Me.SmartCars.Cars(0).CanReceivePoint = False
        Else
            For i As Integer = 0 To Me.SmartCars.Cars.Length - 1
                g.FillPolygon(Me.SmartCars.Cars(i).BodyBrush, Me.SmartCars.Cars(i).Body)
                g.FillEllipse(Brushes.Blue, CInt(Me.SmartCars.Cars(i).posX) + 10, CInt(Me.SmartCars.Cars(i).posY) + 5, 3, 3)
                Me.SmartCars.Cars(i).DrawSensors(g, Me.SmartCars.Cars(i).sensorVisible)
            Next i
        End If
        g.FillPolygon(Brushes.White, Me.SmartCars.Cars(0).Body)
        g.FillEllipse(Brushes.Blue, CInt(Me.SmartCars.Cars(0).posX) + 10, CInt(Me.SmartCars.Cars(0).posY) + 5, 3, 3)

        drawNn.NeuralNetworkToBitmap()
        g.DrawImage(drawNn.NeuralNetworkBitmap, Me.Width - drawNn.NeuralNetworkBitmap.Width, 0, drawNn.NeuralNetworkBitmap.Width, drawNn.NeuralNetworkBitmap.Height)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If useTimer Then
            Me.MeUpdate()
            Me.Invalidate()
        Else
            Do While fastForwardCounter <= fastForwardSpeed
                Me.MeUpdate()
                Me.Invalidate()
                fastForwardCounter += 1
            Loop
            fastForwardCounter = 0
        End If
    End Sub

    Private Sub TrackMap_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim answer As MsgBoxResult = MsgBox("Do you want to override you save?", MsgBoxStyle.YesNo)
        If answer = MsgBoxResult.Yes Then JsonParser.SaveToFile(Me.SmartCars.GeneticAlgorithm, $"{Application.StartupPath}\save.json")
        End
    End Sub


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


    Public countdownTime As Integer = 40 '3 minutes in seconds
    Private Sub CountDownTimer_Tick(sender As Object, e As EventArgs) Handles CountDownTimer.Tick
        If countdownTime <= 0 Then
            countdownTime = 40
            If Me.SmartCars.Cars(0).Crashed Then
                HeroSurvivedCounter = 0
                Me.ShouldChangeTrack = False
            Else
                HeroSurvivedCounter += 1
                Me.ShouldChangeTrack = True
            End If
            If HeroSurvivedCounter >= 10 Then
                useTimer = False
                DrawHeroOnly = True
            Else
                useTimer = False
                DrawHeroOnly = False
            End If
            For i As Integer = 0 To Me.SmartCars.Cars.Length - 1
                If Me.SmartCars.Cars(i).Crashed Then
                    Me.SmartCars.GeneticAlgorithm.NeuralNetworks(i).FitnessScore = 0
                End If
                Me.SmartCars.Cars(i).Crashed = True
            Next
        End If
        countdownTime -= 1
    End Sub

    Private Sub TrackMap_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDoubleClick
        If e.Button = MouseButtons.Left Then
            Me.ClearMap()
            Me.CreateTrack()
            Me.TrackToBitmap()
        ElseIf e.Button = MouseButtons.Right Then
            For i As Integer = 0 To Me.SmartCars.Cars.Length - 1
                Me.SmartCars.Cars(i).Crashed = True
            Next
        End If
    End Sub

End Class