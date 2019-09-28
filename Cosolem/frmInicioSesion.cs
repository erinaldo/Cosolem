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
    public partial class frmInicioSesion : Form
    {
        long idUsuario = 0;

        public frmInicioSesion()
        {
            InitializeComponent();
        }

        private void txtNombreUsuario_Leave(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string nombreUsuario = txtNombreUsuario.Text.Trim();
                using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
                {
                    tbUsuario usuario = _dbCosolemEntities.tbUsuario.Where(x => x.nombreUsuario == nombreUsuario).FirstOrDefault();
                    byte[] imagen = null;
                    pbxFoto.Image = null;
                    idUsuario = 0;
                    if (usuario != null)
                    {
                        idUsuario = usuario.idUsuario;
                        imagen = usuario.tbEmpleado.foto;
                        if (imagen != null) pbxFoto.Image = Util.ObtenerImagen(imagen);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Program.tbUsuario = null;

            string mensaje = String.Empty;

            if (String.IsNullOrEmpty(txtNombreUsuario.Text.Trim())) mensaje += "Ingrese nombre de usuario\n";
            if (String.IsNullOrEmpty(txtContrasena.Text.Trim())) mensaje += "Ingrese contraseña\n";
            if (idUsuario == 0) mensaje += "Usuario no existe\n";

            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                string nombreUsuario = txtNombreUsuario.Text.Trim();
                string contrasena = Util.EncriptaValor(txtContrasena.Text.Trim(), idUsuario.ToString());
                dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities();
                Program.tbUsuario = _dbCosolemEntities.tbUsuario.Include("tbEmpleado.tbPersona").Include("tbEmpleado.tbEmpresa").Include("tbEmpleado.tbTienda").Include("tbUsuarioOpcion.tbOpcion.tbModulo").Where(x => x.nombreUsuario == nombreUsuario && x.contrasena == contrasena).FirstOrDefault();

                if (Program.tbUsuario != null)
                {
                    if (Program.tbUsuario.estadoRegistro)
                    {
                        if (!Program.tbUsuario.fechaHoraPrimerAcceso.HasValue && Program.tbUsuario.terminalPrimerAcceso == null)
                        {
                            Program.tbUsuario.fechaHoraPrimerAcceso = Program.fechaHora;
                            Program.tbUsuario.terminalPrimerAcceso = Program.terminal;
                        }
                        _dbCosolemEntities.SaveChanges();
                        _dbCosolemEntities.Dispose();
                        this.Close();
                        new frmPrincipal().Show();
                    }
                    else
                        MessageBox.Show("Usuario inactivo favor indicar al administrador del sistema", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Usuario y/o contraseña incorrectos, favor verificar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void frmInicioSesion_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Program.tbUsuario == null) Application.Exit();
        }

        private void txtIniciarSesion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) btnIniciarSesion_Click(null, null);
        }
    }
}
