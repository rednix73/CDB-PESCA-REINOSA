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
        Me.btn_imprimir = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintForm1 = New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(Me.components)
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.Mapa_tarjetas1 = New CLub_Deportivo.mapa_tarjetas()
        Me.cmb_posicion = New System.Windows.Forms.ComboBox()
        Me.lbl_tarjeta = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_imprimir
        '
        Me.btn_imprimir.Location = New System.Drawing.Point(29, 610)
        Me.btn_imprimir.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_imprimir.Name = "btn_imprimir"
        Me.btn_imprimir.Size = New System.Drawing.Size(412, 40)
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
        'Mapa_tarjetas1
        '
        Me.Mapa_tarjetas1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Mapa_tarjetas1.Location = New System.Drawing.Point(29, 66)
        Me.Mapa_tarjetas1.Name = "Mapa_tarjetas1"
        Me.Mapa_tarjetas1.Size = New System.Drawing.Size(412, 537)
        Me.Mapa_tarjetas1.TabIndex = 12
        '
        'cmb_posicion
        '
        Me.cmb_posicion.FormattingEnabled = True
        Me.cmb_posicion.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cmb_posicion.Location = New System.Drawing.Point(263, 20)
        Me.cmb_posicion.Name = "cmb_posicion"
        Me.cmb_posicion.Size = New System.Drawing.Size(55, 24)
        Me.cmb_posicion.TabIndex = 13
        '
        'lbl_tarjeta
        '
        Me.lbl_tarjeta.AutoSize = True
        Me.lbl_tarjeta.Location = New System.Drawing.Point(26, 20)
        Me.lbl_tarjeta.Name = "lbl_tarjeta"
        Me.lbl_tarjeta.Size = New System.Drawing.Size(231, 17)
        Me.lbl_tarjeta.TabIndex = 14
        Me.lbl_tarjeta.Text = "Seleccione la posición de la tarjeta:"
        '
        'frm_imprimir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 663)
        Me.Controls.Add(Me.lbl_tarjeta)
        Me.Controls.Add(Me.cmb_posicion)
        Me.Controls.Add(Me.Mapa_tarjetas1)
        Me.Controls.Add(Me.btn_imprimir)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frm_imprimir"
        Me.Text = "frm_imprimir"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_imprimir As Button
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintForm1 As PowerPacks.Printing.PrintForm
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents Mapa_tarjetas1 As mapa_tarjetas
    Friend WithEvents lbl_tarjeta As Label
    Friend WithEvents cmb_posicion As ComboBox
End Class
