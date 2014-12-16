Imports greenbot.Mouse
Imports greenbot.Keyboard
Imports greenbot.bot
Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Mouse.mouseSpeed = 8
        Mouse.MoveMouse(New Point(50, 90))
        waitms(1)
        Mouse.mouseSpeed = 10
        Mouse.MoveMouse(New Point(90, 220))
        wait(1)
        Mouse.mouseSpeed = 20
        Mouse.MoveMouse(New Point(900, 1000))
        wait(0.3)
        Mouse.mouseSpeed = 8
        Mouse.MoveMouse(New Point(900, 900))
    End Sub
End Class
