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
    public partial class frmEmpleado : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Empresa
        {
            public long idEmpresa { get; set; }
            public string razonSocial { get; set; }
            public IEnumerable<Tienda> tbTienda { get; set; }
            public IEnumerable<Departamento> tbDepartamento { get; set; }
        }

        class TipoIdentificacion
        {
            public int idTipoIdentificacion { get; set; }
            public string descripcion { get; set; }
            public int cantidadCaracteres { get; set; }
        }

        class Tienda
        {
            public long idTienda { get; set; }
            public string descripcion { get; set; }
        }

        class Departamento
        {
            public long idDepartamento { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<Cargo> tbCargo { get; set; }
        }

        class Cargo
        {
            public long idCargo { get; set; }
            public string descripcion { get; set; }
        }

        class TipoContrato
        {
            public int idTipoContrato { get; set; }
            public string descripcion { get; set; }
        }

        class TipoNomina
        {
            public int idTipoNomina { get; set; }
            public string descripcion { get; set; }
        }

        class MedioCancelacion
        {
            public int idMedioCancelacion { get; set; }
            public string descripcion { get; set; }
            public bool exigirDatoBancario { get; set; }
        }

        class EntidadBancaria
        {
            public int idEntidadBancaria { get; set; }
            public string descripcion { get; set; }
        }

        tbPersona _tbPersona = null;
        BindingList<tbDireccion> _BindingListtbDireccion = null;
        BindingList<tbTelefonoPersonal> _BindingListtbTelefonoPersonal = null;
        BindingList<tbFormacionAcademica> _BindingListtbFormacionAcademica = null;
        BindingList<tbHorarioLaboral> _BindingListtbHorarioLaboral = null;
        dynamic _tbTipoTelefono = null;
        dynamic _tbOperadora = null;

        public frmEmpleado()
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
                tsbEliminar.Enabled = true;
                this._tbPersona = _tbPersona;
                if (this._tbPersona.tbEmpleado == null)
                {
                    this._tbPersona.tbEmpleado = new tbEmpleado { estadoRegistro = true };
                    MessageBox.Show("Empleado no se encuentra registrado, favor completar los datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tsbEliminar.Enabled = false;
                }
                cmbEmpresa.SelectedValue = this._tbPersona.tbEmpleado.idEmpresa;
                cmbEmpresa_SelectionChangeCommitted(null, null);
                cmbTienda.SelectedValue = this._tbPersona.tbEmpleado.idTienda;
                cmbTipoIdentificacion.SelectedValue = this._tbPersona.idTipoIdentificacion;
                cmbTipoIdentificacion_SelectionChangeCommitted(null, null);
                txtNumeroIdentificacion.Text = this._tbPersona.numeroIdentificacion;
                txtPrimerNombre.Text = this._tbPersona.primerNombre;
                txtSegundoNombre.Text = this._tbPersona.segundoNombre;
                txtApellidoPaterno.Text = this._tbPersona.apellidoPaterno;
                txtApellidoMaterno.Text = this._tbPersona.apellidoMaterno;
                cmbSexo.SelectedValue = this._tbPersona.idSexo;
                dtpFechaNacimiento.Value = this._tbPersona.fechaNacimiento ?? Program.fechaHora.Date;
                cmbEstadoCivil.SelectedValue = this._tbPersona.idEstadoCivil;
                txtCorreoElectronicoPersonal.Text = this._tbPersona.correoElectronico;
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
                _BindingListtbFormacionAcademica.Clear();
                this._tbPersona.tbFormacionAcademica.Where(x => x.estadoRegistro).ToList().ForEach(x =>
                {
                    x.descripcionProvincia = x.tbCanton.tbProvincia.descripcion;
                    x.descripcionCanton = x.tbCanton.descripcion;
                    x.descripcionTipoFormacionAcademica = x.tbTipoFormacionAcademica.descripcion;
                    _BindingListtbFormacionAcademica.Add(x);
                });
                cmbTipoContrato.SelectedValue = this._tbPersona.tbEmpleado.idTipoContrato;
                chbEsAfiliado.Checked = this._tbPersona.tbEmpleado.esAfiliado;
                long idDepartamento = 0;
                if (this._tbPersona.tbEmpleado.tbCargo != null) idDepartamento = this._tbPersona.tbEmpleado.tbCargo.idDepartamento;
                cmbDepartamento.SelectedValue = idDepartamento;
                cmbDepartamento_SelectionChangeCommitted(null, null);
                cmbCargo.SelectedValue = this._tbPersona.tbEmpleado.idCargo;
                dtpAvisoEntrada.Value = this._tbPersona.tbEmpleado.avisoEntrada ?? Program.fechaHora;
                _BindingListtbHorarioLaboral.Clear();
                (from DS in _dbCosolemEntities.tbDiaSemana select DS).ToList().ForEach(x =>
                {
                    tbHorarioLaboral _tbHorarioLaboral = this._tbPersona.tbEmpleado.tbHorarioLaboral.Where(y => y.idDiaSemana == x.idDiaSemana).FirstOrDefault();
                    if (_tbHorarioLaboral != null)
                    {
                        _tbHorarioLaboral.descripcionDiaSemana = x.nombreDiaSemana;
                        _BindingListtbHorarioLaboral.Add(_tbHorarioLaboral);
                    }
                    else
                        _BindingListtbHorarioLaboral.Add(new tbHorarioLaboral { estadoRegistro = false, idEmpleado = 0, idHorarioLaboral = 0, idDiaSemana = x.idDiaSemana, descripcionDiaSemana = x.nombreDiaSemana, horaEntrada = new TimeSpan(0, 0, 0), horaSalida = new TimeSpan(0, 0, 0), idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal });
                });
                if (this._tbPersona.tbEmpleado.foto != null) pbxFoto.Image = Util.ObtenerImagen(this._tbPersona.tbEmpleado.foto);
                txtSalarioBruto.Text = this._tbPersona.tbEmpleado.salarioBruto.ToString(Application.CurrentCulture.NumberFormat);
                txtBonificacion.Text = this._tbPersona.tbEmpleado.bonificacion.ToString(Application.CurrentCulture.NumberFormat);
                cmbTipoNomina.SelectedValue = this._tbPersona.tbEmpleado.idTipoNomina;
                cmbMedioCancelacion.SelectedValue = this._tbPersona.tbEmpleado.idMedioCancelacion;
                cmbMedioCancelacion_SelectionChangeCommitted(null, null);
                cmbEntidadBancaria.SelectedValue = this._tbPersona.tbEmpleado.idEntidadBancaria ?? 0;
                txtNumeroCuenta.Text = this._tbPersona.tbEmpleado.numeroCuenta;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        public List<Control> ObtenerControles(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(x => ObtenerControles(x, type)).Concat(controls).Where(y => y.GetType() == type).ToList();
        }

        private void frmEmpleado_Load(object sender, EventArgs e)
        {
            try
            {
                _dbCosolemEntities = new dbCosolemEntities();

                tsbEliminar.Enabled = true;

                ObtenerControles(this, typeof(TextBox)).ForEach(x =>
                {
                    TextBox textBox = (TextBox)x;
                    if (!textBox.Name.ToLower().Contains("correo")) textBox.CharacterCasing = CharacterCasing.Upper;
                });

                tabControl1.SelectedTab = tabPage1;

                _tbPersona = new tbPersona { idTipoPersona = 1, estadoRegistro = true, tbEmpleado = new tbEmpleado { estadoRegistro = true } };
                _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbPersona, EntityState.Detached);

                List<Empresa> _tbEmpresa = (from E in _dbCosolemEntities.tbEmpresa select new Empresa { idEmpresa = E.idEmpresa, razonSocial = E.ruc + " - " + E.razonSocial, tbTienda = (from T in E.tbTienda select new Tienda { idTienda = T.idTienda, descripcion = T.descripcion }), tbDepartamento = (from D in E.tbDepartamento select new Departamento { idDepartamento = D.idDepartamento, descripcion = D.descripcion, tbCargo = (from C in D.tbCargo select new Cargo { idCargo = C.idCargo, descripcion = C.descripcion }) }) }).ToList();
                _tbEmpresa.Insert(0, new Empresa { idEmpresa = 0, razonSocial = "Seleccione", tbTienda = new List<Tienda> { new Tienda { idTienda = 0, descripcion = "Seleccione" } }, tbDepartamento = new List<Departamento> { new Departamento { idDepartamento = 0, descripcion = "Seleccione", tbCargo = new List<Cargo> { new Cargo { idCargo = 0, descripcion = "Seleccione" } } } } });
                cmbEmpresa.DataSource = _tbEmpresa;
                cmbEmpresa.ValueMember = "idEmpresa";
                cmbEmpresa.DisplayMember = "razonSocial";
                cmbEmpresa_SelectionChangeCommitted(null, null);

                cmbTipoIdentificacion.DataSource = (from TI in _dbCosolemEntities.tbTipoIdentificacion where TI.idTipoIdentificacion != 2 select new TipoIdentificacion { idTipoIdentificacion = TI.idTipoIdentificacion, descripcion = TI.descripcion, cantidadCaracteres = TI.cantidadCaracteres }).ToList();
                cmbTipoIdentificacion.ValueMember = "idTipoIdentificacion";
                cmbTipoIdentificacion.DisplayMember = "descripcion";
                cmbTipoIdentificacion_SelectionChangeCommitted(null, null);

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

                txtCorreoElectronicoPersonal.Clear();

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

                dgvFormacionesAcademicas.AutoGenerateColumns = false;
                _BindingListtbFormacionAcademica = new BindingList<tbFormacionAcademica>(_tbPersona.tbFormacionAcademica.ToList());
                dgvFormacionesAcademicas.DataSource = _BindingListtbFormacionAcademica;

                List<TipoContrato> _tbTipoContrato = (from TC in _dbCosolemEntities.tbTipoContrato select new TipoContrato { idTipoContrato = TC.idTipoContrato, descripcion = TC.descripcion }).ToList();
                _tbTipoContrato.Insert(0, new TipoContrato { idTipoContrato = 0, descripcion = "Seleccione" });
                cmbTipoContrato.DataSource = _tbTipoContrato;
                cmbTipoContrato.ValueMember = "idTipoContrato";
                cmbTipoContrato.DisplayMember = "descripcion";

                chbEsAfiliado.Checked = false;
                dtpAvisoEntrada.Value = Program.fechaHora;

                BindingSource _BindingSourceHoraEntrada = new BindingSource();
                _BindingSourceHoraEntrada.DataSource = Util.ObtenerHorarioLaboral();
                BindingSource _BindingSourceHoraSalida = new BindingSource();
                _BindingSourceHoraSalida.DataSource = Util.ObtenerHorarioLaboral();

                cmbHoraEntrada.DataSource = _BindingSourceHoraEntrada;
                cmbHoraEntrada.ValueMember = "hora";
                cmbHoraEntrada.DisplayMember = "hora";

                cmbHoraSalida.DataSource = _BindingSourceHoraSalida;
                cmbHoraSalida.ValueMember = "hora";
                cmbHoraSalida.DisplayMember = "hora";

                dgvHorarioLaboral.AutoGenerateColumns = false;
                _BindingListtbHorarioLaboral = new BindingList<tbHorarioLaboral>(_tbPersona.tbEmpleado.tbHorarioLaboral.ToList());
                (from DS in _dbCosolemEntities.tbDiaSemana select DS).ToList().ForEach(x => _BindingListtbHorarioLaboral.Add(new tbHorarioLaboral { estadoRegistro = false, idEmpleado = 0, idHorarioLaboral = 0, idDiaSemana = x.idDiaSemana, descripcionDiaSemana = x.nombreDiaSemana, horaEntrada = new TimeSpan(0, 0, 0), horaSalida = new TimeSpan(0, 0, 0), fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal }));
                dgvHorarioLaboral.DataSource = _BindingListtbHorarioLaboral;

                pbxFoto.Image = null;

                txtSalarioBruto.Text = (0M).ToString("n2");
                txtBonificacion.Text = (0M).ToString("n2");

                List<TipoNomina> _tbTipoNomina = (from TN in _dbCosolemEntities.tbTipoNomina select new TipoNomina { idTipoNomina = TN.idTipoNomina, descripcion = TN.descripcion }).ToList();
                _tbTipoNomina.Insert(0, new TipoNomina { idTipoNomina = 0, descripcion = "Seleccione" });
                cmbTipoNomina.DataSource = _tbTipoNomina;
                cmbTipoNomina.ValueMember = "idTipoNomina";
                cmbTipoNomina.DisplayMember = "descripcion";

                List<MedioCancelacion> _tbMedioCancelacion = (from MC in _dbCosolemEntities.tbMedioCancelacion select new MedioCancelacion { idMedioCancelacion = MC.idMedioCancelacion, descripcion = MC.descripcion, exigirDatoBancario = MC.exigirDatoBancario }).ToList();
                _tbMedioCancelacion.Insert(0, new MedioCancelacion { idMedioCancelacion = 0, descripcion = "Seleccione", exigirDatoBancario = false });
                cmbMedioCancelacion.DataSource = _tbMedioCancelacion;
                cmbMedioCancelacion.ValueMember = "idMedioCancelacion";
                cmbMedioCancelacion.DisplayMember = "descripcion";

                cmbMedioCancelacion_SelectionChangeCommitted(null, null);

                List<EntidadBancaria> _tbEntidadBancaria = (from EB in _dbCosolemEntities.tbEntidadBancaria select new EntidadBancaria { idEntidadBancaria = EB.idEntidadBancaria, descripcion = EB.descripcion }).ToList();
                _tbEntidadBancaria.Insert(0, new EntidadBancaria { idEntidadBancaria = 0, descripcion = "Seleccione" });
                cmbEntidadBancaria.DataSource = _tbEntidadBancaria;
                cmbEntidadBancaria.ValueMember = "idEntidadBancaria";
                cmbEntidadBancaria.DisplayMember = "descripcion";

                txtNumeroCuenta.Clear();

                txtNumeroIdentificacion.Select();
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
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

        private string VerificaHorarioLaboral()
        {
            string mensaje = String.Empty;
            List<tbHorarioLaboral> _listtbHorarioLaboral = (from HL in dgvHorarioLaboral.Rows.Cast<DataGridViewRow>() where Convert.ToBoolean(HL.Cells[chbAplicaHorarioLaboral.Index].Value) select (tbHorarioLaboral)HL.DataBoundItem).ToList();
            if (_listtbHorarioLaboral.Count == 0)
                mensaje += "Seleccione al menos 1 día para el horario laboral\n";
            else
            {
                _listtbHorarioLaboral = _listtbHorarioLaboral.Where(x => x.estadoRegistro).ToList();
                foreach (tbHorarioLaboral _tbHorarioLaboral in _listtbHorarioLaboral)
                {
                    TimeSpan totalHoras = (_tbHorarioLaboral.horaSalida - _tbHorarioLaboral.horaEntrada) - new TimeSpan(1, 0, 0);
                    if (totalHoras.TotalHours <= 0)
                        mensaje += "Para el día " + _tbHorarioLaboral.descripcionDiaSemana + " el total de horas es " + totalHoras.TotalHours + ", favor revisar\n";
                }
            }
            return mensaje;
        }

        private string VerificaPersona(int idTipoIdentificacion, string numeroIdentificacion)
        {
            string mensaje = String.Empty;
            if (this._tbPersona.idPersona == 0)
            {
                tbPersona _tbPersona = (from P in _dbCosolemEntities.tbPersona where P.idTipoPersona == 1 && P.idTipoIdentificacion == idTipoIdentificacion && P.numeroIdentificacion == numeroIdentificacion select P).FirstOrDefault();
                if (_tbPersona != null)
                {
                    if (_tbPersona.tbEmpleado != null)
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
                dgvHorarioLaboral_CellEndEdit(null, null);

                string mensajeDatosPersonales = String.Empty;

                if (((Empresa)cmbEmpresa.SelectedItem).idEmpresa == 0) mensajeDatosPersonales += "Seleccione empresa\n";
                if (((Tienda)cmbTienda.SelectedItem).idTienda == 0) mensajeDatosPersonales += "Seleccione tienda\n";
                if (String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) mensajeDatosPersonales += "Ingrese número de identificación\n";
                if (!String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) mensajeDatosPersonales += VerificaPersona(((dynamic)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion, txtNumeroIdentificacion.Text.Trim());
                if (String.IsNullOrEmpty(txtPrimerNombre.Text.Trim())) mensajeDatosPersonales += "Ingrese primer nombre\n";
                if (String.IsNullOrEmpty(txtApellidoPaterno.Text.Trim())) mensajeDatosPersonales += "Ingrese apellido paterno\n";
                if (String.IsNullOrEmpty(txtApellidoMaterno.Text.Trim())) mensajeDatosPersonales += "Ingrese apellido materno\n";
                if (String.IsNullOrEmpty(txtCorreoElectronicoPersonal.Text.Trim())) mensajeDatosPersonales += "Ingrese correo electrónico\n";
                if (!String.IsNullOrEmpty(txtCorreoElectronicoPersonal.Text.Trim())) if (!Util.ValidaEmail(txtCorreoElectronicoPersonal.Text.Trim())) mensajeDatosPersonales += "Correo electrónico inválido, favor verificar\n";
                mensajeDatosPersonales += VerificaDireccion();
                mensajeDatosPersonales += VerificaTelefonoPersonal();
                if (dgvFormacionesAcademicas.RowCount == 0) mensajeDatosPersonales += "Ingrese al menos 1 formación académica\n";
                if (!String.IsNullOrEmpty(mensajeDatosPersonales)) mensajeDatosPersonales = "[Datos personales]\n" + mensajeDatosPersonales;

                string mensajeDatosDeEmpresa = String.Empty;

                mensajeDatosDeEmpresa += VerificaHorarioLaboral();
                if (String.IsNullOrEmpty(txtSalarioBruto.Text.Trim())) mensajeDatosDeEmpresa += "Ingrese salario bruto\n";
                if (!String.IsNullOrEmpty(txtSalarioBruto.Text.Trim()) && Convert.ToDecimal(txtSalarioBruto.Text.Trim()) <= 0) mensajeDatosDeEmpresa += "Ingrese salario bruto mayor a 0\n";
                if (String.IsNullOrEmpty(txtBonificacion.Text.Trim())) mensajeDatosDeEmpresa += "Ingrese bonificación\n";
                if (((TipoNomina)cmbTipoNomina.SelectedItem).idTipoNomina == 0) mensajeDatosDeEmpresa += "Seleccione tipo de nómina\n";
                if (((MedioCancelacion)cmbMedioCancelacion.SelectedItem).idMedioCancelacion == 0) mensajeDatosDeEmpresa += "Seleccione medio de cancelación\n";
                if (((MedioCancelacion)cmbMedioCancelacion.SelectedItem).exigirDatoBancario)
                {
                    if (((EntidadBancaria)cmbEntidadBancaria.SelectedItem).idEntidadBancaria == 0) mensajeDatosDeEmpresa += "Seleccione entidad bancaria\n";
                    if (String.IsNullOrEmpty(txtNumeroCuenta.Text.Trim())) mensajeDatosDeEmpresa += "Ingrese número de cuenta\n";
                }
                if (!String.IsNullOrEmpty(mensajeDatosDeEmpresa)) mensajeDatosDeEmpresa = "[Datos de empresa]\n" + mensajeDatosDeEmpresa;

                string mensaje = mensajeDatosPersonales + "\n" + mensajeDatosDeEmpresa;

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    _tbPersona.idTipoIdentificacion = ((dynamic)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                    _tbPersona.numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
                    _tbPersona.primerNombre = txtPrimerNombre.Text.Trim();
                    _tbPersona.segundoNombre = txtSegundoNombre.Text.Trim();
                    _tbPersona.apellidoPaterno = txtApellidoPaterno.Text.Trim();
                    _tbPersona.apellidoMaterno = txtApellidoMaterno.Text.Trim();
                    _tbPersona.idSexo = ((dynamic)cmbSexo.SelectedItem).idSexo;
                    _tbPersona.fechaNacimiento = dtpFechaNacimiento.Value.Date;
                    _tbPersona.idEstadoCivil = ((dynamic)cmbEstadoCivil.SelectedItem).idEstadoCivil;
                    _tbPersona.correoElectronico = txtCorreoElectronicoPersonal.Text.Trim();
                    _BindingListtbDireccion.ToList().ForEach(x =>
                    {
                        if (x.idDireccion == 0) _tbPersona.tbDireccion.Add(x);
                        else x.tbTelefono.ToList().ForEach(y => { if (y.idTelefono == 0) x.tbTelefono.Add(y); });
                    });
                    _BindingListtbTelefonoPersonal.ToList().ForEach(x => { if (x.idTelefonoPersonal == 0) _tbPersona.tbTelefonoPersonal.Add(x); });
                    _BindingListtbFormacionAcademica.ToList().ForEach(x => { if (x.idFormacionAcademica == 0) _tbPersona.tbFormacionAcademica.Add(x); });
                    _tbPersona.tbEmpleado.idEmpresa = ((Empresa)cmbEmpresa.SelectedItem).idEmpresa;
                    _tbPersona.tbEmpleado.idTienda = ((Tienda)cmbTienda.SelectedItem).idTienda;
                    _tbPersona.tbEmpleado.idTipoContrato = ((TipoContrato)cmbTipoContrato.SelectedItem).idTipoContrato;
                    _tbPersona.tbEmpleado.idCargo = ((Cargo)cmbCargo.SelectedItem).idCargo;
                    _tbPersona.tbEmpleado.esAfiliado = chbEsAfiliado.Checked;
                    _tbPersona.tbEmpleado.idTipoNomina = ((TipoNomina)cmbTipoNomina.SelectedItem).idTipoNomina;
                    _tbPersona.tbEmpleado.idMedioCancelacion = ((MedioCancelacion)cmbMedioCancelacion.SelectedItem).idMedioCancelacion;
                    _tbPersona.tbEmpleado.idEntidadBancaria = null;
                    _tbPersona.tbEmpleado.numeroCuenta = null;
                    if (((MedioCancelacion)cmbMedioCancelacion.SelectedItem).exigirDatoBancario)
                    {
                        _tbPersona.tbEmpleado.idEntidadBancaria = ((EntidadBancaria)cmbEntidadBancaria.SelectedItem).idEntidadBancaria;
                        _tbPersona.tbEmpleado.numeroCuenta = txtNumeroCuenta.Text.Trim();
                    }
                    if (pbxFoto.Image != null) _tbPersona.tbEmpleado.foto = (byte[])new ImageConverter().ConvertTo(pbxFoto.Image, typeof(byte[]));
                    _tbPersona.tbEmpleado.salarioBruto = Convert.ToDecimal(txtSalarioBruto.Text.Trim());
                    _tbPersona.tbEmpleado.bonificacion = Convert.ToDecimal(txtBonificacion.Text.Trim());
                    _tbPersona.tbEmpleado.avisoEntrada = dtpAvisoEntrada.Value.Date;
                    _BindingListtbHorarioLaboral.ToList().ForEach(y =>
                    {
                        if (y.idHorarioLaboral == 0)
                        {
                            if (y.estadoRegistro)
                            {
                                y.fechaHoraIngreso = Program.fechaHora;
                                y.idUsuarioIngreso = idUsuario;
                                y.terminalIngreso = Program.terminal;
                                _tbPersona.tbEmpleado.tbHorarioLaboral.Add(y);
                            }
                        }
                    });
                    if (_tbPersona.EntityState == EntityState.Detached)
                    {
                        _tbPersona.fechaHoraIngreso = Program.fechaHora;
                        _tbPersona.idUsuarioIngreso = idUsuario;
                        _tbPersona.terminalIngreso = Program.terminal;
                        _tbPersona.tbEmpleado.fechaHoraIngreso = Program.fechaHora;
                        _tbPersona.tbEmpleado.idUsuarioIngreso = idUsuario;
                        _tbPersona.tbEmpleado.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbPersona.AddObject(_tbPersona);
                    }
                    else
                    {
                        _tbPersona.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbPersona.idUsuarioUltimaModificacion = idUsuario;
                        _tbPersona.terminalUltimaModificacion = Program.terminal;
                        if (!_dbCosolemEntities.tbEmpleado.Any(x => x.idEmpleado == _tbPersona.tbEmpleado.idEmpleado))
                        {
                            _tbPersona.tbEmpleado.fechaHoraIngreso = Program.fechaHora;
                            _tbPersona.tbEmpleado.idUsuarioIngreso = idUsuario;
                            _tbPersona.tbEmpleado.terminalIngreso = Program.terminal;
                        }
                        else
                        {
                            _tbPersona.tbEmpleado.fechaHoraUltimaModificacion = Program.fechaHora;
                            _tbPersona.tbEmpleado.idUsuarioUltimaModificacion = idUsuario;
                            _tbPersona.tbEmpleado.terminalUltimaModificacion = Program.terminal;
                        }
                    }
                    _dbCosolemEntities.SaveChanges();

                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmEmpleado_Load(null, null);
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void cmbEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Empresa _tbEmpresa = (Empresa)cmbEmpresa.SelectedItem;
            
            List<Tienda> _tbTienda = _tbEmpresa.tbTienda.ToList();
            if (_tbEmpresa.idEmpresa != 0) _tbTienda.Insert(0, new Tienda { idTienda = 0, descripcion = "Seleccione" });
            cmbTienda.DataSource = _tbTienda;
            cmbTienda.ValueMember = "idTienda";
            cmbTienda.DisplayMember = "descripcion";

            List<Departamento> _tbDepartamento = _tbEmpresa.tbDepartamento.ToList();
            if (_tbEmpresa.idEmpresa != 0) _tbDepartamento.Insert(0, new Departamento { idDepartamento = 0, descripcion = "Seleccione", tbCargo = new List<Cargo> { new Cargo { idCargo = 0, descripcion = "Seleccione" } } });
            cmbDepartamento.DataSource = _tbDepartamento;
            cmbDepartamento.ValueMember = "idDepartamento";
            cmbDepartamento.DisplayMember = "descripcion";
            cmbDepartamento_SelectionChangeCommitted(null, null);
        }

        private void cmbDepartamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Departamento _tbDepartamento = (Departamento)cmbDepartamento.SelectedItem;
            List<Cargo> _tbCargo = _tbDepartamento.tbCargo.ToList();
            if (_tbDepartamento.idDepartamento != 0) _tbCargo.Insert(0, new Cargo { idCargo = 0, descripcion = "Seleccione" });
            cmbCargo.DataSource = _tbCargo;
            cmbCargo.ValueMember = "idCargo";
            cmbCargo.DisplayMember = "descripcion";
        }

        private void btnAgregarFormacionesAcademicas_Click(object sender, EventArgs e)
        {
            try
            {
                tbFormacionAcademica _tbFormacionAcademica = new tbFormacionAcademica();
                frmFormacionAcademica _frmFormacionAcademica = new frmFormacionAcademica(ref _tbFormacionAcademica);
                if (_frmFormacionAcademica.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (_tbFormacionAcademica.estadoRegistro) _BindingListtbFormacionAcademica.Add(_tbFormacionAcademica);
                    InactivarRegistros(ref dgvFormacionesAcademicas);
                }
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void dgvFormacionesAcademicas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex != imgEliminarFormacionAcademica.Index)
                {
                    tbFormacionAcademica _tbFormacionAcademica = (tbFormacionAcademica)dgvFormacionesAcademicas.CurrentRow.DataBoundItem;
                    new frmFormacionAcademica(ref _tbFormacionAcademica).ShowDialog();
                    if (_tbFormacionAcademica.idFormacionAcademica == 0 && !_tbFormacionAcademica.estadoRegistro) _BindingListtbFormacionAcademica.Remove(_tbFormacionAcademica);
                    InactivarRegistros(ref dgvFormacionesAcademicas);
                }
            }
        }

        private void dgvFormacionesAcademicas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == imgEliminarFormacionAcademica.Index)
                {
                    if (MessageBox.Show("¿Seguro desea eliminar el registro seleccionado?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        tbFormacionAcademica _tbFormacionAcademica = (tbFormacionAcademica)dgvFormacionesAcademicas.CurrentRow.DataBoundItem;
                        _tbFormacionAcademica.estadoRegistro = false;
                        _tbFormacionAcademica.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbFormacionAcademica.idUsuarioUltimaModificacion = idUsuario;
                        _tbFormacionAcademica.terminalUltimaModificacion = Program.terminal;
                        _tbFormacionAcademica.fechaHoraEliminacion = Program.fechaHora;
                        _tbFormacionAcademica.idUsuarioEliminacion = idUsuario;
                        _tbFormacionAcademica.terminalEliminacion = Program.terminal;
                        if (_tbFormacionAcademica.idFormacionAcademica == 0) _BindingListtbFormacionAcademica.Remove(_tbFormacionAcademica);
                        InactivarRegistros(ref dgvFormacionesAcademicas);
                    }
                }
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

        private void cmbMedioCancelacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MedioCancelacion _tbMedioCancelacion = (MedioCancelacion)cmbMedioCancelacion.SelectedItem;
            cmbEntidadBancaria.SelectedValue = 0;
            cmbEntidadBancaria.Enabled = _tbMedioCancelacion.exigirDatoBancario;
            txtNumeroCuenta.Clear();
            txtNumeroCuenta.Enabled = _tbMedioCancelacion.exigirDatoBancario;
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

        private void dgvHorarioLaboral_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvHorarioLaboral.CommitEdit(DataGridViewDataErrorContexts.Commit);
            foreach (DataGridViewRow _DataGridViewRow in dgvHorarioLaboral.Rows)
            {
                tbHorarioLaboral _tbHorarioLaboral = (tbHorarioLaboral)_DataGridViewRow.DataBoundItem;
                if (_tbHorarioLaboral.EntityState == EntityState.Modified)
                {
                    _tbHorarioLaboral.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbHorarioLaboral.idUsuarioUltimaModificacion = idUsuario;
                    _tbHorarioLaboral.terminalUltimaModificacion = Program.terminal;
                    if (!_tbHorarioLaboral.estadoRegistro)
                    {
                        _tbHorarioLaboral.fechaHoraEliminacion = Program.fechaHora;
                        _tbHorarioLaboral.idUsuarioEliminacion = idUsuario;
                        _tbHorarioLaboral.terminalEliminacion = Program.terminal;
                    }
                }
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbPersona.idPersona == 0)
                MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbPersona.tbEmpleado.avisoSalida = Program.fechaHora;
                _tbPersona.tbEmpleado.estadoRegistro = false;
                _tbPersona.tbEmpleado.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbPersona.tbEmpleado.idUsuarioUltimaModificacion = idUsuario;
                _tbPersona.tbEmpleado.terminalUltimaModificacion = Program.terminal;
                _tbPersona.tbEmpleado.fechaHoraEliminacion = Program.fechaHora;
                _tbPersona.tbEmpleado.idUsuarioEliminacion = idUsuario;
                _tbPersona.tbEmpleado.terminalEliminacion = Program.terminal;

                _tbPersona.tbEmpleado.tbHorarioLaboral.ToList().ForEach(x =>
                {
                    x.estadoRegistro = false;
                    x.fechaHoraUltimaModificacion = Program.fechaHora;
                    x.idUsuarioUltimaModificacion = idUsuario;
                    x.terminalUltimaModificacion = Program.terminal;
                    x.fechaHoraEliminacion = Program.fechaHora;
                    x.idUsuarioEliminacion = idUsuario;
                    x.terminalEliminacion = Program.terminal;
                });

                _tbPersona.tbEmpleado.tbUsuario.estadoRegistro = false;
                _tbPersona.tbEmpleado.tbUsuario.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbPersona.tbEmpleado.tbUsuario.idUsuarioUltimaModificacion = idUsuario;
                _tbPersona.tbEmpleado.tbUsuario.terminalUltimaModificacion = Program.terminal;
                _tbPersona.tbEmpleado.tbUsuario.fechaHoraEliminacion = Program.fechaHora;
                _tbPersona.tbEmpleado.tbUsuario.idUsuarioEliminacion = idUsuario;
                _tbPersona.tbEmpleado.tbUsuario.terminalEliminacion = Program.terminal;

                _tbPersona.tbEmpleado.tbUsuario.tbUsuarioOpcion.ToList().ForEach(x =>
                {
                    x.estadoRegistro = false;
                    x.fechaHoraUltimaModificacion = Program.fechaHora;
                    x.idUsuarioUltimaModificacion = idUsuario;
                    x.terminalUltimaModificacion = Program.terminal;
                    x.fechaHoraEliminacion = Program.fechaHora;
                    x.idUsuarioEliminacion = idUsuario;
                    x.terminalEliminacion = Program.terminal;
                });

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (idUsuario == _tbPersona.tbEmpleado.tbUsuario.idUsuario) Application.Exit();

                frmEmpleado_Load(null, null);
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmEmpleado_Load(null, null);
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
                tbPersona _tbPersona = (from P in _dbCosolemEntities.tbPersona from E in _dbCosolemEntities.tbEmpleado.Where(x => x.idEmpleado == P.idPersona && x.estadoRegistro == P.estadoRegistro).DefaultIfEmpty() where P.estadoRegistro && P.idTipoPersona == 1 && P.numeroIdentificacion == numeroIdentificacion select P).FirstOrDefault();
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

        private void txtSalarioBruto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Program.decimalPoint))
                e.Handled = true;
            if ((e.KeyChar == Program.decimalPoint) && ((sender as TextBox).Text.IndexOf(Program.decimalPoint) > -1))
                e.Handled = true;
        }

        private void txtBonificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Program.decimalPoint))
                e.Handled = true;
            if ((e.KeyChar == Program.decimalPoint) && ((sender as TextBox).Text.IndexOf(Program.decimalPoint) > -1))
                e.Handled = true;
        }

        private void cmbTipoIdentificacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtNumeroIdentificacion.MaxLength = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).cantidadCaracteres;
            txtNumeroIdentificacion.Clear();
            txtNumeroIdentificacion.Select();
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Tienda"), new DataColumn("Tipo de identificación"), new DataColumn("Número de identificación"), new DataColumn("Nombre completo"), new DataColumn("Departamento"), new DataColumn("Cargo"), new DataColumn("Es empleado"), new DataColumn("Fecha de registro"), new DataColumn("persona", typeof(object)) });

            (from P in _dbCosolemEntities.tbPersona
             from E in _dbCosolemEntities.tbEmpleado.Where(x => x.idEmpleado == P.idPersona && x.estadoRegistro == P.estadoRegistro).DefaultIfEmpty()
             where P.estadoRegistro && P.idTipoPersona == 1
             select new
             {
                 empresa = (E != null ? E.tbEmpresa.razonSocial : ""),
                 tienda = (E != null ? E.tbTienda.descripcion : ""),
                 tipoIdentificacion = P.tbTipoIdentificacion.descripcion,
                 numeroIdentificacion = P.numeroIdentificacion,
                 nombreCompleto = P.nombreCompleto,
                 departamento = (E != null ? (E.tbCargo != null ? (E.tbCargo.tbDepartamento != null ? E.tbCargo.tbDepartamento.descripcion : "") : "") : ""),
                 cargo = (E != null ? (E.tbCargo != null ? E.tbCargo.descripcion : "") : ""),
                 esEmpleado = (E == null ? "No" : "Sí"),
                 fechaRegistro = (E == null ? (DateTime?)null : E.fechaHoraIngreso),
                 persona = P
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.empresa, x.tienda, x.tipoIdentificacion, x.numeroIdentificacion, x.nombreCompleto, x.departamento, x.cargo, x.esEmpleado, x.fechaRegistro, x.persona));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearPersona((tbPersona)_frmBusqueda._object);
        }

        private void pbxFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Images (*.bmp;*.jpg;*.gif,*.png,*.tiff)|*.bmp;*.jpg;*.gif;*.png;*.tiff|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                pbxFoto.Image = new Bitmap(openFileDialog.FileName);
        }
    }
}