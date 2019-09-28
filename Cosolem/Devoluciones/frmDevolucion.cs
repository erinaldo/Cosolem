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
    public partial class frmDevolucion : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;
        tbOrdenVentaCabecera ordenVenta = null;

        public frmDevolucion()
        {
            InitializeComponent();
        }

        private void frmDevolucion_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            tsbNuevo_Click(null, null);
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            frmBusquedaFactura busquedaFactura = new frmBusquedaFactura("false", "true");
            busquedaFactura.Text = "Búsqueda de facturas a 8 días atrás";
            if (busquedaFactura.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ordenVenta = busquedaFactura.ordenVenta;

                txtNumeroIdentificacion.Text = ordenVenta.numeroIdentificacion;
                txtNumeroFactura.Text = ordenVenta.numeroFactura.ToString();
                txtFechaFactura.Text = ordenVenta.fechaHoraFactura.Value.ToString("dd/MM/yyyy - HH:mm:ss");
                txtCliente.Text = ordenVenta.cliente;
                txtDireccion.Text = ordenVenta.direccion;
                txtConvencional.Text = ordenVenta.convencional;
                txtCelular.Text = ordenVenta.celular;

                ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro).ToList().ForEach(y =>
                {
                    y.codigoProducto = y.tbProducto.codigoProducto;
                    y.descripcionProducto = y.tbProducto.codigoProducto + " - " + y.tbProducto.descripcion;
                    string descripcionBodegaSalida = string.Empty;
                    if (y.tbProducto.tbTipoProducto.esInventariable) descripcionBodegaSalida = y.idBodega + " - " + y.tbBodega.descripcion;
                    y.descripcionBodegaSalida = descripcionBodegaSalida;
                    y.idBodegaEntrada = 0;
                    y.descripcionBodegaEntrada = string.Empty;
                    y.precio = (ordenVenta.idFormaPago == 1 ? (ordenVenta.tipoVenta == "N" ? y.precioOferta : y.costo) : y.precioVentaPublico);
                });

                dgvVenta.DataSource = ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro).ToList();

                txtSubtotal.Text = Util.FormatoMoneda(ordenVenta.subTotalBruto, 2);
                txtIVA.Text = Util.FormatoMoneda(ordenVenta.IVA, 2);
                txtTotalNeto.Text = Util.FormatoMoneda(ordenVenta.totalNeto, 2);
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            ordenVenta = null;

            dgvVenta.AutoGenerateColumns = false;

            txtNumeroIdentificacion.Clear();
            txtNumeroIdentificacion.ReadOnly = true;
            txtNumeroFactura.Clear();
            txtNumeroFactura.ReadOnly = true;
            txtFechaFactura.Clear();
            txtFechaFactura.ReadOnly = true;
            txtCliente.Clear();
            txtCliente.ReadOnly = true;
            txtDireccion.Clear();
            txtDireccion.ReadOnly = true;
            txtConvencional.Clear();
            txtConvencional.ReadOnly = true;
            txtCelular.Clear();
            txtCelular.ReadOnly = true;

            dgvVenta.DataSource = new List<tbOrdenVentaDetalle>();

            txtComentarios.Clear();

            txtSubtotal.Text = Util.FormatoMoneda(0, 2);
            txtIVA.Text = Util.FormatoMoneda(0, 2);
            txtTotalNeto.Text = Util.FormatoMoneda(0, 2);

            txtNumeroIdentificacion.Select();
        }

        private void dgvVenta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                tbOrdenVentaDetalle ordenVentaDetalle = (tbOrdenVentaDetalle)dgvVenta.CurrentRow.DataBoundItem;

                if (e.ColumnIndex == dtcBodegaEntrada.Index && ordenVentaDetalle.tbProducto.tbTipoProducto.esInventariable)
                {
                    DataTable _DataTable = new DataTable();
                    _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Tienda"), new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Es facturable"), new DataColumn("Fecha de registro"), new DataColumn("bodega", typeof(object)) });

                    (from B in _dbCosolemEntities.tbBodega
                     where B.idTienda == Program.tbUsuario.tbEmpleado.idTienda && B.tbTienda.estadoRegistro && B.estadoRegistro
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
                    {
                        tbBodega bodega = (tbBodega)_frmBusqueda._object;
                        ordenVentaDetalle.idBodegaEntrada = bodega.idBodega;
                        ordenVentaDetalle.descripcionBodegaEntrada = bodega.idBodega + " - " + bodega.descripcion;
                    }
                }
            }
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            List<tbOrdenVentaDetalle> ordenVentaDetalle = new List<tbOrdenVentaDetalle>();
            if (ordenVenta != null) ordenVentaDetalle = ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro && x.tbProducto.tbTipoProducto.esInventariable).ToList();

            if (ordenVenta == null)
                MessageBox.Show("Seleccione una factura para poder realizar la devolución", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (ordenVentaDetalle.Where(x => x.idBodegaEntrada != 0).Count() < ordenVentaDetalle.Count)
                MessageBox.Show("Seleccione una bodega en los productos que va a devolver", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (String.IsNullOrEmpty(txtComentarios.Text.Trim()))
                MessageBox.Show("Ingrese un comentario para poder realizar la devolución", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                long idOrdenVentaCabecera = ordenVenta.idOrdenVentaCabecera;

                ordenVenta = (from OVC in _dbCosolemEntities.tbOrdenVentaCabecera where OVC.idOrdenVentaCabecera == idOrdenVentaCabecera select OVC).FirstOrDefault();

                long idEmpresa = ordenVenta.idEmpresaFactura.Value;
                long idTienda = ordenVenta.idTiendaFactura.Value;
                long numeroFactura = ordenVenta.numeroFactura.Value;

                ordenVenta.idEstadoOrdenVenta = 7;
                ordenVenta.comentarios = txtComentarios.Text.Trim();
                ordenVenta.fechaHoraEliminacion = Program.fechaHora;
                ordenVenta.idUsuarioEliminacion = idUsuario;
                ordenVenta.terminalEliminacion = Program.terminal;

                List<tbInventario> inventario = new List<tbInventario>();
                ordenVentaDetalle.ForEach(y =>
                {
                    long idBodega = y.idBodegaEntrada;
                    long idProducto = y.idProducto;
                    int cantidad = y.cantidad;

                    tbInventario _tbInventario = (from I in _dbCosolemEntities.tbInventario where I.idBodega == idBodega && I.idProducto == idProducto && I.estadoRegistro select I).FirstOrDefault();
                    if (_tbInventario == null)
                    {
                        _tbInventario = new tbInventario();
                        _tbInventario.idBodega = idBodega;
                        _tbInventario.idProducto = idProducto;
                        _tbInventario.fisicoDisponible = cantidad;
                        _tbInventario.reservado = 0;
                        _tbInventario.estadoRegistro = true;
                        _tbInventario.fechaHoraIngreso = Program.fechaHora;
                        _tbInventario.idUsuarioIngreso = idUsuario;
                        _tbInventario.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbInventario.AddObject(_tbInventario);
                    }
                    else
                    {
                        _tbInventario.fisicoDisponible += cantidad;
                        _tbInventario.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbInventario.idUsuarioUltimaModificacion = idUsuario;
                        _tbInventario.terminalUltimaModificacion = Program.terminal;
                    }

                    _tbInventario.concepto = "DEVOLUCIÓN DE FACTURA " + new String('0', 9 - numeroFactura.ToString().Trim().Length) + numeroFactura.ToString().Trim();
                    _tbInventario.fechaFactura = ordenVenta.fechaHoraFactura.Value;
                    _tbInventario.cliente = ordenVenta.numeroIdentificacion + " - " + ordenVenta.cliente;
                    _tbInventario.comentarios = ordenVenta.comentarios;
                    _tbInventario.codigoProducto = y.codigoProducto;
                    _tbInventario.descripcionProducto = y.descripcionProducto;
                    _tbInventario.cantidad = cantidad;

                    inventario.Add(_tbInventario);

                    tbTransaccionInventario _tbTransaccionInventario = new tbTransaccionInventario();
                    _tbTransaccionInventario.tipoTransaccion = "Ingreso de inventario por devolución de factura " + Util.setFormatoNumeroFactura(idEmpresa, idTienda, numeroFactura);
                    _tbTransaccionInventario.idBodega = idBodega;
                    _tbTransaccionInventario.idProducto = idProducto;
                    _tbTransaccionInventario.cantidad = cantidad;
                    _tbTransaccionInventario.estadoRegistro = true;
                    _tbTransaccionInventario.fechaHoraIngreso = Program.fechaHora;
                    _tbTransaccionInventario.idUsuarioIngreso = idUsuario;
                    _tbTransaccionInventario.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbTransaccionInventario.AddObject(_tbTransaccionInventario);
                });

                _dbCosolemEntities.SaveChanges();
                MessageBox.Show("Factura devuelta satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (inventario.Count > 0) new frmReporteIngresoInventarioConcepto(inventario).ShowDialog();
                tsbNuevo_Click(null, null);
            }
        }
    }
}
