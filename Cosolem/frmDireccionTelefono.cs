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
    public partial class frmDireccionTelefono : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;
        tbDireccion _tbDireccion = null;
        BindingList<tbTelefono> _BindingListtbTelefono = null;
        dynamic _tbTipoTelefono = null;
        dynamic _tbOperadora = null;

        public frmDireccionTelefono(ref tbDireccion _tbDireccion)
        {
            this._tbDireccion = _tbDireccion;
            InitializeComponent();
        }

        private void InactivarRegistros()
        {
            CurrencyManager _CurrencyManager = (CurrencyManager)BindingContext[dgvTelefonos.DataSource];
            _CurrencyManager.SuspendBinding();
            foreach (DataGridViewRow _DataGridViewRow in dgvTelefonos.Rows)
            {
                tbTelefono _tbTelefono = (tbTelefono)_DataGridViewRow.DataBoundItem;
                _DataGridViewRow.Visible = _tbTelefono.estadoRegistro;
            }
            _CurrencyManager.ResumeBinding();
        }

        private void frmDireccion_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            var _tbProvincia = (from P in _dbCosolemEntities.tbProvincia select new { idProvincia = P.idProvincia, descripcion = P.descripcion, tbCanton = (from C in P.tbCanton select new { idCanton = C.idCanton, descripcion = C.descripcion }) }).ToList();
            cmbProvincia.DataSource = _tbProvincia;
            cmbProvincia.ValueMember = "idProvincia";
            cmbProvincia.DisplayMember = "descripcion";
            if (_tbDireccion.tbCanton != null) cmbProvincia.SelectedValue = _tbDireccion.tbCanton.idProvincia;

            var _tbCanton = ((dynamic)cmbProvincia.SelectedItem).tbCanton;
            cmbCanton.DataSource = _tbCanton;
            cmbCanton.ValueMember = "idCanton";
            cmbCanton.DisplayMember = "descripcion";
            if (_tbDireccion.idCanton > 0) cmbCanton.SelectedValue = _tbDireccion.idCanton;

            var _tbTipoDireccion = (from TD in _dbCosolemEntities.tbTipoDireccion select new { idTipoDireccion = TD.idTipoDireccion, descripcion = TD.descripcion }).ToList();
            cmbTipoDireccion.DataSource = _tbTipoDireccion;
            cmbTipoDireccion.ValueMember = "idTipoDireccion";
            cmbTipoDireccion.DisplayMember = "descripcion";
            if (_tbDireccion.idTipoDireccion > 0) cmbTipoDireccion.SelectedValue = _tbDireccion.idTipoDireccion;

            txtDireccionCompleta.Text = _tbDireccion.direccionCompleta;
            txtReferencia.Text = _tbDireccion.referencia;

            var _tbTipoTelefono = (from TT in _dbCosolemEntities.tbTipoTelefono select new { idTipoTelefono = TT.idTipoTelefono, descripcion = TT.descripcion }).ToList();
            cmbTipoTelefono.DataSource = _tbTipoTelefono;
            cmbTipoTelefono.ValueMember = "idTipoTelefono";
            cmbTipoTelefono.DisplayMember = "descripcion";
            this._tbTipoTelefono = _tbTipoTelefono.FirstOrDefault();

            var _tbOperadora = (from O in _dbCosolemEntities.tbOperadora select new { idOperadora = O.idOperadora, descripcion = O.descripcion }).ToList();
            cmbOperadora.DataSource = _tbOperadora;
            cmbOperadora.ValueMember = "idOperadora";
            cmbOperadora.DisplayMember = "descripcion";
            this._tbOperadora = _tbOperadora.FirstOrDefault();

            dgvTelefonos.AutoGenerateColumns = false;
            _BindingListtbTelefono = new BindingList<tbTelefono>(_tbDireccion.tbTelefono.ToList());
            dgvTelefonos.DataSource = _BindingListtbTelefono;
            InactivarRegistros();
        }

        private void cmbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var _tbCanton = ((dynamic)cmbProvincia.SelectedItem).tbCanton;

            cmbCanton.DataSource = _tbCanton;
            cmbCanton.ValueMember = "idCanton";
            cmbCanton.DisplayMember = "descripcion";
        }

        private void btnAgregarTelefonos_Click(object sender, EventArgs e)
        {
            _BindingListtbTelefono.Add(new tbTelefono { idTipoTelefono = _tbTipoTelefono.idTipoTelefono, idOperadora = _tbOperadora.idOperadora, estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });
            InactivarRegistros();
        }

        private void dgvTelefonos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == imgEliminar.Index)
                {
                    if (MessageBox.Show("¿Seguro desea eliminar el registro seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        tbTelefono _tbTelefono = (tbTelefono)dgvTelefonos.CurrentRow.DataBoundItem;
                        _tbTelefono.estadoRegistro = false;
                        _tbTelefono.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbTelefono.idUsuarioUltimaModificacion = idUsuario;
                        _tbTelefono.terminalUltimaModificacion = Program.terminal;
                        _tbTelefono.fechaHoraEliminacion = Program.fechaHora;
                        _tbTelefono.idUsuarioEliminacion = idUsuario;
                        _tbTelefono.terminalEliminacion = Program.terminal;
                        if (_tbTelefono.idTelefono == 0) _BindingListtbTelefono.Remove(_tbTelefono);
                        InactivarRegistros();
                    }
                }
            }
        }

        private string VerificaTelefono()
        {
            string mensaje = String.Empty;
            if (dgvTelefonos.Rows.Count == 0)
                mensaje += "Ingrese al menos 1 télefono para la dirección\n";
            else
            {
                List<tbTelefono> _listtbTelefono = (from T in dgvTelefonos.Rows.Cast<DataGridViewRow>() select (tbTelefono)T.DataBoundItem).ToList();
                if (_listtbTelefono.Where(x => String.IsNullOrEmpty(x.numero ?? String.Empty)).Count() > 0)
                    mensaje += "El número de teléfono es necesario, favor verificar\n";
            }
            return mensaje;
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            dgvTelefonos_CellEndEdit(null, null);
            string mensaje = String.Empty;
            if (String.IsNullOrEmpty(txtDireccionCompleta.Text.Trim())) mensaje += "Ingrese dirección completa\n";
            mensaje += VerificaTelefono();

            if (String.IsNullOrEmpty(mensaje))
            {
                dynamic _tbCanton = ((dynamic)cmbCanton.SelectedItem);
                dynamic _tbTipoDireccion = ((dynamic)cmbTipoDireccion.SelectedItem);

                tbDireccion _tbDireccion = new tbDireccion
                {
                    idCanton = _tbCanton.idCanton,
                    idTipoDireccion = _tbTipoDireccion.idTipoDireccion,
                    direccionCompleta = txtDireccionCompleta.Text,
                    referencia = txtReferencia.Text,
                    estadoRegistro = true,
                    descripcionProvincia = ((dynamic)cmbProvincia.SelectedItem).descripcion,
                    descripcionCanton = _tbCanton.descripcion,
                    descripcionTipoDireccion = _tbTipoDireccion.descripcion
                };

                if (this._tbDireccion.idDireccion == 0)
                {
                    this._tbDireccion.fechaHoraIngreso = Program.fechaHora;
                    this._tbDireccion.idUsuarioIngreso = idUsuario;
                    this._tbDireccion.terminalIngreso = Program.terminal;
                }
                else
                {
                    if (_tbDireccion.idCanton != this._tbDireccion.idCanton || _tbDireccion.idTipoDireccion != this._tbDireccion.idTipoDireccion || _tbDireccion.direccionCompleta != this._tbDireccion.direccionCompleta || _tbDireccion.referencia != this._tbDireccion.referencia || _tbDireccion.estadoRegistro != this._tbDireccion.estadoRegistro)
                    {
                        this._tbDireccion.fechaHoraUltimaModificacion = Program.fechaHora;
                        this._tbDireccion.idUsuarioUltimaModificacion = idUsuario;
                        this._tbDireccion.terminalUltimaModificacion = Program.terminal;
                    }
                }
                this._tbDireccion.idCanton = _tbDireccion.idCanton;
                this._tbDireccion.idTipoDireccion = _tbDireccion.idTipoDireccion;
                this._tbDireccion.direccionCompleta = _tbDireccion.direccionCompleta;
                this._tbDireccion.referencia = _tbDireccion.referencia;
                this._tbDireccion.estadoRegistro = _tbDireccion.estadoRegistro;
                this._tbDireccion.descripcionProvincia = _tbDireccion.descripcionProvincia;
                this._tbDireccion.descripcionCanton = _tbDireccion.descripcionCanton;
                this._tbDireccion.descripcionTipoDireccion = _tbDireccion.descripcionTipoDireccion;

                _BindingListtbTelefono.ToList().ForEach(x => this._tbDireccion.tbTelefono.Add(x));

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro desea eliminar el registro?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                _tbDireccion.estadoRegistro = false;
                _tbDireccion.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbDireccion.idUsuarioUltimaModificacion = idUsuario;
                _tbDireccion.terminalUltimaModificacion = Program.terminal;
                _tbDireccion.fechaHoraEliminacion = Program.fechaHora;
                _tbDireccion.idUsuarioEliminacion = idUsuario;
                _tbDireccion.terminalEliminacion = Program.terminal;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void dgvTelefonos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvTelefonos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            foreach (DataGridViewRow _DataGridViewRow in dgvTelefonos.Rows)
            {
                tbTelefono _tbTelefono = (tbTelefono)_DataGridViewRow.DataBoundItem;
                if (_tbTelefono.EntityState == EntityState.Modified)
                {
                    _tbTelefono.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbTelefono.idUsuarioUltimaModificacion = idUsuario;
                    _tbTelefono.terminalUltimaModificacion = Program.terminal;
                }
            }
            InactivarRegistros();
        }

        private void dgvTelefonos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (new List<int> { txtNumero.Index, txtExtension.Index }.Contains(dgvTelefonos.CurrentCell.ColumnIndex))
            {
                if (e.Control is TextBox)
                {
                    TextBox textBox = (TextBox)e.Control;
                    textBox.KeyPress -= textBox_KeyPress;
                    textBox.KeyPress += textBox_KeyPress;
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
