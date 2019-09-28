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
    public partial class frmBusquedaAgendamientoServicioTecnico : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Tecnico
        {
            public long? idEmpleado { get; set; }
            public string nombreCompleto { get; set; }
        }

        public frmBusquedaAgendamientoServicioTecnico()
        {
            InitializeComponent();
        }

        private void frmBusquedaAgendamientoServicioTecnico_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            txtNumeroIdentificacion.Clear();

            List<Tecnico> tecnicos = (from E in _dbCosolemEntities.tbDepartamento.Where(x => x.idEmpresa == idEmpresa && new List<long> { 6, 9 }.Contains(x.idDepartamento) && x.estadoRegistro).FirstOrDefault().tbCargo.Where(y => new List<long> { 17, 24 }.Contains(y.idCargo) && y.estadoRegistro).FirstOrDefault().tbEmpleado where E.estadoRegistro select new Tecnico { idEmpleado = E.idEmpleado, nombreCompleto = E.tbPersona.nombreCompleto }).ToList();
            tecnicos.Insert(0, new Tecnico { idEmpleado = null, nombreCompleto = "Todos" });
            cmbTecnico.DataSource = tecnicos;
            cmbTecnico.ValueMember = "idEmpleado";
            cmbTecnico.DisplayMember = "nombreCompleto";

            dtpFechaDesde.Value = DateTime.Now.Date;
            dtpFechaHasta.Value = DateTime.Now.Date;

            dgvAgendamientos.AutoGenerateColumns = false;

            dgvAgendamientos.DataSource = null;
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            long? idTecnico = ((Tecnico)cmbTecnico.SelectedItem).idEmpleado;

            _dbCosolemEntities = new dbCosolemEntities();
            var _tbOrdenTrabajo = (from OT in _dbCosolemEntities.tbOrdenTrabajo where OT.idEmpresa == idEmpresa && new List<int>{1, 2, 3}.Contains(OT.idEstadoOrdenTrabajo) && EntityFunctions.TruncateTime(OT.fechaHoraIngreso) >= EntityFunctions.TruncateTime(dtpFechaDesde.Value) && EntityFunctions.TruncateTime(OT.fechaHoraIngreso) <= EntityFunctions.TruncateTime(dtpFechaHasta.Value) select OT);
            if (idTecnico.HasValue) _tbOrdenTrabajo = (from OT in _tbOrdenTrabajo where OT.idTecnico == idTecnico select OT);
            if (!String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) _tbOrdenTrabajo = (from OT in _tbOrdenTrabajo where OT.numeroIdentificacion == txtNumeroIdentificacion.Text.Trim() select OT);
            SortableBindingList<tbOrdenTrabajo> ordenesTrabajo = new SortableBindingList<tbOrdenTrabajo>(_tbOrdenTrabajo.ToList());
            ordenesTrabajo.ToList().ForEach(x =>
            {
                x.seleccionado = false;
                x.estadoAgendamiento = x.tbEstadoOrdenTrabajo.descripcion;
                x.tecnicoAsignado = x.idTecnico != null ? x.tbEmpleado.tbPersona.nombreCompleto : String.Empty;
            });

            dgvAgendamientos.DataSource = ordenesTrabajo;
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            dgvAgendamientos_CellEndEdit(null, null);
            List<tbOrdenTrabajo> ordenesTrabajo = ((SortableBindingList<tbOrdenTrabajo>)dgvAgendamientos.DataSource).ToList();
            if (ordenesTrabajo.Where(x => new List<int>{2, 3}.Contains(x.idEstadoOrdenTrabajo) && x.seleccionado).Count() > 0) MessageBox.Show("Entre los registros seleccionados hay agendamientos en curso y/o finalizados y estos no puden ser eliminados", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (ordenesTrabajo.Where(x => x.seleccionado).Count() == 0) MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (MessageBox.Show("¿Seguro desea anular las ordenes de trabajo seleccionadas?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    ordenesTrabajo.Where(x => x.idEstadoOrdenTrabajo == 1).ToList().ForEach(y =>
                    {
                        y.idEstadoOrdenTrabajo = 4;
                        y.fechaHoraEliminacion = Program.fechaHora;
                        y.idUsuarioEliminacion = idUsuario;
                        y.terminalEliminacion = Program.terminal;
                    });
                    _dbCosolemEntities.SaveChanges();
                    MessageBox.Show("Ordenes de venta anuladas satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tsbBuscar_Click(null, null);
                }
            }
        }

        private void dgvAgendamientos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvAgendamientos.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmBusquedaAgendamientoServicioTecnico_Load(null, null);
        }

        private void dgvAgendamientos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                tbOrdenTrabajo ordenTrabajo = (tbOrdenTrabajo)dgvAgendamientos.CurrentRow.DataBoundItem;
                long idOrdenTrabajo = ordenTrabajo.idOrdenTrabajo;
                int idEstadoOrdenTrabajo = ordenTrabajo.idEstadoOrdenTrabajo;

                if (idEstadoOrdenTrabajo != 1)
                    MessageBox.Show("Orden de trabajo ya no puede ser modificada porque ya paso de pendiente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    frmAgendamientoServicioTecnico _frmAgendamientoServicioTecnico = new frmAgendamientoServicioTecnico();
                    _frmAgendamientoServicioTecnico.Text = "Agendamiento de servicio técnico";
                    _frmAgendamientoServicioTecnico.idOrdenTrabajo = idOrdenTrabajo;
                    if (_frmAgendamientoServicioTecnico.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        tsbBuscar_Click(null, null);
                }
            }
        }

        private void dgvAgendamientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
