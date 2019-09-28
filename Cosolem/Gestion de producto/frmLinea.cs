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
    public partial class frmLinea : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        tbLinea _tbLinea = null;

        public frmLinea()
        {
            InitializeComponent();
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDescripcion.Text.Trim()))
                MessageBox.Show("Ingrese descripción", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbLinea.descripcion = txtDescripcion.Text.Trim();
                if (_tbLinea.EntityState == EntityState.Detached)
                {
                    _tbLinea.fechaHoraIngreso = Program.fechaHora;
                    _tbLinea.idUsuarioIngreso = idUsuario;
                    _tbLinea.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbLinea.AddObject(_tbLinea);
                }
                else
                {
                    _tbLinea.fechaHoraUltimaModificacion = Program.fechaHora;
                    _tbLinea.idUsuarioUltimaModificacion = idUsuario;
                    _tbLinea.terminalUltimaModificacion = Program.terminal;
                }
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmLinea_Load(null, null);
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmLinea_Load(null, null);
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (_tbLinea.idLinea == 0)
                MessageBox.Show("Seleccione un registro para poder eliminarlo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _tbLinea.estadoRegistro = false;
                _tbLinea.fechaHoraUltimaModificacion = Program.fechaHora;
                _tbLinea.idUsuarioUltimaModificacion = idUsuario;
                _tbLinea.terminalUltimaModificacion = Program.terminal;
                _tbLinea.fechaHoraEliminacion = Program.fechaHora;
                _tbLinea.idUsuarioEliminacion = idUsuario;
                _tbLinea.terminalEliminacion = Program.terminal;

                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro eliminado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmLinea_Load(null, null);
            }
        }

        private void SetearLinea(tbLinea _tbLinea)
        {
            try
            {
                this._tbLinea = _tbLinea;
                txtCodigo.Text = this._tbLinea.idLinea.ToString();
                txtDescripcion.Text = this._tbLinea.descripcion;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            DataTable _DataTable = new DataTable();
            _DataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Código"), new DataColumn("Descripción"), new DataColumn("Fecha de registro"), new DataColumn("linea", typeof(object)) });

            (from L in _dbCosolemEntities.tbLinea
             where L.estadoRegistro
             select new
             {
                 idLinea = L.idLinea,
                 descripcion = L.descripcion,
                 fechaRegistro = L.fechaHoraIngreso,
                 linea = L
             }).ToList().ForEach(x => _DataTable.Rows.Add(x.idLinea, x.descripcion, x.fechaRegistro, x.linea));

            frmBusqueda _frmBusqueda = new frmBusqueda(this.Text, _DataTable);
            if (_frmBusqueda.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SetearLinea((tbLinea)_frmBusqueda._object);
        }

        private void frmLinea_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            _tbLinea = new tbLinea { estadoRegistro = true };
            _dbCosolemEntities.ObjectStateManager.ChangeObjectState(_tbLinea, EntityState.Detached);

            txtCodigo.Clear();
            txtDescripcion.Clear();
        }
    }
}