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
    public partial class frmBusquedaInventario : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;

        class Empresa
        {
            public long idEmpresa { get; set; }
            public string razonSocial { get; set; }
        }

        class Precio
        {
            public string codigoProducto { get; set; }
            public bool seleccionado { get; set; }
            public string descripcionFormaPago { get; set; }
            public decimal precio { get; set; }
        }

        bool habilitarSeleccionar = false;
        long idEmpresa = 0;
        string formaPago = null;
        string tipoOrdenVenta = null;

        public string codigoProducto = null;
        public tbBodega bodega = null;

        public frmBusquedaInventario(string habilitarSeleccionar, string idEmpresa, string formaPago, string tipoOrdenVenta)
        {
            this.habilitarSeleccionar = Convert.ToBoolean(habilitarSeleccionar);
            this.idEmpresa = Convert.ToInt64(idEmpresa);
            this.formaPago = formaPago;
            this.tipoOrdenVenta = tipoOrdenVenta;
            InitializeComponent();
        }

        private void frmBusquedaInventario_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            tsbSeleccionar.Enabled = habilitarSeleccionar;

            dgvPrecios.AutoGenerateColumns = false;
            dgvPrecios.Columns[seleccionado.Index].Visible = habilitarSeleccionar;

            List<Empresa> _tbEmpresa = (from E in _dbCosolemEntities.tbEmpresa select new Empresa { idEmpresa = E.idEmpresa, razonSocial = E.ruc + " - " + E.razonSocial }).ToList();
            _tbEmpresa.Insert(0, new Empresa { idEmpresa = 0, razonSocial = "Seleccione" });
            cmbEmpresa.DataSource = _tbEmpresa;
            cmbEmpresa.ValueMember = "idEmpresa";
            cmbEmpresa.DisplayMember = "razonSocial";
            cmbEmpresa.SelectedValue = idEmpresa;
            cmbEmpresa.Enabled = (idEmpresa == 0 ? true : false);

            txtCodigoProducto.Clear();
            txtDescripcionProducto.Clear();

            dgvPrecios.DataSource = new List<Precio>();

            lvwInventario.Items.Clear();
            lvwInventario.Groups.Clear();
            lvwInventario.CheckBoxes = tipoOrdenVenta == "O" ? habilitarSeleccionar : false;
        }

        private void SetearProducto(tbProducto _tbProducto)
        {
            long idEmpresa = ((Empresa)cmbEmpresa.SelectedItem).idEmpresa;
            long idProducto = 0;
            txtCodigoProducto.Clear();
            txtDescripcionProducto.Clear();
            List<Precio> precios = new List<Precio>();

            if (idEmpresa == 0)
                MessageBox.Show("Seleccione empresa", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (_tbProducto == null)
                MessageBox.Show("Producto no existe", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                tbPrecio _tbPrecio = edmCosolemFunctions.getPrecio(idEmpresa, _tbProducto.idProducto);
                txtCodigoProducto.Text = _tbProducto.codigoProducto;
                txtDescripcionProducto.Text = _tbProducto.descripcion;
                idProducto = _tbProducto.idProducto;
                if (_tbPrecio != null)
                {
                    precios.Add(new Precio { codigoProducto = _tbProducto.codigoProducto, seleccionado = false, descripcionFormaPago = "Efectivo", precio = _tbPrecio.precioOferta });
                    precios.Add(new Precio { codigoProducto = _tbProducto.codigoProducto, seleccionado = false, descripcionFormaPago = "Crédito", precio = _tbPrecio.precioVentaPublico });
                    precios.Add(new Precio { codigoProducto = _tbProducto.codigoProducto, seleccionado = false, descripcionFormaPago = "Informativo", precio = _tbPrecio.precioInformativo });

                    if (!String.IsNullOrEmpty(formaPago)) precios.Where(x => x.descripcionFormaPago == formaPago).FirstOrDefault().seleccionado = true;
                }
            }
            dgvPrecios.DataSource = precios;

            lvwInventario.Items.Clear();
            lvwInventario.Groups.Clear();

            List<clsInventarioGeneral> inventarioGeneral = edmCosolemFunctions.getInventarioGeneral(idEmpresa, idProducto);
            foreach (var tienda in inventarioGeneral.Select(x => new { idTienda = x.idTienda, descripcionTienda = x.descripcionTienda }).Distinct().ToList())
            {
                ListViewGroup grupo = new ListViewGroup(tienda.descripcionTienda);
                lvwInventario.Groups.Add(grupo);
                foreach (var bodega in inventarioGeneral.Where(x => x.idTienda == tienda.idTienda).Select(y => new { idBodega = y.idBodega, descripcionBodega = y.descripcionBodega, fisicoDisponible = y.fisicoDisponible, reservado = y.reservado, inventario = y.inventario }).ToList())
                {
                    ListViewItem subGrupo = new ListViewItem { Tag = bodega.idBodega, Text = bodega.descripcionBodega, Group = grupo };
                    subGrupo.SubItems.Add(bodega.fisicoDisponible.ToString());
                    subGrupo.SubItems.Add(bodega.reservado.ToString());
                    subGrupo.SubItems.Add(bodega.inventario.ToString());
                    lvwInventario.Items.Add(subGrupo);
                }
            }
        }

        private void tsbBuscarProducto_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Marca"), new DataColumn("Línea"), new DataColumn("Grupo"), new DataColumn("SubGrupo"), new DataColumn("Modelo"), new DataColumn("Código de producto"), new DataColumn("Descripción"), new DataColumn("Características"), new DataColumn("Fecha de registro"), new DataColumn("producto", typeof(object)) });
            var Productos = edmCosolemFunctions.getProductos();
            foreach (var Producto in Productos) _DataTable.Rows.Add(Producto.marca, Producto.linea, Producto.grupo, Producto.subgrupo, Producto.modelo, Producto.codigoProducto, Producto.descripcion, Producto.caracteristicas, Producto.fechaRegistro, Producto.producto);
            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearProducto((tbProducto)_frmBusqueda._object);
        }

        private void txtCodigoProducto_Leave(object sender, EventArgs e)
        {
            txtDescripcionProducto.Clear();
            dgvPrecios.DataSource = new List<Precio>();
            lvwInventario.Items.Clear();
            lvwInventario.Groups.Clear();
            if (!String.IsNullOrEmpty(txtCodigoProducto.Text.Trim()))
            {
                var _tbProducto = edmCosolemFunctions.getProductos(txtCodigoProducto.Text.Trim());
                tbProducto producto = _tbProducto.Count == 0 ? null : _tbProducto[0].producto;
                SetearProducto(producto);
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmBusquedaInventario_Load(null, null);
        }

        private void tsbSeleccionar_Click(object sender, EventArgs e)
        {
            Precio precioSeleccionado = ((List<Precio>)dgvPrecios.DataSource).Where(x => x.seleccionado).FirstOrDefault();
            if (precioSeleccionado == null)
                MessageBox.Show("Para poder seleccionar tiene que realizar la búsqueda de un producto", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (precioSeleccionado.precio == 0)
                MessageBox.Show("Producto no tiene precio", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (tipoOrdenVenta == "O" && lvwInventario.CheckedItems.Count == 0)
                MessageBox.Show("Seleccione una bodega de la cual se va a tomar el inventario", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if(tipoOrdenVenta == "O" && lvwInventario.CheckedItems.Count > 1)
                MessageBox.Show("Solo se puede seleccionar una bodega de la cual se va a tomar el inventario", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                long idBodega = (tipoOrdenVenta == "O" ? Convert.ToInt64(lvwInventario.CheckedItems[0].Tag) : 0);
                codigoProducto = precioSeleccionado.codigoProducto;
                bodega = (from B in _dbCosolemEntities.tbBodega where B.idBodega == idBodega select B).FirstOrDefault();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
