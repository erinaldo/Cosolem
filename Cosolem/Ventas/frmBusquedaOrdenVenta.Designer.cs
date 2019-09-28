namespace Cosolem
{
    partial class frmBusquedaOrdenVenta
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusquedaOrdenVenta));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbUsuario = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNumeroIdentificacion = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grbDetalle = new System.Windows.Forms.GroupBox();
            this.dgvOrdenVentaDetalle = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtcPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtcTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbCliente = new System.Windows.Forms.GroupBox();
            this.dgvOrdenVentaCabecera = new System.Windows.Forms.DataGridView();
            this.dccSeleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dtcidOrdenVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtcSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtcDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtcSubtotalBruto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtcIVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtcTotalNeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dicComentarios = new System.Windows.Forms.DataGridViewImageColumn();
            this.dicHacerOrdenVenta = new System.Windows.Forms.DataGridViewImageColumn();
            this.dicFacturar = new System.Windows.Forms.DataGridViewImageColumn();
            this.dicImprimir = new System.Windows.Forms.DataGridViewImageColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grbDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenVentaDetalle)).BeginInit();
            this.grbCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenVentaCabecera)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbUsuario);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpFechaHasta);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaDesde);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtNumeroIdentificacion);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(993, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de búsqueda";
            // 
            // cmbUsuario
            // 
            this.cmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsuario.FormattingEnabled = true;
            this.cmbUsuario.Location = new System.Drawing.Point(382, 19);
            this.cmbUsuario.Name = "cmbUsuario";
            this.cmbUsuario.Size = new System.Drawing.Size(132, 21);
            this.cmbUsuario.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Usuario:";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.CustomFormat = "dd/MMMM/yyyy";
            this.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaHasta.Location = new System.Drawing.Point(828, 19);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(149, 20);
            this.dtpFechaHasta.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(753, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Fecha hasta:";
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Checked = false;
            this.dtpFechaDesde.CustomFormat = "dd/MMMM/yyyy";
            this.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDesde.Location = new System.Drawing.Point(598, 19);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(149, 20);
            this.dtpFechaDesde.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(520, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Fecha desde:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "Número de identificación:";
            // 
            // txtNumeroIdentificacion
            // 
            this.txtNumeroIdentificacion.Location = new System.Drawing.Point(145, 19);
            this.txtNumeroIdentificacion.MaxLength = 15;
            this.txtNumeroIdentificacion.Name = "txtNumeroIdentificacion";
            this.txtNumeroIdentificacion.Size = new System.Drawing.Size(179, 20);
            this.txtNumeroIdentificacion.TabIndex = 41;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grbDetalle);
            this.groupBox2.Controls.Add(this.grbCliente);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(993, 424);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultados";
            // 
            // grbDetalle
            // 
            this.grbDetalle.Controls.Add(this.dgvOrdenVentaDetalle);
            this.grbDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbDetalle.Location = new System.Drawing.Point(3, 184);
            this.grbDetalle.Name = "grbDetalle";
            this.grbDetalle.Size = new System.Drawing.Size(987, 237);
            this.grbDetalle.TabIndex = 1;
            this.grbDetalle.TabStop = false;
            this.grbDetalle.Text = "Detalle";
            // 
            // dgvOrdenVentaDetalle
            // 
            this.dgvOrdenVentaDetalle.AllowUserToAddRows = false;
            this.dgvOrdenVentaDetalle.AllowUserToDeleteRows = false;
            this.dgvOrdenVentaDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenVentaDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column14,
            this.dtcPrecio,
            this.Column8,
            this.dtcTotal});
            this.dgvOrdenVentaDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenVentaDetalle.Location = new System.Drawing.Point(3, 16);
            this.dgvOrdenVentaDetalle.Name = "dgvOrdenVentaDetalle";
            this.dgvOrdenVentaDetalle.ReadOnly = true;
            this.dgvOrdenVentaDetalle.RowHeadersVisible = false;
            this.dgvOrdenVentaDetalle.Size = new System.Drawing.Size(981, 218);
            this.dgvOrdenVentaDetalle.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "producto";
            this.Column1.HeaderText = "Producto";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 400;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "bodega";
            this.Column14.HeaderText = "Bodega";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 200;
            // 
            // dtcPrecio
            // 
            this.dtcPrecio.DataPropertyName = "precio";
            dataGridViewCellStyle1.Format = "c2";
            this.dtcPrecio.DefaultCellStyle = dataGridViewCellStyle1;
            this.dtcPrecio.HeaderText = "Precio";
            this.dtcPrecio.Name = "dtcPrecio";
            this.dtcPrecio.ReadOnly = true;
            this.dtcPrecio.Width = 80;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "cantidad";
            dataGridViewCellStyle2.Format = "n2";
            this.Column8.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column8.HeaderText = "Cantidad";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 60;
            // 
            // dtcTotal
            // 
            this.dtcTotal.DataPropertyName = "total";
            dataGridViewCellStyle3.Format = "c2";
            this.dtcTotal.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtcTotal.HeaderText = "Total";
            this.dtcTotal.Name = "dtcTotal";
            this.dtcTotal.ReadOnly = true;
            this.dtcTotal.Width = 70;
            // 
            // grbCliente
            // 
            this.grbCliente.Controls.Add(this.dgvOrdenVentaCabecera);
            this.grbCliente.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbCliente.Location = new System.Drawing.Point(3, 16);
            this.grbCliente.Name = "grbCliente";
            this.grbCliente.Size = new System.Drawing.Size(987, 168);
            this.grbCliente.TabIndex = 0;
            this.grbCliente.TabStop = false;
            this.grbCliente.Text = "Cliente";
            // 
            // dgvOrdenVentaCabecera
            // 
            this.dgvOrdenVentaCabecera.AllowUserToAddRows = false;
            this.dgvOrdenVentaCabecera.AllowUserToDeleteRows = false;
            this.dgvOrdenVentaCabecera.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenVentaCabecera.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dccSeleccionado,
            this.dtcidOrdenVenta,
            this.Column15,
            this.Column3,
            this.Column12,
            this.Column2,
            this.dtcSubtotal,
            this.dtcDescuento,
            this.dtcSubtotalBruto,
            this.dtcIVA,
            this.dtcTotalNeto,
            this.Column13,
            this.dicComentarios,
            this.dicHacerOrdenVenta,
            this.dicFacturar,
            this.dicImprimir});
            this.dgvOrdenVentaCabecera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenVentaCabecera.Location = new System.Drawing.Point(3, 16);
            this.dgvOrdenVentaCabecera.Name = "dgvOrdenVentaCabecera";
            this.dgvOrdenVentaCabecera.RowHeadersVisible = false;
            this.dgvOrdenVentaCabecera.Size = new System.Drawing.Size(981, 149);
            this.dgvOrdenVentaCabecera.TabIndex = 0;
            this.dgvOrdenVentaCabecera.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenVentaCabecera_CellClick);
            this.dgvOrdenVentaCabecera.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenVentaCabecera_CellDoubleClick);
            this.dgvOrdenVentaCabecera.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenVentaCabecera_CellEndEdit);
            // 
            // dccSeleccionado
            // 
            this.dccSeleccionado.DataPropertyName = "seleccionado";
            this.dccSeleccionado.HeaderText = "";
            this.dccSeleccionado.Name = "dccSeleccionado";
            this.dccSeleccionado.Width = 20;
            // 
            // dtcidOrdenVenta
            // 
            this.dtcidOrdenVenta.DataPropertyName = "idOrdenVentaCabecera";
            this.dtcidOrdenVenta.HeaderText = "";
            this.dtcidOrdenVenta.Name = "dtcidOrdenVenta";
            this.dtcidOrdenVenta.ReadOnly = true;
            this.dtcidOrdenVenta.Width = 60;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "usuarioIngreso";
            this.Column15.HeaderText = "Usuario ingreso";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 60;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "fechaHoraOrdenVenta";
            this.Column3.HeaderText = "Fecha";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "descripcionFormaPago";
            this.Column12.HeaderText = "Forma de pago";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 50;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "descripcionCliente";
            this.Column2.HeaderText = "Cliente";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // dtcSubtotal
            // 
            this.dtcSubtotal.DataPropertyName = "subTotal";
            dataGridViewCellStyle4.Format = "c2";
            this.dtcSubtotal.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtcSubtotal.HeaderText = "Subtotal";
            this.dtcSubtotal.Name = "dtcSubtotal";
            this.dtcSubtotal.ReadOnly = true;
            this.dtcSubtotal.Width = 60;
            // 
            // dtcDescuento
            // 
            this.dtcDescuento.DataPropertyName = "descuento";
            dataGridViewCellStyle5.Format = "c2";
            this.dtcDescuento.DefaultCellStyle = dataGridViewCellStyle5;
            this.dtcDescuento.HeaderText = "Descuento";
            this.dtcDescuento.Name = "dtcDescuento";
            this.dtcDescuento.ReadOnly = true;
            this.dtcDescuento.Width = 60;
            // 
            // dtcSubtotalBruto
            // 
            this.dtcSubtotalBruto.DataPropertyName = "subTotalBruto";
            dataGridViewCellStyle6.Format = "c2";
            this.dtcSubtotalBruto.DefaultCellStyle = dataGridViewCellStyle6;
            this.dtcSubtotalBruto.HeaderText = "Subtotal bruto";
            this.dtcSubtotalBruto.Name = "dtcSubtotalBruto";
            this.dtcSubtotalBruto.ReadOnly = true;
            this.dtcSubtotalBruto.Width = 60;
            // 
            // dtcIVA
            // 
            this.dtcIVA.DataPropertyName = "IVA";
            dataGridViewCellStyle7.Format = "c2";
            this.dtcIVA.DefaultCellStyle = dataGridViewCellStyle7;
            this.dtcIVA.HeaderText = "IVA";
            this.dtcIVA.Name = "dtcIVA";
            this.dtcIVA.ReadOnly = true;
            this.dtcIVA.Width = 50;
            // 
            // dtcTotalNeto
            // 
            this.dtcTotalNeto.DataPropertyName = "totalNeto";
            dataGridViewCellStyle8.Format = "c2";
            this.dtcTotalNeto.DefaultCellStyle = dataGridViewCellStyle8;
            this.dtcTotalNeto.HeaderText = "Total neto";
            this.dtcTotalNeto.Name = "dtcTotalNeto";
            this.dtcTotalNeto.ReadOnly = true;
            this.dtcTotalNeto.Width = 60;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "descripcionEstadoOrdenVenta";
            this.Column13.HeaderText = "Estado";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // dicComentarios
            // 
            this.dicComentarios.HeaderText = "";
            this.dicComentarios.Image = ((System.Drawing.Image)(resources.GetObject("dicComentarios.Image")));
            this.dicComentarios.Name = "dicComentarios";
            this.dicComentarios.Width = 20;
            // 
            // dicHacerOrdenVenta
            // 
            this.dicHacerOrdenVenta.HeaderText = "";
            this.dicHacerOrdenVenta.Image = ((System.Drawing.Image)(resources.GetObject("dicHacerOrdenVenta.Image")));
            this.dicHacerOrdenVenta.Name = "dicHacerOrdenVenta";
            this.dicHacerOrdenVenta.Width = 20;
            // 
            // dicFacturar
            // 
            this.dicFacturar.HeaderText = "";
            this.dicFacturar.Image = ((System.Drawing.Image)(resources.GetObject("dicFacturar.Image")));
            this.dicFacturar.Name = "dicFacturar";
            this.dicFacturar.Width = 20;
            // 
            // dicImprimir
            // 
            this.dicImprimir.HeaderText = "";
            this.dicImprimir.Image = ((System.Drawing.Image)(resources.GetObject("dicImprimir.Image")));
            this.dicImprimir.Name = "dicImprimir";
            this.dicImprimir.Width = 20;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.toolStripSeparator2,
            this.tsbBuscar,
            this.toolStripSeparator1,
            this.tsbEliminar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(993, 25);
            this.toolStrip1.TabIndex = 33;
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
            // tsbBuscar
            // 
            this.tsbBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsbBuscar.Image")));
            this.tsbBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuscar.Name = "tsbBuscar";
            this.tsbBuscar.Size = new System.Drawing.Size(62, 22);
            this.tsbBuscar.Text = "Buscar";
            this.tsbBuscar.Click += new System.EventHandler(this.tsbBuscar_Click);
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
            // frmBusquedaOrdenVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 500);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmBusquedaOrdenVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmBusquedaOrdenVenta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.grbDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenVentaDetalle)).EndInit();
            this.grbCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenVentaCabecera)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNumeroIdentificacion;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
        private System.Windows.Forms.GroupBox grbCliente;
        private System.Windows.Forms.DataGridView dgvOrdenVentaCabecera;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.GroupBox grbDetalle;
        private System.Windows.Forms.DataGridView dgvOrdenVentaDetalle;
        private System.Windows.Forms.ComboBox cmbUsuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcTotal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dccSeleccionado;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcidOrdenVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcSubtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcSubtotalBruto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcIVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcTotalNeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewImageColumn dicComentarios;
        private System.Windows.Forms.DataGridViewImageColumn dicHacerOrdenVenta;
        private System.Windows.Forms.DataGridViewImageColumn dicFacturar;
        private System.Windows.Forms.DataGridViewImageColumn dicImprimir;
    }
}