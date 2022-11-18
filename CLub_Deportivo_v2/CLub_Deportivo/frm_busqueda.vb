Public Class frm_busqueda
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub btn_usar_Click(sender As Object, e As EventArgs) Handles btn_usar.Click
        Try
            If DataGridView1.SelectedRows.Count > 0 Then
                frm_socio.txt_nsocio.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
                frm_socio.txt_nombre.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
                frm_socio.txt_apellido.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
                frm_socio.txt_dni.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
                frm_socio.txt_direcc.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
                frm_socio.txt_cp.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString()
                frm_socio.cmb_localidad.SelectedText = ""
                frm_socio.cmb_localidad.SelectedText = DataGridView1.SelectedRows(0).Cells(6).Value.ToString()
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
                If Not IsDBNull(DataGridView1.SelectedRows(0).Cells(9).Value) Then
                    Dim fechanac As New Date(CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 6).Remove(4, 8)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 3).Remove(2, 13)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(2, 16)))
                    ' fechanac = CDate(DataGridView1.SelectedRows(0).Cells(9).Value.ToString())
                    frm_socio.dtpk_fecha_nac.Value = fechanac
                    MsgBox(Calcula_edad(fechanac).Year.ToString())
                    If (Calcula_edad(fechanac).Year >= 65) Then
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
    Public Function Calcula_edad(fechanac As Date) As Date
        Dim ahora As New Date()
        Dim edad_socio As New Date(fechanac.Ticks)
        ahora = Date.Now
        Dim años As Integer = DateDiff(DateInterval.Year, edad_socio, ahora)
        Dim meses As Integer = DateDiff(DateInterval.Month, edad_socio, ahora)
        Dim dias As Integer = DateDiff(DateInterval.Day, edad_socio, ahora)
        edad_socio = New Date(años, meses, dias)
        Return edad_socio
    End Function
End Class