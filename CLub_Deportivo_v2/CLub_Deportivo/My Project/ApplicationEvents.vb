Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My

    ' Para agregar controladores de eventos de aplicación, abra el diseñador de My Project,
    ' haga clic en la pestaña "Application" y pulse el botón "View Application Events".
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            Try
                Dim userRes = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CDB-PESCA-REINOSA", "Resources")
                Dim appRes As String = Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources")

                ' Si la carpeta Resources no existe en Application.StartupPath, buscar hacia arriba (entorno de desarrollo)
                If Not Directory.Exists(appRes) Then
                    Dim probe As String = System.Windows.Forms.Application.StartupPath
                    For i As Integer = 0 To 6
                        probe = Path.GetDirectoryName(probe)
                        If String.IsNullOrEmpty(probe) Then Exit For
                        Dim cand = Path.Combine(probe, "Resources")
                        If Directory.Exists(cand) Then
                            appRes = cand
                            Exit For
                        End If
                    Next
                End If

                ' Crear carpeta de usuario y copiar ficheros por defecto si faltan
                If Not Directory.Exists(userRes) Then
                    Directory.CreateDirectory(userRes)
                End If

                If Directory.Exists(appRes) Then
                    For Each src In Directory.GetFiles(appRes)
                        Try
                            Dim dst = Path.Combine(userRes, Path.GetFileName(src))
                            If Not File.Exists(dst) Then
                                File.Copy(src, dst, True)
                            End If
                        Catch
                            ' Ignorar errores por archivo
                        End Try
                    Next
                End If

                ' Normalizar y guardar ruta absoluta en settings
                Dim settingRes As String = My.Settings.ruta_recursos
                Dim finalRes As String = userRes
                If Not String.IsNullOrWhiteSpace(settingRes) Then
                    If Path.IsPathRooted(settingRes) Then
                        ' Si es absoluta y existe, usarla; si no existe, usar userRes
                        If Directory.Exists(settingRes) Then
                            finalRes = settingRes
                        Else
                            finalRes = userRes
                        End If
                    Else
                        ' Ruta relativa: preferir la carpeta de usuario (AppData). Si existe la relativa dentro del ejecutable,
                        ' usarla, en caso contrario usar userRes para evitar apuntar a Program Files u otra carpeta inexistente.
                        Try
                            Dim candidate As String = Path.GetFullPath(Path.Combine(System.Windows.Forms.Application.StartupPath, settingRes))
                            If Directory.Exists(candidate) Then
                                finalRes = candidate
                            Else
                                finalRes = userRes
                            End If
                        Catch
                            finalRes = userRes
                        End Try
                    End If
                End If

                My.Settings.ruta_recursos = finalRes
                Try
                    My.Settings.Save()
                Catch
                End Try
                ' Mensaje de depuración eliminado: la ruta finalRes se guarda en My.Settings.ruta_recursos

            Catch ex As Exception
                ' No detener inicio de la aplicación por errores de migración, pero registrar si se necesita
            End Try
        End Sub

    End Class
End Namespace
