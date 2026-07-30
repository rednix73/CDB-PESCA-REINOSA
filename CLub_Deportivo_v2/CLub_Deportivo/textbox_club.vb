Public Class textbox_club
    Inherits TextBox
    Protected Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)
        If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
            Return
        End If
        MyBase.Text = MyBase.Text.ToUpper()
    End Sub


End Class
