Public Class frm_configuracion
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub

    Private Sub cmb_bd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_bd.SelectedIndexChanged
        If cmb_bd.SelectedIndex = 1 Then
            grpbox_excel.Enabled = False
            grpbox_mysql.Enabled = True
        Else
            grpbox_mysql.Enabled = False
            grpbox_excel.Enabled = True

        End If
    End Sub

    Private Sub frm_configuracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class