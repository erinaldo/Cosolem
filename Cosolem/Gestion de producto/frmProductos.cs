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
    public partial class frmProductos : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Marca
        {
            public long idMarca { get; set; }
            public string descripcion { get; set; }
        }

        class Linea
        {
            public long idLinea { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<Grupo> tbGrupo { get; set; }
        }

        class Grupo
        {
            public long idGrupo { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<SubGrupo> tbSubGrupo { get; set; }
        }

        class SubGrupo
        {
            public long idSubGrupo { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<Modelo> tbModelo { get; set; }
        }

        class Modelo
        {
            public long idModelo { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<tbProducto> tbProducto { get; set; }
        }

        BindingList<tbProducto> _BindingListtbProducto = null;

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

        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvProductos.AutoGenerateColumns = false;

            _BindingListtbProducto = new BindingList<tbProducto>();

            List<Marca> _tbMarca = (from M in _dbCosolemEntities.tbMarca where M.estadoRegistro orderby M.descripcion select new Marca { idMarca = M.idMarca, descripcion = M.descripcion }).ToList();
            _tbMarca.Insert(0, new Marca { idMarca = 0, descripcion = "Seleccione" });
            cmbMarca.DataSource = _tbMarca;
            cmbMarca.ValueMember = "idMarca";
            cmbMarca.DisplayMember = "descripcion";

            List<Linea> _tbLinea = (from L in _dbCosolemEntities.tbLinea where L.estadoRegistro orderby L.descripcion select new Linea { idLinea = L.idLinea, descripcion = L.descripcion, tbGrupo = (from G in L.tbGrupo where G.estadoRegistro select new Grupo { idGrupo = G.idGrupo, descripcion = G.descripcion, tbSubGrupo = (from SG in G.tbSubGrupo where SG.estadoRegistro select new SubGrupo { idSubGrupo = SG.idSubGrupo, descripcion = SG.descripcion, tbModelo = (from M in SG.tbModelo where M.estadoRegistro select new Modelo { idModelo = M.idModelo, descripcion = M.descripcion, tbProducto = M.tbProducto }) }) }) }).ToList();
            _tbLinea.Insert(0, new Linea { idLinea = 0, descripcion = "Seleccione", tbGrupo = new List<Grupo> { new Grupo { idGrupo = 0, descripcion = "Seleccione", tbSubGrupo = new List<SubGrupo> { new SubGrupo { idSubGrupo = 0, descripcion = "Seleccione", tbModelo = new List<Modelo> { new Modelo { idModelo = 0, descripcion = "Seleccione", tbProducto = new List<tbProducto> { } } } } } } } });
            cmbLinea.DataSource = _tbLinea;
            cmbLinea.ValueMember = "idLinea";
            cmbLinea.DisplayMember = "descripcion";
            cmbLinea_SelectionChangeCommitted(null, null);

            dgvProductos.DataSource = _BindingListtbProducto;
        }

        private string VerificaProducto(List<tbProducto> _tbProducto)
        {
            string mensaje = String.Empty;
            List<string> listcodigoProducto = _tbProducto.Where(x => x.idProducto == 0).Select(x => x.codigoProducto).ToList();
            if ((from P in _dbCosolemEntities.tbProducto where listcodigoProducto.Contains(P.codigoProducto) && P.estadoRegistro select P).Count() > 0)
                mensaje += "Código de producto se encuentra registrado, favor verificar\n";
            return mensaje;
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvProductos_CellEndEdit(null, null);

                string mensaje = String.Empty;
                Marca _tbMarca = (Marca)cmbMarca.SelectedItem;
                if (_tbMarca.idMarca == 0) mensaje += "Seleccione marca\n";
                if (((Linea)cmbLinea.SelectedItem).idLinea == 0) mensaje += "Seleccione línea\n";
                if (((Grupo)cmbGrupo.SelectedItem).idGrupo == 0) mensaje += "Seleccione grupo\n";
                if (((SubGrupo)cmbSubGrupo.SelectedItem).idSubGrupo == 0) mensaje += "Seleccione subgrupo\n";
                if (((Modelo)cmbModelo.SelectedItem).idModelo == 0) mensaje += "Seleccione modelo\n";
                if (_BindingListtbProducto.Count == 0) mensaje += "Ingrese al menos 1 producto\n";
                mensaje += VerificaProducto(_BindingListtbProducto.ToList());

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    _BindingListtbProducto.ToList().ForEach(x =>
                    {
                        x.idMarca = _tbMarca.idMarca;
                        if (x.EntityState == EntityState.Detached)
                        {
                            x.fechaHoraIngreso = Program.fechaHora;
                            x.idUsuarioIngreso = idUsuario;
                            x.terminalIngreso = Program.terminal;
                            _dbCosolemEntities.tbProducto.AddObject(x);
                        }
                    });
                    _dbCosolemEntities.SaveChanges();
                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmProductos_Load(null, null);
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
            frmProductos_Load(null, null);
        }

        private void btnAgregarProductos_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (((Marca)cmbMarca.SelectedItem).idMarca == 0) mensaje += "Seleccione marca\n";
            if (((Linea)cmbLinea.SelectedItem).idLinea == 0) mensaje += "Seleccione línea\n";
            if (((Grupo)cmbGrupo.SelectedItem).idGrupo == 0) mensaje += "Seleccione grupo\n";
            if (((SubGrupo)cmbSubGrupo.SelectedItem).idSubGrupo == 0) mensaje += "Seleccione subgrupo\n";
            if (((Modelo)cmbModelo.SelectedItem).idModelo == 0) mensaje += "Seleccione modelo\n";

            if (String.IsNullOrEmpty(mensaje))
            {
                tbProducto _tbProducto = new tbProducto { idMarca = ((Marca)cmbMarca.SelectedItem).idMarca, idModelo = ((Modelo)cmbModelo.SelectedItem).idModelo };
                _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbProducto, EntityState.Detached);
                frmProducto _frmProducto = new frmProducto(_tbProducto);
                _frmProducto.Text = "Producto";
                if (_frmProducto.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _tbProducto = _frmProducto._tbProducto;
                    if (_tbProducto.idProducto == 0 && _tbProducto.estadoRegistro) _BindingListtbProducto.Add(_tbProducto);
                    InactivarRegistros(ref dgvProductos);
                }
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CargarProductos()
        {
            Marca _tbMarca = (Marca)cmbMarca.SelectedItem;
            _BindingListtbProducto.Clear();
            ((Modelo)cmbModelo.SelectedItem).tbProducto.Where(x => x.estadoRegistro).OrderBy(x => x.descripcion).ToList().ForEach(x =>
            {
                x.seleccionado = false;
                if (x.idMarca == _tbMarca.idMarca)
                {
                    x.descripcionTipoProducto = x.tbTipoProducto.descripcion;
                    _BindingListtbProducto.Add(x);
                }
            });
        }

        private void cmbMarca_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void cmbLinea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Linea _tbLinea = (Linea)cmbLinea.SelectedItem;
            List<Grupo> _tbGrupo = _tbLinea.tbGrupo.OrderBy(x => x.descripcion).ToList();
            if (_tbLinea.idLinea != 0) _tbGrupo.Insert(0, new Grupo { idGrupo = 0, descripcion = "Seleccione", tbSubGrupo = new List<SubGrupo> { new SubGrupo { idSubGrupo = 0, descripcion = "Seleccione", tbModelo = new List<Modelo> { new Modelo { idModelo = 0, descripcion = "Seleccione", tbProducto = new List<tbProducto> { } } } } } });
            cmbGrupo.DataSource = _tbGrupo;
            cmbGrupo.ValueMember = "idGrupo";
            cmbGrupo.DisplayMember = "descripcion";
            cmbGrupo_SelectionChangeCommitted(null, null);
        }

        private void cmbGrupo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Grupo _tbGrupo = (Grupo)cmbGrupo.SelectedItem;
            List<SubGrupo> _tbSubGrupo = _tbGrupo.tbSubGrupo.OrderBy(x => x.descripcion).ToList();
            if (_tbGrupo.idGrupo != 0) _tbSubGrupo.Insert(0, new SubGrupo { idSubGrupo = 0, descripcion = "Seleccione", tbModelo = new List<Modelo> { new Modelo { idModelo = 0, descripcion = "Seleccione", tbProducto = new List<tbProducto> { } } } });
            cmbSubGrupo.DataSource = _tbSubGrupo;
            cmbSubGrupo.ValueMember = "idSubGrupo";
            cmbSubGrupo.DisplayMember = "descripcion";
            cmbSubGrupo_SelectionChangeCommitted(null, null);
        }

        private void cmbSubGrupo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SubGrupo _tbSubGrupo = (SubGrupo)cmbSubGrupo.SelectedItem;
            List<Modelo> _tbModelo = _tbSubGrupo.tbModelo.OrderBy(x => x.descripcion).ToList();
            if (_tbSubGrupo.idSubGrupo != 0) _tbModelo.Insert(0, new Modelo { idModelo = 0, descripcion = "Seleccione", tbProducto = new List<tbProducto> { } });
            cmbModelo.DataSource = _tbModelo;
            cmbModelo.ValueMember = "idModelo";
            cmbModelo.DisplayMember = "descripcion";
            cmbModelo_SelectionChangeCommitted(null, null);
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                tbProducto _tbProducto = (tbProducto)dgvProductos.CurrentRow.DataBoundItem;
                frmProducto _frmProducto = new frmProducto(_tbProducto);
                _frmProducto.Text = "Producto";
                if (_frmProducto.ShowDialog() == System.Windows.Forms.DialogResult.OK) _tbProducto = _frmProducto._tbProducto;
                if (_tbProducto.idProducto == 0 && !_tbProducto.estadoRegistro) _BindingListtbProducto.Remove(_tbProducto);
                InactivarRegistros(ref dgvProductos);
            }
        }

        private void SetearProducto(tbProducto _tbProducto)
        {
            try
            {
                cmbMarca.SelectedValue = _tbProducto.idMarca;
                cmbLinea.SelectedValue = _tbProducto.tbModelo.tbSubGrupo.tbGrupo.idLinea;
                cmbLinea_SelectionChangeCommitted(null, null);
                cmbGrupo.SelectedValue = _tbProducto.tbModelo.tbSubGrupo.idGrupo;
                cmbGrupo_SelectionChangeCommitted(null, null);
                cmbSubGrupo.SelectedValue = _tbProducto.tbModelo.tbSubGrupo.idSubGrupo;
                cmbSubGrupo_SelectionChangeCommitted(null, null);
                cmbModelo.SelectedValue = _tbProducto.tbModelo.idModelo;
                cmbModelo_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Marca"), new DataColumn("Línea"), new DataColumn("Grupo"), new DataColumn("SubGrupo"), new DataColumn("Modelo"), new DataColumn("Código de producto"), new DataColumn("Descripción"), new DataColumn("Características"), new DataColumn("Fecha de registro"), new DataColumn("producto", typeof(object)) });

            (from P in _dbCosolemEntities.tbProducto
             where P.estadoRegistro
             select new
             {
                 marca = P.tbMarca.descripcion,
                 linea = P.tbModelo.tbSubGrupo.tbGrupo.tbLinea.descripcion,
                 grupo = P.tbModelo.tbSubGrupo.tbGrupo.descripcion,
                 subgrupo = P.tbModelo.tbSubGrupo.descripcion,
                 modelo = P.tbModelo.descripcion,
                 codigoProducto = P.codigoProducto,
                 descripcion = P.descripcion,
                 caracteristicas = P.caracteristicas,
                 fechaRegistro = P.fechaHoraIngreso,
                 producto = P
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.marca, x.linea, x.grupo, x.subgrupo, x.modelo, x.codigoProducto, x.descripcion, x.caracteristicas, x.fechaRegistro, x.producto));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearProducto((tbProducto)_frmBusqueda._object);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvProductos_CellEndEdit(null, null);
                List<tbProducto> _tbProducto = _BindingListtbProducto.Where(x => x.idProducto != 0 && x.seleccionado).ToList();
                if (_tbProducto.Count > 0)
                {
                    _tbProducto.ForEach(x =>
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
                    frmProductos_Load(null, null);
                }
                else
                    MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void dgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvProductos.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void cmbModelo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarProductos();
        }
    }
}