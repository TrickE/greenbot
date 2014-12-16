Imports System.Threading
Imports System.IO
Imports System.Net

Public Class bot
    Public Shared Function Screenshot() As Bitmap
        Dim bmpScreenshot As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        Dim g As Graphics = Graphics.FromImage(bmpScreenshot)
        g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size)
        Return bmpScreenshot
    End Function

    Public Shared Function Find(ByVal f As Bitmap, ByRef location As Point)
        Dim s As Bitmap = Screenshot()
        For outerX As Integer = 0 To s.Width - f.Width - 1
            For outerY As Integer = 0 To s.Height - f.Height - 1
                For innerX As Integer = 0 To f.Width - 1
                    For innerY As Integer = 0 To f.Height - 1
                        Dim c1 As Color = f.GetPixel(innerX, innerY)
                        Dim c2 As Color = s.GetPixel(innerX + outerX, innerY + outerY)

                        If c1.R <> c2.R OrElse c1.G <> c2.G OrElse c1.B <> c2.B Then
                            GoTo notFound
                        End If
                    Next
                Next
                location = New Point(outerX, outerY)
                Return True
notFound:
                Continue For
            Next
        Next
        location = Point.Empty
        Return False
    End Function
    Public Shared Function FindL(ByVal f As Bitmap)
        Dim location As Point
        Dim s As Bitmap = Screenshot()
        For outerX As Integer = 0 To s.Width - f.Width - 1
            For outerY As Integer = 0 To s.Height - f.Height - 1
                For innerX As Integer = 0 To f.Width - 1
                    For innerY As Integer = 0 To f.Height - 1
                        Dim c1 As Color = f.GetPixel(innerX, innerY)
                        Dim c2 As Color = s.GetPixel(innerX + outerX, innerY + outerY)

                        If c1.R <> c2.R OrElse c1.G <> c2.G OrElse c1.B <> c2.B Then
                            GoTo notFound
                        End If
                    Next
                Next
                location = New Point(outerX, outerY)
                Return location
notFound:
                Continue For
            Next
        Next
        location = Point.Empty
        Return False
    End Function
#Region "ApiShit"
    Public Shared Function biturl(ByVal url As String)
        Dim wc As New WebClient
        Try
            Return Bitmap.FromStream(New MemoryStream(wc.DownloadData(url)))
        Catch ex As Exception
            MsgBox(ex.ToString & url)
        End Try
        Return False
    End Function
    Public Shared Sub waitms(ByVal ms As Integer)
        Thread.Sleep(ms)
    End Sub
    Public Shared Sub wait(ByVal s As Integer)
        Thread.Sleep(s * 1000)
    End Sub
#End Region
#Region "Base64"
    Public Shared Function BaseImg(ByVal base64String As String) As Bitmap 'Convert Base64 to an image
        ' Convert Base64 String to byte[]
        Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
        Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)

        ' Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length)
        Dim image__1 As Bitmap = Bitmap.FromStream(ms, True)
        Return image__1
    End Function

    Public Shared Function ImgBase(ByVal image As Image) As String 'Convert an image to Base64
        Using ms As New MemoryStream()
            ' Convert Image to byte[]
            image.Save(ms, Imaging.ImageFormat.Bmp)
            Dim imageBytes As Byte() = ms.ToArray()

            ' Convert byte[] to Base64 String
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function
#End Region
End Class
