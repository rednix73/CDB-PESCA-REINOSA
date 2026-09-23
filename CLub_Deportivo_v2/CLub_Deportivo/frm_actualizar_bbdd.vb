''' <summary>
''' Actualiza la base de datos histórica de socios (bdsocios) con los datos de la temporada actual.
''' Muestra los socios NUEVOS (no están en la base de datos), MODIFICADOS (algún dato distinto) y
''' CONFLICTOS (hay que revisarlos). La lógica de comparación y actualización está en actualizar_bd.vb.
''' </summary>
Public Class frm_actualizar_bbdd

    Private diferencias As New List(Of DiferenciaSocio)

    ' Colores por estado
    Private ReadOnly COLOR_NUEVO As Color = Color.FromArgb(220, 245, 220)      ' verde claro
    Private ReadOnly COLOR_MODIFICADO As Color = Color.FromArgb(255, 245, 200) ' amarillo claro
    Private ReadOnly COLOR_CONFLICTO As Color = Color.FromArgb(255, 215, 215)  ' rojo claro

    Private Sub frm_actualizar_bbdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PrepararTabla()
        Recalcular()
    End Sub

    ''' <summary>Columnas de la tabla: casilla de selección + datos de solo lectura.</summary>
    Private Sub PrepararTabla()
        With DataGridView1
            .Columns.Clear()
            .AutoGenerateColumns = False
            .ReadOnly = False
            .MultiSelect = True
            .ShowCellToolTips = True
            .Columns.Add(New DataGridViewCheckBoxColumn With {.Name = "sel", .HeaderText = "", .Width = 30})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "estado", .HeaderText = "Estado", .Width = 95, .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "numero", .HeaderText = "Nº", .Width = 50, .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "socio", .HeaderText = "Nombre y apellidos", .Width = 230, .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "dni", .HeaderText = "DNI", .Width = 105, .ReadOnly = True})
            .Columns.Add(New DataGridViewTextBoxColumn With {.Name = "cambios", .HeaderText = "Qué cambia / motivo", .ReadOnly = True,
                         .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill})
        End With
    End Sub

    ''' <summary>Compara las tablas (ya cargadas en memoria) y rellena la tabla.</summary>
    Private Sub Recalcular()
        Try
            Me.Cursor = Cursors.WaitCursor
            diferencias = CompararSocios()
            DataGridView1.Rows.Clear()
            ' Orden: conflictos primero (hay que revisarlos), luego nuevos y modificados
            For Each d In diferencias.OrderBy(Function(x) If(x.Estado = "CONFLICTO", 0, If(x.Estado = "NUEVO", 1, 2))).ThenBy(Function(x) Texto(x.Temporada, "apellidos"))
                Dim detalle As String
                Select Case d.Estado
                    Case "NUEVO" : detalle = "No está en la base de datos de socios."
                    Case "MODIFICADO" : detalle = String.Join("; ", d.Cambios)
                    Case Else : detalle = d.Motivo & If(d.Cambios.Count > 0, "  Cambios: " & String.Join("; ", d.Cambios), "")
                End Select
                If d.Estado = "MODIFICADO" AndAlso d.Motivo <> "" Then detalle = d.Motivo & "  " & detalle

                Dim i = DataGridView1.Rows.Add(False, d.Estado, Texto(d.Temporada, "numero"),
                                               (Texto(d.Temporada, "nombre") & " " & Texto(d.Temporada, "apellidos")).Trim(),
                                               Texto(d.Temporada, "dni"), detalle)
                Dim fila = DataGridView1.Rows(i)
                fila.Tag = d
                fila.DefaultCellStyle.BackColor = If(d.Estado = "NUEVO", COLOR_NUEVO, If(d.Estado = "MODIFICADO", COLOR_MODIFICADO, COLOR_CONFLICTO))
                ' Al pasar el ratón por "Qué cambia" se ve cada cambio en una línea
                fila.Cells("cambios").ToolTipText = If(d.Cambios.Count > 0, String.Join(vbCrLf, d.Cambios), detalle)
                If Not d.Forzable Then fila.Cells("sel").ReadOnly = True ' no se puede actualizar: hay que corregir el Excel
            Next

            Dim nNuevos = diferencias.Where(Function(x) x.Estado = "NUEVO").Count()
            Dim nMod = diferencias.Where(Function(x) x.Estado = "MODIFICADO").Count()
            Dim nConf = diferencias.Where(Function(x) x.Estado = "CONFLICTO").Count()
            If diferencias.Count = 0 Then
                lbl_resumen.Text = "La base de datos de socios está al día con la temporada actual."
            Else
                lbl_resumen.Text = nNuevos & " nuevos  ·  " & nMod & " modificados  ·  " & nConf & " conflictos (en rojo: revíselos antes de actualizarlos)"
            End If
            btn_actualizar_todo.Enabled = (nNuevos + nMod) > 0
            btn_actualizar_sel.Enabled = diferencias.Count > 0
        Catch ex As Exception
            MsgBox("Error comparando los socios: " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ' Que la casilla se marque en el momento (sin esperar a salir de la celda)
    Private Sub DataGridView1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        If DataGridView1.IsCurrentCellDirty Then DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub btn_sel_todo_Click(sender As Object, e As EventArgs) Handles btn_sel_todo.Click
        MarcarTodas(True)
    End Sub

    Private Sub btn_sel_ninguno_Click(sender As Object, e As EventArgs) Handles btn_sel_ninguno.Click
        MarcarTodas(False)
    End Sub

    Private Sub MarcarTodas(valor As Boolean)
        DataGridView1.EndEdit()
        For Each fila As DataGridViewRow In DataGridView1.Rows
            If Not fila.Cells("sel").ReadOnly Then fila.Cells("sel").Value = valor
        Next
    End Sub

    ''' <summary>Nuevos y modificados. Los conflictos NO se incluyen.</summary>
    Private Sub btn_actualizar_todo_Click(sender As Object, e As EventArgs) Handles btn_actualizar_todo.Click
        Dim lista = diferencias.Where(Function(x) x.Estado <> "CONFLICTO").ToList()
        If lista.Count = 0 Then Return
        Dim nNuevos = lista.Where(Function(x) x.Estado = "NUEVO").Count()
        Dim msg = "Se insertarán " & nNuevos & " socios nuevos y se actualizarán " & (lista.Count - nNuevos) & " en la base de datos de socios."
        Dim nConf = diferencias.Count - lista.Count
        If nConf > 0 Then msg &= vbCrLf & "(Los " & nConf & " conflictos no se tocarán.)"
        If MsgBox(msg & vbCrLf & vbCrLf & "¿Desea continuar?", vbYesNo + vbQuestion, "Actualizar todo") <> vbYes Then Return
        Aplicar(lista)
    End Sub

    ''' <summary>Filas marcadas con la casilla o, si no hay ninguna, las filas seleccionadas.</summary>
    Private Sub btn_actualizar_sel_Click(sender As Object, e As EventArgs) Handles btn_actualizar_sel.Click
        DataGridView1.EndEdit()
        Dim filas = DataGridView1.Rows.Cast(Of DataGridViewRow)().Where(Function(f) CBool(If(f.Cells("sel").Value, False))).ToList()
        If filas.Count = 0 Then filas = DataGridView1.SelectedRows.Cast(Of DataGridViewRow)().ToList()
        Dim lista = filas.Select(Function(f) DirectCast(f.Tag, DiferenciaSocio)).ToList()
        If lista.Count = 0 Then
            MsgBox("Marque la casilla de los socios que quiera actualizar (o selecciónelos en la tabla).")
            Return
        End If

        Dim noForzables = lista.Where(Function(x) Not x.Forzable).Count()
        Dim conflictos = lista.Where(Function(x) x.Estado = "CONFLICTO" AndAlso x.Forzable).Count()
        Dim msg = "Se van a actualizar " & (lista.Count - noForzables) & " socio(s) en la base de datos."
        If conflictos > 0 Then msg &= vbCrLf & vbCrLf & "ATENCIÓN: " & conflictos & " de ellos tienen CONFLICTO (por ejemplo, un número que en la base de datos es de otra persona). Asegúrese de que es correcto."
        If noForzables > 0 Then msg &= vbCrLf & vbCrLf & noForzables & " no se pueden actualizar (DNI vacío o repetido en la base de datos) y se saltarán."
        If MsgBox(msg & vbCrLf & vbCrLf & "¿Desea continuar?", vbYesNo + If(conflictos > 0, vbExclamation, vbQuestion), "Actualizar seleccionados") <> vbYes Then Return
        Aplicar(lista)
    End Sub

    Private Sub Aplicar(lista As List(Of DiferenciaSocio))
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim bk = CopiaSeguridadLibro()
            If bk = "" AndAlso MsgBox("No se ha podido hacer una copia de seguridad del libro Excel. ¿Continuar igualmente?", vbYesNo + vbExclamation) <> vbYes Then Return
            Dim resumen = AplicarCambios(lista)
            bbdd.cargar()     ' recargar las tablas para que la comparación refleje los cambios
            Recalcular()
            Me.Cursor = Cursors.Default
            MsgBox(resumen & If(bk <> "", vbCrLf & vbCrLf & "Copia de seguridad: " & bk, ""), vbInformation, "Actualización terminada")
        Catch ex As Exception
            MsgBox("Error al actualizar: " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btn_cerrar_Click(sender As Object, e As EventArgs) Handles btn_cerrar.Click
        Me.Close()
    End Sub
End Class
