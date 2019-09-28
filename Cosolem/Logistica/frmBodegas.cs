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
    public partial class frmBodegas : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Empresa
        {
            public long? idEmpresa { get; set; }
            public string razonSocial { get; set; }
            public IEnumerable<Tienda> tbTienda { get; set; }
        }

        class Tienda
        {
            public long? idTienda { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<tbBodega> tbBodega { get; set; }
        }

        BindingList<tbBodega> _BindingListtbBodega = null;

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

        public frmBodegas()
        {
            InitializeComponent();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvBodegas_CellEndEdit(null, null);

                string mensaje = String.Empty;
                Tienda _tbTienda = (Tienda)cmbTienda.SelectedItem;
                if (((Empresa)cmbEmpresa.SelectedItem).idEmpresa == 0) mensaje += "Seleccione empresa\n";
                if (_tbTienda.idTienda == 0) mensaje += "Seleccione tienda\n";
                if (_BindingListtbBodega.Count == 0) mensaje += "Ingrese al menos 1 bodega\n";
                if (_BindingListtbBodega.Where(x => String.IsNullOrEmpty(x.descripcion.Trim())).Count() > 0) mensaje += "Ingrese descripción de bodega\n";

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    _BindingListtbBodega.ToList().ForEach(x =>
                    {
                        x.idTienda = _tbTienda.idTienda.Value;
                        if (x.EntityState == EntityState.Detached)
                        {
                            x.fechaHoraIngreso = Program.fechaHora;
                            x.idUsuarioIngreso = idUsuario;
                            x.terminalIngreso = Program.terminal;
                            _dbCosolemEntities.tbBodega.AddObject(x);
                        }
                    });
                    _dbCosolemEntities.SaveChanges();

                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmBodegas_Load(null, null);
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
            frmBodegas_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvBodegas_CellEndEdit(null, null);
                List<tbBodega> _tbBodega = _BindingListtbBodega.Where(x => x.idBodega != 0 && x.seleccionado).ToList();
                if (_tbBodega.Count > 0)
                {
                    _tbBodega.ForEach(x =>
                    {
                        x.estadoRegistro = false;
                        x.fechaHoraUltimaModificacion = Program.fechaHora;
                        x.idUsuarioUltimaModificacion = idUsuario;
                        x.terminalUltimaModificacion = Program.terminal;
                        x.fechaHoraEliminacion = Program.fechaHora;
                        x.idUsuarioEliminacion = idUsuario;
                        x.terminalEliminacion = Program.terminal;
                    });

                    _dbCosolemEntities.SaveChanges();

                    MessageBox.Show("Registros eliminados satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmBodegas_Load(null, null);
                }
                else
                    MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void SetearBodega(tbBodega _tbBodega)
        {
            try
            {
                cmbEmpresa.SelectedValue = _tbBodega.tbTienda.idEmpresa;
                cmbEmpresa_SelectionChangeCommitted(null, null);
                cmbTienda.SelectedValue = _tbBodega.idTienda;
                cmbTienda_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Tienda"), new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Es facturable"), new DataColumn("Fecha de registro"), new DataColumn("bodega", typeof(object)) });

            (from B in _dbCosolemEntities.tbBodega
             where B.estadoRegistro
             select new
             {
                 empresa = B.tbTienda.tbEmpresa.razonSocial,
                 tienda = B.tbTienda.descripcion,
                 codigoBodega = B.idBodega,
                 descripcion = B.descripcion,
                 esFacturable = B.esFacturable ? "Sí" : "No",
                 fechaRegistro = B.fechaHoraIngreso,
                 bodega = B
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.empresa, x.tienda, x.codigoBodega, x.descripcion, x.esFacturable, x.fechaRegistro, x.bodega));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearBodega((tbBodega)_frmBusqueda._object);
        }

        private void frmBodegas_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvBodegas.AutoGenerateColumns = false;

            _BindingListtbBodega = new BindingList<tbBodega>();

            List<Empresa> _tbEmpresa = (from E in _dbCosolemEntities.tbEmpresa select new Empresa { idEmpresa = E.idEmpresa, razonSocial = E.ruc + " - " + E.razonSocial, tbTienda = (from T in E.tbTienda select new Tienda { idTienda = T.idTienda, descripcion = T.descripcion, tbBodega = T.tbBodega }) }).ToList();
            _tbEmpresa.Insert(0, new Empresa { idEmpresa = 0, razonSocial = "Seleccione", tbTienda = new List<Tienda> { new Tienda { idTienda = 0, descripcion = "Seleccione", tbBodega = new List<tbBodega> { } } } });
            cmbEmpresa.DataSource = _tbEmpresa;
            cmbEmpresa.ValueMember = "idEmpresa";
            cmbEmpresa.DisplayMember = "razonSocial";
            cmbEmpresa_SelectionChangeCommitted(null, null);

            dgvBodegas.DataSource = _BindingListtbBodega;
        }

        private void btnAgregarBodegas_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (((Empresa)cmbEmpresa.SelectedItem).idEmpresa == 0) mensaje += "Seleccione empresa\n";
            if (((Tienda)cmbTienda.SelectedItem).idTienda == 0) mensaje += "Seleccione tienda\n";

            if (String.IsNullOrEmpty(mensaje))
            {
                tbBodega _tbBodega = new tbBodega { idBodega = 0, descripcion = String.Empty, esFacturable = true, estadoRegistro = true };
                _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbBodega, EntityState.Detached);
                _BindingListtbBodega.Add(_tbBodega);
                InactivarRegistros(ref dgvBodegas);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void cmbEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Empresa _tbEmpresa = (Empresa)cmbEmpresa.SelectedItem;
            List<Tienda> _tbTienda = _tbEmpresa.tbTienda.ToList();
            if (_tbEmpresa.idEmpresa != 0) _tbTienda.Insert(0, new Tienda { idTienda = 0, descripcion = "Seleccione", tbBodega = new List<tbBodega> { } });
            cmbTienda.DataSource = _tbTienda;
            cmbTienda.ValueMember = "idTienda";
            cmbTienda.DisplayMember = "descripcion";
            cmbTienda_SelectionChangeCommitted(null, null);
        }

        private void dgvBodegas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvBodegas.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void CargarBodegas()
        {
            _BindingListtbBodega.Clear();
            ((Tienda)cmbTienda.SelectedItem).tbBodega.Where(x => x.estadoRegistro).ToList().ForEach(x => _BindingListtbBodega.Add(x));
        }

        private void cmbTienda_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarBodegas();
        }
    }
}