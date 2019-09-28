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
    public partial class frmReporteIngresoInventarioConcepto : Form
    {
        List<tbInventario> inventario = null;
        string nombreUsuario = Program.tbUsuario.nombreUsuario;
        string nombreCompleto = Program.tbUsuario.tbEmpleado.tbPersona.nombreCompleto;

        public frmReporteIngresoInventarioConcepto(List<tbInventario> inventario)
        {
            this.inventario = inventario;
            InitializeComponent();
        }

        private void frmReporteIngresoInventarioConcepto_Load(object sender, EventArgs e)
        {
            List<rptIngresoInventario> _rptIngresoInventario = new List<rptIngresoInventario>();
            inventario.ToList().ForEach(x =>
            {
                rptIngresoInventario ingresoInventario = new rptIngresoInventario();
                ingresoInventario.concepto = x.concepto;
                ingresoInventario.fechaFactura = x.fechaFactura;
                ingresoInventario.cliente = x.cliente;
                ingresoInventario.comentarios = x.comentarios;
                ingresoInventario.nombreUsuario = nombreUsuario;
                ingresoInventario.nombreCompleto = nombreCompleto;
                ingresoInventario.codigoProducto = x.codigoProducto;
                ingresoInventario.descripcionProducto = x.descripcionProducto;
                ingresoInventario.inventario = x.fisicoDisponible - x.cantidad;
                ingresoInventario.cantidad = x.cantidad;
                ingresoInventario.descripcionBodega = x.tbBodega.descripcion;
                _rptIngresoInventario.Add(ingresoInventario);
            });
            rvwIngresoInventario.LocalReport.DataSources.Clear();
            rvwIngresoInventario.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\Logistica\\rptIngresoInventarioConcepto.rdlc";
            rvwIngresoInventario.LocalReport.DataSources.Add(new ReportDataSource("dtsIngresoInventario", _rptIngresoInventario));
            rvwIngresoInventario.RefreshReport();
        }
    }
}
