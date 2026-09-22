Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    ' Eventos de aplicación (My Project > Aplicación > Ver eventos de aplicación).
    Partial Friend Class MyApplication

        ''' <summary>
        ''' Al arrancar, fija la carpeta de recursos en %AppData%\CDB-PESCA-REINOSA\Resources.
        ''' Motivo: una vez instalada, la carpeta del programa (Archivos de programa) es de solo
        ''' lectura para el usuario y fallaría al escribir configuracion.txt o deleted_federativas.txt.
        ''' La primera vez se copian ahí los ficheros por defecto que acompañan al programa.
        ''' </summary>
        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            Try
                Dim userRes As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CDB-PESCA-REINOSA", "Resources")
                If Not Directory.Exists(userRes) Then Directory.CreateDirectory(userRes)

                ' Carpeta Resources que acompaña al programa (instalado: junto al .exe;
                ' en Visual Studio: se busca hacia arriba desde bin\Debug hasta la del proyecto).
                Dim appRes As String = Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources")
                If Not Directory.Exists(appRes) OrElse Directory.GetFiles(appRes).Length = 0 Then
                    Dim probe As String = System.Windows.Forms.Application.StartupPath
                    For i As Integer = 0 To 6
                        probe = Path.GetDirectoryName(probe)
                        If String.IsNullOrEmpty(probe) Then Exit For
                        Dim cand = Path.Combine(probe, "Resources")
                        If Directory.Exists(cand) AndAlso Directory.GetFiles(cand).Length > 0 Then
                            appRes = cand
                            Exit For
                        End If
                    Next
                End If

                ' Copiar solo los ficheros que falten: nunca se sobrescriben los del usuario
                ' (su configuración ni su papelera de federativas).
                If Directory.Exists(appRes) Then
                    For Each src In Directory.GetFiles(appRes)
                        Try
                            Dim nombre = Path.GetFileName(src)
                            If nombre.Equals("deleted_federativas.txt", StringComparison.OrdinalIgnoreCase) Then Continue For
                            If nombre.StartsWith("~$") Then Continue For ' temporales de Office
                            Dim dst = Path.Combine(userRes, nombre)
                            If Not File.Exists(dst) Then File.Copy(src, dst)
                        Catch
                        End Try
                    Next
                End If

                My.Settings.ruta_recursos = userRes
                Try
                    My.Settings.Save()
                Catch
                End Try
            Catch
                ' No impedir el arranque por un fallo aquí
            End Try
        End Sub

    End Class
End Namespace
