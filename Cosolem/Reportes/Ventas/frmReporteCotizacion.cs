using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Cosolem
{
    public partial class frmReporteCotizacion : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;

        string empresa = Program.tbUsuario.tbEmpleado.tbEmpresa.razonSocial;
        string tienda = Program.tbUsuario.tbEmpleado.tbTienda.descripcion;
        string empleado = Program.tbUsuario.tbEmpleado.tbPersona.nombreCompleto;
        string usuario = Program.tbUsuario.nombreUsuario;

        long idCotizacion = 0;

        private void GeneracionCotizacion(string tipoGeneracion, string directorioArchivo = null, string nombreArchivo = null, string formatoArchivo = null, string extensionArchivo = null, string impresora = null, int? dePagina = null, int? hastaPagina = null, int? cantidadCopias = null)
        {
            tbGeneracionCotizacion _tbGeneracionCotizacion = new tbGeneracionCotizacion();
            _tbGeneracionCotizacion.empresa = empresa;
            _tbGeneracionCotizacion.tienda = tienda;
            _tbGeneracionCotizacion.empleado = empleado;
            _tbGeneracionCotizacion.usuario = usuario;
            _tbGeneracionCotizacion.idOrdenVentaCabecera = idCotizacion;
            _tbGeneracionCotizacion.tipoGeneracion = tipoGeneracion;
            _tbGeneracionCotizacion.directorioArchivo = directorioArchivo;
            _tbGeneracionCotizacion.nombreArchivo = nombreArchivo;
            _tbGeneracionCotizacion.formatoArchivo = formatoArchivo;
            _tbGeneracionCotizacion.extensionArchivo = extensionArchivo;
            _tbGeneracionCotizacion.impresora = impresora;
            _tbGeneracionCotizacion.dePagina = dePagina;
            _tbGeneracionCotizacion.hastaPagina = hastaPagina;
            _tbGeneracionCotizacion.cantidadCopias = cantidadCopias;
            _tbGeneracionCotizacion.fechaHoraGeneracionCotizacion = Program.fechaHora;
            _tbGeneracionCotizacion.terminalGeneracionCotizacion = Program.terminal;
            _dbCosolemEntities.tbGeneracionCotizacion.AddObject(_tbGeneracionCotizacion);
            _dbCosolemEntities.SaveChanges();
        }

        public frmReporteCotizacion(long idCotizacion)
        {
            this.idCotizacion = idCotizacion;
            InitializeComponent();
        }

        private void frmReporteCotizacion_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            tbOrdenVentaCabecera ordenVenta = (from OVC in _dbCosolemEntities.tbOrdenVentaCabecera where OVC.tipoOrdenVenta == "C" && OVC.idEstadoOrdenVenta == 2 && OVC.estadoRegistro && OVC.idOrdenVentaCabecera == idCotizacion select OVC).FirstOrDefault();
            List<rptCotizacion> _rptCotizacion = new List<rptCotizacion>();
            ordenVenta.tbOrdenVentaDetalle.Where(x => x.estadoRegistro).ToList().ForEach(y =>
            {
                rptCotizacion cotizacion = new rptCotizacion();
                cotizacion.idOrdenVentaCabecera = ordenVenta.idOrdenVentaCabecera;
                cotizacion.fecha = ordenVenta.fechaHoraOrdenVenta.ToString("dd/MM/yyyy");
                cotizacion.hora = ordenVenta.fechaHoraOrdenVenta.ToString("HH:mm:ss");
                cotizacion.usuario = edmCosolemFunctions.getNombreUsuario(y.idUsuarioIngreso);
                cotizacion.tipoIdentificacion = ordenVenta.tipoIdentificacion.ToUpper();
                cotizacion.numeroIdentificacion = ordenVenta.numeroIdentificacion;
                cotizacion.formaPago = ordenVenta.tbFormaPago.descripcion + (ordenVenta.idFormaPago == 1 ? "" : " - " + ordenVenta.tbProductoFinancieroCabecera.descripcion);
                cotizacion.cliente = ordenVenta.cliente;
                cotizacion.direccion = ordenVenta.direccion;
                cotizacion.referencia = ordenVenta.referenciaEntregaDomicilio;
                cotizacion.telefonos = String.Join(", ", new List<string> { ordenVenta.convencional, ordenVenta.celular });
                cotizacion.producto = y.tbProducto.descripcion;
                cotizacion.precio = y.precio;
                cotizacion.cantidad = y.cantidad;
                cotizacion.subTotal = y.subTotal;
                cotizacion.descuento = y.descuento;
                cotizacion.subTotalBruto = y.subTotalBruto;
                cotizacion.etiquetaIVA = "IVA " + (y.porcentajeIVA / 100M).ToString("p0");
                cotizacion.IVA = y.IVA;
                cotizacion.totalNeto = y.totalNeto;
                _rptCotizacion.Add(cotizacion);
            });
            rvwCotizacion.LocalReport.DisplayName = "Cotización_" + idCotizacion.ToString() + "_" + usuario + "_" + Program.fechaHora.ToString("ddMMyyyy_HHmmss");
            rvwCotizacion.LocalReport.DataSources.Clear();
            rvwCotizacion.LocalReport.ReportPath = Application.StartupPath + "\\Reportes\\Ventas\\rptCotizacion.rdlc";
            rvwCotizacion.LocalReport.DataSources.Add(new ReportDataSource("dtsCotizacion", _rptCotizacion));
            rvwCotizacion.RefreshReport();
        }

        private void rvwCotizacion_ReportExport(object sender, Microsoft.Reporting.WinForms.ReportExportEventArgs e)
        {
            e.Cancel = true;
            string formato = e.Extension.Name.ToUpper();
            string extension = (formato == "PDF" ? ".pdf" : (formato == "EXCEL" ? ".xls" : (formato == "WORD" ? ".doc" : null)));
            if (extension != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.FileName = rvwCotizacion.LocalReport.DisplayName;
                saveFileDialog.Filter = e.Extension.LocalizedName + " (*" + extension + ")|*" + extension + "|All files(*.*)|*.*";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rvwCotizacion.ExportDialog(e.Extension, e.DeviceInfo, saveFileDialog.FileName);

                    string directorioArchivo = Path.GetDirectoryName(saveFileDialog.FileName);
                    string nombreArchivo = Path.GetFileName(saveFileDialog.FileName);
                    string extensionArchivo = Path.GetExtension(saveFileDialog.FileName);

                    GeneracionCotizacion("EXPORTAR", directorioArchivo, nombreArchivo, formato, extensionArchivo);
                }
            }
        }

        private void rvwCotizacion_PrintingBegin(object sender, ReportPrintEventArgs e)
        {
            string impresora = e.PrinterSettings.PrinterName;
            int dePagina = e.PrinterSettings.FromPage;
            int hastaPagina = e.PrinterSettings.ToPage;
            int cantidadCopias = e.PrinterSettings.Copies;
            GeneracionCotizacion("IMPRIMIR", null, null, null, null, impresora, dePagina, hastaPagina, cantidadCopias);
        }
    }
}
