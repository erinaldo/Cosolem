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
    public partial class frmProductoFinanciero : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Empresa
        {
            public long idEmpresa { get; set; }
            public string razonSocial { get; set; }
        }

        tbProductoFinancieroCabecera _tbProductoFinancieroCabecera = null;
        BindingList<tbProductoFinancieroDetalle> _BindingListtbProductoFinancieroDetalle = null;

        public frmProductoFinanciero()
        {
            InitializeComponent();
        }

        private void frmProductoFinanciero_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvProductoFinancieroDetalle.AutoGenerateColumns = false;

            _tbProductoFinancieroCabecera = new tbProductoFinancieroCabecera { estadoRegistro = true };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbProductoFinancieroCabecera, EntityState.Detached);
            _BindingListtbProductoFinancieroDetalle = new BindingList<tbProductoFinancieroDetalle>(_tbProductoFinancieroCabecera.tbProductoFinancieroDetalle.ToList());

            List<Empresa> _tbEmpresa = (from E in _dbCosolemEntities.tbEmpresa select new Empresa { idEmpresa = E.idEmpresa, razonSocial = E.ruc + " - " + E.razonSocial }).ToList();
            _tbEmpresa.Insert(0, new Empresa { idEmpresa = 0, razonSocial = "Seleccione" });
            cmbEmpresa.DataSource = _tbEmpresa;
            cmbEmpresa.ValueMember = "idEmpresa";
            cmbEmpresa.DisplayMember = "razonSocial";

            txtDescripcion.Clear();
            dtpFechaDesde.Value = Program.fechaHora;
            dtpFechaHasta.Value = Program.fechaHora;

            List<tbFrecuenciaPago> _tbFrecuenciaPago = (from FP in _dbCosolemEntities.tbFrecuenciaPago select FP).ToList();
            _tbFrecuenciaPago.Insert(0, new tbFrecuenciaPago { idFrecuenciaPago = 0, descripcion = "Seleccione" });
            cmbFrecuenciaPago.DataSource = _tbFrecuenciaPago;
            cmbFrecuenciaPago.ValueMember = "idFrecuenciaPago";
            cmbFrecuenciaPago.DisplayMember = "descripcion";

            txtPlazo.Text = "0";
            txtMesesGracia.Text = "0";
            txtCuotasGratis.Text = "0";
            txtPorcentajeCuotaInicialExigible.Text = "0";

            dgvProductoFinancieroDetalle.DataSource = _BindingListtbProductoFinancieroDetalle;
        }

        private void txtPlazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtMesesGracia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtCuotasGratis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtPorcentajeCuotaInicialExigible_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != Program.decimalPoint))
                e.Handled = true;
            if ((e.KeyChar == Program.decimalPoint) && ((sender as TextBox).Text.IndexOf(Program.decimalPoint) > -1))
                e.Handled = true;
        }

        private void btnGenerarDetalleProductoFinanciero_Click(object sender, EventArgs e)
        {
            _BindingListtbProductoFinancieroDetalle.Clear();
            int plazos = 0;
            int.TryParse(txtPlazo.Text.Trim(), out plazos);
            int mesesGracia = 0;
            int.TryParse(txtMesesGracia.Text.Trim(), out mesesGracia);
            txtMesesGracia.Text = mesesGracia.ToString();
            int cuotasGratis = 0;
            int.TryParse(txtCuotasGratis.Text.Trim(), out cuotasGratis);
            txtCuotasGratis.Text = cuotasGratis.ToString();
            decimal porcentajeCuotaInicialExigible = 0;
            decimal.TryParse(txtPorcentajeCuotaInicialExigible.Text.Trim(), out porcentajeCuotaInicialExigible);
            txtPorcentajeCuotaInicialExigible.Text = porcentajeCuotaInicialExigible.ToString();

            if (plazos == 0) MessageBox.Show("Ingrese plazo para generar detalle producto financiero", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                for (int plazo = 1; plazo <= plazos; plazo++)
                {
                    tbProductoFinancieroDetalle _tbProductoFinancieroDetalle = new tbProductoFinancieroDetalle { estadoRegistro = true, plazo = plazo, aplicaMesGracia = (mesesGracia >= plazo), aplicaCuotaGratis = (plazo > (plazos - cuotasGratis)), fechaHoraIngreso = Program.fechaHora, idUsuarioIngreso = idUsuario, terminalIngreso = Program.terminal };
                    _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbProductoFinancieroDetalle, EntityState.Detached);

                    _BindingListtbProductoFinancieroDetalle.Add(_tbProductoFinancieroDetalle);
                }
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmProductoFinanciero_Load(null, null);
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                long idEmpresa = ((Empresa)cmbEmpresa.SelectedItem).idEmpresa;
                int idFrecuenciaPago = ((tbFrecuenciaPago)cmbFrecuenciaPago.SelectedItem).idFrecuenciaPago;
                int plazos = 0;
                int.TryParse(txtPlazo.Text.Trim(), out plazos);
                int mesesGracia = 0;
                int.TryParse(txtMesesGracia.Text.Trim(), out mesesGracia);
                txtMesesGracia.Text = mesesGracia.ToString();
                int cuotasGratis = 0;
                int.TryParse(txtCuotasGratis.Text.Trim(), out cuotasGratis);
                txtCuotasGratis.Text = cuotasGratis.ToString();
                decimal porcentajeCuotaInicialExigible = 0;
                decimal.TryParse(txtPorcentajeCuotaInicialExigible.Text.Trim(), out porcentajeCuotaInicialExigible);
                txtPorcentajeCuotaInicialExigible.Text = porcentajeCuotaInicialExigible.ToString();

                int maximoPlazo = (_BindingListtbProductoFinancieroDetalle.Count > 0 ? _BindingListtbProductoFinancieroDetalle.Max(x => x.plazo) : 0);
                int cantidadMesesGracia = (_BindingListtbProductoFinancieroDetalle.Count > 0 ? _BindingListtbProductoFinancieroDetalle.Count(x => x.aplicaMesGracia) : 0);
                int cantidadCuotasGratis = (_BindingListtbProductoFinancieroDetalle.Count > 0 ? _BindingListtbProductoFinancieroDetalle.Count(x => x.aplicaCuotaGratis) : 0);

                string mensaje = String.Empty;

                if (idEmpresa == 0) mensaje += "Seleccione empresa\n";
                if (String.IsNullOrEmpty(txtDescripcion.Text.Trim())) mensaje += "Ingrese descripción\n";
                if (this._tbProductoFinancieroCabecera.idProductoFinancieroCabecera == 0 && dtpFechaDesde.Value.Date < Program.fechaHora.Date) mensaje += "Fecha desde tiene que se mayor o igual al día de hoy\n";
                if (dtpFechaDesde.Value.Date > dtpFechaHasta.Value.Date) mensaje += "Fecha desde no puede ser mayor a la fecha hasta\n";
                if (idFrecuenciaPago == 0) mensaje += "Seleccione frecuencia de pago\n";
                if (plazos == 0) mensaje += "Ingrese plazo\n";
                if (plazos > 0 && plazos == mesesGracia) mensaje += "Meses de gracia no pueden ser igual al plazo\n";
                if (plazos > 0 && plazos == cuotasGratis) mensaje += "Cuotas gratis no pueden ser igual al plazo\n";
                if (mesesGracia > 0 && cuotasGratis > 0 && mesesGracia == cuotasGratis) mensaje += "Meses de gracia no pueden ser igual a las cuotas gratis\n";
                if (plazos != maximoPlazo) mensaje += "Favor volver a generar detalle producto financiero ya que el plazo especificado es distinto\n";
                if (mesesGracia != cantidadMesesGracia) mensaje += "Favor volver a generar detalle producto financiero ya que los meses de gracia especificados son distintos\n";
                if (cuotasGratis != cantidadCuotasGratis) mensaje += "Favor volver a generar detalle producto financiero ya que las cuotas gratis especificadas son distintas\n";

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    tbProductoFinancieroCabecera _tbProductoFinancieroCabecera = new tbProductoFinancieroCabecera
                    {
                        idEmpresa = idEmpresa,
                        descripcion = txtDescripcion.Text.Trim(),
                        fechaDesde = dtpFechaDesde.Value.Date,
                        fechaHasta = dtpFechaHasta.Value.Date,
                        idFrecuenciaPago = idFrecuenciaPago,
                        plazo = plazos,
                        mesesGracia = mesesGracia,
                        cuotasGratis = cuotasGratis,
                        porcentajeCuotaInicialExigible = porcentajeCuotaInicialExigible,
                        estadoRegistro = true
                    };
                    this._tbProductoFinancieroCabecera.idEmpresa = _tbProductoFinancieroCabecera.idEmpresa;
                    this._tbProductoFinancieroCabecera.descripcion = _tbProductoFinancieroCabecera.descripcion;
                    this._tbProductoFinancieroCabecera.fechaDesde = _tbProductoFinancieroCabecera.fechaDesde;
                    this._tbProductoFinancieroCabecera.fechaHasta = _tbProductoFinancieroCabecera.fechaHasta;
                    this._tbProductoFinancieroCabecera.idFrecuenciaPago = _tbProductoFinancieroCabecera.idFrecuenciaPago;
                    this._tbProductoFinancieroCabecera.plazo = _tbProductoFinancieroCabecera.plazo;
                    this._tbProductoFinancieroCabecera.mesesGracia = _tbProductoFinancieroCabecera.mesesGracia;
                    this._tbProductoFinancieroCabecera.cuotasGratis = _tbProductoFinancieroCabecera.cuotasGratis;
                    this._tbProductoFinancieroCabecera.porcentajeCuotaInicialExigible = _tbProductoFinancieroCabecera.porcentajeCuotaInicialExigible;
                    this._tbProductoFinancieroCabecera.estadoRegistro = _tbProductoFinancieroCabecera.estadoRegistro;
                    if (this._tbProductoFinancieroCabecera.idProductoFinancieroCabecera == 0)
                    {
                        this._tbProductoFinancieroCabecera.fechaHoraIngreso = Program.fechaHora;
                        this._tbProductoFinancieroCabecera.idUsuarioIngreso = idUsuario;
                        this._tbProductoFinancieroCabecera.terminalIngreso = Program.terminal;
                    }
                    else
                    {
                        if (_BindingListtbProductoFinancieroDetalle.Where(x => x.idProductoFinancieroDetalle == 0).Count() > 0) _dbCosolemEntities.tbProductoFinancieroCabecera.Where(x => x.idProductoFinancieroCabecera == this._tbProductoFinancieroCabecera.idProductoFinancieroCabecera).FirstOrDefault().tbProductoFinancieroDetalle.ToList().ForEach(y => _dbCosolemEntities.tbProductoFinancieroDetalle.DeleteObject(y));
                        this._tbProductoFinancieroCabecera.fechaHoraUltimaModificacion = Program.fechaHora;
                        this._tbProductoFinancieroCabecera.idUsuarioUltimaModificacion = idUsuario;
                        this._tbProductoFinancieroCabecera.terminalUltimaModificacion = Program.terminal;
                    }
                    _BindingListtbProductoFinancieroDetalle.Where(x => x.idProductoFinancieroDetalle == 0).ToList().ForEach(x => this._tbProductoFinancieroCabecera.tbProductoFinancieroDetalle.Add(x));
                    if (this._tbProductoFinancieroCabecera.idProductoFinancieroCabecera == 0) _dbCosolemEntities.tbProductoFinancieroCabecera.AddObject(this._tbProductoFinancieroCabecera);
                    _dbCosolemEntities.SaveChanges();

                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmProductoFinanciero_Load(null, null);
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._tbProductoFinancieroCabecera.idProductoFinancieroCabecera == 0)
                    MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    this._tbProductoFinancieroCabecera.estadoRegistro = false;
                    this._tbProductoFinancieroCabecera.fechaHoraUltimaModificacion = Program.fechaHora;
                    this._tbProductoFinancieroCabecera.idUsuarioUltimaModificacion = idUsuario;
                    this._tbProductoFinancieroCabecera.terminalUltimaModificacion = Program.terminal;
                    this._tbProductoFinancieroCabecera.fechaHoraEliminacion = Program.fechaHora;
                    this._tbProductoFinancieroCabecera.idUsuarioEliminacion = idUsuario;
                    this._tbProductoFinancieroCabecera.terminalEliminacion = Program.terminal;

                    this._tbProductoFinancieroCabecera.tbProductoFinancieroDetalle.ToList().ForEach(x =>
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
                    MessageBox.Show("Registros eliminados satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmProductoFinanciero_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void SetearProductoFinanciero(tbProductoFinancieroCabecera _tbProductoFinancieroCabecera)
        {
            try
            {
                this._tbProductoFinancieroCabecera = _tbProductoFinancieroCabecera;

                cmbEmpresa.SelectedValue = this._tbProductoFinancieroCabecera.idEmpresa;
                txtDescripcion.Text = this._tbProductoFinancieroCabecera.descripcion;
                dtpFechaDesde.Value = this._tbProductoFinancieroCabecera.fechaDesde;
                dtpFechaHasta.Value = this._tbProductoFinancieroCabecera.fechaHasta;
                cmbFrecuenciaPago.SelectedValue = this._tbProductoFinancieroCabecera.idFrecuenciaPago;
                txtPlazo.Text = this._tbProductoFinancieroCabecera.plazo.ToString();
                txtMesesGracia.Text = this._tbProductoFinancieroCabecera.mesesGracia.ToString();
                txtCuotasGratis.Text = this._tbProductoFinancieroCabecera.cuotasGratis.ToString();
                txtPorcentajeCuotaInicialExigible.Text = this._tbProductoFinancieroCabecera.porcentajeCuotaInicialExigible.ToString();

                _BindingListtbProductoFinancieroDetalle.Clear();
                _tbProductoFinancieroCabecera.tbProductoFinancieroDetalle.ToList().ForEach(x => _BindingListtbProductoFinancieroDetalle.Add(x));
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Empresa"), new DataColumn("Producto financiero"), new DataColumn("Desde"), new DataColumn("Hasta"), new DataColumn("Frecuencia de pago"), new DataColumn("Plazo"), new DataColumn("Meses de gracia"), new DataColumn("Cuotas gratis"), new DataColumn("Porcentaje de cuota inicial exigible"), new DataColumn("Fecha de registro"), new DataColumn("productoFinanciero", typeof(object)) });

            (from PFC in _dbCosolemEntities.tbProductoFinancieroCabecera
             where PFC.estadoRegistro
             select new
             {
                 empresa = PFC.tbEmpresa.razonSocial,
                 descripcion = PFC.descripcion,
                 fechaDesde = PFC.fechaDesde,
                 fechaHasta = PFC.fechaHasta,
                 frecuenciaPago = PFC.tbFrecuenciaPago.descripcion,
                 plazo = PFC.plazo,
                 mesesGracia = PFC.mesesGracia,
                 cuotasGratis = PFC.cuotasGratis,
                 porcentajeCuotaInicialExigible = PFC.porcentajeCuotaInicialExigible,
                 fechaRegistro = PFC.fechaHoraIngreso,
                 productoFinanciero = PFC
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.empresa, x.descripcion, x.fechaDesde, x.fechaHasta, x.frecuenciaPago, x.plazo, x.mesesGracia, x.cuotasGratis, x.porcentajeCuotaInicialExigible, x.fechaRegistro, x.productoFinanciero));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearProductoFinanciero((tbProductoFinancieroCabecera)_frmBusqueda._object);
        }
    }
}
