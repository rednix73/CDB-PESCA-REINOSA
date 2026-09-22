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
            Dim baseRes As String = My.Settings.ruta_recursos
            Dim file1 As String = Path.Combine(baseRes, "cantabria_localidades.txt")
            Dim file2 As String = Path.Combine(baseRes, "cantabria_cp.txt")
            Dim file3 As String = Path.Combine(baseRes, "provincias.txt")

            ' Si no existen en la ruta configurada, probar la carpeta Resources junto al ejecutable
            Dim appRes As String = Path.Combine(Application.StartupPath, "Resources")
            If Not File.Exists(file1) Then
                Dim alt = Path.Combine(appRes, "cantabria_localidades.txt")
                If File.Exists(alt) Then file1 = alt
            End If
            If Not File.Exists(file2) Then
                Dim alt = Path.Combine(appRes, "cantabria_cp.txt")
                If File.Exists(alt) Then file2 = alt
            End If
            If Not File.Exists(file3) Then
                Dim alt = Path.Combine(appRes, "provincias.txt")
                If File.Exists(alt) Then file3 = alt
            End If

            ' Abrir solo si existen, si no, lanzar excepción para informar
            If Not File.Exists(file1) Or Not File.Exists(file2) Or Not File.Exists(file3) Then
                Throw New FileNotFoundException("Faltan archivos de recursos en '" & baseRes & "' y en '" & appRes & "'.")
            End If

            sr1 = New StreamReader(file1)
            sr2 = New StreamReader(file2)
            sr3 = New StreamReader(file3)

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
