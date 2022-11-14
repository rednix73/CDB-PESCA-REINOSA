Public Class frm_numeros_libres
    Private Sub frm_numeros_libres_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ultimo1 As Integer
        Dim ultimo2 As Integer
        Dim ult As Integer
        Try
            For Each n In numeros_libres()
                lst_numeros.Items.Add(n)
            Next
            Select Case tp
                Case tipobd.Excel_ODBC
                    ultimo1 = ultimo(tabla_bdsocios_xls)
                    ultimo2 = ultimo(tabla_socios_xls)
                    ult = ultimo1
                    If (ultimo2 > ult) Then
                        ult = ultimo2
                    End If
                    lbl_ultimo.Text = "ULTIMO USADO:" + ult.ToString
                Case tipobd.MySQL
                    ultimo1 = ultimo(tabla_bdsocios_mysql)
                    ultimo2 = ultimo(tabla_socios_mysql)
                    ult = ultimo1
                    If (ultimo2 > ult) Then
                        ult = ultimo2
                    End If
                    lbl_ultimo.Text = "ULTIMO USADO:" + ult.ToString
                Case Else

            End Select
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not lst_numeros.SelectedIndex = -1 Then
            frm_socio.txt_nsocio.Text = lst_numeros.SelectedItem.ToString
            Me.Close()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class