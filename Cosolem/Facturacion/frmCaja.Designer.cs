namespace Cosolem
{
    partial class frmCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCaja));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtNumeroIdentificacion = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.dtpFechaFactura = new System.Windows.Forms.DateTimePicker();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtNumeroFactura = new System.Windows.Forms.TextBox();
            this.dgvOrdenVentaDetalle = new System.Windows.Forms.DataGridView();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtIVA = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.dgvOrdenVentaFormaPago = new System.Windows.Forms.DataGridView();
            this.dicEliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbFormaPago = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.txtValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCancelado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAgregarFormaPago = new System.Windows.Forms.Button();
            this.txtCambio = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenVentaDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenVentaFormaPago)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbGrabar,
            this.toolStripButton1,
            this.tsbEliminar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(658, 25);
            this.toolStrip1.TabIndex = 38;
            this.toolStrip1.Text = "toolStrip1";
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
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 25);
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
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(75, 54);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(301, 20);
            this.txtCliente.TabIndex = 39;
            // 
            // txtNumeroIdentificacion
            // 
            this.txtNumeroIdentificacion.Location = new System.Drawing.Point(75, 80);
            this.txtNumeroIdentificacion.Name = "txtNumeroIdentificacion";
            this.txtNumeroIdentificacion.Size = new System.Drawing.Size(301, 20);
            this.txtNumeroIdentificacion.TabIndex = 40;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(75, 106);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(565, 20);
            this.txtDireccion.TabIndex = 41;
            // 
            // dtpFechaFactura
            // 
            this.dtpFechaFactura.Location = new System.Drawing.Point(440, 54);
            this.dtpFechaFactura.Name = "dtpFechaFactura";
            this.dtpFechaFactura.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFactura.TabIndex = 42;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(440, 80);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(200, 20);
            this.txtTelefono.TabIndex = 43;
            // 
            // txtNumeroFactura
            // 
            this.txtNumeroFactura.Location = new System.Drawing.Point(547, 28);
            this.txtNumeroFactura.Name = "txtNumeroFactura";
            this.txtNumeroFactura.Size = new System.Drawing.Size(93, 20);
            this.txtNumeroFactura.TabIndex = 44;
            this.txtNumeroFactura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroFactura_KeyPress);
            // 
            // dgvOrdenVentaDetalle
            // 
            this.dgvOrdenVentaDetalle.AllowUserToAddRows = false;
            this.dgvOrdenVentaDetalle.AllowUserToDeleteRows = false;
            this.dgvOrdenVentaDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenVentaDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column1,
            this.Column7,
            this.Column11});
            this.dgvOrdenVentaDetalle.Location = new System.Drawing.Point(17, 132);
            this.dgvOrdenVentaDetalle.Name = "dgvOrdenVentaDetalle";
            this.dgvOrdenVentaDetalle.ReadOnly = true;
            this.dgvOrdenVentaDetalle.RowHeadersVisible = false;
            this.dgvOrdenVentaDetalle.Size = new System.Drawing.Size(623, 112);
            this.dgvOrdenVentaDetalle.TabIndex = 45;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(500, 302);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(140, 20);
            this.txtTotal.TabIndex = 93;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(460, 305);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(34, 13);
            this.label28.TabIndex = 94;
            this.label28.Text = "Total:";
            // 
            // txtIVA
            // 
            this.txtIVA.Location = new System.Drawing.Point(500, 276);
            this.txtIVA.Name = "txtIVA";
            this.txtIVA.ReadOnly = true;
            this.txtIVA.Size = new System.Drawing.Size(140, 20);
            this.txtIVA.TabIndex = 91;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(467, 279);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(27, 13);
            this.label27.TabIndex = 92;
            this.label27.Text = "IVA:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Location = new System.Drawing.Point(500, 250);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(140, 20);
            this.txtSubtotal.TabIndex = 89;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(445, 253);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(49, 13);
            this.label26.TabIndex = 90;
            this.label26.Text = "Subtotal:";
            // 
            // dgvOrdenVentaFormaPago
            // 
            this.dgvOrdenVentaFormaPago.AllowUserToAddRows = false;
            this.dgvOrdenVentaFormaPago.AllowUserToDeleteRows = false;
            this.dgvOrdenVentaFormaPago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenVentaFormaPago.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dicEliminar,
            this.cmbFormaPago,
            this.txtValor});
            this.dgvOrdenVentaFormaPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenVentaFormaPago.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvOrdenVentaFormaPago.Location = new System.Drawing.Point(3, 44);
            this.dgvOrdenVentaFormaPago.Name = "dgvOrdenVentaFormaPago";
            this.dgvOrdenVentaFormaPago.RowHeadersVisible = false;
            this.dgvOrdenVentaFormaPago.Size = new System.Drawing.Size(353, 98);
            this.dgvOrdenVentaFormaPago.TabIndex = 95;
            this.dgvOrdenVentaFormaPago.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenVentaFormaPago_CellClick);
            this.dgvOrdenVentaFormaPago.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenVentaFormaPago_CellEndEdit);
            this.dgvOrdenVentaFormaPago.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvOrdenVentaFormaPago_DataError);
            this.dgvOrdenVentaFormaPago.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvOrdenVentaFormaPago_EditingControlShowing);
            // 
            // dicEliminar
            // 
            this.dicEliminar.HeaderText = "";
            this.dicEliminar.Image = ((System.Drawing.Image)(resources.GetObject("dicEliminar.Image")));
            this.dicEliminar.Name = "dicEliminar";
            this.dicEliminar.Width = 20;
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.DataPropertyName = "idFormaPago";
            dataGridViewCellStyle4.Format = "n2";
            this.cmbFormaPago.DefaultCellStyle = dataGridViewCellStyle4;
            this.cmbFormaPago.HeaderText = "Forma de pago";
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cmbFormaPago.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cmbFormaPago.Width = 200;
            // 
            // txtValor
            // 
            this.txtValor.DataPropertyName = "valor";
            this.txtValor.HeaderText = "Valor";
            this.txtValor.Name = "txtValor";
            // 
            // txtCancelado
            // 
            this.txtCancelado.Location = new System.Drawing.Point(500, 346);
            this.txtCancelado.Name = "txtCancelado";
            this.txtCancelado.ReadOnly = true;
            this.txtCancelado.Size = new System.Drawing.Size(140, 20);
            this.txtCancelado.TabIndex = 96;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(433, 349);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 97;
            this.label1.Text = "Cancelado:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(495, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 98;
            this.label2.Text = "Factura:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 99;
            this.label3.Text = "Cliente:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 100;
            this.label4.Text = "RUC/CI:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 101;
            this.label5.Text = "Dirección:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(382, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 102;
            this.label6.Text = "Fecha:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(382, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 103;
            this.label7.Text = "Teléfono:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvOrdenVentaFormaPago);
            this.groupBox1.Controls.Add(this.btnAgregarFormaPago);
            this.groupBox1.Location = new System.Drawing.Point(17, 250);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 145);
            this.groupBox1.TabIndex = 104;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formas de pago";
            // 
            // btnAgregarFormaPago
            // 
            this.btnAgregarFormaPago.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAgregarFormaPago.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarFormaPago.Image")));
            this.btnAgregarFormaPago.Location = new System.Drawing.Point(3, 16);
            this.btnAgregarFormaPago.Name = "btnAgregarFormaPago";
            this.btnAgregarFormaPago.Size = new System.Drawing.Size(353, 28);
            this.btnAgregarFormaPago.TabIndex = 5;
            this.btnAgregarFormaPago.Text = "Agregar forma de pago";
            this.btnAgregarFormaPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarFormaPago.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarFormaPago.UseVisualStyleBackColor = true;
            this.btnAgregarFormaPago.Click += new System.EventHandler(this.btnAgregarFormaPago_Click);
            // 
            // txtCambio
            // 
            this.txtCambio.Location = new System.Drawing.Point(500, 372);
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.ReadOnly = true;
            this.txtCambio.Size = new System.Drawing.Size(140, 20);
            this.txtCambio.TabIndex = 105;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(433, 375);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 106;
            this.label8.Text = "Cambio:";
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "cantidad";
            dataGridViewCellStyle1.Format = "n2";
            this.Column8.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column8.HeaderText = "Cantidad";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 60;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "descripcion";
            this.Column1.HeaderText = "Descripción";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 335;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "precio";
            dataGridViewCellStyle2.Format = "c2";
            this.Column7.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column7.HeaderText = "Precio";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "subTotalBruto";
            dataGridViewCellStyle3.Format = "c2";
            this.Column11.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column11.HeaderText = "Total";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // frmCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 406);
            this.Controls.Add(this.txtCambio);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCancelado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.txtIVA);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.dgvOrdenVentaDetalle);
            this.Controls.Add(this.txtNumeroFactura);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.dtpFechaFactura);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.txtNumeroIdentificacion);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmCaja_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenVentaDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenVentaFormaPago)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.TextBox txtNumeroIdentificacion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.DateTimePicker dtpFechaFactura;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtNumeroFactura;
        private System.Windows.Forms.DataGridView dgvOrdenVentaDetalle;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtIVA;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DataGridView dgvOrdenVentaFormaPago;
        private System.Windows.Forms.TextBox txtCancelado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAgregarFormaPago;
        private System.Windows.Forms.TextBox txtCambio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewImageColumn dicEliminar;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmbFormaPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtValor;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}