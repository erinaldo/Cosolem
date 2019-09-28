namespace Cosolem
{
    partial class frmOrdenPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrdenPedido));
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDescripcionProducto = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.dgvOrdenPedidoDetalle = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dicEliminarProducto = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGenerar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBuscar = new System.Windows.Forms.ToolStripButton();
            this.txtSolicita = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFechaHoraIngreso = new System.Windows.Forms.TextBox();
            this.txtFechaHoraUltimaModificacion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.txtUsuarioUltimaModificacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenPedidoDetalle)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(622, 115);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(52, 20);
            this.txtCantidad.TabIndex = 73;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(564, 118);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(52, 13);
            this.label18.TabIndex = 74;
            this.label18.Text = "Cantidad:";
            // 
            // txtDescripcionProducto
            // 
            this.txtDescripcionProducto.Location = new System.Drawing.Point(238, 115);
            this.txtDescripcionProducto.MaxLength = 15;
            this.txtDescripcionProducto.Name = "txtDescripcionProducto";
            this.txtDescripcionProducto.ReadOnly = true;
            this.txtDescripcionProducto.Size = new System.Drawing.Size(320, 20);
            this.txtDescripcionProducto.TabIndex = 72;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 118);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 13);
            this.label15.TabIndex = 71;
            this.label15.Text = "Producto [F4]:";
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Location = new System.Drawing.Point(92, 115);
            this.txtCodigoProducto.MaxLength = 15;
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(140, 20);
            this.txtCodigoProducto.TabIndex = 70;
            this.txtCodigoProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoProducto_KeyDown);
            this.txtCodigoProducto.Leave += new System.EventHandler(this.txtCodigoProducto_Leave);
            // 
            // dgvOrdenPedidoDetalle
            // 
            this.dgvOrdenPedidoDetalle.AllowUserToAddRows = false;
            this.dgvOrdenPedidoDetalle.AllowUserToDeleteRows = false;
            this.dgvOrdenPedidoDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenPedidoDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column2,
            this.dicEliminarProducto});
            this.dgvOrdenPedidoDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvOrdenPedidoDetalle.Location = new System.Drawing.Point(14, 170);
            this.dgvOrdenPedidoDetalle.Name = "dgvOrdenPedidoDetalle";
            this.dgvOrdenPedidoDetalle.RowHeadersVisible = false;
            this.dgvOrdenPedidoDetalle.Size = new System.Drawing.Size(660, 305);
            this.dgvOrdenPedidoDetalle.TabIndex = 75;
            this.dgvOrdenPedidoDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenPedidoDetalle_CellClick);
            this.dgvOrdenPedidoDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdenPedidoDetalle_CellEndEdit);
            this.dgvOrdenPedidoDetalle.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvOrdenPedidoDetalle_DataError);
            this.dgvOrdenPedidoDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvOrdenPedidoDetalle_EditingControlShowing);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "descripcionProducto";
            this.Column1.HeaderText = "Producto";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 400;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "fisicoDisponible";
            this.Column3.HeaderText = "Físico disponible";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 120;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "cantidad";
            this.Column2.HeaderText = "Cantidad";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // dicEliminarProducto
            // 
            this.dicEliminarProducto.HeaderText = "";
            this.dicEliminarProducto.Image = ((System.Drawing.Image)(resources.GetObject("dicEliminarProducto.Image")));
            this.dicEliminarProducto.Name = "dicEliminarProducto";
            this.dicEliminarProducto.Width = 20;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(614, 141);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 94;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(548, 141);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(60, 23);
            this.btnAgregar.TabIndex = 93;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtMotivo
            // 
            this.txtMotivo.Location = new System.Drawing.Point(92, 62);
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(273, 20);
            this.txtMotivo.TabIndex = 96;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 95;
            this.label1.Text = "Motivo:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.toolStripSeparator2,
            this.tsbGrabar,
            this.toolStripSeparator1,
            this.tsbGenerar,
            this.toolStripSeparator4,
            this.tsbEliminar,
            this.toolStripSeparator3,
            this.tsbBuscar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(690, 25);
            this.toolStrip1.TabIndex = 97;
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
            // tsbGenerar
            // 
            this.tsbGenerar.Image = ((System.Drawing.Image)(resources.GetObject("tsbGenerar.Image")));
            this.tsbGenerar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGenerar.Name = "tsbGenerar";
            this.tsbGenerar.Size = new System.Drawing.Size(68, 22);
            this.tsbGenerar.Text = "Generar";
            this.tsbGenerar.Click += new System.EventHandler(this.tsbGenerar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            // txtSolicita
            // 
            this.txtSolicita.Location = new System.Drawing.Point(92, 36);
            this.txtSolicita.Name = "txtSolicita";
            this.txtSolicita.ReadOnly = true;
            this.txtSolicita.Size = new System.Drawing.Size(273, 20);
            this.txtSolicita.TabIndex = 99;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 98;
            this.label2.Text = "Solicita:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 100;
            this.label3.Text = "Fecha - hora ingreso:";
            // 
            // txtFechaHoraIngreso
            // 
            this.txtFechaHoraIngreso.Location = new System.Drawing.Point(539, 36);
            this.txtFechaHoraIngreso.Name = "txtFechaHoraIngreso";
            this.txtFechaHoraIngreso.ReadOnly = true;
            this.txtFechaHoraIngreso.Size = new System.Drawing.Size(135, 20);
            this.txtFechaHoraIngreso.TabIndex = 101;
            // 
            // txtFechaHoraUltimaModificacion
            // 
            this.txtFechaHoraUltimaModificacion.Location = new System.Drawing.Point(539, 62);
            this.txtFechaHoraUltimaModificacion.Name = "txtFechaHoraUltimaModificacion";
            this.txtFechaHoraUltimaModificacion.ReadOnly = true;
            this.txtFechaHoraUltimaModificacion.Size = new System.Drawing.Size(135, 20);
            this.txtFechaHoraUltimaModificacion.TabIndex = 103;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(371, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 13);
            this.label4.TabIndex = 102;
            this.label4.Text = "Fecha - hora última modificación:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 105;
            this.label5.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Location = new System.Drawing.Point(92, 88);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(273, 21);
            this.cmbProveedor.TabIndex = 104;
            // 
            // txtUsuarioUltimaModificacion
            // 
            this.txtUsuarioUltimaModificacion.Location = new System.Drawing.Point(539, 88);
            this.txtUsuarioUltimaModificacion.Name = "txtUsuarioUltimaModificacion";
            this.txtUsuarioUltimaModificacion.ReadOnly = true;
            this.txtUsuarioUltimaModificacion.Size = new System.Drawing.Size(135, 20);
            this.txtUsuarioUltimaModificacion.TabIndex = 107;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(371, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 13);
            this.label6.TabIndex = 106;
            this.label6.Text = "Usuario última modificación:";
            // 
            // frmOrdenPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 490);
            this.Controls.Add(this.txtUsuarioUltimaModificacion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbProveedor);
            this.Controls.Add(this.txtFechaHoraUltimaModificacion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFechaHoraIngreso);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSolicita);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvOrdenPedidoDetalle);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtDescripcionProducto);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtCodigoProducto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmOrdenPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmOrdenPedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenPedidoDetalle)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtDescripcionProducto;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.DataGridView dgvOrdenPedidoDetalle;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
        private System.Windows.Forms.TextBox txtSolicita;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFechaHoraIngreso;
        private System.Windows.Forms.TextBox txtFechaHoraUltimaModificacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewImageColumn dicEliminarProducto;
        private System.Windows.Forms.ToolStripButton tsbGenerar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.TextBox txtUsuarioUltimaModificacion;
        private System.Windows.Forms.Label label6;
    }
}