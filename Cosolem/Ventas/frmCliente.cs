using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects.DataClasses;

namespace Cosolem
{
    public partial class frmCliente : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class TipoPersona
        {
            public int idTipoPersona { get; set; }
            public string descripcion { get; set; }
        }

        class TipoIdentificacion
        {
            public int idTipoIdentificacion { get; set; }
            public string descripcion { get; set; }
            public int cantidadCaracteres { get; set; }
        }

        tbPersona _tbPersona = null;
        BindingList<tbDireccion> _BindingListtbDireccion = null;
        BindingList<tbTelefonoPersonal> _BindingListtbTelefonoPersonal = null;
        dynamic _tbTipoTelefono = null;
        dynamic _tbOperadora = null;

        public frmCliente()
        {
            InitializeComponent();
        }

        private void InactivarRegistros(ref DataGridView _DataGridView)
        {
            CurrencyManager _CurrencyManager = (CurrencyManager)BindingContext[_DataGridView.DataSource];
            _CurrencyManager.SuspendBinding();
            foreach (DataGridViewRow _DataGridViewRow in _DataGridView.Rows)
            {
                dynamic _dynamic = (dynamic)_DataGridViewRow.DataBoundItem;
                _DataGridViewRow.Visible = _dynamic.estadoRegistro;
            }
            _CurrencyManager.ResumeBinding();
        }

