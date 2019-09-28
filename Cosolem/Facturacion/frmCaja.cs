using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;

namespace Cosolem
{
    public partial class frmCaja : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idTienda = Program.tbUsuario.tbEmpleado.idTienda;
        long idUsuario = Program.tbUsuario.idUsuario;
        long idOrdenVenta = 0;
        tbOrdenVentaCabecera ordenVenta = null;

        BindingList<tbOrdenVentaFormaPago> _BindingListtbOrdenVentaFormaPago = null;

        public frmCaja(long idOrdenVenta)
        {
            this.idOrdenVenta = idOrdenVenta;
            InitializeComponent();
        }

        private void CalcularTotales()
        {
            txtCancelado.Text = Util.FormatoMoneda(_BindingListtbOrdenVentaFormaPago.Sum(x => x.valor), 2);
            decimal cambio = Decimal.Parse(txtCancelado.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture) - Decimal.Parse(txtTotal.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
            txtCambio.Text = Util.FormatoMoneda((cambio < 0 ? 0 : cambio), 2);
        }

        private void AgregarFormaPago(int idFormaPago, decimal valor)
        {
            tbOrdenVentaFormaPago _tbOrdenVentaFormaPago = new tbOrdenVentaFormaPago { idFormaPago = idFormaPago, valor = valor, estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbOrdenVentaFormaPago, EntityState.Detached);

            _BindingListtbOrdenVentaFormaPago.Add(_tbOrdenVentaFormaPago);
            CalcularTotales();
        }

        private void frmCaja_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvOrdenVentaDetalle.AutoGenerateColumns = false;
            dgvOrdenVentaFormaPago.AutoGenerateColumns = false;

            _BindingListtbOrdenVentaFormaPago = new BindingList<tbOrdenVentaFormaPago>();

            ordenVenta = (from OVC in _dbCosolemEntities.tbOrdenVentaCabecera where OVC.tipoOrdenVenta == "O" && OVC.idEstadoOrdenVenta == 1 && OVC.estadoRegistro && OVC.idOrdenVentaCabecera == idOrdenVenta select OVC).FirstOrDefault();
            tbPersona persona = ordenVenta.tbCliente.tbPersona;

            txtCliente.Text = ordenVenta.cliente;
            txtNumeroIdentificacion.Text = persona.numeroIdentificacion;
            txtDireccion.Text = ordenVenta.direccion;
            List<string> telefono = new List<string>();
            if (!String.IsNullOrEmpty(ordenVenta.convencional)) telefono.Add(ordenVenta.convencional);
            if (!String.IsNullOrEmpty(ordenVenta.celular)) telefono.Add(ordenVenta.celular);
            dtpFechaFactura.Value = Program.fechaHora;
            txtTelefono.Text = String.Join(", ", telefono);

            dgvOrdenVentaDetalle.DataSource = ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro).Select(y => new
            {
                cantidad = y.cantidad,
                descripcion = y.tbProducto.descripcion,
                precio = (ordenVenta.idFormaPago == 1 ? (ordenVenta.tipoVenta == "N" ? y.precioOferta : y.costo) : y.precioVentaPublico),
                subTotalBruto = y.subTotalBruto
            }).ToList();

            txtSubtotal.Text = Util.FormatoMoneda(ordenVenta.subTotalBruto, 2);
            txtIVA.Text = Util.FormatoMoneda(ordenVenta.IVA, 2);
            txtTotal.Text = Util.FormatoMoneda(ordenVenta.totalNeto, 2);

            List<tbFormaPago> _tbFormaPago = (from FP in _dbCosolemEntities.tbFormaPago select FP).ToList();
            _tbFormaPago.Insert(0, new tbFormaPago { idFormaPago = 0, descripcion = "Seleccione" });
            cmbFormaPago.DataSource = _tbFormaPago;
            cmbFormaPago.ValueMember = "idFormaPago";
            cmbFormaPago.DisplayMember = "descripcion";

