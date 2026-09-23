Imports System.Data.Odbc
Imports System.IO
Imports System.Text.RegularExpressions

''' <summary>
''' Una diferencia entre un socio de la temporada actual y la base de datos histórica de socios.
''' </summary>
Public Class DiferenciaSocio
    ''' <summary>NUEVO, MODIFICADO o CONFLICTO.</summary>
    Public Estado As String
    ''' <summary>Fila del socio en la temporada actual (datos nuevos).</summary>
    Public Temporada As DataRow
    ''' <summary>Fila del socio en la base de datos (Nothing si es nuevo).</summary>
    Public BaseDatos As DataRow
    ''' <summary>Campos que cambian, con el valor antiguo y el nuevo: "Dirección: 'A' → 'B'".</summary>
    Public Cambios As New List(Of String)
    ''' <summary>Nombres de columna que cambian (solo esos se escriben en la base de datos).</summary>
    Public CamposCambiados As New List(Of String)
    ''' <summary>Explicación del conflicto o comentario (p. ej. "DNI corregido").</summary>
    Public Motivo As String = ""
    ''' <summary>False si no se puede actualizar ni forzándolo (p. ej. DNI repetido en la base de datos).</summary>
    Public Forzable As Boolean = True
End Class

''' <summary>
''' Actualización de la base de datos histórica de socios (bdsocios) con los datos de la temporada actual.
''' - NUEVO: el socio de la temporada no está en la base de datos -> se inserta.
''' - MODIFICADO: está, pero algún dato es distinto -> se actualiza.
''' - CONFLICTO: hay algo raro (número de otra persona, DNI repetido...) -> solo se actualiza si
'''   el usuario lo selecciona a propósito (y nunca si el DNI está repetido en la base de datos).
''' El emparejamiento se hace por DNI (sin guiones ni mayúsculas) y, si no aparece, por número + apellidos.
''' Solo se usan UPDATE e INSERT, que el controlador ODBC de Excel sí admite.
''' </summary>
Module actualizar_bd

    ''' <summary>Campos que se comparan y se copian a la base de datos (los que existan en ambas hojas).</summary>
    Public ReadOnly CAMPOS As String() = {"numero", "nombre", "apellidos", "dni", "direccion", "cp", "localidad", "provincia", "pais", "fechanac", "email", "tarjeta", "tipo_socio"}

    Private ReadOnly ETIQUETAS As New Dictionary(Of String, String) From {
        {"numero", "Nº socio"}, {"nombre", "Nombre"}, {"apellidos", "Apellidos"}, {"dni", "DNI"},
        {"direccion", "Dirección"}, {"cp", "C.P."}, {"localidad", "Localidad"}, {"provincia", "Provincia"},
        {"pais", "País"}, {"fechanac", "F. nacimiento"}, {"email", "Email"}, {"tarjeta", "Tarjeta"}, {"tipo_socio", "Tipo socio"}}

    ''' <summary>Compara la temporada actual con la base de datos (tablas ya cargadas en memoria).</summary>
    Public Function CompararSocios() As List(Of DiferenciaSocio)
        Dim resultado As New List(Of DiferenciaSocio)
        Dim temp As DataTable = If(ds_club.Tables.Contains("socios"), ds_club.Tables("socios"), Nothing)
        Dim bd As DataTable = If(ds_club.Tables.Contains("bdsocios"), ds_club.Tables("bdsocios"), Nothing)
        If temp Is Nothing OrElse bd Is Nothing Then Return resultado

        ' Índices de la base de datos por DNI y por número
        Dim porDni As New Dictionary(Of String, List(Of DataRow))
        Dim porNumero As New Dictionary(Of String, List(Of DataRow))
        For Each r As DataRow In bd.Rows
            Anotar(porDni, NormalizeId(Texto(r, "dni")), r)
            Anotar(porNumero, Clave(r, "numero"), r)
        Next

        Dim campos = CamposComunes(temp, bd)

        For Each s As DataRow In temp.Rows
            ' Filas vacías de la hoja (Excel a veces devuelve filas en blanco): se ignoran
            If campos.All(Function(c) Comparable(s(c), c) = "") Then Continue For
            Dim d As New DiferenciaSocio With {.Temporada = s}
            Dim dni = NormalizeId(Texto(s, "dni"))
            Dim num = Clave(s, "numero")

            If dni = "" Then
                d.Estado = "CONFLICTO" : d.Motivo = "El socio no tiene DNI en la temporada actual." : d.Forzable = False
                resultado.Add(d) : Continue For
            End If

            ' 1) Emparejar por DNI
            Dim candidatos As List(Of DataRow) = Nothing
            If porDni.TryGetValue(dni, candidatos) Then
                If candidatos.Count > 1 Then
                    d.Estado = "CONFLICTO" : d.Forzable = False
                    d.Motivo = "Este DNI está repetido " & candidatos.Count & " veces en la base de datos. Corríjalo en el Excel."
                    resultado.Add(d) : Continue For
                End If
                d.BaseDatos = candidatos(0)
            Else
                ' 2) Si no, por número + mismos apellidos (DNI corregido en la temporada)
                Dim porNum As List(Of DataRow) = Nothing
                If num <> "" AndAlso porNumero.TryGetValue(num, porNum) Then
                    Dim mismos = porNum.Where(Function(r) Comparable(Texto(r, "apellidos")) = Comparable(Texto(s, "apellidos"))).ToList()
                    If mismos.Count = 1 Then
                        d.BaseDatos = mismos(0)
                        d.Motivo = "Emparejado por número y apellidos (el DNI es distinto)."
                    End If
                End If
            End If

            If d.BaseDatos Is Nothing Then
                ' NUEVO... salvo que su número ya lo tenga otra persona en la base de datos
                d.Estado = "NUEVO"
                Dim ocupado As List(Of DataRow) = Nothing
                If num <> "" AndAlso porNumero.TryGetValue(num, ocupado) Then
                    d.Estado = "CONFLICTO"
                    d.Motivo = "Socio nuevo, pero el nº " & num & " ya lo tiene en la base de datos " & Persona(ocupado(0)) & "."
                End If
                resultado.Add(d) : Continue For
            End If

            ' Ya existe: ¿qué campos cambian? (un dato vacío en la temporada NO borra el de la base de datos)
            For Each c In campos
                Dim nuevo = Comparable(s(c), c)
                If nuevo = "" Then Continue For
                If nuevo <> Comparable(d.BaseDatos(c), c) Then
                    d.CamposCambiados.Add(c)
                    d.Cambios.Add(ETIQUETAS(c) & ": '" & Texto(d.BaseDatos, c) & "' → '" & Texto(s, c) & "'")
                End If
            Next
            If d.Cambios.Count = 0 Then Continue For ' idéntico: no se muestra

            d.Estado = "MODIFICADO"
            ' Si cambia el número, comprobar que no sea de OTRA persona en la base de datos
            If num <> "" AndAlso num <> Clave(d.BaseDatos, "numero") Then
                Dim otros As List(Of DataRow) = Nothing
                If porNumero.TryGetValue(num, otros) Then
                    Dim ajenos = otros.Where(Function(r) r IsNot d.BaseDatos).ToList()
                    If ajenos.Count > 0 Then
                        d.Estado = "CONFLICTO"
                        d.Motivo = "El nº " & num & " ya lo tiene en la base de datos " & Persona(ajenos(0)) & "."
                    End If
                End If
            End If
            resultado.Add(d)
        Next
        Return resultado
    End Function

    ''' <summary>
    ''' Aplica a la base de datos las diferencias indicadas (INSERT para nuevos, UPDATE para el resto).
    ''' Devuelve un resumen de lo hecho. Las no forzables se saltan.
    ''' </summary>
    Public Function AplicarCambios(lista As List(Of DiferenciaSocio)) As String
        Dim insertados = 0, actualizados = 0, saltados = 0
        Dim errores As New List(Of String)
        Dim bd As DataTable = ds_club.Tables("bdsocios")
        Dim campos = CamposComunes(ds_club.Tables("socios"), bd)

        conectar()
        Try
            For Each d In lista
                If Not d.Forzable Then saltados += 1 : Continue For
                Try
                    If d.BaseDatos Is Nothing Then
                        ' INSERT con todos los campos comunes
                        Dim sql = "INSERT INTO " & tabla_bdsocios_xls & " (" & String.Join(", ", campos) & ") VALUES (" & String.Join(", ", campos.Select(Function(c) "?")) & ")"
                        Using cmd As New OdbcCommand(sql, conn2)
                            For i = 0 To campos.Count - 1
                                cmd.Parameters.AddWithValue("p" & i, Valor(d.Temporada, campos(i)))
                            Next
                            If cmd.ExecuteNonQuery() > 0 Then insertados += 1
                        End Using
                    Else
                        ' UPDATE solo de los campos que han CAMBIADO. Antes se reescribían todos y, si alguna
                        ' celda de esa fila en la base de datos es una FÓRMULA (o no se puede escribir), el
                        ' controlador daba "No se puede actualizar '(expresión)'; el campo no es actualizable".
                        Dim aCopiar = If(d.CamposCambiados.Count > 0, d.CamposCambiados,
                                         campos.Where(Function(c) Comparable(d.Temporada(c), c) <> "").ToList())
                        Try
                            If ActualizarCampos(d, aCopiar) > 0 Then actualizados += 1 Else errores.Add(Persona(d.Temporada) & ": no se encontró la fila en la base de datos")
                        Catch exTodo As Exception
                            ' Reintentar campo a campo: se actualiza lo que se pueda y se informa de lo que no
                            Dim fallidos As New List(Of String)
                            Dim alguno = False
                            For Each c In aCopiar
                                Try
                                    If ActualizarCampos(d, New List(Of String) From {c}) > 0 Then alguno = True
                                Catch
                                    fallidos.Add(ETIQUETAS(c))
                                End Try
                            Next
                            If alguno Then actualizados += 1
                            errores.Add(Persona(d.Temporada) & ": no se pudo escribir " & String.Join(", ", fallidos) &
                                        " (¿esas celdas de la hoja de la base de datos tienen una fórmula o están protegidas?)")
                        End Try
                    End If
                Catch ex As Exception
                    errores.Add(Persona(d.Temporada) & ": " & ex.Message)
                End Try
            Next
        Finally
            desconectar()
        End Try

        Dim msg = "Insertados: " & insertados & vbCrLf & "Actualizados: " & actualizados
        If saltados > 0 Then msg &= vbCrLf & "No actualizados (DNI vacío o repetido): " & saltados
        If errores.Count > 0 Then msg &= vbCrLf & vbCrLf & "Errores:" & vbCrLf & String.Join(vbCrLf, errores.Take(15))
        Return msg
    End Function

    ''' <summary>
    ''' UPDATE de los campos indicados del socio en la base de datos. Identifica la fila por el DNI
    ''' que tiene en la base de datos (o por número + apellidos si allí no tiene DNI).
    ''' </summary>
    ''' <returns>Filas actualizadas.</returns>
    Private Function ActualizarCampos(d As DiferenciaSocio, columnas As List(Of String)) As Integer
        If columnas.Count = 0 Then Return 0
        Dim sql = "UPDATE " & tabla_bdsocios_xls & " SET " & String.Join(", ", columnas.Select(Function(c) c & " = ?"))
        Dim dniBd = Texto(d.BaseDatos, "dni")
        sql &= If(dniBd <> "", " WHERE dni = ?", " WHERE numero = ? AND apellidos = ?")
        Using cmd As New OdbcCommand(sql, conn2)
            Dim i = 0
            For Each c In columnas
                cmd.Parameters.AddWithValue("p" & i, Valor(d.Temporada, c)) : i += 1
            Next
            If dniBd <> "" Then
                cmd.Parameters.AddWithValue("w1", dniBd)
            Else
                cmd.Parameters.AddWithValue("w1", Texto(d.BaseDatos, "numero"))
                cmd.Parameters.AddWithValue("w2", Texto(d.BaseDatos, "apellidos"))
            End If
            Return cmd.ExecuteNonQuery()
        End Using
    End Function

    ''' <summary>
    ''' Copia de seguridad del libro antes de actualizar. Devuelve la ruta de la copia o "" si no se pudo.
    ''' </summary>
    Public Function CopiaSeguridadLibro() As String
        Try
            Dim libro = RutaLibroDesdeDSN(DSN)
            If libro = "" Then libro = ruta_bd_excel
            If libro = "" OrElse Not File.Exists(libro) Then Return ""
            desconectar()
            System.Data.Odbc.OdbcConnection.ReleaseObjectPool()
            Dim bk = Path.Combine(Path.GetDirectoryName(libro), Path.GetFileNameWithoutExtension(libro) & "_backup_" & DateTime.Now.ToString("yyyyMMddHHmmss") & Path.GetExtension(libro))
            File.Copy(libro, bk)
            Return bk
        Catch
            Return ""
        End Try
    End Function

    ' ---------------------------------------------------------------------------------
    '  Utilidades
    ' ---------------------------------------------------------------------------------

    ''' <summary>Campos de CAMPOS que existen en las dos tablas.</summary>
    Private Function CamposComunes(a As DataTable, b As DataTable) As List(Of String)
        Return CAMPOS.Where(Function(c) a.Columns.Contains(c) AndAlso b.Columns.Contains(c)).ToList()
    End Function

    Private Sub Anotar(dic As Dictionary(Of String, List(Of DataRow)), clave As String, r As DataRow)
        If clave = "" Then Return
        If Not dic.ContainsKey(clave) Then dic(clave) = New List(Of DataRow)
        dic(clave).Add(r)
    End Sub

    ''' <summary>Valor de una columna como texto ("" si no existe o está vacío).</summary>
    Public Function Texto(r As DataRow, col As String) As String
        If r Is Nothing OrElse Not r.Table.Columns.Contains(col) OrElse IsDBNull(r(col)) Then Return ""
        Dim v = r(col)
        If TypeOf v Is Date Then Return CDate(v).ToString("dd/MM/yyyy")
        Return Convert.ToString(v).Trim()
    End Function

    Private Function Valor(r As DataRow, col As String) As Object
        If r Is Nothing OrElse Not r.Table.Columns.Contains(col) Then Return DBNull.Value
        Return r(col)
    End Function

    ''' <summary>Número de socio como clave ("12" aunque Excel lo devuelva como 12.0 o como texto).</summary>
    Private Function Clave(r As DataRow, col As String) As String
        Return Comparable(Valor(r, col), col)
    End Function

    Private Function Persona(r As DataRow) As String
        Return (Texto(r, "nombre") & " " & Texto(r, "apellidos")).Trim() & " (DNI " & Texto(r, "dni") & ")"
    End Function

    ''' <summary>
    ''' Forma "normalizada" de un valor para comparar sin falsos cambios: los números como números
    ''' (39200 = "39200" = 39200.0), las fechas como fechas, los DNI sin guiones y los textos sin
    ''' espacios sobrantes ni diferencias de mayúsculas.
    ''' </summary>
    Public Function Comparable(v As Object, Optional col As String = "") As String
        If v Is Nothing OrElse IsDBNull(v) Then Return ""
        If col = "dni" Then Return NormalizeId(Convert.ToString(v))
        If TypeOf v Is Date Then Return CDate(v).ToString("yyyyMMdd")
        Dim s = Regex.Replace(Convert.ToString(v).Trim(), "\s+", " ")
        If s = "" Then Return ""
        If col = "fechanac" Then
            Dim f As Date
            Dim formatos = {"dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "dd-MM-yyyy"}
            If Date.TryParseExact(s.Split(" "c)(0), formatos, Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, f) Then Return f.ToString("yyyyMMdd")
            If Date.TryParse(s, f) Then Return f.ToString("yyyyMMdd")
        End If
        Dim d As Double
        If Double.TryParse(s.Replace(",", "."), Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, d) Then
            Return d.ToString(Globalization.CultureInfo.InvariantCulture)
        End If
        Return s.ToUpperInvariant()
    End Function

End Module
