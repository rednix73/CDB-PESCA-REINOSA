Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

Module bbdd
    Public cadena As String = "Server=31.220.20.207;Port=3306;Database=u127917223_socio;Uid=u127917223_redn;Pwd=EaHb8Hx2nThGhNCw"
    Public conn As New MySqlConnection
    Public consulta As New MySqlCommand
    Public dr As MySqlDataReader
    Public dr2 As MySqlDataReader

    'Tabla socios
    Public da_socios As MySqlDataAdapter
    Public cb_socios As MySqlCommandBuilder
    Public dw_socios As DataView

    'Tabla Base de datos de socios
    Public da_bdsocios As MySqlDataAdapter
    Public cb_bdsocios As MySqlCommandBuilder
    Public dw_bdsocios As DataView

    'Dataset con ambas tablas.
    Public ds_club As New DataSet

    Public Sub conectar()
        Try
            conn.ConnectionString = cadena
            conn.Open()
            If Not conn.State = ConnectionState.Open Then
                MsgBox("Error de conexion")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub desconectar()
        Try
            conn.Close()
            If conn.State = ConnectionState.Open Then
                MsgBox("Error de desconexión")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub cargar()
        conectar()
        da_socios = New MySqlDataAdapter("Select * from socios", conn)
        da_socios.Fill(ds_club, "socios")
        cb_socios = New MySqlCommandBuilder(da_socios)
        dw_socios = New DataView(ds_club.Tables(0))
        da_bdsocios = New MySqlDataAdapter("Select * from bdsocios", conn)
        da_bdsocios.Fill(ds_club, "bdsocios")
        cb_bdsocios = New MySqlCommandBuilder(da_bdsocios)
        dw_bdsocios = New DataView(ds_club.Tables(1))
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
        libres.Clear()
        libres1.Clear()
        libres2.Clear()

        consulta = New MySqlCommand()
        consulta.Connection = conn
        'Obtenemos el último número usado en la base de datos de socios.
        consulta.CommandText = "Select MAX(n_socio) from bdsocios"
        Dim ultimo_bd As Integer = consulta.ExecuteScalar
        consulta = New MySqlCommand()
        consulta.Connection = conn
        'Obtenemos el último número usado en la tabla de socios actual.
        consulta.CommandText = "Select MAX(n_socio) from socios"
        Dim ultimo_soc As Integer = consulta.ExecuteScalar
        consulta = New MySqlCommand()
        consulta.Connection = conn
        'Obtenemos todos los numeros usados en la tabla bd_socios (base de datos de socios).
        consulta.CommandText = "Select n_socio from bdsocios"

        Dim libre As Boolean = True
        'Recorremos todos los números desde el 1 hasta el último numero usado, y añadimos a la lista libres1 los numeros no usados en la tbal bdsocios.
        For index = 1 To ultimo_bd
            dr = consulta.ExecuteReader()
            While dr.Read
                If index = dr(0) Then
                    libre = False
                End If
            End While
            dr.Close()

            If libre = True Then
                libres1.Add(index)
            End If
            libre = True
        Next

        consulta = New MySqlCommand()
        consulta.Connection = conn
        'Obtenemos todos los numeros usados en la tabla socios (base de datos de socios de la temporada actual) y los añadimos a la lista libres2.
        consulta.CommandText = "Select n_socio from socios"

        dr = consulta.ExecuteReader()
        While dr.Read
            libres2.Add(dr(0))
        End While
        dr.Close()

        'Recorremos la lista de numeros libres de la tabla bdsocios, y eliminamos de la lista de numeros libre los que estén usados en la tabla socios de la temporada actual.
        For Each n2 In libres2
            For i = (libres1.Count - 1) To 0 Step -1
                If (libres1(i).Equals(n2)) Then
                    libres1.RemoveAt(i)
                End If
            Next
        Next



        'Devolvemos la lista de numeros libres de la tabal bdsocios, eliminados los usados en la tabla socios.
        libres = libres1

        Return libres

    End Function

    ''' <summary>
    ''' Método que devuelve el último número de socio usado.
    ''' </summary>
    ''' <returns>Develve un entero con el último número de socio en uso.</returns>
    Public Function ultimo(tabla As String) As Integer

        consulta = New MySqlCommand()
        consulta.Connection = conn
        'Obtenemos el último número usado.
        consulta.CommandText = "Select MAX(n_socio) from " + tabla
        Dim ult As Integer = consulta.ExecuteScalar

        Return ult

    End Function

    Public Sub buscar_nombre(valor As String)

        dw_bdsocios.RowFilter = "nombre Like '%" + valor + "%'"
        frm_busqueda.Show()
        frm_busqueda.DataGridView1.DataSource = dw_bdsocios


    End Sub

    Public Sub buscar_nsocio(valor As String)
        If (valor <> "") Then
            dw_bdsocios.RowFilter = "n_socio=" + valor
        End If

        frm_busqueda.Show()
        frm_busqueda.DataGridView1.DataSource = dw_bdsocios


    End Sub

    Public Sub buscar_dni(valor As String)
        dw_bdsocios.RowFilter = "dni like '%" + valor + "%'"
        frm_busqueda.Show()
        frm_busqueda.DataGridView1.DataSource = dw_bdsocios
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
            consulta = New MySqlCommand()
            consulta.Connection = conn
            If (Not conn.State = ConnectionState.Open) Then
                conectar()
            End If
            'Comprobación de que no existe otro socio en la base de datos con el mismo número de socio.
            Dim sql_txt As String
            sql_txt = "SELECT * FROM socios WHERE n_socio=" + nsocio
            consulta.CommandText = sql_txt
            dr = consulta.ExecuteReader
            If (dr.HasRows) Then
                MsgBox("Número de socio no válido. Ya existe otro socio con ese número asignado. Asigne otro número a este socio")
                dr.Close()
            Else
                dr.Close()
                'Comprobación de que no existe otro socio en la base de datos con el mismo número de DNI.
                Dim sql_txt2 As String
                sql_txt2 = "SELECT * FROM socios WHERE dni='" + dni + "'"
                consulta.CommandText = sql_txt2

                dr2 = consulta.ExecuteReader
                If (dr2.HasRows) Then
                    MsgBox("Socio no válido. Ya existe otro socio en la base de datos con el mismo DNI.")
                    dr2.Close()

                Else
                    dr2.Close()


                    'Consulta de insercion.
                    Dim sql_txt3 As String = "INSERT INTO socios(n_socio,nombre,apellidos,dni,direccion,cp,localidad,provincia,pais,fechanac,email,tarjeta, tipo_socio,pago,comentarios) 
