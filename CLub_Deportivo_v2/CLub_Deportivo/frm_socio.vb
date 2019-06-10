Public Class frm_socio




    Dim pago As Integer
    Dim tipo As String = ""

    Private Sub frm_socio_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = frm_principal

        leer()
        For i = 0 To lista_provincias.Count - 1
            cmb_prov.Items.Add(lista_provincias(i))
        Next
        cmb_prov.SelectedItem = "CANTABRIA"
        cmb_pais.SelectedItem = "ESPAÑA"
    End Sub


    Private Sub cmb_provincias_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmb_prov.SelectedIndexChanged
        If cmb_prov.SelectedItem = "CANTABRIA" Then
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

    Private Sub cmb_localidad_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmb_localidad.SelectedIndexChanged
        txt_cp.Text = lista_localidades(cmb_localidad.SelectedIndex).CP
    End Sub

    Public Sub foco_txt(sender As System.Object, e As System.EventArgs) Handles txt_nsocio.GotFocus, txt_email.GotFocus, txt_direcc.GotFocus, txt_coment.GotFocus
        'Color de fondo: gris
        'Tipo de letra: cursiva.
        sender.Backcolor = Color.WhiteSmoke
        Dim fuente As New Font("San Serif", 8.25, FontStyle.Italic)

        sender.font = fuente

    End Sub
    Public Sub _salir_foco_txt(sender As System.Object, e As System.EventArgs) Handles txt_nsocio.LostFocus, txt_email.LostFocus, txt_direcc.LostFocus, txt_coment.LostFocus
        'Color de fondo: gris
        'Tipo de letra: cursiva.
        sender.Backcolor = Color.White
        Dim fuente As New Font("San Serif", 8.25, FontStyle.Regular)

        sender.font = fuente

    End Sub

    Private Sub rdo_otros_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_otros.CheckedChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_insertar.Click

        If rdo_normal.Checked Then
            tipo = "NORMAL"
        End If
        If rdo_jubilado.Checked Then
            tipo = "JUBILADO"
        End If
        If rdo_otros.Checked Then
            tipo = "MENOR, FEMINA, OTROS"
        End If


        If rdo_pagado.Checked Then
            pago = 1
        End If
        If rdo_nopagado.Checked Then
            pago = 2
        End If


        If (validar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_prov.Text, cmb_pais.Text, dtpk_fecha_nac.Value.Year.ToString + "-" + dtpk_fecha_nac.Value.Month.ToString + "-" + dtpk_fecha_nac.Value.Day.ToString, txt_email.Text, (cmb_tarjeta.SelectedIndex + 1).ToString, tipo.ToString, pago.ToString, txt_coment.Text)) Then
            insertar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_prov.Text, cmb_pais.Text, dtpk_fecha_nac.Value.Year.ToString + "-" + dtpk_fecha_nac.Value.Month.ToString + "-" + dtpk_fecha_nac.Value.Day.ToString, txt_email.Text, (cmb_tarjeta.SelectedIndex + 1).ToString, tipo.ToString, pago.ToString, txt_coment.Text)
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btn_num_libres.Click
        frm_numeros_libres.Show()


    End Sub

    Private Sub btn_buscar_apell_Click(sender As Object, e As EventArgs) Handles btn_buscar_apell.Click
        buscar_nombre(txt_apellido.Text)
    End Sub

    Private Sub btn_buscar_dni_Click(sender As Object, e As EventArgs)
        buscar_dni(txt_dni.Text)
    End Sub

    Private Sub txt_nsocio_TextChanged(sender As Object, e As EventArgs) Handles txt_nsocio.TextChanged

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
            rdo_jubilado.Checked = True

        End If


    End Sub

    Public Function validar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, pago As String, comentarios As String) As Boolean

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
        If (provincia = "") Then
            msg += "Provincia, "
            contador += 1
        End If
        If (pais = "") Then
            msg += "Pais, "
            contador += 1
        End If
        If (tarjeta = "0") Then
            msg += "Tarjeta, "
            contador += 1
        End If
        If (tipo_socio = "") Then
            msg += "Tipo socio, "
            contador += 1
        End If
        If (pago = "") Then
            msg += "Estado(pago), "
            contador += 1
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

    Private Sub grp_socio_Enter(sender As Object, e As EventArgs) Handles grp_socio.Enter

    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txt_cp_TextChanged(sender As Object, e As EventArgs) Handles txt_cp.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub grp_commnent_Enter(sender As Object, e As EventArgs) Handles grp_commnent.Enter

    End Sub

    Private Sub grp_estado_Enter(sender As Object, e As EventArgs) Handles grp_estado.Enter

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub cmb_pais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_pais.SelectedIndexChanged

    End Sub

    Private Sub cmb_tarjeta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_tarjeta.SelectedIndexChanged

    End Sub

    Private Sub txt_email_TextChanged(sender As Object, e As EventArgs) Handles txt_email.TextChanged

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub txt_direcc_TextChanged(sender As Object, e As EventArgs) Handles txt_direcc.TextChanged

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txt_dni_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btn_letranif_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txt_apellido_TextChanged(sender As Object, e As EventArgs) Handles txt_apellido.TextChanged

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub txt_nombre_TextChanged(sender As Object, e As EventArgs) Handles txt_nombre.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btn_eliminar.Click
        eliminar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_prov.Text, cmb_pais.Text, dtpk_fecha_nac.Value.Year.ToString + "-" + dtpk_fecha_nac.Value.Month.ToString + "-" + dtpk_fecha_nac.Value.Day.ToString, txt_email.Text, (cmb_tarjeta.SelectedIndex + 1).ToString, tipo.ToString, pago.ToString, txt_coment.Text)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btn_modificar.Click
        If rdo_normal.Checked Then
            tipo = "NORMAL"
        End If
        If rdo_jubilado.Checked Then
            tipo = "JUBILADO"
        End If
        If rdo_otros.Checked Then
            tipo = "MENOR, FEMINA, OTROS"
        End If


        If rdo_pagado.Checked Then
            pago = 1
        End If
        If rdo_nopagado.Checked Then
            pago = 2
        End If

        If (validar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_prov.Text, cmb_pais.Text, dtpk_fecha_nac.Value.Year.ToString + "-" + dtpk_fecha_nac.Value.Month.ToString + "-" + dtpk_fecha_nac.Value.Day.ToString, txt_email.Text, (cmb_tarjeta.SelectedIndex + 1).ToString, tipo.ToString, pago.ToString, txt_coment.Text)) Then
            modificar_socio(txt_nsocio.Text, txt_nombre.Text, txt_apellido.Text, txt_dni.Text, txt_direcc.Text, txt_cp.Text, cmb_localidad.Text, cmb_prov.Text, cmb_pais.Text, dtpk_fecha_nac.Value.Year.ToString + "-" + dtpk_fecha_nac.Value.Month.ToString + "-" + dtpk_fecha_nac.Value.Day.ToString, txt_email.Text, (cmb_tarjeta.SelectedIndex + 1).ToString, tipo.ToString, pago.ToString, txt_coment.Text)
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    Private Sub rdo_jubilado_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_jubilado.CheckedChanged

    End Sub
End Class