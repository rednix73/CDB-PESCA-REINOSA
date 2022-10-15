Imports System.IO
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
        carga_configuracion()

    End Sub
    Private Sub carga_configuracion()

        'Pestaña general
        cmb_temporada.SelectedItem = bbdd.temporada
        txt_tarjeta_salmon.Text = bbdd.precio_salmon.ToString()
        txt_tarjeta_trucha.Text = bbdd.precio_trucha.ToString()
        pctbox_tsocio_anverso.Image = Image.FromFile(bbdd.tarjeta_socio_anverso)
        pctbox_tsocio_reverso.Image = Image.FromFile(bbdd.tarjeta_socio_reverso)

        ' Pestaña Base de datos
        'ODBC-excel
        cmb_bd.SelectedItem = bbdd.tp.ToString()
        txt_archivo_excel.Text = bbdd.ruta_bd_excel
        txt_dsn.Text = bbdd.DSN
        txt_tabla_socios_xls.Text = tabla_socios_xls
        txt_tabla_bdsocios_xls.Text = tabla_bdsocios_xls
        'mysql
        txt_server.Text = bbdd.server
        txt_port.Text = bbdd.port
        txt_bbdd.Text = bbdd.bd_mysql
        txt_user.Text = bbdd.user
        txt_password.Text = bbdd.password
        txt_tabla_socios_mysql.Text = bbdd.tabla_socios_mysql
        txt_tabla_bdsocios_mysql.Text = bbdd.tabla_bdsocios_mysql
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFile_anverso.Filter = "Imagenes|*.jpg;*.gif;*.png|Todos los archivos|*.*"
        OpenFile_anverso.ShowDialog()

    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFile_anverso.FileOk
        Dim f As FileInfo = New FileInfo(OpenFile_anverso.FileName)
        File.Copy(f.FullName, "../../Resources/" & bbdd.tarjeta_socio_anverso, True)
        pctbox_tsocio_anverso.Image = Image.FromFile(bbdd.tarjeta_socio_anverso)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFile_reverso.Filter = "Imagenes|*.jpg;*.gif;*.png|Todos los archivos|*.*"
        OpenFile_reverso.ShowDialog()
    End Sub

    Private Sub OpenFile_reverso_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFile_reverso.FileOk
        Dim f As FileInfo = New FileInfo(OpenFile_reverso.FileName)
        File.Copy(f.FullName, "../../Resources/" & bbdd.tarjeta_socio_reverso, True)
        pctbox_tsocio_reverso.Image = Image.FromFile(bbdd.tarjeta_socio_reverso)
    End Sub
End Class