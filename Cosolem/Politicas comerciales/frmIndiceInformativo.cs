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
    public partial class frmIndiceInformativo : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;
        DateTimePicker dtpFechaHasta = new DateTimePicker();

        class Empresa
        {
            public long? idEmpresa { get; set; }
            public string razonSocial { get; set; }
        }

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

        public frmIndiceInformativo()
        {
            InitializeComponent();

            dgvProductos.Controls.Add(dtpFechaHasta);
            dtpFechaHasta.Visible = false;
            dtpFechaHasta.Format = DateTimePickerFormat.Custom;
            dtpFechaHasta.CustomFormat = "dd/MM/yyyy";
            dtpFechaHasta.TextChanged += new EventHandler(dtpFechaHasta_TextChanged);
        }

        private void frmIndicesInformativo_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvProductos.AutoGenerateColumns = false;

            _BindingListtbProducto = new BindingList<tbProducto>();

            List<Empresa> _tbEmpresa = (from E in _dbCosolemEntities.tbEmpresa select new Empresa { idEmpresa = E.idEmpresa, razonSocial = E.ruc + " - " + E.razonSocial }).ToList();
            _tbEmpresa.Insert(0, new Empresa { idEmpresa = 0, razonSocial = "Seleccione" });
            cmbEmpresa.DataSource = _tbEmpresa;
            cmbEmpresa.ValueMember = "idEmpresa";
            cmbEmpresa.DisplayMember = "razonSocial";

            List<Marca> _tbMarca = (from M in _dbCosolemEntities.tbMarca where M.estadoRegistro select new Marca { idMarca = M.idMarca, descripcion = M.descripcion }).ToList();
            _tbMarca.Insert(0, new Marca { idMarca = 0, descripcion = "Seleccione" });
            cmbMarca.DataSource = _tbMarca;
            cmbMarca.ValueMember = "idMarca";
            cmbMarca.DisplayMember = "descripcion";

            List<Linea> _tbLinea = (from L in _dbCosolemEntities.tbLinea where L.estadoRegistro select new Linea { idLinea = L.idLinea, descripcion = L.descripcion, tbGrupo = (from G in L.tbGrupo where G.estadoRegistro select new Grupo { idGrupo = G.idGrupo, descripcion = G.descripcion, tbSubGrupo = (from SG in G.tbSubGrupo where SG.estadoRegistro select new SubGrupo { idSubGrupo = SG.idSubGrupo, descripcion = SG.descripcion, tbModelo = (from M in SG.tbModelo select new Modelo { idModelo = M.idModelo, descripcion = M.descripcion, tbProducto = M.tbProducto }) }) }) }).ToList();
            _tbLinea.Insert(0, new Linea { idLinea = 0, descripcion = "Seleccione", tbGrupo = new List<Grupo> { new Grupo { idGrupo = 0, descripcion = "Seleccione", tbSubGrupo = new List<SubGrupo> { new SubGrupo { idSubGrupo = 0, descripcion = "Seleccione", tbModelo = new List<Modelo> { new Modelo { idModelo = 0, descripcion = "Seleccione", tbProducto = new List<tbProducto> { } } } } } } } });
            cmbLinea.DataSource = _tbLinea;
            cmbLinea.ValueMember = "idLinea";
            cmbLinea.DisplayMember = "descripcion";
            cmbLinea_SelectionChangeCommitted(null, null);

            dgvProductos.DataSource = _BindingListtbProducto;
        }

        private void cmbLinea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Linea _tbLinea = (Linea)cmbLinea.SelectedItem;
            List<Grupo> _tbGrupo = _tbLinea.tbGrupo.ToList();
            if (_tbLinea.idLinea != 0) _tbGrupo.Insert(0, new Grupo { idGrupo = 0, descripcion = "Seleccione", tbSubGrupo = new List<SubGrupo> { new SubGrupo { idSubGrupo = 0, descripcion = "Seleccione", tbModelo = new List<Modelo> { new Modelo { idModelo = 0, descripcion = "Seleccione", tbProducto = new List<tbProducto> { } } } } } });
            cmbGrupo.DataSource = _tbGrupo;
            cmbGrupo.ValueMember = "idGrupo";
            cmbGrupo.DisplayMember = "descripcion";
            cmbGrupo_SelectionChangeCommitted(null, null);
        }

        private void cmbGrupo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Grupo _tbGrupo = (Grupo)cmbGrupo.SelectedItem;
            List<SubGrupo> _tbSubGrupo = _tbGrupo.tbSubGrupo.ToList();
            if (_tbGrupo.idGrupo != 0) _tbSubGrupo.Insert(0, new SubGrupo { idSubGrupo = 0, descripcion = "Seleccione", tbModelo = new List<Modelo> { new Modelo { idModelo = 0, descripcion = "Seleccione", tbProducto = new List<tbProducto> { } } } });
            cmbSubGrupo.DataSource = _tbSubGrupo;
            cmbSubGrupo.ValueMember = "idSubGrupo";
            cmbSubGrupo.DisplayMember = "descripcion";
            cmbSubGrupo_SelectionChangeCommitted(null, null);
        }

        private void CargarProductos()
        {
            long idEmpresa = ((Empresa)cmbEmpresa.SelectedItem).idEmpresa.Value;
            dtpFechaHasta_CloseUp(null, null);
            _BindingListtbProducto.Clear();
            ((Modelo)cmbModelo.SelectedItem).tbProducto.Where(x => x.estadoRegistro).ToList().ForEach(x =>
            {
                x.seleccionado = false;
                x.producto = x.idProducto.ToString() + " - " + x.descripcion;
                x.idIndiceInformativo = 0;
                x.indiceInformativo = 0;
                x.idCosto = 0;
                x.costo = 0;
                x.fechaHasta = new DateTime(2020, 12, 31);
                x.idProcesarPrecio = 0;
                tbIndiceComercial _tbIndiceComercial = edmCosolemFunctions.getIndiceComercial(idEmpresa, x.idProducto);
                if (_tbIndiceComercial != null)
                {
                    x.idIndiceComercial = _tbIndiceComercial.idIndiceComercial;
                    x.indiceComercial = _tbIndiceComercial.indiceComercial;
                }
                tbIndiceFinanciero _tbIndiceFinanciero = edmCosolemFunctions.getIndiceFinanciero(idEmpresa, x.idProducto);
                if (_tbIndiceFinanciero != null)
                {
                    x.idIndiceFinanciero = _tbIndiceFinanciero.idIndiceFinanciero;
                    x.indiceFinanciero = _tbIndiceFinanciero.indiceFinanciero;
                }
                tbIndiceInformativo _tbIndiceInformativo = edmCosolemFunctions.getIndiceInformativo(idEmpresa, x.idProducto);
                if (_tbIndiceInformativo != null)
                {
                    x.idIndiceInformativo = _tbIndiceInformativo.idIndiceInformativo;
                    x.indiceInformativo = _tbIndiceInformativo.indiceInformativo;
                    x.fechaHasta = _tbIndiceInformativo.fechaHasta;
                }
                tbCosto _tbCosto = edmCosolemFunctions.getCosto(idEmpresa, x.idProducto);
                if (_tbCosto != null)
                {
                    x.idCosto = _tbCosto.idCosto;
                    x.costo = _tbCosto.costo;
                }
                tbProcesarPrecio _tbProcesarPrecio = edmCosolemFunctions.getProcesarPrecio(idEmpresa, x.idProducto);
                if (_tbProcesarPrecio != null) x.idProcesarPrecio = _tbProcesarPrecio.idProcesarPrecio;
                _BindingListtbProducto.Add(x);
            });
        }

        private void cmbSubGrupo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SubGrupo _tbSubGrupo = (SubGrupo)cmbSubGrupo.SelectedItem;
            List<Modelo> _tbModelo = _tbSubGrupo.tbModelo.ToList();
            if (_tbSubGrupo.idSubGrupo != 0) _tbModelo.Insert(0, new Modelo { idModelo = 0, descripcion = "Seleccione", tbProducto = new List<tbProducto> { } });
            cmbModelo.DataSource = _tbModelo;
            cmbModelo.ValueMember = "idModelo";
            cmbModelo.DisplayMember = "descripcion";
            cmbModelo_SelectionChangeCommitted(null, null);
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == txtFechaHasta.Index)
                {
                    dtpFechaHasta.Value = ((tbProducto)dgvProductos.CurrentRow.DataBoundItem).fechaHasta.Date;
                    Rectangle oRectangle = dgvProductos.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dtpFechaHasta.Size = new Size(oRectangle.Width, oRectangle.Height);
                    dtpFechaHasta.Location = new Point(oRectangle.X, oRectangle.Y);
                    dtpFechaHasta.CloseUp += new EventHandler(dtpFechaHasta_CloseUp);
                    dtpFechaHasta.Visible = true;
                }
            }
        }

        private void dtpFechaHasta_TextChanged(object sender, EventArgs e)
        {
            ((tbProducto)dgvProductos.CurrentRow.DataBoundItem).fechaHasta = dtpFechaHasta.Value.Date;
            dgvProductos_CellEndEdit(null, null);
        }

        private void dtpFechaHasta_CloseUp(object sender, EventArgs e)
        {
            dtpFechaHasta.Visible = false;
        }

        private void dgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dtpFechaHasta_CloseUp(null, null);
            dgvProductos.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvProductos_CellEndEdit(null, null);

                long idEmpresa = ((Empresa)cmbEmpresa.SelectedItem).idEmpresa.Value;

                string mensaje = String.Empty;
                if (idEmpresa == 0) mensaje += "Seleccione empresa\n";
                if (((Marca)cmbMarca.SelectedItem).idMarca == 0) mensaje += "Seleccione marca\n";
                if (((Linea)cmbLinea.SelectedItem).idLinea == 0) mensaje += "Seleccione línea\n";
                if (((Grupo)cmbGrupo.SelectedItem).idGrupo == 0) mensaje += "Seleccione grupo\n";
                if (((SubGrupo)cmbSubGrupo.SelectedItem).idSubGrupo == 0) mensaje += "Seleccione subgrupo\n";
                if (((Modelo)cmbModelo.SelectedItem).idModelo == 0) mensaje += "Seleccione modelo\n";
                if (_BindingListtbProducto.Count == 0) mensaje += "Ingrese al menos 1 producto\n";
                if (_BindingListtbProducto.Where(x => x.fechaHasta.Date < Program.fechaHora.Date).Any()) mensaje += "Indice informativo con fecha de vigencia menor a la fecha actual\n";

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    _BindingListtbProducto.ToList().ForEach(x =>
                    {
                        tbIndiceInformativo _tbIndiceInformativo = (from II in _dbCosolemEntities.tbIndiceInformativo where II.idIndiceInformativo == x.idIndiceInformativo select II).FirstOrDefault();
                        if (_tbIndiceInformativo == null)
                        {
                            _tbIndiceInformativo = new tbIndiceInformativo();
                            _tbIndiceInformativo.idEmpresa = idEmpresa;
                            _tbIndiceInformativo.idProducto = x.idProducto;
                            _tbIndiceInformativo.indiceInformativo = x.indiceInformativo;
                            _tbIndiceInformativo.fechaDesde = Program.fechaHora.Date;
                            _tbIndiceInformativo.fechaHasta = x.fechaHasta.Date;
                            _tbIndiceInformativo.estadoRegistro = true;
                            _tbIndiceInformativo.fechaHoraIngreso = Program.fechaHora;
                            _tbIndiceInformativo.idUsuarioIngreso = idUsuario;
                            _tbIndiceInformativo.terminalIngreso = Program.terminal;
                            _dbCosolemEntities.tbIndiceInformativo.AddObject(_tbIndiceInformativo);
                        }
                        else
                        {
                            _tbIndiceInformativo.indiceInformativo = x.indiceInformativo;
                            _tbIndiceInformativo.fechaHoraUltimaModificacion = Program.fechaHora;
                            _tbIndiceInformativo.idUsuarioUltimaModificacion = idUsuario;
                            _tbIndiceInformativo.terminalUltimaModificacion = Program.terminal;
                        }
                        if (x.indiceComercial > 0 && x.indiceFinanciero > 0 && x.indiceInformativo > 0 && x.costo > 0)
                        {
                            tbProcesarPrecio _tbProcesarPrecio = (from PP in _dbCosolemEntities.tbProcesarPrecio where PP.idProcesarPrecio == x.idProcesarPrecio select PP).FirstOrDefault();
                            if (_tbProcesarPrecio == null)
                            {
                                _tbProcesarPrecio = new tbProcesarPrecio();
                                _tbProcesarPrecio.idEmpresa = idEmpresa;
                                _tbProcesarPrecio.idProducto = x.idProducto;
                                _tbProcesarPrecio.procesarPrecio = true;
                                _tbProcesarPrecio.estadoRegistro = true;
                                _tbProcesarPrecio.fechaHoraIngreso = Program.fechaHora;
                                _tbProcesarPrecio.idUsuarioIngreso = idUsuario;
                                _tbProcesarPrecio.terminalIngreso = Program.terminal;
                                _dbCosolemEntities.tbProcesarPrecio.AddObject(_tbProcesarPrecio);
                            }
                            else
                            {
                                _tbProcesarPrecio.fechaHoraUltimaModificacion = Program.fechaHora;
                                _tbProcesarPrecio.idUsuarioUltimaModificacion = idUsuario;
                                _tbProcesarPrecio.terminalUltimaModificacion = Program.terminal;
                            }
                        }
                    });
                    _dbCosolemEntities.SaveChanges();

                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmIndicesInformativo_Load(null, null);
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void cmbEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void dgvProductos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl _DataGridViewTextBoxEditingControl = (DataGridViewTextBoxEditingControl)e.Control;
            _DataGridViewTextBoxEditingControl.KeyPress += new KeyPressEventHandler(txtCosto_KeyPress);
            e.Control.KeyPress += new KeyPressEventHandler(txtCosto_KeyPress);
        }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Program.decimalPoint))
                e.Handled = true;
            if ((e.KeyChar == Program.decimalPoint) && ((sender as TextBox).Text.IndexOf(Program.decimalPoint) > -1))
                e.Handled = true;
        }

        private void dgvProductos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is FormatException) return;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmIndicesInformativo_Load(null, null);
        }

        private void cmbModelo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarProductos();
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
    }
}
