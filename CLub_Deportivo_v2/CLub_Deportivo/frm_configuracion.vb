Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Public Class frm_configuracion
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

        ' --- Asegurar carpeta de recursos del usuario y normalizar ruta ---
        Dim settingRes As String = My.Settings.ruta_recursos
        Dim userRes As String
        If String.IsNullOrWhiteSpace(settingRes) Then
            userRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CDB-PESCA-REINOSA", "Resources")
        ElseIf Path.IsPathRooted(settingRes) Then
            userRes = settingRes
        Else
            ' Si la ruta es relativa (p.e. "../../Resources" del entorno de desarrollo),
            ' convertirla a absoluta relativa al directorio de la aplicación en tiempo de ejecución.
            Try
                userRes = Path.GetFullPath(Path.Combine(Application.StartupPath, settingRes))
            Catch
                userRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CDB-PESCA-REINOSA", "Resources")
            End Try
        End If
        Directory.CreateDirectory(userRes)

        ' Ruta de recursos empaquetados junto al ejecutable (fallback). Si no existe,
        ' buscar hacia arriba en el árbol de carpetas un directorio "Resources" (útil en desarrollo).
        Dim appRes As String = Path.Combine(Application.StartupPath, "Resources")
        If Not Directory.Exists(appRes) Then
            Dim probe As String = Application.StartupPath
            For i As Integer = 0 To 6
                probe = Path.GetDirectoryName(probe)
                If String.IsNullOrEmpty(probe) Then Exit For
                Dim cand = Path.Combine(probe, "Resources")
                If Directory.Exists(cand) Then
                    appRes = cand
                    Exit For
                End If
            Next
        End If

        ' Migrar archivos que existan en la carpeta de la aplicación pero falten en userRes
        Try
            If Directory.Exists(appRes) Then
                For Each src In Directory.GetFiles(appRes)
                    Try
                        Dim fn = Path.GetFileName(src)
                        Dim dst As String = Path.Combine(userRes, fn)
                        If Not File.Exists(dst) Then
                            File.Copy(src, dst, True)
                        End If
                    Catch
                        ' Ignorar errores de copia por archivo
                    End Try
                Next
            End If
        Catch
            ' Ignorar errores generales de migración
        End Try

        ' Guardar la ruta de recursos del usuario (asegurar persistencia)
        My.Settings.ruta_recursos = userRes
        Try
            My.Settings.Save()
        Catch
            ' Ignorar si no se puede guardar settings
        End Try

        ' Cargar las imágenes comprobando existencia y usando fallback si hace falta
        Dim anversoPath As String = Path.Combine(userRes, bbdd.tarjeta_socio_anverso)
        Dim reversoPath As String = Path.Combine(userRes, bbdd.tarjeta_socio_reverso)

        If File.Exists(anversoPath) Then
            Dim bytes() As Byte = File.ReadAllBytes(anversoPath)
            Using ms As New MemoryStream(bytes)
                Using img As Image = Image.FromStream(ms)
                    pctbox_tsocio_anverso.Image = New Bitmap(img)
                End Using
            End Using
        ElseIf File.Exists(Path.Combine(appRes, bbdd.tarjeta_socio_anverso)) Then
            pctbox_tsocio_anverso.Image = Image.FromFile(Path.Combine(appRes, bbdd.tarjeta_socio_anverso))
        Else
            pctbox_tsocio_anverso.Image = Nothing
        End If

        If File.Exists(reversoPath) Then
            Dim bytes2() As Byte = File.ReadAllBytes(reversoPath)
            Using ms2 As New MemoryStream(bytes2)
                Using img2 As Image = Image.FromStream(ms2)
                    pctbox_tsocio_reverso.Image = New Bitmap(img2)
                End Using
            End Using
        ElseIf File.Exists(Path.Combine(appRes, bbdd.tarjeta_socio_reverso)) Then
            pctbox_tsocio_reverso.Image = Image.FromFile(Path.Combine(appRes, bbdd.tarjeta_socio_reverso))
        Else
            pctbox_tsocio_reverso.Image = Nothing
        End If

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
        txt_tabla_federativas_xls.Text = bbdd.tabla_federa_xls

    End Sub
    Private Sub OpenFile_reverso_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFile_reverso.FileOk
        Dim f As FileInfo = New FileInfo(OpenFile_reverso.FileName)
        Try
            ' Liberar imagen actual si existe
            If pctbox_tsocio_reverso.Image IsNot Nothing Then
                Dim old = pctbox_tsocio_reverso.Image
                pctbox_tsocio_reverso.Image = Nothing
                old.Dispose()
            End If

            ' Usar carpeta de AppData para recursos para evitar problemas de permisos en carpetas de programa
            Dim appData As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CDB-PESCA-REINOSA")
            Dim destDir As String = Path.Combine(appData, "Resources")
            Directory.CreateDirectory(destDir)
            Dim dest As String = Path.Combine(destDir, bbdd.tarjeta_socio_reverso)

            ' Si existe, asegurarse de quitar atributo readonly
            If File.Exists(dest) Then
                File.SetAttributes(dest, FileAttributes.Normal)
            End If

            File.Copy(f.FullName, dest, True)

            ' Cargar la imagen en memoria para no bloquear el archivo en disco
            Dim imgBytes() As Byte = File.ReadAllBytes(dest)
            Using ms As New MemoryStream(imgBytes)
                Using img As Image = Image.FromStream(ms)
                    pctbox_tsocio_reverso.Image = New Bitmap(img)
                End Using
            End Using

            ' Actualizar la ruta de recursos en configuración para próximas cargas
            My.Settings.ruta_recursos = destDir
            My.Settings.Save()

        Catch ua As UnauthorizedAccessException
            MessageBox.Show("Acceso denegado al copiar la imagen. Elija otra ubicación o ejecute con permisos adecuados." & vbCrLf & ua.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error al copiar/cargar la imagen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OpenFile_bbdd.Filter = "Archivos excel|*.xls;*.xlsx|Todos los archivos|*.*"
        OpenFile_bbdd.ShowDialog()
    End Sub

    Private Sub OpenFile_bbdd_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFile_bbdd.FileOk
        txt_archivo_excel.Text = OpenFile_bbdd.FileName
    End Sub

    Private Sub btn_guardar_Click(sender As Object, e As EventArgs) Handles btn_guardar.Click
        Try
            Dim sw As New StreamWriter(My.Settings.ruta_recursos & "\" & "configuracion.txt", False)
            sw.WriteLine("------Configuración - General------")
            sw.WriteLine("Temporada:")
            sw.WriteLine(cmb_temporada.SelectedItem.ToString())
            sw.WriteLine("Precio tarjeta  de Salmon(€):")
            sw.WriteLine(txt_tarjeta_salmon.Text)
            sw.WriteLine("Precio tarjeta  de Trucha(€):")
            sw.WriteLine(txt_tarjeta_trucha.Text)
            sw.WriteLine("------Configuración - Bases de datos------")
            sw.WriteLine("Tipo de base de datos:")
            sw.WriteLine(cmb_bd.SelectedItem.ToString())
            sw.WriteLine()
            sw.WriteLine("----Excel-ODBC----")
            sw.WriteLine("Archivo de base de datos:")
            sw.WriteLine(txt_archivo_excel.Text)
            sw.WriteLine("DSN:")
            sw.WriteLine(txt_dsn.Text)
            sw.WriteLine("Tabla de socios:")
            sw.WriteLine(txt_tabla_socios_xls.Text)
            sw.WriteLine("Tabla de base de datos de socios:")
            sw.WriteLine(txt_tabla_bdsocios_xls.Text)
            sw.WriteLine("Tabla de tarjetas federativas:")
            sw.WriteLine(txt_tabla_federativas_xls.Text)
            sw.WriteLine()

            sw.WriteLine("----MySQL----")
            sw.WriteLine("Servidor:")
            sw.WriteLine(txt_server.Text)
            sw.WriteLine("Puerto:")
            sw.WriteLine(txt_port.Text)
            sw.WriteLine("Base de datos:")
            sw.WriteLine(txt_bbdd.Text)
            sw.WriteLine("Usuario:")
            sw.WriteLine(txt_user.Text)
            sw.WriteLine("Contraseña:")
            sw.WriteLine(txt_password.Text)
            sw.WriteLine("Tabla de socios:")
            sw.WriteLine(txt_tabla_socios_mysql.Text)
            sw.WriteLine("Tabla de base de datos de socios:")
            sw.WriteLine(txt_tabla_bdsocios_mysql.Text)
            sw.WriteLine("Tabla de tarjetas federativas:")
            sw.WriteLine(txt_tabla_federativas_mysql.Text)
            sw.Close()
            MsgBox("Configuración guardada correctamente", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub OpenFile_anverso_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFile_anverso.FileOk
        Dim f As FileInfo = New FileInfo(OpenFile_anverso.FileName)
        Try
            ' Liberar imagen actual si existe
            If pctbox_tsocio_anverso.Image IsNot Nothing Then
                Dim old = pctbox_tsocio_anverso.Image
                pctbox_tsocio_anverso.Image = Nothing
                old.Dispose()
            End If

            ' Usar carpeta de AppData para recursos para evitar problemas de permisos en carpetas de programa
            Dim appData As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CDB-PESCA-REINOSA")
            Dim destDir As String = Path.Combine(appData, "Resources")
            Directory.CreateDirectory(destDir)
            Dim dest As String = Path.Combine(destDir, bbdd.tarjeta_socio_anverso)

            If File.Exists(dest) Then
                File.SetAttributes(dest, FileAttributes.Normal)
            End If

            File.Copy(f.FullName, dest, True)

            ' Cargar la imagen en memoria para no bloquear el archivo en disco
            Dim imgBytes() As Byte = File.ReadAllBytes(dest)
            Using ms As New MemoryStream(imgBytes)
                Using img As Image = Image.FromStream(ms)
                    pctbox_tsocio_anverso.Image = New Bitmap(img)
                End Using
            End Using

            ' Actualizar la ruta de recursos en configuración para próximas cargas
            My.Settings.ruta_recursos = destDir
            My.Settings.Save()

        Catch ua As UnauthorizedAccessException
            MessageBox.Show("Acceso denegado al copiar la imagen. Elija otra ubicación o ejecute con permisos adecuados." & vbCrLf & ua.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error al copiar/cargar la imagen: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_anverso_Click(sender As Object, e As EventArgs) Handles btn_anverso.Click
        OpenFile_anverso.Filter = "Imagenes|*.jpg;*.gif;*.png|Todos los archivos|*.*"
        OpenFile_anverso.ShowDialog()
    End Sub

    Private Sub btn_reverso_Click(sender As Object, e As EventArgs) Handles btn_reverso.Click
        OpenFile_reverso.Filter = "Imagenes|*.jpg;*.gif;*.png|Todos los archivos|*.*"
        OpenFile_reverso.ShowDialog()
    End Sub

    Private Sub btn_cerrar_Click(sender As Object, e As EventArgs) Handles btn_cerrar.Click
        Close()
    End Sub
End Class