Imports System.IO
Imports System.Data.Odbc

Module ficheros
    Dim sr1 As StreamReader
    Dim sr2 As StreamReader
    Dim sr3 As StreamReader
    Public lista_localidades As New List(Of localidad)
    Public lista_provincias As New List(Of String)
    Public Sub leer()
        Try
            sr1 = New StreamReader(My.Settings.ruta_recursos + "/cantabria_localidades.txt")
            sr2 = New StreamReader(My.Settings.ruta_recursos + "/cantabria_cp.txt")
            sr3 = New StreamReader(My.Settings.ruta_recursos + "/provincias.txt")

            While Not sr1.EndOfStream
                Dim Loc As New localidad
                Loc.Nombre = sr1.ReadLine()
                Loc.CP = sr2.ReadLine()
                lista_localidades.Add(Loc)
            End While
            sr1.Close()
            sr2.Close()
            While Not sr3.EndOfStream
                lista_provincias.Add(sr3.ReadLine)
            End While
            sr3.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

End Module
