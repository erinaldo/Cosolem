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
    public partial class frmMarca : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        tbMarca _tbMarca = null;

        public frmMarca()
        {
            InitializeComponent();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDescripcion.Text.Trim()))
                MessageBox.Show("Ingrese descripción", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbMarca.descripcion = txtDescripcion.Text.Trim();
                if (_tbMarca.EntityState == EntityState.Detached)
                {
                    _tbMarca.fechaHoraIngreso = Program.fechaHora;
                    _tbMarca.idUsuarioIngreso = idUsuario;
                    _tbMarca.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbMarca.AddObject(_tbMarca);
                }
                else
                {
                    _tbMarca.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbMarca.idUsuarioUltimaModificacion = idUsuario;
                    _tbMarca.terminalUltimaModificacion = Program.terminal;
                }
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMarca_Load(null, null);
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmMarca_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbMarca.idMarca == 0)
                MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbMarca.estadoRegistro = false;
                _tbMarca.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbMarca.idUsuarioUltimaModificacion = idUsuario;
                _tbMarca.terminalUltimaModificacion = Program.terminal;
                _tbMarca.fechaHoraEliminacion = Program.fechaHora;
                _tbMarca.idUsuarioEliminacion = idUsuario;
                _tbMarca.terminalEliminacion = Program.terminal;

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMarca_Load(null, null);
            }
        }

        private void SetearMarca(tbMarca _tbMarca)
        {
            try
            {
                this._tbMarca = _tbMarca;
                txtCodigo.Text = this._tbMarca.idMarca.ToString();
                txtDescripcion.Text = this._tbMarca.descripcion;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Fecha de registro"), new DataColumn("marca", typeof(object)) });

            (from M in _dbCosolemEntities.tbMarca
             where M.estadoRegistro
             select new
             {
                 idMarca = M.idMarca,
                 descripcion = M.descripcion,
                 fechaRegistro = M.fechaHoraIngreso,
                 marca = M
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.idMarca, x.descripcion, x.fechaRegistro, x.marca));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearMarca((tbMarca)_frmBusqueda._object);
        }

        private void frmMarca_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            _tbMarca = new tbMarca { estadoRegistro = true };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbMarca, EntityState.Detached);

            txtCodigo.Clear();
            txtDescripcion.Clear();
        }
    }
}