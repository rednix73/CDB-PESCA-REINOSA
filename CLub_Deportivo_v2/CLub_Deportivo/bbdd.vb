Imports System.Data.Odbc
Imports MySql.Data.MySqlClient
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports OdbcConnection = System.Data.Odbc.OdbcConnection

Module bbdd
    Public temporada As String = "2023"
    Public precio_salmon As Integer = 16
    Public precio_trucha As Integer = 10

    'Tipo de origen de datos
    Public Enum tipobd
        excel
        mysql
    End Enum
    Public tp As tipobd

    ' Base de datos remota mysql
    Public cadena1 As String = "Server=153.92.7.1;Port=3306;Database=u127917223_socio;Uid=u127917223_redn;Pwd=EaHb8Hx2nThGhNCw"
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
    Public cadena2 As String = "DSN=cdb-pesca-xls"
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
    Public cadena3 As String = "Provider=Microsoft.ACE.OLEDB.12.0;" & "Data source=" & Archivo_excel & ";" & "Extended Properties=Excel 8.0;HDR=Yes"
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
    'Nombre del archivo excel
    Public Archivo_excel As String = "C:\Users\roberto\Documents\tarjetas_socio_2023.xls"
    'Nombre de la hoja excel donde están los datos
    Public Hoja_excel_socios As String = "[socios_2022_2023$]"

    Public dw_socios As DataView
    Public dw_bdsocios As New DataView

    'Dataset con ambas tablas
    Public ds_club As New DataSet

    Public Sub conectar()
        Try
            tp = tipobd.excel
            Select Case tp
                Case tipobd.excel
                    conn2 = New OdbcConnection
                    conn2.ConnectionString = cadena2
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
                Case tipobd.mysql
                    conn1 = New MySqlConnection
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
            If conn3.State = ConnectionState.Open Then
                MsgBox("Error de desconexión")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub cargar()
        conectar()
        Select Case tp
            Case tipobd.excel

                'Conexión por ODBC
                conectar()
                da_socios2 = New OdbcDataAdapter()
                da_socios2 = New OdbcDataAdapter("SELECT * FROM [socios_2022_23$]", conn2)
                da_socios2.Fill(ds_club, "socios")
                cb_socios2 = New OdbcCommandBuilder(da_socios2)
                dw_socios = New DataView(ds_club.Tables(0))
                da_bdsocios2 = New OdbcDataAdapter("SELECT * FROM  [bdsocios$]", conn2)
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


            Case tipobd.mysql
                conectar()
                da_socios1 = New MySqlDataAdapter("Select * from socios", conn1)
                da_socios1.Fill(ds_club, "socios")
                cb_socios1 = New MySqlCommandBuilder(da_socios1)
                dw_socios = New DataView(ds_club.Tables(0))
                da_bdsocios1 = New MySqlDataAdapter("Select * from bdsocios", conn1)
                da_bdsocios1.Fill(ds_club, "bdsocios")
                cb_bdsocios1 = New MySqlCommandBuilder(da_bdsocios1)
                dw_bdsocios = New DataView(ds_club.Tables(1))
            Case Else
        End Select


        desconectar()
    End Sub

    ''' <summary>
    ''' Método que devuelve una lista con los números de socio no utilizados.
    ''' </summary>
    ''' <returns>
    ''' Devuelve una lista de enteros, con los números de socio no usados.
    ''' </returns>
    Public Function numeros_libres() As List(Of Integer)
        Dim libres As New List(Of Integer)
        Dim libres1 As New List(Of Integer)
        Dim libres2 As New List(Of Integer)
        Dim libre As Boolean = True
        libres.Clear()
        libres1.Clear()
        libres2.Clear()

        Dim ultimo_bd As Integer
        Select Case tp
            Case tipobd.excel
                consulta2 = New OdbcCommand()
                consulta2.Connection = conn2
                'Obtenemos el último número usado en la base de datos de socios.
                consulta2.CommandText = "Select MAX(numero) from [bdsocios$]"
                ultimo_bd = consulta2.ExecuteScalar()
                consulta2 = New OdbcCommand()
                consulta2.Connection = conn2
                'Obtenemos el último número usado en la tabla de socios actual.
                consulta2.CommandText = "Select MAX(numero) from [socios_2022_23$]"
                Dim ultimo_soc As Integer = consulta2.ExecuteScalar()
                consulta2 = New OdbcCommand()
                consulta2.Connection = conn2
                'Obtenemos todos los numeros usados en la tabla bd_socios (base de datos de socios).
                consulta2.CommandText = "Select numero from [bdsocios$]"

                'Recorremos todos los números desde el 1 hasta el último numero usado, y añadimos a la lista libres1 los numeros no usados en la tabla bdsocios.
                For index = 1 To ultimo_bd
                    dr3 = consulta2.ExecuteReader()
                    While dr3.Read
                        If index = dr3(0) Then
                            libre = False
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
                consulta2.CommandText = "Select numero from [socios_2022_23$]"

                dr3 = consulta2.ExecuteReader()
                While dr3.Read
                    libres2.Add(dr3(0))
                End While
                dr3.Close()

            Case tipobd.mysql
                consulta1 = New MySqlCommand()
                consulta1.Connection = conn1
                'Obtenemos el último número usado en la base de datos de socios.
                consulta1.CommandText = "Select MAX(n_socio) from bdsocios"
                ultimo_bd = consulta1.ExecuteScalar
                consulta1 = New MySqlCommand()
                consulta1.Connection = conn1
                'Obtenemos el último número usado en la tabla de socios actual.
                consulta1.CommandText = "Select MAX(n_socio) from socios"
                Dim ultimo_soc As Integer = consulta1.ExecuteScalar
                consulta1 = New MySqlCommand()
                consulta1.Connection = conn1
                'Obtenemos todos los numeros usados en la tabla bd_socios (base de datos de socios).
                consulta1.CommandText = "Select n_socio from bdsocios"
                'Recorremos todos los números desde el 1 hasta el último numero usado, y añadimos a la lista libres1 los numeros no usados en la tabla bdsocios.
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
                consulta1.CommandText = "Select n_socio from socios"
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
        Return libres
    End Function
    ''' <summary>
    ''' Método que devuelve el último número de socio usado.
    ''' </summary>
    ''' <returns>Develve un entero con el último número de socio en uso.</returns>
    Public Function ultimo(tabla As String) As Integer
        Try
            Dim ult As Integer
            Select Case tp
                Case tipobd.excel
                    consulta2 = New OdbcCommand()
                    consulta2.Connection = conn2
                    'Obtenemos el último número usado.
                    consulta2.CommandText = "Select MAX(n_socio) from " + tabla
                    ult = consulta2.ExecuteScalar()

                Case tipobd.mysql
                    consulta1 = New MySqlCommand()
                    consulta1.Connection = conn1
                    'Obtenemos el último número usado.
                    consulta1.CommandText = "Select MAX(n_socio) from " + tabla
                    ult = consulta1.ExecuteScalar()
            End Select
            Return ult
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Function

    Public Sub buscar_nombre(valor As String)
        Try
            dw_bdsocios.RowFilter = "apellidos Like '%" + valor + "%'"
            frm_busqueda.Show()
            frm_busqueda.DataGridView1.DataSource = dw_bdsocios
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Public Sub buscar_nsocio(valor As String)
        Try
            If (valor <> "") Then
                dw_bdsocios.RowFilter = "numero=" + valor
            End If

            frm_busqueda.Show()
            frm_busqueda.DataGridView1.DataSource = dw_bdsocios
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

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
    ''' Inserta un registro en la tabla socios.
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
    Public Sub insertar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, pago As String, comentarios As String)

        Try
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
                    Dim sql_txt3 As String = "INSERT INTO socios(n_socio,nombre,apellidos,dni,direccion,cp,localidad,provincia,pais,fechanac,email,tarjeta, tipo_socio,pago,comentarios) 
VALUES(" + nsocio + ",'" + nombre + "','" + apellidos + "','" + dni + "','" + direcc + "'," + cp + ", '" + localidad + "','" + provincia + "','" + pais + "','" + fechanac + "','" + email + "'," + tarjeta + ",'" + tipo_socio + "'," + pago + ",'" + comentarios + "')"

                    consulta1.CommandText = sql_txt3


                    Dim salida3 As Integer = consulta1.ExecuteNonQuery
                    If (salida3 > 0) Then
                        MsgBox("Socio insertado correctamente")
                    End If

                End If
            End If


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
    ''' <param name="pago"></param>
    ''' <param name="comentarios"></param>
    Public Sub modificar_socio(nsocio As String, nombre As String, apellidos As String, dni As String, direcc As String, cp As String, localidad As String, provincia As String, pais As String, fechanac As String, email As String, tarjeta As String, tipo_socio As String, pago As String, comentarios As String)

        Try
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
                    "', fechanac='" + fechanac + "', email='" + email + "', tarjeta=" + tarjeta + ", tipo_socio='" + tipo_socio + "', pago" + pago + ", comentarios='" + comentarios + "'" +
                "WHERE dni=" + dni

            consulta1.CommandText = sql_txt3


            Dim salida3 As Integer = consulta1.ExecuteNonQuery
            If (salida3 > 0) Then
                MsgBox("Socio modificado correctamente")
            End If
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
            consulta1 = New MySqlCommand()
            consulta1.Connection = conn1
            If (Not conn1.State = ConnectionState.Open) Then
                conectar()
            End If

            'Consulta de eliminacion.
            Dim sql_txt3 As String = "DELETE FROM socios WHERE dni='" + dni + "'"

            consulta1.CommandText = sql_txt3
            MsgBox("Socio eliminado correctamente. Se han eliminado: " + consulta1.ExecuteNonQuery.ToString + " registros.")

            desconectar()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub





End Module
