namespace Cosolem
{
    partial class frmProductoFinanciero
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductoFinanciero));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBuscar = new System.Windows.Forms.ToolStripButton();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.cmbFrecuenciaPago = new System.Windows.Forms.ComboBox();
            this.txtPlazo = new System.Windows.Forms.TextBox();
            this.txtMesesGracia = new System.Windows.Forms.TextBox();
            this.txtCuotasGratis = new System.Windows.Forms.TextBox();
            this.txtPorcentajeCuotaInicialExigible = new System.Windows.Forms.TextBox();
            this.dgvProductoFinancieroDetalle = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGenerarDetalleProductoFinanciero = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductoFinancieroDetalle)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(368, 25);
            this.toolStrip1.TabIndex = 37;
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
            // cmbEmpresa
            // 
            this.cmbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(86, 30);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(264, 21);
            this.cmbEmpresa.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Empresa:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(86, 57);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(264, 20);
            this.txtDescripcion.TabIndex = 42;
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDesde.Location = new System.Drawing.Point(86, 83);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(102, 20);
            this.dtpFechaDesde.TabIndex = 43;
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaHasta.Location = new System.Drawing.Point(248, 83);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(102, 20);
            this.dtpFechaHasta.TabIndex = 44;
            // 
            // cmbFrecuenciaPago
            // 
            this.cmbFrecuenciaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrecuenciaPago.FormattingEnabled = true;
            this.cmbFrecuenciaPago.Location = new System.Drawing.Point(86, 109);
            this.cmbFrecuenciaPago.Name = "cmbFrecuenciaPago";
            this.cmbFrecuenciaPago.Size = new System.Drawing.Size(114, 21);
            this.cmbFrecuenciaPago.TabIndex = 45;
            // 
            // txtPlazo
            // 
            this.txtPlazo.Location = new System.Drawing.Point(248, 109);
            this.txtPlazo.Name = "txtPlazo";
            this.txtPlazo.Size = new System.Drawing.Size(102, 20);
            this.txtPlazo.TabIndex = 46;
            this.txtPlazo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlazo_KeyPress);
            // 
            // txtMesesGracia
            // 
            this.txtMesesGracia.Location = new System.Drawing.Point(108, 136);
            this.txtMesesGracia.Name = "txtMesesGracia";
            this.txtMesesGracia.Size = new System.Drawing.Size(80, 20);
            this.txtMesesGracia.TabIndex = 47;
            this.txtMesesGracia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesesGracia_KeyPress);
            // 
            // txtCuotasGratis
            // 
            this.txtCuotasGratis.Location = new System.Drawing.Point(269, 136);
            this.txtCuotasGratis.Name = "txtCuotasGratis";
            this.txtCuotasGratis.Size = new System.Drawing.Size(81, 20);
            this.txtCuotasGratis.TabIndex = 48;
            this.txtCuotasGratis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCuotasGratis_KeyPress);
            // 
            // txtPorcentajeCuotaInicialExigible
            // 
            this.txtPorcentajeCuotaInicialExigible.Location = new System.Drawing.Point(193, 162);
            this.txtPorcentajeCuotaInicialExigible.Name = "txtPorcentajeCuotaInicialExigible";
            this.txtPorcentajeCuotaInicialExigible.Size = new System.Drawing.Size(72, 20);
            this.txtPorcentajeCuotaInicialExigible.TabIndex = 50;
            this.txtPorcentajeCuotaInicialExigible.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcentajeCuotaInicialExigible_KeyPress);
            // 
            // dgvProductoFinancieroDetalle
            // 
            this.dgvProductoFinancieroDetalle.AllowUserToAddRows = false;
            this.dgvProductoFinancieroDetalle.AllowUserToDeleteRows = false;
            this.dgvProductoFinancieroDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductoFinancieroDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvProductoFinancieroDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductoFinancieroDetalle.Location = new System.Drawing.Point(3, 44);
            this.dgvProductoFinancieroDetalle.Name = "dgvProductoFinancieroDetalle";
            this.dgvProductoFinancieroDetalle.ReadOnly = true;
            this.dgvProductoFinancieroDetalle.RowHeadersVisible = false;
            this.dgvProductoFinancieroDetalle.Size = new System.Drawing.Size(327, 335);
            this.dgvProductoFinancieroDetalle.TabIndex = 51;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "plazo";
            this.Column1.HeaderText = "Plazo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "aplicaMesGracia";
            this.Column2.HeaderText = "Mes de gracia";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "aplicaCuotaGratis";
            this.Column3.HeaderText = "Cuota gratis";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Descripción:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Desde:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Hasta:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "Frecuencia:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "Plazo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 57;
            this.label7.Text = "Meses de gracia:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(194, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "Cuotas gratis:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvProductoFinancieroDetalle);
            this.groupBox1.Controls.Add(this.btnGenerarDetalleProductoFinanciero);
            this.groupBox1.Location = new System.Drawing.Point(17, 188);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 382);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle producto financiero";
            // 
            // btnGenerarDetalleProductoFinanciero
            // 
            this.btnGenerarDetalleProductoFinanciero.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGenerarDetalleProductoFinanciero.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarDetalleProductoFinanciero.Image")));
            this.btnGenerarDetalleProductoFinanciero.Location = new System.Drawing.Point(3, 16);
            this.btnGenerarDetalleProductoFinanciero.Name = "btnGenerarDetalleProductoFinanciero";
            this.btnGenerarDetalleProductoFinanciero.Size = new System.Drawing.Size(327, 28);
            this.btnGenerarDetalleProductoFinanciero.TabIndex = 52;
            this.btnGenerarDetalleProductoFinanciero.Text = "Generar detalle producto financiero";
            this.btnGenerarDetalleProductoFinanciero.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarDetalleProductoFinanciero.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerarDetalleProductoFinanciero.UseVisualStyleBackColor = true;
            this.btnGenerarDetalleProductoFinanciero.Click += new System.EventHandler(this.btnGenerarDetalleProductoFinanciero_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 165);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Porcentaje de cuota inicial exigible:";
            // 
            // frmProductoFinanciero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 582);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPorcentajeCuotaInicialExigible);
            this.Controls.Add(this.txtCuotasGratis);
            this.Controls.Add(this.txtMesesGracia);
            this.Controls.Add(this.txtPlazo);
            this.Controls.Add(this.cmbFrecuenciaPago);
            this.Controls.Add(this.dtpFechaHasta);
            this.Controls.Add(this.dtpFechaDesde);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmProductoFinanciero";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmProductoFinanciero_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductoFinancieroDetalle)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
        private System.Windows.Forms.ComboBox cmbEmpresa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.ComboBox cmbFrecuenciaPago;
        private System.Windows.Forms.TextBox txtPlazo;
        private System.Windows.Forms.TextBox txtMesesGracia;
        private System.Windows.Forms.TextBox txtCuotasGratis;
        private System.Windows.Forms.TextBox txtPorcentajeCuotaInicialExigible;
        private System.Windows.Forms.DataGridView dgvProductoFinancieroDetalle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGenerarDetalleProductoFinanciero;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
    }
}