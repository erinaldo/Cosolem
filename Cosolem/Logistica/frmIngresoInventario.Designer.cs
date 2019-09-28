namespace Cosolem
{
    partial class frmIngresoInventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIngresoInventario));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.cmbTienda = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBodega = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFiltroBusqueda = new System.Windows.Forms.TextBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvInventario = new System.Windows.Forms.DataGridView();
            this.dicEliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAgregarParaIngresarInventario = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.toolStripSeparator2,
            this.tsbGrabar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(916, 25);
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
            // cmbTienda
            // 
            this.cmbTienda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTienda.FormattingEnabled = true;
            this.cmbTienda.Location = new System.Drawing.Point(87, 59);
            this.cmbTienda.Name = "cmbTienda";
            this.cmbTienda.Size = new System.Drawing.Size(243, 21);
            this.cmbTienda.TabIndex = 59;
            this.cmbTienda.SelectionChangeCommitted += new System.EventHandler(this.cmbTienda_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "Tienda:";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(87, 32);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(243, 21);
            this.cmbEmpresa.TabIndex = 57;
            this.cmbEmpresa.SelectionChangeCommitted += new System.EventHandler(this.cmbEmpresa_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Empresa:";
            // 
            // cmbBodega
            // 
            this.cmbBodega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(87, 86);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(243, 21);
            this.cmbBodega.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "Bodega:";
            // 
            // txtFiltroBusqueda
            // 
            this.txtFiltroBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFiltroBusqueda.Location = new System.Drawing.Point(3, 16);
            this.txtFiltroBusqueda.Name = "txtFiltroBusqueda";
            this.txtFiltroBusqueda.Size = new System.Drawing.Size(312, 20);
            this.txtFiltroBusqueda.TabIndex = 64;
            this.txtFiltroBusqueda.TextChanged += new System.EventHandler(this.txtFiltroBusqueda_TextChanged);
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column6,
            this.Column1});
            this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductos.Location = new System.Drawing.Point(3, 36);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.Size = new System.Drawing.Size(312, 293);
            this.dgvProductos.TabIndex = 66;
            this.dgvProductos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellEndEdit);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "seleccionado";
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            this.Column2.Width = 20;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "codigoProducto";
            this.Column6.HeaderText = "Código";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "descripcion";
            this.Column1.HeaderText = "Producto";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 260;
            // 
            // dgvInventario
            // 
            this.dgvInventario.AllowUserToAddRows = false;
            this.dgvInventario.AllowUserToDeleteRows = false;
            this.dgvInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dicEliminar,
            this.Column7,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dgvInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventario.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvInventario.Location = new System.Drawing.Point(3, 16);
            this.dgvInventario.Name = "dgvInventario";
            this.dgvInventario.RowHeadersVisible = false;
            this.dgvInventario.Size = new System.Drawing.Size(562, 417);
            this.dgvInventario.TabIndex = 67;
            this.dgvInventario.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventario_CellClick);
            this.dgvInventario.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventario_CellEndEdit);
            this.dgvInventario.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvInventario_DataError);
            this.dgvInventario.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvInventario_EditingControlShowing);
            // 
            // dicEliminar
            // 
            this.dicEliminar.HeaderText = "";
            this.dicEliminar.Image = ((System.Drawing.Image)(resources.GetObject("dicEliminar.Image")));
            this.dicEliminar.Name = "dicEliminar";
            this.dicEliminar.Width = 20;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "codigoProducto";
            this.Column7.HeaderText = "Código";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "descripcionProducto";
            this.Column3.HeaderText = "Producto";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 260;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "fisicoDisponible";
            this.Column4.HeaderText = "Inventario";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 70;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "cantidad";
            this.Column5.HeaderText = "Cantidad";
            this.Column5.Name = "Column5";
            this.Column5.Width = 70;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvProductos);
            this.groupBox1.Controls.Add(this.btnAgregarParaIngresarInventario);
            this.groupBox1.Controls.Add(this.txtFiltroBusqueda);
            this.groupBox1.Location = new System.Drawing.Point(12, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 355);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Productos";
            // 
            // btnAgregarParaIngresarInventario
            // 
            this.btnAgregarParaIngresarInventario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAgregarParaIngresarInventario.Location = new System.Drawing.Point(3, 329);
            this.btnAgregarParaIngresarInventario.Name = "btnAgregarParaIngresarInventario";
            this.btnAgregarParaIngresarInventario.Size = new System.Drawing.Size(312, 23);
            this.btnAgregarParaIngresarInventario.TabIndex = 69;
            this.btnAgregarParaIngresarInventario.Text = "Agregar para ingresar inventario";
            this.btnAgregarParaIngresarInventario.UseVisualStyleBackColor = true;
            this.btnAgregarParaIngresarInventario.Click += new System.EventHandler(this.btnAgregarParaIngresarInventario_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvInventario);
            this.groupBox2.Location = new System.Drawing.Point(336, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(568, 436);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ingresar inventario";
            // 
            // frmIngresoInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 473);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbBodega);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTienda);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmIngresoInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmIngresoInventario_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.ComboBox cmbTienda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbEmpresa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBodega;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFiltroBusqueda;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.DataGridView dgvInventario;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAgregarParaIngresarInventario;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn dicEliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}