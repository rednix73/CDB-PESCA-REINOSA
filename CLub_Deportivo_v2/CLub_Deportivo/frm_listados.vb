Public Class frm_listados
    Private Sub frm_listados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = frm_principal

        dw_socios.RowFilter = ""

        DataGridView1.DataSource = dw_socios

        cmb_bd.SelectedIndex = 0
        cmb_tipo.SelectedIndex = 0

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

    Private Sub lbl_listado_Click(sender As Object, e As EventArgs) Handles lbl_listado.Click

    End Sub

    Private Sub cmb_bd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_bd.SelectedIndexChanged
        Select Case cmb_bd.SelectedIndex
            Case 1
                Select Case cmb_tipo.SelectedIndex
                    Case 0
                        dw_bdsocios.RowFilter = ""
                    Case Else
                        dw_bdsocios.RowFilter = "tarjeta=" + (cmb_tipo.SelectedIndex).ToString()
                End Select

                DataGridView1.DataSource = dw_bdsocios
            Case 0
                Select Case cmb_tipo.SelectedIndex
                    Case 0
                        dw_socios.RowFilter = ""
                    Case Else
                        dw_socios.RowFilter = "tarjeta=" + (cmb_tipo.SelectedIndex).ToString()
                End Select

                DataGridView1.DataSource = dw_socios
            Case Else

        End Select
    End Sub

    Private Sub cmb_tipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_tipo.SelectedIndexChanged
        Select Case cmb_tipo.SelectedIndex
            Case 0
                Select Case cmb_bd.SelectedIndex
                    Case 1
                        dw_bdsocios.RowFilter = ""
                        DataGridView1.DataSource = dw_bdsocios
                    Case 0
                        dw_socios.RowFilter = ""
                        DataGridView1.DataSource = dw_socios
                    Case Else
                End Select

            Case Else
                Select Case cmb_bd.SelectedIndex
                    Case 1
                        dw_bdsocios.RowFilter = "tarjeta=" + (cmb_tipo.SelectedIndex).ToString()
                        DataGridView1.DataSource = dw_bdsocios
                    Case 0
                        dw_socios.RowFilter = "tarjeta=" + (cmb_tipo.SelectedIndex).ToString()
                        DataGridView1.DataSource = dw_socios
                    Case Else
                End Select

        End Select

    End Sub
End Class