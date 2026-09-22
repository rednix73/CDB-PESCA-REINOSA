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
    Public precio_salmon As Integer '= 16
    Public precio_trucha As Integer '= 10
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
    '  ELIMINACIÓN DE FEDERATIVAS EN DOS FASES ("papelera")
    '  1) eliminar_federativa: el driver ODBC de Excel NO admite DELETE, así que el NIF se
    '     apunta en deleted_federativas.txt y cargar() oculta esa fila (borrado lógico).
    '  2) PurgeDeletedFederativas: al guardar o salir se abre el libro con Excel (Interop)
    '     y se borran físicamente las filas apuntadas (vaciar la papelera).
    '  En el fichero se guarda SIEMPRE el NIF normalizado (NormalizeId): mayúsculas, sin
    '  espacios ni guiones. Así la escritura, el filtro y la purga comparan lo mismo.
    ' =====================================================================================

    ''' <summary>Ruta completa del fichero de borrados lógicos (o cadena vacía si no hay ruta de recursos).</summary>
    Private Function RutaFicheroBorrados() As String
        Dim base As String = My.Settings.ruta_recursos
        If String.IsNullOrWhiteSpace(base) Then Return String.Empty
        Return Path.Combine(base, "deleted_federativas.txt")
    End Function

    ''' <summary>Indica si un NIF está marcado como eliminado y pendiente de purga.</summary>
    Public Function EstaMarcadaEliminada(nif As String) As Boolean
        Dim k = NormalizeId(nif)
        Return k <> "" AndAlso GetDeletedFederativasSet().Contains(k)
    End Function

    ''' <summary>Quita del fichero de borrados las claves indicadas (ya purgadas).</summary>
    Public Sub QuitarDeBorrados(purgadas As System.Collections.Generic.ICollection(Of String))
        Dim delFile = RutaFicheroBorrados()
        If delFile = "" OrElse Not File.Exists(delFile) Then Return
        Dim remaining As New System.Collections.Generic.List(Of String)()
        For Each ln In File.ReadAllLines(delFile)
            Dim k = NormalizeId(ln)
            ' Comparamos la línea NORMALIZADA (antes se comparaba la línea tal cual y no se borraba nunca)
            If k <> "" AndAlso Not purgadas.Contains(k) AndAlso Not remaining.Contains(k) Then remaining.Add(k)
        Next
        File.WriteAllLines(delFile, remaining.ToArray())
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

    ''' <summary>
    ''' Purga físicamente del libro Excel las filas marcadas en deleted_federativas.txt.
    ''' Crea una copia de seguridad antes de modificar el archivo.
    ''' </summary>
    ''' <param name="preguntar">Si es False no pide confirmación (se usa al reinsertar un NIF pendiente de purga).</param>
    ''' <returns>Filas eliminadas, 0 si no había nada que purgar o -1 si hubo error / se canceló.</returns>
    Public Function PurgeDeletedFederativas(Optional preguntar As Boolean = True) As Integer
        Try
            Dim delFile As String = RutaFicheroBorrados()
            If delFile = "" OrElse Not File.Exists(delFile) Then Return 0
            Dim keys = GetDeletedFederativasSet()
            If keys.Count = 0 Then Return 0 ' nada pendiente: no molestamos al usuario

            ' El libro a purgar es EL MISMO al que apunta el DSN de ODBC (donde se guardan los datos),
            ' no el de configuracion.txt, que puede no coincidir.
            Dim libro As String = RutaLibroDesdeDSN(DSN)
            If libro = "" Then libro = ruta_bd_excel
            If String.IsNullOrWhiteSpace(libro) OrElse Not File.Exists(libro) Then
                MsgBox("Fichero Excel de datos no encontrado: " & libro)
                Return -1
            End If

            Dim dir = Path.GetDirectoryName(libro)
            Dim bk = Path.Combine(dir, Path.GetFileNameWithoutExtension(libro) & "_backup_" & DateTime.Now.ToString("yyyyMMddHHmmss") & Path.GetExtension(libro))

            If preguntar Then
                Dim msg = "Hay " & keys.Count.ToString() & " tarjeta(s) federativa(s) marcadas para eliminar." & vbCrLf &
                          "¿Desea eliminarlas definitivamente del libro Excel?" & vbCrLf & "(Se creará una copia de seguridad: " & bk & ")"
                If MessageBox.Show(msg, "Confirmar purga", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return -1
            End If

            ' --- CLAVE: soltar el fichero que mantiene abierto el pool de ODBC ---
            ' conn2.Close() devuelve la conexión al pool, pero el driver de Excel sigue con el .xls
            ' abierto. Si no se libera, Excel abre el libro en SOLO LECTURA y Save() falla.
            desconectar()
            System.Data.Odbc.OdbcConnection.ReleaseObjectPool()
            GC.Collect()
            GC.WaitForPendingFinalizers()

            File.Copy(libro, bk)

            Dim eliminadas As Integer
            If Path.GetExtension(libro).ToLowerInvariant() = ".xls" Then
                eliminadas = PurgarFederativasXls(libro, tabla_federa_xls, keys)
                ' Recolección FUERA del método que usó COM: así los RCW ya no están referenciados
                ' y EXCEL.EXE termina de verdad (en Debug las variables locales viven hasta el final del método).
                GC.Collect()
                GC.WaitForPendingFinalizers()
                GC.Collect()
                GC.WaitForPendingFinalizers()
                If eliminadas >= 0 Then QuitarDeBorrados(keys)
            Else
                ' Solo se soporta .xls (Excel 2007 en el equipo destino). Se ha quitado la dependencia de
                ' NPOI (bbdd_npoi.vb): no se usaba con .xls y le faltaban DLLs para funcionar con .xlsx.
                MsgBox("La purga automática solo está disponible para libros .xls. No se ha eliminado nada.")
                Return -1
            End If

            If eliminadas >= 0 AndAlso preguntar Then
                MsgBox("Purga completada. Filas eliminadas del Excel: " & eliminadas.ToString())
            End If
            Return eliminadas

        Catch ex As Exception
            MsgBox("Error al purgar federativas: " & ex.Message)
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Borra con Excel (Interop) las filas de la hoja cuyo NIF (normalizado) está en <paramref name="keys"/>.
    ''' Libera todos los objetos COM para que no quede EXCEL.EXE abierto con el fichero bloqueado.
    ''' </summary>
    ''' <returns>Filas eliminadas o -1 si hubo error.</returns>
    Private Function PurgarFederativasXls(ruta As String, tabla As String, keys As System.Collections.Generic.HashSet(Of String)) As Integer
        Dim xlApp As Microsoft.Office.Interop.Excel.Application = Nothing
        Dim libros As Microsoft.Office.Interop.Excel.Workbooks = Nothing
        Dim xlWb As Microsoft.Office.Interop.Excel.Workbook = Nothing
        Dim hojas As Microsoft.Office.Interop.Excel.Sheets = Nothing
        Dim xlWs As Microsoft.Office.Interop.Excel.Worksheet = Nothing
        Dim usado As Microsoft.Office.Interop.Excel.Range = Nothing
        Dim eliminadas As Integer = 0
        Try
            xlApp = New Microsoft.Office.Interop.Excel.Application()
            xlApp.Visible = False
            xlApp.DisplayAlerts = False
            xlApp.ScreenUpdating = False

            libros = xlApp.Workbooks                       ' evitar "doble punto" (xlApp.Workbooks.Open) que deja objetos COM sin liberar
            xlWb = libros.Open(ruta, 0, False)             ' UpdateLinks:=0, ReadOnly:=False
            If xlWb.ReadOnly Then
                MsgBox("El libro Excel está abierto por otro programa (¿Excel abierto? ¿OneDrive sincronizando?)." & vbCrLf &
                       "Ciérrelo y vuelva a intentarlo. No se ha eliminado nada.")
                xlWb.Close(False)
                Return -1
            End If

            Dim hoja As String = tabla.Replace("[", "").Replace("]", "").TrimEnd("$"c)
            hojas = xlWb.Worksheets
            xlWs = CType(hojas.Item(hoja), Microsoft.Office.Interop.Excel.Worksheet) ' si no existe, error (nunca borrar en otra hoja)

            usado = xlWs.UsedRange
            Dim valores As Object = usado.Value2           ' toda la hoja de una vez: mucho más rápido que celda a celda
            If Not TypeOf valores Is Object(,) Then
                xlWb.Close(False)
                Return 0
            End If
            Dim datos = DirectCast(valores, Object(,))
            Dim filaInicio As Integer = usado.Row          ' UsedRange no tiene por qué empezar en la fila 1
            Dim nFilas = datos.GetUpperBound(0), nCols = datos.GetUpperBound(1)

            Dim colNif As Integer = -1
            For c As Integer = 1 To nCols
                Dim h = Convert.ToString(datos(1, c)).Trim().ToLowerInvariant()
                If h = "nif" OrElse h = "dni" Then colNif = c : Exit For
            Next
            If colNif = -1 Then
                MsgBox("No se encuentra la columna 'nif' en la hoja " & hoja & ". No se ha eliminado nada.")
                xlWb.Close(False)
                Return -1
            End If

            ' De abajo hacia arriba: al borrar una fila, las de debajo suben y no afectan a las que faltan por revisar
            For f As Integer = nFilas To 2 Step -1
                Dim nif = NormalizeId(Convert.ToString(datos(f, colNif)))
                If nif <> "" AndAlso keys.Contains(nif) Then
                    Dim fila = CType(xlWs.Rows(filaInicio + f - 1), Microsoft.Office.Interop.Excel.Range)
                    fila.Delete(Microsoft.Office.Interop.Excel.XlDeleteShiftDirection.xlShiftUp)
                    Marshal.ReleaseComObject(fila)
                    eliminadas += 1
                End If
            Next

            If eliminadas > 0 Then xlWb.Save()
            xlWb.Close(False)
            Return eliminadas

        Catch ex As Exception
            MsgBox("Error al purgar .xls con Excel: " & ex.Message)
            Try
                If xlWb IsNot Nothing Then xlWb.Close(False)
            Catch
            End Try
            Return -1
        Finally
            ' Liberar en orden inverso a como se obtuvieron
            If usado IsNot Nothing Then Marshal.ReleaseComObject(usado)
            If xlWs IsNot Nothing Then Marshal.ReleaseComObject(xlWs)
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

    ' Devuelve el conjunto de NIF (normalizados) marcados como eliminados en el fichero de borrados.
    Private Function GetDeletedFederativasSet() As System.Collections.Generic.HashSet(Of String)
        Dim deletedSet As New System.Collections.Generic.HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Try
            Dim filePath As String = RutaFicheroBorrados()
            If filePath = "" OrElse Not File.Exists(filePath) Then Return deletedSet
            For Each ln In File.ReadAllLines(filePath)
                Dim k = NormalizeId(ln)
                If k <> "" Then deletedSet.Add(k)
            Next
        Catch
        End Try
        Return deletedSet
    End Function

    ''' <summary>Normaliza un identificador: mayúsculas y solo letras/dígitos ("12345678-z " -> "12345678Z").</summary>
    Public Function NormalizeId(ByVal s As String) As String
        If String.IsNullOrWhiteSpace(s) Then Return String.Empty
        Return Regex.Replace(s.Trim().ToUpperInvariant(), "[^A-Z0-9]", "")
    End Function
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
                ' Aplicar filtro de eliminados lógicos almacenados en fichero en ruta_recursos
                Try
                    Dim deletedSet = GetDeletedFederativasSet()
                    If deletedSet IsNot Nothing AndAlso deletedSet.Count > 0 Then
                        Dim dtFed As System.Data.DataTable = ds_club.Tables("federativas")
                        For i As Integer = dtFed.Rows.Count - 1 To 0 Step -1
                            Dim row = dtFed.Rows(i)
                            Dim nifVal As String = String.Empty
                            If dtFed.Columns.Contains("nif") AndAlso Not IsDBNull(row("nif")) Then nifVal = NormalizeId(row("nif").ToString())
                            ' Comparar el NIF NORMALIZADO, igual que se guarda en el fichero de borrados
                            If nifVal <> "" AndAlso deletedSet.Contains(nifVal) Then dtFed.Rows.RemoveAt(i)
                        Next
                    End If
                Catch
                End Try
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

            Dim sr As New StreamReader(ruta_recursos + "/configuracion.txt")
            Dim linea As String = ""
            linea = sr.ReadLine()
            linea = sr.ReadLine()
            temporada = sr.ReadLine()
            linea = sr.ReadLine()
            precio_salmon = sr.ReadLine()
            linea = sr.ReadLine()
            precio_trucha = sr.ReadLine()
            linea = sr.ReadLine()
            linea = sr.ReadLine()
            linea = sr.ReadLine()
            If (linea = tipobd.Excel_ODBC.ToString()) Then
                tp = tipobd.Excel_ODBC
            End If
            If (linea = tipobd.MySQL.ToString()) Then
                tp = tipobd.MySQL
            End If
            linea = sr.ReadLine()
            linea = sr.ReadLine()
            linea = sr.ReadLine()
            ruta_bd_excel = sr.ReadLine()
            linea = sr.ReadLine()
            DSN = sr.ReadLine()
            linea = sr.ReadLine()
            tabla_socios_xls = sr.ReadLine()
            linea = sr.ReadLine()
            tabla_bdsocios_xls = sr.ReadLine()
            linea = sr.ReadLine()
            tabla_federa_xls = sr.ReadLine()
            linea = sr.ReadLine()
            linea = sr.ReadLine()
            server = sr.ReadLine()
            linea = sr.ReadLine()
            port = sr.ReadLine()
            linea = sr.ReadLine()
            bd_mysql = sr.ReadLine()
            linea = sr.ReadLine()
            user = sr.ReadLine()
            linea = sr.ReadLine()
            password = sr.ReadLine()
            linea = sr.ReadLine()
            tabla_socios_mysql = sr.ReadLine()
            linea = sr.ReadLine()
            tabla_bdsocios_mysql = sr.ReadLine()
            sr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub
    ''' <summary>
    ''' Método que busca en la base de datos los números de socios no utilizados y devuelve una lista con los mismos.
    ''' </summary>
    ''' <returns>
    ''' Devuelve una lista de enteros, con los números de socio no usados.
    ''' </returns>
    Public Function numeros_libres() As List(Of Integer)
        Dim libres As New List(Of Integer)
        Dim libres1 As New List(Of Integer)
        Dim libres2 As New List(Of Integer)
        Dim libre As Boolean = True
        Dim ultimo_bd As Integer
        Dim ultimo_soc As Integer
        Try
            conectar()
            libres.Clear()
            libres1.Clear()
            libres2.Clear()

            Select Case tp
                Case tipobd.Excel_ODBC
                    ultimo_bd = ultimo(tabla_bdsocios_xls)
                    ultimo_soc = ultimo(tabla_socios_xls)
                    'consulta2 = New OdbcCommand()
                    'consulta2.Connection = conn2
                    ''Obtenemos el último número usado en la base de datos de socios.
                    'consulta2.CommandText = "Select MAX(numero) from " + tabla_bdsocios_xls
                    'ultimo_bd = consulta2.ExecuteScalar()
                    'consulta2 = New OdbcCommand()
                    'consulta2.Connection = conn2
                    ''Obtenemos el último número usado en la tabla de socios actual.
                    'consulta2.CommandText = "Select MAX(numero) from " + tabla_socios_xls
                    'ultimo_soc = consulta2.ExecuteScalar()
                    consulta2 = New OdbcCommand()
                    consulta2.Connection = conn2
                    'Obtenemos todos los numeros usados en la tabla bd_socios (base de datos de socios).
                    consulta2.CommandText = "select numero from " + tabla_bdsocios_xls


                    'Recorremos todos los números desde el 1 hasta el último numero usado, y añadimos a la lista libres1 los numeros no usados en la tabla bdsocios.
                    For index = 1 To ultimo_bd
                        dr3 = consulta2.ExecuteReader()
                        While dr3.Read

                            If (Not IsDBNull(dr3(0))) Then
                                If index = CInt(dr3(0)) Then
                                    libre = False
                                End If
                            End If

                        End While
                        dr3.Close()

                        If libre = True Then
                            libres1.Add(index)
                        End If
                        libre = True
                    Next

                    consulta2 = New OdbcCommand()
                    consulta2.Connection = conn2
                    'Obtenemos todos los numeros usados en la tabla socios (base de datos de socios de la temporada actual) y los añadimos a la lista libres2.
                    consulta2.CommandText = "Select numero from " + tabla_socios_xls

                    dr3 = consulta2.ExecuteReader()
                    While dr3.Read
                        If (Not IsDBNull(dr3(0))) Then
                            libres2.Add(dr3(0))
                        End If

                    End While
                    dr3.Close()

                Case tipobd.MySQL
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    'Obtenemos el último número usado en la base de datos de socios.
                    consulta1.CommandText = "Select MAX(n_socio) from " + tabla_bdsocios_mysql
                    ultimo_bd = consulta1.ExecuteScalar
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    'Obtenemos el último número usado en la tabla de socios actual.
                    consulta1.CommandText = "Select MAX(n_socio) from " + tabla_socios_mysql
                    ultimo_soc = consulta1.ExecuteScalar
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    'Obtenemos todos los numeros usados en la tabla bd_socios (base de datos de socios).
                    consulta1.CommandText = "Select n_socio from " + tabla_bdsocios_mysql
                    'Recorremos todos los números desde el 1 hasta el último numero usado, y añadimos a la lista libres1 los numeros no usados en la tabla bdsocios (base de datos con todos los socios).
                    For index = 1 To ultimo_bd
                        dr1 = consulta1.ExecuteReader()
                        While dr1.Read
                            If index = dr1(0) Then
                                libre = False
                            End If
                        End While
                        dr1.Close()
                        If libre = True Then
                            libres1.Add(index)
                        End If
                        libre = True
                    Next
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    'Obtenemos todos los numeros usados en la tabla socios (base de datos de socios de la temporada actual) y los añadimos a la lista libres2.
                    consulta1.CommandText = "Select n_socio from " + tabla_socios_mysql
                    dr1 = consulta1.ExecuteReader()
                    While dr1.Read
                        libres2.Add(dr1(0))
                    End While
                    dr1.Close()
            End Select

            'Recorremos la lista de numeros libres de la tabla bdsocios, y eliminamos de la lista de numeros libre los que estén usados en la tabla socios de la temporada actual.
            For Each n2 In libres2
                For i = (libres1.Count - 1) To 0 Step -1
                    If (libres1(i).Equals(n2)) Then
                        libres1.RemoveAt(i)
                    End If
                Next
            Next
            'Devolvemos la lista de numeros libres de la tabla bdsocios, eliminados los usados en la tabla socios.
            libres = libres1
            desconectar()
            Return libres
        Catch ex As Exception
            MsgBox(ex.ToString())
            Return libres
        End Try
    End Function
    ''' <summary>
    ''' Método que devuelve el último número de socio usado.
    ''' </summary>
    ''' <returns>Develve un entero con el último número de socio en uso.</returns>
    Public Function ultimo(tabla As String) As Integer
        Dim ult As Integer
        Try
            conectar()
            Select Case tp
                Case tipobd.Excel_ODBC
                    consulta2 = New OdbcCommand()
                    consulta2.Connection = conn2
                    'Obtenemos el último número usado.
                    consulta2.CommandText = "Select MAX(numero) from " + tabla
                    ult = consulta2.ExecuteScalar()
                Case tipobd.MySQL
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    'Obtenemos el último número usado.
                    consulta1.CommandText = "Select MAX(n_socio) from " + tabla
                    ult = consulta1.ExecuteScalar()
            End Select
            Return ult
            desconectar()
        Catch ex As Exception
            MsgBox(ex.ToString())
            Return ult
        End Try
    End Function
    ''' <summary>
    ''' Método que filtra el dataview que contiene la tabla con la base de datos de socios por los apellidos del socio y muestra la tabla flitrada en un control datagridview que asu vez se muestra en el formulario: frm_busqueda.
    ''' </summary>
    ''' <param name="valor">texto con el apellido a buscar</param>
    Public Sub buscar_nombre(valor As String)
        Try
            dw_bdsocios.RowFilter = "apellidos Like '%" + valor + "%'"
            frm_busqueda.DataGridView1.DataSource = dw_bdsocios
            frm_busqueda.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Método que filtra el dataview que contiene la tabla con la base de datos de socios por el número de socio y muestra la tabla flitrada en un control datagridview que a su vez se muestra en el formulario: frm_busqueda.
    ''' </summary>
    ''' <param name="valor">numero de socio</param>
    Public Sub buscar_nsocio(valor As String)
        Try
            If Not valor = "" Then
                dw_socios.RowFilter = "numero=" + valor
                dw_bdsocios.RowFilter = "numero=" + valor
                frm_busqueda.DataGridView1.DataSource = dw_socios
                frm_busqueda.ShowDialog()
                If (frm_busqueda.DataGridView1.Rows.Count = 1) Then
                    frm_busqueda.Close()
                    MsgBox("Número de socio no encontrado en temporada actual. Buscando en Base de datos...")
                    frm_busqueda.DataGridView1.DataSource = dw_bdsocios
                    frm_busqueda.ShowDialog()
                    If (frm_busqueda.DataGridView1.Rows.Count = 1) Then
                        frm_busqueda.Close()
                        MsgBox("Número de socio no encontrado. A continuacion se muestran todos los socios en la base de datos.")
                        dw_bdsocios.RowFilter = ""
                        frm_busqueda.DataGridView1.DataSource = dw_bdsocios
                        frm_busqueda.ShowDialog()
                    End If
                End If

            Else
                dw_bdsocios.RowFilter = ""
                dw_socios.RowFilter = ""
                frm_busqueda.DataGridView1.DataSource = dw_bdsocios
                frm_busqueda.ShowDialog()

            End If



        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    ''' <summary>
    ''' Método que filtra por el dni del socio indicado en el parámetro el dataview que contiene la tabla con los socios de la temporada actual (dw_socios). En caso de no encontrarse el socio en la tabla de socios se busca en la tabla que contiene la base de datos (dw_bdsocios) de socios y se muestra la tabla filtrada en un control datagridview que a su vez se muestra en el formulario: frm_busqueda.
    ''' </summary>
    ''' <param name="valor">texto que contiene el dni del socio</param>
    Public Sub buscar_dni(valor As String)
        Try
            dw_socios.RowFilter = "dni like '%" + valor + "%'"
            frm_busqueda.DataGridView1.DataSource = dw_socios
            If (frm_busqueda.DataGridView1.Rows.Count > 1) Then
                frm_busqueda.ShowDialog()
            Else
                MsgBox("Socio no encontrado en temporada actual. Buscando en la base de datos de socios...")
                dw_bdsocios.RowFilter = "dni like '%" + valor + "%'"
                frm_busqueda.DataGridView1.DataSource = dw_bdsocios
                If (frm_busqueda.DataGridView1.Rows.Count > 1) Then
                    frm_busqueda.ShowDialog()
                Else
                    MsgBox("Socio no encontrado")
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub

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
    Public Sub insertar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, import As String, comentarios As String)
        Try
            conectar()
            Select Case tp
                Case tipobd.Excel_ODBC
                    consulta2 = New OdbcCommand()
                    consulta2.Connection = conn2
                    If Not conn2.State = ConnectionState.Open Then
                        conectar()
                    End If
                    'Comprobación de que no existe otro socio en la tabla de socios de la temporada actual con el mismo número de socio.
                    Dim sql_txt As String
                    sql_txt = "SELECT * FROM " + tabla_socios_xls + " WHERE NUMERO='" + nsocio + "'"
                    consulta2.CommandText = sql_txt
                    dr3 = consulta2.ExecuteReader
                    If dr3.HasRows Then
                        MsgBox("Número de socio no válido. Ya existe otro socio en la temporada actual con ese número asignado. Asigne otro número a este socio")
                        dr3.Close()
                    Else
                        dr3.Close()

                        'Comprobación de que no existe otro socio en la tabla de socios de la temporada actual con el mismo número de DNI.
                        Dim sql_txt2 As String
                        sql_txt2 = "SELECT * FROM " + tabla_socios_xls + " WHERE dni='" + dni + "'"
                        consulta2.CommandText = sql_txt2
                        dr4 = consulta2.ExecuteReader
                        If (dr4.HasRows) Then
                            MsgBox("DNI no válido. Ya existe otro socio en la temporada actual con el mismo DNI.")
                            dr4.Close()
                        Else
                            dr4.Close()
                            'Consulta de insercion.
                            Dim sql_txt3 As String = "INSERT INTO " + tabla_socios_xls + " (numero,nombre,apellidos,dni,direccion,cp,localidad,provincia,pais,fechanac,email,tarjeta, tipo_socio,importe,comentarios) 
    VALUES(" + nsocio + ",'" + nombre + "','" + apellidos + "','" + dni + "','" + direcc + "'," + cp + ", '" + localidad + "','" + provincia + "','" + pais + "','" + fechanac + "','" + email + "'," + tarjeta + ",'" + tipo_socio + "'," + import + ",'" + comentarios + "')"
                            consulta2.CommandText = sql_txt3
                            Dim salida3 As Integer = consulta2.ExecuteNonQuery
                            If (salida3 > 0) Then
                                MsgBox("Socio insertado correctamente")
                                frm_socio.reset()
                            End If
                        End If
                    End If

                    'Comprobación de que no existe otro socio en la base de datos de socios con el mismo número de socio.
                    sql_txt = "SELECT * FROM " + tabla_bdsocios_xls + " WHERE NUMERO=" + nsocio
                    consulta2.CommandText = sql_txt
                    dr3 = consulta2.ExecuteReader
                    If (dr3.HasRows) Then
                        MsgBox("Ya existe otro socio en la base de datos de socios con ese número asignado. Compruebe que es el mismo socio.")
                        dr3.Close()
                    Else
                        dr3.Close()

                        'Comprobación de que no existe otro socio en la base de datos de socios con el mismo DNI.
                        Dim sql_txt2 As String
                        sql_txt2 = "SELECT * FROM " + tabla_bdsocios_xls + " WHERE dni='" + dni + "'"
                        consulta2.CommandText = sql_txt2
                        dr4 = consulta2.ExecuteReader
                        If (dr4.HasRows) Then
                            MsgBox("DNI no válido. Ya existe otro socio en la base de datos de socios con el mismo DNI. Compruebe que es el mismo socio.")
                            dr4.Close()
                        Else
                            dr4.Close()
                            'Consulta de insercion de nuev socio en la base de datos de socios.
                            Dim sql_txt3 As String = "INSERT INTO " + tabla_bdsocios_xls + " (numero,nombre,apellidos,dni,direccion,cp,localidad,provincia,pais,fechanac,email,tarjeta, tipo_socio) 
    VALUES(" + nsocio + ",'" + nombre + "','" + apellidos + "','" + dni + "','" + direcc + "'," + cp + ", '" + localidad + "','" + provincia + "','" + pais + "','" + fechanac + "','" + email + "'," + tarjeta + ",'" + tipo_socio + "')"
                            consulta2.CommandText = sql_txt3
                            Dim salida3 As Integer = consulta2.ExecuteNonQuery
                            If (salida3 > 0) Then
                                MsgBox("SOCIO NUEVO! Insertado correctamente en la base de datos de socios.")
                                frm_socio.reset()
                            End If
                        End If
                    End If

                    ' NOTA IMPORTANTE: FALTA EL CODIGO DE INSERCION AUTOMATICA EN LA BASE DE DATOS PARA SOCIOS NUEVOS PARA BASE DE DATOS MYSQL.

                Case tipobd.MySQL
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    If (Not conn1.State = ConnectionState.Open) Then
                        conectar()
                    End If
                    'Comprobación de que no existe otro socio en la base de datos con el mismo número de socio.
                    Dim sql_txt As String
                    sql_txt = "SELECT * FROM socios WHERE n_socio=" + nsocio
                    consulta1.CommandText = sql_txt
                    dr1 = consulta1.ExecuteReader
                    If (dr1.HasRows) Then
                        MsgBox("Número de socio no válido. Ya existe otro socio con ese número asignado. Asigne otro número a este socio")
                        dr1.Close()
                    Else
                        dr1.Close()
                        'Comprobación de que no existe otro socio en la base de datos con el mismo número de DNI.
                        Dim sql_txt2 As String
                        sql_txt2 = "SELECT * FROM socios WHERE dni='" + dni + "'"
                        consulta1.CommandText = sql_txt2
                        dr2 = consulta1.ExecuteReader
                        If (dr2.HasRows) Then
                            MsgBox("Socio no válido. Ya existe otro socio en la base de datos con el mismo DNI.")
                            dr2.Close()
                        Else
                            dr2.Close()
                            'Consulta de insercion.
                            Dim sql_txt3 As String = "INSERT INTO socios(n_socio,nombre,apellidos,dni,direccion,cp,localidad,provincia,pais,fechanac,email,tarjeta, tipo_socio,comentarios) 
    VALUES(" + nsocio + ",'" + nombre + "','" + apellidos + "','" + dni + "','" + direcc + "'," + cp + ", '" + localidad + "','" + provincia + "','" + pais + "','" + fechanac + "','" + email + "'," + tarjeta + ",'" + tipo_socio + "','" + comentarios + "')"
                            consulta1.CommandText = sql_txt3
                            Dim salida3 As Integer = consulta1.ExecuteNonQuery
                            If (salida3 > 0) Then
                                MsgBox("Socio insertado correctamente")
                                frm_socio.reset()
                            End If
                        End If
                    End If


            End Select

            desconectar()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub




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
                    ' Comprobar existencia (buscar por dni o por número)
                    Using chk As New MySqlCommand("SELECT n_socio FROM " & tabla_socios_mysql & " WHERE dni = @dni OR n_socio = @n LIMIT 1", conn1)
                        chk.Parameters.AddWithValue("@dni", dni)
                        chk.Parameters.AddWithValue("@n", nsocio)
                        Dim existing = chk.ExecuteScalar()
                        If existing Is Nothing OrElse IsDBNull(existing) Then
                            MsgBox("Registro no encontrado (ni por DNI ni por número). No se realizará la modificación.")
                            Return
                        End If

                        Dim existingNsocio = existing.ToString()

                        ' Pedir confirmación al usuario
                        Dim resp = MsgBox("Se va a actualizar el socio con número " & existingNsocio & " (DNI: " & dni & "). ¿Desea continuar?", vbYesNo + vbQuestion, "Confirmar modificación")
                        If resp <> vbYes Then
                            Return
                        End If

                        ' Ejecutar UPDATE parametrizado
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
                            Dim rows = cmd.ExecuteNonQuery()
                            If rows > 0 Then MsgBox("Socio modificado correctamente")
                        End Using
                    End Using

                Case tipobd.Excel_ODBC
                    ' Normalizar nombre de hoja/rango Excel
                    Dim normalizeTable = Function(t As String) As String
                                             If String.IsNullOrWhiteSpace(t) Then Throw New ArgumentException("Nombre de tabla Excel vacío")
                                             Dim s = t.Trim()
                                             If s.StartsWith("[") AndAlso s.EndsWith("]") Then Return s
                                             If s.EndsWith("$") Then Return "[" & s & "]"
                                             Return "[" & s & "$]"
                                         End Function

                    Dim tableName As String = normalizeTable(tabla_socios_xls)

                    ' Comprobar existencia en Excel (ODBC)
                    Using chk As New OdbcCommand("SELECT numero FROM " & tableName & " WHERE dni = ? OR numero = ?", conn2)
                        chk.Parameters.AddWithValue("p1", dni)
                        chk.Parameters.AddWithValue("p2", nsocio)
                        Dim existingObj = chk.ExecuteScalar()
                        If existingObj Is Nothing OrElse IsDBNull(existingObj) Then
                            MsgBox("Registro no encontrado (ni por DNI ni por número) en la hoja Excel. No se realizará la modificación.")
                            Return
                        End If
                        Dim existingNsocio = existingObj.ToString()

                        Dim resp = MsgBox("Se va a actualizar el socio con número " & existingNsocio & " (DNI: " & dni & "). ¿Desea continuar?", vbYesNo + vbQuestion, "Confirmar modificación")
                        If resp <> vbYes Then
                            Return
                        End If

                        ' UPDATE con placeholders ? en orden
                        Dim sql As String = "UPDATE " & tableName & " SET numero = ?, nombre = ?, apellidos = ?, dni = ?, direccion = ?, cp = ?, localidad = ?, provincia = ?, pais = ?, fechanac = ?, email = ?, tarjeta = ?, tipo_socio = ?, importe = ?, comentarios = ? WHERE numero = ?"
                        Using cmd As New OdbcCommand(sql, conn2)
                            cmd.Parameters.AddWithValue("p1", nsocio)
                            cmd.Parameters.AddWithValue("p2", nombre)
                            cmd.Parameters.AddWithValue("p3", apellidos)
                            cmd.Parameters.AddWithValue("p4", dni)
                            cmd.Parameters.AddWithValue("p5", direcc)
                            cmd.Parameters.AddWithValue("p6", cp)
                            cmd.Parameters.AddWithValue("p7", localidad)
                            cmd.Parameters.AddWithValue("p8", provincia)
                            cmd.Parameters.AddWithValue("p9", pais)
                            cmd.Parameters.AddWithValue("p10", fechanac)
                            cmd.Parameters.AddWithValue("p11", email)
                            cmd.Parameters.AddWithValue("p12", tarjeta)
                            cmd.Parameters.AddWithValue("p13", tipo_socio)
                            cmd.Parameters.AddWithValue("p14", import)
                            cmd.Parameters.AddWithValue("p15", comentarios)
                            ' WHERE numero = existingNsocio
                            cmd.Parameters.AddWithValue("p16", existingNsocio)

                            Dim rows = cmd.ExecuteNonQuery()
                            If rows > 0 Then MsgBox("Socio modificado correctamente")
                        End Using
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
        Try
            ' Confirmación del usuario antes de borrar
            Dim respuesta = MsgBox("Va a eliminar al socio con DNI: " & dni & ". ¿Desea continuar?", vbYesNo + vbQuestion, "Confirmar eliminación")
            If respuesta <> vbYes Then Return

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

                    Dim tableName As String = normalizeTable(tabla_socios_xls)

                    ' ODBC: usar parámetros (?) en orden
                    Using cmd As New OdbcCommand("DELETE FROM " & tableName & " WHERE dni = ?", conn2)
                        cmd.Parameters.AddWithValue("p1", dni)
                        Dim deleted As Integer = cmd.ExecuteNonQuery()
                        MsgBox("Socio eliminado correctamente. Se han eliminado: " & deleted.ToString() & " registros.")
                    End Using

                Case tipobd.MySQL
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
                    Dim filePath As String = RutaFicheroBorrados()
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
