Public Class frm_listados
    Private Sub frm_listados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frm_principal

        DataGridView1.DataSource = ds_club.Tables(0)
        DataGridView1.DataSource = ds_club.Tables(1)

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

    Private Sub lbl_listado_Click(sender As Object, e As EventArgs) Handles lbl_listado.Click

    End Sub
End Class