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
    public partial class frmOrdenVenta : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idTienda = Program.tbUsuario.tbEmpleado.idTienda;
        long idUsuario = Program.tbUsuario.idUsuario;
        BindingList<tbOrdenVentaDetalle> _BindingListtbOrdenVentaDetalle = null;
        string tipoOrdenVenta = null;
        string tipoVenta = null;
        int idEstadoOrdenVenta = 0;

        public long idOrdenVenta = 0;

        class TipoPersona
        {
            public int idTipoPersona { get; set; }
            public string descripcion { get; set; }
        }

        class TipoIdentificacion
        {
            public int idTipoIdentificacion { get; set; }
            public string descripcion { get; set; }
            public int cantidadCaracteres { get; set; }
        }

        private void HabilitarDatosTipoPersona()
        {
            txtPrimerNombre.Clear();
            txtSegundoNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
            txtRazonSocial.Clear();

            txtPrimerNombre.Enabled = true;
            txtSegundoNombre.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            txtRazonSocial.Enabled = false;
            TipoPersona _tbTipoPersona = (TipoPersona)cmbTipoPersona.SelectedItem;

            if (_tbTipoPersona.idTipoPersona == 2)
            {
                txtPrimerNombre.Enabled = false;
                txtSegundoNombre.Enabled = false;
                txtApellidoPaterno.Enabled = false;
                txtApellidoMaterno.Enabled = false;
                txtRazonSocial.Enabled = true;
            }
        }

        public frmOrdenVenta(string tipoOrdenVenta, string tipoVenta, string idEstadoOrdenVenta)
        {
            this.tipoOrdenVenta = tipoOrdenVenta;
            this.tipoVenta = tipoVenta;
            this.idEstadoOrdenVenta = Convert.ToInt32(idEstadoOrdenVenta);
            InitializeComponent();
        }

        private void frmOrdenVenta_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvOrdenVentaDetalle.AutoGenerateColumns = false;

            _BindingListtbOrdenVentaDetalle = new BindingList<tbOrdenVentaDetalle>();

            Util.ObtenerControles(this, typeof(TextBox)).ForEach(x =>
            {
                TextBox textBox = (TextBox)x;
                if (!textBox.Name.ToLower().Contains("correo")) textBox.CharacterCasing = CharacterCasing.Upper;
            });

            cmbTipoPersona.DataSource = (from TP in _dbCosolemEntities.tbTipoPersona select new TipoPersona { idTipoPersona = TP.idTipoPersona, descripcion = TP.descripcion }).ToList();
            cmbTipoPersona.ValueMember = "idTipoPersona";
            cmbTipoPersona.DisplayMember = "descripcion";
            cmbTipoPersona_SelectionChangeCommitted(null, null);

            var _tbProvincia = (from P in _dbCosolemEntities.tbProvincia select new { idProvincia = P.idProvincia, descripcion = P.descripcion, tbCanton = (from C in P.tbCanton select new { idCanton = C.idCanton, descripcion = C.descripcion }) }).ToList();
            cmbProvinciaCliente.DataSource = _tbProvincia;
            cmbProvinciaCliente.ValueMember = "idProvincia";
            cmbProvinciaCliente.DisplayMember = "descripcion";

            cmbProvinciaEntregaDomicilio.BindingContext = new BindingContext();
            cmbProvinciaEntregaDomicilio.DataSource = _tbProvincia;
            cmbProvinciaEntregaDomicilio.ValueMember = "idProvincia";
            cmbProvinciaEntregaDomicilio.DisplayMember = "descripcion";

            var _tbCanton = ((dynamic)cmbProvinciaCliente.SelectedItem).tbCanton;
            cmbCantonCliente.DataSource = _tbCanton;
            cmbCantonCliente.ValueMember = "idCanton";
            cmbCantonCliente.DisplayMember = "descripcion";

            cmbCantonEntregaDomicilio.BindingContext = new BindingContext();
            cmbCantonEntregaDomicilio.DataSource = _tbCanton;
            cmbCantonEntregaDomicilio.ValueMember = "idCanton";
            cmbCantonEntregaDomicilio.DisplayMember = "descripcion";

            List<tbProductoFinancieroCabecera> _tbProductoFinancieroCabecera = (from PFC in _dbCosolemEntities.tbProductoFinancieroCabecera where PFC.idEmpresa == idEmpresa && PFC.estadoRegistro select PFC).ToList();
            _tbProductoFinancieroCabecera.Insert(0, new tbProductoFinancieroCabecera { idProductoFinancieroCabecera = 0, descripcion = "Seleccione", plazo = 0, porcentajeCuotaInicialExigible = 0 });
            cmbProductoFinanciero.DataSource = _tbProductoFinancieroCabecera;
            cmbProductoFinanciero.ValueMember = "idProductoFinancieroCabecera";
            cmbProductoFinanciero.DisplayMember = "descripcion";

            cmbProductoFinanciero.SelectedValue = _tbProductoFinancieroCabecera.FirstOrDefault().idProductoFinancieroCabecera;
            cmbProductoFinanciero.Enabled = !rdbEfectivo.Checked;

            btnLimpiarCliente_Click(null, null);

            btnLimpiarProducto_Click(null, null);
            dgvOrdenVentaDetalle.DataSource = _BindingListtbOrdenVentaDetalle;

            btnLimpiarEntregaDomicilio_Click(null, null);

            rdbCredito.Checked = true;
            if (tipoVenta == "P")
            {
                rdbEfectivo.Checked = true;
                grbFormaPago.Enabled = false;
            }

            if (idOrdenVenta > 0)
            {
                tbOrdenVentaCabecera ordenVenta = (from OVC in _dbCosolemEntities.tbOrdenVentaCabecera where OVC.idOrdenVentaCabecera == idOrdenVenta select OVC).FirstOrDefault();
                SetearPersona(ordenVenta.tbCliente.tbPersona);

                if (ordenVenta.idFormaPago == 1) rdbEfectivo.Checked = true;
                if (ordenVenta.idFormaPago == 2)
                {
                    rdbCredito.Checked = true;
                    cmbProductoFinanciero.SelectedValue = ordenVenta.idProductoFinanciero;
                }

                ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro).ToList().ForEach(y =>
                {
                    tbProducto producto = y.tbProducto;
                    tbPrecio precio = edmCosolemFunctions.getPrecio(idEmpresa, producto.idProducto);
                    tbOrdenVentaDetalle _tbOrdenVentaDetalle = new tbOrdenVentaDetalle { idProducto = producto.idProducto, codigoProducto = producto.codigoProducto, descripcionProducto = producto.codigoProducto + " - " + producto.descripcion, costo = precio.costo, precioOferta = precio.precioOferta, precioVentaPublico = precio.precioVentaPublico, precioInformativo = precio.precioInformativo, idBodega = null, descripcionBodegaSalida = string.Empty, estadoRegistro = true };
                    if (tipoOrdenVenta == "C")
                    {
                        _tbOrdenVentaDetalle.idOrdenVentaCabecera = y.idOrdenVentaCabecera;
                        _tbOrdenVentaDetalle.idOrdenVentaDetalle = y.idOrdenVentaDetalle;
                    }
                    _tbOrdenVentaDetalle.precio = precio.precioInformativo;
                    _tbOrdenVentaDetalle.cantidad = y.cantidad;
                    _tbOrdenVentaDetalle.subTotal = Math.Round(precio.precioInformativo * y.cantidad, 2);
                    if (tipoVenta == "N") _tbOrdenVentaDetalle.descuento = Math.Round(_tbOrdenVentaDetalle.subTotal - ((rdbEfectivo.Checked ? precio.precioOferta : precio.precioVentaPublico) * y.cantidad), 2);
                    if (tipoVenta == "P") _tbOrdenVentaDetalle.descuento = Math.Round(_tbOrdenVentaDetalle.subTotal - (precio.costo * y.cantidad), 2);
                    _tbOrdenVentaDetalle.subTotalBruto = Math.Round(_tbOrdenVentaDetalle.subTotal - _tbOrdenVentaDetalle.descuento, 2);
                    decimal porcentajeIVA = edmCosolemFunctions.getPorcentajeIVA(producto.idProducto, idTienda);
                    _tbOrdenVentaDetalle.porcentajeIVA = porcentajeIVA * 100M;
                    _tbOrdenVentaDetalle.IVA = Math.Round(_tbOrdenVentaDetalle.subTotalBruto * porcentajeIVA, 2);
                    _tbOrdenVentaDetalle.totalNeto = Math.Round(_tbOrdenVentaDetalle.subTotalBruto + _tbOrdenVentaDetalle.IVA, 2);

                    _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbOrdenVentaDetalle, EntityState.Detached);

                    _BindingListtbOrdenVentaDetalle.Add(_tbOrdenVentaDetalle);
                });

                if(ordenVenta.entregaDomicilio)
                {
                    chbEntregaDomicilio.Checked = ordenVenta.entregaDomicilio;
                    cmbProvinciaEntregaDomicilio.SelectedValue = ordenVenta.idProvinciaEntregaDomicilio.Value;
                    cmbCantonEntregaDomicilio.SelectedValue = ordenVenta.idCantonEntregaDomicilio.Value;
                    txtDireccionEntregaDomicilio.Text = ordenVenta.direccionEntregaDomicilio;
                    txtReferenciaEntregaDomicilio.Text = ordenVenta.referenciaEntregaDomicilio;
                    txtContactoEntregaDomicilio.Text = ordenVenta.contactoEntregaDomicilio;
                }
            }

            if (tipoOrdenVenta == "C")
            {
                txtDescripcionTiendaBodega.Enabled = false;
                dgvOrdenVentaDetalle.CellDoubleClick -= new DataGridViewCellEventHandler(dgvOrdenVentaDetalle_CellDoubleClick);

                pnlTotales.Visible = (tipoVenta == "N");
            }
            CalcularTotales();
            txtNumeroIdentificacion.Select();
        }

        private void cmbTipoPersona_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TipoPersona _tbTipoPersona = (TipoPersona)cmbTipoPersona.SelectedItem;
            List<TipoIdentificacion> _tbTipoIdentificacion = (from TI in _dbCosolemEntities.tbTipoIdentificacion select new TipoIdentificacion { idTipoIdentificacion = TI.idTipoIdentificacion, descripcion = TI.descripcion, cantidadCaracteres = TI.cantidadCaracteres }).ToList();
            if (_tbTipoPersona.idTipoPersona == 2) _tbTipoIdentificacion = _tbTipoIdentificacion.Where(x => x.idTipoIdentificacion == 2).ToList();
            cmbTipoIdentificacion.DataSource = _tbTipoIdentificacion;
            cmbTipoIdentificacion.ValueMember = "idTipoIdentificacion";
            cmbTipoIdentificacion.DisplayMember = "descripcion";

            HabilitarDatosTipoPersona();

            cmbTipoIdentificacion_SelectionChangeCommitted(null, null);
        }

        private void cmbTipoIdentificacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtNumeroIdentificacion.MaxLength = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).cantidadCaracteres;
            txtNumeroIdentificacion.Clear();
            txtNumeroIdentificacion.Select();
        }

        private void txtNumeroIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                int idTipoPersona = ((TipoPersona)cmbTipoPersona.SelectedItem).idTipoPersona;
                int idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();

                tbPersona _tbPersona = (from P in _dbCosolemEntities.tbPersona where P.idTipoPersona == idTipoPersona && P.idTipoIdentificacion == idTipoIdentificacion && P.numeroIdentificacion == numeroIdentificacion select P).FirstOrDefault();
                if (_tbPersona == null) MessageBox.Show("Cliente no exite, favor registre sus datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else SetearPersona(_tbPersona);

                if (idTipoPersona == 1) txtPrimerNombre.Select();
                if (idTipoPersona == 2) txtRazonSocial.Select();
            }
            else
            {
                int idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                if (new List<int> { 1, 2 }.Contains(idTipoIdentificacion))
                {
                    if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                    {
                        e.Handled = true;
                        return;
                    }
                }
                else
                {
                    if (!(char.IsLetter(e.KeyChar)) && !(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void SetearPersona(tbPersona _tbPersona)
        {
            cmbTipoPersona.SelectedValue = _tbPersona.idTipoPersona;
            cmbTipoPersona_SelectionChangeCommitted(null, null);
            cmbTipoIdentificacion.SelectedValue = _tbPersona.idTipoIdentificacion;
            cmbTipoIdentificacion_SelectionChangeCommitted(null, null);
            txtNumeroIdentificacion.Text = _tbPersona.numeroIdentificacion;
            txtNumeroIdentificacion.Tag = _tbPersona.idPersona;
            txtPrimerNombre.Text = _tbPersona.primerNombre;
            txtSegundoNombre.Text = _tbPersona.segundoNombre;
            txtApellidoPaterno.Text = _tbPersona.apellidoPaterno;
            txtApellidoMaterno.Text = _tbPersona.apellidoMaterno;
            txtRazonSocial.Text = _tbPersona.razonSocial;
            txtCorreoElectronico.Text = _tbPersona.correoElectronico;
            tbDireccion _tbDireccion = _tbPersona.tbDireccion.Where(x => x.esPrincipal).FirstOrDefault();
            if (_tbDireccion != null)
            {
                cmbProvinciaCliente.SelectedValue = _tbDireccion.tbCanton.idProvincia;
                cmbProvinciaCliente_SelectionChangeCommitted(null, null);
                cmbCantonCliente.SelectedValue = _tbDireccion.idCanton;
                tbTelefono _tbTelefonoConvencional = _tbDireccion.tbTelefono.Where(x => x.idTipoTelefono == 1).FirstOrDefault();
                if (_tbTelefonoConvencional != null)
                {
                    txtConvencional.Tag = _tbTelefonoConvencional.idTelefono;
                    txtConvencional.Text = _tbTelefonoConvencional.numero;
                }
                tbTelefono _tbTelefonoCelular = _tbDireccion.tbTelefono.Where(x => x.idTipoTelefono == 2).FirstOrDefault();
                if (_tbTelefonoCelular != null)
                {
                    txtCelular.Tag = _tbTelefonoCelular.idTelefono;
                    txtCelular.Text = _tbTelefonoCelular.numero;
                }
                txtDireccionCliente.Tag = _tbDireccion.idDireccion;
                txtDireccionCliente.Text = _tbDireccion.direccionCompleta;
            }
            cmbTipoPersona.Enabled = false;
            cmbTipoIdentificacion.Enabled = false;
            txtNumeroIdentificacion.Enabled = false;
            if (_tbPersona.idTipoPersona == 1) txtPrimerNombre.Select();
            if (_tbPersona.idTipoPersona == 2) txtRazonSocial.Select();
        }

        private void txtNumeroIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                DataTable _DataTable = new DataTable();
                _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Tipo de persona"), new DataColumn("Tipo de identificación"), new DataColumn("Número de identificación"), new DataColumn("Nombre completo"), new DataColumn("Es cliente"), new DataColumn("Fecha de registro"), new DataColumn("persona", typeof(object)) });

                (from P in _dbCosolemEntities.tbPersona
                 from C in _dbCosolemEntities.tbCliente.Where(x => x.idCliente == P.idPersona && x.estadoRegistro == P.estadoRegistro).DefaultIfEmpty()
                 where P.estadoRegistro
                 select new
                 {
                     tipoPersona = P.tbTipoPersona.descripcion,
                     tipoIdentificacion = P.tbTipoIdentificacion.descripcion,
                     numeroIdentificacion = P.numeroIdentificacion,
                     nombreCompleto = P.nombreCompleto,
                     esCliente = (C == null ? "No" : "Sí"),
                     fechaRegistro = (C == null ? P.fechaHoraIngreso : C.fechaHoraIngreso),
                     persona = P
                 }).ToList().ForEach(x => _DataTable.Rows.Add(x.tipoPersona, x.tipoIdentificacion, x.numeroIdentificacion, x.nombreCompleto, x.esCliente, x.fechaRegistro, x.persona));

                frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
                if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    SetearPersona((tbPersona)_frmBusqueda._object);
            }
        }

        private void cmbProvinciaCliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var _tbCanton = ((dynamic)cmbProvinciaCliente.SelectedItem).tbCanton;

            cmbCantonCliente.DataSource = _tbCanton;
            cmbCantonCliente.ValueMember = "idCanton";
            cmbCantonCliente.DisplayMember = "descripcion";
        }

        private void cmbProvinciaEntregaDomicilio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var _tbCanton = ((dynamic)cmbProvinciaEntregaDomicilio.SelectedItem).tbCanton;

            cmbCantonEntregaDomicilio.DataSource = _tbCanton;
            cmbCantonEntregaDomicilio.ValueMember = "idCanton";
            cmbCantonEntregaDomicilio.DisplayMember = "descripcion";
        }

        private void SetearProducto(tbProducto _tbProducto)
        {
            txtCodigoProducto.Clear();
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();
            txtPrecio.Tag = null;
            txtPrecio.Clear();
            txtCantidad.Clear();

            if (_tbProducto == null)
                MessageBox.Show("Producto no existe", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (edmCosolemFunctions.isProductoInventariable(_tbProducto.idProducto) && tipoOrdenVenta == "O" && edmCosolemFunctions.getFisicoDisponible(_tbProducto.idProducto) == 0)
                MessageBox.Show("Producto " + _tbProducto.codigoProducto + " no tiene stock suficiente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                tbPrecio _tbPrecio = edmCosolemFunctions.getPrecio(idEmpresa, _tbProducto.idProducto);
                if (_tbPrecio == null || _tbPrecio.precioOferta == 0 || _tbPrecio.precioVentaPublico == 0 || _tbPrecio.precioInformativo == 0) MessageBox.Show("Producto no tiene precio", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    txtPrecio.Tag = _tbPrecio;
                    txtCodigoProducto.Text = _tbProducto.codigoProducto;
                    txtCodigoProducto.Tag = _tbProducto.idProducto;
                    txtDescripcionProducto.Text = _tbProducto.descripcion;
                    decimal precio = rdbEfectivo.Checked ? _tbPrecio.precioOferta : _tbPrecio.precioVentaPublico;
                    txtPrecio.Text = Util.FormatoMoneda(precio, 2);
                    txtCantidad.Text = "1";
                }
            }
        }

        private void txtCodigoProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                DataTable _DataTable = new DataTable();
                _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Marca"), new DataColumn("Línea"), new DataColumn("Grupo"), new DataColumn("SubGrupo"), new DataColumn("Modelo"), new DataColumn("Código de producto"), new DataColumn("Descripción"), new DataColumn("Características"), new DataColumn("Fecha de registro"), new DataColumn("producto", typeof(object)) });
                var Productos = edmCosolemFunctions.getProductos();
                foreach (var Producto in Productos) _DataTable.Rows.Add(Producto.marca, Producto.linea, Producto.grupo, Producto.subgrupo, Producto.modelo, Producto.codigoProducto, Producto.descripcion, Producto.caracteristicas, Producto.fechaRegistro, Producto.producto);
                frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
                if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    SetearProducto((tbProducto)_frmBusqueda._object);
            }
        }

        private void SetearBodega(tbBodega _tbBodega, long idProducto, string codigoProducto, int cantidad)
        {
            txtDescripcionTiendaBodega.Clear();
            txtDescripcionTiendaBodega.Tag = null;

            cantidad += _BindingListtbOrdenVentaDetalle.Where(y => y.idBodega == _tbBodega.idBodega && y.idProducto == idProducto).Sum(z => z.cantidad);

            if (edmCosolemFunctions.isProductoInventariable(idProducto) && tipoOrdenVenta == "O" && edmCosolemFunctions.getFisicoDisponible(idEmpresa, _tbBodega.idBodega, idProducto) < cantidad)
                MessageBox.Show("Producto " + codigoProducto + " no tiene stock suficiente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                txtDescripcionTiendaBodega.Text = _tbBodega.tbTienda.descripcion + " - " + _tbBodega.descripcion;
                txtDescripcionTiendaBodega.Tag = _tbBodega.idBodega;
            }
            btnAgregar.Focus();
        }

        private void txtDescripcionTiendaBodega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                if (txtCodigoProducto.Tag == null || String.IsNullOrEmpty(txtPrecio.Text.Trim()))
                    MessageBox.Show("Seleccione producto", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (String.IsNullOrEmpty(txtCantidad.Text.Trim()))
                    MessageBox.Show("Ingrese cantidad", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    long idProducto = Convert.ToInt64(txtCodigoProducto.Tag);
                    string codigoProducto = txtCodigoProducto.Text;
                    int cantidad = Convert.ToInt32(txtCantidad.Text.Trim());

                    DataTable _DataTable = new DataTable();
                    _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Tienda"), new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Es facturable"), new DataColumn("Disponible"), new DataColumn("Fecha de registro"), new DataColumn("bodega", typeof(object)) });

                    (from B in _dbCosolemEntities.tbBodega
                     join I in _dbCosolemEntities.tbInventario on new { B.idBodega, idProducto } equals new { I.idBodega, I.idProducto }
                     where B.estadoRegistro && I.estadoRegistro
                     select new
                     {
                         empresa = B.tbTienda.tbEmpresa.razonSocial,
                         tienda = B.tbTienda.descripcion,
                         codigoBodega = B.idBodega,
                         descripcion = B.descripcion,
                         esFacturable = B.esFacturable ? "Sí" : "No",
                         disponible = I.fisicoDisponible - I.reservado,
                         fechaRegistro = B.fechaHoraIngreso,
                         bodega = B
                     }).ToList().ForEach(x => _DataTable.Rows.Add(x.empresa, x.tienda, x.codigoBodega, x.descripcion, x.esFacturable, x.disponible, x.fechaRegistro, x.bodega));

                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        long idBodega = Convert.ToInt64(_DataRow[2]);
                        _DataRow[5] = Convert.ToInt64(_DataRow[5]) - _BindingListtbOrdenVentaDetalle.Where(y => y.idBodega == idBodega && y.idProducto == idProducto).Sum(z => z.cantidad);
                    }
                    frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
                    if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        SetearBodega((tbBodega)_frmBusqueda._object, idProducto, codigoProducto, cantidad);
                }
            }
        }

        private void SetearDireccion(tbDireccion _tbDireccion)
        {
            var _tbProvincia = ((dynamic)cmbProvinciaEntregaDomicilio.DataSource)[0];
            cmbProvinciaEntregaDomicilio.SelectedValue = _tbProvincia.idProvincia;
            cmbProvinciaEntregaDomicilio_SelectionChangeCommitted(null, null);
            txtContactoEntregaDomicilio.Clear();
            txtDireccionEntregaDomicilio.Clear();
            txtReferenciaEntregaDomicilio.Clear();

            cmbProvinciaEntregaDomicilio.SelectedValue = _tbDireccion.tbCanton.idProvincia;
            cmbProvinciaEntregaDomicilio_SelectionChangeCommitted(null, null);
            cmbCantonEntregaDomicilio.SelectedValue = _tbDireccion.idCanton;
            txtContactoEntregaDomicilio.Text = String.Join(", ", _tbDireccion.tbTelefono.Where(x => x.estadoRegistro).Select(x => x.numero));
            txtDireccionEntregaDomicilio.Text = _tbDireccion.direccionCompleta;
            txtReferenciaEntregaDomicilio.Text = _tbDireccion.referencia;
        }

        private void txtDireccionEntregaDomicilio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                DataTable _DataTable = new DataTable();
                _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Provincia"), new DataColumn("Cantón"), new DataColumn("Tipo de dirección"), new DataColumn("Dirección completa"), new DataColumn("Referencia"), new DataColumn("Es principal"), new DataColumn("direccion", typeof(object)) });
                long idPersona = 0;
                if (txtNumeroIdentificacion.Tag != null) idPersona = Convert.ToInt64(txtNumeroIdentificacion.Tag);

                (from P in _dbCosolemEntities.tbPersona
                 join D in _dbCosolemEntities.tbDireccion on P.idPersona equals D.idPersona
                 join TD in _dbCosolemEntities.tbTipoDireccion on D.idTipoDireccion equals TD.idTipoDireccion
                 from C in _dbCosolemEntities.tbCliente.Where(x => x.idCliente == P.idPersona && x.estadoRegistro == P.estadoRegistro).DefaultIfEmpty()
                 where P.idPersona == idPersona && P.estadoRegistro
                 select new
                 {
                     provincia = D.tbCanton.tbProvincia.descripcion,
                     canton = D.tbCanton.descripcion,
                     tipoDireccion = TD.descripcion,
                     direccionCompleta = D.direccionCompleta,
                     referencia = D.referencia,
                     esPrincipal = D.esPrincipal ? "Sí" : "No",
                     direccion = D
                 }).ToList().ForEach(x => _DataTable.Rows.Add(x.provincia, x.canton, x.tipoDireccion, x.direccionCompleta, x.referencia, x.esPrincipal, x.direccion));

                frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
                if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    SetearDireccion((tbDireccion)_frmBusqueda._object);
            }
        }

        private void btnLimpiarCliente_Click(object sender, EventArgs e)
        {
            cmbTipoPersona.SelectedValue = ((List<TipoPersona>)cmbTipoPersona.DataSource).FirstOrDefault().idTipoPersona;
            cmbTipoPersona_SelectionChangeCommitted(null, null);
            txtNumeroIdentificacion.Tag = null;
            txtPrimerNombre.Clear();
            txtSegundoNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
            txtRazonSocial.Clear();
            txtCorreoElectronico.Clear();
            cmbProvinciaCliente.SelectedValue = ((dynamic)cmbProvinciaCliente.DataSource)[0].idProvincia;
            cmbProvinciaCliente_SelectionChangeCommitted(null, null);
            txtConvencional.Tag = null;
            txtConvencional.Clear();
            txtCelular.Tag = null;
            txtCelular.Clear();
            txtDireccionCliente.Tag = null;
            txtDireccionCliente.Clear();
            cmbTipoPersona.Enabled = true;
            cmbTipoIdentificacion.Enabled = true;
            txtNumeroIdentificacion.Enabled = true;
            txtNumeroIdentificacion.Select();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                long idProducto = 0;
                long.TryParse((txtCodigoProducto.Tag == null ? String.Empty : txtCodigoProducto.Tag.ToString()), out idProducto);
                string codigoProducto = String.Empty;
                int cantidad = 0;

                long? idBodega = null;

                string mensaje = String.Empty;

                if (_BindingListtbOrdenVentaDetalle.Count >= 11) mensaje += "Ya no se pueden ingresar más productos\n";
                if (idProducto == 0 || String.IsNullOrEmpty(txtPrecio.Text.Trim())) mensaje += "Seleccione producto\n";
                tbPrecio _tbPrecio = txtPrecio.Tag != null ? (tbPrecio)txtPrecio.Tag : null;
                if (_tbPrecio == null || _tbPrecio.precioOferta == 0 || _tbPrecio.precioVentaPublico == 0 || _tbPrecio.precioInformativo == 0) mensaje += "Producto no tiene precio\n";
                if (String.IsNullOrEmpty(txtCantidad.Text.Trim()) || (!String.IsNullOrEmpty(txtCantidad.Text.Trim()) && Convert.ToInt32(txtCantidad.Text.Trim()) <= 0)) mensaje += "Ingrese cantidad\n";
                if (idProducto != 0 && edmCosolemFunctions.isProductoInventariable(idProducto) && tipoOrdenVenta == "O" && txtDescripcionTiendaBodega.Tag == null) mensaje += "Seleccione bodega\n";
                if (idProducto != 0 && edmCosolemFunctions.isProductoInventariable(idProducto) && tipoOrdenVenta == "O" && txtDescripcionTiendaBodega.Tag != null && !String.IsNullOrEmpty(txtCantidad.Text.Trim()) && Convert.ToInt32(txtCantidad.Text.Trim()) > 0)
                {
                    idBodega = Convert.ToInt64(txtDescripcionTiendaBodega.Tag);
                    cantidad = _BindingListtbOrdenVentaDetalle.Where(y => y.idBodega == idBodega && y.idProducto == idProducto).Sum(z => z.cantidad) + Convert.ToInt32(txtCantidad.Text.Trim());

                    codigoProducto = txtCodigoProducto.Text;
                    if (edmCosolemFunctions.isProductoInventariable(idProducto) && tipoOrdenVenta == "O" && edmCosolemFunctions.getFisicoDisponible(idEmpresa, idBodega ?? 0, idProducto) < cantidad)
                        mensaje += "Producto " + codigoProducto + " no tiene stock suficiente";
                }

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    codigoProducto = txtCodigoProducto.Text;
                    decimal costo = _tbPrecio.costo;
                    decimal precioOferta = _tbPrecio.precioOferta;
                    decimal precioVentaPublico = _tbPrecio.precioVentaPublico;
                    decimal precioInformativo = _tbPrecio.precioInformativo;
                    decimal precio = Decimal.Parse(txtPrecio.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
                    cantidad = Convert.ToInt32(txtCantidad.Text.Trim());
                    if (edmCosolemFunctions.isProductoInventariable(idProducto) && tipoOrdenVenta == "O") idBodega = Convert.ToInt64(txtDescripcionTiendaBodega.Tag);

                    tbOrdenVentaDetalle _tbOrdenVentaDetalle = new tbOrdenVentaDetalle { idProducto = idProducto, codigoProducto = codigoProducto, descripcionProducto = codigoProducto + " - " + txtDescripcionProducto.Text.Trim(), costo = costo, precioOferta = precioOferta, precioVentaPublico = precioVentaPublico, precioInformativo = precioInformativo, idBodega = idBodega, descripcionBodegaSalida = txtDescripcionTiendaBodega.Text.Trim(), estadoRegistro = true };
                    _tbOrdenVentaDetalle.precio = precioInformativo;
                    _tbOrdenVentaDetalle.cantidad = cantidad;
                    _tbOrdenVentaDetalle.subTotal = Math.Round(precioInformativo * cantidad, 2);
                    if (tipoVenta == "N") _tbOrdenVentaDetalle.descuento = Math.Round(_tbOrdenVentaDetalle.subTotal - (precio * cantidad), 2);
                    if (tipoVenta == "P") _tbOrdenVentaDetalle.descuento = Math.Round(_tbOrdenVentaDetalle.subTotal - (costo * cantidad), 2);
                    _tbOrdenVentaDetalle.subTotalBruto = Math.Round(_tbOrdenVentaDetalle.subTotal - _tbOrdenVentaDetalle.descuento, 2);
                    decimal porcentajeIVA = edmCosolemFunctions.getPorcentajeIVA(idProducto, idTienda);
                    _tbOrdenVentaDetalle.porcentajeIVA = porcentajeIVA * 100M;
                    _tbOrdenVentaDetalle.IVA = Math.Round(_tbOrdenVentaDetalle.subTotalBruto * porcentajeIVA, 2);
                    _tbOrdenVentaDetalle.totalNeto = Math.Round(_tbOrdenVentaDetalle.subTotalBruto + _tbOrdenVentaDetalle.IVA, 2);

                    _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbOrdenVentaDetalle, EntityState.Detached);

                    _BindingListtbOrdenVentaDetalle.Add(_tbOrdenVentaDetalle);
                    CalcularTotales();

                    btnLimpiarProducto_Click(null, null);
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void btnLimpiarProducto_Click(object sender, EventArgs e)
        {
            txtCodigoProducto.Clear();
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            txtDescripcionTiendaBodega.Clear();
            txtDescripcionTiendaBodega.Tag = null;
            txtCodigoProducto.Select();
        }

        private void HabilitarCamposDireccion(bool habilitar)
        {
            var _tbProvincia = ((dynamic)cmbProvinciaEntregaDomicilio.DataSource)[0];
            cmbProvinciaEntregaDomicilio.SelectedValue = _tbProvincia.idProvincia;
            cmbProvinciaEntregaDomicilio_SelectionChangeCommitted(null, null);
            txtDireccionEntregaDomicilio.Clear();
            txtReferenciaEntregaDomicilio.Clear();
            txtContactoEntregaDomicilio.Clear();

            cmbProvinciaEntregaDomicilio.Enabled = habilitar;
            cmbCantonEntregaDomicilio.Enabled = habilitar;
            txtDireccionEntregaDomicilio.Enabled = habilitar;
            txtReferenciaEntregaDomicilio.Enabled = habilitar;
            txtContactoEntregaDomicilio.Enabled = habilitar;
        }

        private void chbEntregaDomicilio_CheckedChanged(object sender, EventArgs e)
        {
            HabilitarCamposDireccion(chbEntregaDomicilio.Checked);
        }

        private void CalcularTotales()
        {
            txtSubtotal.Text = Util.FormatoMoneda(_BindingListtbOrdenVentaDetalle.Sum(x => x.subTotal), 2);
            txtDescuento.Text = Util.FormatoMoneda(_BindingListtbOrdenVentaDetalle.Sum(x => x.descuento), 2);
            txtSubtotalBruto.Text = Util.FormatoMoneda(_BindingListtbOrdenVentaDetalle.Sum(x => x.subTotalBruto), 2);
            txtIVA.Text = Util.FormatoMoneda(_BindingListtbOrdenVentaDetalle.Sum(x => x.IVA), 2);
            txtTotalNeto.Text = Util.FormatoMoneda(_BindingListtbOrdenVentaDetalle.Sum(x => x.totalNeto), 2);

            decimal totalNeto = Decimal.Parse(txtTotalNeto.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);

            tbProductoFinancieroCabecera _tbProductoFinancieroCabecera = (tbProductoFinancieroCabecera)cmbProductoFinanciero.SelectedItem;
            decimal porcentajeCuotaInicialExigible = _tbProductoFinancieroCabecera.porcentajeCuotaInicialExigible / 100M;
            decimal valorExigible = totalNeto * porcentajeCuotaInicialExigible;
            int plazo = _tbProductoFinancieroCabecera.plazo;
            decimal cuota = 0;
            decimal totalFinanciar = totalNeto - valorExigible;

            if (plazo > 0) cuota = totalFinanciar / plazo;
            txtCuota.Text = Util.FormatoMoneda(cuota, 2);
            txtPorcentajeExigible.Text = Util.FormatoPorcentaje(porcentajeCuotaInicialExigible, 2);
            txtValorCuotaInicialExigible.Text = Util.FormatoMoneda(valorExigible, 2);
            txtTotalFinanciar.Text = Util.FormatoMoneda(totalFinanciar, 2);
        }

        private void dgvOrdenVentaDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dicEliminarProducto.Index)
                {
                    if (MessageBox.Show("¿Seguro desea eliminar el producto seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        _BindingListtbOrdenVentaDetalle.Remove((tbOrdenVentaDetalle)dgvOrdenVentaDetalle.CurrentRow.DataBoundItem);
                    CalcularTotales();
                }
            }
        }

        private void txtCodigoProducto_Leave(object sender, EventArgs e)
        {
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();
            txtPrecio.Tag = null;
            txtPrecio.Clear();
            txtCantidad.Clear();

            if (!String.IsNullOrEmpty(txtCodigoProducto.Text.Trim()))
            {
                txtDescripcionTiendaBodega.Clear();
                txtDescripcionTiendaBodega.Tag = null;

                var _tbProducto = edmCosolemFunctions.getProductos(txtCodigoProducto.Text.Trim());
                tbProducto producto = _tbProducto.Count == 0 ? null : _tbProducto[0].producto;
                SetearProducto(producto);
            }
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = String.Empty;

                if (String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) mensaje += "Ingrese número de identificación\n";
                TipoPersona _tbTipoPersona = (TipoPersona)cmbTipoPersona.SelectedItem;
                if (_tbTipoPersona.idTipoPersona == 1)
                {
                    if (String.IsNullOrEmpty(txtPrimerNombre.Text.Trim())) mensaje += "Ingrese primer nombre\n";
                    if (String.IsNullOrEmpty(txtApellidoPaterno.Text.Trim())) mensaje += "Ingrese apellido paterno\n";
                    if (String.IsNullOrEmpty(txtApellidoMaterno.Text.Trim())) mensaje += "Ingrese apellido materno\n";
                }
                if (_tbTipoPersona.idTipoPersona == 2) if (String.IsNullOrEmpty(txtRazonSocial.Text.Trim())) mensaje += "Ingrese razón social\n";
                if (String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) mensaje += "Ingrese correo electrónico\n";
                if (!String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) if (!Util.ValidaEmail(txtCorreoElectronico.Text.Trim())) mensaje += "Correo electrónico inválido, favor verificar\n";
                if (String.IsNullOrEmpty(txtDireccionCliente.Text.Trim())) mensaje += "Ingrese dirección\n";
                if (String.IsNullOrEmpty(txtConvencional.Text.Trim()) && String.IsNullOrEmpty(txtCelular.Text.Trim())) mensaje += "Ingrese al menos 1 teléfono\n";
                if (rdbCredito.Checked && ((tbProductoFinancieroCabecera)cmbProductoFinanciero.SelectedItem).idProductoFinancieroCabecera == 0) mensaje += "Seleccione un producto financiero ya que la forma de pago es crédito\n";
                if (_BindingListtbOrdenVentaDetalle.Count == 0) mensaje += "Ingrese al menos 1 producto\n";
                if (tipoOrdenVenta == "O" && _BindingListtbOrdenVentaDetalle.Where(x => edmCosolemFunctions.isProductoInventariable(x.idProducto) && !x.idBodega.HasValue).Count() > 0) mensaje += "Seleccione bodega para el detalle de productos\n";
                if (_BindingListtbOrdenVentaDetalle.Where(x => edmCosolemFunctions.isProductoInventariable(x.idProducto) && !x.idBodega.HasValue).Count() == 0)
                {
                    _BindingListtbOrdenVentaDetalle.Select(x => new { x.idBodega, x.idProducto, x.codigoProducto }).Distinct().ToList().ForEach(x =>
                    {
                        int cantidad = _BindingListtbOrdenVentaDetalle.Where(y => y.idBodega == x.idBodega && y.idProducto == x.idProducto).Sum(z => z.cantidad);
                        if (edmCosolemFunctions.isProductoInventariable(x.idProducto) && tipoOrdenVenta == "O" && edmCosolemFunctions.getFisicoDisponible(idEmpresa, x.idBodega ?? 0, x.idProducto) < cantidad)
                            mensaje += "Producto " + x.codigoProducto + " no tiene stock suficiente\n";
                    });
                }
                if (chbEntregaDomicilio.Checked)
                {
                    if (String.IsNullOrEmpty(txtDireccionEntregaDomicilio.Text.Trim())) mensaje += "Ingrese dirección de entrega\n";
                    if (String.IsNullOrEmpty(txtReferenciaEntregaDomicilio.Text.Trim())) mensaje += "Ingrese referencia de entrega\n";
                    if (String.IsNullOrEmpty(txtContactoEntregaDomicilio.Text.Trim())) mensaje += "Ingrese contacto de entrega\n";
                }

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    tbPersona persona = null;

                    long idPersona = 0;
                    if (txtNumeroIdentificacion.Tag != null)
                    {
                        idPersona = Convert.ToInt64(txtNumeroIdentificacion.Tag);

                        persona = (from P in _dbCosolemEntities.tbPersona where P.idPersona == idPersona select P).FirstOrDefault();
                    }
                    else
                    {
                        int idTipoPersona = ((TipoPersona)cmbTipoPersona.SelectedItem).idTipoPersona;
                        int idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                        string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();

                        persona = (from P in _dbCosolemEntities.tbPersona where P.idTipoPersona == idTipoPersona && P.idTipoIdentificacion == idTipoIdentificacion && P.numeroIdentificacion == numeroIdentificacion select P).FirstOrDefault();
                    }

                    if (persona == null)
                    {
                        persona = new tbPersona
                        {
                            idTipoPersona = ((TipoPersona)cmbTipoPersona.SelectedItem).idTipoPersona,
                            idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion,
                            numeroIdentificacion = txtNumeroIdentificacion.Text.Trim(),
                            primerNombre = txtPrimerNombre.Text.Trim(),
                            segundoNombre = txtSegundoNombre.Text.Trim(),
                            apellidoPaterno = txtApellidoPaterno.Text.Trim(),
                            apellidoMaterno = txtApellidoMaterno.Text.Trim(),
                            razonSocial = txtRazonSocial.Text.Trim(),
                            idSexo = 3,
                            idEstadoCivil = 6,
                            correoElectronico = txtCorreoElectronico.Text.Trim(),
                            estadoRegistro = true,
                            fechaHoraIngreso = Program.fechaHora,
                            idUsuarioIngreso = idUsuario,
                            terminalIngreso = Program.terminal
                        };
                        persona.tbDireccion.Add(new tbDireccion
                        {
                            idCanton = ((dynamic)cmbCantonCliente.SelectedItem).idCanton,
                            idTipoDireccion = 1,
                            direccionCompleta = txtDireccionCliente.Text,
                            esPrincipal = true,
                            estadoRegistro = true,
                            fechaHoraIngreso = Program.fechaHora,
                            idUsuarioIngreso = idUsuario,
                            terminalIngreso = Program.terminal
                        });
                        if (!String.IsNullOrEmpty(txtConvencional.Text.Trim()))
                        {
                            persona.tbDireccion.FirstOrDefault().tbTelefono.Add(new tbTelefono
                            {
                                idTipoTelefono = 1,
                                idOperadora = 1,
                                numero = txtConvencional.Text.Trim(),
                                esPrincipal = false,
                                estadoRegistro = true,
                                fechaHoraIngreso = Program.fechaHora,
                                idUsuarioIngreso = idUsuario,
                                terminalIngreso = Program.terminal
                            });
                        }
                        if (!String.IsNullOrEmpty(txtCelular.Text.Trim()))
                        {
                            persona.tbDireccion.FirstOrDefault().tbTelefono.Add(new tbTelefono
                            {
                                idTipoTelefono = 2,
                                idOperadora = 1,
                                numero = txtCelular.Text.Trim(),
                                esPrincipal = false,
                                estadoRegistro = true,
                                fechaHoraIngreso = Program.fechaHora,
                                idUsuarioIngreso = idUsuario,
                                terminalIngreso = Program.terminal
                            });
                        }
                        _dbCosolemEntities.tbPersona.AddObject(persona);
                        _dbCosolemEntities.SaveChanges();
                    }
                    else
                    {
                        persona.primerNombre = txtPrimerNombre.Text.Trim();
                        persona.segundoNombre = txtSegundoNombre.Text.Trim();
                        persona.apellidoPaterno = txtApellidoPaterno.Text.Trim();
                        persona.apellidoMaterno = txtApellidoMaterno.Text.Trim();
                        persona.razonSocial = txtRazonSocial.Text.Trim();
                        persona.correoElectronico = txtCorreoElectronico.Text.Trim();
                        persona.fechaHoraUltimaModificacion = Program.fechaHora;
                        persona.idUsuarioUltimaModificacion = idUsuario;
                        persona.terminalUltimaModificacion = Program.terminal;
                        long idDireccion = 0;
                        if (txtDireccionCliente.Tag != null) idDireccion = Convert.ToInt64(txtDireccionCliente.Tag);
                        tbDireccion _tbDireccion = persona.tbDireccion.Where(x => x.idDireccion == idDireccion).FirstOrDefault();
                        if (_tbDireccion == null)
                        {
                            _tbDireccion = new tbDireccion();
                            _tbDireccion.idCanton = ((dynamic)cmbCantonCliente.SelectedItem).idCanton;
                            _tbDireccion.idTipoDireccion = 1;
                            _tbDireccion.direccionCompleta = txtDireccionCliente.Text;
                            _tbDireccion.esPrincipal = true;
                            _tbDireccion.estadoRegistro = true;
                            _tbDireccion.fechaHoraIngreso = Program.fechaHora;
                            _tbDireccion.idUsuarioIngreso = idUsuario;
                            _tbDireccion.terminalIngreso = Program.terminal;
                        }
                        else
                        {
                            _tbDireccion.idCanton = ((dynamic)cmbCantonCliente.SelectedItem).idCanton;
                            _tbDireccion.direccionCompleta = txtDireccionCliente.Text;
                            _tbDireccion.fechaHoraUltimaModificacion = Program.fechaHora;
                            _tbDireccion.idUsuarioUltimaModificacion = idUsuario;
                            _tbDireccion.terminalUltimaModificacion = Program.terminal;
                        }
                        if (!String.IsNullOrEmpty(txtConvencional.Text.Trim()))
                        {
                            long idTelefonoConvencional = 0;
                            if (txtConvencional.Tag != null) idTelefonoConvencional = Convert.ToInt64(txtConvencional.Tag);
                            tbTelefono _tbTelefonoConvencional = _tbDireccion.tbTelefono.Where(x => x.idTelefono == idTelefonoConvencional).FirstOrDefault();
                            if (_tbTelefonoConvencional == null)
                            {
                                _tbTelefonoConvencional = new tbTelefono();
                                _tbTelefonoConvencional.idTipoTelefono = 1;
                                _tbTelefonoConvencional.idOperadora = 1;
                                _tbTelefonoConvencional.numero = txtConvencional.Text.Trim();
                                _tbTelefonoConvencional.esPrincipal = false;
                                _tbTelefonoConvencional.estadoRegistro = true;
                                _tbTelefonoConvencional.fechaHoraIngreso = Program.fechaHora;
                                _tbTelefonoConvencional.idUsuarioIngreso = idUsuario;
                                _tbTelefonoConvencional.terminalIngreso = Program.terminal;
                            }
                            else
                            {
                                _tbTelefonoConvencional.numero = txtConvencional.Text.Trim();
                                _tbTelefonoConvencional.fechaHoraUltimaModificacion = Program.fechaHora;
                                _tbTelefonoConvencional.idUsuarioUltimaModificacion = idUsuario;
                                _tbTelefonoConvencional.terminalUltimaModificacion = Program.terminal;
                            }
                            if (_tbTelefonoConvencional.EntityState == EntityState.Detached) _tbDireccion.tbTelefono.Add(_tbTelefonoConvencional);
                        }
                        if (!String.IsNullOrEmpty(txtCelular.Text.Trim()))
                        {
                            long idTelefonoCelular = 0;
                            if (txtCelular.Tag != null) idTelefonoCelular = Convert.ToInt64(txtCelular.Tag);
                            tbTelefono _tbTelefonoCelular = _tbDireccion.tbTelefono.Where(x => x.idTelefono == idTelefonoCelular).FirstOrDefault();
                            if (_tbTelefonoCelular == null)
                            {
                                _tbTelefonoCelular = new tbTelefono();
                                _tbTelefonoCelular.idTipoTelefono = 2;
                                _tbTelefonoCelular.idOperadora = 1;
                                _tbTelefonoCelular.numero = txtCelular.Text.Trim();
                                _tbTelefonoCelular.esPrincipal = false;
                                _tbTelefonoCelular.estadoRegistro = true;
                                _tbTelefonoCelular.fechaHoraIngreso = Program.fechaHora;
                                _tbTelefonoCelular.idUsuarioIngreso = idUsuario;
                                _tbTelefonoCelular.terminalIngreso = Program.terminal;
                            }
                            else
                            {
                                _tbTelefonoCelular.numero = txtCelular.Text.Trim();
                                _tbTelefonoCelular.fechaHoraUltimaModificacion = Program.fechaHora;
                                _tbTelefonoCelular.idUsuarioUltimaModificacion = idUsuario;
                                _tbTelefonoCelular.terminalUltimaModificacion = Program.terminal;
                            }
                            if (_tbTelefonoCelular.EntityState == EntityState.Detached) _tbDireccion.tbTelefono.Add(_tbTelefonoCelular);
                        }
                        if (_tbDireccion.EntityState == EntityState.Detached) persona.tbDireccion.Add(_tbDireccion);
                    }
                    if (persona.tbCliente == null)
                    {
                        persona.tbCliente = new tbCliente
                        {
                            estadoRegistro = true,
                            fechaHoraIngreso = Program.fechaHora,
                            idUsuarioIngreso = idUsuario,
                            terminalIngreso = Program.terminal
                        };
                    }
                    else
                    {
                        persona.tbCliente.fechaHoraUltimaModificacion = Program.fechaHora;
                        persona.tbCliente.idUsuarioUltimaModificacion = idUsuario;
                        persona.tbCliente.terminalUltimaModificacion = Program.terminal;
                    }

                    tbOrdenVentaCabecera ordenVenta = new tbOrdenVentaCabecera { estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal };

                    if (tipoOrdenVenta == "C" && idOrdenVenta != 0)
                    {
                        ordenVenta = (from OV in _dbCosolemEntities.tbOrdenVentaCabecera where OV.idOrdenVentaCabecera == idOrdenVenta select OV).FirstOrDefault();
                        ordenVenta.fechaHoraUltimaModificacion = Program.fechaHora;
                        ordenVenta.idUsuarioUltimaModificacion = idUsuario;
                        ordenVenta.terminalUltimaModificacion = Program.terminal;
                    }

                    ordenVenta.idEmpresaOrdenVenta = idEmpresa;
                    ordenVenta.idTiendaOrdenVenta = idTienda;
                    ordenVenta.fechaHoraOrdenVenta = Program.fechaHora;
                    ordenVenta.idCliente = persona.tbCliente.idCliente;
                    ordenVenta.tipoIdentificacion = persona.tbTipoIdentificacion.descripcion;
                    ordenVenta.numeroIdentificacion = persona.numeroIdentificacion;
                    ordenVenta.cliente = persona.nombreCompleto;
                    ordenVenta.direccion = txtDireccionCliente.Text;
                    ordenVenta.convencional = (!String.IsNullOrEmpty(txtConvencional.Text.Trim()) ? txtConvencional.Text.Trim() : null);
                    ordenVenta.celular = (!String.IsNullOrEmpty(txtCelular.Text.Trim()) ? txtCelular.Text.Trim() : null);
                    ordenVenta.correoElectronico = persona.correoElectronico;
                    ordenVenta.idFormaPago = rdbEfectivo.Checked ? 1 : 2;
                    if (rdbCredito.Checked)
                    {
                        tbProductoFinancieroCabecera productoFinanciero = (tbProductoFinancieroCabecera)cmbProductoFinanciero.SelectedItem;
                        ordenVenta.idProductoFinanciero = productoFinanciero.idProductoFinancieroCabecera;

                        long idProductoFinanciero = productoFinanciero.idProductoFinancieroCabecera;
                        string frecuenciaPago = productoFinanciero.tbFrecuenciaPago.descripcion;
                        int plazo = productoFinanciero.plazo;
                        int mesesGracia = productoFinanciero.mesesGracia;
                        int cuotasGratis = productoFinanciero.cuotasGratis;
                        decimal cuota = Decimal.Parse(txtCuota.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
                        decimal porcentajeCuotaInicialExigible = productoFinanciero.porcentajeCuotaInicialExigible;
                        decimal valorCuotaInicialExigible = Decimal.Parse(txtValorCuotaInicialExigible.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
                        decimal totalNeto = Decimal.Parse(txtTotalNeto.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
                        decimal totalFinanciar = Decimal.Parse(txtTotalFinanciar.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);

                        if (ordenVenta.tbOrdenVentaFinanciacion.Count == 0)
                            ordenVenta.tbOrdenVentaFinanciacion.Add(new tbOrdenVentaFinanciacion { idProductoFinanciero = idProductoFinanciero, frecuenciaPago = frecuenciaPago, plazo = plazo, mesesGracia = mesesGracia, cuotasGratis = cuotasGratis, cuota = cuota, porcentajeCuotaInicialExigible = porcentajeCuotaInicialExigible, valorCuotaInicialExigible = valorCuotaInicialExigible, totalNeto = totalNeto, totalFinanciar = totalFinanciar, estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });
                        else
                        {
                            tbOrdenVentaFinanciacion financiacion = ordenVenta.tbOrdenVentaFinanciacion.FirstOrDefault();
                            financiacion.idProductoFinanciero = idProductoFinanciero;
                            financiacion.frecuenciaPago = frecuenciaPago;
                            financiacion.plazo = plazo;
                            financiacion.mesesGracia = mesesGracia;
                            financiacion.cuotasGratis = cuotasGratis;
                            financiacion.cuota = cuota;
                            financiacion.porcentajeCuotaInicialExigible = porcentajeCuotaInicialExigible;
                            financiacion.valorCuotaInicialExigible = valorCuotaInicialExigible;
                            financiacion.totalNeto = totalNeto;
                            financiacion.totalFinanciar = totalFinanciar;
                            financiacion.fechaHoraUltimaModificacion = Program.fechaHora;
                            financiacion.idUsuarioUltimaModificacion = idUsuario;
                            financiacion.terminalUltimaModificacion = Program.terminal;
                        }
                    }
                    int? idProvinciaEntregaDomicilio = null;
                    int? idCantonEntregaDomicilio = null;
                    string direccionEntregaDomicilio = null;
                    string referenciaEntregaDomicilio = null;
                    string contactoEntregaDomicilio = null;
                    if (chbEntregaDomicilio.Checked)
                    {
                        idProvinciaEntregaDomicilio = Convert.ToInt32(cmbProvinciaEntregaDomicilio.SelectedValue);
                        idCantonEntregaDomicilio = Convert.ToInt32(cmbCantonEntregaDomicilio.SelectedValue);
                        direccionEntregaDomicilio = txtDireccionEntregaDomicilio.Text.Trim();
                        referenciaEntregaDomicilio = txtReferenciaEntregaDomicilio.Text.Trim();
                        contactoEntregaDomicilio = txtContactoEntregaDomicilio.Text.Trim();
                    }
                    ordenVenta.entregaDomicilio = chbEntregaDomicilio.Checked;
                    ordenVenta.idProvinciaEntregaDomicilio = idProvinciaEntregaDomicilio;
                    ordenVenta.idCantonEntregaDomicilio = idCantonEntregaDomicilio;
                    ordenVenta.direccionEntregaDomicilio = direccionEntregaDomicilio;
                    ordenVenta.referenciaEntregaDomicilio = referenciaEntregaDomicilio;
                    ordenVenta.contactoEntregaDomicilio = contactoEntregaDomicilio;
                    ordenVenta.subTotal = Decimal.Parse(txtSubtotal.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
                    ordenVenta.descuento = Decimal.Parse(txtDescuento.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
                    ordenVenta.subTotalBruto = Decimal.Parse(txtSubtotalBruto.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
                    ordenVenta.IVA = Decimal.Parse(txtIVA.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
                    ordenVenta.totalNeto = Decimal.Parse(txtTotalNeto.Text.Trim(), NumberStyles.Currency, Application.CurrentCulture);
                    ordenVenta.tipoOrdenVenta = tipoOrdenVenta;
                    ordenVenta.tipoVenta = tipoVenta;
                    ordenVenta.idEstadoOrdenVenta = idEstadoOrdenVenta;
                    if (tipoOrdenVenta == "O" && idOrdenVenta > 0) ordenVenta.idCotizacionCabecera = idOrdenVenta;
                    ordenVenta.tbOrdenVentaDetalle.Where(x => x.idOrdenVentaDetalle != 0).ToList().ForEach(y =>
                    {
                        y.estadoRegistro = false;
                        y.fechaHoraUltimaModificacion = Program.fechaHora;
                        y.idUsuarioUltimaModificacion = idUsuario;
                        y.terminalUltimaModificacion = Program.terminal;
                    });
                    _BindingListtbOrdenVentaDetalle.ToList().ForEach(x =>
                    {
                        if (edmCosolemFunctions.isProductoInventariable(x.idProducto) && tipoOrdenVenta == "O")
                        {
                            tbInventario _tbInventario = (from I in _dbCosolemEntities.tbInventario where I.idBodega == x.idBodega && I.idProducto == x.idProducto && x.estadoRegistro select I).FirstOrDefault();
                            _tbInventario.reservado += x.cantidad;
                            _tbInventario.fechaHoraUltimaModificacion = Program.fechaHora;
                            _tbInventario.idUsuarioUltimaModificacion = idUsuario;
                            _tbInventario.terminalUltimaModificacion = Program.terminal;
                        }
                        tbOrdenVentaDetalle detalle = (from OVD in _dbCosolemEntities.tbOrdenVentaDetalle where OVD.idOrdenVentaDetalle == x.idOrdenVentaDetalle select OVD).FirstOrDefault();
                        if (detalle == null)
                        {
                            x.estadoRegistro = true;
                            x.fechaHoraIngreso = Program.fechaHora;
                            x.idUsuarioIngreso = idUsuario;
                            x.terminalIngreso = Program.terminal;
                            ordenVenta.tbOrdenVentaDetalle.Add(x);
                        }
                        else
                        {
                            detalle.idProducto = x.idProducto;
                            detalle.codigoProducto = x.codigoProducto;
                            detalle.descripcionProducto = x.descripcionProducto;
                            detalle.costo = x.costo;
                            detalle.precioOferta = x.precioOferta;
                            detalle.precioVentaPublico = x.precioVentaPublico;
                            detalle.precioInformativo = x.precioInformativo;
                            detalle.idBodega = x.idBodega;
                            detalle.descripcionBodegaSalida = x.descripcionBodegaSalida;
                            detalle.precio = x.precio;
                            detalle.cantidad = x.cantidad;
                            detalle.subTotal = x.subTotal;
                            detalle.descuento = x.descuento;
                            detalle.subTotalBruto = x.subTotalBruto;
                            detalle.porcentajeIVA = x.porcentajeIVA;
                            detalle.IVA = x.IVA;
                            detalle.totalNeto = x.totalNeto;
                            detalle.estadoRegistro = x.estadoRegistro;
                            detalle.fechaHoraUltimaModificacion = Program.fechaHora;
                            detalle.idUsuarioUltimaModificacion = idUsuario;
                            detalle.terminalUltimaModificacion = Program.terminal;
                        }
                    });
                    if (ordenVenta.tbSeguimientoCotizacionCabecera.Count == 0 && tipoOrdenVenta == "C")
                    {
                        tbSeguimientoCotizacionCabecera _tbSeguimientoCotizacionCabecera = new tbSeguimientoCotizacionCabecera { estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal };
                        _tbSeguimientoCotizacionCabecera.lineasCotizacion = edmCosolemFunctions.getLineas(ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro).Select(y => y.idProducto).Distinct().ToList());
                        _tbSeguimientoCotizacionCabecera.idEstadoSeguimientoCotizacion = 1;
                        _tbSeguimientoCotizacionCabecera.tbSeguimientoCotizacionDetalle.Add(new tbSeguimientoCotizacionDetalle { comentarioSeguimiento = "Se creó cotización del cliente " + persona.nombreCompleto, estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });
                        tbSeguimientoCotizacionDetalle _tbSeguimientoCotizacionDetalle = _tbSeguimientoCotizacionCabecera.tbSeguimientoCotizacionDetalle.FirstOrDefault();
                        _tbSeguimientoCotizacionCabecera.fechaHoraUltimaModificacion = _tbSeguimientoCotizacionDetalle.fechaHoraIngreso;
                        _tbSeguimientoCotizacionCabecera.idUsuarioUltimaModificacion = _tbSeguimientoCotizacionDetalle.idUsuarioIngreso;
                        _tbSeguimientoCotizacionCabecera.terminalUltimaModificacion = _tbSeguimientoCotizacionDetalle.terminalIngreso;
                        ordenVenta.tbSeguimientoCotizacionCabecera.Add(_tbSeguimientoCotizacionCabecera);
                    }
                    if (ordenVenta.idOrdenVentaCabecera == 0) _dbCosolemEntities.tbOrdenVentaCabecera.AddObject(ordenVenta);
                    _dbCosolemEntities.SaveChanges();
                    MessageBox.Show((tipoOrdenVenta == "O" ? "Orden de venta" : "Cotización") + " #" + ordenVenta.idOrdenVentaCabecera.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (tipoOrdenVenta == "C" && tipoVenta == "N") new frmReporteCotizacion(ordenVenta.idOrdenVentaCabecera).ShowDialog();
                    frmOrdenVenta_Load(null, null);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            frmOrdenVenta_Load(null, null);
        }

        private void txtConvencional_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            frmBusquedaOrdenVenta _frmBusquedaOrdenVenta = new frmBusquedaOrdenVenta(tipoOrdenVenta, tipoVenta, (tipoOrdenVenta == "C" && tipoVenta == "N").ToString());
            if (tipoVenta == "N") _frmBusquedaOrdenVenta.Text = "Consulta de " + (tipoOrdenVenta == "O" ? "ordenes de venta" : "cotizaciones");
            if (tipoVenta == "P") _frmBusquedaOrdenVenta.Text = "Búsqueda de " + (tipoOrdenVenta == "O" ? "ordenes de venta outsoursing" : "egresos de pedidos outsoursing");
            _frmBusquedaOrdenVenta.ShowDialog();
        }

        private void rdbFormaPago_CheckedChanged(object sender, EventArgs e)
        {
            cmbProductoFinanciero.SelectedValue = ((List<tbProductoFinancieroCabecera>)cmbProductoFinanciero.DataSource).FirstOrDefault().idProductoFinancieroCabecera;
            cmbProductoFinanciero.Enabled = !rdbEfectivo.Checked;

            btnLimpiarProducto_Click(null, null);

            _BindingListtbOrdenVentaDetalle.ToList().ForEach(x =>
            {
                decimal precio = (rdbEfectivo.Checked ? x.precioOferta : x.precioVentaPublico);

                x.precio = x.precioInformativo;
                x.subTotal = x.precioInformativo * x.cantidad;
                if (tipoVenta == "N") x.descuento = x.subTotal - (precio * x.cantidad);
                if (tipoVenta == "P") x.descuento = x.subTotal - (x.costo * x.cantidad);
                x.subTotalBruto = x.subTotal - x.descuento;
                decimal porcentajeIVA = edmCosolemFunctions.getPorcentajeIVA(x.idProducto, idTienda);
                x.porcentajeIVA = porcentajeIVA * 100M;
                x.IVA = x.subTotalBruto * porcentajeIVA;
                x.totalNeto = x.subTotalBruto + x.IVA;
            });

            CalcularTotales();
        }

        private void tsbConsultaInventarioGeneral_Click(object sender, EventArgs e)
        {
            string formaPago = null;
            if (rdbEfectivo.Checked) formaPago = "Efectivo";
            if (rdbCredito.Checked) formaPago = "Crédito";
            frmBusquedaInventario _frmBusquedaInventario = new frmBusquedaInventario("true", idEmpresa.ToString(), formaPago, tipoOrdenVenta);
            _frmBusquedaInventario.Text = "Consulta de inventario";
            if (_frmBusquedaInventario.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbBodega bodega = _frmBusquedaInventario.bodega;
                long idProducto = 0;
                string codigoProducto = _frmBusquedaInventario.codigoProducto;
                int cantidad = 0;

                txtCodigoProducto.Text = codigoProducto;
                txtCodigoProducto_Leave(null, null);
                idProducto = Convert.ToInt64(txtCodigoProducto.Tag);
                cantidad = Convert.ToInt32(txtCantidad.Text.Trim());

                if (bodega != null)
                {
                    txtCodigoProducto.Leave -= new EventHandler(txtCodigoProducto_Leave);
                    SetearBodega(bodega, idProducto, codigoProducto, cantidad);
                    txtCodigoProducto.Leave += new EventHandler(txtCodigoProducto_Leave);
                }
            }
        }

        private void cmbProductoFinanciero_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CalcularTotales();
        }

        private void dgvOrdenVentaDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dtcBodega.Index)
                {
                    tbOrdenVentaDetalle ordenVentaDetalle = (tbOrdenVentaDetalle)dgvOrdenVentaDetalle.CurrentRow.DataBoundItem;
                    long idProducto = ordenVentaDetalle.idProducto;
                    string codigoProducto = ordenVentaDetalle.codigoProducto;
                    int cantidad = ordenVentaDetalle.cantidad;

                    DataTable _DataTable = new DataTable();
                    _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Tienda"), new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Es facturable"), new DataColumn("Disponible"), new DataColumn("Fecha de registro"), new DataColumn("bodega", typeof(object)) });

                    (from B in _dbCosolemEntities.tbBodega
                     join I in _dbCosolemEntities.tbInventario on new { B.idBodega, idProducto } equals new { I.idBodega, I.idProducto }
                     where B.estadoRegistro && I.estadoRegistro
                     select new
                     {
                         empresa = B.tbTienda.tbEmpresa.razonSocial,
                         tienda = B.tbTienda.descripcion,
                         codigoBodega = B.idBodega,
                         descripcion = B.descripcion,
                         esFacturable = B.esFacturable ? "Sí" : "No",
                         disponible = I.fisicoDisponible - I.reservado,
                         fechaRegistro = B.fechaHoraIngreso,
                         bodega = B
                     }).Where(x => x.disponible > 0).ToList().ForEach(y => _DataTable.Rows.Add(y.empresa, y.tienda, y.codigoBodega, y.descripcion, y.esFacturable, y.disponible, y.fechaRegistro, y.bodega));

                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        long idBodega = Convert.ToInt64(_DataRow[2]);
                        _DataRow[5] = Convert.ToInt64(_DataRow[5]) - _BindingListtbOrdenVentaDetalle.Where(y => y.idBodega == idBodega && y.idProducto == idProducto).Sum(z => z.cantidad);
                    }
                    if (edmCosolemFunctions.isProductoInventariable(idProducto) && _DataTable.Rows.Count == 0)
                        MessageBox.Show("Producto " + codigoProducto + " no tiene stock suficiente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
                        if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            tbBodega bodega = (tbBodega)_frmBusqueda._object;

                            cantidad += _BindingListtbOrdenVentaDetalle.Where(y => y.idBodega == bodega.idBodega && y.idProducto == idProducto).Sum(z => z.cantidad);

                            if (edmCosolemFunctions.isProductoInventariable(idProducto) && tipoOrdenVenta == "O" && edmCosolemFunctions.getFisicoDisponible(idEmpresa, bodega.idBodega, idProducto) < cantidad)
                                MessageBox.Show("Producto " + codigoProducto + " no tiene stock suficiente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                            {
                                ordenVentaDetalle.idBodega = bodega.idBodega;
                                ordenVentaDetalle.descripcionBodegaSalida = bodega.tbTienda.descripcion + " - " + bodega.descripcion;
                            }
                        }
                    }
                }
            }
        }

        private void btnLimpiarEntregaDomicilio_Click(object sender, EventArgs e)
        {
            chbEntregaDomicilio.Checked = false;
            HabilitarCamposDireccion(false);
        }

        private void dgvOrdenVentaDetalle_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl _DataGridViewTextBoxEditingControl = (DataGridViewTextBoxEditingControl)e.Control;
            _DataGridViewTextBoxEditingControl.KeyPress += new KeyPressEventHandler(txtCantidad_KeyPress);
            e.Control.KeyPress += new KeyPressEventHandler(txtCantidad_KeyPress);
        }

        private void dgvOrdenVentaDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tbOrdenVentaDetalle ordenVentaDetalle = (tbOrdenVentaDetalle)dgvOrdenVentaDetalle.CurrentRow.DataBoundItem;
            long idProducto = ordenVentaDetalle.idProducto;

            if (ordenVentaDetalle.cantidad <= 0) ordenVentaDetalle.cantidad = 1;

            if (edmCosolemFunctions.isProductoInventariable(idProducto) && tipoOrdenVenta == "O")
            {
                string codigoProducto = ordenVentaDetalle.codigoProducto;
                long? idBodega = ordenVentaDetalle.idBodega;

                if (edmCosolemFunctions.getFisicoDisponible(idEmpresa, idBodega ?? 0, idProducto) < ordenVentaDetalle.cantidad)
                {
                    MessageBox.Show("Producto " + codigoProducto + " no tiene stock suficiente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ordenVentaDetalle.cantidad = 1;
                }
            }

            ordenVentaDetalle.subTotal = Math.Round(ordenVentaDetalle.precioInformativo * ordenVentaDetalle.cantidad, 2);
            if (tipoVenta == "N") ordenVentaDetalle.descuento = Math.Round(ordenVentaDetalle.subTotal - ((rdbEfectivo.Checked ? ordenVentaDetalle.precioOferta : ordenVentaDetalle.precioVentaPublico) * ordenVentaDetalle.cantidad), 2);
            if (tipoVenta == "P") ordenVentaDetalle.descuento = Math.Round(ordenVentaDetalle.subTotal - (ordenVentaDetalle.costo * ordenVentaDetalle.cantidad), 2);
            ordenVentaDetalle.subTotalBruto = Math.Round(ordenVentaDetalle.subTotal - ordenVentaDetalle.descuento, 2);
            decimal porcentajeIVA = edmCosolemFunctions.getPorcentajeIVA(idProducto, idTienda);
            ordenVentaDetalle.porcentajeIVA = porcentajeIVA * 100M;
            ordenVentaDetalle.IVA = Math.Round(ordenVentaDetalle.subTotalBruto * porcentajeIVA, 2);
            ordenVentaDetalle.totalNeto = Math.Round(ordenVentaDetalle.subTotalBruto + ordenVentaDetalle.IVA, 2);

            dgvOrdenVentaDetalle.CommitEdit(DataGridViewDataErrorContexts.Commit);

            CalcularTotales();
        }

        private void dgvOrdenVentaDetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is FormatException) return;
        }

        private void txtDireccionCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                DataTable _DataTable = new DataTable();
                _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Tipo de dirección"), new DataColumn("Dirección"), new DataColumn("direccion", typeof(object)) });

                long idPersona = 0;
                if (txtNumeroIdentificacion.Tag != null)
                    idPersona = Convert.ToInt64(txtNumeroIdentificacion.Tag);

                (from D in _dbCosolemEntities.tbDireccion
                 where D.idPersona == idPersona
                 select new
                 {
                     tipoDireccion = D.tbTipoDireccion.descripcion,
                     direccionCompleta = D.direccionCompleta,
                     direccion = D
                 }).ToList().ForEach(x => _DataTable.Rows.Add(x.tipoDireccion, x.direccionCompleta, x.direccion));

                frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
                if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbDireccion direccion = (tbDireccion)_frmBusqueda._object;
                    txtDireccionCliente.Tag = direccion.idDireccion;
                    txtDireccionCliente.Text = direccion.direccionCompleta;
                }
            }
        }
    }
}