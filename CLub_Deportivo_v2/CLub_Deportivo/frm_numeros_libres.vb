Public Class frm_numeros_libres

    ' Se rellena cada vez que se muestra la ventana (Load solo se ejecuta la primera vez que se
    ' abre con ShowDialog, así que la lista se quedaba desactualizada y además se duplicaba).
    Private Sub frm_numeros_libres_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Not Me.Visible Then Return
        Try
            lst_numeros.Items.Clear()
            For Each n In numeros_libres()
                lst_numeros.Items.Add(n)
            Next
            Dim ult = Math.Max(ultimo(If(tp = tipobd.MySQL, tabla_bdsocios_mysql, tabla_bdsocios_xls)),
                               ultimo(If(tp = tipobd.MySQL, tabla_socios_mysql, tabla_socios_xls)))
            lbl_ultimo.Text = "ÚLTIMO USADO: " & ult.ToString() & "   (siguiente: " & (ult + 1).ToString() & ")"
            If lst_numeros.Items.Count = 0 Then lst_numeros.Items.Add(ult + 1) ' no hay huecos: proponer el siguiente
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UsarNumero()
    End Sub

    Private Sub lst_numeros_DoubleClick(sender As Object, e As EventArgs) Handles lst_numeros.DoubleClick
        UsarNumero() ' doble clic = Usar
    End Sub

    Private Sub UsarNumero()
        If lst_numeros.SelectedIndex = -1 Then
            MsgBox("Seleccione un número de la lista.")
            Return
        End If
        frm_socio.txt_nsocio.Text = lst_numeros.SelectedItem.ToString()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class
