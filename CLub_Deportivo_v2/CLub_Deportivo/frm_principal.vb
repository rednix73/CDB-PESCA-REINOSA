Public Class frm_principal

    Private Sub SociosToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SociosToolStripMenuItem.Click

    End Sub

    Private Sub frm_principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cargar()
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        End

    End Sub

    Private Sub ListadosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListadosToolStripMenuItem.Click
        frm_listados.Show()

    End Sub

    Private Sub AcercaDeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcercaDeToolStripMenuItem.Click
        Acerca_de.Show()

    End Sub

    Private Sub GuardarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarToolStripMenuItem.Click
        Try
            If (Not ds_club.GetChanges Is Nothing) Then
                conectar()
                da_socios1.Update(ds_club.Tables(0))
                da_bdsocios1.Update(ds_club.Tables(1))
                desconectar()
                ds_club.AcceptChanges()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub NuevoSocioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoSocioToolStripMenuItem.Click
        frm_socio.MdiParent = Me
        frm_socio.Show()
        frm_socio.btn_buscar_nsocio.Visible = True
        frm_socio.btn_buscar_dni.Visible = True
        frm_socio.btn_buscar_apell.Visible = True
        frm_socio.btn_eliminar.Visible = False
        frm_socio.btn_modificar.Visible = False

    End Sub

    Private Sub ModificarSocioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModificarSocioToolStripMenuItem.Click
        frm_socio.MdiParent = Me
        frm_socio.Show()
        frm_socio.btn_buscar_nsocio.Visible = True
        frm_socio.btn_buscar_dni.Visible = True
        frm_socio.btn_buscar_apell.Visible = True
        frm_socio.btn_eliminar.Visible = False
        frm_socio.btn_modificar.Visible = True
        frm_socio.btn_insertar.Visible = False
    End Sub

    Private Sub EliminarSocioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarSocioToolStripMenuItem.Click
        frm_socio.MdiParent = Me
        frm_socio.Show()
        frm_socio.btn_buscar_nsocio.Visible = True
        frm_socio.btn_buscar_dni.Visible = True
        frm_socio.btn_buscar_apell.Visible = True
        frm_socio.btn_eliminar.Visible = True
        frm_socio.btn_modificar.Visible = False
        frm_socio.btn_insertar.Visible = False
    End Sub
End Class