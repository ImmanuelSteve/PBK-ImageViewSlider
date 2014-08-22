Imports System.Net
Imports System.IO
Imports System.Linq
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ImgView1.addLocalImageFolder("E:\PBK\")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        If TextBox1.Text.EndsWith("\") Or TextBox1.Text.EndsWith("/") Then
            ImgView1.addLocalImageFolder(TextBox1.Text)
        Else
            Try
                request = DirectCast(WebRequest.Create("https://ajax.googleapis.com/ajax/services/search/images?v=1.0&q=" + TextBox1.Text + "&rsz=3"), HttpWebRequest)
                response = DirectCast(request.GetResponse(), HttpWebResponse)
                reader = New StreamReader(response.GetResponseStream())

                Dim rawresp As String
                rawresp = reader.ReadToEnd()

                Dim jResults As JObject = JObject.Parse(rawresp)
                Dim responseData As JObject = jResults.GetValue("responseData")
                Dim results As JArray = responseData.GetValue("results")
                Dim arrlist As ArrayList = New ArrayList
                Dim a As List(Of JToken) = results.Children().ToList()



                For Each item As JObject In a
                    For Each prop As JProperty In item.Properties()
                        If prop.Name = "unescapedUrl" Then
                            arrlist.Add(prop.Value.ToString())
                            ImgView1.addImageURL(prop.Value.ToString())
                        End If
                    Next
                Next
                'MsgBox(arrlist.Item(1).ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally
                If Not response Is Nothing Then response.Close()
            End Try
        End If
    End Sub
End Class
