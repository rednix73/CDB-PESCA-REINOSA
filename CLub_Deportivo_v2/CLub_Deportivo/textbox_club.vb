Public Class textbox_club
    Inherits TextBox
    Protected Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)
        MyBase.Text = MyBase.Text.ToUpper




    End Sub


End Class
