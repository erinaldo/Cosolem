using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;

namespace Cosolem
{
    public partial class frmBusquedaFactura : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idTienda = Program.tbUsuario.tbEmpleado.idTienda;
        long idUsuario = Program.tbUsuario.idUsuario;
        bool devoluciones = false;

        public tbOrdenVentaCabecera ordenVenta = null;

        public frmBusquedaFactura(string habilitarEliminar, string devoluciones)
        {
            InitializeComponent();

            tsbEliminar.Visible = Convert.ToBoolean(habilitarEliminar);
            toolStripSeparator1.Visible = Convert.ToBoolean(habilitarEliminar);
            dccSeleccionado.Visible = Convert.ToBoolean(habilitarEliminar);
            this.devoluciones = Convert.ToBoolean(devoluciones);
        }

        private void txtNumeroFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void frmBusquedaFactura_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            txtNumeroIdentificacion.Clear();
            txtNumeroFactura.Clear();

            dgvOrdenVentaCabecera.AutoGenerateColumns = false;
            dgvOrdenVentaDetalle.AutoGenerateColumns = false;
            dgvOrdenVentaFormaPago.AutoGenerateColumns = false;

            dgvOrdenVentaCabecera.DataSource = null;
            dgvOrdenVentaDetalle.DataSource = null;
            dgvOrdenVentaFormaPago.DataSource = null;
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            var ordenesVenta = (from OV in _dbCosolemEntities.tbOrdenVentaCabecera where OV.idEmpresaFactura == idEmpresa && OV.idTiendaFactura == idTienda && OV.tipoOrdenVenta == "O" && OV.idEstadoOrdenVenta == 5 select OV);
            if (devoluciones)
            {
                DateTime fechaDesde = Program.fechaHora.AddDays(-8).Date;
                DateTime fechaHasta = Program.fechaHora.AddDays(-1).Date;
                ordenesVenta = (from OV in ordenesVenta where EntityFunctions.TruncateTime(OV.fechaHoraFactura) >= EntityFunctions.TruncateTime(fechaDesde) && EntityFunctions.TruncateTime(OV.fechaHoraFactura) <= EntityFunctions.TruncateTime(fechaHasta) select OV);
            }
            else
                ordenesVenta = (from OV in ordenesVenta where EntityFunctions.TruncateTime(OV.fechaHoraFactura) == EntityFunctions.TruncateTime(Program.fechaHora.Date) select OV);
            if (!String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) ordenesVenta = (from OV in ordenesVenta where OV.tbCliente.tbPersona.numeroIdentificacion == txtNumeroIdentificacion.Text.Trim() select OV);
            if (!String.IsNullOrEmpty(txtNumeroFactura.Text.Trim()))
            {
                long numeroFactura = Convert.ToInt64(txtNumeroFactura.Text.Trim());
                ordenesVenta = (from OV in ordenesVenta where OV.numeroFactura == numeroFactura select OV);
            }
            SortableBindingList<tbOrdenVentaCabecera> _BindingListtbOrdenVentaCabecera = new SortableBindingList<tbOrdenVentaCabecera>(ordenesVenta.ToList());
            _BindingListtbOrdenVentaCabecera.ToList().ForEach(x =>
            {
                x.seleccionado = false;
                x.descripcionFormaPago = x.tbFormaPago.descripcion;
                x.descripcionCliente = x.numeroIdentificacion + " - " + x.cliente;
                x.descripcionEstadoOrdenVenta = x.tbEstadoOrdenVenta.descripcion;
            });

