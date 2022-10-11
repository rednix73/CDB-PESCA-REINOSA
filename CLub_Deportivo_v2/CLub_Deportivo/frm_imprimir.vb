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

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim fuente As New Font("Arial", 11, FontStyle.Regular)
        Dim starty As Integer = e.MarginBounds.Top

        e.Graphics.DrawString("NOMBRE: " + frm_socio.txt_nombre.Text + " " + frm_socio.txt_apellido.Text, fuente, Brushes.Black, 20, 40)
        e.Graphics.DrawString("Nº SOCIO: " + frm_socio.txt_nsocio.Text, fuente, Brushes.Black, 20, 60)
        e.Graphics.DrawString("D.N.I.: " + frm_socio.txt_dni.Text, fuente, Brushes.Black, 180, 60)
        If (frm_socio.cmb_tarjeta.SelectedItem = "SALMON") Then
            e.Graphics.DrawString(frm_socio.cmb_tarjeta.SelectedItem.ToString(), fuente, Brushes.Black, 20, 80)
            e.Graphics.DrawString("TRUCHA", fuente, Brushes.Black, 20, 100)
        Else
            e.Graphics.DrawString(frm_socio.cmb_tarjeta.SelectedItem.ToString(), fuente, Brushes.Black, 20, 80)
        End If

    End Sub

    Private Sub PrintPreviewControl1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PrintPreviewDialog1_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog1.Load

    End Sub

    Private Sub cmb_posicion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_posicion.SelectedIndexChanged
        Mapa_tarjetas1.tarjeta = cmb_posicion.SelectedItem

    End Sub
End Class