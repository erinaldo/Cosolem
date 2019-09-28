namespace Cosolem
{
    partial class frmPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.mnsOpciones = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslTerminal = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslNombreCompleto = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslEmpresa = new System.Windows.Forms.ToolStripLabel();
            this.ntfSeguimientoCotizacion = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmrTemporizador = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnsOpciones
            // 
            this.mnsOpciones.Location = new System.Drawing.Point(0, 0);
            this.mnsOpciones.Name = "mnsOpciones";
            this.mnsOpciones.Size = new System.Drawing.Size(924, 24);
            this.mnsOpciones.TabIndex = 0;
            this.mnsOpciones.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslTerminal,
            this.toolStripSeparator1,
            this.tslNombreCompleto,
            this.toolStripSeparator2,
            this.tslEmpresa});
            this.toolStrip1.Location = new System.Drawing.Point(0, 568);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(924, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslTerminal
            // 
            this.tslTerminal.Name = "tslTerminal";
            this.tslTerminal.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslNombreCompleto
            // 
            this.tslNombreCompleto.Name = "tslNombreCompleto";
            this.tslNombreCompleto.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslEmpresa
            // 
            this.tslEmpresa.Name = "tslEmpresa";
            this.tslEmpresa.Size = new System.Drawing.Size(86, 22);
            this.tslEmpresa.Text = "toolStripLabel1";
            // 
            // ntfSeguimientoCotizacion
            // 
            this.ntfSeguimientoCotizacion.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.ntfSeguimientoCotizacion.BalloonTipTitle = "Seguimiento de cotizaciones vencidas y/o por vencer";
            this.ntfSeguimientoCotizacion.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfSeguimientoCotizacion.Icon")));
            this.ntfSeguimientoCotizacion.Text = "Seguimiento de cotizaciones vencidas y/o por vencer";
            this.ntfSeguimientoCotizacion.Visible = true;
            this.ntfSeguimientoCotizacion.BalloonTipClicked += new System.EventHandler(this.ntfSeguimientoCotizacion_BalloonTipClicked);
            this.ntfSeguimientoCotizacion.Click += new System.EventHandler(this.ntfSeguimientoCotizacion_Click);
            // 
            // tmrTemporizador
            // 
            this.tmrTemporizador.Interval = 300000;
            this.tmrTemporizador.Tick += new System.EventHandler(this.tmrTemporizador_Tick);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 593);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mnsOpciones);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnsOpciones;
            this.Name = "frmPrincipal";
            this.Text = "Cosolem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tslNombreCompleto;
        private System.Windows.Forms.ToolStripLabel tslTerminal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.MenuStrip mnsOpciones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tslEmpresa;
        private System.Windows.Forms.NotifyIcon ntfSeguimientoCotizacion;
        private System.Windows.Forms.Timer tmrTemporizador;
    }
}

