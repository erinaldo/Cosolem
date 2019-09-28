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
    public partial class frmTienda : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Empresa
        {
            public long idEmpresa { get; set; }
            public string razonSocial { get; set; }
        }

        tbTienda _tbTienda = null;

        public frmTienda()
        {
            InitializeComponent();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            Empresa _tbEmpresa = (Empresa)cmbEmpresa.SelectedItem;

            string mensaje = String.Empty;

            if (_tbEmpresa.idEmpresa == 0) mensaje += "Seleccione empresa\n";
            if (String.IsNullOrEmpty(txtDescripcion.Text.Trim())) mensaje += "Ingrese descripción\n";
            if (String.IsNullOrEmpty(txtDireccion.Text.Trim())) mensaje += "Ingrese dirección\n";
            if (String.IsNullOrEmpty(txtTelefono.Text.Trim())) mensaje += "Ingrese teléfono\n";
            if (String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) mensaje += "Ingrese correo electrónico\n";
            if (!String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) if (!Util.ValidaEmail(txtCorreoElectronico.Text.Trim())) mensaje += "Correo electrónico inválido, favor verificar\n";
            if (String.IsNullOrEmpty(txtPorcentajeIVA.Text.Trim())) mensaje += "Ingrese porcentaje IVA\n";

            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                _tbTienda.idEmpresa = _tbEmpresa.idEmpresa;
                _tbTienda.descripcion = txtDescripcion.Text.Trim();
                _tbTienda.idCanton = ((dynamic)cmbCanton.SelectedItem).idCanton;
                _tbTienda.direccion = txtDireccion.Text.Trim();
                _tbTienda.telefono = txtTelefono.Text.Trim();
                _tbTienda.correoElectronico = txtCorreoElectronico.Text.Trim();
                _tbTienda.porcentajeIVA = Convert.ToDecimal(txtPorcentajeIVA.Text.Trim());
                if (_tbTienda.EntityState == EntityState.Detached)
                {
                    _tbTienda.fechaHoraIngreso = Program.fechaHora;
                    _tbTienda.idUsuarioIngreso = idUsuario;
                    _tbTienda.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbTienda.AddObject(_tbTienda);
                }
                else
                {
                    _tbTienda.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbTienda.idUsuarioUltimaModificacion = idUsuario;
                    _tbTienda.terminalUltimaModificacion = Program.terminal;
                }
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmTienda_Load(null, null);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmTienda_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbTienda.idTienda == 0) MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbTienda.estadoRegistro = false;
                _tbTienda.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbTienda.idUsuarioUltimaModificacion = idUsuario;
                _tbTienda.terminalUltimaModificacion = Program.terminal;
                _tbTienda.fechaHoraEliminacion = Program.fechaHora;
                _tbTienda.idUsuarioEliminacion = idUsuario;
                _tbTienda.terminalEliminacion = Program.terminal;

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmTienda_Load(null, null);
            }
        }

        private void SetearTienda(tbTienda _tbTienda)
        {
            try
            {
                this._tbTienda = _tbTienda;
                cmbEmpresa.SelectedValue = this._tbTienda.idEmpresa;
                txtCodigo.Text = this._tbTienda.idTienda.ToString();
                txtDescripcion.Text = this._tbTienda.descripcion;
                cmbProvincia.SelectedValue = this._tbTienda.tbCanton.idProvincia;
                cmbProvincia_SelectionChangeCommitted(null, null);
                cmbCanton.SelectedValue = this._tbTienda.idCanton;
                txtDireccion.Text = this._tbTienda.direccion;
                txtTelefono.Text = this._tbTienda.telefono;
                txtCorreoElectronico.Text = this._tbTienda.correoElectronico;
                txtPorcentajeIVA.Text = this._tbTienda.porcentajeIVA.ToString("n2");
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Provincia"), new DataColumn("Cantón"), new DataColumn("Dirección"), new DataColumn("Teléfono"), new DataColumn("Correo electrónico"), new DataColumn("Porcentaje IVA"), new DataColumn("Fecha de registro"), new DataColumn("tienda", typeof(object)) });

            (from T in _dbCosolemEntities.tbTienda
             where T.estadoRegistro
             select new
             {
                 empresa = T.tbEmpresa.razonSocial,
                 idTienda = T.idTienda,
                 descripcion = T.descripcion,
                 provincia = T.tbCanton.tbProvincia.descripcion,
                 canton = T.tbCanton.descripcion,
                 direccion = T.direccion,
                 telefono = T.telefono,
                 correoElectronico = T.correoElectronico,
                 porcentajeIVA = T.porcentajeIVA,
                 fechaRegistro = T.fechaHoraIngreso,
                 tienda = T
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.empresa, x.idTienda, x.descripcion, x.provincia, x.canton, x.direccion, x.telefono, x.correoElectronico, x.porcentajeIVA, x.fechaRegistro, x.tienda));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearTienda((tbTienda)_frmBusqueda._object);
        }

        private void frmTienda_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            _tbTienda = new tbTienda { estadoRegistro = true };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbTienda, EntityState.Detached);

            List<Empresa> _tbEmpresa = (from E in _dbCosolemEntities.tbEmpresa select new Empresa { idEmpresa = E.idEmpresa, razonSocial = E.ruc + " - " + E.razonSocial }).ToList();
            _tbEmpresa.Insert(0, new Empresa { idEmpresa = 0, razonSocial = "Seleccione" });
            cmbEmpresa.DataSource = _tbEmpresa;
            cmbEmpresa.ValueMember = "idEmpresa";
            cmbEmpresa.DisplayMember = "razonSocial";

            var _tbProvincia = (from P in _dbCosolemEntities.tbProvincia select new { idProvincia = P.idProvincia, descripcion = P.descripcion, tbCanton = (from C in P.tbCanton select new { idCanton = C.idCanton, descripcion = C.descripcion }) }).ToList();
            cmbProvincia.DataSource = _tbProvincia;
            cmbProvincia.ValueMember = "idProvincia";
            cmbProvincia.DisplayMember = "descripcion";
            if (_tbTienda.tbCanton != null) cmbProvincia.SelectedValue = _tbTienda.tbCanton.idProvincia;

            var _tbCanton = ((dynamic)cmbProvincia.SelectedItem).tbCanton;
            cmbCanton.DataSource = _tbCanton;
            cmbCanton.ValueMember = "idCanton";
            cmbCanton.DisplayMember = "descripcion";
            if (_tbTienda.idCanton > 0) cmbProvincia.SelectedValue = _tbTienda.idCanton;

            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtCorreoElectronico.Clear();
            txtPorcentajeIVA.Text = Convert.ToDecimal(0).ToString("n2");

            txtDescripcion.Select();
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void cmbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var _tbCanton = ((dynamic)cmbProvincia.SelectedItem).tbCanton;

            cmbCanton.DataSource = _tbCanton;
            cmbCanton.ValueMember = "idCanton";
            cmbCanton.DisplayMember = "descripcion";
        }

        private void txtPorcentajeIVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Program.decimalPoint))
                e.Handled = true;
            if ((e.KeyChar == Program.decimalPoint) && ((sender as TextBox).Text.IndexOf(Program.decimalPoint) > -1))
                e.Handled = true;
        }
    }
}