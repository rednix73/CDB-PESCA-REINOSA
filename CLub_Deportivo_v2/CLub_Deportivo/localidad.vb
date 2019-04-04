Public Class localidad
    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _cp As String
    Public Property CP() As String
        Get
            Return _cp
        End Get
        Set(ByVal value As String)
            If value.Length = 5 And IsNumeric(CInt(value)) Then
                _cp = value
            Else
                MsgBox("Formato de codigo postal erroneo. Debe estar formado por 5 números")
            End If

        End Set
    End Property



End Class
