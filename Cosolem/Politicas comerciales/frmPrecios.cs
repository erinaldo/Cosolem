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
    public partial class frmPrecios : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idUsuario = Program.tbUsuario.idUsuario;

        List<clsPrecio> listaPrecio = null;

        public frmPrecios()
        {
            InitializeComponent();
        }

        private void frmPrecios_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            dgvListaPrecios.AutoGenerateColumns = false;
            dgvProcesarPrecios.AutoGenerateColumns = false;

            listaPrecio = edmCosolemFunctions.getPrecios(idEmpresa);
            dgvListaPrecios.DataSource = listaPrecio;
            dgvProcesarPrecios.DataSource = edmCosolemFunctions.getProcesarPrecios(idEmpresa);
            tabControl1.SelectedTab = tabPage1;
        }

        private void tsbProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvProcesarPrecios_CellEndEdit(null, null);
                List<tbProcesarPrecio> _ListtbProcesarPrecio = ((List<tbProcesarPrecio>)dgvProcesarPrecios.DataSource).Where(x => x.seleccionado).ToList();

                string mensaje = String.Empty;

                if (_ListtbProcesarPrecio.Count == 0) mensaje += "Seleccione al menos 1 precio para proceder a procesarlo\n";
                if (_ListtbProcesarPrecio.Where(x => x.costo <= 0 || x.indiceComercial <= 0 || x.indiceFinanciero <= 0 || x.indiceInformativo <= 0).Any()) mensaje += "Hay productos seleccionados con costo, índice comercial, índice financiero o índice informativo menor o igual que 0, favor verificar\n";


                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    _ListtbProcesarPrecio.ForEach(x =>
                    {
                        tbProcesarPrecio _tbProcesarPrecio = (from PP in _dbCosolemEntities.tbProcesarPrecio where PP.idProcesarPrecio == x.idProcesarPrecio select PP).FirstOrDefault();
                        if (_tbProcesarPrecio != null)
                        {
                            _tbProcesarPrecio.procesarPrecio = false;
                            _tbProcesarPrecio.estadoRegistro = false;
                            _tbProcesarPrecio.fechaHoraUltimaModificacion = Program.fechaHora;
                            _tbProcesarPrecio.idUsuarioUltimaModificacion = idUsuario;
                            _tbProcesarPrecio.terminalUltimaModificacion = Program.terminal;
                        }
                        _dbCosolemEntities.tbPrecio.Where(y => y.idEmpresa == idEmpresa && y.idProducto == x.idProducto && y.estadoRegistro).ToList().ForEach(z =>
                        {
                            z.estadoRegistro = false;
                            z.fechaHoraUltimaModificacion = Program.fechaHora;
                            z.idUsuarioUltimaModificacion = idUsuario;
                            z.terminalUltimaModificacion = Program.terminal;
                            z.fechaHoraEliminacion = Program.fechaHora;
                            z.idUsuarioEliminacion = idUsuario;
                            z.terminalEliminacion = Program.terminal;
                        });
                        tbPrecio _tbPrecio = new tbPrecio();
                        _tbPrecio.idEmpresa = idEmpresa;
                        _tbPrecio.idProducto = x.idProducto;
                        _tbPrecio.costo = x.costo;
                        _tbPrecio.precioOferta = _tbPrecio.costo * x.indiceComercial;
                        _tbPrecio.precioVentaPublico = _tbPrecio.precioOferta * x.indiceFinanciero;
                        _tbPrecio.precioInformativo = _tbPrecio.precioVentaPublico * x.indiceInformativo;
                        _tbPrecio.estadoRegistro = true;
                        _tbPrecio.fechaHoraIngreso = Program.fechaHora;
                        _tbPrecio.idUsuarioIngreso = idUsuario;
                        _tbPrecio.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbPrecio.AddObject(_tbPrecio);
                    });
                    _dbCosolemEntities.SaveChanges();

                    MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmPrecios_Load(null, null);
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }

        private void dgvProcesarPrecios_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvProcesarPrecios.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void txtFiltroBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<clsPrecio> listaPrecio = this.listaPrecio;
                if (!String.IsNullOrEmpty(txtFiltroBusqueda.Text.Trim())) listaPrecio = listaPrecio.Where(x => (x.marca + x.linea + x.grupo + x.subgrupo + x.modelo + x.producto).ToUpper().Contains(txtFiltroBusqueda.Text.Trim().ToUpper())).ToList();
                dgvListaPrecios.DataSource = listaPrecio;
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }
    }
}