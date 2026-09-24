Public Class frm_principal

    Private Sub SociosToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SociosToolStripMenuItem.Click

    End Sub

    Private Sub frm_principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            bbdd.leer_configuracion()
            ' Vaciar las "papeleras" (socios y federativas) AL ARRANCAR: es el único momento en que el
            ' controlador ODBC todavía no ha abierto el .xls. Una vez usado, lo mantiene
            ' bloqueado hasta que se cierra el programa y Excel no puede modificarlo.
            Dim purgadas As Integer = bbdd.PurgarPapeleras(False)
            If purgadas > 0 Then
                MsgBox("Se han eliminado definitivamente del Excel " & purgadas.ToString() & " registro(s) (socios y/o tarjetas federativas) borrados en la sesión anterior.")
            End If
            bbdd.cargar()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click

        ' Cierre ordenado ("End" terminaba el proceso de golpe, sin eventos de cierre).
        ' La purga de federativas se hace al arrancar la aplicación (ver frm_principal_Load).
        Me.Close()

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
                Select Case bbdd.tp
                    Case tipobd.Excel_ODBC
                        da_socios2.Update(ds_club.Tables(0))
                        da_bdsocios2.Update(ds_club.Tables(1))
                        da_federa2.Update(ds_club.Tables(2))

                    Case tipobd.MySQL
                        da_socios1.Update(ds_club.Tables(0))
                        da_bdsocios1.Update(ds_club.Tables(1))
                    Case Else
                End Select

                desconectar()
                ds_club.AcceptChanges()
                bbdd.cargar()
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
        frm_socio.btn_insertar.Visible = True ' si antes se abrió en modo modificar/eliminar quedaba oculto

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

    Private Sub ConfiguraciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraciónToolStripMenuItem.Click
        frm_configuracion.Show()
    End Sub

    Private Sub ActuaizarBBDDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActuaizarBBDDToolStripMenuItem.Click
        ' Ventana nueva cada vez: así la comparación se hace siempre con los datos actuales
        Using f As New frm_actualizar_bbdd()
            f.ShowDialog(Me)
        End Using
    End Sub

    Private Sub NuevaTarjetaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevaTarjetaToolStripMenuItem.Click
        frm_federativas.MdiParent = Me
        frm_federativas.Show()
        frm_federativas.btn_buscar_nsocio.Visible = True
        frm_federativas.btn_buscar_dni.Visible = True
        frm_federativas.btn_buscar_apell.Visible = True
        frm_federativas.btn_eliminar.Visible = False
        frm_federativas.btn_modificar.Visible = False
        frm_federativas.btn_insertar.Visible = True ' si antes se abrió en modo modificar/eliminar quedaba oculto
    End Sub

    Private Sub ModificarTarjetaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModificarTarjetaToolStripMenuItem.Click
        frm_federativas.MdiParent = Me
        frm_federativas.Show()
        frm_federativas.btn_buscar_nsocio.Visible = True
        frm_federativas.btn_buscar_dni.Visible = True
        frm_federativas.btn_buscar_apell.Visible = True
        frm_federativas.btn_eliminar.Visible = False
        frm_federativas.btn_modificar.Visible = True
        frm_federativas.btn_insertar.Visible = False
    End Sub

    Private Sub EliminarTarjetaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarTarjetaToolStripMenuItem.Click
        frm_federativas.MdiParent = Me
        frm_federativas.Show()
        frm_federativas.btn_buscar_nsocio.Visible = True
        frm_federativas.btn_buscar_dni.Visible = True
        frm_federativas.btn_buscar_apell.Visible = True
        frm_federativas.btn_eliminar.Visible = True
        frm_federativas.btn_modificar.Visible = False
        frm_federativas.btn_insertar.Visible = False
    End Sub

    ''' <summary>
    ''' Archivo -> Exportar -> Listado socios: genera un PDF con todos los socios de la temporada actual
    ''' (título centrado, línea en blanco y tabla N / NOMBRE / APELLIDOS / DNI / DIRECCION / LOCALIDAD).
    ''' </summary>
    Private Sub ListadoSociosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListadoSociosToolStripMenuItem.Click
        Using dlg As New SaveFileDialog()
            dlg.Title = "Exportar listado de socios"
            dlg.Filter = "Documento PDF (*.pdf)|*.pdf"
            dlg.DefaultExt = "pdf"
            dlg.FileName = "Listado_socios_" & listado_pdf.TextoTemporada() & ".pdf"
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return

            Dim total As Integer
            Try
                Me.Cursor = Cursors.WaitCursor
                total = listado_pdf.GenerarListadoSocios(dlg.FileName)
            Catch ex As IO.IOException
                MsgBox("No se ha podido guardar el PDF. Compruebe que no está abierto en otro programa." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                Return
            Catch ex As Exception
                MsgBox("No se ha podido generar el listado de socios: " & ex.Message, MsgBoxStyle.Critical)
                Return
            Finally
                Me.Cursor = Cursors.Default
            End Try

            If MsgBox("Listado generado con " & total.ToString() & " socios:" & vbCrLf & dlg.FileName & vbCrLf & vbCrLf & "¿Desea abrirlo ahora?",
                      MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Listado de socios") = MsgBoxResult.Yes Then
                Try
                    Process.Start(dlg.FileName)
                Catch ex As Exception
                    MsgBox("El PDF se ha guardado, pero no se ha podido abrir: " & ex.Message)
                End Try
            End If
        End Using
    End Sub
End Class
