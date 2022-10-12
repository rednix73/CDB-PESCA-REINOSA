Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing



Public Class frm_imprimir

    Private printFont As Font
    Private streamToPrint As StreamReader


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_imprimir.Click

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

    Private Sub frm_imprimir_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub dibuja_tarjeta(ntar As Integer, ep As PrintPageEventArgs)
        Try
            Dim fuente As New Font("Arial", 11, FontStyle.Regular)
            Dim starty As Integer = ep.MarginBounds.Top
            Dim x As Integer
            Dim y As Integer = 0
            Dim espaciado As Integer = 150
            If (ntar Mod 2 = 0) Then
                x = 350
            Else
                x = 20
            End If
            Select Case ntar
                Case 1, 2
                    y = starty
                Case 3, 4
                    y = starty + 40 + espaciado
                Case 5, 6
                    y = starty + 40 + espaciado * 2
                Case 7, 8
                    y = starty + 40 + espaciado * 3
                Case 9, 10
                    y = starty + 40 + espaciado * 4
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

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage

        dibuja_tarjeta(Mapa_tarjetas1.tarjeta, e)





    End Sub

    Private Sub PrintPreviewControl1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PrintPreviewDialog1_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog1.Load

    End Sub


    Private Sub Mapa_tarjetas1_Load(sender As Object, e As EventArgs) Handles Mapa_tarjetas1.Load

    End Sub



End Class