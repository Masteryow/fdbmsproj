Imports System.Diagnostics.Eventing.Reader
Imports MySql.Data.MySqlClient

Public Class Main

    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim name1 As String = "Welcome To SkyLink, " & Session.UserName & "!"
    Dim increment As Integer = 0
    Public receivID As Integer

    Private Sub ProductsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsToolStripMenuItem.Click
        Session.fromProduct = True
        Addon.Show()

        Me.Close()

    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick



        If increment < name1.Length Then

            lblUser.Text &= name1(increment)
            increment += 1

        Else

            Timer1.Stop()

        End If

    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Session.userRole <> "Subscriber" OrElse Session.subStatus Is DBNull.Value OrElse Session.subStatus.ToString() = "" Then
            HelpToolStripMenuItem.Visible = False
        End If

        Timer1.Interval = 100
        Timer1.Start()
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubscriptionToolStripMenuItem.Click
        Subscription.Show()
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Cart.Show()
        Me.Close()
    End Sub

    Private Sub TicketsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TicketsToolStripMenuItem.Click
        Tickets.Show()
        Me.Close()
    End Sub

    Private Sub SubscriptionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

    End Sub

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click

    End Sub

    Private Sub HomeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem1.Click

    End Sub
End Class