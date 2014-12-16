Imports System.Threading
Imports System.Runtime.InteropServices
Imports greenbot.bot
Public Class Mouse
    <DllImport("user32.dll")> _
    Public Shared Sub mouse_event(ByVal dwFlags As UInteger, ByVal dx As UInteger, ByVal dy As UInteger, ByVal dwData As UInteger, ByVal dwExtraInfo As Integer)
    End Sub

    Public Enum MouseEventFlags As UInteger
        LEFTDOWN = &H2
        LEFTUP = &H4
        MIDDLEDOWN = &H20
        MIDDLEUP = &H40
        MOVE = &H1
        ABSOLUTE = &H8000
        RIGHTDOWN = &H8
        RIGHTUP = &H10
        WHEEL = &H800
        XDOWN = &H80
        XUP = &H100
    End Enum

    Public Shared Sub CursorJump(ByVal loc As Point)
        Cursor.Position = loc
    End Sub
    Public Shared Sub LeftClick()
        mouse_event(CUInt(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0)
        Thread.Sleep((New Random()).[Next](20, 30))
        mouse_event(CUInt(MouseEventFlags.LEFTUP), 0, 0, 0, 0)
    End Sub
    Public Shared Sub RightClick()
        mouse_event(CUInt(MouseEventFlags.RIGHTDOWN), 0, 0, 0, 0)
        Thread.Sleep((New Random()).[Next](20, 30))
        mouse_event(CUInt(MouseEventFlags.RIGHTUP), 0, 0, 0, 0)
    End Sub
    Public Shared Sub LeftDown()
        mouse_event(CUInt(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0)
    End Sub
    Public Shared Sub RightDown()
        mouse_event(CUInt(MouseEventFlags.RIGHTDOWN), 0, 0, 0, 0)
    End Sub
    Public Shared Sub LeftUp()
        mouse_event(CUInt(MouseEventFlags.LEFTUP), 0, 0, 0, 0)
    End Sub
    Public Shared Sub RightUp()
        mouse_event(CUInt(MouseEventFlags.LEFTUP), 0, 0, 0, 0)
    End Sub
    Public Shared Sub LClick(ByVal b As Bitmap)
        Dim pt As Point
tryagain:
        If Find(b, pt) = False Then GoTo tryagain
        MoveMouse(pt)
        LeftClick()
    End Sub
    Public Shared Sub LClickC(ByVal pt As Point)
        MoveMouse(pt)
        waitms(100)
        LeftClick()
    End Sub
    Public Shared Sub RClick(ByVal b As Bitmap)
        Dim pt As Point
tryagain:
        If Find(b, pt) = False Then GoTo tryagain
        Find(b, pt)
        MoveMouse(pt)
        RightClick()
    End Sub
    Public Shared Sub RClickC(ByVal pt As Point)
        MoveMouse(pt)
        waitms(100)
        RightClick()
    End Sub
    Public Shared Sub ClickAndDrag(ByVal b As Bitmap, ByVal x As Integer, ByVal y As Integer)
        'Dim StartPt As Point
        'Dim StopPt As Point
        'Find(b, StartPt)
        'StopPt.X = StartPt.X + x
        'StopPt.X = StartPt.Y + y
        'CursorMove(StartPt)
        'waitms(100)
        'LeftDown()
        'CursorMove(StopPt)
        'LeftUp()
    End Sub
#Region "Wind Mouse"
    Shared random As New Random()
    Public Shared mouseSpeed As Integer = 15

    Public Shared Sub MoveMouse(ByVal point As Point)
        Dim x As Integer = point.X
        Dim y As Integer = point.Y
        Dim rx As Integer = 0
        Dim ry As Integer = 0


        Dim c As New Point()
        GetCursorPos(c)

        x += random.[Next](rx)
        y += random.[Next](ry)

        Dim randomSpeed As Double = Math.Max((random.[Next](mouseSpeed) / 2.0 + mouseSpeed) / 10.0, 0.1)

        WindMouse(c.X, c.Y, x, y, 9.0, 3.0, _
         10.0 / randomSpeed, 15.0 / randomSpeed, 10.0 * randomSpeed, 10.0 * randomSpeed)
    End Sub

    Private Shared Sub WindMouse(ByVal xs As Double, ByVal ys As Double, ByVal xe As Double, ByVal ye As Double, ByVal gravity As Double, ByVal wind As Double, _
     ByVal minWait As Double, ByVal maxWait As Double, ByVal maxStep As Double, ByVal targetArea As Double)

        Dim dist As Double, windX As Double = 0, windY As Double = 0, veloX As Double = 0, veloY As Double = 0, randomDist As Double, _
         veloMag As Double, [step] As Double
        Dim oldX As Integer, oldY As Integer, newX As Integer = CInt(Math.Round(xs)), newY As Integer = CInt(Math.Round(ys))

        Dim waitDiff As Double = maxWait - minWait
        Dim sqrt2 As Double = Math.Sqrt(2.0)
        Dim sqrt3 As Double = Math.Sqrt(3.0)
        Dim sqrt5 As Double = Math.Sqrt(5.0)

        dist = Hypot(xe - xs, ye - ys)

        While dist > 1.0

            wind = Math.Min(wind, dist)

            If dist >= targetArea Then
                Dim w As Integer = random.[Next](CInt(Math.Round(wind)) * 2 + 1)
                windX = windX / sqrt3 + (w - wind) / sqrt5
                windY = windY / sqrt3 + (w - wind) / sqrt5
            Else
                windX = windX / sqrt2
                windY = windY / sqrt2
                If maxStep < 3 Then
                    maxStep = random.[Next](3) + 3.0
                Else
                    maxStep = maxStep / sqrt5
                End If
            End If

            veloX += windX
            veloY += windY
            veloX = veloX + gravity * (xe - xs) / dist
            veloY = veloY + gravity * (ye - ys) / dist

            If Hypot(veloX, veloY) > maxStep Then
                randomDist = maxStep / 2.0 + random.[Next](CInt(Math.Round(maxStep)) \ 2)
                veloMag = Hypot(veloX, veloY)
                veloX = (veloX / veloMag) * randomDist
                veloY = (veloY / veloMag) * randomDist
            End If

            oldX = CInt(Math.Round(xs))
            oldY = CInt(Math.Round(ys))
            xs += veloX
            ys += veloY
            dist = Hypot(xe - xs, ye - ys)
            newX = CInt(Math.Round(xs))
            newY = CInt(Math.Round(ys))

            If oldX <> newX OrElse oldY <> newY Then
                SetCursorPos(newX, newY)
            End If

            [step] = Hypot(xs - oldX, ys - oldY)
            Dim wait As Integer = CInt(Math.Round(waitDiff * ([step] / maxStep) + minWait))
            Thread.Sleep(wait)
        End While

        Dim endX As Integer = CInt(Math.Round(xe))
        Dim endY As Integer = CInt(Math.Round(ye))
        If endX <> newX OrElse endY <> newY Then
            SetCursorPos(endX, endY)
        End If
    End Sub

    Private Shared Function Hypot(ByVal dx As Double, ByVal dy As Double) As Double
        Return Math.Sqrt(dx * dx + dy * dy)
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function SetCursorPos(ByVal X As Integer, ByVal Y As Integer) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetCursorPos(ByRef p As Point) As Boolean
    End Function
#End Region
End Class
