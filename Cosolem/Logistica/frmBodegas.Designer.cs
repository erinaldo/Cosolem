namespace Cosolem
{
    partial class frmBodegas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBodegas));
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBuscar = new System.Windows.Forms.ToolStripButton();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            this.cmbTienda = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvBodegas = new System.Windows.Forms.DataGridView();
            this.chbSeleccionado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtCaracteristicasProductoSustituto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.esFacturable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnAgregarBodegas = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBodegas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Empresa:";
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
            this.toolStrip1.Size = new System.Drawing.Size(343, 25);
            this.toolStrip1.TabIndex = 36;
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
            this.cmbEmpresa.Location = new System.Drawing.Point(66, 32);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(264, 21);
            this.cmbEmpresa.TabIndex = 39;
            this.cmbEmpresa.SelectionChangeCommitted += new System.EventHandler(this.cmbEmpresa_SelectionChangeCommitted);
            // 
            // cmbTienda
            // 
            this.cmbTienda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTienda.FormattingEnabled = true;
            this.cmbTienda.Location = new System.Drawing.Point(66, 59);
            this.cmbTienda.Name = "cmbTienda";
            this.cmbTienda.Size = new System.Drawing.Size(264, 21);
            this.cmbTienda.TabIndex = 55;
            this.cmbTienda.SelectionChangeCommitted += new System.EventHandler(this.cmbTienda_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Tienda:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvBodegas);
            this.groupBox1.Controls.Add(this.btnAgregarBodegas);
            this.groupBox1.Location = new System.Drawing.Point(12, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 290);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bodegas";
            // 
            // dgvBodegas
            // 
            this.dgvBodegas.AllowUserToAddRows = false;
            this.dgvBodegas.AllowUserToDeleteRows = false;
            this.dgvBodegas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBodegas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chbSeleccionado,
            this.txtCaracteristicasProductoSustituto,
            this.esFacturable});
            this.dgvBodegas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBodegas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvBodegas.Location = new System.Drawing.Point(3, 44);
            this.dgvBodegas.Name = "dgvBodegas";
            this.dgvBodegas.RowHeadersVisible = false;
            this.dgvBodegas.Size = new System.Drawing.Size(312, 243);
            this.dgvBodegas.TabIndex = 0;
            this.dgvBodegas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBodegas_CellEndEdit);
            // 
            // chbSeleccionado
            // 
            this.chbSeleccionado.DataPropertyName = "seleccionado";
            this.chbSeleccionado.HeaderText = "";
            this.chbSeleccionado.Name = "chbSeleccionado";
            this.chbSeleccionado.Width = 20;
            // 
            // txtCaracteristicasProductoSustituto
            // 
            this.txtCaracteristicasProductoSustituto.DataPropertyName = "descripcion";
            this.txtCaracteristicasProductoSustituto.HeaderText = "Descripción";
            this.txtCaracteristicasProductoSustituto.Name = "txtCaracteristicasProductoSustituto";
            this.txtCaracteristicasProductoSustituto.Width = 200;
            // 
            // esFacturable
            // 
            this.esFacturable.DataPropertyName = "esFacturable";
            this.esFacturable.HeaderText = "";
            this.esFacturable.Name = "esFacturable";
            this.esFacturable.Width = 20;
            // 
            // btnAgregarBodegas
            // 
            this.btnAgregarBodegas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAgregarBodegas.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarBodegas.Image")));
            this.btnAgregarBodegas.Location = new System.Drawing.Point(3, 16);
            this.btnAgregarBodegas.Name = "btnAgregarBodegas";
            this.btnAgregarBodegas.Size = new System.Drawing.Size(312, 28);
            this.btnAgregarBodegas.TabIndex = 1;
            this.btnAgregarBodegas.Text = "Agregar bodegas";
            this.btnAgregarBodegas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarBodegas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarBodegas.UseVisualStyleBackColor = true;
            this.btnAgregarBodegas.Click += new System.EventHandler(this.btnAgregarBodegas_Click);
            // 
            // frmBodegas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 387);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbTienda);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbEmpresa);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmBodegas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmBodegas_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBodegas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
        private System.Windows.Forms.ComboBox cmbEmpresa;
        private System.Windows.Forms.ComboBox cmbTienda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvBodegas;
        private System.Windows.Forms.Button btnAgregarBodegas;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chbSeleccionado;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCaracteristicasProductoSustituto;
        private System.Windows.Forms.DataGridViewCheckBoxColumn esFacturable;
    }
}