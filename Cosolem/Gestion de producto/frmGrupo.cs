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
    public partial class frmGrupo : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Linea
        {
            public long idLinea { get; set; }
            public string descripcion { get; set; }
        }

        tbGrupo _tbGrupo = null;

        public frmGrupo()
        {
            InitializeComponent();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            if (((Linea)cmbLinea.SelectedItem).idLinea == 0) mensaje += "Seleccione línea\n";
            if (String.IsNullOrEmpty(txtDescripcion.Text.Trim())) mensaje += "Ingrese descripción\n";

            if (String.IsNullOrEmpty(mensaje))
            {
                _tbGrupo.idLinea = ((Linea)cmbLinea.SelectedItem).idLinea;
                _tbGrupo.descripcion = txtDescripcion.Text.Trim();
                if (_tbGrupo.EntityState == EntityState.Detached)
                {
                    _tbGrupo.fechaHoraIngreso = Program.fechaHora;
                    _tbGrupo.idUsuarioIngreso = idUsuario;
                    _tbGrupo.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbGrupo.AddObject(_tbGrupo);
                }
                else
                {
                    _tbGrupo.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbGrupo.idUsuarioUltimaModificacion = idUsuario;
                    _tbGrupo.terminalUltimaModificacion = Program.terminal;
                }
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmGrupo_Load(null, null);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmGrupo_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbGrupo.idGrupo == 0)
                MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbGrupo.estadoRegistro = false;
                _tbGrupo.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbGrupo.idUsuarioUltimaModificacion = idUsuario;
                _tbGrupo.terminalUltimaModificacion = Program.terminal;
                _tbGrupo.fechaHoraEliminacion = Program.fechaHora;
                _tbGrupo.idUsuarioEliminacion = idUsuario;
                _tbGrupo.terminalEliminacion = Program.terminal;

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmGrupo_Load(null, null);
            }
        }

        private void SetearGrupo(tbGrupo _tbGrupo)
        {
            try
            {
                this._tbGrupo = _tbGrupo;
                cmbLinea.SelectedValue = this._tbGrupo.idLinea;
                txtCodigo.Text = this._tbGrupo.idGrupo.ToString();
                txtDescripcion.Text = this._tbGrupo.descripcion;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Línea"), new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Fecha de registro"), new DataColumn("grupo", typeof(object)) });

            (from G in _dbCosolemEntities.tbGrupo
             where G.estadoRegistro
             select new
             {
                 descripcionLinea = G.tbLinea.descripcion,
                 idGrupo = G.idGrupo,
                 descripcion = G.descripcion,
                 fechaRegistro = G.fechaHoraIngreso,
                 grupo = G
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.descripcionLinea, x.idGrupo, x.descripcion, x.fechaRegistro, x.grupo));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearGrupo((tbGrupo)_frmBusqueda._object);
        }

        private void frmGrupo_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            _tbGrupo = new tbGrupo { estadoRegistro = true };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbGrupo, EntityState.Detached);

            List<Linea> _tbLinea = (from L in _dbCosolemEntities.tbLinea where L.estadoRegistro select new Linea { idLinea = L.idLinea, descripcion = L.descripcion }).ToList();
            _tbLinea.Insert(0, new Linea { idLinea = 0, descripcion = "Seleccione" });
            cmbLinea.DataSource = _tbLinea;
            cmbLinea.ValueMember = "idLinea";
            cmbLinea.DisplayMember = "descripcion";

            txtCodigo.Clear();
            txtDescripcion.Clear();
        }
    }
}
