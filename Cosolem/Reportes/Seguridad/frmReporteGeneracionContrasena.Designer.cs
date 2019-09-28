namespace Cosolem
{
    partial class frmReporteGeneracionContrasena
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
            this.rvwGeneracionContrasena = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvwGeneracionContrasena
            // 
            this.rvwGeneracionContrasena.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvwGeneracionContrasena.Location = new System.Drawing.Point(0, 0);
            this.rvwGeneracionContrasena.Name = "rvwGeneracionContrasena";
            this.rvwGeneracionContrasena.Size = new System.Drawing.Size(654, 822);
            this.rvwGeneracionContrasena.TabIndex = 0;
            // 
            // frmReporteGeneracionContrasena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 822);
            this.Controls.Add(this.rvwGeneracionContrasena);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmReporteGeneracionContrasena";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generación de contraseña";
            this.Load += new System.EventHandler(this.frmReporteGeneracionContrasena_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvwGeneracionContrasena;
    }
}