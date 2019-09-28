namespace Cosolem
{
    partial class frmSeguimientoCotizacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSeguimientoCotizacion));
            this.txtComentarioSeguimiento = new System.Windows.Forms.TextBox();
            this.cmbEstadoSeguimientoCotizacion = new System.Windows.Forms.ComboBox();
            this.dgvDetalleSeguimientoCotizacion = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tstOpciones = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFechaHoraIngreso = new System.Windows.Forms.TextBox();
            this.txtFechaHoraUltimaModificacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUsuarioUltimaModificacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUsuarioIngreso = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFechaProximoSeguimiento = new System.Windows.Forms.DateTimePicker();
            this.pnlDatosSeguimiento = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleSeguimientoCotizacion)).BeginInit();
            this.tstOpciones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlDatosSeguimiento.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtComentarioSeguimiento
            // 
            this.txtComentarioSeguimiento.Location = new System.Drawing.Point(75, 9);
            this.txtComentarioSeguimiento.Multiline = true;
            this.txtComentarioSeguimiento.Name = "txtComentarioSeguimiento";
            this.txtComentarioSeguimiento.Size = new System.Drawing.Size(374, 99);
            this.txtComentarioSeguimiento.TabIndex = 0;
            // 
            // cmbEstadoSeguimientoCotizacion
            // 
            this.cmbEstadoSeguimientoCotizacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoSeguimientoCotizacion.FormattingEnabled = true;
            this.cmbEstadoSeguimientoCotizacion.Location = new System.Drawing.Point(536, 9);
            this.cmbEstadoSeguimientoCotizacion.Name = "cmbEstadoSeguimientoCotizacion";
            this.cmbEstadoSeguimientoCotizacion.Size = new System.Drawing.Size(213, 21);
            this.cmbEstadoSeguimientoCotizacion.TabIndex = 1;
            this.cmbEstadoSeguimientoCotizacion.SelectionChangeCommitted += new System.EventHandler(this.cmbEstadoSeguimientoCotizacion_SelectionChangeCommitted);
            // 
            // dgvDetalleSeguimientoCotizacion
            // 
            this.dgvDetalleSeguimientoCotizacion.AllowUserToAddRows = false;
            this.dgvDetalleSeguimientoCotizacion.AllowUserToDeleteRows = false;
            this.dgvDetalleSeguimientoCotizacion.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDetalleSeguimientoCotizacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleSeguimientoCotizacion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            this.dgvDetalleSeguimientoCotizacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalleSeguimientoCotizacion.Location = new System.Drawing.Point(3, 16);
            this.dgvDetalleSeguimientoCotizacion.Name = "dgvDetalleSeguimientoCotizacion";
            this.dgvDetalleSeguimientoCotizacion.ReadOnly = true;
            this.dgvDetalleSeguimientoCotizacion.RowHeadersVisible = false;
            this.dgvDetalleSeguimientoCotizacion.Size = new System.Drawing.Size(761, 216);
            this.dgvDetalleSeguimientoCotizacion.TabIndex = 2;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "fechaProximoSeguimiento";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column3.HeaderText = "Fecha próximo seguimiento";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 170;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "comentarioSeguimiento";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Comentario";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 380;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "fechaHoraIngreso";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy\' - \'HH:mm:ss";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Fecha - hora ingreso";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // tstOpciones
            // 
            this.tstOpciones.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.toolStripSeparator2,
            this.tsbGrabar});
            this.tstOpciones.Location = new System.Drawing.Point(0, 0);
            this.tstOpciones.Name = "tstOpciones";
            this.tstOpciones.Size = new System.Drawing.Size(767, 25);
            this.tstOpciones.TabIndex = 33;
            this.tstOpciones.Text = "toolStrip1";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvDetalleSeguimientoCotizacion);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(767, 235);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle de seguimiento de cotización";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Comentario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(455, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Estado actual:";
            // 
            // txtFechaHoraIngreso
            // 
            this.txtFechaHoraIngreso.Enabled = false;
            this.txtFechaHoraIngreso.Location = new System.Drawing.Point(625, 36);
            this.txtFechaHoraIngreso.Name = "txtFechaHoraIngreso";
            this.txtFechaHoraIngreso.Size = new System.Drawing.Size(124, 20);
            this.txtFechaHoraIngreso.TabIndex = 37;
            // 
            // txtFechaHoraUltimaModificacion
            // 
            this.txtFechaHoraUltimaModificacion.Enabled = false;
            this.txtFechaHoraUltimaModificacion.Location = new System.Drawing.Point(625, 88);
            this.txtFechaHoraUltimaModificacion.Name = "txtFechaHoraUltimaModificacion";
            this.txtFechaHoraUltimaModificacion.Size = new System.Drawing.Size(124, 20);
            this.txtFechaHoraUltimaModificacion.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(455, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Fecha - hora ingreso:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(455, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Fecha - hora última modificación:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(455, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Usuario última modificación:";
            // 
            // txtUsuarioUltimaModificacion
            // 
            this.txtUsuarioUltimaModificacion.Enabled = false;
            this.txtUsuarioUltimaModificacion.Location = new System.Drawing.Point(625, 114);
            this.txtUsuarioUltimaModificacion.Name = "txtUsuarioUltimaModificacion";
            this.txtUsuarioUltimaModificacion.Size = new System.Drawing.Size(124, 20);
            this.txtUsuarioUltimaModificacion.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(455, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Usuario ingreso:";
            // 
            // txtUsuarioIngreso
            // 
            this.txtUsuarioIngreso.Enabled = false;
            this.txtUsuarioIngreso.Location = new System.Drawing.Point(625, 62);
            this.txtUsuarioIngreso.Name = "txtUsuarioIngreso";
            this.txtUsuarioIngreso.Size = new System.Drawing.Size(124, 20);
            this.txtUsuarioIngreso.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 45;
            this.label7.Text = "Fecha próximo seguiento:";
            // 
            // dtpFechaProximoSeguimiento
            // 
            this.dtpFechaProximoSeguimiento.CustomFormat = "dd/MMMM/yyyy";
            this.dtpFechaProximoSeguimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaProximoSeguimiento.Location = new System.Drawing.Point(140, 114);
            this.dtpFechaProximoSeguimiento.Name = "dtpFechaProximoSeguimiento";
            this.dtpFechaProximoSeguimiento.Size = new System.Drawing.Size(160, 20);
            this.dtpFechaProximoSeguimiento.TabIndex = 46;
            // 
            // pnlDatosSeguimiento
            // 
            this.pnlDatosSeguimiento.Controls.Add(this.label1);
            this.pnlDatosSeguimiento.Controls.Add(this.dtpFechaProximoSeguimiento);
            this.pnlDatosSeguimiento.Controls.Add(this.txtComentarioSeguimiento);
            this.pnlDatosSeguimiento.Controls.Add(this.label7);
            this.pnlDatosSeguimiento.Controls.Add(this.cmbEstadoSeguimientoCotizacion);
            this.pnlDatosSeguimiento.Controls.Add(this.label6);
            this.pnlDatosSeguimiento.Controls.Add(this.label2);
            this.pnlDatosSeguimiento.Controls.Add(this.txtUsuarioIngreso);
            this.pnlDatosSeguimiento.Controls.Add(this.txtFechaHoraIngreso);
            this.pnlDatosSeguimiento.Controls.Add(this.label5);
            this.pnlDatosSeguimiento.Controls.Add(this.txtFechaHoraUltimaModificacion);
            this.pnlDatosSeguimiento.Controls.Add(this.txtUsuarioUltimaModificacion);
            this.pnlDatosSeguimiento.Controls.Add(this.label3);
            this.pnlDatosSeguimiento.Controls.Add(this.label4);
            this.pnlDatosSeguimiento.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDatosSeguimiento.Location = new System.Drawing.Point(0, 25);
            this.pnlDatosSeguimiento.Name = "pnlDatosSeguimiento";
            this.pnlDatosSeguimiento.Size = new System.Drawing.Size(767, 141);
            this.pnlDatosSeguimiento.TabIndex = 47;
            // 
            // frmSeguimientoCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 401);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlDatosSeguimiento);
            this.Controls.Add(this.tstOpciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSeguimientoCotizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSeguimientoCotizacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleSeguimientoCotizacion)).EndInit();
            this.tstOpciones.ResumeLayout(false);
            this.tstOpciones.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.pnlDatosSeguimiento.ResumeLayout(false);
            this.pnlDatosSeguimiento.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtComentarioSeguimiento;
        private System.Windows.Forms.ComboBox cmbEstadoSeguimientoCotizacion;
        private System.Windows.Forms.DataGridView dgvDetalleSeguimientoCotizacion;
        private System.Windows.Forms.ToolStrip tstOpciones;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFechaHoraIngreso;
        private System.Windows.Forms.TextBox txtFechaHoraUltimaModificacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUsuarioUltimaModificacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUsuarioIngreso;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFechaProximoSeguimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Panel pnlDatosSeguimiento;

    }
}