namespace Cosolem
{
    partial class frmReporteCotizacion
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
            this.rvwCotizacion = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvwCotizacion
            // 
            this.rvwCotizacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvwCotizacion.Location = new System.Drawing.Point(0, 0);
            this.rvwCotizacion.Name = "rvwCotizacion";
            this.rvwCotizacion.Size = new System.Drawing.Size(797, 518);
            this.rvwCotizacion.TabIndex = 0;
            this.rvwCotizacion.ReportExport += new Microsoft.Reporting.WinForms.ExportEventHandler(this.rvwCotizacion_ReportExport);
            this.rvwCotizacion.PrintingBegin += new Microsoft.Reporting.WinForms.ReportPrintEventHandler(this.rvwCotizacion_PrintingBegin);
            // 
            // frmReporteCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 518);
            this.Controls.Add(this.rvwCotizacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmReporteCotizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impresión de cotización";
            this.Load += new System.EventHandler(this.frmReporteCotizacion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvwCotizacion;
    }
}