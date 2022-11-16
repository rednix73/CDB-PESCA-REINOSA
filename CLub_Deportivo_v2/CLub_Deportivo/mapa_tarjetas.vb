Public Class mapa_tarjetas
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lbl_t1.Click
        tarjeta = 1
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
                        color1 = Color.White
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
    Private _fondo_tarjeta As String
    ''' <summary>
    ''' Propiedad que contiene la ruta del archivo de imagen que se mostrará como fondo en las tarjetas.
    ''' </summary>
    ''' <returns></returns>
    Public Property Imagen_Fondo_tarjeta() As String
        Get
            Return _fondo_tarjeta
        End Get
        Set(ByVal value As String)
            _fondo_tarjeta = value
        End Set
    End Property
    ''' <summary>
    ''' Método que establece como imagen de fondo de los labels que representan las tarjetas el archivo de imagen en la ruta contenida en la propiedad: Imagen_Fondo_tarjeta.
    ''' </summary>
    Public Sub Establece_fondo()
        Try
            For Each l1 As Control In Me.Controls
                CType(l1, Label).Image = Image.FromFile(Imagen_Fondo_tarjeta)
            Next
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub


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
        tarjeta = 2
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t4_Click(sender As Object, e As EventArgs) Handles lbl_t4.Click
        tarjeta = 4
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t5_Click(sender As Object, e As EventArgs) Handles lbl_t5.Click
        tarjeta = 5
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t6_Click(sender As Object, e As EventArgs) Handles lbl_t6.Click
        tarjeta = 6
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t7_Click(sender As Object, e As EventArgs) Handles lbl_t7.Click
        tarjeta = 7
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t8_Click(sender As Object, e As EventArgs) Handles lbl_t8.Click
        tarjeta = 8
        comprobar_tarjeta()
    End Sub

    Private Sub lbl_t9_Click(sender As Object, e As EventArgs) Handles lbl_t9.Click
        tarjeta = 9
        comprobar_tarjeta()
    End Sub
    Private Sub lbl_t10_Click(sender As Object, e As EventArgs) Handles lbl_t10.Click
        tarjeta = 10
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
                    l1.BackColor = Color.White
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
        tarjeta = 3
        comprobar_tarjeta()
    End Sub
End Class
