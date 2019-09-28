namespace Cosolem
{
    partial class frmDevolucion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDevolucion));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBuscar = new System.Windows.Forms.ToolStripButton();
            this.grbCliente = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFechaFactura = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroFactura = new System.Windows.Forms.TextBox();
            this.txtConvencional = new System.Windows.Forms.TextBox();
            this.txtCelular = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNumeroIdentificacion = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvVenta = new System.Windows.Forms.DataGridView();
            this.pnlTotales = new System.Windows.Forms.Panel();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalNeto = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtIVA = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtcBodega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtcBodegaEntrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.grbCliente.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).BeginInit();
            this.pnlTotales.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.toolStripSeparator2,
            this.tsbGrabar,
            this.toolStripButton1,
            this.tsbBuscar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(863, 25);
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
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBuscar
            // 
            this.tsbBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsbBuscar.Image")));
            this.tsbBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuscar.Name = "tsbBuscar";
            this.tsbBuscar.Size = new System.Drawing.Size(78, 22);
            this.tsbBuscar.Text = "Consultar";
            this.tsbBuscar.Click += new System.EventHandler(this.tsbBuscar_Click);
            // 
            // grbCliente
            // 
            this.grbCliente.Controls.Add(this.label3);
            this.grbCliente.Controls.Add(this.txtFechaFactura);
            this.grbCliente.Controls.Add(this.label1);
            this.grbCliente.Controls.Add(this.txtNumeroFactura);
            this.grbCliente.Controls.Add(this.txtConvencional);
            this.grbCliente.Controls.Add(this.txtCelular);
            this.grbCliente.Controls.Add(this.label8);
            this.grbCliente.Controls.Add(this.txtDireccion);
            this.grbCliente.Controls.Add(this.label13);
            this.grbCliente.Controls.Add(this.label2);
            this.grbCliente.Controls.Add(this.txtCliente);
            this.grbCliente.Controls.Add(this.label14);
            this.grbCliente.Controls.Add(this.label12);
            this.grbCliente.Controls.Add(this.txtNumeroIdentificacion);
            this.grbCliente.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbCliente.Location = new System.Drawing.Point(0, 25);
            this.grbCliente.Name = "grbCliente";
            this.grbCliente.Size = new System.Drawing.Size(863, 136);
            this.grbCliente.TabIndex = 38;
            this.grbCliente.TabStop = false;
            this.grbCliente.Text = "Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(625, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "Fecha de factura:";
            // 
            // txtFechaFactura
            // 
            this.txtFechaFactura.Location = new System.Drawing.Point(722, 25);
            this.txtFechaFactura.MaxLength = 15;
            this.txtFechaFactura.Name = "txtFechaFactura";
            this.txtFechaFactura.Size = new System.Drawing.Size(126, 20);
            this.txtFechaFactura.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Número de factura:";
            // 
            // txtNumeroFactura
            // 
            this.txtNumeroFactura.Location = new System.Drawing.Point(473, 25);
            this.txtNumeroFactura.MaxLength = 15;
            this.txtNumeroFactura.Name = "txtNumeroFactura";
            this.txtNumeroFactura.Size = new System.Drawing.Size(146, 20);
            this.txtNumeroFactura.TabIndex = 63;
            // 
            // txtConvencional
            // 
            this.txtConvencional.Location = new System.Drawing.Point(93, 104);
            this.txtConvencional.Name = "txtConvencional";
            this.txtConvencional.Size = new System.Drawing.Size(146, 20);
            this.txtConvencional.TabIndex = 59;
            // 
            // txtCelular
            // 
            this.txtCelular.Location = new System.Drawing.Point(293, 104);
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.Size = new System.Drawing.Size(146, 20);
            this.txtCelular.TabIndex = 60;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(93, 78);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(755, 20);
            this.txtDireccion.TabIndex = 57;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(245, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 62;
            this.label13.Text = "Celular:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Cliente:";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(93, 52);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(755, 20);
            this.txtCliente.TabIndex = 41;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 107);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 13);
            this.label14.TabIndex = 61;
            this.label14.Text = "Convencional:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 13);
            this.label12.TabIndex = 40;
            this.label12.Text = "Número de identificación:";
            // 
            // txtNumeroIdentificacion
            // 
            this.txtNumeroIdentificacion.Location = new System.Drawing.Point(145, 25);
            this.txtNumeroIdentificacion.MaxLength = 15;
            this.txtNumeroIdentificacion.Name = "txtNumeroIdentificacion";
            this.txtNumeroIdentificacion.Size = new System.Drawing.Size(218, 20);
            this.txtNumeroIdentificacion.TabIndex = 39;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.pnlTotales);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(863, 322);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Venta";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvVenta);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(857, 221);
            this.panel2.TabIndex = 1;
            // 
            // dgvVenta
            // 
            this.dgvVenta.AllowUserToAddRows = false;
            this.dgvVenta.AllowUserToDeleteRows = false;
            this.dgvVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVenta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.dtcBodega,
            this.dtcBodegaEntrada,
            this.Column3,
            this.Column2,
            this.Column4});
            this.dgvVenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVenta.Location = new System.Drawing.Point(0, 0);
            this.dgvVenta.Name = "dgvVenta";
            this.dgvVenta.ReadOnly = true;
            this.dgvVenta.RowHeadersVisible = false;
            this.dgvVenta.Size = new System.Drawing.Size(857, 221);
            this.dgvVenta.TabIndex = 1;
            this.dgvVenta.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVenta_CellDoubleClick);
            // 
            // pnlTotales
            // 
            this.pnlTotales.Controls.Add(this.txtComentarios);
            this.pnlTotales.Controls.Add(this.label4);
            this.pnlTotales.Controls.Add(this.txtTotalNeto);
            this.pnlTotales.Controls.Add(this.label32);
            this.pnlTotales.Controls.Add(this.txtIVA);
            this.pnlTotales.Controls.Add(this.label31);
            this.pnlTotales.Controls.Add(this.txtSubtotal);
            this.pnlTotales.Controls.Add(this.label26);
            this.pnlTotales.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotales.Location = new System.Drawing.Point(3, 237);
            this.pnlTotales.Name = "pnlTotales";
            this.pnlTotales.Size = new System.Drawing.Size(857, 82);
            this.pnlTotales.TabIndex = 0;
            // 
            // txtComentarios
            // 
            this.txtComentarios.Location = new System.Drawing.Point(80, 5);
            this.txtComentarios.Multiline = true;
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(356, 67);
            this.txtComentarios.TabIndex = 98;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 97;
            this.label4.Text = "Comentarios";
            // 
            // txtTotalNeto
            // 
            this.txtTotalNeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalNeto.Location = new System.Drawing.Point(705, 54);
            this.txtTotalNeto.Name = "txtTotalNeto";
            this.txtTotalNeto.ReadOnly = true;
            this.txtTotalNeto.Size = new System.Drawing.Size(140, 18);
            this.txtTotalNeto.TabIndex = 87;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(608, 57);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(63, 13);
            this.label32.TabIndex = 88;
            this.label32.Text = "Total neto";
            this.label32.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtIVA
            // 
            this.txtIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIVA.Location = new System.Drawing.Point(705, 30);
            this.txtIVA.Name = "txtIVA";
            this.txtIVA.ReadOnly = true;
            this.txtIVA.Size = new System.Drawing.Size(140, 18);
            this.txtIVA.TabIndex = 85;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(608, 33);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(31, 13);
            this.label31.TabIndex = 86;
            this.label31.Text = "IVA:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubtotal.Location = new System.Drawing.Point(705, 6);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(140, 18);
            this.txtSubtotal.TabIndex = 83;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(608, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(58, 13);
            this.label26.TabIndex = 84;
            this.label26.Text = "Subtotal:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "descripcionProducto";
            this.Column1.HeaderText = "Producto";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 380;
            // 
            // dtcBodega
            // 
            this.dtcBodega.DataPropertyName = "descripcionBodegaSalida";
            this.dtcBodega.HeaderText = "Bodega salida";
            this.dtcBodega.Name = "dtcBodega";
            this.dtcBodega.ReadOnly = true;
            this.dtcBodega.Width = 110;
            // 
            // dtcBodegaEntrada
            // 
            this.dtcBodegaEntrada.DataPropertyName = "descripcionBodegaEntrada";
            this.dtcBodegaEntrada.HeaderText = "Bodega entrada";
            this.dtcBodegaEntrada.Name = "dtcBodegaEntrada";
            this.dtcBodegaEntrada.ReadOnly = true;
            this.dtcBodegaEntrada.Width = 110;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "precio";
            dataGridViewCellStyle4.Format = "c2";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "Precio";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 70;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "cantidad";
            dataGridViewCellStyle5.Format = "n2";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column2.HeaderText = "Cantidad";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 70;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "subTotalBruto";
            dataGridViewCellStyle6.Format = "c2";
            this.Column4.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column4.HeaderText = "Total";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 80;
            // 
            // frmDevolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 483);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grbCliente);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmDevolucion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDevolucion_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbCliente.ResumeLayout(false);
            this.grbCliente.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).EndInit();
            this.pnlTotales.ResumeLayout(false);
            this.pnlTotales.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.GroupBox grbCliente;
        private System.Windows.Forms.TextBox txtConvencional;
        private System.Windows.Forms.TextBox txtCelular;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNumeroIdentificacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFechaFactura;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumeroFactura;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvVenta;
        private System.Windows.Forms.Panel pnlTotales;
        private System.Windows.Forms.TextBox txtTotalNeto;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtIVA;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcBodega;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtcBodegaEntrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}