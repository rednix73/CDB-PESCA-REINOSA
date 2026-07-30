Public Class frm_busqueda
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub


    'Try
    '    If DataGridView1.SelectedRows.Count > 0 Then
    '        frm_socio.reset()

    '        frm_socio.txt_nsocio.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
    '        frm_socio.txt_nombre.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
    '        frm_socio.txt_apellido.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
    '        frm_socio.txt_dni.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
    '        frm_socio.txt_direcc.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
    '        frm_socio.txt_cp.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString()
    '        frm_socio.cmb_localidad.Text = ""
    '        frm_socio.cmb_localidad.Text = DataGridView1.SelectedRows(0).Cells(6).Value.ToString()
    '        frm_socio.cmb_prov.SelectedItem = DataGridView1.SelectedRows(0).Cells(7).Value.ToString()
    '        frm_socio.cmb_pais.SelectedItem = DataGridView1.SelectedRows(0).Cells(8).Value.ToString()
    '        frm_socio.txt_email.Text = DataGridView1.SelectedRows(0).Cells(10).Value.ToString()
    '        frm_socio.cmb_tarjeta.SelectedIndex = (CInt(DataGridView1.SelectedRows(0).Cells(11).Value) - 1)


    '        frm_federativas.reset()

    '        frm_federativas.txt_nsocio.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
    '        frm_federativas.txt_nombre.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
    '        frm_federativas.txt_apellido.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
    '        frm_federativas.txt_dni.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
    '        frm_federativas.txt_direcc.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
    '        frm_federativas.txt_cp.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString()
    '        frm_federativas.cmb_localidad.Text = ""
    '        frm_federativas.cmb_localidad.Text = DataGridView1.SelectedRows(0).Cells(6).Value.ToString()
    '        frm_federativas.calcula_importe()

    '        Select Case DataGridView1.SelectedRows(0).Cells(12).Value.ToString()
    '            Case "NORMAL"
    '                frm_socio.rdo_normal.Checked = True
    '            Case "JUBILADO"
    '                frm_socio.rdo_jubilado.Checked = True
    '            Case "MENOR, FEMINA, OTROS"
    '                frm_socio.rdo_otros.Checked = True
    '        End Select
    '        'MsgBox(DataGridView1.SelectedRows(0).Cells(9).Value.ToString())
    '        If Not IsDBNull(DataGridView1.SelectedRows(0).Cells(9).Value) Then
    '            'Dim fechanac As New Date(CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 6).Remove(4, 8)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 3).Remove(2, 13)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(2, 16)))
    '            Dim fechanac As New Date(CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 6)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(0, 3).Remove(2, 5)), CInt(DataGridView1.SelectedRows(0).Cells(9).Value.ToString().Remove(2, 8)))
    '            'fechanac = CDate(DataGridView1.SelectedRows(0).Cells(9).Value.ToString())
    '            'MsgBox(fechanac.ToShortDateString())
    '            frm_socio.dtpk_fecha_nac.Value = fechanac.Date
    '            frm_federativas.dtpk_fecha_nac.Value = fechanac.Date
    '            'MsgBox(Calcula_edad(fechanac).Year.ToString())
    '            If (Calcula_edad(fechanac) >= 65) Then
    '                frm_socio.rdo_jubilado.Checked = True
    '                frm_federativas.rdo_normal.Checked = True
    '            End If
    '        Else
    '            frm_socio.dtpk_fecha_nac.Value = "2000/01/01"
    '            frm_federativas.dtpk_fecha_nac.Value = "2000/01/01"

    '        End If

    '    End If
    '    Me.Close()
    'Catch ex As Exception
    '    MsgBox(ex.ToString)
    'End Try

    Private Sub btn_usar_Click(sender As Object, e As EventArgs) Handles btn_usar.Click
        Try
            If DataGridView1.SelectedRows.Count = 0 Then Return
            Dim row = DataGridView1.SelectedRows(0)

            ' Helpers
            Dim GetCellValue = Function(idx As Integer) As Object
                                   If idx >= 0 AndAlso idx < row.Cells.Count Then Return row.Cells(idx).Value
                                   Return Nothing
                               End Function
            Dim GetCellString = Function(idx As Integer) As String
                                    Dim v = GetCellValue(idx)
                                    If v Is Nothing OrElse IsDBNull(v) Then Return String.Empty
                                    Return v.ToString().Trim()
                                End Function
            Dim ParseDateSafe = Function(obj As Object) As Date
                                    Dim def As Date = New Date(2000, 1, 1)
                                    If obj Is Nothing OrElse IsDBNull(obj) Then Return def
                                    Try
                                        If TypeOf obj Is Date Then Return CDate(obj)
                                        If TypeOf obj Is Double OrElse TypeOf obj Is Decimal Then Return Date.FromOADate(Convert.ToDouble(obj))
                                        Dim s = obj.ToString().Trim()
                                        Dim d As Date
                                        If Date.TryParse(s, d) Then Return d
                                        Dim formatos = New String() {"yyyy/MM/dd", "yyyy-MM-dd", "dd/MM/yyyy", "dd-MM-yyyy", "yyyy"}
                                        For Each f In formatos
                                            If Date.TryParseExact(s, f, Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, d) Then Return d
                                        Next
                                        Dim m = System.Text.RegularExpressions.Regex.Match(s, "\d{4}")
                                        If m.Success Then Return New Date(Integer.Parse(m.Value), 1, 1)
                                    Catch
                                    End Try
                                    Return def
                                End Function

            ' Mapear columnas (ajustar índices si tu DataGridView difiere)
            Dim numero = GetCellString(0)
            Dim nombre = GetCellString(1)
            Dim apellidos = GetCellString(2)
            Dim dni = GetCellString(3)
            Dim direccion = GetCellString(4)
            Dim cp = GetCellString(5)
            Dim localidad = GetCellString(6)
            Dim provincia = GetCellString(7)
            Dim pais = GetCellString(8)
            Dim fechanacObj = If(9 < row.Cells.Count, row.Cells(9).Value, Nothing)
            Dim fechanac = ParseDateSafe(fechanacObj)
            Dim email = GetCellString(10)
            Dim tipo_tarjeta_val = GetCellString(11)
            Dim tipo_socio = If(12 < row.Cells.Count, GetCellString(12), String.Empty)
            Dim comentarios = If(13 < row.Cells.Count, GetCellString(13), String.Empty)
            Dim telefono = If(14 < row.Cells.Count, GetCellString(14), String.Empty)

            ' Rellenar frm_socio
            Try
                frm_socio.reset()
                frm_socio.txt_nsocio.Text = numero
                frm_socio.txt_nombre.Text = nombre
                frm_socio.txt_apellido.Text = apellidos
                frm_socio.txt_dni.Text = dni
                frm_socio.txt_direcc.Text = direccion
                frm_socio.txt_cp.Text = cp
                frm_socio.txt_email.Text = email
                frm_socio.txt_coment.Text = comentarios

                ' Provincia primero (dispara población de localidades)
                If Not String.IsNullOrEmpty(provincia) Then
                    Dim provIdx = frm_socio.cmb_prov.FindStringExact(provincia)
                    If provIdx >= 0 Then
                        frm_socio.cmb_prov.SelectedIndex = provIdx
                    Else
                        frm_socio.cmb_prov.SelectedItem = provincia
                    End If
                End If

                ' Localidad: intentar seleccionar si existe en items, si no, asignar texto
                If Not String.IsNullOrEmpty(localidad) Then
                    If frm_socio.cmb_localidad.Items.Count = 0 Then
                        ' intentar poblar si lista global existe y provincia es CANTABRIA
                        Try
                            If frm_socio.cmb_prov.SelectedItem IsNot Nothing AndAlso frm_socio.cmb_prov.SelectedItem.ToString().ToUpper() = "CANTABRIA" Then
                                frm_socio.cmb_localidad.Items.Clear()
                                For i = 0 To lista_localidades.Count - 1
                                    frm_socio.cmb_localidad.Items.Add(lista_localidades(i).Nombre)
                                Next
                            End If
                        Catch
                        End Try
                    End If
                    Dim locIdx = frm_socio.cmb_localidad.FindStringExact(localidad)
                    If locIdx >= 0 Then frm_socio.cmb_localidad.SelectedIndex = locIdx Else frm_socio.cmb_localidad.Text = localidad
                End If

                ' País
                If Not String.IsNullOrEmpty(pais) Then
                    Dim pIdx = frm_socio.cmb_pais.FindStringExact(pais)
                    If pIdx >= 0 Then frm_socio.cmb_pais.SelectedIndex = pIdx Else frm_socio.cmb_pais.Text = pais
                End If

                ' Fecha
                frm_socio.dtpk_fecha_nac.Value = fechanac

                ' Tipo tarjeta: si viene número (tipo_tarjeta) usar índice = valor-1, si viene texto buscar
                If Not String.IsNullOrEmpty(tipo_tarjeta_val) Then
                    Dim idxNum As Integer
                    If Integer.TryParse(tipo_tarjeta_val, idxNum) Then
                        idxNum = Math.Max(0, idxNum - 1)
                        If idxNum >= 0 AndAlso idxNum < frm_socio.cmb_tarjeta.Items.Count Then frm_socio.cmb_tarjeta.SelectedIndex = idxNum
                    Else
                        Dim idxText = frm_socio.cmb_tarjeta.FindStringExact(tipo_tarjeta_val)
                        If idxText >= 0 Then frm_socio.cmb_tarjeta.SelectedIndex = idxText Else
                        Dim idxPartial = frm_socio.cmb_tarjeta.FindString(tipo_tarjeta_val)
                        If idxPartial >= 0 Then frm_socio.cmb_tarjeta.SelectedIndex = idxPartial
                    End If
                End If


                ' Tipo socio -> radios: >=65 jubilado, <16 otros
                Dim edad = Calcula_edad(fechanac)
                If edad >= 65 Then
                    frm_socio.rdo_jubilado.Checked = True
                ElseIf edad < 16 Then
                    frm_socio.rdo_otros.Checked = True
                Else
                    frm_socio.rdo_normal.Checked = True
                End If
            Catch ex As Exception
                MsgBox("Error asignando datos a frm_socio: " & ex.Message)
            End Try

            ' Rellenar frm_federativas
            Try
                frm_federativas.reset()
                frm_federativas.txt_nsocio.Text = numero
                frm_federativas.txt_nombre.Text = nombre
                frm_federativas.txt_apellido.Text = apellidos
                frm_federativas.txt_dni.Text = dni
                frm_federativas.txt_direcc.Text = direccion
                frm_federativas.txt_cp.Text = cp
                frm_federativas.cmb_localidad.Text = localidad
                frm_federativas.txt_telefono.Text = telefono
                frm_federativas.txt_coment.Text = comentarios
                frm_federativas.dtpk_fecha_nac.Value = fechanac

                ' Edad -> si <16 marcar rdo_gratis (fallback rdo_otros)
                Dim edadF = Calcula_edad(fechanac)
                If edadF < 16 Then
                    Dim found = frm_federativas.Controls.Find("rdo_gratis", True)
                    If found IsNot Nothing AndAlso found.Length > 0 Then
                        CType(found(0), RadioButton).Checked = True
                    ElseIf frm_federativas.Controls.Find("rdo_otros", True).Length > 0 Then
                        CType(frm_federativas.Controls.Find("rdo_otros", True)(0), RadioButton).Checked = True
                    End If
                Else
                    If frm_federativas.Controls.Find("rdo_normal", True).Length > 0 Then
                        CType(frm_federativas.Controls.Find("rdo_normal", True)(0), RadioButton).Checked = True
                    End If
                End If

                frm_federativas.calcula_importe()
                ' cmb_compite / cmb_comite y cmb_modalidad se dejan para rellenar manualmente según indicaste
            Catch ex As Exception
                MsgBox("Error asignando datos a frm_federativas: " & ex.Message)
            End Try

            Me.Close()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_cerrar.Click
        Me.DataGridView1.EndEdit()

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