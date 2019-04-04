<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_socio
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_socio))
        Me.dtpk_fecha_nac = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_email = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmb_tarjeta = New System.Windows.Forms.ComboBox()
        Me.cmb_pais = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_direcc = New System.Windows.Forms.TextBox()
        Me.grp_socio = New System.Windows.Forms.GroupBox()
        Me.rdo_normal = New System.Windows.Forms.RadioButton()
        Me.rdo_jubilado = New System.Windows.Forms.RadioButton()
        Me.rdo_otros = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grp_estado = New System.Windows.Forms.GroupBox()
        Me.rdo_pagado = New System.Windows.Forms.RadioButton()
        Me.rdo_nopagado = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.grp_commnent = New System.Windows.Forms.GroupBox()
        Me.txt_coment = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_insertar = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txt_nsocio = New System.Windows.Forms.TextBox()
        Me.btn_buscar_apell = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_buscar_dni = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btn_letranif = New System.Windows.Forms.Button()
        Me.cmb_prov = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_num_libres = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_cp = New System.Windows.Forms.TextBox()
        Me.cmb_localidad = New System.Windows.Forms.ComboBox()
        Me.btn_buscar_nsocio = New System.Windows.Forms.Button()
        Me.btn_modificar = New System.Windows.Forms.Button()
        Me.btn_eliminar = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.txt_dni = New CLub_Deportivo.textbox_dni()
        Me.txt_apellido = New CLub_Deportivo.textbox_club()
        Me.txt_nombre = New CLub_Deportivo.textbox_club()
        Me.grp_socio.SuspendLayout()
        Me.grp_estado.SuspendLayout()
        Me.grp_commnent.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpk_fecha_nac
        '
        Me.dtpk_fecha_nac.Location = New System.Drawing.Point(13, 353)
        Me.dtpk_fecha_nac.Name = "dtpk_fecha_nac"
        Me.dtpk_fecha_nac.Size = New System.Drawing.Size(200, 20)
        Me.dtpk_fecha_nac.TabIndex = 26
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(270, 336)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 13)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "Email:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 337)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(111, 13)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Fecha de Nacimiento:"
        '
        'txt_email
        '
        Me.txt_email.Location = New System.Drawing.Point(270, 353)
        Me.txt_email.Name = "txt_email"
        Me.txt_email.Size = New System.Drawing.Size(182, 20)
        Me.txt_email.TabIndex = 28
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(327, 275)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(32, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "País:"
        '
        'cmb_tarjeta
        '
        Me.cmb_tarjeta.FormattingEnabled = True
        Me.cmb_tarjeta.Items.AddRange(New Object() {"SALMON", "TRUCHA"})
        Me.cmb_tarjeta.Location = New System.Drawing.Point(16, 401)
        Me.cmb_tarjeta.Name = "cmb_tarjeta"
        Me.cmb_tarjeta.Size = New System.Drawing.Size(121, 21)
        Me.cmb_tarjeta.TabIndex = 31
        Me.cmb_tarjeta.Text = "Seleccionar..."
        '
        'cmb_pais
        '
        Me.cmb_pais.FormattingEnabled = True
        Me.cmb_pais.Items.AddRange(New Object() {"ESPAÑA", "UNION EUROPEA", "OTROS"})
        Me.cmb_pais.Location = New System.Drawing.Point(331, 294)
        Me.cmb_pais.Name = "cmb_pais"
        Me.cmb_pais.Size = New System.Drawing.Size(121, 21)
        Me.cmb_pais.TabIndex = 22
        Me.cmb_pais.Text = "Seleccionar..."
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(13, 385)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "Tarjeta:"
        '
        'txt_direcc
        '
        Me.txt_direcc.Location = New System.Drawing.Point(11, 236)
        Me.txt_direcc.Name = "txt_direcc"
        Me.txt_direcc.Size = New System.Drawing.Size(270, 20)
        Me.txt_direcc.TabIndex = 21
        '
        'grp_socio
        '
        Me.grp_socio.Controls.Add(Me.rdo_otros)
        Me.grp_socio.Controls.Add(Me.rdo_jubilado)
        Me.grp_socio.Controls.Add(Me.rdo_normal)
        Me.grp_socio.Location = New System.Drawing.Point(11, 439)
        Me.grp_socio.Name = "grp_socio"
        Me.grp_socio.Size = New System.Drawing.Size(345, 46)
        Me.grp_socio.TabIndex = 35
        Me.grp_socio.TabStop = False
        Me.grp_socio.Text = "Tipo socio:"
        '
        'rdo_normal
        '
        Me.rdo_normal.AutoSize = True
        Me.rdo_normal.Location = New System.Drawing.Point(17, 22)
        Me.rdo_normal.Name = "rdo_normal"
        Me.rdo_normal.Size = New System.Drawing.Size(61, 17)
        Me.rdo_normal.TabIndex = 0
        Me.rdo_normal.TabStop = True
        Me.rdo_normal.Text = "Normal."
        Me.rdo_normal.UseVisualStyleBackColor = True
        '
        'rdo_jubilado
        '
        Me.rdo_jubilado.AutoSize = True
        Me.rdo_jubilado.Location = New System.Drawing.Point(84, 22)
        Me.rdo_jubilado.Name = "rdo_jubilado"
        Me.rdo_jubilado.Size = New System.Drawing.Size(96, 17)
        Me.rdo_jubilado.TabIndex = 1
        Me.rdo_jubilado.TabStop = True
        Me.rdo_jubilado.Text = "Jubilado (50%)."
        Me.rdo_jubilado.UseVisualStyleBackColor = True
        '
        'rdo_otros
        '
        Me.rdo_otros.AutoSize = True
        Me.rdo_otros.Location = New System.Drawing.Point(183, 23)
        Me.rdo_otros.Name = "rdo_otros"
        Me.rdo_otros.Size = New System.Drawing.Size(156, 17)
        Me.rdo_otros.TabIndex = 2
        Me.rdo_otros.TabStop = True
        Me.rdo_otros.Text = "Menor/fémina/otros (gratis)."
        Me.rdo_otros.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 219)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Dirección:"
        '
        'grp_estado
        '
        Me.grp_estado.Controls.Add(Me.rdo_nopagado)
        Me.grp_estado.Controls.Add(Me.rdo_pagado)
        Me.grp_estado.Location = New System.Drawing.Point(374, 439)
        Me.grp_estado.Name = "grp_estado"
        Me.grp_estado.Size = New System.Drawing.Size(179, 46)
        Me.grp_estado.TabIndex = 36
        Me.grp_estado.TabStop = False
        Me.grp_estado.Text = "Estado:"
        '
        'rdo_pagado
        '
        Me.rdo_pagado.AutoSize = True
        Me.rdo_pagado.Location = New System.Drawing.Point(13, 19)
        Me.rdo_pagado.Name = "rdo_pagado"
        Me.rdo_pagado.Size = New System.Drawing.Size(65, 17)
        Me.rdo_pagado.TabIndex = 0
        Me.rdo_pagado.TabStop = True
        Me.rdo_pagado.Text = "Pagado."
        Me.rdo_pagado.UseVisualStyleBackColor = True
        '
        'rdo_nopagado
        '
        Me.rdo_nopagado.AutoSize = True
        Me.rdo_nopagado.Location = New System.Drawing.Point(84, 19)
        Me.rdo_nopagado.Name = "rdo_nopagado"
        Me.rdo_nopagado.Size = New System.Drawing.Size(81, 17)
        Me.rdo_nopagado.TabIndex = 1
        Me.rdo_nopagado.TabStop = True
        Me.rdo_nopagado.Text = "No pagado."
        Me.rdo_nopagado.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 135)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "DNI:"
        '
        'grp_commnent
        '
        Me.grp_commnent.Controls.Add(Me.txt_coment)
        Me.grp_commnent.Location = New System.Drawing.Point(11, 500)
        Me.grp_commnent.Name = "grp_commnent"
        Me.grp_commnent.Size = New System.Drawing.Size(542, 73)
        Me.grp_commnent.TabIndex = 37
        Me.grp_commnent.TabStop = False
        Me.grp_commnent.Text = "Comentarios:"
        '
        'txt_coment
        '
        Me.txt_coment.Location = New System.Drawing.Point(17, 19)
        Me.txt_coment.Multiline = True
        Me.txt_coment.Name = "txt_coment"
        Me.txt_coment.Size = New System.Drawing.Size(511, 48)
        Me.txt_coment.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(142, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Apellidos:"
        '
        'btn_insertar
        '
        Me.btn_insertar.Location = New System.Drawing.Point(28, 579)
        Me.btn_insertar.Name = "btn_insertar"
        Me.btn_insertar.Size = New System.Drawing.Size(121, 23)
        Me.btn_insertar.TabIndex = 38
        Me.btn_insertar.Text = "Insertar"
        Me.btn_insertar.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Nombre:"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(330, 627)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(110, 23)
        Me.Button2.TabIndex = 39
        Me.Button2.Text = "Imprimir tarjeta"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txt_nsocio
        '
        Me.txt_nsocio.Location = New System.Drawing.Point(9, 55)
        Me.txt_nsocio.Name = "txt_nsocio"
        Me.txt_nsocio.Size = New System.Drawing.Size(100, 20)
        Me.txt_nsocio.TabIndex = 13
        '
        'btn_buscar_apell
        '
        Me.btn_buscar_apell.Location = New System.Drawing.Point(312, 107)
        Me.btn_buscar_apell.Name = "btn_buscar_apell"
        Me.btn_buscar_apell.Size = New System.Drawing.Size(75, 23)
        Me.btn_buscar_apell.TabIndex = 40
        Me.btn_buscar_apell.Text = "Buscar"
        Me.btn_buscar_apell.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Nº de socio:"
        '
        'btn_buscar_dni
        '
        Me.btn_buscar_dni.Location = New System.Drawing.Point(119, 151)
        Me.btn_buscar_dni.Name = "btn_buscar_dni"
        Me.btn_buscar_dni.Size = New System.Drawing.Size(75, 23)
        Me.btn_buscar_dni.TabIndex = 41
        Me.btn_buscar_dni.Text = "Buscar"
        Me.btn_buscar_dni.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(142, 275)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Provincia:"
        '
        'btn_letranif
        '
        Me.btn_letranif.Location = New System.Drawing.Point(209, 151)
        Me.btn_letranif.Name = "btn_letranif"
        Me.btn_letranif.Size = New System.Drawing.Size(107, 23)
        Me.btn_letranif.TabIndex = 42
        Me.btn_letranif.Text = "Calcular letra"
        Me.btn_letranif.UseVisualStyleBackColor = True
        '
        'cmb_prov
        '
        Me.cmb_prov.FormattingEnabled = True
        Me.cmb_prov.Location = New System.Drawing.Point(143, 294)
        Me.cmb_prov.Name = "cmb_prov"
        Me.cmb_prov.Size = New System.Drawing.Size(113, 21)
        Me.cmb_prov.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 279)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Código Postal:"
        '
        'btn_num_libres
        '
        Me.btn_num_libres.Location = New System.Drawing.Point(209, 55)
        Me.btn_num_libres.Name = "btn_num_libres"
        Me.btn_num_libres.Size = New System.Drawing.Size(115, 23)
        Me.btn_num_libres.TabIndex = 44
        Me.btn_num_libres.Text = "Ver Números Libres"
        Me.btn_num_libres.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(327, 219)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Localidad:"
        '
        'txt_cp
        '
        Me.txt_cp.Location = New System.Drawing.Point(15, 295)
        Me.txt_cp.Name = "txt_cp"
        Me.txt_cp.Size = New System.Drawing.Size(100, 20)
        Me.txt_cp.TabIndex = 7
        '
        'cmb_localidad
        '
        Me.cmb_localidad.FormattingEnabled = True
        Me.cmb_localidad.Location = New System.Drawing.Point(330, 236)
        Me.cmb_localidad.Name = "cmb_localidad"
        Me.cmb_localidad.Size = New System.Drawing.Size(121, 21)
        Me.cmb_localidad.TabIndex = 6
        '
        'btn_buscar_nsocio
        '
        Me.btn_buscar_nsocio.Location = New System.Drawing.Point(119, 55)
        Me.btn_buscar_nsocio.Name = "btn_buscar_nsocio"
        Me.btn_buscar_nsocio.Size = New System.Drawing.Size(75, 23)
        Me.btn_buscar_nsocio.TabIndex = 47
        Me.btn_buscar_nsocio.Text = "Buscar"
        Me.btn_buscar_nsocio.UseVisualStyleBackColor = True
        '
        'btn_modificar
        '
        Me.btn_modificar.Location = New System.Drawing.Point(227, 579)
        Me.btn_modificar.Name = "btn_modificar"
        Me.btn_modificar.Size = New System.Drawing.Size(110, 23)
        Me.btn_modificar.TabIndex = 48
        Me.btn_modificar.Text = "Modificar"
        Me.btn_modificar.UseVisualStyleBackColor = True
        '
        'btn_eliminar
        '
        Me.btn_eliminar.Location = New System.Drawing.Point(429, 579)
        Me.btn_eliminar.Name = "btn_eliminar"
        Me.btn_eliminar.Size = New System.Drawing.Size(110, 23)
        Me.btn_eliminar.TabIndex = 49
        Me.btn_eliminar.Text = "Eliminar"
        Me.btn_eliminar.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(103, 627)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(110, 23)
        Me.Button6.TabIndex = 50
        Me.Button6.Text = "Tabla socios"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'txt_dni
        '
        Me.txt_dni.BackColor = System.Drawing.Color.White
        Me.txt_dni.Foco = System.Drawing.Color.Empty
        Me.txt_dni.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txt_dni.Location = New System.Drawing.Point(12, 151)
        Me.txt_dni.Name = "txt_dni"
        Me.txt_dni.Size = New System.Drawing.Size(100, 23)
        Me.txt_dni.TabIndex = 43
        '
        'txt_apellido
        '
        Me.txt_apellido.Location = New System.Drawing.Point(145, 107)
        Me.txt_apellido.Name = "txt_apellido"
        Me.txt_apellido.Size = New System.Drawing.Size(140, 20)
        Me.txt_apellido.TabIndex = 46
        '
        'txt_nombre
        '
        Me.txt_nombre.Location = New System.Drawing.Point(9, 107)
        Me.txt_nombre.Name = "txt_nombre"
        Me.txt_nombre.Size = New System.Drawing.Size(100, 20)
        Me.txt_nombre.TabIndex = 45
        '
        'frm_socio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 662)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.btn_eliminar)
        Me.Controls.Add(Me.btn_modificar)
        Me.Controls.Add(Me.btn_buscar_nsocio)
        Me.Controls.Add(Me.cmb_localidad)
        Me.Controls.Add(Me.txt_dni)
        Me.Controls.Add(Me.txt_apellido)
        Me.Controls.Add(Me.dtpk_fecha_nac)
        Me.Controls.Add(Me.txt_cp)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txt_nombre)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_email)
        Me.Controls.Add(Me.btn_num_libres)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmb_tarjeta)
        Me.Controls.Add(Me.cmb_pais)
        Me.Controls.Add(Me.cmb_prov)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btn_letranif)
        Me.Controls.Add(Me.txt_direcc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.grp_socio)
        Me.Controls.Add(Me.btn_buscar_dni)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.grp_estado)
        Me.Controls.Add(Me.btn_buscar_apell)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_nsocio)
        Me.Controls.Add(Me.grp_commnent)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btn_insertar)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_socio"
        Me.Text = "C.D.B. Pesca Reinosa -Gestion - Socios."
        Me.grp_socio.ResumeLayout(False)
        Me.grp_socio.PerformLayout()
        Me.grp_estado.ResumeLayout(False)
        Me.grp_estado.PerformLayout()
        Me.grp_commnent.ResumeLayout(False)
        Me.grp_commnent.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_buscar_nsocio As Button
    Friend WithEvents cmb_localidad As ComboBox
    Friend WithEvents txt_apellido As textbox_club
    Friend WithEvents txt_cp As TextBox
    Friend WithEvents txt_nombre As textbox_club
    Friend WithEvents Label1 As Label
    Friend WithEvents btn_num_libres As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_dni As textbox_dni
    Friend WithEvents cmb_prov As ComboBox
    Friend WithEvents btn_letranif As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btn_buscar_dni As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents btn_buscar_apell As Button
    Friend WithEvents txt_nsocio As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents btn_insertar As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents grp_commnent As GroupBox
    Friend WithEvents txt_coment As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents grp_estado As GroupBox
    Friend WithEvents rdo_nopagado As RadioButton
    Friend WithEvents rdo_pagado As RadioButton
    Friend WithEvents Label8 As Label
    Friend WithEvents grp_socio As GroupBox
    Friend WithEvents rdo_otros As RadioButton
    Friend WithEvents rdo_jubilado As RadioButton
    Friend WithEvents rdo_normal As RadioButton
    Friend WithEvents txt_direcc As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents cmb_pais As ComboBox
    Friend WithEvents cmb_tarjeta As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txt_email As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents dtpk_fecha_nac As DateTimePicker
    Friend WithEvents btn_modificar As Button
    Friend WithEvents btn_eliminar As Button
    Friend WithEvents Button6 As Button
End Class
