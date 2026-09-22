Imports CLub_Deportivo.bbdd
Public Class frm_federativas

    Dim pago As Decimal
    Dim tipo As String = ""
    Dim importe As Decimal = 0
    Dim precio_competicion As Decimal = 50
    Dim precio_tarjeta As Decimal = 30
    ' Declaración a nivel de clase
    Private updating As Boolean = False
    Private Sub frm_federativas_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = frm_principal
        Try
            leer()

            ' Localidades (si sigue usando lista_localidades)
            cmb_localidad.Items.Clear()
            For i = 0 To lista_localidades.Count - 1
                cmb_localidad.Items.Add(lista_localidades(i).Nombre)
            Next

            ' Combo "Compite?" (SI/NO)
            cmb_compite.Items.Clear()
            cmb_compite.Items.Add("SI")
            cmb_compite.Items.Add("NO")
            cmb_compite.SelectedIndex = 1 ' por defecto NO

            ' Modalidad (vacío hasta que compita = SI)
            cmb_modalidad.Items.Clear()
            cmb_modalidad.Items.Add("MOSCA")
            cmb_modalidad.Items.Add("LANCE")
            cmb_modalidad.Enabled = False
            cmb_modalidad.SelectedIndex = -1

            rdo_normal.Checked = True
            importe = calcula_importe()
            lbl_importe.Text = "Importe: " & importe.ToString() & "€"
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Public Function calcula_importe() As Decimal
        Try
            If rdo_competicion.Checked Then
                ' Si compite, se suma precio de competición + tarjeta
                importe = precio_competicion + precio_tarjeta
            ElseIf rdo_normal.Checked Then
                importe = precio_tarjeta
            ElseIf rdo_gratis.Checked Then
                importe = 0
            Else
                importe = 0
            End If
            lbl_importe.Text = "Importe: " & importe.ToString() & "€"
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
        Try
            Dim edad As Integer = CInt(DateDiff(DateInterval.Year, dtpk_fecha_nac.Value, DateTime.Now))
            If edad < 16 Then
                rdo_gratis.Checked = True
            ElseIf edad >= 65 Then
                rdo_normal.Checked = True
            Else
                rdo_normal.Checked = True
            End If
            calcula_importe()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
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
    ''' <param name="fechanac"></param>
    ''' <param name="comentarios"></param>
    ''' <returns>Devuelve true si no hay campos vacíos. En caso de que que haya algún campo vacío devuelve false.</returns>
    Public Function validar_federativa(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, fechanac As String, telefono As String, comentarios As String) As Boolean

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
        End If

        ' Nueva comprobación: si en cmb_compite está "SI" entonces cmb_modalidad debe ser LANCE o MOSCA
        If cmb_compite.SelectedItem?.ToString()?.ToUpper() = "SI" Then
            If cmb_modalidad.Items.Count = 0 OrElse cmb_modalidad.SelectedIndex < 0 Then
                MsgBox("Debe seleccionar una modalidad (LANCE o MOSCA) cuando 'COMPITE' está en SI.")
                cmb_modalidad.Focus()
                Return False
            End If

            Dim modalidad As String = cmb_modalidad.SelectedItem?.ToString()
            If String.IsNullOrEmpty(modalidad) OrElse (modalidad.ToUpper() <> "LANCE" AndAlso modalidad.ToUpper() <> "MOSCA") Then
                MsgBox("La modalidad seleccionada no es válida. Elija LANCE o MOSCA.")
                cmb_modalidad.Focus()
                Return False
            End If
        End If

        Return True

    End Function

    Private Sub txt_buscar_nsocio_Click(sender As Object, e As EventArgs) Handles btn_buscar_nsocio.Click
        buscar_nsocio(txt_nsocio.Text)
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
        If updating Then Return
        Try
            updating = True
            If cmb_compite.Items.Count >= 1 AndAlso cmb_compite.SelectedIndex <> 0 Then
                cmb_compite.SelectedIndex = 0
                cmb_modalidad.Enabled = True
                cmb_modalidad.Items.Clear()
                cmb_modalidad.Items.Add("LANCE")
                cmb_modalidad.Items.Add("MOSCA")
            End If
            importe = calcula_importe()
            lbl_importe.Text = "Importe: " + calcula_importe().ToString() + "€"
        Finally
            updating = False
        End Try

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
            dtpk_fecha_nac.Value = New Date(2000, 1, 1)
            txt_direcc.ResetText()
            txt_cp.ResetText()
            txt_telefono.ResetText()
            txt_coment.ResetText()
            lbl_importe.ResetText()

            rdo_normal.Checked = True
            cmb_localidad.SelectedIndex = -1
            cmb_compite.SelectedIndex = 1
            cmb_modalidad.SelectedIndex = -1
            cmb_modalidad.Enabled = False
        Catch ex As Exception
            ' silenciar excepciones ligadas a control inexistente
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
            ' Validación (usar la función existente)
            Dim fechaValidacion As String = dtpk_fecha_nac.Value.ToString("yyyy-MM-dd")
            If validar_federativa(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, fechaValidacion, txt_telefono.Text, txt_coment.Text) Then
                Dim importeStr As String = calcula_importe().ToString().Replace(",", ".")
                ' Pasamos apellido2 vacío: bbdd.inserta_federativa separa si hace falta
                bbdd.inserta_federativa(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, String.Empty, txt_dni.Text, dtpk_fecha_nac.Value.ToString("dd/MM/yyyy"), txt_direcc.Text, cmb_localidad.Text, txt_cp.Text, cmb_modalidad.Text, importeStr, txt_telefono.Text, txt_coment.Text)
                bbdd.cargar()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub btn_modificar_Click(sender As Object, e As EventArgs) Handles btn_modificar.Click
        Try
            ' Validar campos (usa la función existente con 10 parámetros)
            Dim fechaValidacion As String = dtpk_fecha_nac.Value.Year.ToString & "-" & dtpk_fecha_nac.Value.Month.ToString & "-" & dtpk_fecha_nac.Value.Day.ToString
            If Not validar_federativa(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, fechaValidacion, txt_telefono.Text, txt_coment.Text) Then
                Return
            End If

            ' Separar apellidos en apellido1 / apellido2
            Dim apellidoFull As String = txt_apellido.Text.Trim()
            Dim apellido1 As String = String.Empty
            Dim apellido2 As String = String.Empty
            If Not String.IsNullOrWhiteSpace(apellidoFull) Then
                Dim partes = apellidoFull.Split(New Char() {" "c}, 2, StringSplitOptions.RemoveEmptyEntries)
                If partes.Length = 1 Then
                    apellido1 = partes(0)
                Else
                    apellido1 = partes(0)
                    apellido2 = partes(1)
                End If
            End If

            ' Confirmación del usuario
            Dim resp = MsgBox("Se va a modificar la tarjeta federativa del NIF: " & txt_dni.Text & ". ¿Desea continuar?", vbYesNo + vbQuestion, "Confirmar modificación")
            If resp <> vbYes Then Return

            ' Llamada al método de datos
            Dim importeStr As String = calcula_importe().ToString().Replace(",", ".")
            bbdd.modificar_federativa(
            txt_nsocio.Text,
            txt_nombre.Text,
            apellido1,
            apellido2,
            txt_dni.Text,
            dtpk_fecha_nac.Value.ToString("dd/MM/yyyy"),
            txt_direcc.Text,
            cmb_localidad.Text,
            txt_cp.Text,
            If(cmb_modalidad.SelectedItem IsNot Nothing, cmb_modalidad.SelectedItem.ToString(), String.Empty),
            importeStr,
            txt_telefono.Text,
            txt_coment.Text)

            ' Refrescar datos en memoria
            bbdd.cargar()
        Catch ex As Exception
            MsgBox("Error en modificación: " & ex.Message)
        End Try
    End Sub

    Private Sub btn_eliminar_Click(sender As Object, e As EventArgs) Handles btn_eliminar.Click
        Try
            If String.IsNullOrWhiteSpace(txt_dni.Text) Then
                MsgBox("Indique el NIF de la tarjeta federativa a eliminar (use Buscar para cargarla).")
                Return
            End If
            ' Confirmación del usuario (única: bbdd.eliminar_federativa ya no vuelve a preguntar)
            Dim resp = MsgBox("Va a eliminar la tarjeta federativa de " & txt_nombre.Text & " " & txt_apellido.Text & " (NIF: " & txt_dni.Text & "). ¿Desea continuar?", vbYesNo + vbQuestion, "Confirmar eliminación")
            If resp <> vbYes Then Return

            ' Separar apellidos como información (no necesario para la eliminación, pero la firma lo requiere)
            Dim apellidoFull As String = txt_apellido.Text.Trim()
            Dim apellido1 As String = String.Empty
            Dim apellido2 As String = String.Empty
            If Not String.IsNullOrWhiteSpace(apellidoFull) Then
                Dim partes = apellidoFull.Split(New Char() {" "c}, 2, StringSplitOptions.RemoveEmptyEntries)
                If partes.Length = 1 Then
                    apellido1 = partes(0)
                Else
                    apellido1 = partes(0)
                    apellido2 = partes(1)
                End If
            End If

            ' Llamada al método de datos para eliminar
            bbdd.eliminar_federativa(
            txt_nsocio.Text,
            txt_nombre.Text,
            apellido1,
            apellido2,
            txt_dni.Text,
            dtpk_fecha_nac.Value.ToString("dd/MM/yyyy"),
            txt_direcc.Text,
            cmb_localidad.Text,
            txt_cp.Text,
            If(cmb_modalidad.SelectedItem IsNot Nothing, cmb_modalidad.SelectedItem.ToString(), String.Empty),
            calcula_importe().ToString().Replace(",", "."),
            txt_telefono.Text,
            txt_coment.Text)

            ' Refrescar datos en memoria y limpiar el formulario
            bbdd.cargar()
            Me.reset()
        Catch ex As Exception
            MsgBox("Error en eliminación: " & ex.Message)
        End Try
    End Sub

    Private Sub cmb_compite_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_compite.SelectedIndexChanged
        If updating Then Return
        Try
            updating = True
            Try
                Select Case cmb_compite.SelectedItem?.ToString()
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
                        cmb_modalidad.SelectedIndex = -1
                End Select

                If cmb_compite.SelectedItem?.ToString() = "SI" Then
                    If Not rdo_competicion.Checked Then rdo_competicion.Checked = True
                ElseIf cmb_compite.SelectedItem?.ToString() = "NO" Then
                    If Not rdo_normal.Checked Then rdo_normal.Checked = True
                End If
                calcula_importe()
            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try
        Finally
            updating = False
        End Try
    End Sub

    Private Sub cmb_modalidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_modalidad.SelectedIndexChanged

    End Sub

    Private Sub rdo_normal_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdo_normal.CheckedChanged
        If updating Then Return
        Try
            updating = True
            If cmb_compite.Items.Count >= 1 AndAlso cmb_compite.SelectedIndex <> 1 Then
                cmb_compite.SelectedIndex = 1
                cmb_modalidad.Items.Clear()
                cmb_modalidad.Enabled = False
                cmb_modalidad.SelectedIndex = -1
            End If

            importe = calcula_importe()
            lbl_importe.Text = "Importe: " + calcula_importe().ToString() + "€"
        Finally
            updating = False
        End Try

    End Sub
End Class
