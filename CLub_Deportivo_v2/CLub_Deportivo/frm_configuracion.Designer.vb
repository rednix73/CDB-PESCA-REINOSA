<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_configuracion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_configuracion))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txt_tarjeta_trucha = New System.Windows.Forms.TextBox()
        Me.txt_tarjeta_salmon = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btn_reverso = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pctbox_tsocio_reverso = New System.Windows.Forms.PictureBox()
        Me.btn_anverso = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pctbox_tsocio_anverso = New System.Windows.Forms.PictureBox()
        Me.cmb_temporada = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.grpbox_excel = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_tabla_bdsocios_xls = New System.Windows.Forms.TextBox()
        Me.txt_tabla_socios_xls = New System.Windows.Forms.TextBox()
        Me.txt_dsn = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_archivo_excel = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.grpbox_mysql = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txt_tabla_bdsocios_mysql = New System.Windows.Forms.TextBox()
        Me.txt_tabla_socios_mysql = New System.Windows.Forms.TextBox()
        Me.txt_password = New System.Windows.Forms.TextBox()
        Me.txt_user = New System.Windows.Forms.TextBox()
        Me.txt_bbdd = New System.Windows.Forms.TextBox()
        Me.txt_port = New System.Windows.Forms.TextBox()
        Me.txt_server = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmb_bd = New System.Windows.Forms.ComboBox()
        Me.lbl_bd = New System.Windows.Forms.Label()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.btn_cerrar = New System.Windows.Forms.Button()
        Me.OpenFile_anverso = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFile_reverso = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFile_bbdd = New System.Windows.Forms.OpenFileDialog()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.pctbox_tsocio_reverso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctbox_tsocio_anverso, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.grpbox_excel.SuspendLayout()
        Me.grpbox_mysql.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(776, 412)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txt_tarjeta_trucha)
        Me.TabPage1.Controls.Add(Me.txt_tarjeta_salmon)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.btn_reverso)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.pctbox_tsocio_reverso)
        Me.TabPage1.Controls.Add(Me.btn_anverso)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.pctbox_tsocio_anverso)
        Me.TabPage1.Controls.Add(Me.cmb_temporada)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(768, 379)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txt_tarjeta_trucha
        '
        Me.txt_tarjeta_trucha.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.txt_tarjeta_trucha.Location = New System.Drawing.Point(677, 62)
        Me.txt_tarjeta_trucha.Name = "txt_tarjeta_trucha"
        Me.txt_tarjeta_trucha.Size = New System.Drawing.Size(66, 25)
        Me.txt_tarjeta_trucha.TabIndex = 12
        '
        'txt_tarjeta_salmon
        '
        Me.txt_tarjeta_salmon.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.txt_tarjeta_salmon.Location = New System.Drawing.Point(262, 62)
        Me.txt_tarjeta_salmon.Name = "txt_tarjeta_salmon"
        Me.txt_tarjeta_salmon.Size = New System.Drawing.Size(67, 25)
        Me.txt_tarjeta_salmon.TabIndex = 11
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label20.Location = New System.Drawing.Point(439, 65)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(200, 20)
        Me.Label20.TabIndex = 10
        Me.Label20.Text = "Precio tarjeta de trucha (€):"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.Label19.Location = New System.Drawing.Point(23, 65)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(206, 20)
        Me.Label19.TabIndex = 9
        Me.Label19.Text = "Precio tarjeta de salmón (€):"
        '
        'btn_reverso
        '
        Me.btn_reverso.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_reverso.Location = New System.Drawing.Point(443, 322)
        Me.btn_reverso.Name = "btn_reverso"
        Me.btn_reverso.Size = New System.Drawing.Size(300, 36)
        Me.btn_reverso.TabIndex = 8
        Me.btn_reverso.Text = "Seleccionar Imagen"
        Me.btn_reverso.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(439, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(202, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Tarjeta de socio (reverso)"
        '
        'pctbox_tsocio_reverso
        '
        Me.pctbox_tsocio_reverso.Location = New System.Drawing.Point(443, 142)
        Me.pctbox_tsocio_reverso.Name = "pctbox_tsocio_reverso"
        Me.pctbox_tsocio_reverso.Size = New System.Drawing.Size(300, 180)
        Me.pctbox_tsocio_reverso.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pctbox_tsocio_reverso.TabIndex = 6
        Me.pctbox_tsocio_reverso.TabStop = False
        '
        'btn_anverso
        '
        Me.btn_anverso.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_anverso.Location = New System.Drawing.Point(27, 322)
        Me.btn_anverso.Name = "btn_anverso"
        Me.btn_anverso.Size = New System.Drawing.Size(300, 36)
        Me.btn_anverso.TabIndex = 5
        Me.btn_anverso.Text = "Seleccionar Imagen"
        Me.btn_anverso.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(205, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Tarjeta de socio (anverso)"
        '
        'pctbox_tsocio_anverso
        '
        Me.pctbox_tsocio_anverso.Location = New System.Drawing.Point(27, 142)
        Me.pctbox_tsocio_anverso.Name = "pctbox_tsocio_anverso"
        Me.pctbox_tsocio_anverso.Size = New System.Drawing.Size(300, 180)
        Me.pctbox_tsocio_anverso.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pctbox_tsocio_anverso.TabIndex = 3
        Me.pctbox_tsocio_anverso.TabStop = False
        '
        'cmb_temporada
        '
        Me.cmb_temporada.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_temporada.FormattingEnabled = True
        Me.cmb_temporada.Items.AddRange(New Object() {"2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030"})
        Me.cmb_temporada.Location = New System.Drawing.Point(128, 19)
        Me.cmb_temporada.Name = "cmb_temporada"
        Me.cmb_temporada.Size = New System.Drawing.Size(121, 28)
        Me.cmb_temporada.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Temporada:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.grpbox_excel)
        Me.TabPage2.Controls.Add(Me.grpbox_mysql)
        Me.TabPage2.Controls.Add(Me.cmb_bd)
        Me.TabPage2.Controls.Add(Me.lbl_bd)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(768, 379)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Base de datos"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'grpbox_excel
        '
        Me.grpbox_excel.Controls.Add(Me.Label7)
        Me.grpbox_excel.Controls.Add(Me.Label6)
        Me.grpbox_excel.Controls.Add(Me.Label5)
        Me.grpbox_excel.Controls.Add(Me.txt_tabla_bdsocios_xls)
        Me.grpbox_excel.Controls.Add(Me.txt_tabla_socios_xls)
        Me.grpbox_excel.Controls.Add(Me.txt_dsn)
        Me.grpbox_excel.Controls.Add(Me.Label15)
        Me.grpbox_excel.Controls.Add(Me.Label14)
        Me.grpbox_excel.Controls.Add(Me.Label13)
        Me.grpbox_excel.Controls.Add(Me.txt_archivo_excel)
        Me.grpbox_excel.Controls.Add(Me.Label4)
        Me.grpbox_excel.Controls.Add(Me.Button3)
        Me.grpbox_excel.Location = New System.Drawing.Point(9, 49)
        Me.grpbox_excel.Name = "grpbox_excel"
        Me.grpbox_excel.Size = New System.Drawing.Size(432, 310)
        Me.grpbox_excel.TabIndex = 20
        Me.grpbox_excel.TabStop = False
        Me.grpbox_excel.Text = "Excel-ODBC"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(0, 180)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(246, 20)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Indique el nombre de las tablas:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(-1, 243)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(265, 20)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Tabla de base de datos de socios:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(0, 210)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(132, 20)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Tabla de socios:"
        '
        'txt_tabla_bdsocios_xls
        '
        Me.txt_tabla_bdsocios_xls.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tabla_bdsocios_xls.Location = New System.Drawing.Point(251, 268)
        Me.txt_tabla_bdsocios_xls.Name = "txt_tabla_bdsocios_xls"
        Me.txt_tabla_bdsocios_xls.Size = New System.Drawing.Size(175, 24)
        Me.txt_tabla_bdsocios_xls.TabIndex = 25
        '
        'txt_tabla_socios_xls
        '
        Me.txt_tabla_socios_xls.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tabla_socios_xls.Location = New System.Drawing.Point(251, 207)
        Me.txt_tabla_socios_xls.Name = "txt_tabla_socios_xls"
        Me.txt_tabla_socios_xls.Size = New System.Drawing.Size(175, 24)
        Me.txt_tabla_socios_xls.TabIndex = 24
        '
        'txt_dsn
        '
        Me.txt_dsn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_dsn.Location = New System.Drawing.Point(53, 142)
        Me.txt_dsn.Name = "txt_dsn"
        Me.txt_dsn.Size = New System.Drawing.Size(193, 24)
        Me.txt_dsn.TabIndex = 23
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(0, 144)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(50, 20)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = "DSN:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(-8, 118)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(428, 17)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = " Recuerde crear un conector ODBC de 32 bits e indicar el nombre:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 93)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(416, 17)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "El archivo se copiara a la carpeta de instalación de la aplicación."
        '
        'txt_archivo_excel
        '
        Me.txt_archivo_excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_archivo_excel.Location = New System.Drawing.Point(4, 64)
        Me.txt_archivo_excel.Name = "txt_archivo_excel"
        Me.txt_archivo_excel.Size = New System.Drawing.Size(305, 24)
        Me.txt_archivo_excel.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(203, 20)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Archivo de base de datos:"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(315, 62)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(105, 28)
        Me.Button3.TabIndex = 17
        Me.Button3.Text = "Examinar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'grpbox_mysql
        '
        Me.grpbox_mysql.Controls.Add(Me.Label16)
        Me.grpbox_mysql.Controls.Add(Me.Label17)
        Me.grpbox_mysql.Controls.Add(Me.Label18)
        Me.grpbox_mysql.Controls.Add(Me.txt_tabla_bdsocios_mysql)
        Me.grpbox_mysql.Controls.Add(Me.txt_tabla_socios_mysql)
        Me.grpbox_mysql.Controls.Add(Me.txt_password)
        Me.grpbox_mysql.Controls.Add(Me.txt_user)
        Me.grpbox_mysql.Controls.Add(Me.txt_bbdd)
        Me.grpbox_mysql.Controls.Add(Me.txt_port)
        Me.grpbox_mysql.Controls.Add(Me.txt_server)
        Me.grpbox_mysql.Controls.Add(Me.Label12)
        Me.grpbox_mysql.Controls.Add(Me.Label11)
        Me.grpbox_mysql.Controls.Add(Me.Label10)
        Me.grpbox_mysql.Controls.Add(Me.Label9)
        Me.grpbox_mysql.Controls.Add(Me.Label8)
        Me.grpbox_mysql.Location = New System.Drawing.Point(447, 48)
        Me.grpbox_mysql.Name = "grpbox_mysql"
        Me.grpbox_mysql.Size = New System.Drawing.Size(303, 311)
        Me.grpbox_mysql.TabIndex = 19
        Me.grpbox_mysql.TabStop = False
        Me.grpbox_mysql.Text = "Mysql"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(0, 181)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(246, 20)
        Me.Label16.TabIndex = 33
        Me.Label16.Text = "Indique el nombre de las tablas:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(0, 246)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(265, 20)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Tabla de base de datos de socios:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(0, 211)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(132, 20)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "Tabla de socios:"
        '
        'txt_tabla_bdsocios_mysql
        '
        Me.txt_tabla_bdsocios_mysql.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tabla_bdsocios_mysql.Location = New System.Drawing.Point(147, 269)
        Me.txt_tabla_bdsocios_mysql.Name = "txt_tabla_bdsocios_mysql"
        Me.txt_tabla_bdsocios_mysql.Size = New System.Drawing.Size(150, 24)
        Me.txt_tabla_bdsocios_mysql.TabIndex = 30
        '
        'txt_tabla_socios_mysql
        '
        Me.txt_tabla_socios_mysql.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tabla_socios_mysql.Location = New System.Drawing.Point(147, 211)
        Me.txt_tabla_socios_mysql.Name = "txt_tabla_socios_mysql"
        Me.txt_tabla_socios_mysql.Size = New System.Drawing.Size(150, 24)
        Me.txt_tabla_socios_mysql.TabIndex = 29
        '
        'txt_password
        '
        Me.txt_password.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_password.Location = New System.Drawing.Point(135, 149)
        Me.txt_password.Name = "txt_password"
        Me.txt_password.Size = New System.Drawing.Size(162, 24)
        Me.txt_password.TabIndex = 23
        '
        'txt_user
        '
        Me.txt_user.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_user.Location = New System.Drawing.Point(135, 116)
        Me.txt_user.Name = "txt_user"
        Me.txt_user.Size = New System.Drawing.Size(162, 24)
        Me.txt_user.TabIndex = 23
        '
        'txt_bbdd
        '
        Me.txt_bbdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_bbdd.Location = New System.Drawing.Point(135, 83)
        Me.txt_bbdd.Name = "txt_bbdd"
        Me.txt_bbdd.Size = New System.Drawing.Size(162, 24)
        Me.txt_bbdd.TabIndex = 23
        '
        'txt_port
        '
        Me.txt_port.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_port.Location = New System.Drawing.Point(135, 52)
        Me.txt_port.Name = "txt_port"
        Me.txt_port.Size = New System.Drawing.Size(68, 24)
        Me.txt_port.TabIndex = 22
        '
        'txt_server
        '
        Me.txt_server.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_server.Location = New System.Drawing.Point(135, 23)
        Me.txt_server.Name = "txt_server"
        Me.txt_server.Size = New System.Drawing.Size(162, 24)
        Me.txt_server.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(0, 152)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(100, 20)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Contraseña:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(0, 119)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 20)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Usuario:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(0, 87)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(122, 20)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "Base de datos:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(0, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 20)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Puerto:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(0, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 20)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Servidor:"
        '
        'cmb_bd
        '
        Me.cmb_bd.FormattingEnabled = True
        Me.cmb_bd.Items.AddRange(New Object() {"Excel_ODBC", "MySQL"})
        Me.cmb_bd.Location = New System.Drawing.Point(191, 14)
        Me.cmb_bd.Name = "cmb_bd"
        Me.cmb_bd.Size = New System.Drawing.Size(152, 28)
        Me.cmb_bd.TabIndex = 14
        '
        'lbl_bd
        '
        Me.lbl_bd.AutoSize = True
        Me.lbl_bd.Location = New System.Drawing.Point(6, 17)
        Me.lbl_bd.Name = "lbl_bd"
        Me.lbl_bd.Size = New System.Drawing.Size(179, 20)
        Me.lbl_bd.TabIndex = 13
        Me.lbl_bd.Text = "Tipo de base de datos:"
        '
        'btn_guardar
        '
        Me.btn_guardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_guardar.Location = New System.Drawing.Point(647, 430)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(105, 28)
        Me.btn_guardar.TabIndex = 18
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'btn_cerrar
        '
        Me.btn_cerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cerrar.Location = New System.Drawing.Point(49, 430)
        Me.btn_cerrar.Name = "btn_cerrar"
        Me.btn_cerrar.Size = New System.Drawing.Size(105, 28)
        Me.btn_cerrar.TabIndex = 19
        Me.btn_cerrar.Text = "Cerrar"
        Me.btn_cerrar.UseVisualStyleBackColor = True
        '
        'OpenFile_anverso
        '
        Me.OpenFile_anverso.FileName = "OpenFileDialog1"
        '
        'OpenFile_reverso
        '
        Me.OpenFile_reverso.FileName = "OpenFileDialog1"
        '
        'OpenFile_bbdd
        '
        Me.OpenFile_bbdd.FileName = "OpenFileDialog1"
        '
        'frm_configuracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 469)
        Me.Controls.Add(Me.btn_cerrar)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_configuracion"
        Me.Text = "  C.D.B. pesca Reinosa - Configuración"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.pctbox_tsocio_reverso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctbox_tsocio_anverso, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.grpbox_excel.ResumeLayout(False)
        Me.grpbox_excel.PerformLayout()
        Me.grpbox_mysql.ResumeLayout(False)
        Me.grpbox_mysql.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btn_reverso As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents pctbox_tsocio_reverso As PictureBox
    Friend WithEvents btn_anverso As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents pctbox_tsocio_anverso As PictureBox
    Friend WithEvents cmb_temporada As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_archivo_excel As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents cmb_bd As ComboBox
    Friend WithEvents lbl_bd As Label
    Friend WithEvents btn_guardar As Button
    Friend WithEvents btn_cerrar As Button
    Friend WithEvents grpbox_mysql As GroupBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_password As TextBox
    Friend WithEvents txt_user As TextBox
    Friend WithEvents txt_bbdd As TextBox
    Friend WithEvents txt_port As TextBox
    Friend WithEvents txt_server As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents grpbox_excel As GroupBox
    Friend WithEvents txt_dsn As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_tabla_bdsocios_xls As TextBox
    Friend WithEvents txt_tabla_socios_xls As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents txt_tabla_bdsocios_mysql As TextBox
    Friend WithEvents txt_tabla_socios_mysql As TextBox
    Friend WithEvents OpenFile_anverso As OpenFileDialog
    Friend WithEvents txt_tarjeta_trucha As TextBox
    Friend WithEvents txt_tarjeta_salmon As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents OpenFile_reverso As OpenFileDialog
    Friend WithEvents OpenFile_bbdd As OpenFileDialog
End Class
