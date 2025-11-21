Public Class QuickTest

    Private oneCount As Integer
    Private twoCount As Integer
    Private Sub QuickTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub QuickTestButton_Click(sender As Object, e As EventArgs) Handles QuickTestButton.Click
        Dim rndInt As Integer
        For i As Integer = 1 To 100000
            rndInt = Maths.RandomInt(1, 2)
            If rndInt = 1 Then
                oneCount += 1
            Else
                twoCount += 1
            End If
        Next
        Debug.Print($"RndInt: {rndInt}, oneCount: {oneCount}, twoCount: {twoCount} %Chance: {(oneCount / (oneCount + twoCount) * 100).ToString("F2")} / {(twoCount / (oneCount + twoCount) * 100).ToString("F2")}")
    End Sub

End Class