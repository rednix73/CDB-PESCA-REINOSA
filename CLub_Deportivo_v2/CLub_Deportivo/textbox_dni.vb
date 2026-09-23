Imports System.Text
Imports System.Text.RegularExpressions

''' <summary>
''' Cuadro de texto para el documento de identidad. Admite:
'''  - DNI: 8 (o 7) números + letra  -> se formatea como 12345678-Z
'''  - NIE: X/Y/Z + 7 números + letra -> se formatea como X-1234567-L
'''  - Pasaporte u otro documento extranjero: letras y números libres (sin formato)
''' La letra del DNI/NIE se calcula sola al salir del cuadro o con el botón CALCULA LETRA.
''' </summary>
Public Class textbox_dni
    Inherits TextBox

    Private Const LETRAS As String = "TRWAGMYFPDXBNJZSQVHLCKE"

    Dim _foco As Color
    Public Property Foco As Color
        Get
            Return _foco
        End Get
        Set(value As Color)
            _foco = value
        End Set
    End Property

    Protected Overrides Sub OnKeyPress(e As System.Windows.Forms.KeyPressEventArgs)
        ' Letras, números, guion y teclas de control (Retroceso, Ctrl+C, Ctrl+V...).
        ' Antes solo se admitían números, así que no se podía escribir un NIE ni un pasaporte.
        If Not (Char.IsControl(e.KeyChar) OrElse Char.IsLetterOrDigit(e.KeyChar) OrElse e.KeyChar = "-"c) Then
            e.Handled = True
        End If
        MyBase.OnKeyPress(e)
    End Sub

    Protected Overrides Sub OnLostFocus(e As System.EventArgs)
        MyBase.OnLostFocus(e)

        ' Evitar ejecutar lógica en tiempo de diseño
        If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
            MyBase.BackColor = Color.White
            Return
        End If

        ' Si es un DNI o NIE se completa/corrige la letra; si es otro documento se deja tal cual.
        Dim doc = CalcularLetra()
        If doc <> "" Then MyBase.Text = doc

        MyBase.BackColor = Color.White
    End Sub

    Protected Overrides Sub OnGotFocus(e As System.EventArgs)
        MyBase.BackColor = Foco
        MyBase.OnGotFocus(e)
    End Sub

    ''' <summary>
    ''' Devuelve el DNI o NIE escrito, con su letra y formateado ("12345678-Z" / "X-1234567-L"),
    ''' o cadena vacía si lo escrito no es un DNI ni un NIE (p. ej. un pasaporte).
    ''' </summary>
    Public Function CalcularLetra() As String
        Return FormatearDocumento(MyBase.Text)
    End Function

    ''' <summary>"DNI", "NIE", "OTRO" (pasaporte u otro documento) o "" si está vacío.</summary>
    Public Shared Function TipoDocumento(s As String) As String
        Dim t = Limpio(s)
        If t = "" Then Return ""
        If Regex.IsMatch(t, "^\d{7,8}[A-Z]?$") Then Return "DNI"   ' también DNI antiguos de 7 números
        If Regex.IsMatch(t, "^[XYZ]\d{7}[A-Z]?$") Then Return "NIE"
        Return "OTRO"
    End Function

    ''' <summary>True si es un DNI o NIE con la letra correcta.</summary>
    Public Shared Function LetraCorrecta(s As String) As Boolean
        Dim t = Limpio(s)
        If Not Regex.IsMatch(t, "^(\d{7,8}|[XYZ]\d{7})[A-Z]$") Then Return False
        Return t.EndsWith(LetraDe(t.Substring(0, t.Length - 1)))
    End Function

    ''' <summary>Formatea un DNI/NIE con la letra calculada; "" si no es DNI ni NIE.</summary>
    Public Shared Function FormatearDocumento(s As String) As String
        Dim t = Limpio(s)
        Select Case TipoDocumento(t)
            Case "DNI"
                Dim num = t.Substring(0, 8)
                Return num & "-" & LetraDe(num)
            Case "NIE"
                Dim cuerpo = t.Substring(0, 8)          ' X1234567
                Return cuerpo.Substring(0, 1) & "-" & cuerpo.Substring(1) & "-" & LetraDe(cuerpo)
            Case Else
                Return ""
        End Select
    End Function

    ''' <summary>Letra de control. En el NIE la X, Y, Z valen 0, 1, 2.</summary>
    Private Shared Function LetraDe(cuerpo As String) As String
        Dim c = cuerpo.Replace("X", "0").Replace("Y", "1").Replace("Z", "2")
        Return LETRAS.Chars(CInt(Long.Parse(c) Mod 23)).ToString()
    End Function

    ''' <summary>Mayúsculas y sin espacios ni guiones.</summary>
    Private Shared Function Limpio(s As String) As String
        Return Regex.Replace(If(s, "").ToUpperInvariant(), "[^A-Z0-9]", "")
    End Function

    Public Sub New()
        MyBase.BackColor = Color.White
        MyBase.Font = New Font("Arial", 10, FontStyle.Bold)
        MyBase.CharacterCasing = CharacterCasing.Upper
        MyBase.MaxLength = 20
    End Sub

End Class
