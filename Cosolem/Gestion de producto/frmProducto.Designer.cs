namespace Cosolem
{
    partial class frmProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProducto));
            this.cmbTipoProducto = new System.Windows.Forms.ComboBox();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtCaracteristicas = new System.Windows.Forms.TextBox();
            this.txtUrlEspecificaciones = new System.Windows.Forms.TextBox();
            this.txtUrlManual = new System.Windows.Forms.TextBox();
            this.pbxImagen = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvProductosComplementarios = new System.Windows.Forms.DataGridView();
            this.imgEliminarProductoComplementario = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCaracteristicasProductoComplementario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAgregarProductosComplementarios = new System.Windows.Forms.Button();
            this.txtCapacidad = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosComplementarios)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbTipoProducto
            // 
            this.cmbTipoProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoProducto.FormattingEnabled = true;
            this.cmbTipoProducto.Location = new System.Drawing.Point(119, 36);
            this.cmbTipoProducto.Name = "cmbTipoProducto";
            this.cmbTipoProducto.Size = new System.Drawing.Size(156, 21);
            this.cmbTipoProducto.TabIndex = 6;
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Location = new System.Drawing.Point(119, 63);
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(156, 20);
            this.txtCodigoProducto.TabIndex = 7;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(119, 89);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(156, 20);
            this.txtDescripcion.TabIndex = 8;
            // 
            // txtCaracteristicas
            // 
            this.txtCaracteristicas.Location = new System.Drawing.Point(119, 115);
            this.txtCaracteristicas.Multiline = true;
            this.txtCaracteristicas.Name = "txtCaracteristicas";
            this.txtCaracteristicas.Size = new System.Drawing.Size(156, 73);
            this.txtCaracteristicas.TabIndex = 9;
            // 
            // txtUrlEspecificaciones
            // 
            this.txtUrlEspecificaciones.Location = new System.Drawing.Point(119, 194);
            this.txtUrlEspecificaciones.Name = "txtUrlEspecificaciones";
            this.txtUrlEspecificaciones.Size = new System.Drawing.Size(156, 20);
            this.txtUrlEspecificaciones.TabIndex = 10;
            // 
            // txtUrlManual
            // 
            this.txtUrlManual.Location = new System.Drawing.Point(119, 220);
            this.txtUrlManual.Name = "txtUrlManual";
            this.txtUrlManual.Size = new System.Drawing.Size(156, 20);
            this.txtUrlManual.TabIndex = 11;
            // 
            // pbxImagen
            // 
            this.pbxImagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbxImagen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxImagen.Location = new System.Drawing.Point(332, 36);
            this.pbxImagen.Name = "pbxImagen";
            this.pbxImagen.Size = new System.Drawing.Size(239, 230);
            this.pbxImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxImagen.TabIndex = 12;
            this.pbxImagen.TabStop = false;
            this.pbxImagen.Click += new System.EventHandler(this.pbxImagen_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Tipo de producto:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Código de producto:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Descripción:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Url manual:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Url especificaciones:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Características:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(281, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Imagen:";
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
            this.toolStrip1.Size = new System.Drawing.Size(581, 25);
            this.toolStrip1.TabIndex = 32;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvProductosComplementarios);
            this.groupBox1.Controls.Add(this.btnAgregarProductosComplementarios);
            this.groupBox1.Location = new System.Drawing.Point(11, 272);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 264);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Productos sustitutos";
            // 
            // dgvProductosComplementarios
            // 
            this.dgvProductosComplementarios.AllowUserToAddRows = false;
            this.dgvProductosComplementarios.AllowUserToDeleteRows = false;
            this.dgvProductosComplementarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductosComplementarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imgEliminarProductoComplementario,
            this.Column1,
            this.txtCaracteristicasProductoComplementario});
            this.dgvProductosComplementarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductosComplementarios.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvProductosComplementarios.Location = new System.Drawing.Point(3, 44);
            this.dgvProductosComplementarios.Name = "dgvProductosComplementarios";
            this.dgvProductosComplementarios.ReadOnly = true;
            this.dgvProductosComplementarios.RowHeadersVisible = false;
            this.dgvProductosComplementarios.Size = new System.Drawing.Size(554, 217);
            this.dgvProductosComplementarios.TabIndex = 0;
            this.dgvProductosComplementarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductosComplementarios_CellClick);
            this.dgvProductosComplementarios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductosComplementarios_CellDoubleClick);
            // 
            // imgEliminarProductoComplementario
            // 
            this.imgEliminarProductoComplementario.HeaderText = "";
            this.imgEliminarProductoComplementario.Image = ((System.Drawing.Image)(resources.GetObject("imgEliminarProductoComplementario.Image")));
            this.imgEliminarProductoComplementario.Name = "imgEliminarProductoComplementario";
            this.imgEliminarProductoComplementario.ReadOnly = true;
            this.imgEliminarProductoComplementario.Width = 20;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "producto";
            this.Column1.HeaderText = "Producto";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // txtCaracteristicasProductoComplementario
            // 
            this.txtCaracteristicasProductoComplementario.DataPropertyName = "caracteristicas";
            this.txtCaracteristicasProductoComplementario.HeaderText = "Características";
            this.txtCaracteristicasProductoComplementario.Name = "txtCaracteristicasProductoComplementario";
            this.txtCaracteristicasProductoComplementario.ReadOnly = true;
            this.txtCaracteristicasProductoComplementario.Width = 300;
            // 
            // btnAgregarProductosComplementarios
            // 
            this.btnAgregarProductosComplementarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAgregarProductosComplementarios.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarProductosComplementarios.Image")));
            this.btnAgregarProductosComplementarios.Location = new System.Drawing.Point(3, 16);
            this.btnAgregarProductosComplementarios.Name = "btnAgregarProductosComplementarios";
            this.btnAgregarProductosComplementarios.Size = new System.Drawing.Size(554, 28);
            this.btnAgregarProductosComplementarios.TabIndex = 1;
            this.btnAgregarProductosComplementarios.Text = "Agregar productos complementarios";
            this.btnAgregarProductosComplementarios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarProductosComplementarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarProductosComplementarios.UseVisualStyleBackColor = true;
            this.btnAgregarProductosComplementarios.Click += new System.EventHandler(this.btnAgregarProductosComplementarios_Click);
            // 
            // txtCapacidad
            // 
            this.txtCapacidad.Location = new System.Drawing.Point(119, 246);
            this.txtCapacidad.Name = "txtCapacidad";
            this.txtCapacidad.Size = new System.Drawing.Size(156, 20);
            this.txtCapacidad.TabIndex = 34;
            this.txtCapacidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCapacidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapacidad_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Capacidad:";
            // 
            // frmProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 542);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCapacidad);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pbxImagen);
            this.Controls.Add(this.txtUrlManual);
            this.Controls.Add(this.txtUrlEspecificaciones);
            this.Controls.Add(this.txtCaracteristicas);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtCodigoProducto);
            this.Controls.Add(this.cmbTipoProducto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagen)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosComplementarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbTipoProducto;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtCaracteristicas;
        private System.Windows.Forms.TextBox txtUrlEspecificaciones;
        private System.Windows.Forms.TextBox txtUrlManual;
        private System.Windows.Forms.PictureBox pbxImagen;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvProductosComplementarios;
        private System.Windows.Forms.Button btnAgregarProductosComplementarios;
        private System.Windows.Forms.DataGridViewImageColumn imgEliminarProductoComplementario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCaracteristicasProductoComplementario;
        private System.Windows.Forms.TextBox txtCapacidad;
        private System.Windows.Forms.Label label7;
    }
}