        private void SetearPersona(tbPersona _tbPersona)
        {
            try
            {
                this._tbPersona = _tbPersona;
                if (this._tbPersona.tbCliente == null)
                {
                    this._tbPersona.tbCliente = new tbCliente { estadoRegistro = true };
                    MessageBox.Show("Cliente no se encuentra registrado, favor completar los datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                cmbTipoPersona.SelectedValue = this._tbPersona.idTipoPersona;
                cmbTipoPersona_SelectionChangeCommitted(null, null);
                cmbTipoIdentificacion.SelectedValue = this._tbPersona.idTipoIdentificacion;
                txtNumeroIdentificacion.Text = this._tbPersona.numeroIdentificacion;
                txtPrimerNombre.Text = this._tbPersona.primerNombre;
                txtSegundoNombre.Text = this._tbPersona.segundoNombre;
                txtApellidoPaterno.Text = this._tbPersona.apellidoPaterno;
                txtApellidoMaterno.Text = this._tbPersona.apellidoMaterno;
                txtRazonSocial.Text = this._tbPersona.razonSocial;
                cmbSexo.SelectedValue = this._tbPersona.idSexo;
                dtpFechaNacimiento.Checked = false;
                if (this._tbPersona.fechaNacimiento.HasValue)
                    dtpFechaNacimiento.Value = this._tbPersona.fechaNacimiento.Value;
                cmbEstadoCivil.SelectedValue = this._tbPersona.idEstadoCivil;
                txtCorreoElectronico.Text = this._tbPersona.correoElectronico;
                _BindingListtbDireccion.Clear();
                this._tbPersona.tbDireccion.Where(x => x.estadoRegistro).ToList().ForEach(x =>
                {
                    x.descripcionProvincia = x.tbCanton.tbProvincia.descripcion;
                    x.descripcionCanton = x.tbCanton.descripcion;
                    x.descripcionTipoDireccion = x.tbTipoDireccion.descripcion;
                    _BindingListtbDireccion.Add(x);
                });
                _BindingListtbTelefonoPersonal.Clear();
                this._tbPersona.tbTelefonoPersonal.Where(x => x.estadoRegistro).ToList().ForEach(x => _BindingListtbTelefonoPersonal.Add(x));
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            Util.ObtenerControles(this, typeof(TextBox)).ForEach(x =>
            {
                TextBox textBox = (TextBox)x;
                if (!textBox.Name.ToLower().Contains("correo")) textBox.CharacterCasing = CharacterCasing.Upper;
            });

            tabControl1.SelectedTab = tabPage1;

            _tbPersona = new tbPersona { estadoRegistro = true, tbCliente = new tbCliente { estadoRegistro = true } };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbPersona, EntityState.Detached);

            cmbTipoPersona.DataSource = (from TP in _dbCosolemEntities.tbTipoPersona select new TipoPersona { idTipoPersona = TP.idTipoPersona, descripcion = TP.descripcion }).ToList();
            cmbTipoPersona.ValueMember = "idTipoPersona";
            cmbTipoPersona.DisplayMember = "descripcion";
            cmbTipoPersona_SelectionChangeCommitted(null, null);

            txtNumeroIdentificacion.Clear();
            txtPrimerNombre.Clear();
            txtSegundoNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();

            cmbSexo.DataSource = (from S in _dbCosolemEntities.tbSexo select new { idSexo = S.idSexo, descripcion = S.descripcion }).ToList();
            cmbSexo.ValueMember = "idSexo";
            cmbSexo.DisplayMember = "descripcion";

            dtpFechaNacimiento.Value = Program.fechaHora;

            cmbEstadoCivil.DataSource = (from EC in _dbCosolemEntities.tbEstadoCivil select new { idEstadoCivil = EC.idEstadoCivil, descripcion = EC.descripcion }).ToList();
            cmbEstadoCivil.ValueMember = "idEstadoCivil";
            cmbEstadoCivil.DisplayMember = "descripcion";

            txtCorreoElectronico.Clear();

            dgvDireccionesTelefonos.AutoGenerateColumns = false;
            _BindingListtbDireccion = new BindingList<tbDireccion>(_tbPersona.tbDireccion.ToList());
            dgvDireccionesTelefonos.DataSource = _BindingListtbDireccion;

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

            dgvTelefonosPersonales.AutoGenerateColumns = false;
            _BindingListtbTelefonoPersonal = new BindingList<tbTelefonoPersonal>(_tbPersona.tbTelefonoPersonal.ToList());
            dgvTelefonosPersonales.DataSource = _BindingListtbTelefonoPersonal;

            txtNumeroIdentificacion.Select();
        }

        private void btnAgregarDireccionesTelefonos_Click(object sender, EventArgs e)
        {
            tbDireccion _tbDireccion = new tbDireccion();
            frmDireccionTelefono _frmDireccion = new frmDireccionTelefono(ref _tbDireccion);
            if (_frmDireccion.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (_tbDireccion.estadoRegistro) _BindingListtbDireccion.Add(_tbDireccion);
                InactivarRegistros(ref dgvDireccionesTelefonos);
            }
        }

        private void dgvDireccionesTelefonos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!new List<int> { imgEliminarDireccionTelefono.Index, chbDireccionPrincipal.Index }.Contains(e.ColumnIndex))
                {
                    tbDireccion _tbDireccion = (tbDireccion)dgvDireccionesTelefonos.CurrentRow.DataBoundItem;
                    new frmDireccionTelefono(ref _tbDireccion).ShowDialog();
                    if (_tbDireccion.idDireccion == 0 && !_tbDireccion.estadoRegistro) _BindingListtbDireccion.Remove(_tbDireccion);
                    InactivarRegistros(ref dgvDireccionesTelefonos);
                }
            }
        }

        private void dgvDireccionesTelefonos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == imgEliminarDireccionTelefono.Index)
                {
                    if (MessageBox.Show("¿Seguro desea eliminar el registro seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        tbDireccion _tbDireccion = (tbDireccion)dgvDireccionesTelefonos.CurrentRow.DataBoundItem;
                        _tbDireccion.estadoRegistro = false;
                        if (_tbDireccion.idDireccion == 0) _BindingListtbDireccion.Remove(_tbDireccion);
                        InactivarRegistros(ref dgvDireccionesTelefonos);
                    }
                }
            }
        }

        private string VerificaDireccion()
        {
            string mensaje = String.Empty;
            if (dgvDireccionesTelefonos.RowCount == 0)
                mensaje += "Ingrese al menos 1 dirección y teléfono\n";
            else
            {
                List<tbDireccion> _listtbDireccion = (from D in dgvDireccionesTelefonos.Rows.Cast<DataGridViewRow>() where Convert.ToBoolean(D.Cells[chbDireccionPrincipal.Index].Value) select (tbDireccion)D.DataBoundItem).ToList();
                if (_listtbDireccion.Count == 0)
                    mensaje += "Seleccione al menos 1 dirección como principal\n";
            }
            return mensaje;
        }

        private string VerificaTelefonoPersonal()
        {
            string mensaje = String.Empty;
            if (dgvTelefonosPersonales.Rows.Count == 0)
                mensaje += "Ingrese al menos 1 teléfono personal\n";
            else
            {
                List<tbTelefonoPersonal> _listtbTelefonoPersonal = (from TP in dgvTelefonosPersonales.Rows.Cast<DataGridViewRow>() select (tbTelefonoPersonal)TP.DataBoundItem).ToList();
                if (_listtbTelefonoPersonal.Where(x => String.IsNullOrEmpty(x.numero ?? String.Empty)).Count() > 0)
                    mensaje += "El número de teléfono personal es necesario, favor verificar\n";
            }
            return mensaje;
        }

        private string VerificaPersona(int idTipoPersona, int idTipoIdentificacion, string numeroIdentificacion)
        {
            string mensaje = String.Empty;
            if (this._tbPersona.idPersona == 0)
            {
                tbPersona _tbPersona = (from P in _dbCosolemEntities.tbPersona where P.idTipoPersona == idTipoPersona && P.idTipoIdentificacion == idTipoIdentificacion && P.numeroIdentificacion == numeroIdentificacion select P).FirstOrDefault();
                if (_tbPersona != null)
                {
                    if (_tbPersona.tbCliente != null)
                        mensaje += "Persona se encuentra registrada, favor verificar\n";
                }
            }
            return mensaje;
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDireccionesTelefonos_CellEndEdit(null, null);
                dgvTelefonosPersonales_CellEndEdit(null, null);

                string mensajeDatosPersonales = String.Empty;

                if (String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) mensajeDatosPersonales += "Ingrese número de identificación\n";
                if (!String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) mensajeDatosPersonales += VerificaPersona(((TipoPersona)cmbTipoPersona.SelectedItem).idTipoPersona, ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion, txtNumeroIdentificacion.Text.Trim());
                TipoPersona _tbTipoPersona = (TipoPersona)cmbTipoPersona.SelectedItem;
                if (_tbTipoPersona.idTipoPersona == 1)
                {
                    if (String.IsNullOrEmpty(txtPrimerNombre.Text.Trim())) mensajeDatosPersonales += "Ingrese primer nombre\n";
                    if (String.IsNullOrEmpty(txtApellidoPaterno.Text.Trim())) mensajeDatosPersonales += "Ingrese apellido paterno\n";
                    if (String.IsNullOrEmpty(txtApellidoMaterno.Text.Trim())) mensajeDatosPersonales += "Ingrese apellido materno\n";
                }
                if (_tbTipoPersona.idTipoPersona == 2) if (String.IsNullOrEmpty(txtRazonSocial.Text.Trim())) mensajeDatosPersonales += "Ingrese razón social\n";
                if (String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) mensajeDatosPersonales += "Ingrese correo electrónico\n";
                if (!String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) if (!Util.ValidaEmail(txtCorreoElectronico.Text.Trim())) mensajeDatosPersonales += "Correo electrónico inválido, favor verificar\n";
                mensajeDatosPersonales += VerificaDireccion();
                mensajeDatosPersonales += VerificaTelefonoPersonal();
                if (!String.IsNullOrEmpty(mensajeDatosPersonales)) mensajeDatosPersonales = "[Datos personales]\n" + mensajeDatosPersonales;

                string mensaje = mensajeDatosPersonales;

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    _tbPersona.idTipoPersona = _tbTipoPersona.idTipoPersona;
                    _tbPersona.idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                    _tbPersona.numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
                    _tbPersona.primerNombre = txtPrimerNombre.Text.Trim();
                    _tbPersona.segundoNombre = txtSegundoNombre.Text.Trim();
                    _tbPersona.apellidoPaterno = txtApellidoPaterno.Text.Trim();
                    _tbPersona.apellidoMaterno = txtApellidoMaterno.Text.Trim();
                    _tbPersona.razonSocial = txtRazonSocial.Text.Trim();
                    _tbPersona.idSexo = ((dynamic)cmbSexo.SelectedItem).idSexo;
                    _tbPersona.fechaNacimiento = null;
                    if (dtpFechaNacimiento.Checked) _tbPersona.fechaNacimiento = dtpFechaNacimiento.Value.Date;
                    _tbPersona.idEstadoCivil = ((dynamic)cmbEstadoCivil.SelectedItem).idEstadoCivil;
                    _tbPersona.correoElectronico = txtCorreoElectronico.Text.Trim();
                    _BindingListtbDireccion.ToList().ForEach(x =>
                    {
                        if (x.idDireccion == 0) _tbPersona.tbDireccion.Add(x);
                        else x.tbTelefono.ToList().ForEach(y => { if (y.idTelefono == 0) x.tbTelefono.Add(y); });
                    });
                    _BindingListtbTelefonoPersonal.ToList().ForEach(x => { if (x.idTelefonoPersonal == 0) _tbPersona.tbTelefonoPersonal.Add(x); });
                    if (_tbPersona.EntityState == EntityState.Detached)
                    {
                        _tbPersona.fechaHoraIngreso = Program.fechaHora;
                        _tbPersona.idUsuarioIngreso = idUsuario;
                        _tbPersona.terminalIngreso = Program.terminal;
                        _tbPersona.tbCliente.fechaHoraIngreso = Program.fechaHora;
                        _tbPersona.tbCliente.idUsuarioIngreso = idUsuario;
                        _tbPersona.tbCliente.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbPersona.AddObject(_tbPersona);
                    }
                    else
                    {
                        _tbPersona.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbPersona.idUsuarioUltimaModificacion = idUsuario;
                        _tbPersona.terminalUltimaModificacion = Program.terminal;
                        if (!_dbCosolemEntities.tbCliente.Any(x => x.idCliente == _tbPersona.tbCliente.idCliente))
                        {
                            _tbPersona.tbCliente.fechaHoraIngreso = Program.fechaHora;
                            _tbPersona.tbCliente.idUsuarioIngreso = idUsuario;
                            _tbPersona.tbCliente.terminalIngreso = Program.terminal;
                        }
                        else
                        {
                            _tbPersona.tbCliente.fechaHoraUltimaModificacion = Program.fechaHora;
                            _tbPersona.tbCliente.idUsuarioUltimaModificacion = idUsuario;
                            _tbPersona.tbCliente.terminalUltimaModificacion = Program.terminal;
                        }
                    }
                    _dbCosolemEntities.SaveChanges();

                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmCliente_Load(null, null);
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }


        private void btnAgregarTelefonos_Click(object sender, EventArgs e)
        {
            _BindingListtbTelefonoPersonal.Add(new tbTelefonoPersonal { idTipoTelefono = _tbTipoTelefono.idTipoTelefono, idOperadora = _tbOperadora.idOperadora, estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });
            InactivarRegistros(ref dgvTelefonosPersonales);
        }

        private void dgvTelefonosPersonales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == imgEliminarTelefonoPersonal.Index)
                {
                    if (MessageBox.Show("¿Seguro desea eliminar el registro seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        tbTelefonoPersonal _tbTelefonoPersonal = (tbTelefonoPersonal)dgvTelefonosPersonales.CurrentRow.DataBoundItem;
                        _tbTelefonoPersonal.estadoRegistro = false;
                        _tbTelefonoPersonal.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbTelefonoPersonal.idUsuarioUltimaModificacion = idUsuario;
                        _tbTelefonoPersonal.terminalUltimaModificacion = Program.terminal;
                        _tbTelefonoPersonal.fechaHoraEliminacion = Program.fechaHora;
                        _tbTelefonoPersonal.idUsuarioEliminacion = idUsuario;
                        _tbTelefonoPersonal.terminalEliminacion = Program.terminal;
                        if (_tbTelefonoPersonal.idTelefonoPersonal == 0) _BindingListtbTelefonoPersonal.Remove(_tbTelefonoPersonal);
                        InactivarRegistros(ref dgvTelefonosPersonales);
                    }
                }
            }
        }

        private void dgvTelefonosPersonales_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvTelefonosPersonales.CommitEdit(DataGridViewDataErrorContexts.Commit);
            foreach (DataGridViewRow _DataGridViewRow in dgvTelefonosPersonales.Rows)
            {
                tbTelefonoPersonal _tbTelefonoPersonal = (tbTelefonoPersonal)_DataGridViewRow.DataBoundItem;
                if (_tbTelefonoPersonal.EntityState == EntityState.Modified)
                {
                    _tbTelefonoPersonal.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbTelefonoPersonal.idUsuarioUltimaModificacion = idUsuario;
                    _tbTelefonoPersonal.terminalUltimaModificacion = Program.terminal;
                }
            }
            InactivarRegistros(ref dgvTelefonosPersonales);
        }

        private void dgvDireccionesTelefonos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvDireccionesTelefonos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            foreach (DataGridViewRow _DataGridViewRow in dgvDireccionesTelefonos.Rows)
            {
                tbDireccion _tbDireccion = (tbDireccion)_DataGridViewRow.DataBoundItem;
                if (_tbDireccion.EntityState == EntityState.Modified)
                {
                    _tbDireccion.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbDireccion.idUsuarioUltimaModificacion = idUsuario;
                    _tbDireccion.terminalUltimaModificacion = Program.terminal;
                }
            }
            InactivarRegistros(ref dgvDireccionesTelefonos);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbPersona.idPersona == 0)
                MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbPersona.tbCliente.estadoRegistro = false;
                _tbPersona.tbCliente.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbPersona.tbCliente.idUsuarioUltimaModificacion = idUsuario;
                _tbPersona.tbCliente.terminalUltimaModificacion = Program.terminal;
                _tbPersona.tbCliente.fechaHoraEliminacion = Program.fechaHora;
                _tbPersona.tbCliente.idUsuarioEliminacion = idUsuario;
                _tbPersona.tbCliente.terminalEliminacion = Program.terminal;

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmCliente_Load(null, null);
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmCliente_Load(null, null);
        }

        private void txtNumeroCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtNumeroIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
                tbPersona _tbPersona = (from P in _dbCosolemEntities.tbPersona where P.numeroIdentificacion == numeroIdentificacion select P).FirstOrDefault();
                if (_tbPersona != null) SetearPersona(_tbPersona);
                else MessageBox.Show("Persona no se encuentra registrada, favor verificar", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                if (new List<int> { 1, 2 }.Contains(idTipoIdentificacion))
                {
                    if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                    {
                        e.Handled = true;
                        return;
                    }
                }
                else
                {
                    if (!(char.IsLetter(e.KeyChar)) && !(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }

        private void dgvTelefonosPersonales_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (new List<int> { txtNumeroTelefonoPersonal.Index, txtExtensionTelefonoPersonal.Index }.Contains(dgvTelefonosPersonales.CurrentCell.ColumnIndex))
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

        private void cmbTipoIdentificacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtNumeroIdentificacion.MaxLength = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).cantidadCaracteres;
            txtNumeroIdentificacion.Clear();
            txtNumeroIdentificacion.Select();
        }

        private void cmbTipoPersona_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TipoPersona _tbTipoPersona = (TipoPersona)cmbTipoPersona.SelectedItem;
            List<TipoIdentificacion> _tbTipoIdentificacion = (from TI in _dbCosolemEntities.tbTipoIdentificacion select new TipoIdentificacion { idTipoIdentificacion = TI.idTipoIdentificacion, descripcion = TI.descripcion, cantidadCaracteres = TI.cantidadCaracteres }).ToList();
            if (_tbTipoPersona.idTipoPersona == 2) _tbTipoIdentificacion = _tbTipoIdentificacion.Where(x => x.idTipoIdentificacion == 2).ToList();
            cmbTipoIdentificacion.DataSource = _tbTipoIdentificacion;
            cmbTipoIdentificacion.ValueMember = "idTipoIdentificacion";
            cmbTipoIdentificacion.DisplayMember = "descripcion";

            HabilitarDatosTipoPersona();

            cmbTipoIdentificacion_SelectionChangeCommitted(null, null);
        }

        private void HabilitarDatosTipoPersona()
        {
            txtPrimerNombre.Clear();
            txtSegundoNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
            txtRazonSocial.Clear();

            txtPrimerNombre.Enabled = true;
            txtSegundoNombre.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            txtRazonSocial.Enabled = false;
            TipoPersona _tbTipoPersona = (TipoPersona)cmbTipoPersona.SelectedItem;

            if (_tbTipoPersona.idTipoPersona == 2)
            {
                txtPrimerNombre.Enabled = false;
                txtSegundoNombre.Enabled = false;
                txtApellidoPaterno.Enabled = false;
                txtApellidoMaterno.Enabled = false;
                txtRazonSocial.Enabled = true;
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Tipo de persona"), new DataColumn("Tipo de identificación"), new DataColumn("Número de identificación"), new DataColumn("Nombre completo"), new DataColumn("Es cliente"), new DataColumn("Fecha de registro"), new DataColumn("persona", typeof(object)) });

            (from P in _dbCosolemEntities.tbPersona
             from C in _dbCosolemEntities.tbCliente.Where(x => x.idCliente == P.idPersona && x.estadoRegistro == P.estadoRegistro).DefaultIfEmpty()
             where P.estadoRegistro
             select new
             {
                 tipoPersona = P.tbTipoPersona.descripcion,
                 tipoIdentificacion = P.tbTipoIdentificacion.descripcion,
                 numeroIdentificacion = P.numeroIdentificacion,
                 nombreCompleto = P.nombreCompleto,
                 esCliente = (C == null ? "No" : "Sí"),
                 fechaRegistro = (C == null ? P.fechaHoraIngreso : C.fechaHoraIngreso),
                 persona = P
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.tipoPersona, x.tipoIdentificacion, x.numeroIdentificacion, x.nombreCompleto, x.esCliente, x.fechaRegistro, x.persona));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearPersona((tbPersona)_frmBusqueda._object);
        }
    }
}