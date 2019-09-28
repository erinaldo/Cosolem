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
    public partial class frmProveedores : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class TipoPersona
        {
            public int idTipoPersona { get; set; }
            public string descripcion { get; set; }
        }

        class TipoIdentificacion
        {
            public int idTipoIdentificacion { get; set; }
            public string descripcion { get; set; }
            public int cantidadCaracteres { get; set; }
        }

        tbProveedor _tbProveedor = null;

        public frmProveedores()
        {
            InitializeComponent();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;

            if (String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) mensaje += "Ingrese número de identificación\n";
            if (String.IsNullOrEmpty(txtNombres.Text.Trim())) mensaje += "Ingrese nombres\n";
            if (String.IsNullOrEmpty(txtDireccion.Text.Trim())) mensaje += "Ingrese dirección\n";
            if (String.IsNullOrEmpty(txtConvencional.Text.Trim())) mensaje += "Ingrese convencional\n";
            if (String.IsNullOrEmpty(txtCelular.Text.Trim())) mensaje += "Ingrese celular\n";
            if (String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) mensaje += "Ingrese correo electrónico\n";
            if (!String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) if (!Util.ValidaEmail(txtCorreoElectronico.Text.Trim())) mensaje += "Correo electrónico inválido, favor verificar\n";

            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                _tbProveedor.idTipoPersona = ((TipoPersona)cmbTipoPersona.SelectedItem).idTipoPersona;
                _tbProveedor.idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                _tbProveedor.numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
                _tbProveedor.nombres = txtNombres.Text.Trim();
                _tbProveedor.direccion = txtDireccion.Text.Trim();
                _tbProveedor.convencional = txtConvencional.Text.Trim();
                _tbProveedor.celular = txtCelular.Text.Trim();
                _tbProveedor.correoElectronico = txtCorreoElectronico.Text.Trim();
                if (_tbProveedor.EntityState == EntityState.Detached)
                {
                    _tbProveedor.fechaHoraIngreso = Program.fechaHora;
                    _tbProveedor.idUsuarioIngreso = idUsuario;
                    _tbProveedor.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbProveedor.AddObject(_tbProveedor);
                }
                else
                {
                    _tbProveedor.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbProveedor.idUsuarioUltimaModificacion = idUsuario;
                    _tbProveedor.terminalUltimaModificacion = Program.terminal;
                }
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmProveedores_Load(null, null);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmProveedores_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbProveedor.idProveedor == 0) MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbProveedor.estadoRegistro = false;
                _tbProveedor.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbProveedor.idUsuarioUltimaModificacion = idUsuario;
                _tbProveedor.terminalUltimaModificacion = Program.terminal;
                _tbProveedor.fechaHoraEliminacion = Program.fechaHora;
                _tbProveedor.idUsuarioEliminacion = idUsuario;
                _tbProveedor.terminalEliminacion = Program.terminal;

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmProveedores_Load(null, null);
            }
        }

        private void SetearProveedor(tbProveedor _tbProveedor)
        {
            try
            {
                this._tbProveedor = _tbProveedor;
                cmbTipoPersona.SelectedValue = this._tbProveedor.idTipoPersona;
                cmbTipoPersona_SelectionChangeCommitted(null, null);
                cmbTipoIdentificacion.SelectedValue = this._tbProveedor.idTipoIdentificacion;
                txtNumeroIdentificacion.Text = this._tbProveedor.numeroIdentificacion;
                txtNombres.Text = this._tbProveedor.nombres;
                txtDireccion.Text = this._tbProveedor.direccion;
                txtConvencional.Text = this._tbProveedor.convencional;
                txtCelular.Text = this._tbProveedor.celular;
                txtCorreoElectronico.Text = this._tbProveedor.correoElectronico;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Código"), new DataColumn("Tipo de persona"), new DataColumn("Tipo de identificación"), new DataColumn("Número de identificación"), new DataColumn("Nombres"), new DataColumn("Dirección"), new DataColumn("Convencional"), new DataColumn("Celular"), new DataColumn("Correo electrónico"), new DataColumn("Fecha de registro"), new DataColumn("proveedor", typeof(object)) });

            (from P in _dbCosolemEntities.tbProveedor
             where P.estadoRegistro
             select new
             {
                 idProveedor = P.idProveedor,
                 tipoPersona = P.tbTipoPersona.descripcion,
                 tipoIdentificacion = P.tbTipoIdentificacion.descripcion,
                 numeroIdentificacion = P.numeroIdentificacion,
                 nombres = P.nombres,
                 direccion = P.direccion,
                 convencional = P.convencional,
                 celular = P.celular,
                 correoElectronico = P.correoElectronico,
                 fechaRegistro = P.fechaHoraIngreso,
                 proveedor = P
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.idProveedor, x.tipoPersona, x.tipoIdentificacion, x.numeroIdentificacion, x.nombres, x.direccion, x.convencional, x.celular, x.correoElectronico, x.fechaRegistro, x.proveedor));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearProveedor((tbProveedor)_frmBusqueda._object);
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            _tbProveedor = new tbProveedor { estadoRegistro = true };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbProveedor, EntityState.Detached);

            cmbTipoPersona.DataSource = (from TP in _dbCosolemEntities.tbTipoPersona select new TipoPersona { idTipoPersona = TP.idTipoPersona, descripcion = TP.descripcion }).ToList();
            cmbTipoPersona.ValueMember = "idTipoPersona";
            cmbTipoPersona.DisplayMember = "descripcion";
            cmbTipoPersona_SelectionChangeCommitted(null, null);

            txtNumeroIdentificacion.Clear();
            txtNombres.Clear();
            txtDireccion.Clear();
            txtConvencional.Clear();
            txtCorreoElectronico.Clear();

            txtNumeroIdentificacion.Select();
        }

        private void cmbTipoPersona_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TipoPersona _tbTipoPersona = (TipoPersona)cmbTipoPersona.SelectedItem;

            List<TipoIdentificacion> _tbTipoIdentificacion = (from TI in _dbCosolemEntities.tbTipoIdentificacion select new TipoIdentificacion { idTipoIdentificacion = TI.idTipoIdentificacion, descripcion = TI.descripcion, cantidadCaracteres = TI.cantidadCaracteres }).ToList();
            if (_tbTipoPersona.idTipoPersona == 2) _tbTipoIdentificacion = _tbTipoIdentificacion.Where(x => x.idTipoIdentificacion == 2).ToList();
            cmbTipoIdentificacion.DataSource = _tbTipoIdentificacion;
            cmbTipoIdentificacion.ValueMember = "idTipoIdentificacion";
            cmbTipoIdentificacion.DisplayMember = "descripcion";

            cmbTipoIdentificacion_SelectionChangeCommitted(null, null);
        }

        private void cmbTipoIdentificacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtNumeroIdentificacion.MaxLength = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).cantidadCaracteres;
            txtNumeroIdentificacion.Clear();
            txtNumeroIdentificacion.Select();
        }

        private void txtNumeroIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
