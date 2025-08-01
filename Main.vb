Imports System.Diagnostics.Eventing.Reader
Imports MySql.Data.MySqlClient

Public Class Main

    Dim strCon As String = "server=localhost; userid=root; database=fdbmsproject"
    Dim name1 As String = "Welcome To SkyLink, " & Session.UserName & "!"
    Dim increment As Integer = 0
    Public receivID As Integer
    Dim locationY As Integer = 15
    Private Sub ProductsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsToolStripMenuItem.Click
        Session.fromProduct = True
        products.Show()

        Me.Close()

    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick



        If increment < name1.Length Then

            lblUser.Text &= name1(increment)
            increment += 1
            locationY += 1

            Panel1.Location = New Point(Panel1.Location.X, locationY)

        Else

            Timer1.Stop()

        End If

    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Anti Glitch
        Dim panelType As Type = GetType(Panel)
        Dim pi As Reflection.PropertyInfo = panelType.GetProperty("DoubleBuffered", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        pi.SetValue(Panel1, True, Nothing)




        Panel1.Location = New Point(Panel1.Location.X, Panel1.Location.Y = locationY)
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

        Session.fromProduct = True
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
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub

    Private Sub HomeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem1.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Subscription.Show()
        Me.Close()
    End Sub

    Private Sub PictureBox5_Hover(sender As Object, e As EventArgs) Handles PictureBox5.MouseHover
        PictureBox5.Image = My.Resources.yiloBrowse
    End Sub

    Private Sub PictureBox5_Leave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        PictureBox5.Image = My.Resources.Copilot_20250729_220331
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class