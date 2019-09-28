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
    public partial class frmNivelesAcceso : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Usuario
        {
            public long idUsuario { get; set; }
            public string descripcion { get; set; }
            public System.Data.Objects.DataClasses.EntityCollection<tbUsuarioOpcion> tbUsuarioOpcion { get; set; }
        }

        public frmNivelesAcceso()
        {
            InitializeComponent();
        }

        private void frmNivelesAcceso_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            List<Usuario> tbUsuario = (from U in _dbCosolemEntities.tbUsuario where U.estadoRegistro select new Usuario { idUsuario = U.idUsuario, descripcion = U.nombreUsuario + " - " + U.tbEmpleado.tbPersona.nombreCompleto, tbUsuarioOpcion = U.tbUsuarioOpcion }).ToList();
            tbUsuario.Insert(0, new Usuario { idUsuario = 0, descripcion = "Seleccione", tbUsuarioOpcion = null });
            cmbUsuario.DataSource = tbUsuario;
            cmbUsuario.ValueMember = "idUsuario";
            cmbUsuario.DisplayMember = "descripcion";

            lvwOpciones.Items.Clear();
            lvwOpciones.Groups.Clear();

            var _tbModulo = (from M in _dbCosolemEntities.tbModulo orderby M.ordenPosicion select new { id = M.idModulo, descripcion = M.descripcion, tbOpcion = M.tbOpcion }).ToList();
            foreach (var tbModulo in _tbModulo)
            {
                ListViewGroup modulo = new ListViewGroup(tbModulo.descripcion);
                lvwOpciones.Groups.Add(modulo);
                foreach (tbOpcion _tbOpcion in tbModulo.tbOpcion.ToList())
                    lvwOpciones.Items.Add(new ListViewItem { Tag = _tbOpcion.idOpcion, Text = _tbOpcion.descripcion, Group = modulo });
            }
        }

        private void cmbUsuario_SelectionChangeCommitted(object sender, EventArgs e)
        {
            foreach (ListViewItem listViewItem in lvwOpciones.Items) listViewItem.Checked = false;

            Usuario tbUsuario = (Usuario)cmbUsuario.SelectedItem;
            if (tbUsuario.tbUsuarioOpcion != null && tbUsuario.tbUsuarioOpcion.Count > 0)
            {
                foreach (ListViewItem listViewItem in lvwOpciones.Items)
                {
                    tbUsuarioOpcion usuarioOpcion = tbUsuario.tbUsuarioOpcion.Where(x => x.idOpcion == (int)listViewItem.Tag).FirstOrDefault();
                    if (usuarioOpcion != null) listViewItem.Checked = usuarioOpcion.tieneAcceso;
                }
            }
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;

            Usuario tbUsuario = (Usuario)cmbUsuario.SelectedItem;
            if (tbUsuario.idUsuario == 0) mensaje += "Seleccione usuario\n";

            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                foreach (ListViewItem listViewItem in lvwOpciones.Items)
                {
                    long idUsuario = tbUsuario.idUsuario;
                    int idOpcion = (int)listViewItem.Tag;
                    tbUsuarioOpcion _tbUsuarioOpcion = (from UO in _dbCosolemEntities.tbUsuarioOpcion where UO.idUsuario == idUsuario && UO.idOpcion == idOpcion select UO).FirstOrDefault();
                    if (_tbUsuarioOpcion == null)
                    {
                        _tbUsuarioOpcion = new tbUsuarioOpcion();
                        _tbUsuarioOpcion.idUsuario = idUsuario;
                        _tbUsuarioOpcion.idOpcion = idOpcion;
                        _tbUsuarioOpcion.fechaHoraIngreso = Program.fechaHora;
                        _tbUsuarioOpcion.idUsuarioIngreso = idUsuario;
                        _tbUsuarioOpcion.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbUsuarioOpcion.AddObject(_tbUsuarioOpcion);
                    }
                    else
                    {
                        _tbUsuarioOpcion.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbUsuarioOpcion.idUsuarioUltimaModificacion = idUsuario;
                        _tbUsuarioOpcion.terminalUltimaModificacion = Program.terminal;
                    }
                    _tbUsuarioOpcion.tieneAcceso = listViewItem.Checked;
                    _tbUsuarioOpcion.estadoRegistro = true;
                }
                _dbCosolemEntities.SaveChanges();

                MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmNivelesAcceso_Load(null, null);
            }
            else
                MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmNivelesAcceso_Load(null, null);
        }
    }
}
