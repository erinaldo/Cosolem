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
    public partial class frmFormacionAcademica : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;
        tbFormacionAcademica _tbFormacionAcademica = null;

        public frmFormacionAcademica(ref tbFormacionAcademica _tbFormacionAcademica)
        {
            this._tbFormacionAcademica = _tbFormacionAcademica;
            InitializeComponent();
        }

        private void frmFormacionAcademica_Load(object sender, EventArgs e)
        {
            try
            {
                _dbCosolemEntities = new dbCosolemEntities();

                var _tbProvincia = (from P in _dbCosolemEntities.tbProvincia select new { idProvincia = P.idProvincia, descripcion = P.descripcion, tbCanton = (from C in P.tbCanton select new { idCanton = C.idCanton, descripcion = C.descripcion }) }).ToList();
                cmbProvincia.DataSource = _tbProvincia;
                cmbProvincia.ValueMember = "idProvincia";
                cmbProvincia.DisplayMember = "descripcion";
                if (_tbFormacionAcademica.tbCanton != null) cmbProvincia.SelectedValue = _tbFormacionAcademica.tbCanton.idProvincia;

                var _tbCanton = ((dynamic)cmbProvincia.SelectedItem).tbCanton;
                cmbCanton.DataSource = _tbCanton;
                cmbCanton.ValueMember = "idCanton";
                cmbCanton.DisplayMember = "descripcion";
                if (_tbFormacionAcademica.idCanton > 0) cmbProvincia.SelectedValue = _tbFormacionAcademica.idCanton;

                var _tbTipoFormacionAcademica = (from TFA in _dbCosolemEntities.tbTipoFormacionAcademica select new { idTipoFormacionAcademica = TFA.idTipoFormacionAcademica, descripcion = TFA.descripcion }).ToList();
                cmbTipoFormacionAcademica.DataSource = _tbTipoFormacionAcademica;
                cmbTipoFormacionAcademica.ValueMember = "idTipoFormacionAcademica";
                cmbTipoFormacionAcademica.DisplayMember = "descripcion";
                if (_tbFormacionAcademica.idTipoFormacionAcademica > 0) cmbTipoFormacionAcademica.SelectedValue = _tbFormacionAcademica.idTipoFormacionAcademica;

                txtNombreCentroEstudio.Text = _tbFormacionAcademica.nombreCentroEstudio;
                dtpFechaInicio.Value = _tbFormacionAcademica.fechaInicio == DateTime.MinValue ? Program.fechaHora : _tbFormacionAcademica.fechaInicio;
                if (_tbFormacionAcademica.fechaFin.HasValue)
                {
                    dtpFechaFin.Checked = true;
                    dtpFechaFin.Value = _tbFormacionAcademica.fechaFin.Value;
                }
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void cmbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var _tbCanton = ((dynamic)cmbProvincia.SelectedItem).tbCanton;

            cmbCanton.DataSource = _tbCanton;
            cmbCanton.ValueMember = "idCanton";
            cmbCanton.DisplayMember = "descripcion";
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (String.IsNullOrEmpty(txtNombreCentroEstudio.Text.Trim())) mensaje += "Ingrese nombre de centro de estudio\n";

            if (String.IsNullOrEmpty(mensaje))
            {
                dynamic _tbCanton = ((dynamic)cmbCanton.SelectedItem);
                dynamic _tbTipoFormacionAcademica = ((dynamic)cmbTipoFormacionAcademica.SelectedItem);

                tbFormacionAcademica _tbFormacionAcademica = new tbFormacionAcademica
                {
                    idCanton = _tbCanton.idCanton,
                    idTipoFormacionAcademica = _tbTipoFormacionAcademica.idTipoFormacionAcademica,
                    nombreCentroEstudio = txtNombreCentroEstudio.Text,
                    fechaInicio = dtpFechaInicio.Value.Date,
                    fechaFin = (dtpFechaFin.Checked ? dtpFechaFin.Value.Date : (DateTime?)null),
                    estadoRegistro = true,
                    descripcionProvincia = ((dynamic)cmbProvincia.SelectedItem).descripcion,
                    descripcionCanton = _tbCanton.descripcion,
                    descripcionTipoFormacionAcademica = _tbTipoFormacionAcademica.descripcion
                };

                if (this._tbFormacionAcademica.idFormacionAcademica == 0)
                {
                    this._tbFormacionAcademica.fechaHoraIngreso = Program.fechaHora;
                    this._tbFormacionAcademica.idUsuarioIngreso = idUsuario;
                    this._tbFormacionAcademica.terminalIngreso = Program.terminal;
                }
                else
                {
                    if (_tbFormacionAcademica.idCanton != this._tbFormacionAcademica.idCanton || _tbFormacionAcademica.idTipoFormacionAcademica != this._tbFormacionAcademica.idTipoFormacionAcademica || _tbFormacionAcademica.nombreCentroEstudio != this._tbFormacionAcademica.nombreCentroEstudio || _tbFormacionAcademica.fechaInicio != this._tbFormacionAcademica.fechaInicio || _tbFormacionAcademica.fechaFin != this._tbFormacionAcademica.fechaFin || _tbFormacionAcademica.estadoRegistro != this._tbFormacionAcademica.estadoRegistro)
                    {
                        this._tbFormacionAcademica.fechaHoraUltimaModificacion = Program.fechaHora;
                        this._tbFormacionAcademica.idUsuarioUltimaModificacion = idUsuario;
                        this._tbFormacionAcademica.terminalUltimaModificacion = Program.terminal;
                    }
                }
                this._tbFormacionAcademica.idCanton = _tbFormacionAcademica.idCanton;
                this._tbFormacionAcademica.idTipoFormacionAcademica = _tbFormacionAcademica.idTipoFormacionAcademica;
                this._tbFormacionAcademica.nombreCentroEstudio = _tbFormacionAcademica.nombreCentroEstudio;
                this._tbFormacionAcademica.fechaInicio = _tbFormacionAcademica.fechaInicio;
                this._tbFormacionAcademica.fechaFin = _tbFormacionAcademica.fechaFin;
                this._tbFormacionAcademica.estadoRegistro = _tbFormacionAcademica.estadoRegistro;
                this._tbFormacionAcademica.descripcionProvincia = _tbFormacionAcademica.descripcionProvincia;
                this._tbFormacionAcademica.descripcionCanton = _tbFormacionAcademica.descripcionCanton;
                this._tbFormacionAcademica.descripcionTipoFormacionAcademica = _tbFormacionAcademica.descripcionTipoFormacionAcademica;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro desea eliminar el registro?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                _tbFormacionAcademica.estadoRegistro = false;
                _tbFormacionAcademica.fechaHoraEliminacion = Program.fechaHora;
                _tbFormacionAcademica.idUsuarioEliminacion = idUsuario;
                _tbFormacionAcademica.terminalEliminacion = Program.terminal;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
