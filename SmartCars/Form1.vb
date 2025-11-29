
''' <summary>
''' For evolving the neural network send out multiple cars at the same time.
''' each generation send out the cars multiple times. once with 1% of their w/b completly random
''' once with a weight or bias increased or decreased by a very small random number like 0.01 - 0.000001 or something
''' once with 90% of the cars with combinations of the top 10 genes
''' 4 times with one random w/b completly random -1 to 1.
''' keep the top 10% by sorting the cars of the population and copy the top ten to the other 90%
''' </summary>
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'QuickTest.Show()
        TrackMap.Show()
        TrackMap.Left = 0
        TrackMap.WindowState = WindowState.Maximized
    End Sub

End Class
