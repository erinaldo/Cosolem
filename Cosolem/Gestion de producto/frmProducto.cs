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
    public partial class frmProducto : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;
        long idProducto = 0;

        class Producto
        {
            public long idProducto { get; set; }
            public string descripcion { get; set; }
            public string caracteristicas { get; set; }
        }

        public tbProducto _tbProducto = null;
        BindingList<tbProductoComplementario> productosComplementarios = null;

        public frmProducto(tbProducto _tbProducto)
        {
            this._tbProducto = _tbProducto;
            InitializeComponent();
        }

        private void InactivarRegistros(ref DataGridView _DataGridView)
        {
            CurrencyManager _CurrencyManager = (CurrencyManager)BindingContext[_DataGridView.DataSource];
            _CurrencyManager.SuspendBinding();
            foreach (DataGridViewRow _DataGridViewRow in _DataGridView.Rows)
            {
                dynamic _dynamic = (dynamic)_DataGridViewRow.DataBoundItem;
                _DataGridViewRow.Visible = _dynamic.estadoRegistro;
            }
            _CurrencyManager.ResumeBinding();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            cmbTipoProducto.DataSource = (from TP in _dbCosolemEntities.tbTipoProducto select new { idTipoProducto = TP.idTipoProducto, descripcion = TP.descripcion }).ToList();
            cmbTipoProducto.ValueMember = "idTipoProducto";
            cmbTipoProducto.DisplayMember = "descripcion";

            if (this._tbProducto.idTipoProducto > 0) cmbTipoProducto.SelectedValue = this._tbProducto.idTipoProducto;
            txtCodigoProducto.Text = this._tbProducto.codigoProducto;
            if (this._tbProducto.idProducto != 0)
            {
                idProducto = this._tbProducto.idProducto;
                txtCodigoProducto.Enabled = false;
            }
            txtDescripcion.Text = this._tbProducto.descripcion;
            txtCaracteristicas.Text = this._tbProducto.caracteristicas;
            txtUrlEspecificaciones.Text = this._tbProducto.urlEspecificaciones;
            txtUrlManual.Text = this._tbProducto.urlManual;
            txtCapacidad.Text = this._tbProducto.capacidad.ToString(Application.CurrentCulture.NumberFormat);
            if (this._tbProducto.imagen != null) pbxImagen.Image = Util.ObtenerImagen(this._tbProducto.imagen);

            dgvProductosComplementarios.AutoGenerateColumns = false;
            this._tbProducto.tbProductoComplementario.ToList().ForEach(x => 
            {
                tbProducto producto = _dbCosolemEntities.tbProducto.Where(y => y.idProducto == x.idProductoComplementario).FirstOrDefault();
                x.producto = producto.codigoProducto + " - " + producto.descripcion;
                x.caracteristicas = producto.caracteristicas;
            });
            productosComplementarios = new BindingList<tbProductoComplementario>(this._tbProducto.tbProductoComplementario.ToList());
            dgvProductosComplementarios.DataSource = productosComplementarios;
            InactivarRegistros(ref dgvProductosComplementarios);
        }

        private string VerificaProducto(string codigoProducto)
        {
            string mensaje = String.Empty;
            if ((from P in _dbCosolemEntities.tbProducto where P.codigoProducto == codigoProducto && P.estadoRegistro select P).Count() > 0)
                mensaje += "Código de producto se encuentra registrado, favor verificar\n";
            return mensaje;
        }

        private string VerificaProductoComplementario()
        {
            string mensaje = String.Empty;
            List<tbProductoComplementario> productosComplementarios = this.productosComplementarios.Where(x => x.estadoRegistro).ToList();
            if (productosComplementarios.Where(x => x.idProductoComplementario == 0).Count() > 0)
                mensaje += "Seleccione productos complementarios, favor verificar\n";
            if ((from PS in productosComplementarios group PS by PS.idProductoComplementario into g select new { g.Key, count = g.Count() }).Where(x => x.count > 1).Count() > 0)
                mensaje += "Productos complementarios repetidos, favor verificar\n";
            return mensaje;
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            dynamic _tbTipoProducto = (dynamic)cmbTipoProducto.SelectedItem;

            string mensaje = String.Empty;
            if (String.IsNullOrEmpty(txtCodigoProducto.Text.Trim())) mensaje += "Ingrese código de producto\n";
            if (String.IsNullOrEmpty(txtDescripcion.Text.Trim())) mensaje += "Ingrese descripción\n";
            if (String.IsNullOrEmpty(txtCapacidad.Text.Trim())) mensaje += "Ingrese capacidad\n";
            if (pbxImagen.Image == null) mensaje += "Seleccione imagen\n";
            if (txtCodigoProducto.Enabled) mensaje += VerificaProducto(txtCodigoProducto.Text.Trim());
            mensaje += VerificaProductoComplementario();

            if (String.IsNullOrEmpty(mensaje))
            {
                tbProducto _tbProducto = new tbProducto
                {
                    idTipoProducto = _tbTipoProducto.idTipoProducto,
                    codigoProducto = txtCodigoProducto.Text.Trim(),
                    descripcion = txtDescripcion.Text.Trim(),
                    caracteristicas = txtCaracteristicas.Text.Trim(),
                    urlEspecificaciones = txtUrlEspecificaciones.Text.Trim(),
                    urlManual = txtUrlManual.Text.Trim(),
                    capacidad = Convert.ToDecimal(txtCapacidad.Text.Trim()),
                    imagen = (byte[])new ImageConverter().ConvertTo(pbxImagen.Image, typeof(byte[])),
                    estadoRegistro = true,
                    descripcionTipoProducto = _tbTipoProducto.descripcion
                };

                if (this._tbProducto.idProducto == 0)
                {
                    this._tbProducto.fechaHoraIngreso = Program.fechaHora;
                    this._tbProducto.idUsuarioIngreso = idUsuario;
                    this._tbProducto.terminalIngreso = Program.terminal;
                }
                else
                {
                    if (_tbProducto.idTipoProducto != this._tbProducto.idTipoProducto || _tbProducto.codigoProducto != this._tbProducto.codigoProducto || _tbProducto.descripcion != this._tbProducto.descripcion || _tbProducto.caracteristicas != this._tbProducto.caracteristicas || _tbProducto.urlEspecificaciones != this._tbProducto.urlEspecificaciones || _tbProducto.urlManual != this._tbProducto.urlManual || _tbProducto.estadoRegistro != this._tbProducto.estadoRegistro)
                    {
                        this._tbProducto.fechaHoraUltimaModificacion = Program.fechaHora;
                        this._tbProducto.idUsuarioUltimaModificacion = idUsuario;
                        this._tbProducto.terminalUltimaModificacion = Program.terminal;
                    }
                }
                this._tbProducto.idTipoProducto = _tbProducto.idTipoProducto;
                this._tbProducto.codigoProducto = _tbProducto.codigoProducto;
                this._tbProducto.descripcion = _tbProducto.descripcion;
                this._tbProducto.caracteristicas = _tbProducto.caracteristicas;
                this._tbProducto.urlEspecificaciones = _tbProducto.urlEspecificaciones;
                this._tbProducto.urlManual = _tbProducto.urlManual;
                this._tbProducto.capacidad = _tbProducto.capacidad;
                this._tbProducto.imagen = _tbProducto.imagen;
                this._tbProducto.estadoRegistro = _tbProducto.estadoRegistro;
                this._tbProducto.descripcionTipoProducto = _tbProducto.descripcionTipoProducto;
                productosComplementarios.ToList().ForEach(x => { if (x.idSecuencia == 0) this._tbProducto.tbProductoComplementario.Add(x); });

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void pbxImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Images (*.bmp;*.jpg;*.gif,*.png,*.tiff)|*.bmp;*.jpg;*.gif;*.png;*.tiff|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                pbxImagen.Image = new Bitmap(openFileDialog.FileName);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro desea eliminar el registro?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                _tbProducto.estadoRegistro = false;
                _tbProducto.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbProducto.idUsuarioUltimaModificacion = idUsuario;
                _tbProducto.terminalUltimaModificacion = Program.terminal;
                _tbProducto.fechaHoraEliminacion = Program.fechaHora;
                _tbProducto.idUsuarioEliminacion = idUsuario;
                _tbProducto.terminalEliminacion = Program.terminal;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnAgregarProductosComplementarios_Click(object sender, EventArgs e)
        {
            productosComplementarios.Add(new tbProductoComplementario { idProducto = _tbProducto.idProducto, idProductoComplementario = 0, producto = String.Empty, caracteristicas = String.Empty, estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });
            InactivarRegistros(ref dgvProductosComplementarios);
        }

        private void dgvProductosComplementarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == imgEliminarProductoComplementario.Index)
                {
                    if (MessageBox.Show("¿Seguro desea eliminar el registro seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        tbProductoComplementario productoComplementario = (tbProductoComplementario)dgvProductosComplementarios.CurrentRow.DataBoundItem;
                        productoComplementario.estadoRegistro = false;
                        productoComplementario.fechaHoraUltimaModificacion = Program.fechaHora;
                        productoComplementario.idUsuarioUltimaModificacion = idUsuario;
                        productoComplementario.terminalUltimaModificacion = Program.terminal;
                        productoComplementario.fechaHoraEliminacion = Program.fechaHora;
                        productoComplementario.idUsuarioEliminacion = idUsuario;
                        productoComplementario.terminalEliminacion = Program.terminal;
                        if (productoComplementario.idSecuencia == 0) productosComplementarios.Remove(productoComplementario);
                        InactivarRegistros(ref dgvProductosComplementarios);
                    }
                }
            }
        }

        private void dgvProductosComplementarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataTable _DataTable = new DataTable();
                _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Marca"), new DataColumn("Línea"), new DataColumn("Grupo"), new DataColumn("SubGrupo"), new DataColumn("Modelo"), new DataColumn("Código de producto"), new DataColumn("Descripción"), new DataColumn("Características"), new DataColumn("Fecha de registro"), new DataColumn("producto", typeof(object)) });

                (from P in _dbCosolemEntities.tbProducto
                 where P.estadoRegistro && P.idProducto != idProducto
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
                {
                    tbProductoComplementario productoComplementario = (tbProductoComplementario)dgvProductosComplementarios.CurrentRow.DataBoundItem;
                    tbProducto producto = (tbProducto)_frmBusqueda._object;
                    productoComplementario.idProductoComplementario = producto.idProducto;
                    productoComplementario.producto = producto.codigoProducto + " - " + producto.descripcion;
                    productoComplementario.caracteristicas = producto.caracteristicas;
                    dgvProductosComplementarios.Refresh();
                }
            }
        }

        private void txtCapacidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Program.decimalPoint))
                e.Handled = true;
            if ((e.KeyChar == Program.decimalPoint) && ((sender as TextBox).Text.IndexOf(Program.decimalPoint) > -1))
                e.Handled = true;
        }
    }
}