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
    public partial class frmAgendamientoServicioTecnico : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idUsuario = Program.tbUsuario.idUsuario;

        public long idOrdenTrabajo = 0;

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

        class Tecnico
        {
            public long idEmpleado { get; set; }
            public string nombreCompleto { get; set; }
        }

        private void ConsultarAgendamientos(string numeroIdentificacion, DateTime? fechaAgendamiento, long? idTecnico)
        {
            dgvAgendamientos.DataSource = null;

            if (numeroIdentificacion != null || fechaAgendamiento.HasValue || idTecnico.HasValue)
            {
                IQueryable<tbOrdenTrabajo> query = (from OT in _dbCosolemEntities.tbOrdenTrabajo where OT.idEmpresa == idEmpresa && OT.estadoRegistro select OT);
                if (numeroIdentificacion != null)
                    query = (from OT in query where OT.numeroIdentificacion == numeroIdentificacion select OT);
                if (fechaAgendamiento.HasValue)
                    query = (from OT in query where EntityFunctions.TruncateTime(OT.fechaHoraOrdenTrabajo) == EntityFunctions.TruncateTime(fechaAgendamiento) select OT);
                if (idTecnico.HasValue)
                    query = (from OT in query where OT.idTecnico == idTecnico select OT);
                SortableBindingList<tbOrdenTrabajo> ordenesTrabajo = new SortableBindingList<tbOrdenTrabajo>(query.ToList());
                ordenesTrabajo.ToList().ForEach(x =>
                {
                    x.estadoAgendamiento = x.tbEstadoOrdenTrabajo.descripcion;
                    x.tecnicoAsignado = x.idTecnico != null ? x.tbEmpleado.tbPersona.nombreCompleto : String.Empty;
                });
                dgvAgendamientos.DataSource = ordenesTrabajo;
            }
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

        public frmAgendamientoServicioTecnico()
        {
            InitializeComponent();
        }

        private void SetearPersona(tbPersona _tbPersona)
        {
            cmbTipoPersona.SelectedValue = _tbPersona.idTipoPersona;
            cmbTipoPersona_SelectionChangeCommitted(null, null);
            cmbTipoIdentificacion.SelectedValue = _tbPersona.idTipoIdentificacion;
            cmbTipoIdentificacion_SelectionChangeCommitted(null, null);
            txtNumeroIdentificacion.Text = _tbPersona.numeroIdentificacion;
            txtNumeroIdentificacion.Tag = _tbPersona.idPersona;
            txtPrimerNombre.Text = _tbPersona.primerNombre;
            txtSegundoNombre.Text = _tbPersona.segundoNombre;
            txtApellidoPaterno.Text = _tbPersona.apellidoPaterno;
            txtApellidoMaterno.Text = _tbPersona.apellidoMaterno;
            txtRazonSocial.Text = _tbPersona.razonSocial;
            txtCorreoElectronico.Text = _tbPersona.correoElectronico;
            tbDireccion _tbDireccion = _tbPersona.tbDireccion.Where(x => x.esPrincipal).FirstOrDefault();
            if (_tbDireccion != null)
            {
                cmbProvinciaCliente.SelectedValue = _tbDireccion.tbCanton.idProvincia;
                cmbProvinciaCliente_SelectionChangeCommitted(null, null);
                cmbCantonCliente.SelectedValue = _tbDireccion.idCanton;
                tbTelefono _tbTelefonoConvencional = _tbDireccion.tbTelefono.Where(x => x.idTipoTelefono == 1).FirstOrDefault();
                if (_tbTelefonoConvencional != null)
                {
                    txtConvencional.Tag = _tbTelefonoConvencional.idTelefono;
                    txtConvencional.Text = _tbTelefonoConvencional.numero;
                }
                tbTelefono _tbTelefonoCelular = _tbDireccion.tbTelefono.Where(x => x.idTipoTelefono == 2).FirstOrDefault();
                if (_tbTelefonoCelular != null)
                {
                    txtCelular.Tag = _tbTelefonoCelular.idTelefono;
                    txtCelular.Text = _tbTelefonoCelular.numero;
                }
                txtDireccionCliente.Tag = _tbDireccion.idDireccion;
                txtDireccionCliente.Text = _tbDireccion.direccionCompleta;
            }
            cmbTipoPersona.Enabled = false;
            cmbTipoIdentificacion.Enabled = false;
            txtNumeroIdentificacion.Enabled = false;
            if (_tbPersona.idTipoPersona == 1) txtPrimerNombre.Select();
            if (_tbPersona.idTipoPersona == 2) txtRazonSocial.Select();

            string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
            DateTime? fechaAgendamiento = (dtpFechaHoraAgendamiento.Checked ? dtpFechaHoraAgendamiento.Value : (DateTime?)null);
            long? idTecnico = ((Tecnico)cmbTecnicoAsignado.SelectedItem).idEmpleado;

            ConsultarAgendamientos(numeroIdentificacion, fechaAgendamiento, idTecnico);
        }

        private void txtNumeroIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
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

        private void frmAgendamientoServicioTecnico_Load(object sender, EventArgs e)
        {
            dgvAgendamientos.AutoGenerateColumns = false;

            _dbCosolemEntities = new dbCosolemEntities();

            cmbTipoPersona.DataSource = (from TP in _dbCosolemEntities.tbTipoPersona select new TipoPersona { idTipoPersona = TP.idTipoPersona, descripcion = TP.descripcion }).ToList();
            cmbTipoPersona.ValueMember = "idTipoPersona";
            cmbTipoPersona.DisplayMember = "descripcion";
            cmbTipoPersona_SelectionChangeCommitted(null, null);

            var _tbProvincia = (from P in _dbCosolemEntities.tbProvincia select new { idProvincia = P.idProvincia, descripcion = P.descripcion, tbCanton = (from C in P.tbCanton select new { idCanton = C.idCanton, descripcion = C.descripcion }) }).ToList();
            cmbProvinciaCliente.DataSource = _tbProvincia;
            cmbProvinciaCliente.ValueMember = "idProvincia";
            cmbProvinciaCliente.DisplayMember = "descripcion";

            var _tbCanton = ((dynamic)cmbProvinciaCliente.SelectedItem).tbCanton;
            cmbCantonCliente.DataSource = _tbCanton;
            cmbCantonCliente.ValueMember = "idCanton";
            cmbCantonCliente.DisplayMember = "descripcion";

            List<Tecnico> tecnicos = (from E in _dbCosolemEntities.tbDepartamento.Where(x => x.idEmpresa == idEmpresa && new List<long> { 6, 9 }.Contains(x.idDepartamento) && x.estadoRegistro).FirstOrDefault().tbCargo.Where(y => new List<long> { 17, 24 }.Contains(y.idCargo) && y.estadoRegistro).FirstOrDefault().tbEmpleado where E.estadoRegistro select new Tecnico { idEmpleado = E.idEmpleado, nombreCompleto = E.tbPersona.nombreCompleto }).ToList();
            tecnicos.Insert(0, new Tecnico { idEmpleado = 0, nombreCompleto = "Seleccione" });
            cmbTecnicoAsignado.DataSource = tecnicos;
            cmbTecnicoAsignado.ValueMember = "idEmpleado";
            cmbTecnicoAsignado.DisplayMember = "nombreCompleto";

            btnLimpiarCliente_Click(null, null);

            dtpFechaHoraAgendamiento.Value = DateTime.Now;
            dtpFechaHoraAgendamiento.Checked = false;
            txtCodigoProducto.Clear();
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();
            txtSerie.Clear();
            txtFallaReportada.Clear();

            dgvAgendamientos.DataSource = null;

            if (idOrdenTrabajo > 0)
            {
                tbOrdenTrabajo ordenTrabajo = (from OV in _dbCosolemEntities.tbOrdenTrabajo where OV.idOrdenTrabajo == idOrdenTrabajo select OV).FirstOrDefault();
                SetearPersona(ordenTrabajo.tbCliente.tbPersona);

                if (ordenTrabajo.fechaHoraOrdenTrabajo.HasValue) dtpFechaHoraAgendamiento.Value = ordenTrabajo.fechaHoraOrdenTrabajo.Value;
                cmbTecnicoAsignado.SelectedValue = ordenTrabajo.idTecnico ?? 0;
                txtCodigoProducto.Text = ordenTrabajo.tbProducto.codigoProducto;
                txtCodigoProducto.Tag = ordenTrabajo.idProducto;
                txtDescripcionProducto.Text = ordenTrabajo.tbProducto.descripcion;
                txtSerie.Text = ordenTrabajo.serie;
                txtFallaReportada.Text = ordenTrabajo.fallaReportada;

                string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
                DateTime? fechaAgendamiento = (dtpFechaHoraAgendamiento.Checked ? dtpFechaHoraAgendamiento.Value : (DateTime?)null);
                long? idTecnico = ((Tecnico)cmbTecnicoAsignado.SelectedItem).idEmpleado;

                ConsultarAgendamientos(numeroIdentificacion, fechaAgendamiento, idTecnico);
            }

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

        private void cmbTipoIdentificacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtNumeroIdentificacion.MaxLength = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).cantidadCaracteres;
            txtNumeroIdentificacion.Clear();
            txtNumeroIdentificacion.Select();
        }

        private void cmbProvinciaCliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var _tbCanton = ((dynamic)cmbProvinciaCliente.SelectedItem).tbCanton;

            cmbCantonCliente.DataSource = _tbCanton;
            cmbCantonCliente.ValueMember = "idCanton";
            cmbCantonCliente.DisplayMember = "descripcion";
        }

        private void txtConvencional_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void btnLimpiarCliente_Click(object sender, EventArgs e)
        {
            cmbTipoPersona.SelectedValue = ((List<TipoPersona>)cmbTipoPersona.DataSource).FirstOrDefault().idTipoPersona;
            cmbTipoPersona_SelectionChangeCommitted(null, null);
            txtNumeroIdentificacion.Tag = null;
            txtPrimerNombre.Clear();
            txtSegundoNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
            txtRazonSocial.Clear();
            txtCorreoElectronico.Clear();
            cmbProvinciaCliente.SelectedValue = ((dynamic)cmbProvinciaCliente.DataSource)[0].idProvincia;
            cmbProvinciaCliente_SelectionChangeCommitted(null, null);
            txtConvencional.Tag = null;
            txtConvencional.Clear();
            txtCelular.Tag = null;
            txtCelular.Clear();
            txtDireccionCliente.Tag = null;
            txtDireccionCliente.Clear();
            cmbTipoPersona.Enabled = true;
            cmbTipoIdentificacion.Enabled = true;
            txtNumeroIdentificacion.Enabled = true;
            dgvAgendamientos.DataSource = null;
            txtNumeroIdentificacion.Select();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmAgendamientoServicioTecnico_Load(null, null);
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                long idTecnico = ((Tecnico)cmbTecnicoAsignado.SelectedItem).idEmpleado;
                TipoPersona _tbTipoPersona = (TipoPersona)cmbTipoPersona.SelectedItem;

                string mensaje = String.Empty;
                if (String.IsNullOrEmpty(txtNumeroIdentificacion.Text.Trim())) mensaje += "Ingrese número de identificación\n";
                if (_tbTipoPersona.idTipoPersona == 1)
                {
                    if (String.IsNullOrEmpty(txtPrimerNombre.Text.Trim())) mensaje += "Ingrese primer nombre\n";
                    if (String.IsNullOrEmpty(txtApellidoPaterno.Text.Trim())) mensaje += "Ingrese apellido paterno\n";
                    if (String.IsNullOrEmpty(txtApellidoMaterno.Text.Trim())) mensaje += "Ingrese apellido materno\n";
                }
                if (_tbTipoPersona.idTipoPersona == 2) if (String.IsNullOrEmpty(txtRazonSocial.Text.Trim())) mensaje += "Ingrese razón social\n";
                if (String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) mensaje += "Ingrese correo electrónico\n";
                if (!String.IsNullOrEmpty(txtCorreoElectronico.Text.Trim())) if (!Util.ValidaEmail(txtCorreoElectronico.Text.Trim())) mensaje += "Correo electrónico inválido, favor verificar\n";
                if (String.IsNullOrEmpty(txtDireccionCliente.Text.Trim())) mensaje += "Ingrese dirección\n";
                if (String.IsNullOrEmpty(txtConvencional.Text.Trim()) && String.IsNullOrEmpty(txtCelular.Text.Trim())) mensaje += "Ingrese al menos 1 teléfono\n";
                if (txtCodigoProducto.Tag == null) mensaje += "Seleccione un producto\n";
                if (String.IsNullOrEmpty(txtSerie.Text.Trim())) mensaje += "Ingrese serie del producto\n";

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    tbPersona persona = null;

                    long idPersona = 0;
                    if (txtNumeroIdentificacion.Tag != null)
                    {
                        idPersona = Convert.ToInt64(txtNumeroIdentificacion.Tag);

                        persona = (from P in _dbCosolemEntities.tbPersona where P.idPersona == idPersona select P).FirstOrDefault();
                    }
                    else
                    {
                        int idTipoPersona = ((TipoPersona)cmbTipoPersona.SelectedItem).idTipoPersona;
                        int idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                        string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();

                        persona = (from P in _dbCosolemEntities.tbPersona where P.idTipoPersona == idTipoPersona && P.idTipoIdentificacion == idTipoIdentificacion && P.numeroIdentificacion == numeroIdentificacion select P).FirstOrDefault();
                    }

                    if (persona == null)
                    {
                        persona = new tbPersona
                        {
                            idTipoPersona = ((TipoPersona)cmbTipoPersona.SelectedItem).idTipoPersona,
                            idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion,
                            numeroIdentificacion = txtNumeroIdentificacion.Text.Trim(),
                            primerNombre = txtPrimerNombre.Text.Trim(),
                            segundoNombre = txtSegundoNombre.Text.Trim(),
                            apellidoPaterno = txtApellidoPaterno.Text.Trim(),
                            apellidoMaterno = txtApellidoMaterno.Text.Trim(),
                            razonSocial = txtRazonSocial.Text.Trim(),
                            idSexo = 3,
                            idEstadoCivil = 6,
                            correoElectronico = txtCorreoElectronico.Text.Trim(),
                            estadoRegistro = true,
                            fechaHoraIngreso = Program.fechaHora,
                            idUsuarioIngreso = idUsuario,
                            terminalIngreso = Program.terminal
                        };
                        persona.tbDireccion.Add(new tbDireccion
                        {
                            idCanton = ((dynamic)cmbCantonCliente.SelectedItem).idCanton,
                            idTipoDireccion = 1,
                            direccionCompleta = txtDireccionCliente.Text,
                            esPrincipal = true,
                            estadoRegistro = true,
                            fechaHoraIngreso = Program.fechaHora,
                            idUsuarioIngreso = idUsuario,
                            terminalIngreso = Program.terminal
                        });
                        if (!String.IsNullOrEmpty(txtConvencional.Text.Trim()))
                        {
                            persona.tbDireccion.FirstOrDefault().tbTelefono.Add(new tbTelefono
                            {
                                idTipoTelefono = 1,
                                idOperadora = 1,
                                numero = txtConvencional.Text.Trim(),
                                esPrincipal = false,
                                estadoRegistro = true,
                                fechaHoraIngreso = Program.fechaHora,
                                idUsuarioIngreso = idUsuario,
                                terminalIngreso = Program.terminal
                            });
                        }
                        if (!String.IsNullOrEmpty(txtCelular.Text.Trim()))
                        {
                            persona.tbDireccion.FirstOrDefault().tbTelefono.Add(new tbTelefono
                            {
                                idTipoTelefono = 2,
                                idOperadora = 1,
                                numero = txtCelular.Text.Trim(),
                                esPrincipal = false,
                                estadoRegistro = true,
                                fechaHoraIngreso = Program.fechaHora,
                                idUsuarioIngreso = idUsuario,
                                terminalIngreso = Program.terminal
                            });
                        }
                        _dbCosolemEntities.tbPersona.AddObject(persona);
                        _dbCosolemEntities.SaveChanges();
                    }
                    else
                    {
                        persona.primerNombre = txtPrimerNombre.Text.Trim();
                        persona.segundoNombre = txtSegundoNombre.Text.Trim();
                        persona.apellidoPaterno = txtApellidoPaterno.Text.Trim();
                        persona.apellidoMaterno = txtApellidoMaterno.Text.Trim();
                        persona.razonSocial = txtRazonSocial.Text.Trim();
                        persona.correoElectronico = txtCorreoElectronico.Text.Trim();
                        persona.fechaHoraUltimaModificacion = Program.fechaHora;
                        persona.idUsuarioUltimaModificacion = idUsuario;
                        persona.terminalUltimaModificacion = Program.terminal;
                        long idDireccion = 0;
                        if (txtDireccionCliente.Tag != null) idDireccion = Convert.ToInt64(txtDireccionCliente.Tag);
                        tbDireccion _tbDireccion = persona.tbDireccion.Where(x => x.idDireccion == idDireccion).FirstOrDefault();
                        if (_tbDireccion == null)
                        {
                            _tbDireccion = new tbDireccion();
                            _tbDireccion.idCanton = ((dynamic)cmbCantonCliente.SelectedItem).idCanton;
                            _tbDireccion.idTipoDireccion = 1;
                            _tbDireccion.direccionCompleta = txtDireccionCliente.Text;
                            _tbDireccion.esPrincipal = true;
                            _tbDireccion.estadoRegistro = true;
                            _tbDireccion.fechaHoraIngreso = Program.fechaHora;
                            _tbDireccion.idUsuarioIngreso = idUsuario;
                            _tbDireccion.terminalIngreso = Program.terminal;
                        }
                        else
                        {
                            _tbDireccion.idCanton = ((dynamic)cmbCantonCliente.SelectedItem).idCanton;
                            _tbDireccion.direccionCompleta = txtDireccionCliente.Text;
                            _tbDireccion.fechaHoraUltimaModificacion = Program.fechaHora;
                            _tbDireccion.idUsuarioUltimaModificacion = idUsuario;
                            _tbDireccion.terminalUltimaModificacion = Program.terminal;
                        }
                        if (!String.IsNullOrEmpty(txtConvencional.Text.Trim()))
                        {
                            long idTelefonoConvencional = 0;
                            if (txtConvencional.Tag != null) idTelefonoConvencional = Convert.ToInt64(txtConvencional.Tag);
                            tbTelefono _tbTelefonoConvencional = _tbDireccion.tbTelefono.Where(x => x.idTelefono == idTelefonoConvencional).FirstOrDefault();
                            if (_tbTelefonoConvencional == null)
                            {
                                _tbTelefonoConvencional = new tbTelefono();
                                _tbTelefonoConvencional.idTipoTelefono = 1;
                                _tbTelefonoConvencional.idOperadora = 1;
                                _tbTelefonoConvencional.numero = txtConvencional.Text.Trim();
                                _tbTelefonoConvencional.esPrincipal = false;
                                _tbTelefonoConvencional.estadoRegistro = true;
                                _tbTelefonoConvencional.fechaHoraIngreso = Program.fechaHora;
                                _tbTelefonoConvencional.idUsuarioIngreso = idUsuario;
                                _tbTelefonoConvencional.terminalIngreso = Program.terminal;
                            }
                            else
                            {
                                _tbTelefonoConvencional.numero = txtConvencional.Text.Trim();
                                _tbTelefonoConvencional.fechaHoraUltimaModificacion = Program.fechaHora;
                                _tbTelefonoConvencional.idUsuarioUltimaModificacion = idUsuario;
                                _tbTelefonoConvencional.terminalUltimaModificacion = Program.terminal;
                            }
                            if (_tbTelefonoConvencional.EntityState == EntityState.Detached) _tbDireccion.tbTelefono.Add(_tbTelefonoConvencional);
                        }
                        if (!String.IsNullOrEmpty(txtCelular.Text.Trim()))
                        {
                            long idTelefonoCelular = 0;
                            if (txtCelular.Tag != null) idTelefonoCelular = Convert.ToInt64(txtCelular.Tag);
                            tbTelefono _tbTelefonoCelular = _tbDireccion.tbTelefono.Where(x => x.idTelefono == idTelefonoCelular).FirstOrDefault();
                            if (_tbTelefonoCelular == null)
                            {
                                _tbTelefonoCelular = new tbTelefono();
                                _tbTelefonoCelular.idTipoTelefono = 2;
                                _tbTelefonoCelular.idOperadora = 1;
                                _tbTelefonoCelular.numero = txtCelular.Text.Trim();
                                _tbTelefonoCelular.esPrincipal = false;
                                _tbTelefonoCelular.estadoRegistro = true;
                                _tbTelefonoCelular.fechaHoraIngreso = Program.fechaHora;
                                _tbTelefonoCelular.idUsuarioIngreso = idUsuario;
                                _tbTelefonoCelular.terminalIngreso = Program.terminal;
                            }
                            else
                            {
                                _tbTelefonoCelular.numero = txtCelular.Text.Trim();
                                _tbTelefonoCelular.fechaHoraUltimaModificacion = Program.fechaHora;
                                _tbTelefonoCelular.idUsuarioUltimaModificacion = idUsuario;
                                _tbTelefonoCelular.terminalUltimaModificacion = Program.terminal;
                            }
                            if (_tbTelefonoCelular.EntityState == EntityState.Detached) _tbDireccion.tbTelefono.Add(_tbTelefonoCelular);
                        }
                        if (_tbDireccion.EntityState == EntityState.Detached) persona.tbDireccion.Add(_tbDireccion);
                    }
                    if (persona.tbCliente == null)
                    {
                        persona.tbCliente = new tbCliente
                        {
                            estadoRegistro = true,
                            fechaHoraIngreso = Program.fechaHora,
                            idUsuarioIngreso = idUsuario,
                            terminalIngreso = Program.terminal
                        };
                    }
                    else
                    {
                        persona.tbCliente.fechaHoraUltimaModificacion = Program.fechaHora;
                        persona.tbCliente.idUsuarioUltimaModificacion = idUsuario;
                        persona.tbCliente.terminalUltimaModificacion = Program.terminal;
                    }

                    tbOrdenTrabajo ordenTrabajo = new tbOrdenTrabajo { estadoRegistro = true, fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal };

                    if (idOrdenTrabajo != 0)
                    {
                        ordenTrabajo = (from OT in _dbCosolemEntities.tbOrdenTrabajo where OT.idOrdenTrabajo == idOrdenTrabajo select OT).FirstOrDefault();
                        ordenTrabajo.fechaHoraUltimaModificacion = Program.fechaHora;
                        ordenTrabajo.idUsuarioUltimaModificacion = idUsuario;
                        ordenTrabajo.terminalUltimaModificacion = Program.terminal;
                    }

                    ordenTrabajo.idEmpresa = idEmpresa;
                    ordenTrabajo.idCliente = persona.tbCliente.idCliente;
                    ordenTrabajo.tipoIdentificacion = persona.tbTipoIdentificacion.descripcion;
                    ordenTrabajo.numeroIdentificacion = persona.numeroIdentificacion;
                    ordenTrabajo.cliente = persona.nombreCompleto;
                    ordenTrabajo.direccion = txtDireccionCliente.Text;
                    ordenTrabajo.convencional = (!String.IsNullOrEmpty(txtConvencional.Text.Trim()) ? txtConvencional.Text.Trim() : null);
                    ordenTrabajo.celular = (!String.IsNullOrEmpty(txtCelular.Text.Trim()) ? txtCelular.Text.Trim() : null);
                    ordenTrabajo.correoElectronico = persona.correoElectronico;
                    ordenTrabajo.fechaHoraOrdenTrabajo = (dtpFechaHoraAgendamiento.Checked ? dtpFechaHoraAgendamiento.Value : (DateTime?)null);
                    ordenTrabajo.idTecnico = (idTecnico == 0 ? (long?)null : idTecnico);
                    ordenTrabajo.idEstadoOrdenTrabajo = (ordenTrabajo.fechaHoraOrdenTrabajo.HasValue && ordenTrabajo.idTecnico.HasValue ? 2 : 1);
                    ordenTrabajo.idProducto = Convert.ToInt64(txtCodigoProducto.Tag);
                    ordenTrabajo.serie = txtSerie.Text.Trim();
                    ordenTrabajo.fallaReportada = txtFallaReportada.Text.Trim();
                    if (ordenTrabajo.idOrdenTrabajo == 0) _dbCosolemEntities.tbOrdenTrabajo.AddObject(ordenTrabajo);
                    _dbCosolemEntities.SaveChanges();
                    MessageBox.Show("Agendamiento de servicio técnico #" + ordenTrabajo.idOrdenTrabajo.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmAgendamientoServicioTecnico_Load(null, null);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            frmBusquedaAgendamientoServicioTecnico _frmBusquedaAgendamientoServicioTecnico = new frmBusquedaAgendamientoServicioTecnico();
            _frmBusquedaAgendamientoServicioTecnico.Text = "Consulta de agendamientos";
            _frmBusquedaAgendamientoServicioTecnico.ShowDialog();
        }

        private void SetearProducto(tbProducto _tbProducto)
        {
            txtCodigoProducto.Clear();
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();

            if (_tbProducto == null)
                MessageBox.Show("Producto no existe", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                txtCodigoProducto.Text = _tbProducto.codigoProducto;
                txtCodigoProducto.Tag = _tbProducto.idProducto;
                txtDescripcionProducto.Text = _tbProducto.descripcion;
            }
        }

        private void txtCodigoProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                DataTable _DataTable = new DataTable();
                _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Marca"), new DataColumn("Línea"), new DataColumn("Grupo"), new DataColumn("SubGrupo"), new DataColumn("Modelo"), new DataColumn("Código de producto"), new DataColumn("Descripción"), new DataColumn("Características"), new DataColumn("Fecha de registro"), new DataColumn("producto", typeof(object)) });
                var Productos = edmCosolemFunctions.getProductos(null, true);
                foreach (var Producto in Productos) _DataTable.Rows.Add(Producto.marca, Producto.linea, Producto.grupo, Producto.subgrupo, Producto.modelo, Producto.codigoProducto, Producto.descripcion, Producto.caracteristicas, Producto.fechaRegistro, Producto.producto);
                frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
                if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    SetearProducto((tbProducto)_frmBusqueda._object);
            }
        }

        private void txtCodigoProducto_Leave(object sender, EventArgs e)
        {
            txtCodigoProducto.Tag = null;
            txtDescripcionProducto.Clear();

            if (!String.IsNullOrEmpty(txtCodigoProducto.Text.Trim()))
            {
                var _tbProducto = edmCosolemFunctions.getProductos(txtCodigoProducto.Text.Trim(), true);
                tbProducto producto = _tbProducto.Count == 0 ? null : _tbProducto[0].producto;
                SetearProducto(producto);
            }
        }

        private void dtpFechaHoraAgendamiento_ValueChanged(object sender, EventArgs e)
        {
            string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
            DateTime? fechaAgendamiento = (dtpFechaHoraAgendamiento.Checked ? dtpFechaHoraAgendamiento.Value : (DateTime?)null);
            long? idTecnico = ((Tecnico)cmbTecnicoAsignado.SelectedItem).idEmpleado;

            ConsultarAgendamientos(numeroIdentificacion, fechaAgendamiento, idTecnico);
        }

        private void cmbTecnicoAsignado_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
            DateTime? fechaAgendamiento = (dtpFechaHoraAgendamiento.Checked ? dtpFechaHoraAgendamiento.Value : (DateTime?)null);
            long? idTecnico = ((Tecnico)cmbTecnicoAsignado.SelectedItem).idEmpleado;

            ConsultarAgendamientos(numeroIdentificacion, fechaAgendamiento, idTecnico);
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && !(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtNumeroIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                int idTipoPersona = ((TipoPersona)cmbTipoPersona.SelectedItem).idTipoPersona;
                int idTipoIdentificacion = ((TipoIdentificacion)cmbTipoIdentificacion.SelectedItem).idTipoIdentificacion;
                string numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();

                tbPersona _tbPersona = (from P in _dbCosolemEntities.tbPersona where P.idTipoPersona == idTipoPersona && P.idTipoIdentificacion == idTipoIdentificacion && P.numeroIdentificacion == numeroIdentificacion select P).FirstOrDefault();
                if (_tbPersona == null) MessageBox.Show("Cliente no exite, favor registre sus datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else SetearPersona(_tbPersona);

                if (idTipoPersona == 1) txtPrimerNombre.Select();
                if (idTipoPersona == 2) txtRazonSocial.Select();
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

        private void txtDireccionCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                DataTable _DataTable = new DataTable();
                _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Tipo de dirección"), new DataColumn("Dirección"), new DataColumn("direccion", typeof(object)) });

                long idPersona = 0;
                if (txtNumeroIdentificacion.Tag != null)
                    idPersona = Convert.ToInt64(txtNumeroIdentificacion.Tag);

                (from D in _dbCosolemEntities.tbDireccion
                 where D.idPersona == idPersona
                 select new
                 {
                     tipoDireccion = D.tbTipoDireccion.descripcion,
                     direccionCompleta = D.direccionCompleta,
                     direccion = D
                 }).ToList().ForEach(x => _DataTable.Rows.Add(x.tipoDireccion, x.direccionCompleta, x.direccion));

                frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
                if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbDireccion direccion = (tbDireccion)_frmBusqueda._object;
                    txtDireccionCliente.Tag = direccion.idDireccion;
                    txtDireccionCliente.Text = direccion.direccionCompleta;
                }
            }
        }
    }
}
