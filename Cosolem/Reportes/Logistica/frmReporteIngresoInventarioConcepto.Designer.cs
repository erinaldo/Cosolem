namespace Cosolem
{
    partial class frmReporteIngresoInventarioConcepto
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
            this.rvwIngresoInventario = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvwIngresoInventario
            // 
            this.rvwIngresoInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvwIngresoInventario.Location = new System.Drawing.Point(0, 0);
            this.rvwIngresoInventario.Name = "rvwIngresoInventario";
            this.rvwIngresoInventario.Size = new System.Drawing.Size(745, 507);
            this.rvwIngresoInventario.TabIndex = 0;
            // 
            // frmReporteIngresoInventarioConcepto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 507);
            this.Controls.Add(this.rvwIngresoInventario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmReporteIngresoInventarioConcepto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso de inventario";
            this.Load += new System.EventHandler(this.frmReporteIngresoInventarioConcepto_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvwIngresoInventario;
    }
}