            if (ordenVenta.tbOrdenVentaFinanciacion.Any())
            {
                tbOrdenVentaFinanciacion ordenVentaFinanciacion = ordenVenta.tbOrdenVentaFinanciacion.FirstOrDefault();
                if (ordenVentaFinanciacion.valorCuotaInicialExigible > 0) AgregarFormaPago(1, ordenVentaFinanciacion.valorCuotaInicialExigible);
            }
            dgvOrdenVentaFormaPago.DataSource = _BindingListtbOrdenVentaFormaPago;

            CalcularTotales();
        }

        private void btnAgregarFormaPago_Click(object sender, EventArgs e)
        {
            AgregarFormaPago(0, 0);            
        }

        private void dgvOrdenVentaFormaPago_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                TextBox txtValor = (TextBox)e.Control;
                txtValor.KeyPress -= txtValor_KeyPress;
                txtValor.KeyPress += txtValor_KeyPress;
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Program.decimalPoint))
                e.Handled = true;
            if ((e.KeyChar == Program.decimalPoint) && ((sender as TextBox).Text.IndexOf(Program.decimalPoint) > -1))
                e.Handled = true;
        }

        private void dgvOrdenVentaFormaPago_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrdenVentaFormaPago.CommitEdit(DataGridViewDataErrorContexts.Commit);
            CalcularTotales();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            dgvOrdenVentaFormaPago_CellEndEdit(null, null);
            string mensaje = String.Empty;

            decimal total = Decimal.Parse(txtTotal.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
            decimal cancelado = Decimal.Parse(txtCancelado.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
            long numeroFactura = 0;
            long.TryParse(txtNumeroFactura.Text.Trim(), out numeroFactura);

            if (numeroFactura == 0) mensaje += "Favor ingrese número de factura\n";
            if (numeroFactura > 0 && edmCosolemFunctions.existNumeroFactura(idEmpresa, idTienda, numeroFactura)) mensaje += "Número de factura ya existe\n";
            if (cancelado < total) mensaje += "Total de " + Util.FormatoMoneda((total - cancelado), 2) + " pendiente por cancelar\n";
            if (_BindingListtbOrdenVentaFormaPago.Where(x => x.idFormaPago == 0).Count() > 0) mensaje += "Favor seleccionar forma de pago\n";
            if (_BindingListtbOrdenVentaFormaPago.Where(x => x.valor == 0).Count() > 0) mensaje += "Favor ingrese valor\n";

            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                ordenVenta.idEstadoOrdenVenta = 5;
                ordenVenta.idEmpresaFactura = idEmpresa;
                ordenVenta.idTiendaFactura = idTienda;
                ordenVenta.numeroFactura = numeroFactura;
                ordenVenta.fechaHoraFactura = Program.fechaHora;
                ordenVenta.fechaHoraUltimaModificacion = Program.fechaHora;
                ordenVenta.idUsuarioUltimaModificacion = idUsuario;
                ordenVenta.terminalUltimaModificacion = Program.terminal;
                ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro).ToList().ForEach(y =>
                {
                    long idProducto = y.idProducto;
                    if (edmCosolemFunctions.isProductoInventariable(idProducto))
                    {
                        long idBodega = y.idBodega.Value;
                        int cantidad = y.cantidad;
                        bool estadoRegistro = y.estadoRegistro;

                        tbInventario _tbInventario = (from I in _dbCosolemEntities.tbInventario where I.idBodega == idBodega && I.idProducto == idProducto && estadoRegistro select I).FirstOrDefault();
                        _tbInventario.fisicoDisponible -= cantidad;
                        _tbInventario.reservado -= cantidad;
                        _tbInventario.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbInventario.idUsuarioUltimaModificacion = idUsuario;
                        _tbInventario.terminalUltimaModificacion = Program.terminal;

                        tbTransaccionInventario _tbTransaccionInventario = new tbTransaccionInventario();
                        _tbTransaccionInventario.tipoTransaccion = "Egreso de inventario por factura " + Util.setFormatoNumeroFactura(idEmpresa, idTienda, numeroFactura);
                        _tbTransaccionInventario.idBodega = idBodega;
                        _tbTransaccionInventario.idProducto = idProducto;
                        _tbTransaccionInventario.cantidad = -cantidad;
                        _tbTransaccionInventario.estadoRegistro = true;
                        _tbTransaccionInventario.fechaHoraIngreso = Program.fechaHora;
                        _tbTransaccionInventario.idUsuarioIngreso = idUsuario;
                        _tbTransaccionInventario.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbTransaccionInventario.AddObject(_tbTransaccionInventario);
                    }
                });
                _BindingListtbOrdenVentaFormaPago.ToList().ForEach(x => ordenVenta.tbOrdenVentaFormaPago.Add(x));
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Orden de venta facturada satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                Excel.Application application = new Excel.Application();
                application.Visible = false;
                Excel.Workbook workbook = application.Workbooks.Open(Application.StartupPath + "\\Reportes\\FORMATO FACTURA.xlsx");
                Excel.Worksheet worksheet = workbook.Worksheets["FORMATO"];

                worksheet.Cells[5, 2].value = ordenVenta.cliente;
                worksheet.Cells[6, 2].value = ordenVenta.numeroIdentificacion;
                worksheet.Cells[7, 2].value = ordenVenta.direccion;

                worksheet.Cells[5, 10].value = ordenVenta.fechaHoraFactura.Value.ToString("dd/MM/yyyy");
                worksheet.Cells[6, 10].value = String.Join(", ", new List<string> { ordenVenta.convencional, ordenVenta.celular });

                int RowIndex = 11;
                ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro).ToList().ForEach(y =>
                {
                    decimal precio = (ordenVenta.idFormaPago == 1 ? y.precioOferta : y.precioVentaPublico);
                    if (ordenVenta.tipoVenta == "P") precio = y.precio;

                    worksheet.Cells[RowIndex, 1].value = y.cantidad;
                    worksheet.Cells[RowIndex, 3].value = y.tbProducto.descripcion;
                    worksheet.Cells[RowIndex, 9].value = precio;
                    worksheet.Cells[RowIndex, 11].value = precio * y.cantidad;
                    RowIndex += 1;
                });

                worksheet.Cells[22, 3].value = ordenVenta.tbFormaPago.descripcion + (ordenVenta.idFormaPago == 1 ? "" : " - " + ordenVenta.tbProductoFinancieroCabecera.descripcion);
                worksheet.Cells[23, 11].value = ordenVenta.subTotalBruto;
                worksheet.Cells[24, 5].value = Util.ConvertirNumeroALetras(ordenVenta.totalNeto.ToString(), true, "dólares");
                worksheet.Cells[24, 11].value = (ordenVenta.tipoVenta == "P" ? ordenVenta.descuento : 0M);
                worksheet.Cells[26, 11].value = ordenVenta.IVA;
                worksheet.Cells[27, 11].value = ordenVenta.totalNeto;

                worksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                application.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgvOrdenVentaFormaPago_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == dicEliminar.Index)
                {
                    _BindingListtbOrdenVentaFormaPago.Remove((tbOrdenVentaFormaPago)dgvOrdenVentaFormaPago.CurrentRow.DataBoundItem);
                    dgvOrdenVentaFormaPago_CellEndEdit(null, null);
                }
            }
        }

        private void txtNumeroFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro desea anular la orden de venta a facturar?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ordenVenta.idEstadoOrdenVenta = 4;
                ordenVenta.fechaHoraEliminacion = Program.fechaHora;
                ordenVenta.idUsuarioEliminacion = idUsuario;
                ordenVenta.terminalEliminacion = Program.terminal;

                ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro).ToList().ForEach(y =>
                {
                    long idBodega = y.idBodega.Value;
                    long idProducto = y.idProducto;
                    int cantidad = y.cantidad;

                    tbInventario _tbInventario = (from I in _dbCosolemEntities.tbInventario where I.idBodega == idBodega && I.idProducto == idProducto && y.estadoRegistro select I).FirstOrDefault();
                    _tbInventario.reservado -= cantidad;
                    _tbInventario.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbInventario.idUsuarioUltimaModificacion = idUsuario;
                    _tbInventario.terminalUltimaModificacion = Program.terminal;
                });
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Orden de venta anulada satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void dgvOrdenVentaFormaPago_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is FormatException) return;
        }
    }
}
