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
    public partial class frmSubGrupo : Form
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
        }

        tbSubGrupo _tbSubGrupo = null;

        public frmSubGrupo()
        {
            InitializeComponent();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (((Linea)cmbLinea.SelectedItem).idLinea == 0) mensaje += "Seleccione línea\n";
            if (((Grupo)cmbGrupo.SelectedItem).idGrupo == 0) mensaje += "Seleccione grupo\n";
            if (String.IsNullOrEmpty(txtDescripcion.Text.Trim())) mensaje += "Ingrese descripción\n";

            if (String.IsNullOrEmpty(mensaje))
            {
                _tbSubGrupo.idGrupo = ((Grupo)cmbGrupo.SelectedItem).idGrupo;
                _tbSubGrupo.descripcion = txtDescripcion.Text.Trim();
                if (_tbSubGrupo.EntityState == EntityState.Detached)
                {
                    _tbSubGrupo.fechaHoraIngreso = Program.fechaHora;
                    _tbSubGrupo.idUsuarioIngreso = idUsuario;
                    _tbSubGrupo.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbSubGrupo.AddObject(_tbSubGrupo);
                }
                else
                {
                    _tbSubGrupo.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbSubGrupo.idUsuarioUltimaModificacion = idUsuario;
                    _tbSubGrupo.terminalUltimaModificacion = Program.terminal;
                }
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmSubGrupo_Load(null, null);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmSubGrupo_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbSubGrupo.idSubGrupo == 0)
                MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbSubGrupo.estadoRegistro = false;
                _tbSubGrupo.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbSubGrupo.idUsuarioUltimaModificacion = idUsuario;
                _tbSubGrupo.terminalUltimaModificacion = Program.terminal;
                _tbSubGrupo.fechaHoraEliminacion = Program.fechaHora;
                _tbSubGrupo.idUsuarioEliminacion = idUsuario;
                _tbSubGrupo.terminalEliminacion = Program.terminal;

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmSubGrupo_Load(null, null);
            }
        }

        private void SetearSubGrupo(tbSubGrupo _tbSubGrupo)
        {
            try
            {
                this._tbSubGrupo = _tbSubGrupo;
                cmbLinea.SelectedValue = this._tbSubGrupo.tbGrupo.idLinea;
                cmbLinea_SelectionChangeCommitted(null, null);
                cmbGrupo.SelectedValue = this._tbSubGrupo.idGrupo;
                txtCodigo.Text = this._tbSubGrupo.idSubGrupo.ToString();
                txtDescripcion.Text = this._tbSubGrupo.descripcion;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Línea"), new DataColumn("Grupo"), new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Fecha de registro"), new DataColumn("subgrupo", typeof(object)) });

            (from SG in _dbCosolemEntities.tbSubGrupo
             where SG.estadoRegistro
             select new
             {
                 descripcionLinea = SG.tbGrupo.tbLinea.descripcion,
                 descripcionGrupo = SG.tbGrupo.descripcion,
                 idSubGrupo = SG.idSubGrupo,
                 descripcion = SG.descripcion,
                 fechaRegistro = SG.fechaHoraIngreso,
                 subgrupo = SG
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.descripcionLinea, x.descripcionGrupo, x.idSubGrupo, x.descripcion, x.fechaRegistro, x.subgrupo));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearSubGrupo((tbSubGrupo)_frmBusqueda._object);
        }

        private void frmSubGrupo_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            _tbSubGrupo = new tbSubGrupo { estadoRegistro = true };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbSubGrupo, EntityState.Detached);

            List<Linea> _tbLinea = (from L in _dbCosolemEntities.tbLinea where L.estadoRegistro select new Linea { idLinea = L.idLinea, descripcion = L.descripcion, tbGrupo = (from G in L.tbGrupo where G.estadoRegistro select new Grupo { idGrupo = G.idGrupo, descripcion = G.descripcion }) }).ToList();
            _tbLinea.Insert(0, new Linea { idLinea = 0, descripcion = "Seleccione", tbGrupo = new List<Grupo> { new Grupo { idGrupo = 0, descripcion = "Seleccione" } } });
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
            if (_tbLinea.idLinea != 0) _tbGrupo.Insert(0, new Grupo { idGrupo = 0, descripcion = "Seleccione" });
            cmbGrupo.DataSource = _tbGrupo;
            cmbGrupo.ValueMember = "idGrupo";
            cmbGrupo.DisplayMember = "descripcion";
        }
    }
}