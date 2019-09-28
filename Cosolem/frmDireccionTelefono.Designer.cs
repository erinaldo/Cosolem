namespace Cosolem
{
    partial class frmDireccionTelefono
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDireccionTelefono));
            this.cmbProvincia = new System.Windows.Forms.ComboBox();
            this.cmbCanton = new System.Windows.Forms.ComboBox();
            this.cmbTipoDireccion = new System.Windows.Forms.ComboBox();
            this.txtDireccionCompleta = new System.Windows.Forms.TextBox();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.dgvTelefonos = new System.Windows.Forms.DataGridView();
            this.imgEliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbTipoTelefono = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cmbOperadora = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.txtNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtExtension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbEsPrincipal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAgregarTelefonos = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelefonos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbProvincia
            // 
            this.cmbProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvincia.FormattingEnabled = true;
            this.cmbProvincia.Location = new System.Drawing.Point(112, 33);
            this.cmbProvincia.Name = "cmbProvincia";
            this.cmbProvincia.Size = new System.Drawing.Size(156, 21);
            this.cmbProvincia.TabIndex = 0;
            this.cmbProvincia.SelectionChangeCommitted += new System.EventHandler(this.cmbProvincia_SelectionChangeCommitted);
            // 
            // cmbCanton
            // 
            this.cmbCanton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCanton.FormattingEnabled = true;
            this.cmbCanton.Location = new System.Drawing.Point(324, 33);
            this.cmbCanton.Name = "cmbCanton";
            this.cmbCanton.Size = new System.Drawing.Size(156, 21);
            this.cmbCanton.TabIndex = 1;
            // 
            // cmbTipoDireccion
            // 
            this.cmbTipoDireccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoDireccion.FormattingEnabled = true;
            this.cmbTipoDireccion.Location = new System.Drawing.Point(112, 60);
            this.cmbTipoDireccion.Name = "cmbTipoDireccion";
            this.cmbTipoDireccion.Size = new System.Drawing.Size(368, 21);
            this.cmbTipoDireccion.TabIndex = 2;
            // 
            // txtDireccionCompleta
            // 
            this.txtDireccionCompleta.Location = new System.Drawing.Point(112, 87);
            this.txtDireccionCompleta.Multiline = true;
            this.txtDireccionCompleta.Name = "txtDireccionCompleta";
            this.txtDireccionCompleta.Size = new System.Drawing.Size(368, 60);
            this.txtDireccionCompleta.TabIndex = 3;
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(112, 153);
            this.txtReferencia.Multiline = true;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(368, 60);
            this.txtReferencia.TabIndex = 4;
            // 
            // dgvTelefonos
            // 
            this.dgvTelefonos.AllowUserToAddRows = false;
            this.dgvTelefonos.AllowUserToDeleteRows = false;
            this.dgvTelefonos.AllowUserToResizeRows = false;
            this.dgvTelefonos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTelefonos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imgEliminar,
            this.cmbTipoTelefono,
            this.cmbOperadora,
            this.txtNumero,
            this.txtExtension,
            this.chbEsPrincipal});
            this.dgvTelefonos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTelefonos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTelefonos.Location = new System.Drawing.Point(3, 39);
            this.dgvTelefonos.Name = "dgvTelefonos";
            this.dgvTelefonos.RowHeadersVisible = false;
            this.dgvTelefonos.Size = new System.Drawing.Size(466, 125);
            this.dgvTelefonos.TabIndex = 6;
            this.dgvTelefonos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTelefonos_CellClick);
            this.dgvTelefonos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTelefonos_CellEndEdit);
            this.dgvTelefonos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvTelefonos_EditingControlShowing);
            // 
            // imgEliminar
            // 
            this.imgEliminar.HeaderText = "";
            this.imgEliminar.Image = ((System.Drawing.Image)(resources.GetObject("imgEliminar.Image")));
            this.imgEliminar.Name = "imgEliminar";
            this.imgEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.imgEliminar.Width = 20;
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
            // txtNumero
            // 
            this.txtNumero.DataPropertyName = "numero";
            this.txtNumero.HeaderText = "Número";
            this.txtNumero.Name = "txtNumero";
            // 
            // txtExtension
            // 
            this.txtExtension.DataPropertyName = "extension";
            this.txtExtension.HeaderText = "Extensión";
            this.txtExtension.Name = "txtExtension";
            // 
            // chbEsPrincipal
            // 
            this.chbEsPrincipal.DataPropertyName = "esPrincipal";
            this.chbEsPrincipal.HeaderText = "";
            this.chbEsPrincipal.Name = "chbEsPrincipal";
            this.chbEsPrincipal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chbEsPrincipal.Width = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Provincia:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cantón:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tipo de dirección:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Dirección completa:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Referencia:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvTelefonos);
            this.groupBox1.Controls.Add(this.btnAgregarTelefonos);
            this.groupBox1.Location = new System.Drawing.Point(8, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 167);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Teléfonos";
            // 
            // btnAgregarTelefonos
            // 
            this.btnAgregarTelefonos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAgregarTelefonos.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarTelefonos.Image")));
            this.btnAgregarTelefonos.Location = new System.Drawing.Point(3, 16);
            this.btnAgregarTelefonos.Name = "btnAgregarTelefonos";
            this.btnAgregarTelefonos.Size = new System.Drawing.Size(466, 23);
            this.btnAgregarTelefonos.TabIndex = 7;
            this.btnAgregarTelefonos.Text = "Agregar teléfonos";
            this.btnAgregarTelefonos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarTelefonos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarTelefonos.UseVisualStyleBackColor = true;
            this.btnAgregarTelefonos.Click += new System.EventHandler(this.btnAgregarTelefonos_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbGrabar,
            this.toolStripSeparator1,
            this.tsbEliminar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(488, 25);
            this.toolStrip1.TabIndex = 13;
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
            // frmDireccionTelefono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 389);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReferencia);
            this.Controls.Add(this.txtDireccionCompleta);
            this.Controls.Add(this.cmbTipoDireccion);
            this.Controls.Add(this.cmbCanton);
            this.Controls.Add(this.cmbProvincia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmDireccionTelefono";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Direcciones y teléfonos";
            this.Load += new System.EventHandler(this.frmDireccion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelefonos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProvincia;
        private System.Windows.Forms.ComboBox cmbCanton;
        private System.Windows.Forms.ComboBox cmbTipoDireccion;
        private System.Windows.Forms.TextBox txtDireccionCompleta;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.DataGridView dgvTelefonos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAgregarTelefonos;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.DataGridViewImageColumn imgEliminar;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmbTipoTelefono;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmbOperadora;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtExtension;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chbEsPrincipal;
    }
}