            dgvOrdenVentaCabecera.DataSource = _BindingListtbOrdenVentaCabecera;
            dgvOrdenVentaDetalle.DataSource = null;
            dgvOrdenVentaFormaPago.DataSource = null;
        }

        private void dgvOrdenVentaCabecera_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                tbOrdenVentaCabecera ordenVenta = (tbOrdenVentaCabecera)dgvOrdenVentaCabecera.CurrentRow.DataBoundItem;
                dgvOrdenVentaDetalle.DataSource = new List<tbOrdenVentaDetalle>((ordenVenta).tbOrdenVentaDetalle).Where(x => x.estadoRegistro).Select(y => new
                {
                    producto = y.tbProducto.codigoProducto + " - " + y.tbProducto.descripcion,
                    bodega = (y.idBodega.HasValue ? y.idBodega.ToString() + " - " + y.tbBodega.descripcion : String.Empty),
                    precio = (ordenVenta.idFormaPago == 1 ? (ordenVenta.tipoVenta == "N" ? y.precioOferta : y.costo) : y.precioVentaPublico),
                    cantidad = y.cantidad,
                    subTotalBruto = y.subTotalBruto,
                    IVA = y.IVA,
                    totalNeto = y.totalNeto
                }).ToList();
                dgvOrdenVentaFormaPago.DataSource = new List<tbOrdenVentaFormaPago>((ordenVenta).tbOrdenVentaFormaPago).Select(x => new
                {
                    descripcionFormaPago = x.tbFormaPago.descripcion,
                    valor = x.valor
                }).ToList();
            }
        }

        private void dgvOrdenVentaCabecera_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrdenVentaCabecera.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmBusquedaFactura_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            dgvOrdenVentaCabecera_CellEndEdit(null, null);
            List<tbOrdenVentaCabecera> ordenesVenta = ((BindingList<tbOrdenVentaCabecera>)dgvOrdenVentaCabecera.DataSource).ToList();
            if (ordenesVenta.Where(x => x.seleccionado).Count() == 0) MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (MessageBox.Show("¿Seguro desea anular las facturas seleccionadas?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    ordenesVenta.Where(x => x.seleccionado).ToList().ForEach(x =>
                    {
                        long numeroFactura = x.numeroFactura.Value;

                        x.idEstadoOrdenVenta = 6;
                        x.fechaHoraEliminacion = Program.fechaHora;
                        x.idUsuarioEliminacion = idUsuario;
                        x.terminalEliminacion = Program.terminal;

                        x.tbOrdenVentaDetalle.Where(y => y.estadoRegistro).ToList().ForEach(z =>
                        {
                            long idProducto = z.idProducto;
                            if (edmCosolemFunctions.isProductoInventariable(idProducto))
                            {
                                long idBodega = z.idBodega.Value;
                                int cantidad = z.cantidad;

                                tbInventario _tbInventario = (from I in _dbCosolemEntities.tbInventario where I.idBodega == idBodega && I.idProducto == idProducto && I.estadoRegistro select I).FirstOrDefault();
                                _tbInventario.fisicoDisponible += cantidad;
                                _tbInventario.fechaHoraUltimaModificacion = Program.fechaHora;
                                _tbInventario.idUsuarioUltimaModificacion = idUsuario;
                                _tbInventario.terminalUltimaModificacion = Program.terminal;

                                tbTransaccionInventario _tbTransaccionInventario = new tbTransaccionInventario();
                                _tbTransaccionInventario.tipoTransaccion = "Ingreso de inventario por anulación de factura " + Util.setFormatoNumeroFactura(idEmpresa, idTienda, numeroFactura);
                                _tbTransaccionInventario.idBodega = idBodega;
                                _tbTransaccionInventario.idProducto = idProducto;
                                _tbTransaccionInventario.cantidad = cantidad;
                                _tbTransaccionInventario.estadoRegistro = true;
                                _tbTransaccionInventario.fechaHoraIngreso = Program.fechaHora;
                                _tbTransaccionInventario.idUsuarioIngreso = idUsuario;
                                _tbTransaccionInventario.terminalIngreso = Program.terminal;
                                _dbCosolemEntities.tbTransaccionInventario.AddObject(_tbTransaccionInventario);
                            }
                        });
                    });
                    _dbCosolemEntities.SaveChanges();
                    MessageBox.Show("Facturas anuladas satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tsbBuscar_Click(null, null);
                }
            }
        }

        private void dgvOrdenVentaCabecera_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && devoluciones)
            {
                ordenVenta = (tbOrdenVentaCabecera)dgvOrdenVentaCabecera.CurrentRow.DataBoundItem;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
