namespace Cosolem
{
    partial class frmTransferencias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransferencias));
            this.label5 = new System.Windows.Forms.Label();
            this.txtFisicoDisponible = new System.Windows.Forms.TextBox();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbBodegaSaliente = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTiendaSaliente = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGrabar = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbTiendaEntrante = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBodegaEntrante = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCantidadTransferir = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtReservado = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtInventario = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(280, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "Fisíco disponible:";
            // 
            // txtFisicoDisponible
            // 
            this.txtFisicoDisponible.Enabled = false;
            this.txtFisicoDisponible.Location = new System.Drawing.Point(375, 121);
            this.txtFisicoDisponible.Name = "txtFisicoDisponible";
            this.txtFisicoDisponible.Size = new System.Drawing.Size(60, 20);
            this.txtFisicoDisponible.TabIndex = 75;
            // 
            // cmbProducto
            // 
            this.cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(68, 121);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(206, 21);
            this.cmbProducto.TabIndex = 74;
            this.cmbProducto.SelectionChangeCommitted += new System.EventHandler(this.cmbProducto_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 73;
            this.label4.Text = "Producto:";
            // 
            // cmbBodegaSaliente
            // 
            this.cmbBodegaSaliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBodegaSaliente.FormattingEnabled = true;
            this.cmbBodegaSaliente.Location = new System.Drawing.Point(71, 46);
            this.cmbBodegaSaliente.Name = "cmbBodegaSaliente";
            this.cmbBodegaSaliente.Size = new System.Drawing.Size(243, 21);
            this.cmbBodegaSaliente.TabIndex = 72;
            this.cmbBodegaSaliente.SelectionChangeCommitted += new System.EventHandler(this.cmbBodegaSaliente_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Bodega:";
            // 
            // cmbTiendaSaliente
            // 
            this.cmbTiendaSaliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTiendaSaliente.FormattingEnabled = true;
            this.cmbTiendaSaliente.Location = new System.Drawing.Point(71, 19);
            this.cmbTiendaSaliente.Name = "cmbTiendaSaliente";
            this.cmbTiendaSaliente.Size = new System.Drawing.Size(243, 21);
            this.cmbTiendaSaliente.TabIndex = 70;
            this.cmbTiendaSaliente.SelectionChangeCommitted += new System.EventHandler(this.cmbTiendaSaliente_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Tienda:";
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
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.toolStripSeparator2,
            this.tsbGrabar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(712, 25);
            this.toolStrip1.TabIndex = 66;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbTiendaSaliente);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbBodegaSaliente);
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 83);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bodega saliente";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmbTiendaEntrante);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbBodegaEntrante);
            this.groupBox2.Location = new System.Drawing.Point(358, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 83);
            this.groupBox2.TabIndex = 84;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bodega entrante";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 69;
            this.label7.Text = "Tienda:";
            // 
            // cmbTiendaEntrante
            // 
            this.cmbTiendaEntrante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTiendaEntrante.FormattingEnabled = true;
            this.cmbTiendaEntrante.Location = new System.Drawing.Point(71, 19);
            this.cmbTiendaEntrante.Name = "cmbTiendaEntrante";
            this.cmbTiendaEntrante.Size = new System.Drawing.Size(243, 21);
            this.cmbTiendaEntrante.TabIndex = 70;
            this.cmbTiendaEntrante.SelectionChangeCommitted += new System.EventHandler(this.cmbTiendaEntrante_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 71;
            this.label8.Text = "Bodega:";
            // 
            // cmbBodegaEntrante
            // 
            this.cmbBodegaEntrante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBodegaEntrante.FormattingEnabled = true;
            this.cmbBodegaEntrante.Location = new System.Drawing.Point(71, 46);
            this.cmbBodegaEntrante.Name = "cmbBodegaEntrante";
            this.cmbBodegaEntrante.Size = new System.Drawing.Size(243, 21);
            this.cmbBodegaEntrante.TabIndex = 72;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(528, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 86;
            this.label9.Text = "Cantidad a transferir:";
            // 
            // txtCantidadTransferir
            // 
            this.txtCantidadTransferir.Location = new System.Drawing.Point(638, 147);
            this.txtCantidadTransferir.Name = "txtCantidadTransferir";
            this.txtCantidadTransferir.Size = new System.Drawing.Size(60, 20);
            this.txtCantidadTransferir.TabIndex = 85;
            this.txtCantidadTransferir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadTransferir_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(441, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 88;
            this.label10.Text = "Reservado:";
            // 
            // txtReservado
            // 
            this.txtReservado.Enabled = false;
            this.txtReservado.Location = new System.Drawing.Point(509, 121);
            this.txtReservado.Name = "txtReservado";
            this.txtReservado.Size = new System.Drawing.Size(60, 20);
            this.txtReservado.TabIndex = 87;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(575, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 90;
            this.label11.Text = "Inventario:";
            // 
            // txtInventario
            // 
            this.txtInventario.Enabled = false;
            this.txtInventario.Location = new System.Drawing.Point(638, 121);
            this.txtInventario.Name = "txtInventario";
            this.txtInventario.Size = new System.Drawing.Size(60, 20);
            this.txtInventario.TabIndex = 89;
            // 
            // frmTransferencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 176);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtInventario);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtReservado);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCantidadTransferir);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFisicoDisponible);
            this.Controls.Add(this.cmbProducto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmTransferencias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmTransferencias_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFisicoDisponible;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbBodegaSaliente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTiendaSaliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbGrabar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTiendaEntrante;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBodegaEntrante;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCantidadTransferir;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtReservado;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtInventario;
    }
}