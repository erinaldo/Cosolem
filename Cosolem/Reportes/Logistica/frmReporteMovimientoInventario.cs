using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Cosolem
{
    public partial class frmReporteMovimientoInventario : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;

        public frmReporteMovimientoInventario(string imprimir, string exportar)
        {
            InitializeComponent();
            rvwMovimientoInventario.ShowPrintButton = Convert.ToBoolean(imprimir);
            rvwMovimientoInventario.ShowExportButton = Convert.ToBoolean(exportar);
        }

        private void frmReporteInventarioGeneral_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();
            tsbNuevo_Click(null, null);
        }

        private void CargarReporte(List<rptMovimientoInventario> _rptMovimientoInventario = null)
        {
            if (_rptMovimientoInventario == null) _rptMovimientoInventario = new List<rptMovimientoInventario>();

            rvwMovimientoInventario.LocalReport.DataSources.Clear();
            rvwMovimientoInventario.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\Logistica\\rptMovimientoInventario.rdlc";
            rvwMovimientoInventario.LocalReport.DataSources.Add(new ReportDataSource("dtsMovimientoInventario", _rptMovimientoInventario));
            rvwMovimientoInventario.RefreshReport();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            long idProducto = 0;

            if (txtCodigoProducto.Tag != null) idProducto = Convert.ToInt64(txtCodigoProducto.Tag);

            int anioActual = DateTime.Now.Year;
            var movimientoInventario = (from TI in _dbCosolemEntities.tbTransaccionInventario
                                        join B in _dbCosolemEntities.tbBodega on TI.idBodega equals B.idBodega
                                        join P in _dbCosolemEntities.tbProducto on TI.idProducto equals P.idProducto
                                        join U in _dbCosolemEntities.tbUsuario on TI.idUsuarioIngreso equals U.idUsuario
                                        where B.tbTienda.idEmpresa == idEmpresa && P.tbTipoProducto.esInventariable && TI.fechaHoraIngreso.Year == anioActual
                                        select new rptMovimientoInventario
                                        {
                                            tipoMovimiento = TI.tipoTransaccion,
                                            idBodega = TI.idBodega,
                                            descripcionBodega = B.descripcion,
                                            idProducto = TI.idProducto,
                                            codigoProducto = P.codigoProducto,
                                            descripcionProducto = P.descripcion,
                                            cantidadIngreso = TI.cantidad > 0 ? TI.cantidad : 0,
                                            cantidadEgreso = TI.cantidad < 0 ? TI.cantidad : 0,
                                            cantidad = TI.cantidad,
                                            fechaHoraMovimiento = TI.fechaHoraIngreso,
                                            usuarioGeneroMovimiento = U.nombreUsuario
                                        });

            if (idProducto != 0) movimientoInventario = (from MI in movimientoInventario where MI.idProducto == idProducto select MI);

            CargarReporte(movimientoInventario.ToList());
        }

        private void SetearProducto(tbProducto _tbProducto)
        {
            txtCodigoProducto.Clear();
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();

            if (_tbProducto == null)
                MessageBox.Show("Producto no existe", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                txtCodigoProducto.Text = _tbProducto.codigoProducto;
                txtCodigoProducto.Tag = _tbProducto.idProducto;
                txtDescripcionProducto.Text = _tbProducto.descripcion;
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

        private void txtCodigoProducto_Leave(object sender, EventArgs e)
        {
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();

            if (!String.IsNullOrEmpty(txtCodigoProducto.Text.Trim()))
            {
                var _tbProducto = edmCosolemFunctions.getProductos(txtCodigoProducto.Text.Trim());
                tbProducto producto = _tbProducto.Count == 0 ? null : _tbProducto[0].producto;
                SetearProducto(producto);
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            txtCodigoProducto.Clear();
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();
            CargarReporte();
        }
    }
}