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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
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
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.grpbox_excel.SuspendLayout()
        Me.grpbox_mysql.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(776, 412)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.PictureBox2)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.PictureBox1)
        Me.TabPage1.Controls.Add(Me.cmb_temporada)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(768, 383)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(359, 311)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(221, 26)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Seleccionar Imagen"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(356, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(172, 17)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Tarjeta de socio (reverso)"
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(359, 177)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(221, 134)
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(93, 311)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(221, 26)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Seleccionar Imagen"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(90, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(175, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Tarjeta de socio (anverso)"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(93, 177)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(221, 134)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'cmb_temporada
        '
        Me.cmb_temporada.FormattingEnabled = True
        Me.cmb_temporada.Items.AddRange(New Object() {"2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030"})
        Me.cmb_temporada.Location = New System.Drawing.Point(407, 18)
        Me.cmb_temporada.Name = "cmb_temporada"
        Me.cmb_temporada.Size = New System.Drawing.Size(121, 24)
        Me.cmb_temporada.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(316, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Temporada:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.grpbox_excel)
        Me.TabPage2.Controls.Add(Me.grpbox_mysql)
        Me.TabPage2.Controls.Add(Me.cmb_bd)
        Me.TabPage2.Controls.Add(Me.lbl_bd)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(768, 383)
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
        Me.grpbox_excel.Size = New System.Drawing.Size(432, 301)
        Me.grpbox_excel.TabIndex = 20
        Me.grpbox_excel.TabStop = False
        Me.grpbox_excel.Text = "Excel-ODBC"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 180)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(209, 17)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Indique el nombre de las tablas:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 248)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(226, 17)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Tabla de base de datos de socios:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 210)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 17)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Tabla de socios:"
        '
        'txt_tabla_bdsocios_xls
        '
        Me.txt_tabla_bdsocios_xls.Location = New System.Drawing.Point(238, 248)
        Me.txt_tabla_bdsocios_xls.Name = "txt_tabla_bdsocios_xls"
        Me.txt_tabla_bdsocios_xls.Size = New System.Drawing.Size(162, 22)
        Me.txt_tabla_bdsocios_xls.TabIndex = 25
        '
        'txt_tabla_socios_xls
        '
        Me.txt_tabla_socios_xls.Location = New System.Drawing.Point(238, 210)
        Me.txt_tabla_socios_xls.Name = "txt_tabla_socios_xls"
        Me.txt_tabla_socios_xls.Size = New System.Drawing.Size(162, 22)
        Me.txt_tabla_socios_xls.TabIndex = 24
        '
        'txt_dsn
        '
        Me.txt_dsn.Location = New System.Drawing.Point(53, 142)
        Me.txt_dsn.Name = "txt_dsn"
        Me.txt_dsn.Size = New System.Drawing.Size(98, 22)
        Me.txt_dsn.TabIndex = 23
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 144)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(41, 17)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = "DSN:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 118)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(428, 17)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = " Recuerde crear un conector ODBC de 32 bits e indicar el nombre:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 93)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(416, 17)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "El archivo se copiara a la carpeta de isntalación de la aplicación."
        '
        'txt_archivo_excel
        '
        Me.txt_archivo_excel.Location = New System.Drawing.Point(185, 33)
        Me.txt_archivo_excel.Name = "txt_archivo_excel"
        Me.txt_archivo_excel.Size = New System.Drawing.Size(226, 22)
        Me.txt_archivo_excel.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(173, 17)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Archivo de base de datos:"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(185, 62)
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
        Me.grpbox_mysql.Location = New System.Drawing.Point(457, 48)
        Me.grpbox_mysql.Name = "grpbox_mysql"
        Me.grpbox_mysql.Size = New System.Drawing.Size(293, 302)
        Me.grpbox_mysql.TabIndex = 19
        Me.grpbox_mysql.TabStop = False
        Me.grpbox_mysql.Text = "Mysql"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 181)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(209, 17)
        Me.Label16.TabIndex = 33
        Me.Label16.Text = "Indique el nombre de las tablas:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 249)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(226, 17)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Tabla de base de datos de socios:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 211)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(112, 17)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "Tabla de socios:"
        '
        'txt_tabla_bdsocios_mysql
        '
        Me.txt_tabla_bdsocios_mysql.Location = New System.Drawing.Point(118, 269)
        Me.txt_tabla_bdsocios_mysql.Name = "txt_tabla_bdsocios_mysql"
        Me.txt_tabla_bdsocios_mysql.Size = New System.Drawing.Size(162, 22)
        Me.txt_tabla_bdsocios_mysql.TabIndex = 30
        '
        'txt_tabla_socios_mysql
        '
        Me.txt_tabla_socios_mysql.Location = New System.Drawing.Point(121, 211)
        Me.txt_tabla_socios_mysql.Name = "txt_tabla_socios_mysql"
        Me.txt_tabla_socios_mysql.Size = New System.Drawing.Size(162, 22)
        Me.txt_tabla_socios_mysql.TabIndex = 29
        '
        'txt_password
        '
        Me.txt_password.Location = New System.Drawing.Point(120, 145)
        Me.txt_password.Name = "txt_password"
        Me.txt_password.Size = New System.Drawing.Size(162, 22)
        Me.txt_password.TabIndex = 23
        '
        'txt_user
        '
        Me.txt_user.Location = New System.Drawing.Point(120, 116)
        Me.txt_user.Name = "txt_user"
        Me.txt_user.Size = New System.Drawing.Size(162, 22)
        Me.txt_user.TabIndex = 23
        '
        'txt_bbdd
        '
        Me.txt_bbdd.Location = New System.Drawing.Point(118, 84)
        Me.txt_bbdd.Name = "txt_bbdd"
        Me.txt_bbdd.Size = New System.Drawing.Size(162, 22)
        Me.txt_bbdd.TabIndex = 23
        '
        'txt_port
        '
        Me.txt_port.Location = New System.Drawing.Point(121, 52)
        Me.txt_port.Name = "txt_port"
        Me.txt_port.Size = New System.Drawing.Size(68, 22)
        Me.txt_port.TabIndex = 22
        '
        'txt_server
        '
        Me.txt_server.Location = New System.Drawing.Point(120, 23)
        Me.txt_server.Name = "txt_server"
        Me.txt_server.Size = New System.Drawing.Size(162, 22)
        Me.txt_server.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 148)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 17)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Contraseña:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 119)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(61, 17)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Usuario:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 87)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(103, 17)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "Base de datos:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(54, 17)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Puerto:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 17)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Servidor:"
        '
        'cmb_bd
        '
        Me.cmb_bd.FormattingEnabled = True
        Me.cmb_bd.Items.AddRange(New Object() {"Excel_ODBC", "MySQL"})
        Me.cmb_bd.Location = New System.Drawing.Point(166, 14)
        Me.cmb_bd.Name = "cmb_bd"
        Me.cmb_bd.Size = New System.Drawing.Size(133, 24)
        Me.cmb_bd.TabIndex = 14
        '
        'lbl_bd
        '
        Me.lbl_bd.AutoSize = True
        Me.lbl_bd.Location = New System.Drawing.Point(6, 17)
        Me.lbl_bd.Name = "lbl_bd"
        Me.lbl_bd.Size = New System.Drawing.Size(154, 17)
        Me.lbl_bd.TabIndex = 13
        Me.lbl_bd.Text = "Tipo de base de datos:"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(648, 430)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(105, 28)
        Me.Button4.TabIndex = 18
        Me.Button4.Text = "Guardar"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(50, 430)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(105, 28)
        Me.Button5.TabIndex = 19
        Me.Button5.Text = "Cerrar"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'frm_configuracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 469)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_configuracion"
        Me.Text = " C.D.B. pesca Reinosa - Configuracion"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents cmb_temporada As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_archivo_excel As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents cmb_bd As ComboBox
    Friend WithEvents lbl_bd As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
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
End Class
