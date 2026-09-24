Imports System.IO
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Globalization

''' <summary>
''' Genera en PDF el listado de socios de la temporada actual (Archivo -> Exportar -> Listado socios).
'''
''' No necesita ninguna librería externa: el PDF se escribe "a mano" (formato PDF 1.4) usando las
''' fuentes estándar Helvetica y Helvetica-Bold, que todos los lectores de PDF traen de serie, por lo
''' que no hay que incrustarlas. Para medir el ancho de los textos (centrar el título y recortar los
''' que no caben en su columna) se usa Arial, que tiene exactamente las mismas medidas que Helvetica.
''' </summary>
Module listado_pdf

    ' ---- Página A4 vertical, en puntos (1 pt = 1/72 de pulgada) ----
    Private Const ANCHO_PAG As Single = 595.28F
    Private Const ALTO_PAG As Single = 841.89F
    Private Const MARGEN_IZQ As Single = 30.0F
    Private Const MARGEN_SUP As Single = 36.0F
    Private Const MARGEN_INF As Single = 40.0F

    ' ---- Tabla ----
    Private Const ALTO_FILA As Single = 12.0F
    Private Const TAM_LETRA As Single = 8.0F
    Private Const TAM_TITULO As Single = 11.0F
    Private Const RELLENO As Single = 2.0F          ' espacio entre el borde de la celda y el texto

    Private ReadOnly Cabeceras As String() = {"N", "NOMBRE", "APELLIDOS", "DNI", "DIRECCION", "LOCALIDAD"}
    ' Anchos de columna: suman el ancho útil de la página (595.28 - 2 x 30 = 535.28)
    Private ReadOnly Anchos As Single() = {22.0F, 95.0F, 125.0F, 60.0F, 140.0F, 93.28F}

    Private ReadOnly Win1252 As Encoding = Encoding.GetEncoding(1252)

    ' Estado mientras se genera el documento
    Private _g As Graphics
    Private _fNormal As Font
    Private _fNegrita As Font
    Private _paginas As List(Of StringBuilder)
    Private _pag As StringBuilder
    Private _y As Single

    ''' <summary>
    ''' Texto de la temporada para el título: en configuracion.txt se guarda el año en que termina
    ''' (p. ej. "2027") y en el listado se muestra como "2026-27".
    ''' </summary>
    Public Function TextoTemporada() As String
        Dim t As String = If(temporada, "").Trim()
        Dim anio As Integer
        If Integer.TryParse(t, anio) AndAlso anio > 1900 Then
            Return (anio - 1).ToString() & "-" & (anio Mod 100).ToString("00")
        End If
        Return t
    End Function

    ''' <summary>
    ''' Crea el PDF con todos los socios de la temporada actual en la ruta indicada.
    ''' Devuelve el número de socios incluidos.
    ''' </summary>
    Public Function GenerarListadoSocios(ruta As String) As Integer
        Dim socios As List(Of String()) = ObtenerSocios()
        Dim titulo As String = "CDB PESCA REINOSA - LISTADO DE SOCIOS TEMPORADA " & TextoTemporada()

        Using bmp As New Bitmap(1, 1),
              g As Graphics = Graphics.FromImage(bmp),
              fNormal As New Font("Arial", TAM_LETRA, FontStyle.Regular, GraphicsUnit.Point),
              fNegrita As New Font("Arial", TAM_LETRA, FontStyle.Bold, GraphicsUnit.Point),
              fTitulo As New Font("Arial", TAM_TITULO, FontStyle.Bold, GraphicsUnit.Point)

            g.PageUnit = GraphicsUnit.Point                      ' medidas en puntos, como el PDF
            g.TextRenderingHint = TextRenderingHint.AntiAlias    ' sin ajuste a píxel: ancho "de diseño"
            _g = g : _fNormal = fNormal : _fNegrita = fNegrita
            _paginas = New List(Of StringBuilder)()

            ' Primera página: título centrado + línea en blanco + cabecera de la tabla
            NuevaPagina()
            Dim anchoTitulo As Single = Ancho(titulo, fTitulo)
            _y -= TAM_TITULO
            EscribirTexto(_pag, "F2", TAM_TITULO, (ANCHO_PAG - anchoTitulo) / 2, _y, titulo)
            _y -= ALTO_FILA * 2                                  ' línea en blanco de separación
            FilaCabecera()

            ' Filas de socios (N = contador 1, 2, 3...; no es el número de socio)
            For i As Integer = 0 To socios.Count - 1
                If _y - ALTO_FILA < MARGEN_INF Then
                    NuevaPagina()
                    FilaCabecera()                              ' se repite la cabecera en cada página
                End If
                Dim s As String() = socios(i)
                Fila({(i + 1).ToString(), s(0), s(1), s(2), s(3), s(4)}, False)
            Next

            ' Pie de página: fecha y "Página x de y"
            Dim total As Integer = _paginas.Count
            Dim fecha As String = "Generado el " & DateTime.Now.ToString("dd/MM/yyyy")
            For p As Integer = 0 To total - 1
                Dim pie As String = "Página " & (p + 1).ToString() & " de " & total.ToString()
                EscribirTexto(_paginas(p), "F1", 7, MARGEN_IZQ, 22, fecha)
                EscribirTexto(_paginas(p), "F1", 7, ANCHO_PAG - MARGEN_IZQ - Ancho(pie, fNormal) * 7 / TAM_LETRA, 22, pie)
            Next

            EscribirPdf(ruta, titulo, _paginas)
            _g = Nothing : _fNormal = Nothing : _fNegrita = Nothing : _pag = Nothing : _paginas = Nothing
        End Using

        Return socios.Count
    End Function

    ' =====================================================================================
    '  Datos
    ' =====================================================================================

    ''' <summary>
    ''' Socios de la temporada actual (tabla "socios" ya cargada en ds_club), ordenados por número
    ''' de socio. Se descartan las filas vacías del Excel y los socios que están en la papelera.
    ''' Cada elemento: {nombre, apellidos, dni, dirección, localidad}.
    ''' </summary>
    Private Function ObtenerSocios() As List(Of String())
        If ds_club Is Nothing OrElse Not ds_club.Tables.Contains("socios") Then
            Throw New InvalidOperationException("No están cargados los socios de la temporada actual.")
        End If

        Dim papelera = LeerPapelera(PAPELERA_SOCIOS)
        Dim lista As New List(Of Tuple(Of Double, String()))

        For Each r As DataRow In ds_club.Tables("socios").Rows
            If r.RowState = DataRowState.Deleted OrElse r.RowState = DataRowState.Detached Then Continue For

            Dim nombre As String = Campo(r, "nombre")
            Dim apellidos As String = Campo(r, "apellidos")
            Dim dni As String = Campo(r, "dni")
            If nombre = "" AndAlso apellidos = "" AndAlso dni = "" Then Continue For          ' fila vacía
            If dni <> "" AndAlso papelera.Contains(NormalizeId(dni)) Then Continue For        ' eliminado

            Dim num As Double
            If Not Double.TryParse(Campo(r, "numero"), num) Then num = Double.MaxValue       ' sin número: al final

            lista.Add(Tuple.Create(num, {nombre, apellidos, dni, Campo(r, "direccion"), Campo(r, "localidad")}))
        Next

        Return lista.OrderBy(Function(t) t.Item1) _
                    .ThenBy(Function(t) t.Item2(1), StringComparer.CurrentCultureIgnoreCase) _
                    .Select(Function(t) t.Item2).ToList()
    End Function

    Private Function Campo(r As DataRow, columna As String) As String
        If Not r.Table.Columns.Contains(columna) Then Return ""
        Dim v As Object = r(columna)
        If v Is Nothing OrElse IsDBNull(v) Then Return ""
        Return Convert.ToString(v).Trim()
    End Function

    ' =====================================================================================
    '  Maquetación
    ' =====================================================================================

    Private Sub NuevaPagina()
        _pag = New StringBuilder()
        _paginas.Add(_pag)
        _pag.Append("0.5 w" & vbLf)                           ' grosor de las líneas de la tabla
        _y = ALTO_PAG - MARGEN_SUP
    End Sub

    Private Sub FilaCabecera()
        Fila(Cabeceras, True)
    End Sub

    ''' <summary>Dibuja una fila de la tabla en la posición actual y baja una fila.</summary>
    Private Sub Fila(valores As String(), esCabecera As Boolean)
        Dim abajo As Single = _y - ALTO_FILA
        Dim x As Single = MARGEN_IZQ
        Dim fuente As Font = If(esCabecera, _fNegrita, _fNormal)
        Dim nombreFuente As String = If(esCabecera, "F2", "F1")

        If esCabecera Then                                    ' fondo gris claro en la cabecera
            Dim anchoTabla As Single = Anchos.Sum()
            _pag.Append("0.85 g " & N(MARGEN_IZQ) & " " & N(abajo) & " " & N(anchoTabla) & " " & N(ALTO_FILA) & " re f 0 g" & vbLf)
        End If

        For c As Integer = 0 To Anchos.Length - 1
            Dim txt As String = Recortar(valores(c), fuente, Anchos(c) - 2 * RELLENO)
            Dim tx As Single = x + RELLENO
            If c = 0 AndAlso Not esCabecera Then tx = x + Anchos(c) - RELLENO - Ancho(txt, fuente)   ' N a la derecha
            If txt <> "" Then EscribirTexto(_pag, nombreFuente, TAM_LETRA, tx, abajo + 3.2F, txt)
            _pag.Append(N(x) & " " & N(abajo) & " " & N(Anchos(c)) & " " & N(ALTO_FILA) & " re S" & vbLf)   ' borde
            x += Anchos(c)
        Next
        _y = abajo
    End Sub

    ''' <summary>Recorta el texto por la derecha hasta que quepa en el ancho indicado.</summary>
    Private Function Recortar(texto As String, f As Font, anchoMax As Single) As String
        Dim t As String = If(texto, "")
        Do While t.Length > 0 AndAlso Ancho(t, f) > anchoMax
            t = t.Substring(0, t.Length - 1)
        Loop
        Return t.TrimEnd()
    End Function

    Private Function Ancho(texto As String, f As Font) As Single
        If String.IsNullOrEmpty(texto) Then Return 0
        Return _g.MeasureString(texto, f, PointF.Empty, StringFormat.GenericTypographic).Width
    End Function

    ' =====================================================================================
    '  Escritura del PDF
    ' =====================================================================================

    Private Sub EscribirTexto(sb As StringBuilder, fuente As String, tam As Single, x As Single, y As Single, texto As String)
        sb.Append("BT /" & fuente & " " & N(tam) & " Tf " & N(x) & " " & N(y) & " Td " & CadenaPdf(texto) & " Tj ET" & vbLf)
    End Sub

    ''' <summary>Número con punto decimal (el PDF no admite la coma decimal española).</summary>
    Private Function N(v As Single) As String
        Return v.ToString("0.##", CultureInfo.InvariantCulture)
    End Function

    ''' <summary>
    ''' Cadena literal PDF en codificación WinAnsi (Windows-1252: tildes, ñ, º, ª...). Se escapan
    ''' los paréntesis y la barra invertida, y los caracteres no ASCII se escriben en octal (\ddd).
    ''' </summary>
    Private Function CadenaPdf(s As String) As String
        Dim sb As New StringBuilder("(")
        For Each b As Byte In Win1252.GetBytes(If(s, ""))
            Select Case b
                Case 40, 41, 92                                   ' (  )  \
                    sb.Append("\"c).Append(ChrW(b))
                Case Is < 32, Is > 126
                    sb.Append("\"c).Append(Convert.ToString(b, 8).PadLeft(3, "0"c))
                Case Else
                    sb.Append(ChrW(b))
            End Select
        Next
        Return sb.Append(")"c).ToString()
    End Function

    ''' <summary>
    ''' Monta el fichero PDF. Objetos: 1 catálogo, 2 árbol de páginas, 3 Helvetica, 4 Helvetica-Bold,
    ''' 5 información del documento y, por cada página, el objeto página y su contenido.
    ''' </summary>
    Private Sub EscribirPdf(ruta As String, titulo As String, paginas As List(Of StringBuilder))
        Dim objetos As New List(Of String)
        Dim kids As New StringBuilder()
        For p As Integer = 0 To paginas.Count - 1
            kids.Append((6 + 2 * p).ToString() & " 0 R ")
        Next

        objetos.Add("<< /Type /Catalog /Pages 2 0 R >>")
        objetos.Add("<< /Type /Pages /Kids [" & kids.ToString().Trim() & "] /Count " & paginas.Count.ToString() & " >>")
        objetos.Add("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>")
        objetos.Add("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>")
        objetos.Add("<< /Title " & CadenaPdf(titulo) & " /Author (CDB Pesca Reinosa) /CreationDate (D:" & DateTime.Now.ToString("yyyyMMddHHmmss") & ") >>")

        For p As Integer = 0 To paginas.Count - 1
            Dim contenido As String = paginas(p).ToString()
            objetos.Add("<< /Type /Page /Parent 2 0 R /MediaBox [0 0 " & N(ANCHO_PAG) & " " & N(ALTO_PAG) & "]" &
                        " /Resources << /Font << /F1 3 0 R /F2 4 0 R >> >> /Contents " & (7 + 2 * p).ToString() & " 0 R >>")
            ' El contenido es ASCII puro (lo no ASCII va en octal), así que Length = número de caracteres
            objetos.Add("<< /Length " & contenido.Length.ToString() & " >>" & vbLf & "stream" & vbLf & contenido & "endstream")
        Next

        Using fs As New FileStream(ruta, FileMode.Create, FileAccess.Write)
            Dim desplazamientos As New List(Of Long)
            Dim escribir = Sub(texto As String)
                               Dim bytes As Byte() = Encoding.ASCII.GetBytes(texto)
                               fs.Write(bytes, 0, bytes.Length)
                           End Sub

            escribir("%PDF-1.4" & vbLf)
            fs.Write(New Byte() {37, 226, 227, 207, 211, 10}, 0, 6)          ' comentario binario recomendado: %âãÏÓ

            For i As Integer = 0 To objetos.Count - 1
                desplazamientos.Add(fs.Position)
                escribir((i + 1).ToString() & " 0 obj" & vbLf & objetos(i) & vbLf & "endobj" & vbLf)
            Next

            Dim inicioXref As Long = fs.Position
            Dim xref As New StringBuilder()
            xref.Append("xref" & vbLf & "0 " & (objetos.Count + 1).ToString() & vbLf)
            xref.Append("0000000000 65535 f " & vbLf)
            For Each d As Long In desplazamientos
                xref.Append(d.ToString("0000000000") & " 00000 n " & vbLf)
            Next
            xref.Append("trailer" & vbLf & "<< /Size " & (objetos.Count + 1).ToString() & " /Root 1 0 R /Info 5 0 R >>" & vbLf)
            xref.Append("startxref" & vbLf & inicioXref.ToString() & vbLf & "%%EOF" & vbLf)
            escribir(xref.ToString())
        End Using
    End Sub

End Module
