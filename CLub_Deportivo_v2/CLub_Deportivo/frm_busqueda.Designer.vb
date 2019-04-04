<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_busqueda
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btn_usar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 76)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(297, 159)
        Me.DataGridView1.TabIndex = 0
        '
        'btn_usar
        '
        Me.btn_usar.Location = New System.Drawing.Point(87, 254)
        Me.btn_usar.Name = "btn_usar"
        Me.btn_usar.Size = New System.Drawing.Size(134, 23)
        Me.btn_usar.TabIndex = 1
        Me.btn_usar.Text = "Usar seleccionado"
        Me.btn_usar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(87, 304)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(134, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Cerrar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frm_busqueda
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(321, 353)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btn_usar)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frm_busqueda"
        Me.Text = "fm_busqueda"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btn_usar As Button
    Friend WithEvents Button1 As Button
End Class
