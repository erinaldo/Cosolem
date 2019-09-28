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
    public partial class frmOrdenPedido : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;

        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idEmpleado = Program.tbUsuario.tbEmpleado.idEmpleado;
        long idUsuario = Program.tbUsuario.idUsuario;
        tbOrdenPedidoCabecera ordenPedido = null;
        BindingList<tbOrdenPedidoDetalle> ordenPedidoDetalle = null;

        private void InactivarRegistros()
        {
            CurrencyManager _CurrencyManager = (CurrencyManager)BindingContext[dgvOrdenPedidoDetalle.DataSource];
            _CurrencyManager.SuspendBinding();
            foreach (DataGridViewRow _DataGridViewRow in dgvOrdenPedidoDetalle.Rows)
            {
                tbOrdenPedidoDetalle ordenPedidoDetalle = (tbOrdenPedidoDetalle)_DataGridViewRow.DataBoundItem;
                _DataGridViewRow.Visible = ordenPedidoDetalle.estadoRegistro;
            }
            _CurrencyManager.ResumeBinding();
        }

        private void SetearProducto(tbProducto _tbProducto)
        {
            txtCodigoProducto.Clear();
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();
            txtCantidad.Clear();

            if (_tbProducto == null)
                MessageBox.Show("Producto no existe", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                txtCodigoProducto.Text = _tbProducto.codigoProducto;
                txtCodigoProducto.Tag = _tbProducto.idProducto;
                txtDescripcionProducto.Text = _tbProducto.descripcion;
                txtCantidad.Text = "1";
            }
        }

        private void SetearOrdenPedido(tbOrdenPedidoCabecera ordenPedido)
        {
            try
            {
                this.ordenPedido = ordenPedido;
                txtSolicita.Text = this.ordenPedido.tbEmpleado.tbPersona.nombreCompleto;
                txtMotivo.Text = this.ordenPedido.motivo;
                txtFechaHoraIngreso.Text = this.ordenPedido.fechaHoraIngreso.ToString("dd/MM/yyyy - HH:mm:ss");
                txtFechaHoraUltimaModificacion.Clear();
                if (this.ordenPedido.fechaHoraUltimaModificacion.HasValue) txtFechaHoraUltimaModificacion.Text = this.ordenPedido.fechaHoraUltimaModificacion.Value.ToString("dd/MM/yyyy - HH:mm:ss");
                txtUsuarioUltimaModificacion.Text = edmCosolemFunctions.getNombreUsuario(this.ordenPedido.idUsuarioUltimaModificacion ?? 0);
                btnLimpiar_Click(null, null);

                ordenPedidoDetalle.Clear();
                this.ordenPedido.tbOrdenPedidoDetalle.ToList().ForEach(x =>
                {
                    x.descripcionProducto = x.tbProducto.codigoProducto + " - " + x.tbProducto.descripcion;
                    x.fisicoDisponible = edmCosolemFunctions.getFisicoDisponible(idEmpresa, x.idProducto);
                    ordenPedidoDetalle.Add(x);
                });
                InactivarRegistros();
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        public frmOrdenPedido()
        {
            InitializeComponent();
        }

        private void frmOrdenPedido_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvOrdenPedidoDetalle.AutoGenerateColumns = false;

            ordenPedido = new tbOrdenPedidoCabecera { idEmpresa = idEmpresa, idEmpleado = idEmpleado, estadoRegistro = true, tbOrdenPedidoDetalle = new System.Data.Objects.DataClasses.EntityCollection<tbOrdenPedidoDetalle> { } };
            ordenPedidoDetalle = new BindingList<tbOrdenPedidoDetalle>(ordenPedido.tbOrdenPedidoDetalle.ToList());
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(ordenPedido, EntityState.Detached);

            txtSolicita.Text = Program.tbUsuario.tbEmpleado.tbPersona.nombreCompleto;
            txtFechaHoraIngreso.Clear();
            txtMotivo.Clear();
            
            List<tbProveedor> proveedores = (from P in _dbCosolemEntities.tbProveedor where P.estadoRegistro select P).ToList();
            proveedores.Insert(0, new tbProveedor { idProveedor = 0, nombres = "Seleccione" });
            cmbProveedor.DataSource = proveedores;
            cmbProveedor.ValueMember = "idProveedor";
            cmbProveedor.DisplayMember = "nombres";

            txtFechaHoraUltimaModificacion.Clear();
            txtUsuarioUltimaModificacion.Clear();
            btnLimpiar_Click(null, null);

            dgvOrdenPedidoDetalle.DataSource = ordenPedidoDetalle;
            InactivarRegistros();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            
            long cantidad = 0;
            long.TryParse(txtCantidad.Text.Trim(), out cantidad);

            if (txtCodigoProducto.Tag == null) mensaje += "Seleccione producto\n";
            if (cantidad == 0) mensaje += "Ingrese cantidad\n";

            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                long idProducto = Convert.ToInt64(txtCodigoProducto.Tag);
                string descripcionProducto = txtCodigoProducto.Text + " - " + txtDescripcionProducto.Text.Trim();
                long fisicoDisponible = edmCosolemFunctions.getFisicoDisponible(idEmpresa, idProducto);

                ordenPedidoDetalle.Add(new tbOrdenPedidoDetalle { idProducto = idProducto, descripcionProducto = descripcionProducto, fisicoDisponible = fisicoDisponible, cantidad = cantidad, estadoRegistro = true });
                InactivarRegistros();
                btnLimpiar_Click(null, null);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtCodigoProducto_Leave(object sender, EventArgs e)
        {
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();
            txtCantidad.Clear();

            if (!String.IsNullOrEmpty(txtCodigoProducto.Text.Trim()))
            {
                var _tbProducto = edmCosolemFunctions.getProductos(txtCodigoProducto.Text.Trim());
                tbProducto producto = _tbProducto.Count == 0 ? null : _tbProducto[0].producto;
                SetearProducto(producto);
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCodigoProducto.Clear();
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();
            txtCantidad.Clear();
            txtCodigoProducto.Select();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            dgvOrdenPedidoDetalle_CellEndEdit(null, null);

            string mensaje = String.Empty;

            if (String.IsNullOrEmpty(txtSolicita.Text.Trim())) mensaje += "No hay datos de quien solicita la orden de pedido";
            if (String.IsNullOrEmpty(txtMotivo.Text.Trim())) mensaje += "Ingrese un motivo";
            if (ordenPedidoDetalle.Where(x => x.estadoRegistro).Count() == 0) mensaje += "Ingrese al menos 1 producto\n";

            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                ordenPedido.motivo = txtMotivo.Text.Trim();
                ordenPedidoDetalle.Where(x => x.idOrdenPedidoDetalle == 0).ToList().ForEach(y =>
                {
                    y.fechaHoraIngreso = Program.fechaHora;
                    y.idUsuarioIngreso = idUsuario;
                    y.terminalIngreso = Program.terminal;
                    ordenPedido.tbOrdenPedidoDetalle.Add(y);
                });

                if (ordenPedido.EntityState == EntityState.Detached)
                {
                    ordenPedido.fechaHoraIngreso = Program.fechaHora;
                    ordenPedido.idUsuarioIngreso = idUsuario;
                    ordenPedido.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbOrdenPedidoCabecera.AddObject(ordenPedido);
                }
                else
                {
                    ordenPedido.fechaHoraUltimaModificacion = Program.fechaHora;
                    ordenPedido.idUsuarioUltimaModificacion = idUsuario;
                    ordenPedido.terminalUltimaModificacion = Program.terminal;
                }

                _dbCosolemEntities.SaveChanges();
                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmOrdenPedido_Load(null, null);
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
                SetearOrdenPedido((tbOrdenPedidoCabecera)_frmBusqueda._object);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmOrdenPedido_Load(null, null);
        }

        private void dgvOrdenPedidoDetalle_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl _DataGridViewTextBoxEditingControl = (DataGridViewTextBoxEditingControl)e.Control;
            _DataGridViewTextBoxEditingControl.KeyPress += new KeyPressEventHandler(txtCantidad_KeyPress);
            e.Control.KeyPress += new KeyPressEventHandler(txtCantidad_KeyPress);
        }

        private void dgvOrdenPedidoDetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is FormatException) return;
        }

        private void dgvOrdenPedidoDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrdenPedidoDetalle.CommitEdit(DataGridViewDataErrorContexts.Commit);
            foreach (DataGridViewRow _DataGridViewRow in dgvOrdenPedidoDetalle.Rows)
            {
                tbOrdenPedidoDetalle ordenPedidoDetalle = (tbOrdenPedidoDetalle)_DataGridViewRow.DataBoundItem;
                if (ordenPedidoDetalle.EntityState == EntityState.Modified)
                {
                    ordenPedidoDetalle.fechaHoraUltimaModificacion = Program.fechaHora;
                    ordenPedidoDetalle.idUsuarioUltimaModificacion = idUsuario;
                    ordenPedidoDetalle.terminalUltimaModificacion = Program.terminal;
                }
            }
            InactivarRegistros();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (ordenPedido.idOrdenPedidoCabecera == 0) MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                ordenPedido.estadoRegistro = false;
                ordenPedido.fechaHoraUltimaModificacion = Program.fechaHora;
                ordenPedido.idUsuarioUltimaModificacion = idUsuario;
                ordenPedido.terminalUltimaModificacion = Program.terminal;
                ordenPedido.fechaHoraEliminacion = Program.fechaHora;
                ordenPedido.idUsuarioEliminacion = idUsuario;
                ordenPedido.terminalEliminacion = Program.terminal;

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmOrdenPedido_Load(null, null);
            }
        }

        private void dgvOrdenPedidoDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dicEliminarProducto.Index)
                {
                    if (MessageBox.Show("¿Seguro desea eliminar el registro seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        tbOrdenPedidoDetalle ordenPedidoDetalle = (tbOrdenPedidoDetalle)dgvOrdenPedidoDetalle.CurrentRow.DataBoundItem;
                        ordenPedidoDetalle.estadoRegistro = false;
                        ordenPedidoDetalle.fechaHoraUltimaModificacion = Program.fechaHora;
                        ordenPedidoDetalle.idUsuarioUltimaModificacion = idUsuario;
                        ordenPedidoDetalle.terminalUltimaModificacion = Program.terminal;
                        ordenPedidoDetalle.fechaHoraEliminacion = Program.fechaHora;
                        ordenPedidoDetalle.idUsuarioEliminacion = idUsuario;
                        ordenPedidoDetalle.terminalEliminacion = Program.terminal;
                        if (ordenPedidoDetalle.idOrdenPedidoDetalle == 0) this.ordenPedidoDetalle.Remove(ordenPedidoDetalle);
                        InactivarRegistros();
                    }
                }
            }
        }

        private void tsbGenerar_Click(object sender, EventArgs e)
        {
            tbProveedor proveedor = (tbProveedor)cmbProveedor.SelectedItem;
            if (ordenPedido.idOrdenPedidoCabecera == 0) MessageBox.Show("Seleccione un registro para poder generar la orden de pedido", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (proveedor.idProveedor == 0) MessageBox.Show("Seleccione un proveedor para poder generar la orden de pedido", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (ordenPedidoDetalle.Where(x => x.estadoRegistro).Count() == 0) MessageBox.Show("Ingrese al menos 1 producto para poder generar la orden de pedido", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

            }
        }
    }
}
