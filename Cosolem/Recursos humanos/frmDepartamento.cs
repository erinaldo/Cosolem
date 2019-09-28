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
    public partial class frmDepartamento : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Empresa
        {
            public long idEmpresa { get; set; }
            public string razonSocial { get; set; }
        }

        tbDepartamento _tbDepartamento = null;
        BindingList<tbCargo> _BindingListtbCargo = null;

        private void InactivarRegistros(ref DataGridView _DataGridView)
        {
            CurrencyManager _CurrencyManager = (CurrencyManager)BindingContext[_DataGridView.DataSource];
            _CurrencyManager.SuspendBinding();
            foreach (DataGridViewRow _DataGridViewRow in _DataGridView.Rows)
            {
                dynamic _dynamic = (dynamic)_DataGridViewRow.DataBoundItem;
                _DataGridViewRow.Visible = _dynamic.estadoRegistro;
            }
            _CurrencyManager.ResumeBinding();
        }

        private void SetearDepartamento(tbDepartamento _tbDepartamento)
        {
            try
            {
                this._tbDepartamento = _tbDepartamento;
                cmbEmpresa.SelectedValue = this._tbDepartamento.idEmpresa;
                txtDescripcion.Text = this._tbDepartamento.descripcion;
                _BindingListtbCargo.Clear();
                this._tbDepartamento.tbCargo.ToList().ForEach(x => { if (x.estadoRegistro) _BindingListtbCargo.Add(x); });
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private string VerificaCargo()
        {
            string mensaje = String.Empty;
            if (dgvCargos.RowCount == 0)
                mensaje += "Ingrese al menos 1 cargo\n";
            else
            {
                List<tbCargo> _listtbCargo = (from C in dgvCargos.Rows.Cast<DataGridViewRow>() select (tbCargo)C.DataBoundItem).ToList();
                if (_listtbCargo.Where(x => x.estadoRegistro && String.IsNullOrEmpty(x.descripcion ?? String.Empty)).Count() > 0)
                    mensaje += "La descripción del cargo es necesario, favor verificar\n";
            }
            return mensaje;
        }

        public frmDepartamento()
        {
            InitializeComponent();
        }

        private void frmDepartamento_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            _tbDepartamento = new tbDepartamento { estadoRegistro = true };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbDepartamento, EntityState.Detached);

            List<Empresa> _tbEmpresa = (from E in _dbCosolemEntities.tbEmpresa select new Empresa { idEmpresa = E.idEmpresa, razonSocial = E.ruc + " - " + E.razonSocial }).ToList();
            _tbEmpresa.Insert(0, new Empresa { idEmpresa = 0, razonSocial = "Seleccione" });
            cmbEmpresa.DataSource = _tbEmpresa;
            cmbEmpresa.ValueMember = "idEmpresa";
            cmbEmpresa.DisplayMember = "razonSocial";

            txtDescripcion.Clear();

            dgvCargos.AutoGenerateColumns = false;
            _BindingListtbCargo = new BindingList<tbCargo>(_tbDepartamento.tbCargo.ToList());
            dgvCargos.DataSource = _BindingListtbCargo;

            txtDescripcion.Select();
        }

        private void btnAgregarCargos_Click(object sender, EventArgs e)
        {
            _BindingListtbCargo.Add(new tbCargo { estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });
            InactivarRegistros(ref dgvCargos);
        }

        private void dgvCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == imgEliminarCargo.Index)
                {
                    if (MessageBox.Show("¿Seguro desea eliminar el registro seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        tbCargo _tbCargo = (tbCargo)dgvCargos.CurrentRow.DataBoundItem;
                        _tbCargo.estadoRegistro = false;
                        _tbCargo.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbCargo.idUsuarioUltimaModificacion = idUsuario;
                        _tbCargo.terminalUltimaModificacion = Program.terminal;
                        _tbCargo.fechaHoraEliminacion = Program.fechaHora;
                        _tbCargo.idUsuarioEliminacion = idUsuario;
                        _tbCargo.terminalEliminacion = Program.terminal;
                        if (_tbCargo.idCargo == 0) _BindingListtbCargo.Remove(_tbCargo);
                        InactivarRegistros(ref dgvCargos);
                    }
                }
            }
        }

        private void dgvCargos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvCargos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            foreach (DataGridViewRow _DataGridViewRow in dgvCargos.Rows)
            {
                tbCargo _tbCargo = (tbCargo)_DataGridViewRow.DataBoundItem;
                if (_tbCargo.EntityState == EntityState.Modified)
                {
                    _tbCargo.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbCargo.idUsuarioUltimaModificacion = idUsuario;
                    _tbCargo.terminalUltimaModificacion = Program.terminal;
                }
            }
            InactivarRegistros(ref dgvCargos);
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCargos_CellEndEdit(null, null);

                string mensaje = String.Empty;
                if (((Empresa)cmbEmpresa.SelectedItem).idEmpresa == 0) mensaje += "Seleccione empresa\n";
                if (String.IsNullOrEmpty(txtDescripcion.Text.Trim())) mensaje += "Ingrese descripción\n";
                mensaje += VerificaCargo();

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    _tbDepartamento.idEmpresa = ((Empresa)cmbEmpresa.SelectedItem).idEmpresa;
                    _tbDepartamento.descripcion = txtDescripcion.Text.Trim();
                    _BindingListtbCargo.ToList().ForEach(x => { if (x.idCargo == 0) _tbDepartamento.tbCargo.Add(x); });
                    if (_tbDepartamento.EntityState == EntityState.Detached)
                    {
                        _tbDepartamento.fechaHoraIngreso = Program.fechaHora;
                        _tbDepartamento.idUsuarioIngreso = idUsuario;
                        _tbDepartamento.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbDepartamento.AddObject(_tbDepartamento);
                    }
                    else
                    {
                        _tbDepartamento.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbDepartamento.idUsuarioUltimaModificacion = idUsuario;
                        _tbDepartamento.terminalUltimaModificacion = Program.terminal;
                    }
                    _dbCosolemEntities.SaveChanges();

                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmDepartamento_Load(null, null);
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmDepartamento_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbDepartamento.idDepartamento == 0)
                MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbDepartamento.estadoRegistro = false;
                _tbDepartamento.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbDepartamento.idUsuarioUltimaModificacion = idUsuario;
                _tbDepartamento.terminalUltimaModificacion = Program.terminal;
                _tbDepartamento.fechaHoraEliminacion = Program.fechaHora;
                _tbDepartamento.idUsuarioEliminacion = idUsuario;
                _tbDepartamento.terminalEliminacion = Program.terminal;
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmDepartamento_Load(null, null);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Departamento"), new DataColumn("Tiene cargos"), new DataColumn("Fecha de registro"), new DataColumn("departamentos", typeof(object)) });

            (from D in _dbCosolemEntities.tbDepartamento
             from C in _dbCosolemEntities.tbCargo.Where(x => x.idDepartamento == D.idDepartamento && x.estadoRegistro == D.estadoRegistro).DefaultIfEmpty()
             where D.estadoRegistro
             select new
             {
                 empresa = D.tbEmpresa.razonSocial,
                 departamento = D.descripcion,
                 tieneCargos = (C == null ? "No" : "Sí"),
                 fechaRegistro = D.fechaHoraIngreso,
                 departamentos = D
             }).Distinct().ToList().ForEach(x => _DataTable.Rows.Add(x.empresa, x.departamento, x.tieneCargos, x.fechaRegistro, x.departamentos));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearDepartamento((tbDepartamento)_frmBusqueda._object);
        }
    }
}
