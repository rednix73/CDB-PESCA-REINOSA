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
        Me.grp_socio = New System.Windows.Forms.GroupBox()
        Me.rdo_otros = New System.Windows.Forms.RadioButton()
        Me.rdo_jubilado = New System.Windows.Forms.RadioButton()
        Me.rdo_normal = New System.Windows.Forms.RadioButton()
        Me.grp_estado = New System.Windows.Forms.GroupBox()
        Me.lbl_importe = New System.Windows.Forms.Label()
        Me.rdo_nopagado = New System.Windows.Forms.RadioButton()
        Me.rdo_pagado = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.grp_commnent = New System.Windows.Forms.GroupBox()
        Me.txt_coment = New System.Windows.Forms.TextBox()
        Me.btn_insertar = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txt_nsocio = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_buscar_dni = New System.Windows.Forms.Button()
        Me.btn_letranif = New System.Windows.Forms.Button()
        Me.btn_num_libres = New System.Windows.Forms.Button()
        Me.btn_buscar_nsocio = New System.Windows.Forms.Button()
        Me.btn_modificar = New System.Windows.Forms.Button()
        Me.btn_eliminar = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmb_tarjeta = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txt_nombre = New CLub_Deportivo.textbox_club()
        Me.txt_apellido = New CLub_Deportivo.textbox_club()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_buscar_apell = New System.Windows.Forms.Button()
        Me.txt_dni = New CLub_Deportivo.textbox_dni()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmb_localidad = New System.Windows.Forms.ComboBox()
        Me.txt_cp = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmb_pais = New System.Windows.Forms.ComboBox()
        Me.cmb_prov = New System.Windows.Forms.ComboBox()
        Me.txt_direcc = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grp_socio.SuspendLayout()
        Me.grp_estado.SuspendLayout()
        Me.grp_commnent.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpk_fecha_nac
        '
        Me.dtpk_fecha_nac.Location = New System.Drawing.Point(160, 146)
        Me.dtpk_fecha_nac.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpk_fecha_nac.Name = "dtpk_fecha_nac"
        Me.dtpk_fecha_nac.Size = New System.Drawing.Size(271, 24)
        Me.dtpk_fecha_nac.TabIndex = 26
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 148)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(138, 18)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "Correo Electrónico:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 151)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(152, 18)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Fecha de Nacimiento:"
        '
        'txt_email
        '
        Me.txt_email.Location = New System.Drawing.Point(12, 170)
        Me.txt_email.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_email.Name = "txt_email"
        Me.txt_email.Size = New System.Drawing.Size(271, 24)
        Me.txt_email.TabIndex = 28
        '
        'grp_socio
        '
        Me.grp_socio.Controls.Add(Me.rdo_otros)
        Me.grp_socio.Controls.Add(Me.rdo_jubilado)
        Me.grp_socio.Controls.Add(Me.rdo_normal)
        Me.grp_socio.Location = New System.Drawing.Point(644, 203)
        Me.grp_socio.Margin = New System.Windows.Forms.Padding(4)
        Me.grp_socio.Name = "grp_socio"
        Me.grp_socio.Padding = New System.Windows.Forms.Padding(4)
        Me.grp_socio.Size = New System.Drawing.Size(249, 134)
        Me.grp_socio.TabIndex = 35
        Me.grp_socio.TabStop = False
        Me.grp_socio.Text = "Tipo de socio:"
        '
        'rdo_otros
        '
        Me.rdo_otros.AutoSize = True
        Me.rdo_otros.Location = New System.Drawing.Point(26, 90)
        Me.rdo_otros.Margin = New System.Windows.Forms.Padding(4)
        Me.rdo_otros.Name = "rdo_otros"
        Me.rdo_otros.Size = New System.Drawing.Size(213, 22)
        Me.rdo_otros.TabIndex = 2
        Me.rdo_otros.Text = "Menor/fémina/otros (gratis)."
        Me.rdo_otros.UseVisualStyleBackColor = True
        '
        'rdo_jubilado
        '
        Me.rdo_jubilado.AutoSize = True
        Me.rdo_jubilado.Location = New System.Drawing.Point(26, 60)
        Me.rdo_jubilado.Margin = New System.Windows.Forms.Padding(4)
        Me.rdo_jubilado.Name = "rdo_jubilado"
        Me.rdo_jubilado.Size = New System.Drawing.Size(131, 22)
        Me.rdo_jubilado.TabIndex = 1
        Me.rdo_jubilado.Text = "Jubilado (50%)."
        Me.rdo_jubilado.UseVisualStyleBackColor = True
        '
        'rdo_normal
        '
        Me.rdo_normal.AutoSize = True
        Me.rdo_normal.Checked = True
        Me.rdo_normal.Location = New System.Drawing.Point(26, 30)
        Me.rdo_normal.Margin = New System.Windows.Forms.Padding(4)
        Me.rdo_normal.Name = "rdo_normal"
        Me.rdo_normal.Size = New System.Drawing.Size(82, 22)
        Me.rdo_normal.TabIndex = 0
        Me.rdo_normal.TabStop = True
        Me.rdo_normal.Text = "Normal."
        Me.rdo_normal.UseVisualStyleBackColor = True
        '
        'grp_estado
        '
        Me.grp_estado.Controls.Add(Me.lbl_importe)
        Me.grp_estado.Controls.Add(Me.rdo_nopagado)
        Me.grp_estado.Controls.Add(Me.rdo_pagado)
        Me.grp_estado.Location = New System.Drawing.Point(644, 345)
        Me.grp_estado.Margin = New System.Windows.Forms.Padding(4)
        Me.grp_estado.Name = "grp_estado"
        Me.grp_estado.Padding = New System.Windows.Forms.Padding(4)
        Me.grp_estado.Size = New System.Drawing.Size(249, 87)
        Me.grp_estado.TabIndex = 36
        Me.grp_estado.TabStop = False
        Me.grp_estado.Text = "Pago:"
        '
        'lbl_importe
        '
        Me.lbl_importe.AutoSize = True
        Me.lbl_importe.Location = New System.Drawing.Point(147, 44)
        Me.lbl_importe.Name = "lbl_importe"
        Me.lbl_importe.Size = New System.Drawing.Size(88, 18)
        Me.lbl_importe.TabIndex = 2
        Me.lbl_importe.Text = "Importe: -- €"
        '
        'rdo_nopagado
        '
        Me.rdo_nopagado.AutoSize = True
        Me.rdo_nopagado.Location = New System.Drawing.Point(26, 55)
        Me.rdo_nopagado.Margin = New System.Windows.Forms.Padding(4)
        Me.rdo_nopagado.Name = "rdo_nopagado"
        Me.rdo_nopagado.Size = New System.Drawing.Size(85, 22)
        Me.rdo_nopagado.TabIndex = 1
        Me.rdo_nopagado.Text = "Gratuita."
        Me.rdo_nopagado.UseVisualStyleBackColor = True
        '
        'rdo_pagado
        '
        Me.rdo_pagado.AutoSize = True
        Me.rdo_pagado.Checked = True
        Me.rdo_pagado.Location = New System.Drawing.Point(26, 25)
        Me.rdo_pagado.Margin = New System.Windows.Forms.Padding(4)
        Me.rdo_pagado.Name = "rdo_pagado"
        Me.rdo_pagado.Size = New System.Drawing.Size(84, 22)
        Me.rdo_pagado.TabIndex = 0
        Me.rdo_pagado.TabStop = True
        Me.rdo_pagado.Text = "Pagado."
        Me.rdo_pagado.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(303, 33)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 18)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "DNI:"
        '
        'grp_commnent
        '
        Me.grp_commnent.Controls.Add(Me.txt_coment)
        Me.grp_commnent.Location = New System.Drawing.Point(11, 440)
        Me.grp_commnent.Margin = New System.Windows.Forms.Padding(4)
        Me.grp_commnent.Name = "grp_commnent"
        Me.grp_commnent.Padding = New System.Windows.Forms.Padding(4)
        Me.grp_commnent.Size = New System.Drawing.Size(882, 101)
        Me.grp_commnent.TabIndex = 37
        Me.grp_commnent.TabStop = False
        Me.grp_commnent.Text = "Comentarios:"
        '
        'txt_coment
        '
        Me.txt_coment.Location = New System.Drawing.Point(8, 25)
        Me.txt_coment.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_coment.Multiline = True
        Me.txt_coment.Name = "txt_coment"
        Me.txt_coment.Size = New System.Drawing.Size(864, 65)
        Me.txt_coment.TabIndex = 0
        '
        'btn_insertar
        '
        Me.btn_insertar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_insertar.Location = New System.Drawing.Point(24, 549)
        Me.btn_insertar.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_insertar.Name = "btn_insertar"
        Me.btn_insertar.Size = New System.Drawing.Size(181, 32)
        Me.btn_insertar.TabIndex = 38
        Me.btn_insertar.Text = "Insertar"
        Me.btn_insertar.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(455, 600)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(438, 48)
        Me.Button2.TabIndex = 39
        Me.Button2.Text = "Imprimir tarjeta"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txt_nsocio
        '
        Me.txt_nsocio.Location = New System.Drawing.Point(8, 44)
        Me.txt_nsocio.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_nsocio.Name = "txt_nsocio"
        Me.txt_nsocio.Size = New System.Drawing.Size(65, 24)
        Me.txt_nsocio.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 22)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 18)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Número:"
        '
        'btn_buscar_dni
        '
        Me.btn_buscar_dni.Location = New System.Drawing.Point(341, 58)
        Me.btn_buscar_dni.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_buscar_dni.Name = "btn_buscar_dni"
        Me.btn_buscar_dni.Size = New System.Drawing.Size(148, 32)
        Me.btn_buscar_dni.TabIndex = 41
        Me.btn_buscar_dni.Text = "Buscar"
        Me.btn_buscar_dni.UseVisualStyleBackColor = True
        '
        'btn_letranif
        '
        Me.btn_letranif.Location = New System.Drawing.Point(488, 33)
        Me.btn_letranif.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_letranif.Name = "btn_letranif"
        Me.btn_letranif.Size = New System.Drawing.Size(105, 57)
        Me.btn_letranif.TabIndex = 42
        Me.btn_letranif.Text = "Calcular letra"
        Me.btn_letranif.UseVisualStyleBackColor = True
        '
        'btn_num_libres
        '
        Me.btn_num_libres.Location = New System.Drawing.Point(81, 47)
        Me.btn_num_libres.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_num_libres.Name = "btn_num_libres"
        Me.btn_num_libres.Size = New System.Drawing.Size(172, 29)
        Me.btn_num_libres.TabIndex = 44
        Me.btn_num_libres.Text = "Ver Números Libres"
        Me.btn_num_libres.UseVisualStyleBackColor = True
        '
        'btn_buscar_nsocio
        '
        Me.btn_buscar_nsocio.Location = New System.Drawing.Point(81, 22)
        Me.btn_buscar_nsocio.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_buscar_nsocio.Name = "btn_buscar_nsocio"
        Me.btn_buscar_nsocio.Size = New System.Drawing.Size(172, 25)
        Me.btn_buscar_nsocio.TabIndex = 47
        Me.btn_buscar_nsocio.Text = "Buscar"
        Me.btn_buscar_nsocio.UseVisualStyleBackColor = True
        '
        'btn_modificar
        '
        Me.btn_modificar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_modificar.Location = New System.Drawing.Point(367, 549)
        Me.btn_modificar.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_modificar.Name = "btn_modificar"
        Me.btn_modificar.Size = New System.Drawing.Size(165, 32)
        Me.btn_modificar.TabIndex = 48
        Me.btn_modificar.Text = "Modificar"
        Me.btn_modificar.UseVisualStyleBackColor = True
        '
        'btn_eliminar
        '
        Me.btn_eliminar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_eliminar.Location = New System.Drawing.Point(714, 549)
        Me.btn_eliminar.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_eliminar.Name = "btn_eliminar"
        Me.btn_eliminar.Size = New System.Drawing.Size(165, 32)
        Me.btn_eliminar.TabIndex = 49
        Me.btn_eliminar.Text = "Eliminar"
        Me.btn_eliminar.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Location = New System.Drawing.Point(11, 600)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(436, 48)
        Me.Button6.TabIndex = 50
        Me.Button6.Text = "Tabla socios"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmb_tarjeta)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txt_nsocio)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btn_buscar_nsocio)
        Me.GroupBox1.Controls.Add(Me.btn_num_libres)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(265, 184)
        Me.GroupBox1.TabIndex = 51
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Socio"
        '
        'cmb_tarjeta
        '
        Me.cmb_tarjeta.FormattingEnabled = True
        Me.cmb_tarjeta.Items.AddRange(New Object() {"SALMON", "TRUCHA"})
        Me.cmb_tarjeta.Location = New System.Drawing.Point(10, 125)
        Me.cmb_tarjeta.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb_tarjeta.Name = "cmb_tarjeta"
        Me.cmb_tarjeta.Size = New System.Drawing.Size(180, 26)
        Me.cmb_tarjeta.TabIndex = 48
        Me.cmb_tarjeta.Text = "Seleccionar..."
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(7, 103)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(105, 18)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "Tipo de tarjeta:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txt_nombre)
        Me.GroupBox2.Controls.Add(Me.txt_apellido)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.btn_buscar_apell)
        Me.GroupBox2.Controls.Add(Me.btn_letranif)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txt_dni)
        Me.GroupBox2.Controls.Add(Me.btn_buscar_dni)
        Me.GroupBox2.Controls.Add(Me.dtpk_fecha_nac)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(286, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(605, 184)
        Me.GroupBox2.TabIndex = 52
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos personales"
        '
        'txt_nombre
        '
        Me.txt_nombre.Location = New System.Drawing.Point(81, 33)
        Me.txt_nombre.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_nombre.Name = "txt_nombre"
        Me.txt_nombre.Size = New System.Drawing.Size(208, 24)
        Me.txt_nombre.TabIndex = 56
        '
        'txt_apellido
        '
        Me.txt_apellido.Location = New System.Drawing.Point(81, 73)
        Me.txt_apellido.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_apellido.Name = "txt_apellido"
        Me.txt_apellido.Size = New System.Drawing.Size(208, 24)
        Me.txt_apellido.TabIndex = 57
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 33)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 18)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Nombre:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 76)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 18)
        Me.Label6.TabIndex = 53
        Me.Label6.Text = "Apellidos:"
        '
        'btn_buscar_apell
        '
        Me.btn_buscar_apell.Location = New System.Drawing.Point(81, 96)
        Me.btn_buscar_apell.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_buscar_apell.Name = "btn_buscar_apell"
        Me.btn_buscar_apell.Size = New System.Drawing.Size(208, 25)
        Me.btn_buscar_apell.TabIndex = 55
        Me.btn_buscar_apell.Text = "Buscar"
        Me.btn_buscar_apell.UseVisualStyleBackColor = True
        '
        'txt_dni
        '
        Me.txt_dni.BackColor = System.Drawing.Color.White
        Me.txt_dni.Foco = System.Drawing.Color.Empty
        Me.txt_dni.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txt_dni.Location = New System.Drawing.Point(341, 33)
        Me.txt_dni.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_dni.Name = "txt_dni"
        Me.txt_dni.Size = New System.Drawing.Size(148, 27)
        Me.txt_dni.TabIndex = 43
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.cmb_localidad)
        Me.GroupBox3.Controls.Add(Me.txt_cp)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.txt_email)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.cmb_pais)
        Me.GroupBox3.Controls.Add(Me.cmb_prov)
        Me.GroupBox3.Controls.Add(Me.txt_direcc)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 202)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(625, 230)
        Me.GroupBox3.TabIndex = 53
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Domicilio"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 30)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 18)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Dirección:"
        '
        'cmb_localidad
        '
        Me.cmb_localidad.FormattingEnabled = True
        Me.cmb_localidad.Location = New System.Drawing.Point(433, 52)
        Me.cmb_localidad.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb_localidad.Name = "cmb_localidad"
        Me.cmb_localidad.Size = New System.Drawing.Size(180, 26)
        Me.cmb_localidad.TabIndex = 55
        '
        'txt_cp
        '
        Me.txt_cp.Location = New System.Drawing.Point(12, 111)
        Me.txt_cp.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_cp.Name = "txt_cp"
        Me.txt_cp.Size = New System.Drawing.Size(148, 24)
        Me.txt_cp.TabIndex = 56
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(428, 30)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 18)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Localidad:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(428, 89)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 18)
        Me.Label9.TabIndex = 63
        Me.Label9.Text = "País:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 89)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 18)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Código Postal:"
        '
        'cmb_pais
        '
        Me.cmb_pais.FormattingEnabled = True
        Me.cmb_pais.Items.AddRange(New Object() {"ESPAÑA", "UNION EUROPEA", "OTROS"})
        Me.cmb_pais.Location = New System.Drawing.Point(433, 111)
        Me.cmb_pais.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb_pais.Name = "cmb_pais"
        Me.cmb_pais.Size = New System.Drawing.Size(180, 26)
        Me.cmb_pais.TabIndex = 62
        Me.cmb_pais.Text = "Seleccionar..."
        '
        'cmb_prov
        '
        Me.cmb_prov.FormattingEnabled = True
        Me.cmb_prov.Location = New System.Drawing.Point(205, 111)
        Me.cmb_prov.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb_prov.Name = "cmb_prov"
        Me.cmb_prov.Size = New System.Drawing.Size(167, 26)
        Me.cmb_prov.TabIndex = 59
        '
        'txt_direcc
        '
        Me.txt_direcc.Location = New System.Drawing.Point(10, 52)
        Me.txt_direcc.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_direcc.Name = "txt_direcc"
        Me.txt_direcc.Size = New System.Drawing.Size(403, 24)
        Me.txt_direcc.TabIndex = 61
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(202, 89)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 18)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Provincia:"
        '
        'frm_socio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(909, 658)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.btn_eliminar)
        Me.Controls.Add(Me.btn_modificar)
        Me.Controls.Add(Me.grp_socio)
        Me.Controls.Add(Me.grp_estado)
        Me.Controls.Add(Me.grp_commnent)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btn_insertar)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frm_socio"
        Me.Text = "C.D.B. Pesca Reinosa -Gestion - Socios."
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grp_socio.ResumeLayout(False)
        Me.grp_socio.PerformLayout()
        Me.grp_estado.ResumeLayout(False)
        Me.grp_estado.PerformLayout()
        Me.grp_commnent.ResumeLayout(False)
        Me.grp_commnent.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_buscar_nsocio As Button
    Friend WithEvents btn_num_libres As Button
    Friend WithEvents txt_dni As textbox_dni
    Friend WithEvents btn_letranif As Button
    Friend WithEvents btn_buscar_dni As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txt_nsocio As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents btn_insertar As Button
    Friend WithEvents grp_commnent As GroupBox
    Friend WithEvents txt_coment As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents grp_estado As GroupBox
    Friend WithEvents rdo_nopagado As RadioButton
    Friend WithEvents rdo_pagado As RadioButton
    Friend WithEvents grp_socio As GroupBox
    Friend WithEvents rdo_otros As RadioButton
    Friend WithEvents rdo_jubilado As RadioButton
    Friend WithEvents rdo_normal As RadioButton
    Friend WithEvents txt_email As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents dtpk_fecha_nac As DateTimePicker
    Friend WithEvents btn_modificar As Button
    Friend WithEvents btn_eliminar As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txt_nombre As textbox_club
    Friend WithEvents txt_apellido As textbox_club
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btn_buscar_apell As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents cmb_localidad As ComboBox
    Friend WithEvents txt_cp As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmb_pais As ComboBox
    Friend WithEvents cmb_prov As ComboBox
    Friend WithEvents txt_direcc As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmb_tarjeta As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents lbl_importe As Label
End Class