VALUES(" + nsocio + ",'" + nombre + "','" + apellidos + "','" + dni + "','" + direcc + "'," + cp + ", '" + localidad + "','" + provincia + "','" + pais + "','" + fechanac + "','" + email + "'," + tarjeta + ",'" + tipo_socio + "'," + pago + ",'" + comentarios + "')"

                    consulta.CommandText = sql_txt3


                    Dim salida3 As Integer = consulta.ExecuteNonQuery
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
            consulta = New MySqlCommand()
            consulta.Connection = conn
            If (Not conn.State = ConnectionState.Open) Then
                conectar()
            End If
            'Comprobación de que no existe otro socio en la base de datos con el mismo número de socio.
            'Se muestran los socios con el mismo número.
            Dim sql_txt As String
            sql_txt = "SELECT * FROM socios WHERE n_socio=" + nsocio
            consulta.CommandText = sql_txt
            dr = consulta.ExecuteReader
            Dim n_reg As Integer = 0
            Dim socios As String = ""
            If (dr.HasRows) Then
                While (dr.Read())
                    n_reg += 1
                    socios += dr(0).ToString + vbCrLf
                End While
                dr.Close()
                If (n_reg > 1) Then
                    MsgBox("Ya existe otro/s socios con el mismo Número.Socios con el número: " + nsocio + ". " + vbCrLf + socios + vbCrLf + ". Cambie el número de socio asignado.")
                End If
            Else
                dr.Close()
            End If

            'Comprobación de que no existe otro socio en la base de datos con el mismo número de DNI.
            Dim sql_txt2 As String
            sql_txt2 = "SELECT * FROM socios WHERE dni='" + dni + "'"
            consulta.CommandText = sql_txt2
            dr2 = consulta.ExecuteReader
            n_reg = 0
            socios = ""
            If (dr2.HasRows) Then
                While (dr.Read())
                    n_reg += 1
                    socios += dr(0) + vbCrLf
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

            consulta.CommandText = sql_txt3


            Dim salida3 As Integer = consulta.ExecuteNonQuery
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
            consulta = New MySqlCommand()
            consulta.Connection = conn
            If (Not conn.State = ConnectionState.Open) Then
                conectar()
            End If

            'Consulta de eliminacion.
            Dim sql_txt3 As String = "DELETE FROM socios WHERE dni='" + dni + "'"

            consulta.CommandText = sql_txt3
            MsgBox("Socio eliminado correctamente. Se han eliminado: " + consulta.ExecuteNonQuery.ToString + " registros.")

            desconectar()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub





End Module
