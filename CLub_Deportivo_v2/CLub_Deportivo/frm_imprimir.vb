Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing

Public Class frm_imprimir

    Private printFont As Font
    Private streamToPrint As StreamReader

    ''' <summary>
    ''' Método que establece el fondo de la tarjeta de socio (anverso o reverso) en función del radio button que esté seleccionado.
    ''' </summary>
    Private Sub establece_fondo()
        Try
            If rdo_anverso.Checked Then
                Mapa_tarjetas2.Imagen_Fondo_tarjeta = My.Settings.ruta_recursos & "\" & bbdd.tarjeta_socio_anverso
                Mapa_tarjetas2.Establece_fondo()
            End If
            If rdo_reverso.Checked Then
                Mapa_tarjetas2.Imagen_Fondo_tarjeta = My.Settings.ruta_recursos & "\" & bbdd.tarjeta_socio_reverso
                Mapa_tarjetas2.Establece_fondo()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub dibuja_tarjeta(ntar As Integer, ep As PrintPageEventArgs)
        Try
            Dim fuente As New Font("Arial", 11, FontStyle.Regular)

            Dim starty As Integer = ep.MarginBounds.Top + 60
            Dim startx As Integer = 50
            Dim espaciadoh As Integer = 20
            Dim espaciadov As Integer = 20
            Dim x As Integer = startx
            Dim y As Integer = starty
            Dim ancho_tarjeta As Integer = 350
            Dim alto_tarjeta As Integer = 175

            If (ntar Mod 2 = 0) Then
                x = startx + ancho_tarjeta + espaciadoh
            Else
                x = startx
            End If
            Select Case ntar
                Case 1, 2
                    y = starty
                Case 3, 4
                    y = starty + alto_tarjeta + espaciadov
                Case 5, 6
                    y = starty + alto_tarjeta * 2 + espaciadov * 2
                Case 7, 8
                    y = starty + alto_tarjeta * 3 + espaciadov * 3
                Case 9, 10
                    y = starty + alto_tarjeta * 4 + espaciadov * 4
            End Select

            ep.Graphics.DrawString("NOMBRE: " + frm_socio.txt_nombre.Text + " " + frm_socio.txt_apellido.Text, fuente, Brushes.Black, x, y)
            ep.Graphics.DrawString("Nº SOCIO: " + frm_socio.txt_nsocio.Text, fuente, Brushes.Black, x, y + 20)
            ep.Graphics.DrawString("D.N.I.: " + frm_socio.txt_dni.Text, fuente, Brushes.Black, x + 160, y + 20)
            If (frm_socio.cmb_tarjeta.SelectedItem = "SALMON") Then
                ep.Graphics.DrawString(frm_socio.cmb_tarjeta.SelectedItem.ToString(), fuente, Brushes.Black, x, y + 40)
                ep.Graphics.DrawString("TRUCHA", fuente, Brushes.Black, x, y + 60)
            Else
                ep.Graphics.DrawString(frm_socio.cmb_tarjeta.SelectedItem.ToString(), fuente, Brushes.Black, x, y + 40)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub


    Private Sub dibuja_fondo_tarjeta(ep As PrintPageEventArgs)
        Try
            Dim starty As Integer = ep.MarginBounds.Top
            Dim startx As Integer = 50
            Dim espaciadoh As Integer = 20
            Dim espaciadov As Integer = 20
            Dim x As Integer = startx
            Dim y As Integer = starty
            Dim ancho_tarjeta As Integer = 350
            Dim alto_tarjeta As Integer = 175
            Dim rectang As New Rectangle(x, y, ancho_tarjeta, alto_tarjeta)

            For i = 1 To 10
                If (i Mod 2 = 0) Then
                    x = startx + ancho_tarjeta + espaciadoh
                Else
                    x = startx
                End If

                Select Case i
                    Case 1, 2
                        y = starty
                    Case 3, 4
                        y = starty + alto_tarjeta + espaciadov
                    Case 5, 6
                        y = starty + alto_tarjeta * 2 + espaciadov * 2
                    Case 7, 8
                        y = starty + alto_tarjeta * 3 + espaciadov * 3
                    Case 9, 10
                        y = starty + alto_tarjeta * 4 + espaciadov * 4
                End Select
                rectang.X = x
                rectang.Y = y
                rectang.Width = ancho_tarjeta
                rectang.Height = alto_tarjeta
                ep.Graphics.DrawImage(Image.FromFile(Mapa_tarjetas2.Imagen_Fondo_tarjeta), rectang)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub frm_imprimir2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        establece_fondo()
    End Sub

    Private Sub rdo_anverso_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_anverso.CheckedChanged
        establece_fondo()
    End Sub

    Private Sub rdo_reverso_CheckedChanged(sender As Object, e As EventArgs) Handles rdo_reverso.CheckedChanged
        establece_fondo()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        dibuja_tarjeta(Mapa_tarjetas1.tarjeta, e)
    End Sub

    Private Sub PrintDocument2_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument2.PrintPage
        dibuja_fondo_tarjeta(e)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub btn_imprimir_fondo_Click(sender As Object, e As EventArgs) Handles btn_imprimir_fondo.Click
        If (PrintDialog1.ShowDialog() = DialogResult.OK) Then
            PrintPreviewDialog1.Document = PrintDocument2
            PrintPreviewDialog1.WindowState = FormWindowState.Maximized
            PrintPreviewDialog1.ShowDialog()
        End If

        'If (PrintDialog1.ShowDialog() = DialogResult.OK) Then
        '    PrintForm1.PrintAction = PrintAction.PrintToPreview
        '    Mapa_tarjetas2.Enabled = True
        '    PrintForm1.Print()
        'End If
    End Sub

    Private Sub btn_imprimir_tarjeta_Click(sender As Object, e As EventArgs) Handles btn_imprimir_tarjeta.Click
        If (PrintDialog1.ShowDialog() = DialogResult.OK) Then
            PrintPreviewDialog1.Document = PrintDocument1
            PrintPreviewDialog1.WindowState = FormWindowState.Maximized
            PrintPreviewDialog1.ShowDialog()
        End If

        'PrintForm1.Print()

        ' streamToPrint = New StreamReader("C:\\My Documents\\MyFile.txt")
        'Try

        '    printFont = New Font("Arial", 10)
        '    Dim pd As New PrintDocument

        '    pd.PrintPage += New PrintPageEventHandler
        '           (this.pd_PrintPage)
        '        pd.Print()
        '    }
        '    Finally
        '    {
        '        streamToPrint.Close()
        '    }
        '}
        'Catch (Exception ex)
        '{
        '    MessageBox.Show(ex.Message)
        '}
        'End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click
        establece_fondo()
    End Sub
End Class