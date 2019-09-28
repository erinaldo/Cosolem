using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.Objects;

namespace Cosolem
{
    public partial class frmPrincipal : Form
    {
        long idUsuario = Program.tbUsuario.idUsuario;

        private void setNotificaciones()
        {
            try
            {
                DateTime fechaActual = DateTime.Now.AddDays(3).Date;

                using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
                {
                    var seguimientos = (from SCD in _dbCosolemEntities.tbSeguimientoCotizacionDetalle
                                        where SCD.tbSeguimientoCotizacionCabecera.idUsuarioIngreso == idUsuario && SCD.tbSeguimientoCotizacionCabecera.idEstadoSeguimientoCotizacion == 1 && EntityFunctions.TruncateTime(SCD.fechaProximoSeguimiento) <= EntityFunctions.TruncateTime(fechaActual)
                                        group SCD by SCD.idSeguimientoCotizacionCabecera into G
                                        select new
                                        {
                                            fechaProximoSeguimiento = G.Max(x => x.fechaProximoSeguimiento)
                                        }).ToList();
                    if (seguimientos.Count > 0)
                    {
                        int cantidadSeguimientosVencidas = seguimientos.Where(x => x.fechaProximoSeguimiento <= DateTime.Now.Date).Count();
                        int cantidadSeguimientosVencer = seguimientos.Where(x => x.fechaProximoSeguimiento > DateTime.Now.Date).Count();

                        ntfSeguimientoCotizacion.BalloonTipText = cantidadSeguimientosVencidas + (cantidadSeguimientosVencidas == 1 ? " notificación " : " notificaciones ") + "de seguimiento de cotización vencidas\n" + cantidadSeguimientosVencer + (cantidadSeguimientosVencer == 1 ? " notificación " : " notificaciones ") + "de seguimiento de cotización por vencer";
                        ntfSeguimientoCotizacion.ShowBalloonTip(3000);
                    }
                }
            }
            catch { }
        }

        public frmPrincipal()
        {
            InitializeComponent();

            setNotificaciones();
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            tslEmpresa.Text = "Empresa " + Program.tbUsuario.tbEmpleado.tbEmpresa.razonSocial;
            tslNombreCompleto.Text = "Bienvenid@ " + Program.tbUsuario.tbEmpleado.tbPersona.nombreCompleto;
            tslTerminal.Text = Program.terminal;

            if (Program.tbUsuario.cambiarContrasena)
            {
                frmCambiarContrasena _frmCambiarContrasena = new frmCambiarContrasena();
                _frmCambiarContrasena.ShowInTaskbar = false;
                _frmCambiarContrasena.ShowDialog();
                if (Program.tbUsuario.cambiarContrasena) Application.Exit();
            }

            List<tbUsuarioOpcion> permisos = Program.tbUsuario.tbUsuarioOpcion.Where(x => x.tieneAcceso).ToList();
            List<tbModulo> modulos = permisos.Select(x => x.tbOpcion.tbModulo).OrderBy(y => y.ordenPosicion).Distinct().ToList();
            foreach (var modulo in modulos)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem { Text = modulo.descripcion };
                permisos.Where(x => x.tbOpcion.idModulo == modulo.idModulo).ToList().ForEach(y => 
                {
                    tbOpcion opcion = y.tbOpcion;
                    ToolStripItem item = new ToolStripMenuItem { Text = opcion.descripcion, Tag = opcion };
                    item.Click += new EventHandler(item_Click);
                    menu.DropDownItems.Add(item);
                });
                mnsOpciones.Items.Add(menu);
            }

            tmrTemporizador.Start();
        }

        private void item_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripMenuItem)sender;
            tbOpcion opcion = (tbOpcion)item.Tag;
            Type type = Type.GetType("Cosolem." + opcion.nombreFormulario);
            object[] args = { };
            if (opcion.parametros != null) args = opcion.parametros.Split(',');
            Form form = (Form)Activator.CreateInstance(type, args);
            form.Text = opcion.descripcion;
            form.MdiParent = this;
            form.Show();
        }

        private void tmrTemporizador_Tick(object sender, EventArgs e)
        {
            setNotificaciones();
        }

        private void ntfSeguimientoCotizacion_Click(object sender, EventArgs e)
        {
            frmBusquedaSeguimientoCotizacion busquedaSeguimientoCotizacion = new frmBusquedaSeguimientoCotizacion();
            busquedaSeguimientoCotizacion.MdiParent = this;
            busquedaSeguimientoCotizacion.Show();
        }

        private void ntfSeguimientoCotizacion_BalloonTipClicked(object sender, EventArgs e)
        {
            frmBusquedaSeguimientoCotizacion busquedaSeguimientoCotizacion = new frmBusquedaSeguimientoCotizacion();
            busquedaSeguimientoCotizacion.MdiParent = this;
            busquedaSeguimientoCotizacion.Show();
        }
    }
}
