Public Class track

    Private tiles(,) As Image

    Private trackStright2 As Image = Image.FromFile($"{Application.StartupPath}\track\stright2.png")
    Private trackTurn2 As Image = Image.FromFile($"{Application.StartupPath}\track\turn2.png")

    Private Sub track_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 11 * 134
        Me.Height = 6 * 134
        Setup()
        Timer1.Start()
    End Sub
    Private Sub track_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Me.MeDraw(e.Graphics)
    End Sub

    Private Sub Setup()
        ReDim tiles(Me.Width / 134, Me.Height / 134)
        For i As Integer = 0 To Me.Width / 134
            For j As Integer = 0 To Me.Height / 134
                Dim rnd As Integer = Maths.RandomInt(0, 3)
                If Maths.RandomInt(0, 1) = 1 Then
                    tiles(i, j) = Image.FromFile($"{Application.StartupPath}\track\stright2.png")
                Else
                    tiles(i, j) = Image.FromFile($"{Application.StartupPath}\track\turn2.png")
                End If
                Select Case rnd
                    Case 0
                        tiles(i, j).RotateFlip(RotateFlipType.RotateNoneFlipNone)
                    Case 1
                        tiles(i, j).RotateFlip(RotateFlipType.Rotate90FlipNone)
                    Case 2
                        tiles(i, j).RotateFlip(RotateFlipType.Rotate180FlipNone)
                    Case 3
                        tiles(i, j).RotateFlip(RotateFlipType.Rotate270FlipNone)
                End Select
            Next
        Next
    End Sub

    Private Sub MeUpdate()

    End Sub

    Private flipFlop As Boolean = False
    Private Sub MeDraw(g As Graphics)
        For x As Integer = 0 To Me.tiles.GetLength(0) - 1
            For y As Integer = 0 To Me.tiles.GetLength(1) - 1
                g.DrawImage(tiles(x, y), x * 134, y * 134, 134, 134)
            Next
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.MeUpdate()
        Me.Invalidate()
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
    End Sub

End Class