Public Class frm_busqueda
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub btn_usar_Click(sender As Object, e As EventArgs) Handles btn_usar.Click
        Try
            If DataGridView1.SelectedRows.Count > 0 Then
                frm_socio.reset()

                frm_socio.txt_nsocio.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
                frm_socio.txt_nombre.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
                frm_socio.txt_apellido.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
                frm_socio.txt_dni.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
                frm_socio.txt_direcc.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
                frm_socio.txt_cp.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString()
                frm_socio.cmb_localidad.Text = ""
                frm_socio.cmb_localidad.Text = DataGridView1.SelectedRows(0).Cells(6).Value.ToString()
                frm_socio.cmb_prov.SelectedItem = DataGridView1.SelectedRows(0).Cells(7).Value.ToString()
                frm_socio.cmb_pais.SelectedItem = DataGridView1.SelectedRows(0).Cells(8).Value.ToString()
                frm_socio.txt_email.Text = DataGridView1.SelectedRows(0).Cells(10).Value.ToString()
                frm_socio.cmb_tarjeta.SelectedIndex = (CInt(DataGridView1.SelectedRows(0).Cells(11).Value) - 1)
                Select Case DataGridView1.SelectedRows(0).Cells(12).Value.ToString()
                    Case "NORMAL"
                        frm_socio.rdo_normal.Checked = True
                    Case "JUBILADO"
                        frm_socio.rdo_jubilado.Checked = True
                    Case "MENOR, FEMINA, OTROS"
                        frm_socio.rdo_otros.Checked = True
                End Select
                'MsgBox(DataGridView1.SelectedRows(0).Cells(9).Value.ToString())
                If Not IsDBNull(DataGridView1.SelectedRows(0).Cells(9).Value) Then
                    'Dim fechanac As New Date(CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 6).Remove(4, 8)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 3).Remove(2, 13)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(2, 16)))
                    Dim fechanac As New Date(CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 6)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 3).Remove(2, 5)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(2, 8)))
                    'fechanac = CDate(DataGridView1.SelectedRows(0).Cells(9).Value.ToString())
                    'MsgBox(fechanac.ToShortDateString())
                    frm_socio.dtpk_fecha_nac.Value = fechanac.Date
                    'MsgBox(Calcula_edad(fechanac).Year.ToString())
                    If (Calcula_edad(fechanac) >= 65) Then
                        frm_socio.rdo_jubilado.Checked = True
                    End If
                Else
                    frm_socio.dtpk_fecha_nac.Value = "2000/01/01"
                End If

            End If
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_cerrar.Click
        Me.Close()

    End Sub

    Private Sub frm_busqueda_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Function Calcula_edad(fechanac1 As Date) As Integer
        Dim ahora As New Date()
        Dim edad_socio As Integer

        ahora = Date.Now
        Dim años As Integer = CInt(DateDiff(DateInterval.Year, fechanac1, ahora))
        Dim meses As Integer = CInt(DateDiff(DateInterval.Month, fechanac1, ahora))
        Dim dias As Integer = CInt(DateDiff(DateInterval.Day, fechanac1, ahora))
        'MsgBox(años.ToString() + ";" + meses.ToString() + ";" + dias.ToString())
        edad_socio = años.ToString()
        Return edad_socio
    End Function
End Class