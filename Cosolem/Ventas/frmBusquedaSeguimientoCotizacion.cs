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
    public partial class frmBusquedaSeguimientoCotizacion : Form
    {
        long idUsuario = Program.tbUsuario.idUsuario;

        SortableBindingList<tbSeguimientoCotizacionDetalle> seguimientosCotizacionDetalle = new SortableBindingList<tbSeguimientoCotizacionDetalle>();

        public frmBusquedaSeguimientoCotizacion()
        {
            InitializeComponent();
        }

        private void frmBusquedaSeguimientoCotizacion_Load(object sender, EventArgs e)
        {
            dgvSeguimientoCotizacionVencidas.AutoGenerateColumns = false;
            dgvSeguimientoCotizacionVencer.AutoGenerateColumns = false;

            DateTime fechaActual = DateTime.Now.AddDays(3).Date;

            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                var seguimientos = (from SCD in _dbCosolemEntities.tbSeguimientoCotizacionDetalle
                                    where SCD.tbSeguimientoCotizacionCabecera.idUsuarioIngreso == idUsuario && SCD.tbSeguimientoCotizacionCabecera.idEstadoSeguimientoCotizacion == 1 && EntityFunctions.TruncateTime(SCD.fechaProximoSeguimiento) <= EntityFunctions.TruncateTime(fechaActual)
                                    group SCD by SCD.idSeguimientoCotizacionCabecera into G
                                    select new
                                    {
                                        datos = G.Select(x => new { idOrdenVentaCabecera = x.tbSeguimientoCotizacionCabecera.idOrdenVentaCabecera, cliente = x.tbSeguimientoCotizacionCabecera.tbOrdenVentaCabecera.cliente, idEstadoOrdenVenta = x.tbSeguimientoCotizacionCabecera.tbOrdenVentaCabecera.idEstadoOrdenVenta, fechaSeguimiento = x.fechaProximoSeguimiento.Value, comentarioSeguimiento = x.comentarioSeguimiento, fechaHoraIngreso = x.fechaHoraIngreso }),
                                        fechaSeguimiento = G.Max(x => x.fechaProximoSeguimiento.Value)
                                    }).OrderBy(x => x.fechaSeguimiento).ToList();
                foreach (var seguimiento in seguimientos)
                {
                    tbSeguimientoCotizacionDetalle seguimientoCotizacionDetalle = new tbSeguimientoCotizacionDetalle();
                    foreach (var seguimientoEspecifico in seguimiento.datos.Where(x => x.fechaSeguimiento.Date == seguimiento.fechaSeguimiento.Date).ToList())
                    {
                        seguimientoCotizacionDetalle.idOrdenVentaCabecera = seguimientoEspecifico.idOrdenVentaCabecera;
                        seguimientoCotizacionDetalle.cliente = seguimientoEspecifico.cliente;
                        seguimientoCotizacionDetalle.idEstadoOrdenVenta = seguimientoEspecifico.idEstadoOrdenVenta;
                        seguimientoCotizacionDetalle.fechaSeguimiento = seguimientoEspecifico.fechaSeguimiento;
                        seguimientoCotizacionDetalle.comentarioSeguimiento += seguimientoEspecifico.comentarioSeguimiento + "\n";
                        seguimientoCotizacionDetalle.fechaHoraIngreso = seguimientoEspecifico.fechaHoraIngreso;
                    }
                    seguimientosCotizacionDetalle.Add(seguimientoCotizacionDetalle);
                }

                dgvSeguimientoCotizacionVencidas.DataSource = seguimientosCotizacionDetalle.Where(x => x.fechaSeguimiento <= DateTime.Now.Date).ToList();
                dgvSeguimientoCotizacionVencer.DataSource = seguimientosCotizacionDetalle.Where(x => x.fechaSeguimiento > DateTime.Now.Date).ToList();
            }
        }
    }
}
