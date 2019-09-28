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
    public partial class frmIngresoInventario : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Empresa
        {
            public long idEmpresa { get; set; }
            public string razonSocial { get; set; }
            public IEnumerable<Tienda> tbTienda { get; set; }
        }

        class Tienda
        {
            public long idTienda { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<Bodega> tbBodega { get; set; }
        }

        class Bodega
        {
            public long idBodega { get; set; }
            public string descripcion { get; set; }
        }

        class Producto
        {
            public bool seleccionado { get; set; }
            public long idProducto { get; set; }
            public string codigoProducto { get; set; }
            public string descripcion { get; set; }
        }

        List<Producto> listaProducto = null;
        BindingList<tbInventario> _BindingListtbInventario = null;

        public frmIngresoInventario()
        {
            InitializeComponent();
        }

        private void frmIngresoInventario_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvProductos.AutoGenerateColumns = false;
            dgvInventario.AutoGenerateColumns = false;

            _BindingListtbInventario = new BindingList<tbInventario>();

            List<Empresa> _tbEmpresa = (from E in _dbCosolemEntities.tbEmpresa select new Empresa { idEmpresa = E.idEmpresa, razonSocial = E.ruc + " - " + E.razonSocial, tbTienda = (from T in E.tbTienda where T.estadoRegistro select new Tienda { idTienda = T.idTienda, descripcion = T.descripcion, tbBodega = (from B in T.tbBodega where B.estadoRegistro select new Bodega { idBodega = B.idBodega, descripcion = B.descripcion }) }) }).ToList();
            _tbEmpresa.Insert(0, new Empresa { idEmpresa = 0, razonSocial = "Seleccione", tbTienda = new List<Tienda> { new Tienda { idTienda = 0, descripcion = "Seleccione", tbBodega = new List<Bodega> { new Bodega { idBodega = 0, descripcion = "Seleccione" } } } } });
            cmbEmpresa.DataSource = _tbEmpresa;
            cmbEmpresa.ValueMember = "idEmpresa";
            cmbEmpresa.DisplayMember = "razonSocial";
            cmbEmpresa_SelectionChangeCommitted(null, null);

            listaProducto = (from P in _dbCosolemEntities.tbProducto where P.estadoRegistro orderby P.descripcion select new Producto { seleccionado = false, idProducto = P.idProducto, codigoProducto = P.codigoProducto, descripcion = P.descripcion }).ToList();

            txtFiltroBusqueda.Clear();
            dgvProductos.DataSource = listaProducto;
            dgvInventario.DataSource = _BindingListtbInventario;
        }

        private void cmbEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Empresa _tbEmpresa = (Empresa)cmbEmpresa.SelectedItem;
            List<Tienda> _tbTienda = _tbEmpresa.tbTienda.ToList();
            if (_tbEmpresa.idEmpresa != 0) _tbTienda.Insert(0, new Tienda { idTienda = 0, descripcion = "Seleccione", tbBodega = new List<Bodega> { new Bodega { idBodega = 0, descripcion = "Seleccione" } } });
            cmbTienda.DataSource = _tbTienda;
            cmbTienda.ValueMember = "idTienda";
            cmbTienda.DisplayMember = "descripcion";
            cmbTienda_SelectionChangeCommitted(null, null);
        }

        private void cmbTienda_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Tienda _tbTienda = (Tienda)cmbTienda.SelectedItem;
            List<Bodega> _tbBodega = _tbTienda.tbBodega.ToList();
            if (_tbTienda.idTienda != 0) _tbBodega.Insert(0, new Bodega { idBodega = 0, descripcion = "Seleccione" });
            cmbBodega.DataSource = _tbBodega;
            cmbBodega.ValueMember = "idBodega";
            cmbBodega.DisplayMember = "descripcion";
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvInventario_CellEndEdit(null, null);

                string mensaje = String.Empty;

                long idEmpresa = ((Empresa)cmbEmpresa.SelectedItem).idEmpresa;
                long idTienda = ((Tienda)cmbTienda.SelectedItem).idTienda;
                long idBodega = ((Bodega)cmbBodega.SelectedItem).idBodega;

                if (idEmpresa == 0) mensaje += "Seleccione empresa\n";
                if (idTienda == 0) mensaje += "Seleccione tienda\n";
                if (idBodega == 0) mensaje += "Seleccione bodega\n";
                if (idBodega > 0 && _BindingListtbInventario.Where(x => x.idBodega != idBodega).Count() > 0) mensaje += "Favor verifique los parámetros seleccionados son distintos a los agregados en el inventario\n";
                if (_BindingListtbInventario.Where(x => x.cantidad == 0).Count() > 0) mensaje += "Ingrese cantidad\n";
                _BindingListtbInventario.ToList().ForEach(x =>
                {
                    tbCosto _tbCosto = edmCosolemFunctions.getCosto(idEmpresa, x.idProducto);
                    if (_tbCosto == null || _tbCosto.costo == 0) mensaje += "Producto " + x.descripcionProducto + " no tiene costo\n";
                });

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    _BindingListtbInventario.ToList().ForEach(x =>
                    {
                        tbTransaccionInventario _tbTransaccionInventario = new tbTransaccionInventario();
                        _tbTransaccionInventario.tipoTransaccion = "Ingreso de inventario";
                        _tbTransaccionInventario.idBodega = idBodega;
                        _tbTransaccionInventario.idProducto = x.idProducto;
                        _tbTransaccionInventario.cantidad = x.cantidad;
                        _tbTransaccionInventario.estadoRegistro = true;
                        _tbTransaccionInventario.fechaHoraIngreso = Program.fechaHora;
                        _tbTransaccionInventario.idUsuarioIngreso = idUsuario;
                        _tbTransaccionInventario.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbTransaccionInventario.AddObject(_tbTransaccionInventario);

                        tbInventario _tbInventario = (from I in _dbCosolemEntities.tbInventario where I.idBodega == _tbTransaccionInventario.idBodega && I.idProducto == _tbTransaccionInventario.idProducto && I.estadoRegistro select I).FirstOrDefault();
                        if (_tbInventario == null)
                        {
                            _tbInventario = new tbInventario();
                            _tbInventario.idBodega = idBodega;
                            _tbInventario.idProducto = x.idProducto;
                            _tbInventario.fisicoDisponible = x.cantidad;
                            _tbInventario.reservado = 0;
                            _tbInventario.estadoRegistro = true;
                            _tbInventario.fechaHoraIngreso = Program.fechaHora;
                            _tbInventario.idUsuarioIngreso = idUsuario;
                            _tbInventario.terminalIngreso = Program.terminal;
                            _dbCosolemEntities.tbInventario.AddObject(_tbInventario);
                        }
                        else
                        {
                            _tbInventario.fisicoDisponible = _tbInventario.fisicoDisponible + _tbTransaccionInventario.cantidad;
                            _tbInventario.fechaHoraUltimaModificacion = Program.fechaHora;
                            _tbInventario.idUsuarioUltimaModificacion = idUsuario;
                            _tbInventario.terminalUltimaModificacion = Program.terminal;
                        }
                    });
                    _dbCosolemEntities.SaveChanges();

                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new frmReporteIngresoInventario(_BindingListtbInventario.ToList()).ShowDialog();
                    frmIngresoInventario_Load(null, null);
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
            frmIngresoInventario_Load(null, null);
        }

        private void txtFiltroBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.listaProducto.ForEach(x => x.seleccionado = false);
                List<Producto> listaProducto = this.listaProducto;
                if (!String.IsNullOrEmpty(txtFiltroBusqueda.Text.Trim())) listaProducto = listaProducto.Where(x => (x.codigoProducto + x.descripcion).ToUpper().Contains(txtFiltroBusqueda.Text.Trim().ToUpper())).ToList();
                dgvProductos.DataSource = listaProducto;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void btnAgregarParaIngresarInventario_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;

            long idEmpresa = ((Empresa)cmbEmpresa.SelectedItem).idEmpresa;
            long idTienda = ((Tienda)cmbTienda.SelectedItem).idTienda;
            long idBodega = ((Bodega)cmbBodega.SelectedItem).idBodega;

            if (idEmpresa == 0) mensaje += "Seleccione empresa\n";
            if (idTienda == 0) mensaje += "Seleccione tienda\n";
            if (idBodega == 0) mensaje += "Seleccione bodega\n";
            if (idBodega > 0 && _BindingListtbInventario.Where(x => x.idBodega != idBodega).Count() > 0) mensaje += "Favor verifique los parámetros seleccionados son distintos a los agregados en el inventario\n";
            if (this.listaProducto.Where(x => x.seleccionado).Count() == 0) mensaje += "Seleccione al menos 1 producto\n";

            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                List<Producto> listaProducto = this.listaProducto.Where(x => x.seleccionado).ToList();
                listaProducto.ForEach(x =>
                {
                    tbInventario _tbInventario = _BindingListtbInventario.Where(y => y.idBodega == idBodega && y.idProducto == x.idProducto).FirstOrDefault();
                    long fisicoDisponible = edmCosolemFunctions.getFisicoDisponible(idEmpresa, idBodega, x.idProducto);
                    if (_tbInventario == null) _BindingListtbInventario.Add(new tbInventario { idBodega = idBodega, idProducto = x.idProducto, codigoProducto = x.codigoProducto, descripcionProducto = x.descripcion, fisicoDisponible = fisicoDisponible, cantidad = 0 });
                    else _tbInventario.fisicoDisponible = fisicoDisponible;
                });
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvProductos.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvInventario_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvInventario.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvInventario_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl _DataGridViewTextBoxEditingControl = (DataGridViewTextBoxEditingControl)e.Control;
            _DataGridViewTextBoxEditingControl.KeyPress += new KeyPressEventHandler(txtCantidad_KeyPress);
            e.Control.KeyPress += new KeyPressEventHandler(txtCantidad_KeyPress);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void dgvInventario_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is FormatException) return;
        }

        private void dgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == dicEliminar.Index)
                {
                    if (MessageBox.Show("¿Seguro desea eliminar el registro seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        _BindingListtbInventario.Remove((tbInventario)dgvInventario.CurrentRow.DataBoundItem);
                }
            }
        }
    }
}