namespace Cosolem
{
    partial class frmCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCliente));
            this.cmbTipoIdentificacion = new System.Windows.Forms.ComboBox();
            this.txtNumeroIdentificacion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBuscar = new System.Windows.Forms.ToolStripButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvTelefonosPersonales = new System.Windows.Forms.DataGridView();
            this.imgEliminarTelefonoPersonal = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbTipoTelefono = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cmbOperadora = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.txtNumeroTelefonoPersonal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtExtensionTelefonoPersonal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbTelefonoPersonalPrincipal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnAgregarTelefonos = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrimerNombre = new System.Windows.Forms.TextBox();
            this.txtSegundoNombre = new System.Windows.Forms.TextBox();
            this.txtApellidoPaterno = new System.Windows.Forms.TextBox();
            this.txtApellidoMaterno = new System.Windows.Forms.TextBox();
            this.txtCorreoElectronico = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDireccionesTelefonos = new System.Windows.Forms.DataGridView();
            this.imgEliminarDireccionTelefono = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbDireccionPrincipal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnAgregarDireccionesTelefonos = new System.Windows.Forms.Button();
            this.cmbSexo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbEstadoCivil = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbTipoPersona = new System.Windows.Forms.ComboBox();
            this.toolStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelefonosPersonales)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDireccionesTelefonos)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbTipoIdentificacion
            // 
            this.cmbTipoIdentificacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoIdentificacion.FormattingEnabled = true;
            this.cmbTipoIdentificacion.Location = new System.Drawing.Point(388, 33);
            this.cmbTipoIdentificacion.Name = "cmbTipoIdentificacion";
            this.cmbTipoIdentificacion.Size = new System.Drawing.Size(156, 21);
            this.cmbTipoIdentificacion.TabIndex = 0;
            this.cmbTipoIdentificacion.SelectionChangeCommitted += new System.EventHandler(this.cmbTipoIdentificacion_SelectionChangeCommitted);
            // 
            // txtNumeroIdentificacion
            // 
            this.txtNumeroIdentificacion.Location = new System.Drawing.Point(147, 60);
            this.txtNumeroIdentificacion.MaxLength = 15;
            this.txtNumeroIdentificacion.Name = "txtNumeroIdentificacion";
            this.txtNumeroIdentificacion.Size = new System.Drawing.Size(156, 20);
            this.txtNumeroIdentificacion.TabIndex = 1;
            this.txtNumeroIdentificacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroIdentificacion_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(309, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Identificación:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Número de identificación:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.toolStripSeparator2,
            this.tsbGrabar,
            this.toolStripSeparator1,
            this.tsbEliminar,
            this.toolStripSeparator3,
            this.tsbBuscar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(564, 25);
            this.toolStrip1.TabIndex = 31;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNuevo
            // 
            this.tsbNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsbNuevo.Image")));
            this.tsbNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNuevo.Name = "tsbNuevo";
            this.tsbNuevo.Size = new System.Drawing.Size(62, 22);
            this.tsbNuevo.Text = "Nuevo";
            this.tsbNuevo.Click += new System.EventHandler(this.tsbNuevo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbGrabar
            // 
            this.tsbGrabar.Image = ((System.Drawing.Image)(resources.GetObject("tsbGrabar.Image")));
            this.tsbGrabar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGrabar.Name = "tsbGrabar";
            this.tsbGrabar.Size = new System.Drawing.Size(62, 22);
            this.tsbGrabar.Text = "Grabar";
            this.tsbGrabar.Click += new System.EventHandler(this.tsbGrabar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbEliminar
            // 
            this.tsbEliminar.Image = ((System.Drawing.Image)(resources.GetObject("tsbEliminar.Image")));
            this.tsbEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEliminar.Name = "tsbEliminar";
            this.tsbEliminar.Size = new System.Drawing.Size(70, 22);
            this.tsbEliminar.Text = "Eliminar";
            this.tsbEliminar.Click += new System.EventHandler(this.tsbEliminar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBuscar
            // 
            this.tsbBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsbBuscar.Image")));
            this.tsbBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuscar.Name = "tsbBuscar";
            this.tsbBuscar.Size = new System.Drawing.Size(62, 22);
            this.tsbBuscar.Text = "Buscar";
            this.tsbBuscar.Click += new System.EventHandler(this.tsbBuscar_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtRazonSocial);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtPrimerNombre);
            this.tabPage1.Controls.Add(this.txtSegundoNombre);
            this.tabPage1.Controls.Add(this.txtApellidoPaterno);
            this.tabPage1.Controls.Add(this.txtApellidoMaterno);
            this.tabPage1.Controls.Add(this.txtCorreoElectronico);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.cmbSexo);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.dtpFechaNacimiento);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.cmbEstadoCivil);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(546, 418);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Datos personales";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Location = new System.Drawing.Point(101, 62);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(433, 20);
            this.txtRazonSocial.TabIndex = 33;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "Razón social:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvTelefonosPersonales);
            this.groupBox5.Controls.Add(this.btnAgregarTelefonos);
            this.groupBox5.Location = new System.Drawing.Point(12, 282);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(522, 134);
            this.groupBox5.TabIndex = 32;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Teléfonos personales";
            // 
            // dgvTelefonosPersonales
            // 
            this.dgvTelefonosPersonales.AllowUserToAddRows = false;
            this.dgvTelefonosPersonales.AllowUserToDeleteRows = false;
            this.dgvTelefonosPersonales.AllowUserToResizeRows = false;
            this.dgvTelefonosPersonales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTelefonosPersonales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imgEliminarTelefonoPersonal,
            this.cmbTipoTelefono,
            this.cmbOperadora,
            this.txtNumeroTelefonoPersonal,
            this.txtExtensionTelefonoPersonal,
            this.chbTelefonoPersonalPrincipal});
            this.dgvTelefonosPersonales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTelefonosPersonales.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTelefonosPersonales.Location = new System.Drawing.Point(3, 39);
            this.dgvTelefonosPersonales.Name = "dgvTelefonosPersonales";
            this.dgvTelefonosPersonales.RowHeadersVisible = false;
            this.dgvTelefonosPersonales.Size = new System.Drawing.Size(516, 92);
            this.dgvTelefonosPersonales.TabIndex = 6;
            this.dgvTelefonosPersonales.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTelefonosPersonales_CellClick);
            this.dgvTelefonosPersonales.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTelefonosPersonales_CellEndEdit);
            this.dgvTelefonosPersonales.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvTelefonosPersonales_EditingControlShowing);
            // 
            // imgEliminarTelefonoPersonal
            // 
            this.imgEliminarTelefonoPersonal.HeaderText = "";
            this.imgEliminarTelefonoPersonal.Image = ((System.Drawing.Image)(resources.GetObject("imgEliminarTelefonoPersonal.Image")));
            this.imgEliminarTelefonoPersonal.Name = "imgEliminarTelefonoPersonal";
            this.imgEliminarTelefonoPersonal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.imgEliminarTelefonoPersonal.Width = 20;
            // 
            // cmbTipoTelefono
            // 
            this.cmbTipoTelefono.DataPropertyName = "idTipoTelefono";
            this.cmbTipoTelefono.HeaderText = "Tipo de teléfono";
            this.cmbTipoTelefono.Name = "cmbTipoTelefono";
            // 
            // cmbOperadora
            // 
            this.cmbOperadora.DataPropertyName = "idOperadora";
            this.cmbOperadora.HeaderText = "Operadora";
            this.cmbOperadora.Name = "cmbOperadora";
            // 
            // txtNumeroTelefonoPersonal
            // 
            this.txtNumeroTelefonoPersonal.DataPropertyName = "numero";
            this.txtNumeroTelefonoPersonal.HeaderText = "Número";
            this.txtNumeroTelefonoPersonal.Name = "txtNumeroTelefonoPersonal";
            // 
            // txtExtensionTelefonoPersonal
            // 
            this.txtExtensionTelefonoPersonal.DataPropertyName = "extension";
            this.txtExtensionTelefonoPersonal.HeaderText = "Extensión";
            this.txtExtensionTelefonoPersonal.Name = "txtExtensionTelefonoPersonal";
            // 
            // chbTelefonoPersonalPrincipal
            // 
            this.chbTelefonoPersonalPrincipal.DataPropertyName = "esPrincipal";
            this.chbTelefonoPersonalPrincipal.HeaderText = "";
            this.chbTelefonoPersonalPrincipal.Name = "chbTelefonoPersonalPrincipal";
            this.chbTelefonoPersonalPrincipal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chbTelefonoPersonalPrincipal.Width = 20;
            // 
            // btnAgregarTelefonos
            // 
            this.btnAgregarTelefonos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAgregarTelefonos.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarTelefonos.Image")));
            this.btnAgregarTelefonos.Location = new System.Drawing.Point(3, 16);
            this.btnAgregarTelefonos.Name = "btnAgregarTelefonos";
            this.btnAgregarTelefonos.Size = new System.Drawing.Size(516, 23);
            this.btnAgregarTelefonos.TabIndex = 7;
            this.btnAgregarTelefonos.Text = "Agregar teléfonos personales";
            this.btnAgregarTelefonos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarTelefonos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarTelefonos.UseVisualStyleBackColor = true;
            this.btnAgregarTelefonos.Click += new System.EventHandler(this.btnAgregarTelefonos_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Primer nombre:";
            // 
            // txtPrimerNombre
            // 
            this.txtPrimerNombre.Location = new System.Drawing.Point(101, 10);
            this.txtPrimerNombre.Name = "txtPrimerNombre";
            this.txtPrimerNombre.Size = new System.Drawing.Size(156, 20);
            this.txtPrimerNombre.TabIndex = 2;
            // 
            // txtSegundoNombre
            // 
            this.txtSegundoNombre.Location = new System.Drawing.Point(378, 10);
            this.txtSegundoNombre.Name = "txtSegundoNombre";
            this.txtSegundoNombre.Size = new System.Drawing.Size(156, 20);
            this.txtSegundoNombre.TabIndex = 3;
            // 
            // txtApellidoPaterno
            // 
            this.txtApellidoPaterno.Location = new System.Drawing.Point(101, 36);
            this.txtApellidoPaterno.Name = "txtApellidoPaterno";
            this.txtApellidoPaterno.Size = new System.Drawing.Size(156, 20);
            this.txtApellidoPaterno.TabIndex = 4;
            // 
            // txtApellidoMaterno
            // 
            this.txtApellidoMaterno.Location = new System.Drawing.Point(378, 36);
            this.txtApellidoMaterno.Name = "txtApellidoMaterno";
            this.txtApellidoMaterno.Size = new System.Drawing.Size(156, 20);
            this.txtApellidoMaterno.TabIndex = 5;
            // 
            // txtCorreoElectronico
            // 
            this.txtCorreoElectronico.Location = new System.Drawing.Point(378, 115);
            this.txtCorreoElectronico.Name = "txtCorreoElectronico";
            this.txtCorreoElectronico.Size = new System.Drawing.Size(156, 20);
            this.txtCorreoElectronico.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvDireccionesTelefonos);
            this.groupBox1.Controls.Add(this.btnAgregarDireccionesTelefonos);
            this.groupBox1.Location = new System.Drawing.Point(12, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 134);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direcciones y teléfonos";
            // 
            // dgvDireccionesTelefonos
            // 
            this.dgvDireccionesTelefonos.AllowUserToAddRows = false;
            this.dgvDireccionesTelefonos.AllowUserToDeleteRows = false;
            this.dgvDireccionesTelefonos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDireccionesTelefonos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imgEliminarDireccionTelefono,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column5,
            this.Column1,
            this.chbDireccionPrincipal});
            this.dgvDireccionesTelefonos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDireccionesTelefonos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDireccionesTelefonos.Location = new System.Drawing.Point(3, 39);
            this.dgvDireccionesTelefonos.Name = "dgvDireccionesTelefonos";
            this.dgvDireccionesTelefonos.RowHeadersVisible = false;
            this.dgvDireccionesTelefonos.Size = new System.Drawing.Size(516, 92);
            this.dgvDireccionesTelefonos.TabIndex = 28;
            this.dgvDireccionesTelefonos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDireccionesTelefonos_CellClick);
            this.dgvDireccionesTelefonos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDireccionesTelefonos_CellDoubleClick);
            this.dgvDireccionesTelefonos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDireccionesTelefonos_CellEndEdit);
            // 
            // imgEliminarDireccionTelefono
            // 
            this.imgEliminarDireccionTelefono.HeaderText = "";
            this.imgEliminarDireccionTelefono.Image = ((System.Drawing.Image)(resources.GetObject("imgEliminarDireccionTelefono.Image")));
            this.imgEliminarDireccionTelefono.Name = "imgEliminarDireccionTelefono";
            this.imgEliminarDireccionTelefono.ReadOnly = true;
            this.imgEliminarDireccionTelefono.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.imgEliminarDireccionTelefono.Width = 20;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "descripcionProvincia";
            this.Column3.HeaderText = "Provincia";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 70;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "descripcionCanton";
            this.Column4.HeaderText = "Cantón";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "descripcionTipoDireccion";
            this.Column6.HeaderText = "Tipo de dirección";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 115;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "direccionCompleta";
            this.Column5.HeaderText = "Dirección completa";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 200;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "referencia";
            this.Column1.HeaderText = "Referencia";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // chbDireccionPrincipal
            // 
            this.chbDireccionPrincipal.DataPropertyName = "esPrincipal";
            this.chbDireccionPrincipal.HeaderText = "";
            this.chbDireccionPrincipal.Name = "chbDireccionPrincipal";
            this.chbDireccionPrincipal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chbDireccionPrincipal.Width = 20;
            // 
            // btnAgregarDireccionesTelefonos
            // 
            this.btnAgregarDireccionesTelefonos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAgregarDireccionesTelefonos.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarDireccionesTelefonos.Image")));
            this.btnAgregarDireccionesTelefonos.Location = new System.Drawing.Point(3, 16);
            this.btnAgregarDireccionesTelefonos.Name = "btnAgregarDireccionesTelefonos";
            this.btnAgregarDireccionesTelefonos.Size = new System.Drawing.Size(516, 23);
            this.btnAgregarDireccionesTelefonos.TabIndex = 29;
            this.btnAgregarDireccionesTelefonos.Text = "Agregar direcciones y teléfonos";
            this.btnAgregarDireccionesTelefonos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarDireccionesTelefonos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarDireccionesTelefonos.UseVisualStyleBackColor = true;
            this.btnAgregarDireccionesTelefonos.Click += new System.EventHandler(this.btnAgregarDireccionesTelefonos_Click);
            // 
            // cmbSexo
            // 
            this.cmbSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSexo.FormattingEnabled = true;
            this.cmbSexo.Location = new System.Drawing.Point(101, 88);
            this.cmbSexo.Name = "cmbSexo";
            this.cmbSexo.Size = new System.Drawing.Size(156, 21);
            this.cmbSexo.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(263, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Correo electrónico:";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.CustomFormat = "dd/MMMM/yyyy";
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(378, 89);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.ShowCheckBox = true;
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(156, 20);
            this.dtpFechaNacimiento.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Estado civil:";
            // 
            // cmbEstadoCivil
            // 
            this.cmbEstadoCivil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoCivil.FormattingEnabled = true;
            this.cmbEstadoCivil.Location = new System.Drawing.Point(101, 115);
            this.cmbEstadoCivil.Name = "cmbEstadoCivil";
            this.cmbEstadoCivil.Size = new System.Drawing.Size(156, 21);
            this.cmbEstadoCivil.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(263, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Fecha de nacimiento:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Sexo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Segundo nombre:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(263, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Apellido materno:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Apellido paterno:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(6, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(554, 444);
            this.tabControl1.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Tipo de persona:";
            // 
            // cmbTipoPersona
            // 
            this.cmbTipoPersona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPersona.FormattingEnabled = true;
            this.cmbTipoPersona.Location = new System.Drawing.Point(147, 33);
            this.cmbTipoPersona.Name = "cmbTipoPersona";
            this.cmbTipoPersona.Size = new System.Drawing.Size(156, 21);
            this.cmbTipoPersona.TabIndex = 33;
            this.cmbTipoPersona.SelectionChangeCommitted += new System.EventHandler(this.cmbTipoPersona_SelectionChangeCommitted);
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 535);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbTipoPersona);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumeroIdentificacion);
            this.Controls.Add(this.cmbTipoIdentificacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmCliente_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelefonosPersonales)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDireccionesTelefonos)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTipoIdentificacion;
        private System.Windows.Forms.TextBox txtNumeroIdentificacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvTelefonosPersonales;
        private System.Windows.Forms.DataGridViewImageColumn imgEliminarTelefonoPersonal;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmbTipoTelefono;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmbOperadora;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtNumeroTelefonoPersonal;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtExtensionTelefonoPersonal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chbTelefonoPersonalPrincipal;
        private System.Windows.Forms.Button btnAgregarTelefonos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrimerNombre;
        private System.Windows.Forms.TextBox txtSegundoNombre;
        private System.Windows.Forms.TextBox txtApellidoPaterno;
        private System.Windows.Forms.TextBox txtApellidoMaterno;
        private System.Windows.Forms.TextBox txtCorreoElectronico;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvDireccionesTelefonos;
        private System.Windows.Forms.DataGridViewImageColumn imgEliminarDireccionTelefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chbDireccionPrincipal;
        private System.Windows.Forms.Button btnAgregarDireccionesTelefonos;
        private System.Windows.Forms.ComboBox cmbSexo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbEstadoCivil;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbTipoPersona;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
    }
}