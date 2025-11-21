Imports System.IO
Imports Newtonsoft.Json
Public Class JsonParser
    Public Shared Sub SaveToFile(Of T)(obj As T, filePath As String)
        Try
            Dim json As String = JsonConvert.SerializeObject(obj, Formatting.Indented)
            File.WriteAllText(filePath, json)
        Catch ex As Exception
            MessageBox.Show("Error saving JSON: " & ex.Message)
        End Try
    End Sub
    Public Shared Function LoadFromFile(Of T)(filePath As String) As T
        Try
            If Not File.Exists(filePath) Then
                Throw New FileNotFoundException("File not found: " & filePath)
            End If

            Dim json As String = File.ReadAllText(filePath)
            Return JsonConvert.DeserializeObject(Of T)(json)
        Catch ex As Exception
            MessageBox.Show("Error loading JSON: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Shared Function JsonToString(Of T)(obj As T) As String
        Try
            Return JsonConvert.SerializeObject(obj, Formatting.Indented)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function JsonStringToObject(Of T)(json As String) As T
        Try
            Return JsonConvert.DeserializeObject(Of T)(json)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class

