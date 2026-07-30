Imports CLub_Deportivo.bbdd
Public Class frm_federativas

    Dim pago As Decimal
    Dim tipo As String = ""
    Dim importe As Decimal = 0
    Dim precio_competicion As Decimal = 50
    Dim precio_tarjeta As Decimal = 30
    Private Sub frm_federativas_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = frm_principal
        Try
            leer()
            cmb_localidad.Items.Clear()
            For i = 0 To lista_localidades.Count - 1
                cmb_localidad.Items.Add(lista_localidades(i).Nombre)
            Next
            rdo_normal.Checked = True
            importe = calcula_importe()
            lbl_importe.Text = "Importe: " + importe.ToString() + "€"
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub
    Public Function calcula_importe() As Decimal

        Try
            If rdo_competicion.Checked Then
                importe = precio_competicion + precio_tarjeta * (cmb_compite.SelectedIndex + 1)
            End If
            If rdo_normal.Checked Then
                importe = precio_tarjeta
            End If
            If rdo_gratis.Checked Then
                importe = 0
            End If
            lbl_importe.Text = "Importe: " + importe.ToString() + "€"
            Return importe
        Catch ex As Exception
            MsgBox(ex.ToString())
            Return importe
        End Try
    End Function

    Private Sub cmb_provincias_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        If cmb_compite.SelectedItem = "CANTABRIA" Then
            cmb_localidad.Items.Clear()
            For i = 0 To lista_localidades.Count - 1
                cmb_localidad.Items.Add(lista_localidades(i).Nombre)
            Next
        Else
            cmb_localidad.Items.Clear()
            txt_cp.Clear()

            MsgBox("introduzca manualmente la localidad y el código postal")
            cmb_localidad.SelectedIndex = -1
            cmb_localidad.Text = ""
        End If
    End Sub

    Private Sub cmb_localidad_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        txt_cp.Text = lista_localidades(cmb_localidad.SelectedIndex).CP
    End Sub

    Public Sub foco_txt(sender As System.Object, e As System.EventArgs) Handles txt_nsocio.GotFocus, txt_telefono.GotFocus, txt_coment.GotFocus
        'Color de fondo: gris
        'Tipo de letra: cursiva.
        sender.Backcolor = Color.WhiteSmoke
        Dim fuente As New Font("San Serif", 8.25, FontStyle.Italic)

        sender.font = fuente

    End Sub
    Public Sub _salir_foco_txt(sender As System.Object, e As System.EventArgs) Handles txt_nsocio.LostFocus, txt_telefono.LostFocus, txt_coment.LostFocus
        'Color de fondo: gris
        'Tipo de letra: cursiva.
        sender.Backcolor = Color.White
        Dim fuente As New Font("San Serif", 8.25, FontStyle.Regular)

        sender.font = fuente

    End Sub

    Private Sub rdo_otros_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_gratis.CheckedChanged
        importe = calcula_importe()
        lbl_importe.Text = "Importe: " + importe.ToString() + "€"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        frm_numeros_libres.ShowDialog()


    End Sub

    Private Sub btn_buscar_apell_Click(sender As Object, e As EventArgs)
        buscar_nombre(txt_apellido.Text)
    End Sub

    Private Sub btn_buscar_dni_Click(sender As Object, e As EventArgs)
        buscar_dni(txt_dni.Text)
    End Sub

    Private Sub txt_nsocio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nsocio.KeyPress
        If Char.IsNumber(e.KeyChar) Or Asc(e.KeyChar) = Keys.Back Then

            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub dtpk_fecha_nac_ValueChanged(sender As Object, e As EventArgs) Handles dtpk_fecha_nac.ValueChanged

        If (DateDiff(DateInterval.Year, dtpk_fecha_nac.Value, DateTime.Now) > 65) Then
            rdo_normal.Checked = True

        End If


    End Sub
    ''' <summary>
    ''' Método que valida los campos de la tabla de socios que se reciben como parámentros, para que no queden campos vacíos y evitar errores posteriores en las consultas. EN caso de que haya algún campo vacío muestra un mensaje informando del campo vacío.
    ''' </summary>
    ''' <param name="nsocio">numero de socio (texto)</param>
    ''' <param name="nombre">nombre de socio (texto)</param>
    ''' <param name="apellidos">apellidos de socio (texto)</param>
    ''' <param name="dni">DNI del socio (texto)</param>
    ''' <param name="direcc">dirección postal de socio (texto)</param>
    ''' <param name="cp">codigo postal del socio (texto)</param>
    ''' <param name="localidad"></param>
    ''' <param name="provincia"></param>
    ''' <param name="pais"></param>
    ''' <param name="fechanac"></param>
    ''' <param name="email"></param>
    ''' <param name="tipo_socio"></param>
    ''' <param name="comentarios"></param>
    ''' <returns>Devuelve true si no hay campos vacíos. En caso de que que haya algún campo vacío devuelve false.</returns>
    Public Function validar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, fechanac As String, telefono As String, comentarios As String) As Boolean

        Dim msg As String = "Debe completar obligatoriamente, al menos los siguientes datos: "
        Dim contador As Integer = 0
        If (nsocio = "") Then
            msg += "Número de socio, "
            contador += 1
        End If
        If (nombre = "") Then
            msg += "Nombre, "
            contador += 1
        End If
        If (apellidos = "") Then
            msg += "Apellidos, "
            contador += 1
        End If
        If (dni = "") Then
            msg += "DNI, "
            contador += 1
        End If
        If (direcc = "") Then
            msg += "Dirección, "
            contador += 1
        End If
        If (cp = "") Then
            msg += "Código Postal, "
            contador += 1
        End If
        If (localidad = "") Then
            msg += "Localidad, "
            contador += 1
        End If
        If (fechanac = "") Then
            msg += "Fecha de nacimiento, "
            contador += 1
        End If
        If (telefono = "") Then
            txt_telefono.Text = " "
        End If
        If (comentarios = "") Then
            txt_coment.Text = " "
        End If

        If (contador > 0) Then
            MsgBox(msg)
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub txt_buscar_nsocio_Click(sender As Object, e As EventArgs) Handles btn_buscar_nsocio.Click
        buscar_nsocio(txt_nsocio.Text)
    End Sub
    Private Sub rdo_jubilado_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_normal.CheckedChanged

        lbl_importe.Text = "Importe: " + calcula_importe().ToString() + "€"
    End Sub

    Private Sub btn_buscar_dni_Click_1(sender As Object, e As EventArgs) Handles btn_buscar_dni.Click
        buscar_dni(txt_dni.Text)
    End Sub
    Private Sub btn_buscar_apell_Click_1(sender As Object, e As EventArgs) Handles btn_buscar_apell.Click
        buscar_nombre(txt_apellido.Text)
    End Sub

    Private Sub cmb_tarjeta_SelectedIndexChanged_1(sender As Object, e As EventArgs)
        lbl_importe.Text = "Importe: " + calcula_importe().ToString() + "€"
    End Sub

    Private Sub rdo_normal_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_competicion.CheckedChanged
        importe = calcula_importe()
        lbl_importe.Text = "Importe: " + calcula_importe().ToString() + "€"
    End Sub


    Private Sub cmb_localidad_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cmb_localidad.SelectedIndexChanged
        Try
            Dim localidad_selected As String = cmb_localidad.SelectedItem
            For i = 0 To lista_localidades.Count - 1
                If (lista_localidades(i).Nombre = localidad_selected) Then
                    txt_cp.Text = lista_localidades(i).CP
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try


    End Sub
    Private Sub btn_cerrar_Click(sender As Object, e As EventArgs) Handles btn_cerrar.Click

        Me.Close()

    End Sub
    ''' <summary>
    ''' Método que borra el contenido de los controles del formulario.
    ''' </summary>
    Public Sub reset()
        Try
            txt_nsocio.ResetText()
            txt_nombre.Clear()
            txt_apellido.ResetText()
            txt_dni.ResetText()
            dtpk_fecha_nac.Value = "2000/01/01"
            txt_direcc.ResetText()
            txt_cp.ResetText()
            txt_telefono.ResetText()
            txt_coment.ResetText()
            lbl_importe.ResetText()

            rdo_normal.Checked = True
            cmb_localidad.SelectedIndex = -1
            cmb_compite.SelectedIndex = -1
            cmb_modalidad.SelectedIndex = -1

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btn_reset_Click(sender As Object, e As EventArgs) Handles btn_reset.Click
        Me.reset()

    End Sub

    Private Sub btn_socios_Click(sender As Object, e As EventArgs) Handles btn_socios.Click
        frm_busqueda.DataGridView1.DataSource = bbdd.ds_club.Tables(2)

        frm_busqueda.ShowDialog()
        'frm_busqueda.btn_usar.Visible = False
    End Sub

    Private Sub btn_insertar_Click(sender As Object, e As EventArgs) Handles btn_insertar.Click
        Try
            ' Formatear fecha para la validación
            Dim fechaValidacion As String = dtpk_fecha_nac.Value.Year.ToString & "-" & dtpk_fecha_nac.Value.Month.ToString & "-" & dtpk_fecha_nac.Value.Day.ToString

            ' LLAMADA A validar_socio: exactamente 10 parámetros
            If (validar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, fechaValidacion, txt_telefono.Text, txt_coment.Text)) Then
                Dim importe As String = calcula_importe().ToString().Replace(",", ".")
                ' Llamar al método específico de federativas en bbdd
                bbdd.inserta_federativa(
                txt_nsocio.Text,
                txt_nombre.Text,
                txt_apellido.Text,   ' pasamos todo en apellido1; bbdd separa si hace falta
                "",                  ' apellido2 vacío (bbdd hará separación si procede)
                txt_dni.Text,
                dtpk_fecha_nac.Value.ToString("dd/MM/yyyy"),
                txt_direcc.Text,
                cmb_localidad.Text,
                txt_cp.Text,
                cmb_modalidad.Text,
                importe,
                txt_telefono.Text,
                txt_coment.Text)

                bbdd.cargar()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub btn_modificar_Click(sender As Object, e As EventArgs) Handles btn_modificar.Click
        'Try
        '    If (validar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_compite.Text, cmb_modalidad.Text, dtpk_fecha_nac.Value.Year.ToString + "-" + dtpk_fecha_nac.Value.Month.ToString + "-" + dtpk_fecha_nac.Value.Day.ToString, txt_telefono.Text, tipo.ToString, txt_coment.Text)) Then
        '        modificar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_compite.Text, cmb_modalidad.Text, dtpk_fecha_nac.Value.Day.ToString + "/" + dtpk_fecha_nac.Value.Month.ToString + "/" + dtpk_fecha_nac.Value.Year.ToString, txt_telefono.Text, (cmb_compite.SelectedIndex + 1).ToString, tipo.ToString, calcula_importe().ToString, txt_coment.Text)
        '        bbdd.cargar()
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.ToString())
        'End Try
    End Sub

    Private Sub btn_eliminar_Click(sender As Object, e As EventArgs) Handles btn_eliminar.Click
        Try
            eliminar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_compite.Text, cmb_modalidad.Text, dtpk_fecha_nac.Value.Year.ToString + "-" + dtpk_fecha_nac.Value.Month.ToString + "-" + dtpk_fecha_nac.Value.Day.ToString, txt_telefono.Text, (cmb_compite.SelectedIndex + 1).ToString, tipo.ToString, pago.ToString, txt_coment.Text)
            bbdd.cargar()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub cmb_compite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_compite.SelectedIndexChanged
        Try
            Select Case cmb_compite.SelectedItem
                Case "SI"
                    cmb_modalidad.Enabled = True
                    rdo_competicion.Checked = True
                    cmb_modalidad.Items.Clear()
                    cmb_modalidad.Items.Add("LANCE")
                    cmb_modalidad.Items.Add("MOSCA")

                Case "NO"
                    rdo_normal.Checked = True
                    cmb_modalidad.Items.Clear()
                    cmb_modalidad.Enabled = False

            End Select
            calcula_importe()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub cmb_modalidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_modalidad.SelectedIndexChanged

    End Sub
End Class
