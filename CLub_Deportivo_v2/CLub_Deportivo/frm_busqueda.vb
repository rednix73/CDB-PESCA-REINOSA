Public Class frm_busqueda
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub btn_usar_Click(sender As Object, e As EventArgs) Handles btn_usar.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            frm_socio.txt_nsocio.Text = DataGridView1.SelectedRows(0).Cells(0).Value
            frm_socio.txt_nombre.Text = DataGridView1.SelectedRows(0).Cells(1).Value
            frm_socio.txt_apellido.Text = DataGridView1.SelectedRows(0).Cells(2).Value
            frm_socio.txt_dni.Text = DataGridView1.SelectedRows(0).Cells(3).Value
            frm_socio.txt_direcc.Text = DataGridView1.SelectedRows(0).Cells(4).Value
            frm_socio.cmb_pais.SelectedItem = DataGridView1.SelectedRows(0).Cells(8).Value
            frm_socio.cmb_prov.SelectedItem = DataGridView1.SelectedRows(0).Cells(7).Value
            frm_socio.cmb_localidad.SelectedText = DataGridView1.SelectedRows(0).Cells(6).Value
            frm_socio.txt_cp.Text = DataGridView1.SelectedRows(0).Cells(5).Value
            frm_socio.txt_email.Text = DataGridView1.SelectedRows(0).Cells(10).Value
            frm_socio.cmb_tarjeta.SelectedIndex = DataGridView1.SelectedRows(0).Cells(11).Value
            frm_socio.dtpk_fecha_nac.Value = DataGridView1.SelectedRows(0).Cells(9).Value
            Select Case DataGridView1.SelectedRows(0).Cells(12).Value
                Case "NORMAL"
                    frm_socio.rdo_normal.Checked = True
                Case "JUBILADO"
                    frm_socio.rdo_jubilado.Checked = True
                Case "MENOR, FEMINA, OTROS"

                    frm_socio.rdo_otros.Checked = True
            End Select

            frm_socio.dtpk_fecha_nac.Value = DataGridView1.SelectedRows(0).Cells(9).Value





        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub frm_busqueda_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class