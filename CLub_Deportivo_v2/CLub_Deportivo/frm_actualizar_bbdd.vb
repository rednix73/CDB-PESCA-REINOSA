Public Class frm_actualizar_bbdd
    Private Sub frm_actualizar_bbdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs)
        ' Al ejecutar la actualización de BBDD, ofrecer purga física
        Try
            bbdd.PurgeDeletedFederativas()
        Catch ex As Exception
            MessageBox.Show("Error al purgar federativas: " & ex.Message)
        End Try
        Me.Close()
    End Sub
End Class