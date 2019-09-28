using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Cosolem
{
    public partial class frmOrdenCompra : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;

        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idEmpleado = Program.tbUsuario.tbEmpleado.idEmpleado;
        long idUsuario = Program.tbUsuario.idUsuario;
        tbOrdenCompraCabecera ordenCompra = null;
        BindingList<tbOrdenCompraDetalle> ordenCompraDetalle = null;

        private void CalcularTotales()
        {
            ordenCompraDetalle.ToList().ForEach(x => x.total = x.costo * x.cantidad);

            txtSubtotal.Text = Util.FormatoNumero(ordenCompraDetalle.Sum(x => x.total), 2);

            decimal subtotal = Decimal.Parse(txtSubtotal.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
            decimal descuento = Decimal.Parse(txtDescuento.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
            decimal transporte = Decimal.Parse(txtTransporte.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
            decimal impuesto = Decimal.Parse(txtImpuesto.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);

            txtTotal.Text = Util.FormatoNumero(subtotal - descuento + transporte + impuesto, 2);
        }

        private void SetearOrdenCompra(tbOrdenPedidoCabecera ordenPedido)
        {
            try
            {
                ordenCompra.idEmpresa = ordenPedido.idEmpresa;
                ordenCompra.idOrdenPedidoCabecera = ordenPedido.idOrdenPedidoCabecera;
                ordenCompra.idEmpleado = ordenPedido.idEmpleado;

                ordenCompraDetalle.Clear();
                ordenPedido.tbOrdenPedidoDetalle.Where(x => x.estadoRegistro).ToList().ForEach(y =>
                {
                    long idProducto = y.idProducto;
                    string descripcionProducto = y.tbProducto.codigoProducto + " - " + y.tbProducto.descripcion;
                    decimal costo = 0;
                    long cantidad = y.cantidad;
                    bool estadoRegistro = y.estadoRegistro;

                    ordenCompraDetalle.Add(new tbOrdenCompraDetalle { idProducto = idProducto, descripcionProducto = descripcionProducto, costo = costo, cantidad = cantidad, total = costo * cantidad, estadoRegistro = estadoRegistro });
                });
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        public frmOrdenCompra()
        {
            InitializeComponent();
        }

        private void frmOrdenCompra_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvOrdenCompraDetalle.AutoGenerateColumns = false;

            ordenCompra = new tbOrdenCompraCabecera { idOrdenPedidoCabecera = 0, estadoRegistro = true, tbOrdenCompraDetalle = new System.Data.Objects.DataClasses.EntityCollection<tbOrdenCompraDetalle> { } };
            ordenCompraDetalle = new BindingList<tbOrdenCompraDetalle>(ordenCompra.tbOrdenCompraDetalle.ToList());
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(ordenCompra, EntityState.Detached);

            List<tbProveedor> proveedores = (from P in _dbCosolemEntities.tbProveedor where P.estadoRegistro select P).ToList();
            proveedores.Insert(0, new tbProveedor { idProveedor = 0, nombres = "Seleccione" });
            cmbProveedor.DataSource = proveedores;
            cmbProveedor.ValueMember = "idProveedor";
            cmbProveedor.DisplayMember = "nombres";

            dgvOrdenCompraDetalle.DataSource = ordenCompraDetalle;

            txtSubtotal.Text = Util.FormatoNumero(0, 2);
            txtDescuento.Text = Util.FormatoNumero(0, 2);
            txtTransporte.Text = Util.FormatoNumero(0, 2);
            txtImpuesto.Text = Util.FormatoNumero(0, 2);
            txtTotal.Text = Util.FormatoNumero(0, 2);

            CalcularTotales();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            dgvOrdenCompraDetalle_CellEndEdit(null, null);

            tbProveedor proveedor = (tbProveedor)cmbProveedor.SelectedItem;

            string mensaje = String.Empty;

            if (ordenCompra.idOrdenPedidoCabecera == 0) mensaje += "Seleccione una orden de pedido para poder grabar orden de compra\n";
            if (proveedor.idProveedor == 0) mensaje += "Seleccione un proveedor\n";
            if (dtpFechaRequisicion.Value.Date < Program.fechaHora.Date) mensaje += "Fecha de requisición tiene que se mayor o igual al día de hoy\n";
            if (ordenCompraDetalle.Count == 0) mensaje += "La orden de pedido al menos debe tener 1 producto para poder grabar orden de compra\n";
            if (ordenCompraDetalle.Where(x => x.costo == 0 || x.cantidad == 0 || x.total == 0).Count() == 0) mensaje += "Favor revisar costo, cantidad o total en cero\n";

            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                ordenCompra.idProveedor = proveedor.idProveedor;
                ordenCompra.fechaRequisicion = dtpFechaRequisicion.Value.Date;
                ordenCompra.fechaHoraIngreso = Program.fechaHora;
                ordenCompra.idUsuarioIngreso = idUsuario;
                ordenCompra.terminalIngreso = Program.terminal;
                ordenCompraDetalle.Where(x => x.idOrdenCompraDetalle == 0).ToList().ForEach(y =>
                {
                    y.fechaHoraIngreso = Program.fechaHora;
                    y.idUsuarioIngreso = idUsuario;
                    y.terminalIngreso = Program.terminal;
                    ordenCompra.tbOrdenCompraDetalle.Add(y);
                });
                _dbCosolemEntities.tbOrdenCompraCabecera.AddObject(ordenCompra);

                _dbCosolemEntities.SaveChanges();
                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmOrdenCompra_Load(null, null);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Solicita"), new DataColumn("Motivo"), new DataColumn("Cantidad de productos"), new DataColumn("Cantidad total de productos solicitados"), new DataColumn("Fecha de registro"), new DataColumn("ordenPedido", typeof(object)) });

            (from OPC in _dbCosolemEntities.tbOrdenPedidoCabecera
             where OPC.idEmpresa == idEmpresa && OPC.estadoRegistro
             select new
             {
                 empresa = OPC.tbEmpresa.razonSocial,
                 solicita = OPC.tbEmpleado.tbPersona.nombreCompleto,
                 motivo = OPC.motivo,
                 cantidadProductos = OPC.tbOrdenPedidoDetalle.Where(x => x.estadoRegistro).Count(),
                 cantidadTotalProductosSolicitados = OPC.tbOrdenPedidoDetalle.Where(x => x.estadoRegistro).Sum(y => y.cantidad),
                 fechaRegistro = OPC.fechaHoraIngreso,
                 ordenPedido = OPC
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.empresa, x.solicita, x.motivo, x.cantidadProductos, x.cantidadTotalProductosSolicitados, x.fechaRegistro, x.ordenPedido));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearOrdenCompra((tbOrdenPedidoCabecera)_frmBusqueda._object);
        }

        private void dgvOrdenCompraDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrdenCompraDetalle.CommitEdit(DataGridViewDataErrorContexts.Commit);
            CalcularTotales();
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Program.decimalPoint))
                e.Handled = true;
            if ((e.KeyChar == Program.decimalPoint) && ((sender as TextBox).Text.IndexOf(Program.decimalPoint) > -1))
                e.Handled = true;
        }

        private void dgvOrdenCompraDetalle_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                TextBox txtValor = (TextBox)e.Control;
                txtValor.KeyPress -= txtValor_KeyPress;
                txtValor.KeyPress += txtValor_KeyPress;
            }
        }

        private void txtValor_Leave(object sender, EventArgs e)
        {
            TextBox txtValor = (TextBox)sender;
            decimal valor = 0;
            if (!String.IsNullOrEmpty(txtValor.Text.Trim()) && txtValor.Text.Trim() != Program.decimalPoint.ToString()) valor = Decimal.Parse(txtValor.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
            txtValor.Text = Util.FormatoNumero(valor, 2);
            CalcularTotales();
        }

        private void dgvOrdenCompraDetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is FormatException) return;
        }
    }
}
