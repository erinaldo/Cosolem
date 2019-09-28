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
    public partial class frmSeguimientoCotizacion : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;
        long idCotizacion = 0;

        tbSeguimientoCotizacionCabecera _tbSeguimientoCotizacionCabecera = null;

        public frmSeguimientoCotizacion(long idCotizacion, int idEstadoOrdenVenta)
        {
            this.idCotizacion = idCotizacion;
            InitializeComponent();
            if (idEstadoOrdenVenta != 2)
            {
                tstOpciones.Enabled = false;
                pnlDatosSeguimiento.Enabled = false;
            }
        }

        private void frmSeguimientoCotizacion_Load(object sender, EventArgs e)
        {
            dgvDetalleSeguimientoCotizacion.AutoGenerateColumns = false;

            _dbCosolemEntities = new dbCosolemEntities();

            _tbSeguimientoCotizacionCabecera = (from SCC in _dbCosolemEntities.tbSeguimientoCotizacionCabecera where SCC.idOrdenVentaCabecera == idCotizacion select SCC).FirstOrDefault();

            txtComentarioSeguimiento.Clear();
            dtpFechaProximoSeguimiento.Value = Program.fechaHora.Date;

            cmbEstadoSeguimientoCotizacion.DataSource = (from ESC in _dbCosolemEntities.tbEstadoSeguimientoCotizacion where ESC.idEstadoSeguimientoCotizacion != 2 select ESC).ToList();
            cmbEstadoSeguimientoCotizacion.ValueMember = "idEstadoSeguimientoCotizacion";
            cmbEstadoSeguimientoCotizacion.DisplayMember = "descripcion";
            cmbEstadoSeguimientoCotizacion_SelectionChangeCommitted(null, null);

            cmbEstadoSeguimientoCotizacion.SelectedValue = _tbSeguimientoCotizacionCabecera.idEstadoSeguimientoCotizacion;
            txtFechaHoraIngreso.Text = _tbSeguimientoCotizacionCabecera.fechaHoraIngreso.ToString("dd/MM/yyyy - HH:mm:ss");
            txtUsuarioIngreso.Text = edmCosolemFunctions.getNombreUsuario(_tbSeguimientoCotizacionCabecera.idUsuarioIngreso);
            txtFechaHoraUltimaModificacion.Text = _tbSeguimientoCotizacionCabecera.fechaHoraUltimaModificacion.ToString("dd/MM/yyyy - HH:mm:ss");
            txtUsuarioUltimaModificacion.Text = edmCosolemFunctions.getNombreUsuario(_tbSeguimientoCotizacionCabecera.idUsuarioUltimaModificacion);
            dgvDetalleSeguimientoCotizacion.DataSource = new SortableBindingList<tbSeguimientoCotizacionDetalle>(_tbSeguimientoCotizacionCabecera.tbSeguimientoCotizacionDetalle.ToList());
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmSeguimientoCotizacion_Load(null, null);
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            tbEstadoSeguimientoCotizacion estadoSeguimientoCotizacion = (tbEstadoSeguimientoCotizacion)cmbEstadoSeguimientoCotizacion.SelectedItem;
            if (estadoSeguimientoCotizacion.exigeComentario && String.IsNullOrEmpty(txtComentarioSeguimiento.Text.Trim()))
                MessageBox.Show("Ingrese comentario", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (estadoSeguimientoCotizacion.exigeFechaProximoSeguimiento && dtpFechaProximoSeguimiento.Value.Date <= Program.fechaHora.Date)
                MessageBox.Show("Fecha próximo seguimiento tiene que se mayor a la fecha de hoy", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbSeguimientoCotizacionCabecera.idEstadoSeguimientoCotizacion = estadoSeguimientoCotizacion.idEstadoSeguimientoCotizacion;
                _tbSeguimientoCotizacionCabecera.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbSeguimientoCotizacionCabecera.idUsuarioUltimaModificacion = idUsuario;
                _tbSeguimientoCotizacionCabecera.terminalUltimaModificacion = Program.terminal;
                
                DateTime? fechaProximoSeguimiento = null;
                if (estadoSeguimientoCotizacion.exigeFechaProximoSeguimiento) fechaProximoSeguimiento = dtpFechaProximoSeguimiento.Value.Date;
                
                _tbSeguimientoCotizacionCabecera.tbSeguimientoCotizacionDetalle.Add(new tbSeguimientoCotizacionDetalle { comentarioSeguimiento = txtComentarioSeguimiento.Text.Trim(), fechaProximoSeguimiento = fechaProximoSeguimiento, estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void cmbEstadoSeguimientoCotizacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dtpFechaProximoSeguimiento.Enabled = ((tbEstadoSeguimientoCotizacion)cmbEstadoSeguimientoCotizacion.SelectedItem).exigeFechaProximoSeguimiento;
        }
    }
}
