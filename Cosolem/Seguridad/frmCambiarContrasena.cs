using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cosolem
{
    public partial class frmCambiarContrasena : Form
    {
        long idUsuario = Program.tbUsuario.idUsuario;

        public frmCambiarContrasena()
        {
            InitializeComponent();
        }

        private void frmCambiarContrasena_Load(object sender, EventArgs e)
        {
            txtNombreUsuario.Text = Program.tbUsuario.nombreUsuario;
            txtContrasenaActual.Clear();
            txtContrasenaNueva.Clear();
            txtConfirmarContrasena.Clear();

            txtContrasenaActual.Select();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                string contrasena = Program.tbUsuario.contrasena;

                string mensaje = String.Empty;
                if (String.IsNullOrEmpty(txtContrasenaActual.Text.Trim())) mensaje += "Ingrese contraseña actual\n";
                if (String.IsNullOrEmpty(txtContrasenaNueva.Text.Trim())) mensaje += "Ingrese contraseña nueva\n";
                if (String.IsNullOrEmpty(txtConfirmarContrasena.Text.Trim())) mensaje += "Ingrese confirmación de contraseña\n";
                if (!String.IsNullOrEmpty(txtContrasenaActual.Text.Trim())) if (Util.EncriptaValor(txtContrasenaActual.Text.Trim(), idUsuario.ToString()) != contrasena) mensaje += "Contraseña actual incorrecta, favor verificar\n";
                if (!String.IsNullOrEmpty(txtContrasenaActual.Text.Trim()) && !String.IsNullOrEmpty(txtContrasenaNueva.Text.Trim())) if (Util.EncriptaValor(txtContrasenaActual.Text.Trim(), idUsuario.ToString()) == Util.EncriptaValor(txtContrasenaNueva.Text.Trim(), idUsuario.ToString())) mensaje += "Contraseña nueva no puede ser igual a la actual, favor verificar\n";
                if (!String.IsNullOrEmpty(txtContrasenaNueva.Text.Trim()) && !String.IsNullOrEmpty(txtConfirmarContrasena.Text.Trim())) if (txtContrasenaNueva.Text.Trim() != txtConfirmarContrasena.Text.Trim()) mensaje += "Contraseña nueva y confirmación de contraseña no coinciden\n";

                if (String.IsNullOrEmpty(mensaje))
                {
                    using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
                    {
                        tbUsuario usuario = (from U in _dbCosolemEntities.tbUsuario where U.idUsuario == idUsuario select U).FirstOrDefault();
                        usuario.contrasena = Util.EncriptaValor(txtContrasenaNueva.Text.Trim(), idUsuario.ToString());
                        usuario.cambiarContrasena = false;
                        usuario.fechaHoraUltimaModificacion = Program.fechaHora;
                        usuario.idUsuarioUltimaModificacion = idUsuario;
                        usuario.terminalUltimaModificacion = Program.terminal;

                        _dbCosolemEntities.SaveChanges();

                        Program.tbUsuario = usuario;
                    }
                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }
    }
}