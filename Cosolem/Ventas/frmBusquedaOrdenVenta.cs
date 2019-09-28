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
    public partial class frmBusquedaOrdenVenta : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idTienda = Program.tbUsuario.tbEmpleado.idTienda;
        long idUsuario = Program.tbUsuario.idUsuario;
        string tipoOrdenVenta = null;
        string tipoVenta = null;
        List<int> estadosOrdenVenta = new List<int> { };
        bool hacerOrdenVenta = false;

        public frmBusquedaOrdenVenta(string tipoOrdenVenta, string tipoVenta, string hacerOrdenVenta)
        {
            this.tipoOrdenVenta = tipoOrdenVenta;
            this.tipoVenta = tipoVenta;
            if (tipoOrdenVenta == "O") estadosOrdenVenta = new List<int> { 1 };
            else estadosOrdenVenta = new List<int> { 2, 3 };
            this.hacerOrdenVenta = Convert.ToBoolean(hacerOrdenVenta);
            InitializeComponent();
        }

        private void frmBusquedaOrdenVenta_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            txtNumeroIdentificacion.Clear();

            List<tbUsuario> usuarios = (from U in _dbCosolemEntities.tbUsuario where U.estadoRegistro && U.tbEmpleado.idEmpresa == idEmpresa select U).ToList();
            usuarios.Insert(0, new tbUsuario { idUsuario = 0, nombreUsuario = "Todos" });
            cmbUsuario.DataSource = usuarios;
            cmbUsuario.ValueMember = "idUsuario";
            cmbUsuario.DisplayMember = "nombreUsuario";
            cmbUsuario.SelectedValue = idUsuario;

            dtpFechaDesde.Value = DateTime.Now.Date;
            dtpFechaHasta.Value = DateTime.Now.Date;

            dgvOrdenVentaCabecera.AutoGenerateColumns = false;
            dgvOrdenVentaDetalle.AutoGenerateColumns = false;

            dgvOrdenVentaCabecera.DataSource = null;
            dgvOrdenVentaDetalle.DataSource = null;

            if (tipoOrdenVenta == "O")
            {
                dtcidOrdenVenta.HeaderText = "Orden de venta #";
                dgvOrdenVentaCabecera.Columns[dicComentarios.Index].Visible = false;
                dgvOrdenVentaCabecera.Columns[dicImprimir.Index].Visible = false;
            }
            if (tipoOrdenVenta == "C")
            {
                if (tipoVenta == "N")
                {
                    grbCliente.Dock = DockStyle.Fill;
                    grbDetalle.Visible = false;
                }
                if (tipoVenta == "P")
                {
                    dgvOrdenVentaCabecera.Columns[dtcSubtotal.Index].Visible = hacerOrdenVenta;
                    dgvOrdenVentaCabecera.Columns[dtcDescuento.Index].Visible = hacerOrdenVenta;
                    dgvOrdenVentaCabecera.Columns[dtcSubtotalBruto.Index].Visible = hacerOrdenVenta;
                    dgvOrdenVentaCabecera.Columns[dtcIVA.Index].Visible = hacerOrdenVenta;
                    dgvOrdenVentaCabecera.Columns[dtcTotalNeto.Index].Visible = hacerOrdenVenta;
                    dgvOrdenVentaCabecera.Columns[dicImprimir.Index].Visible = hacerOrdenVenta;

                    dgvOrdenVentaDetalle.Columns[dtcPrecio.Index].Visible = hacerOrdenVenta;
                    dgvOrdenVentaDetalle.Columns[dtcTotal.Index].Visible = hacerOrdenVenta;
                }
                tsbEliminar.Enabled = false;
                dtcidOrdenVenta.HeaderText = "Cotización #";
                dgvOrdenVentaCabecera.Columns[dccSeleccionado.Index].Visible = false;
                dgvOrdenVentaCabecera.Columns[dicFacturar.Index].Visible = false;
            }
            dgvOrdenVentaCabecera.Columns[dicHacerOrdenVenta.Index].Visible = hacerOrdenVenta;
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            long idUsuario = ((tbUsuario)cmbUsuario.SelectedItem).idUsuario;

            _dbCosolemEntities = new dbCosolemEntities();
            var _tbOrdenVentaCabecera = (from OV in _dbCosolemEntities.tbOrdenVentaCabecera where OV.idEmpresaOrdenVenta == idEmpresa && OV.idTiendaOrdenVenta == idTienda && OV.tipoOrdenVenta == tipoOrdenVenta && OV.tipoVenta == tipoVenta && estadosOrdenVenta.Contains(OV.idEstadoOrdenVenta) && EntityFunctions.TruncateTime(OV.fechaHoraOrdenVenta) >= EntityFunctions.TruncateTime(dtpFechaDesde.Value) && EntityFunctions.TruncateTime(OV.fechaHoraOrdenVenta) <= EntityFunctions.TruncateTime(dtpFechaHasta.Value) select OV);
            if (idUsuario != 0) _tbOrdenVentaCabecera = (from OV in _tbOrdenVentaCabecera where OV.idUsuarioIngreso == idUsuario select OV);
            if (!String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) _tbOrdenVentaCabecera = (from OV in _tbOrdenVentaCabecera where OV.numeroIdentificacion == txtNumeroIdentificacion.Text.Trim() select OV);
            if (tipoOrdenVenta == "C") _tbOrdenVentaCabecera = (from OV in _tbOrdenVentaCabecera where OV.tbSeguimientoCotizacionCabecera.Any(x => x.idEstadoSeguimientoCotizacion != 3) select OV);
            SortableBindingList<tbOrdenVentaCabecera> ordenesVenta = new SortableBindingList<tbOrdenVentaCabecera>(_tbOrdenVentaCabecera.ToList());
            ordenesVenta.ToList().ForEach(x =>
            {
                x.seleccionado = false;
                x.usuarioIngreso = edmCosolemFunctions.getNombreUsuario(x.idUsuarioIngreso);
                x.descripcionFormaPago = x.tbFormaPago.descripcion;
                x.descripcionCliente = x.numeroIdentificacion + " - " + x.cliente;
                x.descripcionEstadoOrdenVenta = x.tbEstadoOrdenVenta.descripcion;
            });

            dgvOrdenVentaCabecera.DataSource = ordenesVenta;
            dgvOrdenVentaDetalle.DataSource = null;
        }

        private void dgvOrdenVentaCabecera_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                tbOrdenVentaCabecera ordenVenta = (tbOrdenVentaCabecera)dgvOrdenVentaCabecera.CurrentRow.DataBoundItem;
                long idOrdenVenta = ordenVenta.idOrdenVentaCabecera;
                int idEstadoOrdenVenta = ordenVenta.idEstadoOrdenVenta;
                if (!new List<int> { dccSeleccionado.Index, dicComentarios.Index, dicHacerOrdenVenta.Index, dicFacturar.Index, dicImprimir.Index }.Contains(e.ColumnIndex))
                {
                    dgvOrdenVentaDetalle.DataSource = new List<tbOrdenVentaDetalle>((ordenVenta).tbOrdenVentaDetalle).Where(x => x.estadoRegistro).Select(y => new
                    {
                        producto = y.tbProducto.codigoProducto + " - " + y.tbProducto.descripcion,
                        bodega = (y.idBodega.HasValue ? y.tbBodega.tbTienda.descripcion + " - " + y.tbBodega.descripcion : String.Empty),
                        precio = (ordenVenta.tipoVenta == "N" ? y.precioInformativo : y.costo),
                        cantidad = y.cantidad,
                        total = y.subTotal
                    }).ToList();
                }
                if (e.ColumnIndex == dicComentarios.Index)
                {
                    frmSeguimientoCotizacion _frmSeguimientoCotizacion = new frmSeguimientoCotizacion(idOrdenVenta, idEstadoOrdenVenta);
                    _frmSeguimientoCotizacion.Text = "Seguimiento de cotización " + " " + idOrdenVenta.ToString();
                    if (_frmSeguimientoCotizacion.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        tsbBuscar_Click(null, null);
                }
                if (e.ColumnIndex == dicHacerOrdenVenta.Index)
                {
                    if (idEstadoOrdenVenta == 3)
                        MessageBox.Show("Cotización ya no se puede volver a convertirse en orden de venta", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        frmOrdenVenta _frmOrdenVenta = new frmOrdenVenta("O", tipoVenta, "1");
                        _frmOrdenVenta.Text = (tipoVenta == "N" ? "Orden de venta" : "Orden de venta outsoursing");
                        _frmOrdenVenta.idOrdenVenta = idOrdenVenta;
                        if (_frmOrdenVenta.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            ordenVenta = (from OV in _dbCosolemEntities.tbOrdenVentaCabecera where OV.idOrdenVentaCabecera == idOrdenVenta select OV).FirstOrDefault();
                            ordenVenta.idEstadoOrdenVenta = 3;
                            ordenVenta.fechaHoraUltimaModificacion = Program.fechaHora;
                            ordenVenta.idUsuarioUltimaModificacion = idUsuario;
                            ordenVenta.terminalUltimaModificacion = Program.terminal;

                            tbSeguimientoCotizacionCabecera seguimientoCotizacion = ordenVenta.tbSeguimientoCotizacionCabecera.FirstOrDefault();
                            seguimientoCotizacion.idEstadoSeguimientoCotizacion = 2;
                            seguimientoCotizacion.fechaHoraUltimaModificacion = Program.fechaHora;
                            seguimientoCotizacion.idUsuarioUltimaModificacion = idUsuario;
                            seguimientoCotizacion.terminalUltimaModificacion = Program.terminal;
                            seguimientoCotizacion.tbSeguimientoCotizacionDetalle.Add(new tbSeguimientoCotizacionDetalle { comentarioSeguimiento = "Cotización " + idOrdenVenta.ToString() + " es una orden de venta", estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });

                            _dbCosolemEntities.SaveChanges();

                            tsbBuscar_Click(null, null);
                        }
                    }
                }
                if (e.ColumnIndex == dicFacturar.Index)
                {
                    frmCaja _frmCaja = new frmCaja(idOrdenVenta);
                    _frmCaja.Text = this.Text + " " + idOrdenVenta.ToString();
                    if (_frmCaja.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        tsbBuscar_Click(null, null);
                }
                if (e.ColumnIndex == dicImprimir.Index)
                {
                    if (idEstadoOrdenVenta == 3)
                        MessageBox.Show("Cotización ya no se puede imprimir porque se convirtió en orden de venta", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        new frmReporteCotizacion(idOrdenVenta).ShowDialog();
                }
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            dgvOrdenVentaCabecera_CellEndEdit(null, null);
            List<tbOrdenVentaCabecera> ordenesVenta = ((SortableBindingList<tbOrdenVentaCabecera>)dgvOrdenVentaCabecera.DataSource).ToList();
            if (ordenesVenta.Where(x => x.seleccionado).Count() == 0) MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (MessageBox.Show("¿Seguro desea anular las ordenes de venta seleccionadas?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    ordenesVenta.ForEach(x =>
                    {
                        x.idEstadoOrdenVenta = 4;
                        x.fechaHoraEliminacion = Program.fechaHora;
                        x.idUsuarioEliminacion = idUsuario;
                        x.terminalEliminacion = Program.terminal;

                        x.tbOrdenVentaDetalle.Where(y => y.estadoRegistro).ToList().ForEach(z =>
                        {
                            long idBodega = z.idBodega.Value;
                            long idProducto = z.idProducto;
                            int cantidad = z.cantidad;

                            tbInventario _tbInventario = (from I in _dbCosolemEntities.tbInventario where I.idBodega == idBodega && I.idProducto == idProducto && z.estadoRegistro select I).FirstOrDefault();
                            _tbInventario.reservado -= cantidad;
                            _tbInventario.fechaHoraUltimaModificacion = Program.fechaHora;
                            _tbInventario.idUsuarioUltimaModificacion = idUsuario;
                            _tbInventario.terminalUltimaModificacion = Program.terminal;
                        });
                    });
                    _dbCosolemEntities.SaveChanges();
                    MessageBox.Show("Ordenes de venta anuladas satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tsbBuscar_Click(null, null);
                }
            }
        }

        private void dgvOrdenVentaCabecera_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrdenVentaCabecera.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmBusquedaOrdenVenta_Load(null, null);
        }

        private void dgvOrdenVentaCabecera_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (tipoOrdenVenta == "C")
                {
                    tbOrdenVentaCabecera ordenVenta = (tbOrdenVentaCabecera)dgvOrdenVentaCabecera.CurrentRow.DataBoundItem;
                    long idOrdenVenta = ordenVenta.idOrdenVentaCabecera;
                    int idEstadoOrdenVenta = ordenVenta.idEstadoOrdenVenta;

                    if (idEstadoOrdenVenta == 3)
                        MessageBox.Show("Cotización ya no puede ser modificada porque se convirtió en una orden de venta", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        frmOrdenVenta _frmOrdenVenta = new frmOrdenVenta("C", tipoVenta, "2");
                        _frmOrdenVenta.Text = (tipoVenta == "N" ? "Cotización" : "Egreso de pedidos outsoursing");
                        _frmOrdenVenta.idOrdenVenta = idOrdenVenta;
                        if (_frmOrdenVenta.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            ordenVenta = (from OV in _dbCosolemEntities.tbOrdenVentaCabecera where OV.idOrdenVentaCabecera == idOrdenVenta select OV).FirstOrDefault();

                            tbSeguimientoCotizacionCabecera seguimientoCotizacion = ordenVenta.tbSeguimientoCotizacionCabecera.FirstOrDefault();
                            seguimientoCotizacion.idEstadoSeguimientoCotizacion = 1;
                            seguimientoCotizacion.fechaHoraUltimaModificacion = Program.fechaHora;
                            seguimientoCotizacion.idUsuarioUltimaModificacion = idUsuario;
                            seguimientoCotizacion.terminalUltimaModificacion = Program.terminal;
                            seguimientoCotizacion.tbSeguimientoCotizacionDetalle.Add(new tbSeguimientoCotizacionDetalle { comentarioSeguimiento = "Cotización " + idOrdenVenta.ToString() + " modificada", estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });

                            _dbCosolemEntities.SaveChanges();

                            tsbBuscar_Click(null, null);
                        }
                    }
                }
            }
        }
    }
}
