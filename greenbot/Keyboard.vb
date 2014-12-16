Public Class Keyboard
    Public Shared Sub type(ByVal str As String)
        SendKeys.SendWait(str)
    End Sub
End Class
