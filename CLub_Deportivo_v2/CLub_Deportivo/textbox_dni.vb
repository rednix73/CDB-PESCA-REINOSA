Imports System.Text

Public Class textbox_dni
    Inherits TextBox
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
        ' Sobreescribimos el evento OnKeyPress para que el textbox solamente admita 
        ' numeros y un número máximo de 8 caracteres.
        If MyBase.Text.Length > 7 Then
            e.Handled = True
        Else
            If Not Char.IsNumber(e.KeyChar) Then
                e.Handled = True
            End If

        End If

        MyBase.OnKeyPress(e)
    End Sub

    Protected Overrides Sub OnLostFocus(e As System.EventArgs)
        MyBase.OnLostFocus(e)
        If MyBase.Text.Length < 8 Then
            MsgBox("El número de caracteres para poder calcular la letra del NIF introducido debe ser de ocho")
            'MyBase.Focus()
        Else
            Dim nif As String
            nif = CalculaNIF(MyBase.Text)
            MyBase.Text = nif

        End If


        MyBase.BackColor = Color.White




    End Sub

    Protected Overrides Sub OnGotFocus(e As System.EventArgs)
        MyBase.BackColor = Foco

        MyBase.OnGotFocus(e)

    End Sub


    Private Function CalculaNIF(ByVal strA As String) As String
        '----------------------------------------------------------------------
        ' Calcular la letra del NIF
        ' Código original adaptado a Visual Basic                   (13/Sep/95)
        ' Adaptado a Visual Basic .NET (VB 9.0/2008)                (09/May/08)
        ' y convertido en función que devuelve el NIF correcto
        '----------------------------------------------------------------------
        Const cCADENA As String = "TRWAGMYFPDXBNJZSQVHLCKE"
        Const cNUMEROS As String = "0123456789"
        Dim a, b, c, NIF As Integer
        Dim sb As New StringBuilder

        strA = Trim(strA)
        If Len(strA) = 0 Then Return ""

        ' Dejar sólo los números
        For i As Integer = 0 To strA.Length - 1
            If cNUMEROS.IndexOf(strA(i)) > -1 Then
                sb.Append(strA(i))
            End If
        Next

        strA = sb.ToString
        a = 0
        NIF = CInt(Val(strA))
        Do
            b = CInt(Int(NIF / 24))
            c = NIF - (24 * b)
            a = a + c
            NIF = b
        Loop While b <> 0
        b = CInt(Int(a / 23))
        c = a - (23 * b)

        Return strA & "-" & Mid(cCADENA, CInt(c + 1), 1)

    End Function

    Public Sub New()
        MyBase.BackColor = Color.White
        MyBase.Font = New Font("Arial", 10, FontStyle.Bold)

    End Sub


End Class
