Imports System.Data.Odbc
Imports MySql.Data.MySqlClient
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports OdbcConnection = System.Data.Odbc.OdbcConnection
Imports System.IO

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
    Public tabla_socios_xls As String ' = "[socios_2022_23$]"
    Public tabla_bdsocios_xls As String '= "[bdsocios$]"

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
    Public da_socios2 As OdbcDataAdapter
    Public cb_socios2 As OdbcCommandBuilder
    'Tabla Base de datos de socios excel
    Public da_bdsocios2 As OdbcDataAdapter
    Public cb_bdsocios2 As OdbcCommandBuilder

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

    'Dataset con ambas tablas
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
                da_socios2 = New OdbcDataAdapter("SELECT * FROM " & tabla_socios_xls & "", conn2)
                da_socios2.Fill(ds_club, "socios")
                cb_socios2 = New OdbcCommandBuilder(da_socios2)
                dw_socios = New DataView(ds_club.Tables(0))
                da_bdsocios2 = New OdbcDataAdapter("SELECT * FROM " + tabla_bdsocios_xls, conn2)
                da_bdsocios2.Fill(ds_club, "bdsocios")
                cb_bdsocios2 = New OdbcCommandBuilder(da_bdsocios2)
                dw_bdsocios = New DataView(ds_club.Tables(1))

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
                        libres2.Add(dr3(0))
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
            frm_busqueda.Show()
            frm_busqueda.DataGridView1.DataSource = dw_bdsocios
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
            If (valor <> "") Then
                dw_socios.RowFilter = "numero=" + valor
                dw_bdsocios.RowFilter = "numero=" + valor
                frm_busqueda.DataGridView1.DataSource = dw_socios
                frm_busqueda.Show()
                If (frm_busqueda.DataGridView1.Rows.Count = 1) Then
                    frm_busqueda.Close()
                    MsgBox("Número de socio no encontrado en temporada actual. Buscando en Base de datos...")
                    frm_busqueda.DataGridView1.DataSource = dw_bdsocios
                    frm_busqueda.Show()
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
                frm_busqueda.Show()
            Else
                MsgBox("Socio no encontrado en temporada actual. Buscando en la base de datos de socios...")
                dw_bdsocios.RowFilter = "dni like '%" + valor + "%'"
                frm_busqueda.DataGridView1.DataSource = dw_bdsocios
                If (frm_busqueda.DataGridView1.Rows.Count > 1) Then
                    frm_busqueda.Show()
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
                    sql_txt = "SELECT * FROM " + tabla_socios_xls + " WHERE NUMERO=" + nsocio
                    consulta2.CommandText = sql_txt
                    dr3 = consulta2.ExecuteReader
                    If (dr3.HasRows) Then
                        MsgBox("Número de socio no válido. Ya existe otro socio en la temporada actual con ese número asignado. Asigne otro número a este socio")
                        dr3.Close()
                    Else
                        dr3.Close()
                        'Comprobación de que no existe otro socio en la base de datos de socios con el mismo número de socio.
                        sql_txt = "SELECT * FROM " + tabla_socios_xls + " WHERE NUMERO=" + nsocio
                        consulta2.CommandText = sql_txt
                        dr3 = consulta2.ExecuteReader
                        If (dr3.HasRows) Then
                            MsgBox("Ya existe otro socio en la bbdd de socios con ese número asignado. Compruebe que sea el mismo socio.")
                        End If
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
    ''' Modifica un registro existente en la tabla socios.
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
    ''' <param name="comentarios"></param>
    Public Sub modificar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, import As String, comentarios As String)

        Try
            conectar()
            Select Case tp
                Case tipobd.MySQL
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    If (Not conn1.State = ConnectionState.Open) Then
                        conectar()
                    End If
                    'Comprobación de que no existe otro socio en la base de datos con el mismo número de socio.
                    'Se muestran los socios con el mismo número.
                    Dim sql_txt As String
                    sql_txt = "SELECT * FROM socios WHERE n_socio=" + nsocio
                    consulta1.CommandText = sql_txt
                    dr1 = consulta1.ExecuteReader
                    Dim n_reg As Integer = 0
                    Dim socios As String = ""
                    If (dr1.HasRows) Then
                        While (dr1.Read())
                            n_reg += 1
                            socios += dr1(0).ToString + vbCrLf
                        End While
                        dr1.Close()
                        If (n_reg > 1) Then
                            MsgBox("Ya existe otro/s socios con el mismo Número.Socios con el número: " + nsocio + ". " + vbCrLf + socios + vbCrLf + ". Cambie el número de socio asignado.")
                        End If
                    Else
                        dr1.Close()
                    End If

                    'Comprobación de que no existe otro socio en la base de datos con el mismo número de DNI.
                    Dim sql_txt2 As String
                    sql_txt2 = "SELECT * FROM socios WHERE dni='" + dni + "'"
                    consulta1.CommandText = sql_txt2
                    dr2 = consulta1.ExecuteReader
                    n_reg = 0
                    socios = ""
                    If (dr2.HasRows) Then
                        While (dr1.Read())
                            n_reg += 1
                            socios += dr1(0) + vbCrLf
                        End While
                        If (n_reg > 1) Then
                            MsgBox("Ya existe otro/s socios con el mismo DNI:" + vbCrLf + socios + vbCrLf + ". Compruebe que es correcto el DNI asignado, ó elimine duplicados.")
                        End If
                        MsgBox("Socio no válido. Ya existe otro socio en la base de datos con el mismo DNI.")
                        dr2.Close()
                    Else
                        dr2.Close()
                    End If

                    'Consulta de modificacion.
                    Dim sql_txt3 As String = "UPDATE socios SET n_socio=" + nsocio + ", nombre='" + nombre + "', apellidos='" + apellidos +
                            "', dni='" + dni + "', direccion='" + direcc + "', cp='" + cp + "', localidad='" + localidad + "', provincia='" + provincia + "', pais='" + pais +
                            "', fechanac='" + fechanac + "', email='" + email + "', tarjeta=" + tarjeta + ", tipo_socio='" + tipo_socio + "', comentarios='" + comentarios + "'" +
                        "WHERE dni=" + dni

                    consulta1.CommandText = sql_txt3

                    Dim salida3 As Integer = consulta1.ExecuteNonQuery
                    If (salida3 > 0) Then
                        MsgBox("Socio modificado correctamente")
                    End If

                Case tipobd.Excel_ODBC

                    consulta2 = New OdbcCommand()
                    consulta2.Connection = conn2
                    If (Not conn1.State = ConnectionState.Open) Then
                        conectar()
                    End If
                    'Comprobación de que no existe otro socio en la base de datos con el mismo número de socio.
                    'Se muestran los socios con el mismo número.
                    Dim sql_txt As String
                    sql_txt = "SELECT * FROM " + tabla_socios_xls + " WHERE NUMERO=" + nsocio
                    consulta2.CommandText = sql_txt
                    dr3 = consulta2.ExecuteReader
                    Dim n_reg As Integer = 0
                    Dim socios As String = ""
                    If (dr3.HasRows) Then
                        While (dr3.Read())
                            n_reg += 1
                            socios += dr3(0).ToString + vbCrLf
                        End While
                        dr3.Close()
                        If (n_reg > 1) Then
                            MsgBox("Ya existe otro/s socios con el mismo Número.Socios con el número: " + nsocio + ". " + vbCrLf + socios + vbCrLf + ". Cambie el número de socio asignado.")
                        End If
                    Else
                        dr3.Close()
                    End If

                    'Comprobación de que no existe otro socio en la base de datos con el mismo número de DNI.
                    Dim sql_txt2 As String
                    sql_txt2 = "SELECT * FROM " + tabla_socios_xls + " WHERE dni='" + dni + "'"
                    consulta2.CommandText = sql_txt2
                    dr4 = consulta2.ExecuteReader
                    n_reg = 0
                    socios = ""
                    If (dr4.HasRows) Then
                        While (dr4.Read())
                            n_reg += 1
                            socios += dr4(0).ToString + vbCrLf
                        End While
                        If (n_reg > 1) Then
                            MsgBox("Ya existe otro/s socios con el mismo DNI:" + vbCrLf + socios + vbCrLf + ". Compruebe que es correcto el DNI asignado, ó elimine duplicados.")
                        End If
                        MsgBox("Socio no válido. Ya existe otro socio en la base de datos con el mismo DNI.")
                        dr4.Close()
                    Else
                        dr4.Close()
                    End If

                    'Consulta de modificacion.
                    Dim sql_txt3 As String = "UPDATE " + tabla_socios_xls + " SET numero=" + nsocio + ", nombre='" + nombre + "', apellidos='" + apellidos +
                            "', dni='" + dni + "', direccion='" + direcc + "', cp='" + cp + "', localidad='" + localidad + "', provincia='" + provincia + "', pais='" + pais +
                            "', fechanac='" + fechanac + "', email='" + email + "', tarjeta=" + tarjeta + ", tipo_socio='" + tipo_socio + "', importe=" + import + ", comentarios='" + comentarios + "'" +
                        " WHERE dni='" + dni + "'"

                    consulta2.CommandText = sql_txt3

                    Dim salida3 As Integer = consulta2.ExecuteNonQuery
                    If (salida3 > 0) Then
                        MsgBox("Socio modificado correctamente")
                    End If
                Case Else
            End Select

            desconectar()

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub
    ''' <summary>
    ''' Elimina un registro existente en la tabla socios.
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
    ''' <param name="pago"></param>
    ''' <param name="comentarios"></param>
    Public Sub eliminar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, pago As String, comentarios As String)

        Try
            conectar()

            Select Case tp
                Case tipobd.Excel_ODBC
                    consulta2 = New OdbcCommand()
                    consulta2.Connection = conn2
                    If (Not conn2.State = ConnectionState.Open) Then
                        conectar()
                    End If

                    'Consulta de eliminacion.
                    Dim sql_txt3 As String = "DELETE * FROM " + tabla_socios_xls + " WHERE dni='" + dni + "'"

                    consulta2.CommandText = sql_txt3
                    MsgBox("Socio eliminado correctamente. Se han eliminado: " + consulta2.ExecuteNonQuery.ToString + " registros.")

                Case tipobd.MySQL
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    If (Not conn1.State = ConnectionState.Open) Then
                        conectar()
                    End If

                    'Consulta de eliminacion.
                    Dim sql_txt3 As String = "DELETE FROM socios WHERE dni='" + dni + "'"

                    consulta1.CommandText = sql_txt3
                    MsgBox("Socio eliminado correctamente. Se han eliminado: " + consulta1.ExecuteNonQuery.ToString + " registros.")
            End Select

            desconectar()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub





End Module
