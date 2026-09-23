Imports System.Data.Odbc
Imports MySql.Data.MySqlClient
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports OdbcConnection = System.Data.Odbc.OdbcConnection
Imports System.IO
Imports System.Text.RegularExpressions
'
' NPOI imports removed from this module to avoid forcing load of netstandard/NPOI
' when only the .xls (Interop) branch is needed. See bbdd_npoi.vb for .xlsx handling.
Imports System.Runtime.InteropServices

Module bbdd

    ' ---------CONFIGURACIÓN------------------------------------------------------------
    ' General
    Public temporada As String '= "2023"
    Public precio_salmon As Decimal '= 16 (Decimal: admite precios con céntimos)
    Public precio_trucha As Decimal '= 10
    Public tarjeta_socio_anverso As String = "tarjeta_socio_anverso.gif"
    Public tarjeta_socio_reverso As String = "tarjeta_socio_reverso.jpg"
    Public ruta_recursos As String = My.Settings.ruta_recursos

    'Base de datos:
    'Tipo de origen de datos
    Public Enum tipobd
        Excel_ODBC
        MySQL
    End Enum
    ''' <summary>
    ''' Tipo de base de datos.
    ''' </summary>
    Public tp As tipobd

    '----Excel-ODBC----
    Public ruta_bd_excel As String '= "C:\Users\roberto\Documents\tarjetas_socio_2023.xls"
    Public DSN As String '= "cdb-pesca-xls"
    'Nombre de las tablas donde están los datos. Formato en excel: [Tabla$]
    Public tabla_socios_xls As String = "[socios_2027$]"
    Public tabla_bdsocios_xls As String = "[bdsocios_2027$]"
    Public tabla_federa_xls As String = "[federativas_2027$]"

    '----Mysql----
    Public server As String '= "153.92.7.1"
    Public port As String '= "3306"
    Public bd_mysql As String '= "u127917223_socio"
    Public user As String '= "u127917223_redn"
    Public password As String '= "EaHb8Hx2nThGhNCw"
    'Nombre de las tablas Mysql donde están los datos.
    Public tabla_socios_mysql As String '= "socios"
    Public tabla_bdsocios_mysql As String '= "bdsocios"
    '------------FIN VARIABLES DE  CONFIGURACIÓN------------------------------------------------------------------



    '------- OBJETOS DE CONEXIÓN A LAS BASES DE DATOS------------------------------------
    ' Base de datos remota mysql
    Public cadena1 As String = "Server=" & server & ";Port=" & port & ";Database=" & bd_mysql & ";Uid=" + user + ";Pwd=" & password & ";"
    Public conn1 As New MySqlConnection
    Public consulta1 As New MySqlCommand
    Public dr1 As MySqlDataReader
    Public dr2 As MySqlDataReader
    'Tabla socios mysql
    Public da_socios1 As MySqlDataAdapter
    Public cb_socios1 As MySqlCommandBuilder
    'Tabla Base de datos de socios mysql
    Public da_bdsocios1 As MySqlDataAdapter
    Public cb_bdsocios1 As MySqlCommandBuilder

    ' Base de datos local excel - conexion odbc
    Public cadena2 As String = "DSN=" + DSN
    Public conn2 As New OdbcConnection
    Public consulta2 As New OdbcCommand
    Public dr3 As OdbcDataReader
    Public dr4 As OdbcDataReader
    'Tabla socios excel
    Public da_socios2 As New OdbcDataAdapter
    Public cb_socios2 As New OdbcCommandBuilder
    'Tabla Base de datos de socios excel
    Public da_bdsocios2 As New OdbcDataAdapter
    Public cb_bdsocios2 As New OdbcCommandBuilder
    'Tabla tarjetas federativas de excel
    Public da_federa2 As New OdbcDataAdapter
    Public cb_federa2 As New OdbcCommandBuilder

    ' Base de datos local excel - conexion oledb - falla cadena de conexion
    Public cadena3 As String = "Provider=Microsoft.ACE.OLEDB.12.0;" & "Data source=" & ruta_bd_excel & ";" & "Extended Properties=Excel 8.0;HDR=Yes"
    'Public cadena3 As String ="Provider=Microsoft.ACE.OLEDB.12.0;" & "data source=" & Archivo_excel & "; " & "Extended Properties='Excel 12.0 Xml;HDR=Yes'"
    Public conn3 As New OleDb.OleDbConnection
    Public consulta3 As New OleDbCommand
    Public dr5 As OleDbDataReader
    Public dr6 As OleDbDataReader
    'Tabla socios excel
    Public da_socios3 As OleDbDataAdapter
    Public cb_socios3 As OleDbCommandBuilder
    'Tabla Base de datos de socios excel
    Public da_bdsocios3 As OleDbDataAdapter
    Public cb_bdsocios3 As OleDbCommandBuilder

    Public dw_socios As DataView
    Public dw_bdsocios As New DataView
    Public dw_federa As New DataView


    'Dataset con todas las tablas
    Public ds_club As New DataSet
    ''' <summary>
    ''' Método en el que se establece la conexión con la base de datos en función del valor del campo: tp en el que se indica el tipo de base de datos a utilizar.
    ''' Admite dos tipos de conexión : Excel ODBC ó MySQL.
    ''' </summary>
    Public Sub conectar()
        Try

            'tp = tipobd.Excel_ODBC

            Select Case tp
                Case tipobd.Excel_ODBC
                    cadena2 = "DSN=" + DSN
                    conn2 = New OdbcConnection(cadena2)
                    conn2.Open()
                    If Not conn2.State = ConnectionState.Open Then
                        MsgBox("Error de conexion")
                    End If
                    'conn3 = New OleDb.OleDbConnection()
                    'conn3.ConnectionString = cadena3
                    'conn3.Open()
                    'If Not conn3.State = ConnectionState.Open Then
                    '    MsgBox("Error de conexion")
                    'End If
                Case tipobd.MySQL
                    conn1 = New MySqlConnection()
                    cadena1 = "Server=" & server & "; Port=" & port & "; Database=" & bd_mysql & "; Uid=" + user + "; Pwd=" & password & ";"
                    conn1.ConnectionString = cadena1
                    conn1.Open()
                    If Not conn1.State = ConnectionState.Open Then
                        MsgBox("Error de conexion")
                    End If
                Case Else
            End Select
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    ' =====================================================================================
    '  ELIMINACIÓN EN DOS FASES ("papelera") PARA FEDERATIVAS Y SOCIOS DE LA TEMPORADA
    '  1) Al eliminar: el driver ODBC de Excel NO admite DELETE, así que el NIF/DNI se apunta
    '     en un fichero de texto (la papelera) y cargar() oculta esas filas (borrado lógico).
    '       - federativas_2027 -> deleted_federativas.txt (columna nif)
    '       - socios_2027      -> deleted_socios.txt      (columna dni)
    '     (bdsocios, la base histórica de socios, NO se toca: un socio que se da de baja en
    '      la temporada sigue en el histórico).
    '  2) Al ARRANCAR la aplicación (antes de que ODBC bloquee el .xls) se abre el libro con
    '     Excel (Interop) y se borran físicamente todas las filas apuntadas (vaciar la papelera).
    '  En los ficheros se guarda SIEMPRE el identificador normalizado (NormalizeId): mayúsculas,
    '  sin espacios ni guiones. Así la escritura, el filtro y la purga comparan lo mismo.
    ' =====================================================================================

    Public Const PAPELERA_FEDERATIVAS As String = "deleted_federativas.txt"
    Public Const PAPELERA_SOCIOS As String = "deleted_socios.txt"

    ''' <summary>Ruta completa de un fichero de papelera (o cadena vacía si no hay ruta de recursos).</summary>
    Private Function RutaPapelera(fichero As String) As String
        Dim base As String = My.Settings.ruta_recursos
        If String.IsNullOrWhiteSpace(base) Then Return String.Empty
        Return Path.Combine(base, fichero)
    End Function

    ''' <summary>Identificadores (normalizados) que hay en una papelera.</summary>
    Public Function LeerPapelera(fichero As String) As System.Collections.Generic.HashSet(Of String)
        Dim conjunto As New System.Collections.Generic.HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Try
            Dim ruta As String = RutaPapelera(fichero)
            If ruta = "" OrElse Not File.Exists(ruta) Then Return conjunto
            For Each ln In File.ReadAllLines(ruta)
                Dim k = NormalizeId(ln)
                If k <> "" Then conjunto.Add(k)
            Next
        Catch
        End Try
        Return conjunto
    End Function

    ''' <summary>Indica si un identificador está en la papelera indicada.</summary>
    Public Function EnPapelera(fichero As String, id As String) As Boolean
        Dim k = NormalizeId(id)
        Return k <> "" AndAlso LeerPapelera(fichero).Contains(k)
    End Function

    ''' <summary>Apunta un identificador en la papelera (si no estaba ya). Devuelve False si no se pudo.</summary>
    Public Function AnotarEnPapelera(fichero As String, id As String) As Boolean
        Dim ruta As String = RutaPapelera(fichero)
        Dim k = NormalizeId(id)
        If ruta = "" OrElse k = "" Then Return False
        If Not LeerPapelera(fichero).Contains(k) Then
            Using sw As New StreamWriter(ruta, True)
                sw.WriteLine(k)
            End Using
        End If
        Return True
    End Function

    ''' <summary>Quita de la papelera los identificadores indicados (ya purgados o recuperados).</summary>
    Public Sub QuitarDePapelera(fichero As String, ids As System.Collections.Generic.IEnumerable(Of String))
        Dim ruta As String = RutaPapelera(fichero)
        If ruta = "" OrElse Not File.Exists(ruta) Then Return
        Dim quitar As New System.Collections.Generic.HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each i In ids
            quitar.Add(NormalizeId(i))
        Next
        Dim quedan As New System.Collections.Generic.List(Of String)()
        For Each ln In File.ReadAllLines(ruta)
            Dim k = NormalizeId(ln)
            If k <> "" AndAlso Not quitar.Contains(k) AndAlso Not quedan.Contains(k) Then quedan.Add(k)
        Next
        File.WriteAllLines(ruta, quedan.ToArray())
    End Sub

    ' --- Atajos usados por federativas y socios (mantienen los nombres que ya usaba el código) ---
    Public Function EstaMarcadaEliminada(nif As String) As Boolean
        Return EnPapelera(PAPELERA_FEDERATIVAS, nif)
    End Function
    Public Sub QuitarDeBorrados(purgadas As System.Collections.Generic.IEnumerable(Of String))
        QuitarDePapelera(PAPELERA_FEDERATIVAS, purgadas)
    End Sub
    Private Function GetDeletedFederativasSet() As System.Collections.Generic.HashSet(Of String)
        Return LeerPapelera(PAPELERA_FEDERATIVAS)
    End Function
    Public Function SocioEliminado(dni As String) As Boolean
        Return EnPapelera(PAPELERA_SOCIOS, dni)
    End Function

    ''' <summary>
    ''' Quita de una tabla en memoria las filas cuyo identificador está en la papelera.
    ''' Se usa en cargar() para que los registros eliminados no aparezcan.
    ''' </summary>
    Private Sub OcultarEliminados(tabla As System.Data.DataTable, columna As String, fichero As String)
        Try
            Dim papelera = LeerPapelera(fichero)
            If papelera.Count = 0 OrElse tabla Is Nothing OrElse Not tabla.Columns.Contains(columna) Then Return
            For i As Integer = tabla.Rows.Count - 1 To 0 Step -1
                Dim v = tabla.Rows(i)(columna)
                If Not IsDBNull(v) AndAlso papelera.Contains(NormalizeId(v.ToString())) Then tabla.Rows.RemoveAt(i)
            Next
            tabla.AcceptChanges()
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Devuelve la ruta del libro Excel configurado en un DSN de ODBC (valor DBQ del registro),
    ''' o cadena vacía si no se encuentra. Busca primero en DSN de usuario y luego en DSN de sistema,
    ''' en la vista de 32 bits del registro (la aplicación y el driver ODBC son de 32 bits).
    ''' </summary>
    Public Function RutaLibroDesdeDSN(nombreDsn As String) As String
        If String.IsNullOrWhiteSpace(nombreDsn) Then Return ""
        For Each hive In New Microsoft.Win32.RegistryHive() {Microsoft.Win32.RegistryHive.CurrentUser, Microsoft.Win32.RegistryHive.LocalMachine}
            Try
                Using raiz = Microsoft.Win32.RegistryKey.OpenBaseKey(hive, Microsoft.Win32.RegistryView.Registry32)
                    Using k = raiz.OpenSubKey("SOFTWARE\ODBC\ODBC.INI\" & nombreDsn.Trim())
                        If k IsNot Nothing Then
                            Dim dbq = Convert.ToString(k.GetValue("DBQ"))
                            If Not String.IsNullOrWhiteSpace(dbq) Then Return dbq
                        End If
                    End Using
                End Using
            Catch
            End Try
        Next
        Return ""
    End Function

    ''' <summary>Se mantiene por compatibilidad: ahora purga todas las papeleras (federativas y socios).</summary>
    Public Function PurgeDeletedFederativas(Optional preguntar As Boolean = True) As Integer
        Return PurgarPapeleras(preguntar)
    End Function

    ''' <summary>
    ''' Vacía las papeleras: borra físicamente del libro Excel (el del DSN) las filas apuntadas en
    ''' deleted_federativas.txt y deleted_socios.txt, en UNA sola apertura de Excel.
    ''' Debe llamarse al arrancar, antes de cargar(): después ODBC mantiene el .xls bloqueado.
    ''' Crea una copia de seguridad del libro antes de modificarlo.
    ''' </summary>
    ''' <param name="preguntar">Si es True pide confirmación y muestra el resultado.</param>
    ''' <returns>Filas eliminadas, 0 si no había nada que purgar o -1 si hubo error / se canceló.</returns>
    Public Function PurgarPapeleras(Optional preguntar As Boolean = True) As Integer
        Try
            ' Trabajos: (hoja, columnas posibles del identificador, fichero de papelera, identificadores)
            Dim trabajos As New System.Collections.Generic.List(Of Tuple(Of String, String(), String, System.Collections.Generic.HashSet(Of String)))
            Dim fed = LeerPapelera(PAPELERA_FEDERATIVAS)
            If fed.Count > 0 Then trabajos.Add(Tuple.Create(tabla_federa_xls, New String() {"nif", "dni"}, PAPELERA_FEDERATIVAS, fed))
            Dim soc = LeerPapelera(PAPELERA_SOCIOS)
            If soc.Count > 0 Then trabajos.Add(Tuple.Create(tabla_socios_xls, New String() {"dni", "nif"}, PAPELERA_SOCIOS, soc))
            If trabajos.Count = 0 Then Return 0 ' nada pendiente: no molestamos al usuario

            ' El libro a purgar es EL MISMO al que apunta el DSN de ODBC (donde se guardan los datos).
            Dim libro As String = RutaLibroDesdeDSN(DSN)
            If libro = "" Then libro = ruta_bd_excel
            If String.IsNullOrWhiteSpace(libro) OrElse Not File.Exists(libro) Then
                MsgBox("Fichero Excel de datos no encontrado: " & libro)
                Return -1
            End If
            If Path.GetExtension(libro).ToLowerInvariant() <> ".xls" Then
                MsgBox("La purga automática solo está disponible para libros .xls. No se ha eliminado nada.")
                Return -1
            End If

            Dim bk = Path.Combine(Path.GetDirectoryName(libro), Path.GetFileNameWithoutExtension(libro) & "_backup_" & DateTime.Now.ToString("yyyyMMddHHmmss") & Path.GetExtension(libro))

            If preguntar Then
                Dim msg = "Hay " & (fed.Count + soc.Count).ToString() & " registro(s) marcados para eliminar." & vbCrLf &
                          "¿Desea eliminarlos definitivamente del libro Excel?" & vbCrLf & "(Se creará una copia de seguridad: " & bk & ")"
                If MessageBox.Show(msg, "Confirmar purga", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return -1
            End If

            ' Soltar el fichero por si ODBC lo tuviera abierto (al arrancar no debería)
            desconectar()
            System.Data.Odbc.OdbcConnection.ReleaseObjectPool()
            GC.Collect()
            GC.WaitForPendingFinalizers()

            File.Copy(libro, bk)

            Dim eliminadas As Integer = PurgarHojasXls(libro, trabajos)
            ' Recolección FUERA del método que usó COM: así EXCEL.EXE termina de verdad
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
            GC.WaitForPendingFinalizers()

            If eliminadas >= 0 Then
                For Each t In trabajos
                    QuitarDePapelera(t.Item3, t.Item4)
                Next
                If preguntar Then MsgBox("Purga completada. Filas eliminadas del Excel: " & eliminadas.ToString())
            End If
            Return eliminadas

        Catch ex As Exception
            MsgBox("Error al vaciar la papelera: " & ex.Message)
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Abre el libro con Excel (Interop) y, en cada hoja indicada, borra las filas cuyo
    ''' identificador (normalizado) está en su conjunto. Libera todos los objetos COM.
    ''' Si falla cualquier hoja NO se guarda nada (todo o nada).
    ''' </summary>
    ''' <returns>Total de filas eliminadas o -1 si hubo error.</returns>
    Private Function PurgarHojasXls(ruta As String, trabajos As System.Collections.Generic.List(Of Tuple(Of String, String(), String, System.Collections.Generic.HashSet(Of String)))) As Integer
        Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
        Dim libros As Microsoft.Office.Interop.Excel.Workbooks = Nothing
        Dim xlWb As Microsoft.Office.Interop.Excel.Workbook = Nothing
        Dim hojas As Microsoft.Office.Interop.Excel.Sheets = Nothing
        Dim total As Integer = 0
        Try
            xlApp = New Microsoft.Office.Interop.Excel.Application()
            xlApp.Visible = False
            xlApp.DisplayAlerts = False
            xlApp.ScreenUpdating = False

            libros = xlApp.Workbooks                       ' evitar "doble punto", que deja objetos COM sin liberar
            xlWb = libros.Open(ruta, 0, False)             ' UpdateLinks:=0, ReadOnly:=False
            If xlWb.ReadOnly Then
                MsgBox("El libro Excel está abierto por otro programa (¿Excel abierto? ¿OneDrive sincronizando?)." & vbCrLf &
                       "Ciérrelo y vuelva a abrir la aplicación. No se ha eliminado nada.")
                xlWb.Close(False)
                Return -1
            End If
            hojas = xlWb.Worksheets

            For Each t In trabajos
                Dim n = BorrarFilasHoja(hojas, t.Item1, t.Item2, t.Item4)
                If n < 0 Then
                    xlWb.Close(False)                      ' no guardar nada si una hoja falla
                    Return -1
                End If
                total += n
            Next

            If total > 0 Then xlWb.Save()
            xlWb.Close(False)
            Return total

        Catch ex As Exception
            MsgBox("Error al purgar el libro con Excel: " & ex.Message)
            Try
                If xlWb IsNot Nothing Then xlWb.Close(False)
            Catch
            End Try
            Return -1
        Finally
            If hojas IsNot Nothing Then Marshal.ReleaseComObject(hojas)
            If xlWb IsNot Nothing Then Marshal.ReleaseComObject(xlWb)
            If libros IsNot Nothing Then Marshal.ReleaseComObject(libros)
            If xlApp IsNot Nothing Then
                Try
                    xlApp.Quit()
                Catch
                End Try
                Marshal.ReleaseComObject(xlApp)
            End If
        End Try
    End Function

    ''' <summary>Borra las filas de una hoja cuyo identificador está en <paramref name="ids"/>. Devuelve cuántas o -1.</summary>
    Private Function BorrarFilasHoja(hojas As Microsoft.Office.Interop.Excel.Sheets, tabla As String, columnas As String(), ids As System.Collections.Generic.HashSet(Of String)) As Integer
        Dim xlWs As Microsoft.Office.Interop.Excel.Worksheet = Nothing
        Dim usado As Microsoft.Office.Interop.Excel.Range = Nothing
        Dim eliminadas As Integer = 0
        Dim hoja As String = tabla.Replace("[", "").Replace("]", "").TrimEnd("$"c)
        Try
            xlWs = CType(hojas.Item(hoja), Microsoft.Office.Interop.Excel.Worksheet) ' si no existe, error (nunca borrar en otra hoja)
            usado = xlWs.UsedRange
            Dim valores As Object = usado.Value2           ' toda la hoja de una vez: mucho más rápido que celda a celda
            If Not TypeOf valores Is Object(,) Then Return 0
            Dim datos = DirectCast(valores, Object(,))
            Dim filaInicio As Integer = usado.Row          ' UsedRange no tiene por qué empezar en la fila 1
            Dim nFilas = datos.GetUpperBound(0), nCols = datos.GetUpperBound(1)

            Dim col As Integer = -1
            For Each nombre In columnas
                For c As Integer = 1 To nCols
                    If Convert.ToString(datos(1, c)).Trim().Equals(nombre, StringComparison.OrdinalIgnoreCase) Then col = c : Exit For
                Next
                If col <> -1 Then Exit For
            Next
            If col = -1 Then
                MsgBox("No se encuentra la columna '" & columnas(0) & "' en la hoja " & hoja & ". No se ha eliminado nada.")
                Return -1
            End If

            ' De abajo hacia arriba: al borrar una fila, las de debajo suben y no afectan a las que faltan por revisar
            For f As Integer = nFilas To 2 Step -1
                Dim id = NormalizeId(Convert.ToString(datos(f, col)))
                If id <> "" AndAlso ids.Contains(id) Then
                    Dim fila = CType(xlWs.Rows(filaInicio + f - 1), Microsoft.Office.Interop.Excel.Range)
                    fila.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp)
                    Marshal.ReleaseComObject(fila)
                    eliminadas += 1
                End If
            Next
            Return eliminadas
        Catch ex As Exception
            MsgBox("Error al purgar la hoja " & hoja & ": " & ex.Message)
            Return -1
        Finally
            If usado IsNot Nothing Then Marshal.ReleaseComObject(usado)
            If xlWs IsNot Nothing Then Marshal.ReleaseComObject(xlWs)
        End Try
    End Function

    ''' <summary>Normaliza un identificador: mayúsculas y solo letras/dígitos ("12345678-z " -> "12345678Z").</summary>
    Public Function NormalizeId(ByVal s As String) As String
        If String.IsNullOrWhiteSpace(s) Then Return String.Empty
        Return Regex.Replace(s.Trim().ToUpperInvariant(), "[^A-Z0-9]", "")
    End Function

    ''' <summary>
    ''' Convierte un texto a número para pasarlo como parámetro ODBC (evita ambigüedades con
    ''' la coma/punto decimal). Si está vacío devuelve DBNull; si no es número, el propio texto.
    ''' </summary>
    Public Function ParamNumero(s As String) As Object
        If String.IsNullOrWhiteSpace(s) Then Return DBNull.Value
        Dim t = s.Trim().Replace(",", ".")
        Dim d As Double
        If Double.TryParse(t, Globalization.NumberStyles.Float, Globalization.CultureInfo.InvariantCulture, d) Then Return d
        Return s.Trim()
    End Function

    ''' <summary>Añade parámetros ODBC (?) en orden.</summary>
    Private Sub AddParams(cmd As OdbcCommand, ParamArray valores() As Object)
        For i As Integer = 0 To valores.Length - 1
            cmd.Parameters.AddWithValue("p" & (i + 1).ToString(), If(valores(i), DBNull.Value))
        Next
    End Sub

    ''' <summary>
    ''' Método que desconecta de la base de datos.
    ''' </summary>
    Public Sub desconectar()

        Try
            conn1.Close()
            If conn1.State = ConnectionState.Open Then
                MsgBox("Error de desconexión")
            End If
            conn2.Close()
            If conn2.State = ConnectionState.Open Then
                MsgBox("Error de desconexión")
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' En este método se establece la conexión con la base de datos en función del valor del campo: tp en el que se indica el tipo de base de datos a utilizar (Excel ODBC ó MySQL).
    ''' A continuación se descargan las tablas: socios y bdsocios de la base de datos y se desconecta del origen de datos.
    ''' </summary>
    Public Sub cargar()
        conectar()
        Select Case tp
            Case tipobd.Excel_ODBC

                'Conexión por ODBC
                ds_club = New DataSet()
                da_socios2 = New OdbcDataAdapter()
                da_socios2 = New OdbcDataAdapter("SELECT * FROM " + tabla_socios_xls, conn2)
                da_socios2.Fill(ds_club, "socios")
                cb_socios2 = New OdbcCommandBuilder(da_socios2)
                dw_socios = New DataView(ds_club.Tables(0))
                da_bdsocios2 = New OdbcDataAdapter("SELECT * FROM " + tabla_bdsocios_xls, conn2)
                da_bdsocios2.Fill(ds_club, "bdsocios")
                cb_bdsocios2 = New OdbcCommandBuilder(da_bdsocios2)
                dw_bdsocios = New DataView(ds_club.Tables(1))
                da_federa2 = New OdbcDataAdapter("SELECT * FROM " + tabla_federa_xls, conn2)
                da_federa2.Fill(ds_club, "federativas")
                cb_federa2 = New OdbcCommandBuilder(da_federa2)
                ' Ocultar los registros que están en la papelera (borrado lógico pendiente de purga)
                OcultarEliminados(ds_club.Tables("federativas"), "nif", PAPELERA_FEDERATIVAS)
                OcultarEliminados(ds_club.Tables("socios"), "dni", PAPELERA_SOCIOS)
                dw_socios = New DataView(ds_club.Tables(0))
                dw_federa = New DataView(ds_club.Tables(2))

                ''Conexión directa por OLEDB
                'conectar()
                'da_socios3 = New OleDbDataAdapter()
                'da_socios3 = New OleDbDataAdapter("SELECT * FROM " & Hoja_excel_socios, conn3)
                'da_socios3.Fill(ds_club, "socios")
                'cb_socios3 = New OleDbCommandBuilder(da_socios3)
                'dw_socios = New DataView(ds_club.Tables(0))
                'da_bdsocios3 = New OleDbDataAdapter("SELECT * FROM [bdsocios$]", conn3)
                'da_bdsocios3.Fill(ds_club, "bdsocios")
                'cb_bdsocios3 = New OleDbCommandBuilder(da_bdsocios3)
                'dw_bdsocios = New DataView(ds_club.Tables(1))


            Case tipobd.MySQL

                'Conexión a bbdd MySQL
                ds_club = New DataSet()
                da_socios1 = New MySqlDataAdapter("Select * from " + tabla_socios_mysql, conn1)
                da_socios1.Fill(ds_club, "socios")
                cb_socios1 = New MySqlCommandBuilder(da_socios1)
                dw_socios = New DataView(ds_club.Tables(0))
                da_bdsocios1 = New MySqlDataAdapter("Select * from " + tabla_bdsocios_mysql, conn1)
                da_bdsocios1.Fill(ds_club, "bdsocios")
                cb_bdsocios1 = New MySqlCommandBuilder(da_bdsocios1)
                dw_bdsocios = New DataView(ds_club.Tables(1))
            Case Else
        End Select


        desconectar()
    End Sub
    ''' <summary>
    ''' Lee la configuración establecida en el fichero configuración.txt y la carga en las variables globales correspondientes para que la aplicación
    ''' pueda mostrarla en el formulario de configuración (frm_configuracion)
    ''' </summary>
    Public Sub leer_configuracion()
        Try
            ' Mismo fichero que escribe frm_configuracion (carpeta de recursos de AppData)
            Dim base As String = My.Settings.ruta_recursos
            If String.IsNullOrWhiteSpace(base) Then base = ruta_recursos
            Dim ruta As String = Path.Combine(base, "configuracion.txt")
            If Not File.Exists(ruta) Then
                MsgBox("No se encuentra el fichero de configuración: " & ruta & vbCrLf & "Revise la configuración (menú Configuración).")
                Return
            End If

            ' El fichero alterna líneas de título y de valor. Se lee entero y se cierra enseguida
            ' (antes el StreamReader quedaba abierto si fallaba algo y Configuración no podía guardar).
            Dim l As String() = File.ReadAllLines(ruta)
            Dim V = Function(i As Integer) As String
                        Return If(i < l.Length, l(i).Trim(), "")
                    End Function

            temporada = V(2)
            precio_salmon = LeerPrecio(V(4))
            precio_trucha = LeerPrecio(V(6))
            If V(9) = tipobd.MySQL.ToString() Then tp = tipobd.MySQL Else tp = tipobd.Excel_ODBC
            ruta_bd_excel = V(13)
            DSN = V(15)
            If V(17) <> "" Then tabla_socios_xls = V(17)
            If V(19) <> "" Then tabla_bdsocios_xls = V(19)
            If V(21) <> "" Then tabla_federa_xls = V(21)
            ' MySQL: cada valor va DEBAJO de su título. Antes se leía una línea antes de tiempo
            ' (el servidor tomaba el texto "Servidor:") y al guardar se duplicaban los títulos.
            server = V(25)
            port = V(27)
            bd_mysql = V(29)
            user = V(31)
            password = V(33)
            tabla_socios_mysql = V(35)
            tabla_bdsocios_mysql = V(37)
        Catch ex As Exception
            MsgBox("Error leyendo la configuración: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Convierte el precio escrito en la configuración a número. Admite "17", "17,5", "17.5" o "17 €".
    ''' Devuelve 0 si no es un número válido.
    ''' </summary>
    Public Function LeerPrecio(texto As String) As Decimal
        Dim t = If(texto, "").Replace("€", "").Trim().Replace(",", ".")
        Dim d As Decimal
        If Decimal.TryParse(t, Globalization.NumberStyles.Number, Globalization.CultureInfo.InvariantCulture, d) AndAlso d >= 0 Then Return d
        Return 0
    End Function
    ''' <summary>
    ''' Método que busca en la base de datos los números de socios no utilizados y devuelve una lista con los mismos.
    ''' </summary>
    ''' <returns>
    ''' Devuelve una lista de enteros, con los números de socio no usados.
    ''' </returns>
    Public Function numeros_libres() As List(Of Integer)
        ' Se calcula con las tablas ya cargadas en memoria (ds_club), sin volver a consultar el Excel.
        ' Antes: se abría un lector ODBC por CADA número (muy lento en un PC de 2 GB), se dejaban
        ' conexiones abiertas (ultimo() llamaba a conectar() otra vez), MAX(numero) devolvía DBNull
        ' y fallaba, y los números de la temporada no se descartaban porque se comparaba un
        ' Integer con un Double (Equals daba siempre False).
        Dim libres As New List(Of Integer)
        Try
            Dim usados = NumerosUsados(TablaSocios(True))
            usados.UnionWith(NumerosUsados(TablaSocios(False)))
            If usados.Count = 0 Then Return libres
            Dim maximo = usados.Max()
            For n As Integer = 1 To maximo
                If Not usados.Contains(n) Then libres.Add(n)
            Next
        Catch ex As Exception
            MsgBox("Error calculando los números libres: " & ex.Message)
        End Try
        Return libres
    End Function

    ''' <summary>
    ''' Último número de socio usado en la tabla indicada (base de datos de socios o temporada actual).
    ''' </summary>
    ''' <returns>El número más alto, o 0 si no hay ninguno.</returns>
    Public Function ultimo(tabla As String) As Integer
        Try
            Dim esBd = String.Equals(tabla, tabla_bdsocios_xls, StringComparison.OrdinalIgnoreCase) OrElse
                       String.Equals(tabla, tabla_bdsocios_mysql, StringComparison.OrdinalIgnoreCase)
            Dim usados = NumerosUsados(TablaSocios(esBd))
            Return If(usados.Count = 0, 0, usados.Max())
        Catch ex As Exception
            MsgBox("Error obteniendo el último número: " & ex.Message)
            Return 0
        End Try
    End Function

    ''' <summary>Tabla en memoria de la base de datos de socios (True) o de la temporada actual (False).</summary>
    Private Function TablaSocios(baseDatos As Boolean) As System.Data.DataTable
        Dim nombre = If(baseDatos, "bdsocios", "socios")
        If ds_club Is Nothing OrElse Not ds_club.Tables.Contains(nombre) Then Return Nothing
        Return ds_club.Tables(nombre)
    End Function

    ''' <summary>
    ''' Números de socio (enteros positivos) que aparecen en la tabla. Admite que Excel los
    ''' devuelva como número o como texto e ignora las celdas vacías.
    ''' </summary>
    Private Function NumerosUsados(tabla As System.Data.DataTable) As System.Collections.Generic.HashSet(Of Integer)
        Dim usados As New System.Collections.Generic.HashSet(Of Integer)
        If tabla Is Nothing Then Return usados
        Dim col As String = If(tabla.Columns.Contains("numero"), "numero", If(tabla.Columns.Contains("n_socio"), "n_socio", ""))
        If col = "" Then Return usados
        For Each r As DataRow In tabla.Rows
            Dim v = r(col)
            If IsDBNull(v) OrElse v Is Nothing Then Continue For
            Dim d As Double
            If Double.TryParse(Convert.ToString(v).Trim(), Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, d) AndAlso d >= 1 AndAlso d = Math.Floor(d) AndAlso d < 1000000 Then
                usados.Add(CInt(d))
            End If
        Next
        Return usados
    End Function
    ''' <summary>
    ''' Método que filtra el dataview que contiene la tabla con la base de datos de socios por los apellidos del socio y muestra la tabla flitrada en un control datagridview que asu vez se muestra en el formulario: frm_busqueda.
    ''' </summary>
    ''' <param name="valor">texto con el apellido a buscar</param>
    Public Sub buscar_nombre(valor As String)
        ' Primero en la temporada actual; si no está, en la base de datos histórica de socios
        Dim filtro = "apellidos LIKE '%" & EscaparFiltro(valor) & "%'"
        Try
            If String.IsNullOrWhiteSpace(valor) Then
                ' Sin apellido: mostrar todos los socios de la base de datos (como hacía antes)
                MostrarBusqueda(dw_bdsocios, "")
                Return
            End If
            If MostrarBusqueda(dw_socios, filtro) Then Return
            MsgBox("No encontrado en la temporada actual. Buscando en la base de datos de socios...")
            If MostrarBusqueda(dw_bdsocios, filtro) Then Return
            MsgBox("Socio no encontrado.")
        Catch ex As Exception
            MsgBox("Error en la búsqueda: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Busca por número de socio: primero en la temporada actual y, si no está, en la base de datos
    ''' de socios. Si el número está vacío (o no se encuentra) muestra todos los socios de la base de datos.
    ''' </summary>
    ''' <param name="valor">numero de socio</param>
    Public Sub buscar_nsocio(valor As String)
        Try
            Dim n As Integer
            If String.IsNullOrWhiteSpace(valor) Then
                ' Sin número: mostrar TODOS los socios de la base de datos para elegir uno
                If Not MostrarBusqueda(dw_bdsocios, "") Then MsgBox("La base de datos de socios está vacía.")
                Return
            End If
            If Not Integer.TryParse(valor.Trim(), n) Then
                MsgBox("El número de socio debe ser un número.")
                Return
            End If
            Dim filtro = "numero = " & n.ToString()
            If MostrarBusqueda(dw_socios, filtro) Then Return
            MsgBox("Número de socio no encontrado en la temporada actual. Buscando en la base de datos de socios...")
            If MostrarBusqueda(dw_bdsocios, filtro) Then Return
            MsgBox("Número de socio no encontrado. A continuación se muestran todos los socios de la base de datos.")
            MostrarBusqueda(dw_bdsocios, "")
        Catch ex As Exception
            MsgBox("Error en la búsqueda: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Busca por DNI (o parte de él): primero en la temporada actual y, si no está, en la base de datos de socios.
    ''' </summary>
    ''' <param name="valor">texto que contiene el dni del socio</param>
    Public Sub buscar_dni(valor As String)
        Dim filtro = "dni LIKE '%" & EscaparFiltro(valor) & "%'"
        Try
            If MostrarBusqueda(dw_socios, filtro) Then Return
            MsgBox("Socio no encontrado en la temporada actual. Buscando en la base de datos de socios...")
            If MostrarBusqueda(dw_bdsocios, filtro) Then Return
            MsgBox("Socio no encontrado.")
        Catch ex As Exception
            MsgBox("Error en la búsqueda: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Aplica el filtro a la vista y, si hay resultados, la muestra en frm_busqueda.
    ''' Se cuenta con DataView.Count (antes se usaba DataGridView.Rows.Count, que incluye la
    ''' fila vacía de "nuevo registro" y se comprobaba DESPUÉS de mostrar la ventana).
    ''' </summary>
    Private Function MostrarBusqueda(vista As DataView, filtro As String) As Boolean
        If vista Is Nothing Then Return False
        vista.RowFilter = filtro
        If vista.Count = 0 Then Return False
        frm_busqueda.DataGridView1.DataSource = vista
        frm_busqueda.ShowDialog()
        Return True
    End Function

    ''' <summary>Evita que una comilla o un comodín en el texto rompa el filtro (p. ej. apellido "D'Ors").</summary>
    Private Function EscaparFiltro(v As String) As String
        If String.IsNullOrEmpty(v) Then Return ""
        Dim s = v.Trim().Replace("'", "''")
        For Each c In New String() {"[", "]", "*", "%"}
            s = s.Replace(c, "")
        Next
        Return s
    End Function

    ''' <summary>
    ''' Inserta un socio en la tabla socios de la base de datos.
    ''' </summary>
    ''' <param name="nsocio"></param>
    ''' <param name="nombre"></param>
    ''' <param name="apellidos"></param>
    ''' <param name="dni"></param>
    ''' <param name="direcc"></param>
    ''' <param name="cp"></param>
    ''' <param name="localidad"></param>
    ''' <param name="provincia"></param>
    ''' <param name="pais"></param>
    ''' <param name="fechanac"></param>
    ''' <param name="email"></param>
    ''' <param name="tarjeta"></param>
    ''' <param name="tipo_socio"></param>
    ''' <param name="import"></param>
    ''' <param name="comentarios"></param>
    Public Function insertar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, import As String, comentarios As String) As Boolean
        Try
            conectar()
            Select Case tp
                Case tipobd.Excel_ODBC
                    dni = dni.Trim()
                    nsocio = nsocio.Trim()

                    ' 1) El número no puede estar asignado a OTRO socio activo de la temporada
                    '    (las filas en la papelera no cuentan: se borrarán en la próxima purga).
                    For Each f In Consultar("SELECT dni, nombre, apellidos FROM " & tabla_socios_xls & " WHERE numero = ?", nsocio)
                        Dim otroDni = Convert.ToString(f(0))
                        If NormalizeId(otroDni) <> NormalizeId(dni) AndAlso Not SocioEliminado(otroDni) Then
                            MsgBox("El número " & nsocio & " ya está asignado en la temporada actual a " & Convert.ToString(f(1)) & " " & Convert.ToString(f(2)) & " (DNI " & otroDni & ")." & vbCrLf & "Asigne otro número a este socio.")
                            Return False
                        End If
                    Next

                    ' 2) Si este DNI se eliminó en esta sesión, su fila sigue en el Excel (se purga al
                    '    arrancar). No se puede borrar ahora, así que se REUTILIZA con los datos nuevos.
                    If SocioEliminado(dni) Then
                        Using cmd As New OdbcCommand("UPDATE " & tabla_socios_xls & " SET numero = ?, nombre = ?, apellidos = ?, direccion = ?, cp = ?, localidad = ?, provincia = ?, pais = ?, fechanac = ?, email = ?, tarjeta = ?, tipo_socio = ?, importe = ?, comentarios = ? WHERE dni = ?", conn2)
                            AddParams(cmd, ParamNumero(nsocio), nombre, apellidos, direcc, ParamNumero(cp), localidad, provincia, pais, fechanac, email, ParamNumero(tarjeta), tipo_socio, ParamNumero(import), comentarios, dni)
                            If cmd.ExecuteNonQuery() > 0 Then
                                QuitarDePapelera(PAPELERA_SOCIOS, New String() {dni})
                                MsgBox("Este socio se había eliminado en esta sesión. Se ha recuperado con los datos nuevos.")
                                Return True
                            End If
                        End Using
                        MsgBox("Este DNI está pendiente de eliminar. Cierre y vuelva a abrir la aplicación antes de darlo de alta de nuevo.")
                        Return False
                    End If

                    ' 3) El DNI no puede estar ya en la temporada
                    If Consultar("SELECT numero FROM " & tabla_socios_xls & " WHERE dni = ?", dni).Count > 0 Then
                        MsgBox("Ya existe un socio en la temporada actual con el DNI " & dni & ".")
                        Return False
                    End If

                    ' 4) Base de datos histórica: ¿ese número pertenece a otra persona?
                    For Each f In Consultar("SELECT dni, nombre, apellidos FROM " & tabla_bdsocios_xls & " WHERE numero = ?", nsocio)
                        Dim otroDni = Convert.ToString(f(0))
                        If NormalizeId(otroDni) <> NormalizeId(dni) Then
                            Dim r = MsgBox("En la base de datos de socios el número " & nsocio & " pertenece a " & Convert.ToString(f(1)) & " " & Convert.ToString(f(2)) & " (DNI " & otroDni & ")." & vbCrLf & "¿Desea dar de alta igualmente a este socio con ese número?", vbYesNo + vbExclamation, "Número ya usado")
                            If r <> vbYes Then Return False
                            Exit For
                        End If
                    Next

                    ' 5) Alta en la temporada (con parámetros: un apóstrofo en un apellido ya no rompe la consulta)
                    Using cmd As New OdbcCommand("INSERT INTO " & tabla_socios_xls & " (numero, nombre, apellidos, dni, direccion, cp, localidad, provincia, pais, fechanac, email, tarjeta, tipo_socio, importe, comentarios) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", conn2)
                        AddParams(cmd, ParamNumero(nsocio), nombre, apellidos, dni, direcc, ParamNumero(cp), localidad, provincia, pais, fechanac, email, ParamNumero(tarjeta), tipo_socio, ParamNumero(import), comentarios)
                        If cmd.ExecuteNonQuery() = 0 Then
                            MsgBox("No se ha podido insertar el socio.")
                            Return False
                        End If
                    End Using

                    ' 6) Si es un socio nuevo (no está en la base histórica), añadirlo también allí
                    Dim msg = "Socio insertado correctamente en la temporada actual."
                    If Consultar("SELECT numero FROM " & tabla_bdsocios_xls & " WHERE dni = ?", dni).Count = 0 Then
                        Using cmd As New OdbcCommand("INSERT INTO " & tabla_bdsocios_xls & " (numero, nombre, apellidos, dni, direccion, cp, localidad, provincia, pais, fechanac, email, tarjeta, tipo_socio) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", conn2)
                            AddParams(cmd, ParamNumero(nsocio), nombre, apellidos, dni, direcc, ParamNumero(cp), localidad, provincia, pais, fechanac, email, ParamNumero(tarjeta), tipo_socio)
                            If cmd.ExecuteNonQuery() > 0 Then msg &= vbCrLf & "¡SOCIO NUEVO! Se ha añadido también a la base de datos de socios."
                        End Using
                    End If
                    MsgBox(msg)
                    Return True

                Case tipobd.MySQL
                    ' NOTA: rama MySQL sin cambios (no se usa actualmente).
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    If (Not conn1.State = ConnectionState.Open) Then
                        conectar()
                    End If
                    Dim sql_txt As String = "SELECT * FROM socios WHERE n_socio=" + nsocio
                    consulta1.CommandText = sql_txt
                    dr1 = consulta1.ExecuteReader
                    If (dr1.HasRows) Then
                        MsgBox("Número de socio no válido. Ya existe otro socio con ese número asignado. Asigne otro número a este socio")
                        dr1.Close()
                        Return False
                    End If
                    dr1.Close()
                    consulta1.CommandText = "SELECT * FROM socios WHERE dni='" + dni + "'"
                    dr2 = consulta1.ExecuteReader
                    If (dr2.HasRows) Then
                        MsgBox("Socio no válido. Ya existe otro socio en la base de datos con el mismo DNI.")
                        dr2.Close()
                        Return False
                    End If
                    dr2.Close()
                    consulta1.CommandText = "INSERT INTO socios(n_socio,nombre,apellidos,dni,direccion,cp,localidad,provincia,pais,fechanac,email,tarjeta, tipo_socio,comentarios) VALUES(" + nsocio + ",'" + nombre + "','" + apellidos + "','" + dni + "','" + direcc + "'," + cp + ", '" + localidad + "','" + provincia + "','" + pais + "','" + fechanac + "','" + email + "'," + tarjeta + ",'" + tipo_socio + "','" + comentarios + "')"
                    If consulta1.ExecuteNonQuery > 0 Then
                        MsgBox("Socio insertado correctamente")
                        Return True
                    End If
            End Select
            Return False
        Catch ex As Exception
            MsgBox("Error al insertar el socio: " & ex.Message)
            Return False
        Finally
            desconectar()
        End Try
    End Function

    ''' <summary>
    ''' Ejecuta una consulta SELECT con parámetros (?) sobre la conexión ODBC abierta y devuelve
    ''' las filas como arrays de valores. Cierra siempre el lector (antes quedaban abiertos si fallaba algo).
    ''' </summary>
    Private Function Consultar(sql As String, ParamArray valores() As Object) As System.Collections.Generic.List(Of Object())
        Dim filas As New System.Collections.Generic.List(Of Object())
        Using cmd As New OdbcCommand(sql, conn2)
            AddParams(cmd, valores)
            Using dr = cmd.ExecuteReader()
                While dr.Read()
                    Dim v(dr.FieldCount - 1) As Object
                    dr.GetValues(v)
                    filas.Add(v)
                End While
            End Using
        End Using
        Return filas
    End Function




    ''' <summary>
    ''' Inserta una nueva tarjeta federativa en la tabla de tarjetas federativas de la base de datos. Se comprueba que no exista otra tarjeta federativa con el mismo DNI en la temporada actual.
    ''' </summary>
    ''' <param name="nsocio"></param>
    ''' <param name="nombre"></param>
    ''' <param name="dni"></param>
    ''' <param name="cp"></param>
    ''' <param name="localidad"></param>
    ''' <param name="fechanac"></param>
    ''' <param name="comentarios"></param>
    Public Sub inserta_federativa(nsocio As String, nombre As String, apellido1 As String, apellido2 As String, dni As String, fechanac As String, domicilio As String, localidad As String, cp As String, modalidad As String, precio As String, telefono As String, comentarios As String)
        Try
            ' Normalizar y separar apellidos si sólo viene uno
            apellido1 = If(apellido1, String.Empty).Trim()
            apellido2 = If(apellido2, String.Empty).Trim()

            If String.IsNullOrWhiteSpace(apellido2) AndAlso Not String.IsNullOrWhiteSpace(apellido1) Then
                ' Separar en dos partes: primer token = apellido1, resto = apellido2
                Dim partes As String() = apellido1.Split(New Char() {" "c}, 2, StringSplitOptions.RemoveEmptyEntries)
                If partes.Length = 1 Then
                    apellido1 = partes(0)
                    apellido2 = String.Empty
                Else
                    apellido1 = partes(0)
                    apellido2 = partes(1)
                End If
            End If

            ' Escapar comillas simples para evitar romper la query
            Dim E As Func(Of String, String) = Function(s As String) If(String.IsNullOrEmpty(s), "", s.Replace("'", "''"))

            ' Si este NIF se eliminó en esta sesión, su fila sigue en el Excel (se purga al arrancar).
            ' No se puede purgar ahora porque ODBC tiene el fichero bloqueado, así que se REUTILIZA
            ' esa fila con los datos nuevos y se quita de la papelera.
            If tp = tipobd.Excel_ODBC AndAlso EstaMarcadaEliminada(dni) Then
                conectar()
                Try
                    Dim sqlRec As String = "UPDATE " & tabla_federa_xls & " SET numero = ?, nombre = ?, apellido1 = ?, apellido2 = ?, fechanac = ?, domicilio = ?, localidad = ?, cp = ?, modalidad = ?, precio = ?, telefono = ?, comentarios = ? WHERE nif = ?"
                    Using cmd As New OdbcCommand(sqlRec, conn2)
                        cmd.Parameters.AddWithValue("p1", nsocio)
                        cmd.Parameters.AddWithValue("p2", nombre)
                        cmd.Parameters.AddWithValue("p3", apellido1)
                        cmd.Parameters.AddWithValue("p4", apellido2)
                        cmd.Parameters.AddWithValue("p5", fechanac)
                        cmd.Parameters.AddWithValue("p6", domicilio)
                        cmd.Parameters.AddWithValue("p7", localidad)
                        cmd.Parameters.AddWithValue("p8", cp)
                        cmd.Parameters.AddWithValue("p9", modalidad)
                        cmd.Parameters.AddWithValue("p10", precio)
                        cmd.Parameters.AddWithValue("p11", telefono)
                        cmd.Parameters.AddWithValue("p12", comentarios)
                        cmd.Parameters.AddWithValue("p13", dni)
                        If cmd.ExecuteNonQuery() > 0 Then
                            QuitarDeBorrados(New List(Of String) From {NormalizeId(dni)})
                            MsgBox("Esta tarjeta federativa se había eliminado en esta sesión. Se ha recuperado con los datos nuevos.")
                        Else
                            MsgBox("Este NIF está pendiente de eliminar. Cierre y vuelva a abrir la aplicación antes de darlo de alta de nuevo.")
                        End If
                    End Using
                Finally
                    desconectar()
                End Try
                Return
            End If

            conectar()
            Select Case tp
                Case tipobd.Excel_ODBC
                    consulta2 = New OdbcCommand()
                    consulta2.Connection = conn2
                    If Not conn2.State = ConnectionState.Open Then
                        conectar()
                    End If

                    ' Comprobación de DNI duplicado
                    Dim sql_txt2 As String = "SELECT * FROM " & tabla_federa_xls & " WHERE nif='" & E(dni) & "'"
                    consulta2.CommandText = sql_txt2
                    dr4 = consulta2.ExecuteReader()
                    If (dr4.HasRows) Then
                        MsgBox("DNI no válido. Ya existe otra tarjeta federativa en la temporada actual con el mismo DNI.")
                        dr4.Close()
                    Else
                        dr4.Close()
                        ' Consulta de inserción (valores escapados)
                        Dim sql_txt3 As String = "INSERT INTO " & tabla_federa_xls &
                        " (numero,nombre,apellido1,apellido2,nif,fechanac,domicilio,localidad,cp,modalidad,precio,telefono,comentarios) " &
                        "VALUES(" & E(nsocio) & ",'" & E(nombre) & "','" & E(apellido1) & "','" & E(apellido2) & "','" & E(dni) & "','" & E(fechanac) & "', '" & E(domicilio) & "','" & E(localidad) & "'," & E(cp) & ",'" & E(modalidad) & "','" & E(precio) & "','" & E(telefono) & "','" & E(comentarios) & "')"

                        consulta2.CommandText = sql_txt3
                        Dim salida3 As Integer = consulta2.ExecuteNonQuery()
                        If (salida3 > 0) Then
                            MsgBox("Tarjeta federativa insertada correctamente")
                            ' Si quieres resetear frm_federativas, usa frm_federativas.reset()
                            If TypeOf frm_socio Is Form Then
                                ' no tocar si tu intención era usar frm_socio.reset(); aquí preferible reset del propio formulario
                            End If
                        End If
                    End If

                Case tipobd.MySQL
                    ' De momento no se usa MySQL para federativas - implementar si hace falta.
            End Select

            desconectar()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Modifica un socio existente en la fuente de datos (MySQL o Excel ODBC).
    ''' Antes de actualizar comprueba si existe el registro (por DNI o por número) y solicita confirmación al usuario.
    ''' Usa consultas parametrizadas para evitar problemas de concatenación o inyección.
    ''' </summary>
    ''' <param name="nsocio">Número de socio a establecer.</param>
    ''' <param name="nombre">Nombre del socio.</param>
    ''' <param name="apellidos">Apellidos del socio.</param>
    ''' <param name="dni">DNI/NIF del socio (se usa para comprobación y/o búsqueda).</param>
    ''' <param name="direcc">Dirección postal.</param>
    ''' <param name="cp">Código postal.</param>
    ''' <param name="localidad">Localidad.</param>
    ''' <param name="provincia">Provincia.</param>
    ''' <param name="pais">País.</param>
    ''' <param name="fechanac">Fecha de nacimiento (texto).</param>
    ''' <param name="email">Correo electrónico.</param>
    ''' <param name="tarjeta">Tipo de tarjeta (numérico o texto según origen).</param>
    ''' <param name="tipo_socio">Tipo de socio (texto).</param>
    ''' <param name="import">Importe (texto).</param>
    ''' <param name="comentarios">Comentarios.</param>
    Public Sub modificar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, import As String, comentarios As String)
        Try
            conectar()

            Select Case tp
                Case tipobd.MySQL
                    ' NOTA: rama MySQL sin cambios (no se usa actualmente).
                    Using chk As New MySqlCommand("SELECT n_socio FROM " & tabla_socios_mysql & " WHERE dni = @dni OR n_socio = @n LIMIT 1", conn1)
                        chk.Parameters.AddWithValue("@dni", dni)
                        chk.Parameters.AddWithValue("@n", nsocio)
                        Dim existing = chk.ExecuteScalar()
                        If existing Is Nothing OrElse IsDBNull(existing) Then
                            MsgBox("Registro no encontrado (ni por DNI ni por número). No se realizará la modificación.")
                            Return
                        End If
                        Dim existingNsocio = existing.ToString()
                        Dim resp = MsgBox("Se va a actualizar el socio con número " & existingNsocio & " (DNI: " & dni & "). ¿Desea continuar?", vbYesNo + vbQuestion, "Confirmar modificación")
                        If resp <> vbYes Then Return
                        Using cmd As New MySqlCommand("UPDATE " & tabla_socios_mysql & " SET n_socio=@n, nombre=@nom, apellidos=@ape, dni=@dni, direccion=@dir, cp=@cp, localidad=@loc, provincia=@prov, pais=@pais, fechanac=@fna, email=@email, tarjeta=@tarj, tipo_socio=@tipo, importe=@imp, comentarios=@com WHERE n_socio=@whereN", conn1)
                            cmd.Parameters.AddWithValue("@n", nsocio)
                            cmd.Parameters.AddWithValue("@nom", nombre)
                            cmd.Parameters.AddWithValue("@ape", apellidos)
                            cmd.Parameters.AddWithValue("@dni", dni)
                            cmd.Parameters.AddWithValue("@dir", direcc)
                            cmd.Parameters.AddWithValue("@cp", cp)
                            cmd.Parameters.AddWithValue("@loc", localidad)
                            cmd.Parameters.AddWithValue("@prov", provincia)
                            cmd.Parameters.AddWithValue("@pais", pais)
                            cmd.Parameters.AddWithValue("@fna", fechanac)
                            cmd.Parameters.AddWithValue("@email", email)
                            cmd.Parameters.AddWithValue("@tarj", tarjeta)
                            cmd.Parameters.AddWithValue("@tipo", tipo_socio)
                            cmd.Parameters.AddWithValue("@imp", import)
                            cmd.Parameters.AddWithValue("@com", comentarios)
                            cmd.Parameters.AddWithValue("@whereN", existingNsocio)
                            If cmd.ExecuteNonQuery() > 0 Then MsgBox("Socio modificado correctamente")
                        End Using
                    End Using

                Case tipobd.Excel_ODBC
                    dni = dni.Trim()
                    nsocio = nsocio.Trim()
                    If SocioEliminado(dni) Then
                        MsgBox("El socio con DNI " & dni & " está eliminado. No se puede modificar.")
                        Return
                    End If

                    ' Localizar el socio de la temporada: primero por DNI y, si no aparece (p. ej. se está
                    ' corrigiendo el DNI), por número. Antes se buscaba "dni = ? OR numero = ?" y, si el DNI
                    ' era de un socio y el número de otro, se podía modificar el que no era.
                    Dim filas = Consultar("SELECT numero, dni FROM " & tabla_socios_xls & " WHERE dni = ?", dni)
                    If filas.Count = 0 Then
                        filas = Consultar("SELECT numero, dni FROM " & tabla_socios_xls & " WHERE numero = ?", nsocio)
                        filas.RemoveAll(Function(f) SocioEliminado(Convert.ToString(f(1))))
                    End If
                    If filas.Count = 0 Then
                        MsgBox("No existe ningún socio en la temporada actual con DNI " & dni & " ni con número " & nsocio & ". No se realizará la modificación.")
                        Return
                    End If
                    If filas.Count > 1 Then
                        MsgBox("Hay varios socios en la temporada con el número " & nsocio & ". Búsquelo por DNI y vuelva a intentarlo.")
                        Return
                    End If
                    Dim numeroActual = Convert.ToString(filas(0)(0))
                    Dim dniActual = Convert.ToString(filas(0)(1))

                    ' El nuevo número no puede ser de OTRO socio activo
                    For Each f In Consultar("SELECT dni, nombre, apellidos FROM " & tabla_socios_xls & " WHERE numero = ?", nsocio)
                        Dim otroDni = Convert.ToString(f(0))
                        If NormalizeId(otroDni) <> NormalizeId(dniActual) AndAlso Not SocioEliminado(otroDni) Then
                            MsgBox("El número " & nsocio & " ya está asignado a " & Convert.ToString(f(1)) & " " & Convert.ToString(f(2)) & " (DNI " & otroDni & "). No se realizará la modificación.")
                            Return
                        End If
                    Next
                    ' Si se cambia el DNI, el nuevo no puede ser de otro socio
                    If NormalizeId(dni) <> NormalizeId(dniActual) AndAlso Consultar("SELECT numero FROM " & tabla_socios_xls & " WHERE dni = ?", dni).Count > 0 Then
                        MsgBox("Ya existe otro socio en la temporada con el DNI " & dni & ". No se realizará la modificación.")
                        Return
                    End If

                    Dim resp = MsgBox("Se va a modificar el socio número " & numeroActual & " (DNI: " & dniActual & "). ¿Desea continuar?", vbYesNo + vbQuestion, "Confirmar modificación")
                    If resp <> vbYes Then Return

                    Using cmd As New OdbcCommand("UPDATE " & tabla_socios_xls & " SET numero = ?, nombre = ?, apellidos = ?, dni = ?, direccion = ?, cp = ?, localidad = ?, provincia = ?, pais = ?, fechanac = ?, email = ?, tarjeta = ?, tipo_socio = ?, importe = ?, comentarios = ? WHERE dni = ?", conn2)
                        AddParams(cmd, ParamNumero(nsocio), nombre, apellidos, dni, direcc, ParamNumero(cp), localidad, provincia, pais, fechanac, email, ParamNumero(tarjeta), tipo_socio, ParamNumero(import), comentarios, dniActual)
                        If cmd.ExecuteNonQuery() > 0 Then
                            MsgBox("Socio modificado correctamente.")
                        Else
                            MsgBox("No se ha modificado ningún registro.")
                        End If
                    End Using
            End Select

        Catch ex As Exception
            MsgBox("Error en modificar_socio: " & ex.Message)
        Finally
            Try
                desconectar()
            Catch
            End Try
        End Try
    End Sub


    ''' <summary>
    ''' Modifica una entrada de la tabla/hoja de <c>federativas</c>.
    ''' Comprueba si existe el registro (por <c>nif</c> o por <c>numero</c>), pide confirmación y realiza el UPDATE.
    ''' Implementado para Excel vía ODBC; muestra mensaje si se intenta usar MySQL (no configurado por defecto).
    ''' </summary>
    ''' <param name="nsocio">Número de socio (texto).</param>
    ''' <param name="nombre">Nombre.</param>
    ''' <param name="apellido1">Primer apellido.</param>
    ''' <param name="apellido2">Segundo apellido (opcional).</param>
    ''' <param name="dni">NIF/DNI.</param>
    ''' <param name="fechanac">Fecha de nacimiento (texto).</param>
    ''' <param name="domicilio">Domicilio.</param>
    ''' <param name="localidad">Localidad.</param>
    ''' <param name="cp">Código postal.</param>
    ''' <param name="modalidad">Modalidad (MOSCA/LANCE).</param>
    ''' <param name="precio">Precio/importe (texto).</param>
    ''' <param name="telefono">Teléfono.</param>
    ''' <param name="comentarios">Comentarios.</param>
    Public Sub modificar_federativa(nsocio As String, nombre As String, apellido1 As String, apellido2 As String, dni As String, fechanac As String, domicilio As String, localidad As String, cp As String, modalidad As String, precio As String, telefono As String, comentarios As String)
        Try
            If EstaMarcadaEliminada(dni) Then
                MsgBox("La tarjeta federativa con NIF " & dni & " está eliminada. No se puede modificar.")
                Return
            End If
            conectar()

            Select Case tp
                Case tipobd.Excel_ODBC
                    ' Normalizar nombre de hoja/rango Excel
                    Dim normalizeTable = Function(t As String) As String
                                             If String.IsNullOrWhiteSpace(t) Then Throw New ArgumentException("Nombre de tabla Excel vacío")
                                             Dim s = t.Trim()
                                             If s.StartsWith("[") AndAlso s.EndsWith("]") Then Return s
                                             If s.EndsWith("$") Then Return "[" & s & "]"
                                             Return "[" & s & "$]"
                                         End Function

                    Dim tableName As String = normalizeTable(tabla_federa_xls)

                    ' Comprobar existencia por nif o numero
                    Using chk As New OdbcCommand("SELECT numero FROM " & tableName & " WHERE nif = ? OR numero = ?", conn2)
                        chk.Parameters.AddWithValue("p1", dni)
                        chk.Parameters.AddWithValue("p2", nsocio)
                        Dim existingObj = chk.ExecuteScalar()
                        If existingObj Is Nothing OrElse IsDBNull(existingObj) Then
                            MsgBox("Registro de federativa no encontrado (ni por NIF ni por número) en la hoja Excel. No se realizará la modificación.")
                            Return
                        End If
                        Dim existingNumero = existingObj.ToString()

                        ' Confirmación
                        Dim resp = MsgBox("Se va a actualizar la tarjeta federativa con número " & existingNumero & " (NIF: " & dni & "). ¿Desea continuar?", vbYesNo + vbQuestion, "Confirmar modificación")
                        If resp <> vbYes Then Return

                        ' UPDATE con placeholders ? (orden de parámetros debe coincidir)
                        Dim sql As String = "UPDATE " & tableName & " SET numero = ?, nombre = ?, apellido1 = ?, apellido2 = ?, nif = ?, fechanac = ?, domicilio = ?, localidad = ?, cp = ?, modalidad = ?, precio = ?, telefono = ?, comentarios = ? WHERE numero = ?"
                        Using cmd As New OdbcCommand(sql, conn2)
                            cmd.Parameters.AddWithValue("p1", nsocio)
                            cmd.Parameters.AddWithValue("p2", nombre)
                            cmd.Parameters.AddWithValue("p3", apellido1)
                            cmd.Parameters.AddWithValue("p4", apellido2)
                            cmd.Parameters.AddWithValue("p5", dni)
                            cmd.Parameters.AddWithValue("p6", fechanac)
                            cmd.Parameters.AddWithValue("p7", domicilio)
                            cmd.Parameters.AddWithValue("p8", localidad)
                            cmd.Parameters.AddWithValue("p9", cp)
                            cmd.Parameters.AddWithValue("p10", modalidad)
                            cmd.Parameters.AddWithValue("p11", precio)
                            cmd.Parameters.AddWithValue("p12", telefono)
                            cmd.Parameters.AddWithValue("p13", comentarios)
                            ' WHERE numero = existingNumero
                            cmd.Parameters.AddWithValue("p14", existingNumero)

                            Dim rows = cmd.ExecuteNonQuery()
                            If rows > 0 Then
                                MsgBox("Tarjeta federativa modificada correctamente. Registros afectados: " & rows.ToString())
                            Else
                                MsgBox("No se ha modificado ningún registro.")
                            End If
                        End Using
                    End Using

                Case tipobd.MySQL
                    ' Si se desea soporte MySQL, implementar aquí la lógica equivalente parametrizada.
                    MsgBox("Modificación de federativas en MySQL no está implementada. Use Excel ODBC o implemente la tabla MySQL correspondiente.")
                Case Else
                    MsgBox("Tipo de BBDD no soportado para modificar federativas.")
            End Select

        Catch ex As Exception
            MsgBox("Error en modificar_federativa: " & ex.Message)
        Finally
            Try
                desconectar()
            Catch
            End Try
        End Try
    End Sub


    ''' <summary>
    ''' Elimina un socio del origen de datos (Excel ODBC o MySQL) buscando por DNI.
    ''' Solicita confirmación al usuario antes de ejecutar la operación.
    ''' </summary>
    ''' <param name="nsocio">Número de socio (opcional, no usado para la eliminación actual).</param>
    ''' <param name="nombre">Nombre (informativo).</param>
    ''' <param name="apellidos">Apellidos (informativo).</param>
    ''' <param name="dni">DNI/NIF utilizado en la cláusula WHERE para identificar el registro a eliminar.</param>
    ''' <param name="direcc">Dirección (informativo).</param>
    ''' <param name="cp">Código postal (informativo).</param>
    ''' <param name="localidad">Localidad (informativo).</param>
    ''' <param name="provincia">Provincia (informativo).</param>
    ''' <param name="pais">País (informativo).</param>
    ''' <param name="fechanac">Fecha de nacimiento (informativo).</param>
    ''' <param name="email">Email (informativo).</param>
    ''' <param name="tarjeta">Tarjeta (informativo).</param>
    ''' <param name="tipo_socio">Tipo de socio (informativo).</param>
    ''' <param name="pago">Pago (informativo).</param>
    ''' <param name="comentarios">Comentarios (informativo).</param>
    Public Sub eliminar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, pago As String, comentarios As String)
        ' La confirmación se pide en frm_socio. Solo se usa el DNI; el resto de parámetros son informativos.
        Try
            Select Case tp
                Case tipobd.Excel_ODBC
                    ' El driver ODBC de Excel no admite DELETE: borrado lógico apuntando el DNI en la
                    ' papelera. La fila se borra físicamente del Excel la próxima vez que se abra la aplicación.
                    Dim key As String = NormalizeId(dni)
                    If key = "" Then
                        MsgBox("Indique el DNI del socio a eliminar.")
                        Return
                    End If
                    ' Comprobar que ese DNI es de un socio de la temporada (tabla en memoria, ya sin eliminados)
                    Dim existe As Boolean = False
                    If ds_club.Tables.Contains("socios") AndAlso ds_club.Tables("socios").Columns.Contains("dni") Then
                        For Each r As DataRow In ds_club.Tables("socios").Rows
                            If Not IsDBNull(r("dni")) AndAlso NormalizeId(r("dni").ToString()) = key Then existe = True : Exit For
                        Next
                    End If
                    If Not existe Then
                        MsgBox("No existe ningún socio de la temporada actual con DNI '" & dni & "'. No se ha eliminado nada.")
                        Return
                    End If
                    If AnotarEnPapelera(PAPELERA_SOCIOS, key) Then
                        MsgBox("Socio con DNI " & dni & " eliminado de la temporada actual." & vbCrLf &
                               "(Se borrará definitivamente del Excel la próxima vez que abra la aplicación.)")
                    Else
                        MsgBox("No se pudo registrar la eliminación (ruta de recursos no configurada).")
                    End If

                Case tipobd.MySQL
                    conectar()
                    Using cmd As New MySqlCommand("DELETE FROM " & tabla_socios_mysql & " WHERE dni = @dni", conn1)
                        cmd.Parameters.AddWithValue("@dni", dni)
                        Dim deleted As Integer = cmd.ExecuteNonQuery()
                        MsgBox("Socio eliminado correctamente. Se han eliminado: " & deleted.ToString() & " registros.")
                    End Using

                Case Else
                    MsgBox("Tipo de BBDD no soportado para eliminación.")
            End Select

        Catch ex As Exception
            MsgBox("Error al eliminar socio: " & ex.Message)
        Finally
            Try
                desconectar()
            Catch
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' Elimina una tarjeta federativa de la hoja Excel (o de MySQL si se implementa) buscando por NIF o por número.
    ''' Solicita confirmación al usuario antes de ejecutar la eliminación y muestra el número de filas afectadas.
    ''' </summary>
    ''' <param name="nsocio">Número de socio (texto).</param>
    ''' <param name="nombre">Nombre (informativo).</param>
    ''' <param name="apellido1">Primer apellido (informativo).</param>
    ''' <param name="apellido2">Segundo apellido (informativo).</param>
    ''' <param name="dni">NIF/DNI utilizado para identificar el registro a eliminar.</param>
    ''' <param name="fechanac">Fecha de nacimiento (informativo).</param>
    ''' <param name="domicilio">Domicilio (informativo).</param>
    ''' <param name="localidad">Localidad (informativo).</param>
    ''' <param name="cp">Código postal (informativo).</param>
    ''' <param name="modalidad">Modalidad (informativo).</param>
    ''' <param name="precio">Precio/importe (informativo).</param>
    ''' <param name="telefono">Teléfono (informativo).</param>
    ''' <param name="comentarios">Comentarios (informativo).</param>
    Public Sub eliminar_federativa(nsocio As String, nombre As String, apellido1 As String, apellido2 As String, dni As String, fechanac As String, domicilio As String, localidad As String, cp As String, modalidad As String, precio As String, telefono As String, comentarios As String)
        ' La confirmación se pide en frm_federativas (antes se preguntaba dos veces).
        Try
            Select Case tp
                Case tipobd.Excel_ODBC
                    ' El driver ODBC de Excel no admite DELETE: borrado lógico apuntando el NIF.
                    ' No hace falta abrir la conexión ODBC para esto (y así no se bloquea el fichero).
                    Dim filePath As String = RutaPapelera(PAPELERA_FEDERATIVAS)
                    If filePath = "" Then
                        MsgBox("Ruta de recursos no configurada. No se puede eliminar.")
                        Return
                    End If
                    ' Siempre por NIF: mezclar NIF y números en el mismo fichero podía borrar filas equivocadas.
                    Dim key As String = NormalizeId(dni)
                    If key = "" Then
                        MsgBox("Indique el NIF de la tarjeta federativa a eliminar.")
                        Return
                    End If
                    ' Comprobar que ese NIF existe de verdad en la tabla de federativas; así no se
                    ' apunta basura en la papelera si el formulario tiene un dato erróneo.
                    Dim existe As Boolean = False
                    If ds_club.Tables.Contains("federativas") AndAlso ds_club.Tables("federativas").Columns.Contains("nif") Then
                        For Each r As DataRow In ds_club.Tables("federativas").Rows
                            If Not IsDBNull(r("nif")) AndAlso NormalizeId(r("nif").ToString()) = key Then existe = True : Exit For
                        Next
                    End If
                    If Not existe Then
                        MsgBox("No existe ninguna tarjeta federativa con NIF '" & dni & "'. No se ha eliminado nada.")
                        Return
                    End If
                    If Not EstaMarcadaEliminada(key) Then
                        Using sw As New StreamWriter(filePath, True)
                            sw.WriteLine(key)
                        End Using
                    End If
                    MsgBox("Tarjeta federativa con NIF " & key & " eliminada." & vbCrLf &
                           "(Se borrará definitivamente del Excel al guardar o al salir de la aplicación.)")

                Case tipobd.MySQL
                    conectar()
                    Using cmd As New MySqlCommand("DELETE FROM " & tabla_federa_xls & " WHERE nif = @dni", conn1)
                        cmd.Parameters.AddWithValue("@dni", dni)
                        Dim deleted As Integer = cmd.ExecuteNonQuery()
                        MsgBox("Eliminación completada en MySQL. Registros eliminados: " & deleted.ToString())
                    End Using

                Case Else
                    MsgBox("Tipo de BBDD no soportado para eliminar federativas.")
            End Select

        Catch ex As Exception
            MsgBox("Error en eliminar_federativa: " & ex.Message)
        Finally
            Try
                desconectar()
            Catch
            End Try
        End Try
    End Sub

End Module
