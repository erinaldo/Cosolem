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
    public partial class frmReporteIngresoInventario : Form
    {
        List<tbInventario> inventario = null;
        string nombreUsuario = Program.tbUsuario.nombreUsuario;
        string nombreCompleto = Program.tbUsuario.tbEmpleado.tbPersona.nombreCompleto;

        public frmReporteIngresoInventario(List<tbInventario> inventario)
        {
            this.inventario = inventario;
            InitializeComponent();
        }

        private void frmReporteIngresoInventario_Load(object sender, EventArgs e)
        {
            List<rptIngresoInventario> _rptIngresoInventario = new List<rptIngresoInventario>();
            inventario.ToList().ForEach(x =>
            {
                rptIngresoInventario ingresoInventario = new rptIngresoInventario();
                ingresoInventario.nombreUsuario = nombreUsuario;
                ingresoInventario.nombreCompleto = nombreCompleto;
                ingresoInventario.codigoProducto = x.codigoProducto;
                ingresoInventario.descripcionProducto = x.descripcionProducto;
                ingresoInventario.inventario = x.fisicoDisponible;
                ingresoInventario.cantidad = x.cantidad;
                _rptIngresoInventario.Add(ingresoInventario);
            });
            rvwIngresoInventario.LocalReport.DataSources.Clear();
            rvwIngresoInventario.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\Logistica\\rptIngresoInventario.rdlc";
            rvwIngresoInventario.LocalReport.DataSources.Add(new ReportDataSource("dtsIngresoInventario", _rptIngresoInventario));
            rvwIngresoInventario.RefreshReport();
        }
    }
}
