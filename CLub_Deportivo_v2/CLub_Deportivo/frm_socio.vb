Imports CLub_Deportivo.bbdd
Public Class frm_socio

    Dim tipo As String = ""
    Dim importe As Decimal = 0

    Private Sub frm_socio_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = frm_principal

        leer()
        cmb_prov.Items.Clear()   ' evita provincias duplicadas si el formulario se vuelve a cargar
        For i = 0 To lista_provincias.Count - 1
            cmb_prov.Items.Add(lista_provincias(i))
        Next
        cmb_prov.SelectedItem = "CANTABRIA"
        cmb_pais.SelectedItem = "ESPAÑA"
        ' La tarjeta solo se puede ELEGIR de la lista: si se escribía el texto a mano ("salmon"),
        ' no quedaba ningún elemento seleccionado y el importe era 0.
        cmb_tarjeta.DropDownStyle = ComboBoxStyle.DropDownList
        cmb_tarjeta.SelectedIndex = -1
        If precio_salmon = 0 OrElse precio_trucha = 0 Then
            MsgBox("Atención: el precio de alguna tarjeta es 0. Revise los precios en Configuración.")
        End If
        ActualizarImporte()
    End Sub

    ' ------------------------------------------------------------------------------
    '  Cálculo del importe
    ' ------------------------------------------------------------------------------
    Private Function calcula_importe() As Decimal
        ' El precio se elige por el TEXTO de la tarjeta (SALMON / TRUCHA), no por la posición en la lista.
        Try
            Dim precio As Decimal
            Select Case Convert.ToString(cmb_tarjeta.SelectedItem).Trim().ToUpperInvariant()
                Case "SALMON", "SALMÓN" : precio = precio_salmon
                Case "TRUCHA" : precio = precio_trucha
                Case Else : Return 0
            End Select
            If rdo_jubilado.Checked Then Return precio / 2
            If rdo_otros.Checked Then Return 0
            Return precio
        Catch ex As Exception
            MsgBox(ex.ToString())
            Return 0
        End Try
    End Function

    ''' <summary>Muestra el importe con el formato "Importe: 17 €" (o "Importe: 8,5 €" para jubilados).</summary>
    Private Sub ActualizarImporte()
        importe = calcula_importe()
        If cmb_tarjeta.SelectedIndex < 0 Then
            lbl_importe.Text = "Importe: -- €"   ' aún no se ha elegido tarjeta
        Else
            lbl_importe.Text = "Importe: " & importe.ToString("0.##") & " €"
        End If
    End Sub

    ''' <summary>Recalcula el importe (lo usa Configuración al cambiar los precios).</summary>
    Public Sub RefrescarImporte()
        ActualizarImporte()
    End Sub

    Private Sub CambioImporte(sender As Object, e As EventArgs) Handles rdo_normal.CheckedChanged, rdo_jubilado.CheckedChanged, rdo_otros.CheckedChanged, cmb_tarjeta.SelectedIndexChanged
        ActualizarImporte()
    End Sub

    ''' <summary>Tipo de socio según el botón de opción marcado.</summary>
    Private Function TipoSocio() As String
        If rdo_jubilado.Checked Then Return "JUBILADO"
        If rdo_otros.Checked Then Return "OTROS"
        Return "NORMAL"
    End Function

    ''' <summary>Fecha de nacimiento siempre con el mismo formato (antes: "5/3/1970" sin ceros).</summary>
    Private Function FechaNac() As String
        Return dtpk_fecha_nac.Value.ToString("dd/MM/yyyy")
    End Function

    ''' <summary>Edad real en años (DateDiff por años solo resta los años del calendario).</summary>
    Private Function Edad(nacimiento As Date) As Integer
        Dim hoy = Date.Today
        Dim anos = hoy.Year - nacimiento.Year
        If nacimiento.Date > hoy.AddYears(-anos) Then anos -= 1
        Return anos
    End Function

    Private Sub dtpk_fecha_nac_ValueChanged(sender As Object, e As EventArgs) Handles dtpk_fecha_nac.ValueChanged
        ' 65 o más: jubilado. Menor de 16: gratis. Entre medias se quita "jubilado" si estaba
        ' marcado, pero se respeta "otros" (féminas, etc., que se eligen a mano).
        Dim anos = Edad(dtpk_fecha_nac.Value)
        If anos >= 65 Then
            rdo_jubilado.Checked = True
        ElseIf anos < 16 Then
            rdo_otros.Checked = True
        ElseIf rdo_jubilado.Checked Then
            rdo_normal.Checked = True
        End If
    End Sub

    ' ------------------------------------------------------------------------------
    '  Aspecto de los cuadros de texto
    ' ------------------------------------------------------------------------------
    Public Sub foco_txt(sender As System.Object, e As System.EventArgs) Handles txt_nsocio.GotFocus, txt_email.GotFocus, txt_coment.GotFocus
        'Color de fondo: gris. Tipo de letra: cursiva.
        sender.Backcolor = Color.WhiteSmoke
        sender.font = New Font("San Serif", 8.25, FontStyle.Italic)
    End Sub
    Public Sub _salir_foco_txt(sender As System.Object, e As System.EventArgs) Handles txt_nsocio.LostFocus, txt_email.LostFocus, txt_coment.LostFocus
        'Color de fondo: blanco. Tipo de letra: normal.
        sender.Backcolor = Color.White
        sender.font = New Font("San Serif", 8.25, FontStyle.Regular)
    End Sub

    Private Sub txt_nsocio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nsocio.KeyPress
        ' Solo dígitos y teclas de control (borrar, copiar/pegar...)
        e.Handled = Not (Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar))
    End Sub

    ' ------------------------------------------------------------------------------
    '  Provincia / localidad
    ' ------------------------------------------------------------------------------
    Private Sub cmb_prov_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_prov.SelectedIndexChanged
        Try
            cmb_localidad.Items.Clear()
            If Convert.ToString(cmb_prov.SelectedItem) = "CANTABRIA" Then
                For i = 0 To lista_localidades.Count - 1
                    cmb_localidad.Items.Add(lista_localidades(i).Nombre)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub cmb_localidad_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cmb_localidad.SelectedIndexChanged
        Try
            Dim localidad_selected As String = Convert.ToString(cmb_localidad.SelectedItem)
            For i = 0 To lista_localidades.Count - 1
                If lista_localidades(i).Nombre = localidad_selected Then
                    txt_cp.Text = lista_localidades(i).CP
                    Exit For
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    ' ------------------------------------------------------------------------------
    '  Validación
    ' ------------------------------------------------------------------------------
    ''' <summary>
    ''' Valida que no falten datos obligatorios. Si falta alguno muestra un mensaje con todos ellos.
    ''' </summary>
    ''' <returns>True si los datos son correctos.</returns>
    Public Function validar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, comentarios As String) As Boolean
        Dim faltan As New List(Of String)
        If nsocio.Trim() = "" Then faltan.Add("Número de socio")
        If nombre.Trim() = "" Then faltan.Add("Nombre")
        If apellidos.Trim() = "" Then faltan.Add("Apellidos")
        If dni.Trim() = "" Then faltan.Add("DNI")
        If direcc.Trim() = "" Then faltan.Add("Dirección")
        If cp.Trim() = "" Then faltan.Add("Código Postal")
        If localidad.Trim() = "" Then faltan.Add("Localidad")
        If provincia.Trim() = "" Then faltan.Add("Provincia")
        If pais.Trim() = "" OrElse pais = "Seleccionar..." Then faltan.Add("País")
        If tarjeta = "0" Then faltan.Add("Tipo de tarjeta")
        If tipo_socio = "" Then faltan.Add("Tipo de socio")

        If faltan.Count > 0 Then
            MsgBox("Debe completar obligatoriamente los siguientes datos: " & String.Join(", ", faltan) & ".")
            Return False
        End If

        ' Documento de identidad: DNI o NIE con letra correcta, o pasaporte/documento extranjero
        Select Case textbox_dni.TipoDocumento(dni)
            Case "DNI", "NIE"
                If Not textbox_dni.LetraCorrecta(dni) Then
                    MsgBox("La letra del " & textbox_dni.TipoDocumento(dni) & " no es correcta. Use el botón CALCULA LETRA.")
                    txt_dni.Focus()
                    Return False
                End If
            Case Else
                ' Pasaporte u otro documento extranjero: sin formato fijo, pero con un mínimo de caracteres
                If System.Text.RegularExpressions.Regex.Replace(dni, "[^A-Za-z0-9]", "").Length < 5 Then
                    MsgBox("El documento de identidad no es válido. Escriba un DNI (12345678-Z), un NIE (X-1234567-L) o el número de pasaporte.")
                    txt_dni.Focus()
                    Return False
                End If
                If MsgBox("'" & dni.Trim() & "' no es un DNI ni un NIE español." & vbCrLf & "¿Es un pasaporte u otro documento extranjero?", vbYesNo + vbQuestion, "Documento extranjero") <> vbYes Then
                    txt_dni.Focus()
                    Return False
                End If
        End Select
        Return True
    End Function

    ''' <summary>Valida los datos del formulario tal como están ahora.</summary>
    Private Function ValidarFormulario() As Boolean
        tipo = TipoSocio()
        Return validar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_prov.Text, cmb_pais.Text, FechaNac(), txt_email.Text, (cmb_tarjeta.SelectedIndex + 1).ToString(), tipo, txt_coment.Text)
    End Function

    ' ------------------------------------------------------------------------------
    '  Botones de búsqueda
    ' ------------------------------------------------------------------------------
    Private Sub txt_buscar_nsocio_Click(sender As Object, e As EventArgs) Handles btn_buscar_nsocio.Click
        buscar_nsocio(txt_nsocio.Text)
    End Sub

    Private Sub btn_buscar_dni_Click_1(sender As Object, e As EventArgs) Handles btn_buscar_dni.Click
        buscar_dni(txt_dni.Text)
    End Sub

    Private Sub btn_buscar_apell_Click_1(sender As Object, e As EventArgs) Handles btn_buscar_apell.Click
        buscar_nombre(txt_apellido.Text)
    End Sub

    Private Sub btn_socios_Click(sender As Object, e As EventArgs) Handles btn_socios.Click
        frm_busqueda.DataGridView1.DataSource = bbdd.ds_club.Tables(0)
        frm_busqueda.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btn_num_libres.Click
        frm_numeros_libres.ShowDialog()
    End Sub

    Private Sub btn_letranif_Click_1(sender As Object, e As EventArgs) Handles btn_letranif.Click
        ' Antes este botón no hacía nada
        Dim nif = txt_dni.CalcularLetra()
        If nif = "" Then
            MsgBox("Para calcular la letra escriba los 8 números del DNI, o la X/Y/Z y los 7 números del NIE." & vbCrLf & "(Los pasaportes no llevan letra de control.)")
        Else
            txt_dni.Text = nif
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frm_imprimir.ShowDialog()
    End Sub

    ' ------------------------------------------------------------------------------
    '  Insertar / modificar / eliminar
    ' ------------------------------------------------------------------------------
    Private Sub btn_insertar_Click(sender As Object, e As EventArgs) Handles btn_insertar.Click
        Try
            If Not ValidarFormulario() Then Return
            If insertar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_prov.Text, cmb_pais.Text, FechaNac(), txt_email.Text, (cmb_tarjeta.SelectedIndex + 1).ToString(), tipo, calcula_importe().ToString(), txt_coment.Text) Then
                bbdd.cargar()
                Me.reset()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub btn_modificar_Click(sender As Object, e As EventArgs) Handles btn_modificar.Click
        Try
            If Not ValidarFormulario() Then Return
            modificar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_prov.Text, cmb_pais.Text, FechaNac(), txt_email.Text, (cmb_tarjeta.SelectedIndex + 1).ToString(), tipo, calcula_importe().ToString(), txt_coment.Text)
            bbdd.cargar()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub btn_eliminar_Click(sender As Object, e As EventArgs) Handles btn_eliminar.Click
        Try
            If String.IsNullOrWhiteSpace(txt_dni.Text) Then
                MsgBox("Indique el DNI/NIE o pasaporte del socio a eliminar (use BUSCAR para cargarlo).")
                Return
            End If
            ' Confirmación (única: bbdd.eliminar_socio ya no vuelve a preguntar)
            Dim resp = MsgBox("Va a eliminar de la temporada actual al socio " & txt_nombre.Text & " " & txt_apellido.Text & " (DNI: " & txt_dni.Text & ")." & vbCrLf & "¿Desea continuar?", vbYesNo + vbQuestion, "Confirmar eliminación")
            If resp <> vbYes Then Return

            eliminar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_prov.Text, cmb_pais.Text, FechaNac(), txt_email.Text, (cmb_tarjeta.SelectedIndex + 1).ToString(), TipoSocio(), calcula_importe().ToString(), txt_coment.Text)
            bbdd.cargar()
            Me.reset()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    ' ------------------------------------------------------------------------------
    '  Resetear / cerrar
    ' ------------------------------------------------------------------------------
    ''' <summary>
    ''' Borra el contenido de los controles y deja los valores por defecto (Cantabria, España, 01/01/2000).
    ''' </summary>
    Public Sub reset()
        Try
            txt_nsocio.ResetText()
            txt_nombre.Clear()
            txt_apellido.ResetText()
            txt_dni.ResetText()
            txt_direcc.ResetText()
            txt_cp.ResetText()
            txt_email.ResetText()
            txt_coment.ResetText()
            cmb_tarjeta.SelectedIndex = -1
            cmb_prov.SelectedItem = "CANTABRIA"
            cmb_localidad.SelectedIndex = -1
            cmb_localidad.Text = ""
            cmb_pais.SelectedItem = "ESPAÑA"
            dtpk_fecha_nac.Value = New Date(2000, 1, 1)
            rdo_normal.Checked = True
            ActualizarImporte()
        Catch ex As Exception
            ' silenciar excepciones ligadas a controles no inicializados
        End Try
    End Sub

    Private Sub btn_reset_Click(sender As Object, e As EventArgs) Handles btn_reset.Click
        Me.reset()
    End Sub

    Private Sub btn_cerrar_Click(sender As Object, e As EventArgs) Handles btn_cerrar.Click
        Me.Close()
    End Sub

End Class
