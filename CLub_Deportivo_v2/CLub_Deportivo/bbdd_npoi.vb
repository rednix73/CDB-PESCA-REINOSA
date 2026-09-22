Imports System.IO
Imports System.Reflection

Module bbdd_npoi

    ''' <summary>
    ''' Implementación de purga para .xlsx que carga las DLLs de NPOI dinámicamente
    ''' desde la carpeta "lib" de la aplicación para evitar forzar la resolución de
    ''' netstandard/NPOI cuando solo se necesita la rama .xls (Interop).
    ''' </summary>
    Public Sub PurgeDeletedFederativas_Xlsx(ruta_bd_excel As String, delFile As String, keys As System.Collections.Generic.HashSet(Of String), tabla_federa_xls As String)
        Dim deletedCount As Integer = 0
        Dim sheetName = tabla_federa_xls.Replace("[", "").Replace("]", "")
        If sheetName.EndsWith("$") Then sheetName = sheetName.TrimEnd("$"c)

        ' Intentar cargar las DLLs NPOI desde la carpeta lib
        Dim baseDir = AppDomain.CurrentDomain.BaseDirectory
        Dim libDir = Path.Combine(baseDir, "lib")
        Dim required = New String() {"NPOI.OOXML.dll", "NPOI.Core.dll", "NPOI.OpenXml4Net.dll", "NPOI.OpenXmlFormats.dll"}
        For Each d In required
            Dim p = Path.Combine(libDir, d)
            If Not File.Exists(p) Then
                MsgBox("Faltan dependencias NPOI: " & p & vbCrLf & "Coloque las DLLs en la carpeta 'lib' junto al ejecutable.")
                Return
            End If
            Try
                Assembly.LoadFrom(p)
            Catch ex As Exception
                MsgBox("No se pudo cargar la dependencia: " & p & vbCrLf & ex.Message)
                Return
            End Try
        Next

        Try
            ' Usar objetos late-bound para invocar NPOI sin referencias en tiempo de compilación
            Dim wb As Object = Nothing
            Using inFs As New FileStream(ruta_bd_excel, FileMode.Open, FileAccess.Read)
                ' Crear instancia de NPOI.XSSF.UserModel.XSSFWorkbook pasando el stream
                Dim ass = (From a In AppDomain.CurrentDomain.GetAssemblies() Where a.GetName().Name = "NPOI.OOXML" Select a).FirstOrDefault()
                If ass Is Nothing Then
                    MsgBox("No se encontró el ensamblado NPOI.OOXML cargado.")
                    Return
                End If
                Dim xwType = ass.GetType("NPOI.XSSF.UserModel.XSSFWorkbook")
                If xwType Is Nothing Then
                    MsgBox("Tipo XSSFWorkbook no encontrado en NPOI.OOXML.")
                    Return
                End If
                wb = Activator.CreateInstance(xwType, inFs)
            End Using

            Dim sheet As Object = Nothing
            If Not String.IsNullOrEmpty(sheetName) Then
                sheet = wb.GetSheet(sheetName)
            End If
            If sheet Is Nothing Then
                If wb.NumberOfSheets = 0 Then
                    MsgBox("No se encontró ninguna hoja en el libro Excel.")
                    Return
                Else
                    sheet = wb.GetSheetAt(0)
                End If
            End If

            Dim header As Object = sheet.GetRow(0)
            If header Is Nothing Then
                MsgBox("La hoja no contiene fila de encabezado.")
                Return
            End If

            Dim lastRow As Integer = sheet.LastRowNum
            If lastRow < 1 Then
                MsgBox("No hay datos para purgar.")
                Return
            End If

            Dim lastCol As Integer = header.LastCellNum - 1
            Dim nifCol As Integer = -1
            Dim numCol As Integer = -1
            For c As Integer = 0 To lastCol
                Dim cell = header.GetCell(c)
                Dim h As String = If(cell IsNot Nothing, cell.ToString().Trim().ToLower(), String.Empty)
                If nifCol = -1 AndAlso (h = "nif" OrElse h = "dni") Then nifCol = c
                If numCol = -1 AndAlso (h = "numero" OrElse h = "num") Then numCol = c
            Next

            For r As Integer = lastRow To 1 Step -1
                Dim row As Object = sheet.GetRow(r)
                If row Is Nothing Then Continue For
                Dim vNif As String = String.Empty
                Dim vNum As String = String.Empty
                Try
                    If nifCol >= 0 Then
                        Dim c1 = row.GetCell(nifCol)
                        If c1 IsNot Nothing Then vNif = c1.ToString().Trim()
                    End If
                    If numCol >= 0 Then
                        Dim c2 = row.GetCell(numCol)
                        If c2 IsNot Nothing Then vNum = c2.ToString().Trim()
                    End If
                Catch
                End Try
                If (Not String.IsNullOrEmpty(vNif) AndAlso keys.Contains(vNif)) OrElse (Not String.IsNullOrEmpty(vNum) AndAlso keys.Contains(vNum)) Then
                    sheet.ShiftRows(r + 1, sheet.LastRowNum, -1)
                    deletedCount += 1
                End If
            Next

            If deletedCount > 0 Then
                Using outFs As New FileStream(ruta_bd_excel, FileMode.Create, FileAccess.Write)
                    ' wb es un objeto late-bound; Write se invoca dinámicamente
                    wb.Write(outFs)
                End Using

                Dim remaining = New System.Collections.Generic.List(Of String)()
                For Each ln In File.ReadAllLines(delFile)
                    Dim s = ln.Trim()
                    If Not String.IsNullOrEmpty(s) AndAlso Not keys.Contains(s) Then remaining.Add(s)
                Next
                File.WriteAllLines(delFile, remaining.ToArray())
            End If

            MsgBox("Purgado completado. Registros eliminados físicamente: " & deletedCount.ToString())

        Catch ex As Exception
            MsgBox("Error al purgar .xlsx con NPOI: " & ex.Message)
        End Try
    End Sub

End Module
