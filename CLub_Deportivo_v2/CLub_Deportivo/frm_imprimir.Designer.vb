<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_imprimir
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_imprimir))
        Me.Mapa_tarjetas1 = New CLub_Deportivo.mapa_tarjetas()
        Me.btn_imprimir_tarjeta = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btn_imprimir_fondo = New System.Windows.Forms.Button()
        Me.Mapa_tarjetas2 = New CLub_Deportivo.mapa_tarjetas()
        Me.grp_box_tarjeta = New System.Windows.Forms.GroupBox()
        Me.rdo_reverso = New System.Windows.Forms.RadioButton()
        Me.rdo_anverso = New System.Windows.Forms.RadioButton()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintForm1 = New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(Me.components)
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDocument2 = New System.Drawing.Printing.PrintDocument()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.grp_box_tarjeta.SuspendLayout()
        Me.SuspendLayout()
        '
        'Mapa_tarjetas1
        '
        Me.Mapa_tarjetas1.AutoSize = True
        Me.Mapa_tarjetas1.BackColor = System.Drawing.Color.White
        Me.Mapa_tarjetas1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Mapa_tarjetas1.Imagen_Fondo_tarjeta = Nothing
        Me.Mapa_tarjetas1.Location = New System.Drawing.Point(20, 8)
        Me.Mapa_tarjetas1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Mapa_tarjetas1.Name = "Mapa_tarjetas1"
        Me.Mapa_tarjetas1.Size = New System.Drawing.Size(406, 625)
        Me.Mapa_tarjetas1.TabIndex = 17
        Me.Mapa_tarjetas1.tarjeta = 0
        '
        'btn_imprimir_tarjeta
        '
        Me.btn_imprimir_tarjeta.Location = New System.Drawing.Point(20, 642)
        Me.btn_imprimir_tarjeta.Margin = New System.Windows.Forms.Padding(5)
        Me.btn_imprimir_tarjeta.Name = "btn_imprimir_tarjeta"
        Me.btn_imprimir_tarjeta.Size = New System.Drawing.Size(186, 42)
        Me.btn_imprimir_tarjeta.TabIndex = 18
        Me.btn_imprimir_tarjeta.Text = "Imprimir"
        Me.btn_imprimir_tarjeta.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(467, 785)
        Me.TabControl1.TabIndex = 19
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Controls.Add(Me.Mapa_tarjetas1)
        Me.TabPage1.Controls.Add(Me.btn_imprimir_tarjeta)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(459, 752)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Tarjeta"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(233, 643)
        Me.Button2.Margin = New System.Windows.Forms.Padding(5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(193, 42)
        Me.Button2.TabIndex = 20
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Button3)
        Me.TabPage2.Controls.Add(Me.btn_imprimir_fondo)
        Me.TabPage2.Controls.Add(Me.Mapa_tarjetas2)
        Me.TabPage2.Controls.Add(Me.grp_box_tarjeta)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(459, 752)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Fondo"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(229, 700)
        Me.Button3.Margin = New System.Windows.Forms.Padding(5)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(196, 42)
        Me.Button3.TabIndex = 21
        Me.Button3.Text = "Cerrar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btn_imprimir_fondo
        '
        Me.btn_imprimir_fondo.Location = New System.Drawing.Point(17, 700)
        Me.btn_imprimir_fondo.Margin = New System.Windows.Forms.Padding(5)
        Me.btn_imprimir_fondo.Name = "btn_imprimir_fondo"
        Me.btn_imprimir_fondo.Size = New System.Drawing.Size(187, 42)
        Me.btn_imprimir_fondo.TabIndex = 20
        Me.btn_imprimir_fondo.Text = "Imprimir"
        Me.btn_imprimir_fondo.UseVisualStyleBackColor = True
        '
        'Mapa_tarjetas2
        '
        Me.Mapa_tarjetas2.AutoSize = True
        Me.Mapa_tarjetas2.BackColor = System.Drawing.Color.White
        Me.Mapa_tarjetas2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Mapa_tarjetas2.Imagen_Fondo_tarjeta = Nothing
        Me.Mapa_tarjetas2.Location = New System.Drawing.Point(18, 67)
        Me.Mapa_tarjetas2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Mapa_tarjetas2.Name = "Mapa_tarjetas2"
        Me.Mapa_tarjetas2.Size = New System.Drawing.Size(407, 625)
        Me.Mapa_tarjetas2.TabIndex = 18
        Me.Mapa_tarjetas2.tarjeta = 0
        '
        'grp_box_tarjeta
        '
        Me.grp_box_tarjeta.Controls.Add(Me.rdo_reverso)
        Me.grp_box_tarjeta.Controls.Add(Me.rdo_anverso)
        Me.grp_box_tarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grp_box_tarjeta.Location = New System.Drawing.Point(17, 6)
        Me.grp_box_tarjeta.Name = "grp_box_tarjeta"
        Me.grp_box_tarjeta.Size = New System.Drawing.Size(408, 58)
        Me.grp_box_tarjeta.TabIndex = 16
        Me.grp_box_tarjeta.TabStop = False
        Me.grp_box_tarjeta.Text = "Seleccione el fondo:"
        '
        'rdo_reverso
        '
        Me.rdo_reverso.AutoSize = True
        Me.rdo_reverso.Location = New System.Drawing.Point(301, 19)
        Me.rdo_reverso.Name = "rdo_reverso"
        Me.rdo_reverso.Size = New System.Drawing.Size(107, 26)
        Me.rdo_reverso.TabIndex = 1
        Me.rdo_reverso.Text = "Reverso."
        Me.rdo_reverso.UseVisualStyleBackColor = True
        '
        'rdo_anverso
        '
        Me.rdo_anverso.AutoSize = True
        Me.rdo_anverso.Checked = True
        Me.rdo_anverso.Location = New System.Drawing.Point(40, 19)
        Me.rdo_anverso.Name = "rdo_anverso"
        Me.rdo_anverso.Size = New System.Drawing.Size(106, 26)
        Me.rdo_anverso.TabIndex = 0
        Me.rdo_anverso.TabStop = True
        Me.rdo_anverso.Text = "Anverso."
        Me.rdo_anverso.UseVisualStyleBackColor = True
        '
        'PrintDocument1
        '
        '
        'PrintForm1
        '
        Me.PrintForm1.DocumentName = "document"
        Me.PrintForm1.Form = Me
        Me.PrintForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPreview
        Me.PrintForm1.PrinterSettings = CType(resources.GetObject("PrintForm1.PrinterSettings"), System.Drawing.Printing.PrinterSettings)
        Me.PrintForm1.PrintFileName = Nothing
        '
        'PrintDialog1
        '
        Me.PrintDialog1.Document = Me.PrintDocument1
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PrintDocument2
        '
        '
        'frm_imprimir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(496, 800)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_imprimir"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "C.D.B. pesca Reinosa - Imprimir"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.grp_box_tarjeta.ResumeLayout(False)
        Me.grp_box_tarjeta.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Mapa_tarjetas1 As mapa_tarjetas
    Friend WithEvents btn_imprimir_tarjeta As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btn_imprimir_fondo As Button
    Friend WithEvents Mapa_tarjetas2 As mapa_tarjetas
    Friend WithEvents grp_box_tarjeta As GroupBox
    Friend WithEvents rdo_reverso As RadioButton
    Friend WithEvents rdo_anverso As RadioButton
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintForm1 As PowerPacks.Printing.PrintForm
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents PrintDocument2 As Printing.PrintDocument
End Class
