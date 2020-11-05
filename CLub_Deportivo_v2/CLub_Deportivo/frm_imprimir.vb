Imports System.IO
Imports System.Drawing.Printing


Public Class frm_imprimir

    Private printFont As Font
    Private streamToPrint As StreamReader


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        streamToPrint = New StreamReader("C:\\My Documents\\MyFile.txt")
        Try

            printFont = New Font("Arial", 10)
            Dim pd As New PrintDocument

            pd.PrintPage += New PrintPageEventHandler
                   (this.pd_PrintPage);
                pd.Print();
            }
            Finally
            {
                streamToPrint.Close();
            }
        }
        Catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    End Sub

    Private Sub frm_imprimir_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


End Class