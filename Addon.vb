Public Class Addon

    Dim imageRcv As Image = Session.planImage
    Dim planName As String = Session.planName
    Dim planType As String = Session.planType
    Dim planPrice As Decimal = Session.planPrice
    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Addon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pbxPlanImage.Image = imageRcv
        lblName.Text = "Plan: " & planName
        lblType.Text = "Type: " & planType
        lblPrice.Text = "Price: " & planPrice

    End Sub
End Class