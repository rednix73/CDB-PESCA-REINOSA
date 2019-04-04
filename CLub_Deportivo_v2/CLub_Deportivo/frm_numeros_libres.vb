Public Class frm_numeros_libres
    Private Sub frm_numeros_libres_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conectar()

        For Each n In numeros_libres()
            lst_numeros.Items.Add(n)
        Next
        Dim ultimo1 As Integer = ultimo("bdsocios")
        Dim ultimo2 As Integer = ultimo("socios")
        Dim ult As Integer = ultimo1
        If (ultimo2 > ult) Then
            ult = ultimo2
        End If
        lbl_ultimo.Text = "ULTIMO USADO:" + ult.ToString
        desconectar()
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