<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_actualizar_bbdd
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_actualizar_bbdd))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.lbl_resumen = New System.Windows.Forms.Label()
        Me.btn_sel_todo = New System.Windows.Forms.Button()
        Me.btn_sel_ninguno = New System.Windows.Forms.Button()
        Me.btn_actualizar_todo = New System.Windows.Forms.Button()
        Me.btn_actualizar_sel = New System.Windows.Forms.Button()
        Me.btn_cerrar = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 42)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 24
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1056, 440)
        Me.DataGridView1.TabIndex = 0
        '
        'lbl_resumen
        '
        Me.lbl_resumen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_resumen.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_resumen.Location = New System.Drawing.Point(12, 12)
        Me.lbl_resumen.Name = "lbl_resumen"
        Me.lbl_resumen.Size = New System.Drawing.Size(1056, 24)
        Me.lbl_resumen.TabIndex = 1
        Me.lbl_resumen.Text = "Comparando..."
        '
        'btn_sel_todo
        '
        Me.btn_sel_todo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_sel_todo.Location = New System.Drawing.Point(12, 494)
        Me.btn_sel_todo.Name = "btn_sel_todo"
        Me.btn_sel_todo.Size = New System.Drawing.Size(140, 40)
        Me.btn_sel_todo.TabIndex = 2
        Me.btn_sel_todo.Text = "Seleccionar todo"
        Me.btn_sel_todo.UseVisualStyleBackColor = True
        '
        'btn_sel_ninguno
        '
        Me.btn_sel_ninguno.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_sel_ninguno.Location = New System.Drawing.Point(160, 494)
        Me.btn_sel_ninguno.Name = "btn_sel_ninguno"
        Me.btn_sel_ninguno.Size = New System.Drawing.Size(140, 40)
        Me.btn_sel_ninguno.TabIndex = 3
        Me.btn_sel_ninguno.Text = "Quitar selección"
        Me.btn_sel_ninguno.UseVisualStyleBackColor = True
        '
        'btn_actualizar_todo
        '
        Me.btn_actualizar_todo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_actualizar_todo.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_actualizar_todo.Location = New System.Drawing.Point(568, 494)
        Me.btn_actualizar_todo.Name = "btn_actualizar_todo"
        Me.btn_actualizar_todo.Size = New System.Drawing.Size(170, 40)
        Me.btn_actualizar_todo.TabIndex = 4
        Me.btn_actualizar_todo.Text = "ACTUALIZAR TODO"
        Me.btn_actualizar_todo.UseVisualStyleBackColor = True
        '
        'btn_actualizar_sel
        '
        Me.btn_actualizar_sel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_actualizar_sel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_actualizar_sel.Location = New System.Drawing.Point(746, 494)
        Me.btn_actualizar_sel.Name = "btn_actualizar_sel"
        Me.btn_actualizar_sel.Size = New System.Drawing.Size(200, 40)
        Me.btn_actualizar_sel.TabIndex = 5
        Me.btn_actualizar_sel.Text = "ACTUALIZAR SELECCION"
        Me.btn_actualizar_sel.UseVisualStyleBackColor = True
        '
        'btn_cerrar
        '
        Me.btn_cerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_cerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_cerrar.Location = New System.Drawing.Point(954, 494)
        Me.btn_cerrar.Name = "btn_cerrar"
        Me.btn_cerrar.Size = New System.Drawing.Size(114, 40)
        Me.btn_cerrar.TabIndex = 6
        Me.btn_cerrar.Text = "CERRAR"
        Me.btn_cerrar.UseVisualStyleBackColor = True
        '
        'frm_actualizar_bbdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_cerrar
        Me.ClientSize = New System.Drawing.Size(1080, 546)
        Me.Controls.Add(Me.btn_cerrar)
        Me.Controls.Add(Me.btn_actualizar_sel)
        Me.Controls.Add(Me.btn_actualizar_todo)
        Me.Controls.Add(Me.btn_sel_ninguno)
        Me.Controls.Add(Me.btn_sel_todo)
        Me.Controls.Add(Me.lbl_resumen)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(900, 400)
        Me.Name = "frm_actualizar_bbdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "C.D.B. pesca Reinosa - Actualizar base de datos de socios"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents lbl_resumen As Label
    Friend WithEvents btn_sel_todo As Button
    Friend WithEvents btn_sel_ninguno As Button
    Friend WithEvents btn_actualizar_todo As Button
    Friend WithEvents btn_actualizar_sel As Button
    Friend WithEvents btn_cerrar As Button
End Class
