Imports System.IO

Public Class Intro

    Dim tempVideoPath As String = Path.Combine(Path.GetTempPath(), "intro.mp4")

    Private Sub Intro_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        File.WriteAllBytes(tempVideoPath, My.Resources.intro)

        AxWindowsMediaPlayer1.URL = tempVideoPath
        AxWindowsMediaPlayer1.uiMode = "none"
        AxWindowsMediaPlayer1.Ctlcontrols.play()


    End Sub

    Private Sub AxWindowsMediaPlayer1_PlayStateChange(sender As Object, e As AxWMPLib._WMPOCXEvents_PlayStateChangeEvent) Handles AxWindowsMediaPlayer1.PlayStateChange
        If e.newState = WMPLib.WMPPlayState.wmppsMediaEnded Then
            Form1.Show()
            Me.Close()
        End If
    End Sub
End Class