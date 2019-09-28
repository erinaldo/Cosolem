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
    public partial class frmModelo : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Linea
        {
            public long idLinea { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<Grupo> tbGrupo { get; set; }
        }

        class Grupo
        {
            public long idGrupo { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<SubGrupo> tbSubGrupo { get; set; }
        }

        class SubGrupo
        {
            public long idSubGrupo { get; set; }
            public string descripcion { get; set; }
        }

        tbModelo _tbModelo = null;

        public frmModelo()
        {
            InitializeComponent();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (((Linea)cmbLinea.SelectedItem).idLinea == 0) mensaje += "Seleccione línea\n";
            if (((Grupo)cmbGrupo.SelectedItem).idGrupo == 0) mensaje += "Seleccione grupo\n";
            if (((SubGrupo)cmbSubGrupo.SelectedItem).idSubGrupo == 0) mensaje += "Seleccione subgrupo\n";
            if (String.IsNullOrEmpty(txtDescripcion.Text.Trim())) mensaje += "Ingrese descripción\n";

            if (String.IsNullOrEmpty(mensaje))
            {
                _tbModelo.idSubGrupo = ((SubGrupo)cmbSubGrupo.SelectedItem).idSubGrupo;
                _tbModelo.descripcion = txtDescripcion.Text.Trim();
                if (_tbModelo.EntityState == EntityState.Detached)
                {
                    _tbModelo.fechaHoraIngreso = Program.fechaHora;
                    _tbModelo.idUsuarioIngreso = idUsuario;
                    _tbModelo.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbModelo.AddObject(_tbModelo);
                }
                else
                {
                    _tbModelo.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbModelo.idUsuarioUltimaModificacion = idUsuario;
                    _tbModelo.terminalUltimaModificacion = Program.terminal;
                }
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmModelo_Load(null, null);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmModelo_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbModelo.idModelo == 0)
                MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbModelo.estadoRegistro = false;
                _tbModelo.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbModelo.idUsuarioUltimaModificacion = idUsuario;
                _tbModelo.terminalUltimaModificacion = Program.terminal;
                _tbModelo.fechaHoraEliminacion = Program.fechaHora;
                _tbModelo.idUsuarioEliminacion = idUsuario;
                _tbModelo.terminalEliminacion = Program.terminal;

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmModelo_Load(null, null);
            }
        }

        private void SetearModelo(tbModelo _tbModelo)
        {
            try
            {
                this._tbModelo = _tbModelo;
                cmbLinea.SelectedValue = this._tbModelo.tbSubGrupo.tbGrupo.idLinea;
                cmbLinea_SelectionChangeCommitted(null, null);
                cmbGrupo.SelectedValue = this._tbModelo.tbSubGrupo.idGrupo;
                cmbGrupo_SelectionChangeCommitted(null, null);
                cmbSubGrupo.SelectedValue = this._tbModelo.idSubGrupo;
                txtCodigo.Text = this._tbModelo.idSubGrupo.ToString();
                txtDescripcion.Text = this._tbModelo.descripcion;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Línea"), new DataColumn("Grupo"), new DataColumn("SubGrupo"), new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Fecha de registro"), new DataColumn("modelo", typeof(object)) });

            (from M in _dbCosolemEntities.tbModelo
             where M.estadoRegistro
             select new
             {
                 descripcionLinea = M.tbSubGrupo.tbGrupo.tbLinea.descripcion,
                 descripcionGrupo = M.tbSubGrupo.tbGrupo.descripcion,
                 descripcionSubGrupo = M.tbSubGrupo.descripcion,
                 idModelo = M.idModelo,
                 descripcion = M.descripcion,
                 fechaRegistro = M.fechaHoraIngreso,
                 modelo = M
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.descripcionLinea, x.descripcionGrupo, x.descripcionSubGrupo, x.idModelo, x.descripcion, x.fechaRegistro, x.modelo));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearModelo((tbModelo)_frmBusqueda._object);
        }

        private void frmModelo_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            _tbModelo = new tbModelo { estadoRegistro = true };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbModelo, EntityState.Detached);

            List<Linea> _tbLinea = (from L in _dbCosolemEntities.tbLinea where L.estadoRegistro select new Linea { idLinea = L.idLinea, descripcion = L.descripcion, tbGrupo = (from G in L.tbGrupo where G.estadoRegistro select new Grupo { idGrupo = G.idGrupo, descripcion = G.descripcion, tbSubGrupo = (from SG in G.tbSubGrupo where SG.estadoRegistro select new SubGrupo { idSubGrupo = SG.idSubGrupo, descripcion = SG.descripcion }) }) }).ToList();
            _tbLinea.Insert(0, new Linea { idLinea = 0, descripcion = "Seleccione", tbGrupo = new List<Grupo> { new Grupo { idGrupo = 0, descripcion = "Seleccione", tbSubGrupo = new List<SubGrupo> { new SubGrupo { idSubGrupo = 0, descripcion = "Seleccione" } } } } });
            cmbLinea.DataSource = _tbLinea;
            cmbLinea.ValueMember = "idLinea";
            cmbLinea.DisplayMember = "descripcion";
            cmbLinea_SelectionChangeCommitted(null, null);

            txtCodigo.Clear();
            txtDescripcion.Clear();
        }

        private void cmbLinea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Linea _tbLinea = (Linea)cmbLinea.SelectedItem;
            List<Grupo> _tbGrupo = _tbLinea.tbGrupo.ToList();
            if (_tbLinea.idLinea != 0) _tbGrupo.Insert(0, new Grupo { idGrupo = 0, descripcion = "Seleccione", tbSubGrupo = new List<SubGrupo> { new SubGrupo { idSubGrupo = 0, descripcion = "Seleccione" } } });
            cmbGrupo.DataSource = _tbGrupo;
            cmbGrupo.ValueMember = "idGrupo";
            cmbGrupo.DisplayMember = "descripcion";
            cmbGrupo_SelectionChangeCommitted(null, null);
        }

        private void cmbGrupo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Grupo _tbGrupo = (Grupo)cmbGrupo.SelectedItem;
            List<SubGrupo> _tbSubGrupo = _tbGrupo.tbSubGrupo.ToList();
            if (_tbGrupo.idGrupo != 0) _tbSubGrupo.Insert(0, new SubGrupo { idSubGrupo = 0, descripcion = "Seleccione" });
            cmbSubGrupo.DataSource = _tbSubGrupo;
            cmbSubGrupo.ValueMember = "idSubGrupo";
            cmbSubGrupo.DisplayMember = "descripcion";
        }
    }
}