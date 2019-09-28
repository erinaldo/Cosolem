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
    public partial class frmGeneracionContrasena : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;
        tbPersona _tbPersona = null;

        class TipoIdentificacion
        {
            public int idTipoIdentificacion { get; set; }
            public string descripcion { get; set; }
            public int cantidadCaracteres { get; set; }
        }

        private void SetearPersona(tbPersona _tbPersona)
        {
            try
            {
                this._tbPersona = _tbPersona;
                if (this._tbPersona.tbEmpleado.tbUsuario == null) _tbPersona.tbEmpleado.tbUsuario = new tbUsuario { estadoRegistro = true };
                else
                {
                    txtCorreoElectronico.Enabled = false;
                    txtNombreUsuario.Enabled = false;
                }
                cmbTipoIdentificacion.SelectedValue = this._tbPersona.idTipoIdentificacion;
                cmbTipoIdentificacion_SelectionChangeCommitted(null, null);
                txtNumeroIdentificacion.Text = this._tbPersona.numeroIdentificacion;
                cmbTipoIdentificacion.Enabled = false;
                txtNumeroIdentificacion.Enabled = false;
                txtNombresCompletos.Text = this._tbPersona.nombreCompleto;
                string correoElectronico = this._tbPersona.tbEmpleado.correoElectronico;
                string nombreUsuario = this._tbPersona.tbEmpleado.tbUsuario == null ? null : this._tbPersona.tbEmpleado.tbUsuario.nombreUsuario;
                if (String.IsNullOrEmpty(nombreUsuario))
                {
                    nombreUsuario = this._tbPersona.primerNombre.Trim().Substring(0, 1) + this._tbPersona.apellidoPaterno;
                    int cantidad = (from U in _dbCosolemEntities.tbUsuario where U.nombreUsuario == nombreUsuario select U).Count();
                    if (cantidad > 0) nombreUsuario += (cantidad + 1).ToString();
                }
                if (String.IsNullOrEmpty(correoElectronico)) correoElectronico = nombreUsuario + "@" + _tbPersona.tbEmpleado.tbEmpresa.dominio;
                txtCorreoElectronico.Text = correoElectronico;
                txtNombreUsuario.Text = nombreUsuario;
                txtContrasena.Text = Util.GeneraContrasena();
                chbContrasenaNuncaExpira.Checked = false;
                chbCambiarContrasena.Checked = true;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        public frmGeneracionContrasena()
        {
            InitializeComponent();
        }

        private void frmGeneracionContrasena_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            _tbPersona = null;

            cmbTipoIdentificacion.DataSource = (from TI in _dbCosolemEntities.tbTipoIdentificacion where TI.idTipoIdentificacion != 2 select new TipoIdentificacion { idTipoIdentificacion = TI.idTipoIdentificacion, descripcion = TI.descripcion, cantidadCaracteres = TI.cantidadCaracteres }).ToList();
            cmbTipoIdentificacion.ValueMember = "idTipoIdentificacion";
            cmbTipoIdentificacion.DisplayMember = "descripcion";
            cmbTipoIdentificacion_SelectionChangeCommitted(null, null);

            cmbTipoIdentificacion.Enabled = true;
            txtNumeroIdentificacion.Clear();
            txtNumeroIdentificacion.Enabled = true;

            txtNombresCompletos.Clear();
            txtCorreoElectronico.Enabled = true;
            txtCorreoElectronico.Clear();
            txtNombreUsuario.Enabled = true;
            txtNombreUsuario.Clear();
            txtContrasena.Clear();
            chbContrasenaNuncaExpira.Checked = false;
            chbCambiarContrasena.Checked = true;

            txtNumeroIdentificacion.Select();
        }

        private void cmbTipoIdentificacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtNumeroIdentificacion.MaxLength = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).cantidadCaracteres;
            txtNumeroIdentificacion.Clear();
            txtNumeroIdentificacion.Select();
        }

        private void txtNumeroIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
                tbPersona _tbPersona = (from P in _dbCosolemEntities.tbPersona join E in _dbCosolemEntities.tbEmpleado on new { idEmpleado = P.idPersona, P.estadoRegistro } equals new { E.idEmpleado, E.estadoRegistro } where P.estadoRegistro && P.idTipoPersona == 1 && P.numeroIdentificacion == numeroIdentificacion select P).FirstOrDefault();
                if (_tbPersona != null) SetearPersona(_tbPersona);
                else MessageBox.Show("Empleado no se encuentra registrado, favor verificar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                if (new List<int> { 1, 2 }.Contains(idTipoIdentificacion))
                {
                    if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                    {
                        e.Handled = true;
                        return;
                    }
                }
                else
                {
                    if (!(char.IsLetter(e.KeyChar)) && !(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmGeneracionContrasena_Load(null, null);
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Tipo de identificación"), new DataColumn("Número de identificación"), new DataColumn("Nombre completo"), new DataColumn("Departamento"), new DataColumn("Cargo"), new DataColumn("Fecha de registro"), new DataColumn("persona", typeof(object)) });

            (from P in _dbCosolemEntities.tbPersona
             join E in _dbCosolemEntities.tbEmpleado on new { idEmpleado = P.idPersona, P.estadoRegistro } equals new { E.idEmpleado, E.estadoRegistro }
             where P.estadoRegistro && P.idTipoPersona == 1
             select new
             {
                 empresa = E.tbEmpresa.razonSocial,
                 tipoIdentificacion = P.tbTipoIdentificacion.descripcion,
                 numeroIdentificacion = P.numeroIdentificacion,
                 nombreCompleto = P.nombreCompleto,
                 departamento = E.tbCargo.tbDepartamento.descripcion,
                 cargo = E.tbCargo.descripcion,
                 fechaRegistro = E.fechaHoraIngreso,
                 persona = P
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.empresa, x.tipoIdentificacion, x.numeroIdentificacion, x.nombreCompleto, x.departamento, x.cargo, x.fechaRegistro, x.persona));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearPersona((tbPersona)_frmBusqueda._object);
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = String.Empty;

                if (_tbPersona == null) mensaje += "Seleccione un empleado para proceder con la generación de contraseña\n";
                if (String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) mensaje += "Ingrese correo electrónico\n";
                if (!String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) if (!Util.ValidaEmail(txtCorreoElectronico.Text.Trim())) mensaje += "Correo electrónico inválido, favor verificar\n";
                if (String.IsNullOrEmpty(txtNombreUsuario.Text.Trim())) mensaje += "Ingrese nombre de usuario\n";
                if (!String.IsNullOrEmpty(txtNombreUsuario.Text.Trim()) && _dbCosolemEntities.tbUsuario.Count(x => x.nombreUsuario == txtNombreUsuario.Text.Trim()) > 1) mensaje += "Nombre de usuario ya existe, favor verificar\n";

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    _tbPersona.tbEmpleado.correoElectronico = txtCorreoElectronico.Text.Trim();
                    _tbPersona.tbEmpleado.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbPersona.tbEmpleado.idUsuarioUltimaModificacion = idUsuario;
                    _tbPersona.tbEmpleado.terminalUltimaModificacion = Program.terminal;
                    _tbPersona.tbEmpleado.tbUsuario.nombreUsuario = txtNombreUsuario.Text.Trim();
                    _tbPersona.tbEmpleado.tbUsuario.contrasena = Util.EncriptaValor(txtContrasena.Text.Trim(), _tbPersona.tbEmpleado.tbUsuario.idUsuario.ToString());
                    _tbPersona.tbEmpleado.tbUsuario.contrasenaNuncaExpira = chbContrasenaNuncaExpira.Checked;
                    _tbPersona.tbEmpleado.tbUsuario.cambiarContrasena = chbCambiarContrasena.Checked;
                    _tbPersona.tbEmpleado.tbUsuario.fechaHoraGeneracionContrasena = Program.fechaHora;
                    _tbPersona.tbEmpleado.tbUsuario.idUsuarioGeneracionContrasena = idUsuario;
                    _tbPersona.tbEmpleado.tbUsuario.terminalGeneracionContrasena = Program.terminal;
                    if (!_dbCosolemEntities.tbUsuario.Any(x => x.idUsuario == _tbPersona.tbEmpleado.tbUsuario.idUsuario))
                    {
                        _tbPersona.tbEmpleado.tbUsuario.fechaHoraIngreso = Program.fechaHora;
                        _tbPersona.tbEmpleado.tbUsuario.idUsuarioIngreso = idUsuario;
                        _tbPersona.tbEmpleado.tbUsuario.terminalIngreso = Program.terminal;
                    }
                    else
                    {
                        _tbPersona.tbEmpleado.tbUsuario.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbPersona.tbEmpleado.tbUsuario.idUsuarioUltimaModificacion = idUsuario;
                        _tbPersona.tbEmpleado.tbUsuario.terminalUltimaModificacion = Program.terminal;
                    }
                    _dbCosolemEntities.SaveChanges();
                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new frmReporteGeneracionContrasena(_tbPersona).ShowDialog();
                    frmGeneracionContrasena_Load(null, null);
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