<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_imprimir
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_imprimir))
        Me.btn_imprimir = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintForm1 = New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(Me.components)
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.lbl_tarjeta = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Mapa_tarjetas1 = New CLub_Deportivo.mapa_tarjetas()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Mapa_tarjetas2 = New CLub_Deportivo.mapa_tarjetas()
        Me.grp_box_tarjeta = New System.Windows.Forms.GroupBox()
        Me.rdo_reverso = New System.Windows.Forms.RadioButton()
        Me.rdo_anverso = New System.Windows.Forms.RadioButton()
        Me.btn_imprime_fondo = New System.Windows.Forms.Button()
        Me.PrintDocument2 = New System.Drawing.Printing.PrintDocument()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.grp_box_tarjeta.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_imprimir
        '
        Me.btn_imprimir.Location = New System.Drawing.Point(9, 646)
        Me.btn_imprimir.Margin = New System.Windows.Forms.Padding(5)
        Me.btn_imprimir.Name = "btn_imprimir"
        Me.btn_imprimir.Size = New System.Drawing.Size(458, 42)
        Me.btn_imprimir.TabIndex = 11
        Me.btn_imprimir.Text = "Imprimir"
        Me.btn_imprimir.UseVisualStyleBackColor = True
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
        'lbl_tarjeta
        '
        Me.lbl_tarjeta.AutoSize = True
        Me.lbl_tarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_tarjeta.Location = New System.Drawing.Point(41, -49)
        Me.lbl_tarjeta.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_tarjeta.Name = "lbl_tarjeta"
        Me.lbl_tarjeta.Size = New System.Drawing.Size(392, 24)
        Me.lbl_tarjeta.TabIndex = 14
        Me.lbl_tarjeta.Text = "Seleccione la posición de la tarjeta a imprimir:"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(7, 6)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(510, 791)
        Me.TabControl1.TabIndex = 15
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.Mapa_tarjetas1)
        Me.TabPage1.Controls.Add(Me.lbl_tarjeta)
        Me.TabPage1.Controls.Add(Me.btn_imprimir)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(502, 758)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Tarjeta"
        '
        'Mapa_tarjetas1
        '
        Me.Mapa_tarjetas1.AutoSize = True
        Me.Mapa_tarjetas1.BackColor = System.Drawing.Color.White
        Me.Mapa_tarjetas1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Mapa_tarjetas1.Imagen_Fondo_tarjeta = Nothing
        Me.Mapa_tarjetas1.Location = New System.Drawing.Point(8, 8)
        Me.Mapa_tarjetas1.Margin = New System.Windows.Forms.Padding(4)
        Me.Mapa_tarjetas1.Name = "Mapa_tarjetas1"
        Me.Mapa_tarjetas1.Size = New System.Drawing.Size(459, 629)
        Me.Mapa_tarjetas1.TabIndex = 15
        Me.Mapa_tarjetas1.tarjeta = 0
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.Mapa_tarjetas2)
        Me.TabPage2.Controls.Add(Me.grp_box_tarjeta)
        Me.TabPage2.Controls.Add(Me.btn_imprime_fondo)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Size = New System.Drawing.Size(502, 758)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Fondo"
        '
        'Mapa_tarjetas2
        '
        Me.Mapa_tarjetas2.AutoSize = True
        Me.Mapa_tarjetas2.BackColor = System.Drawing.Color.White
        Me.Mapa_tarjetas2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Mapa_tarjetas2.Imagen_Fondo_tarjeta = Nothing
        Me.Mapa_tarjetas2.Location = New System.Drawing.Point(23, 62)
        Me.Mapa_tarjetas2.Margin = New System.Windows.Forms.Padding(4)
        Me.Mapa_tarjetas2.Name = "Mapa_tarjetas2"
        Me.Mapa_tarjetas2.Size = New System.Drawing.Size(466, 621)
        Me.Mapa_tarjetas2.TabIndex = 16
        Me.Mapa_tarjetas2.tarjeta = 0
        '
        'grp_box_tarjeta
        '
        Me.grp_box_tarjeta.Controls.Add(Me.rdo_reverso)
        Me.grp_box_tarjeta.Controls.Add(Me.rdo_anverso)
        Me.grp_box_tarjeta.Location = New System.Drawing.Point(23, 8)
        Me.grp_box_tarjeta.Name = "grp_box_tarjeta"
        Me.grp_box_tarjeta.Size = New System.Drawing.Size(466, 45)
        Me.grp_box_tarjeta.TabIndex = 15
        Me.grp_box_tarjeta.TabStop = False
        Me.grp_box_tarjeta.Text = "Seleccione el fondo:"
        '
        'rdo_reverso
        '
        Me.rdo_reverso.AutoSize = True
        Me.rdo_reverso.Location = New System.Drawing.Point(352, 17)
        Me.rdo_reverso.Name = "rdo_reverso"
        Me.rdo_reverso.Size = New System.Drawing.Size(96, 24)
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
        Me.rdo_anverso.Size = New System.Drawing.Size(95, 24)
        Me.rdo_anverso.TabIndex = 0
        Me.rdo_anverso.TabStop = True
        Me.rdo_anverso.Text = "Anverso."
        Me.rdo_anverso.UseVisualStyleBackColor = True
        '
        'btn_imprime_fondo
        '
        Me.btn_imprime_fondo.AutoSize = True
        Me.btn_imprime_fondo.Location = New System.Drawing.Point(1, 692)
        Me.btn_imprime_fondo.Margin = New System.Windows.Forms.Padding(5)
        Me.btn_imprime_fondo.Name = "btn_imprime_fondo"
        Me.btn_imprime_fondo.Size = New System.Drawing.Size(500, 44)
        Me.btn_imprime_fondo.TabIndex = 13
        Me.btn_imprime_fondo.Text = "Imprimir"
        Me.btn_imprime_fondo.UseVisualStyleBackColor = True
        '
        'PrintDocument2
        '
        '
        'frm_imprimir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(534, 810)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frm_imprimir"
        Me.Text = " C.D.B. pesca Reinosa - Imprimir"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.grp_box_tarjeta.ResumeLayout(False)
        Me.grp_box_tarjeta.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_imprimir As Button
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintForm1 As PowerPacks.Printing.PrintForm
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents lbl_tarjeta As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btn_imprime_fondo As Button
    Friend WithEvents grp_box_tarjeta As GroupBox
    Friend WithEvents rdo_reverso As RadioButton
    Friend WithEvents rdo_anverso As RadioButton
    Friend WithEvents Mapa_tarjetas2 As mapa_tarjetas
    Friend WithEvents Mapa_tarjetas1 As mapa_tarjetas
    Friend WithEvents PrintDocument2 As Printing.PrintDocument
End Class
