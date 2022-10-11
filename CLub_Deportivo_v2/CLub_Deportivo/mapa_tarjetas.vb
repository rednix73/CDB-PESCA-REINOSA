Public Class mapa_tarjetas
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lbl_t1.Click
        restablecer()

        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green

        If lbl_t1.BackColor = color1 Then
            lbl_t1.BackColor = color2
        Else
            lbl_t1.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub
    Private _tarjeta As Integer
    Public Property tarjeta() As Integer
        Get
            Return _tarjeta
        End Get
        Set(ByVal value As Integer)
            _tarjeta = value
            Try
                restablecer()
                Dim contador As Integer = 0
                For Each l1 As Label In Me.Controls
                    contador += 1
                    Dim num As Integer
                    num = CInt(Strings.Mid(l1.Name, 6))
                    If (_tarjeta = num) Then
                        Dim color1 As New Color
                        color1 = SystemColors.Control
                        Dim color2 As New Color
                        color2 = Color.Green
                        If l1.BackColor = color1 Then
                            l1.BackColor = color2
                        Else
                            l1.BackColor = color1
                        End If
                        comprobar_tarjeta()
                    End If
                Next
            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try
        End Set
    End Property

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lbl_t2_Click(sender As Object, e As EventArgs) Handles lbl_t2.Click
        restablecer()
        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green

        If lbl_t2.BackColor = color1 Then
            lbl_t2.BackColor = color2
        Else
            lbl_t2.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t4_Click(sender As Object, e As EventArgs) Handles lbl_t4.Click
        restablecer()
        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green

        If lbl_t4.BackColor = color1 Then
            lbl_t4.BackColor = color2
        Else
            lbl_t4.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t5_Click(sender As Object, e As EventArgs) Handles lbl_t5.Click
        restablecer()
        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green

        If lbl_t5.BackColor = color1 Then
            lbl_t5.BackColor = color2
        Else
            lbl_t5.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t6_Click(sender As Object, e As EventArgs) Handles lbl_t6.Click
        restablecer()
        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green

        If lbl_t6.BackColor = color1 Then
            lbl_t6.BackColor = color2
        Else
            lbl_t6.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t7_Click(sender As Object, e As EventArgs) Handles lbl_t7.Click
        restablecer()
        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green

        If lbl_t7.BackColor = color1 Then
            lbl_t7.BackColor = color2
        Else
            lbl_t7.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t8_Click(sender As Object, e As EventArgs) Handles lbl_t8.Click
        restablecer()
        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green

        If lbl_t8.BackColor = color1 Then
            lbl_t8.BackColor = color2
        Else
            lbl_t8.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t9_Click(sender As Object, e As EventArgs) Handles lbl_t9.Click
        restablecer()
        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green

        If lbl_t9.BackColor = color1 Then
            lbl_t9.BackColor = color2
        Else
            lbl_t9.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub
    Private Sub lbl_t10_Click(sender As Object, e As EventArgs) Handles lbl_t10.Click
        restablecer()
        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green
        If lbl_t10.BackColor = color1 Then
            lbl_t10.BackColor = color2
        Else
            lbl_t10.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub

    Private Sub mapa_tarjetas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    ''' <summary>
    ''' Establece los colores de fondo de todos los labels al color original.
    ''' </summary>
    Public Sub restablecer()
        Try
            For Each l1 As Label In Me.Controls
                If l1.BackColor = Color.Green Then
                    l1.BackColor = SystemColors.Control
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub
    Public Function comprobar_tarjeta() As Boolean
        Dim contador As Integer = 0
        For Each l1 As Label In Me.Controls
            If l1.BackColor = Color.Green Then
                contador += 1
            End If
        Next
        Select Case contador
            Case 0
                MsgBox("Debe seleccionar la posición en la hoja de impresión de tarjeta de socio haciendo click con el ratón en la posición deseada.")
                Return False
            Case 1
                Return True
            Case Else
                MsgBox("Debe seleccionar solamente una posición en la hoja de impresión. Haga click en las posiciones no deseadas para desmarcarlas.")
                Return False
        End Select
    End Function

    Private Sub lbl_t3_Click(sender As Object, e As EventArgs) Handles lbl_t3.Click
        restablecer()
        Dim color1 As New Color
        color1 = SystemColors.Control
        Dim color2 As New Color
        color2 = Color.Green

        If lbl_t3.BackColor = color1 Then
            lbl_t3.BackColor = color2
        Else
            lbl_t3.BackColor = color1
        End If
        comprobar_tarjeta()
    End Sub
End Class
