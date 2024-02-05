<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_numeros_libres
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_numeros_libres))
        Me.lst_numeros = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lbl_ultimo = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lst_numeros
        '
        Me.lst_numeros.FormattingEnabled = True
        Me.lst_numeros.ItemHeight = 16
        Me.lst_numeros.Location = New System.Drawing.Point(21, 36)
        Me.lst_numeros.Margin = New System.Windows.Forms.Padding(4)
        Me.lst_numeros.Name = "lst_numeros"
        Me.lst_numeros.Size = New System.Drawing.Size(159, 228)
        Me.lst_numeros.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "NÚMEROS LIBRES:"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(40, 303)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 28)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Usar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(40, 350)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 28)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'lbl_ultimo
        '
        Me.lbl_ultimo.AutoSize = True
        Me.lbl_ultimo.Location = New System.Drawing.Point(17, 268)
        Me.lbl_ultimo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ultimo.Name = "lbl_ultimo"
        Me.lbl_ultimo.Size = New System.Drawing.Size(117, 17)
        Me.lbl_ultimo.TabIndex = 4
        Me.lbl_ultimo.Text = "ÚLTIMO USADO:"
        '
        'frm_numeros_libres
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(204, 395)
        Me.Controls.Add(Me.lbl_ultimo)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lst_numeros)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frm_numeros_libres"
        Me.Text = " C.D.B. pesca Reinosa - Numeros libres"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lst_numeros As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents lbl_ultimo As Label
End Class
