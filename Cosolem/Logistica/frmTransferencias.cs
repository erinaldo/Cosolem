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
    public partial class frmTransferencias : Form
    {
        dbCosolemEntities _dbCosolemEntities = null;
        long idEmpresa = Program.tbUsuario.tbEmpleado.idEmpresa;
        long idUsuario = Program.tbUsuario.idUsuario;

        class Tienda
        {
            public long idTienda { get; set; }
            public string descripcion { get; set; }
            public IEnumerable<Bodega> tbBodega { get; set; }
        }

        class Bodega
        {
            public long idBodega { get; set; }
            public string descripcion { get; set; }
        }

        class Producto
        {
            public long idProducto { get; set; }
            public string descripcion { get; set; }
        }

        public frmTransferencias()
        {
            InitializeComponent();
        }

        private void frmTransferencias_Load(object sender, EventArgs e)
        {
            _dbCosolemEntities = new dbCosolemEntities();

            List<Tienda> tiendas = (from T in _dbCosolemEntities.tbTienda where T.estadoRegistro && T.idEmpresa == idEmpresa select new Tienda { idTienda = T.idTienda, descripcion = T.descripcion, tbBodega = (from B in T.tbBodega where B.estadoRegistro select new Bodega { idBodega = B.idBodega, descripcion = B.descripcion }) }).ToList();
            tiendas.Insert(0, new Tienda { idTienda = 0, descripcion = "Seleccione", tbBodega = new List<Bodega> { new Bodega { idBodega = 0, descripcion = "Seleccione" } } });

            cmbTiendaSaliente.BindingContext = new BindingContext();
            cmbTiendaSaliente.DataSource = tiendas;
            cmbTiendaSaliente.ValueMember = "idTienda";
            cmbTiendaSaliente.DisplayMember = "descripcion";
            cmbTiendaSaliente_SelectionChangeCommitted(null, null);

            List<Producto> productos = (from P in _dbCosolemEntities.tbProducto where P.estadoRegistro orderby P.descripcion select new Producto { idProducto = P.idProducto, descripcion = P.descripcion }).ToList();
            productos.Insert(0, new Producto { idProducto = 0, descripcion = "Seleccione" });
            cmbProducto.DataSource = productos;
            cmbProducto.ValueMember = "idProducto";
            cmbProducto.DisplayMember = "descripcion";
            cmbProducto_SelectionChangeCommitted(null, null);

            cmbTiendaEntrante.BindingContext = new BindingContext();
            cmbTiendaEntrante.DataSource = tiendas;
            cmbTiendaEntrante.ValueMember = "idTienda";
            cmbTiendaEntrante.DisplayMember = "descripcion";
            cmbTiendaEntrante_SelectionChangeCommitted(null, null);
        }

        private void cmbTiendaSaliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Tienda _tbTienda = (Tienda)cmbTiendaSaliente.SelectedItem;
            List<Bodega> _tbBodega = _tbTienda.tbBodega.ToList();
            if (_tbTienda.idTienda != 0) _tbBodega.Insert(0, new Bodega { idBodega = 0, descripcion = "Seleccione" });
            cmbBodegaSaliente.BindingContext = new BindingContext();
            cmbBodegaSaliente.DataSource = _tbBodega;
            cmbBodegaSaliente.ValueMember = "idBodega";
            cmbBodegaSaliente.DisplayMember = "descripcion";
            cmbProducto_SelectionChangeCommitted(null, null);
        }

        private void cmbTiendaEntrante_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Tienda _tbTienda = (Tienda)cmbTiendaEntrante.SelectedItem;
            List<Bodega> _tbBodega = _tbTienda.tbBodega.ToList();
            if (_tbTienda.idTienda != 0) _tbBodega.Insert(0, new Bodega { idBodega = 0, descripcion = "Seleccione" });
            cmbBodegaEntrante.BindingContext = new BindingContext();
            cmbBodegaEntrante.DataSource = _tbBodega;
            cmbBodegaEntrante.ValueMember = "idBodega";
            cmbBodegaEntrante.DisplayMember = "descripcion";
        }

        private void cmbBodegaSaliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbProducto_SelectionChangeCommitted(null, null);
        }

        private void txtCantidadTransferir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void cmbProducto_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtFisicoDisponible.Text = "0";
            txtReservado.Text = "0";
            txtInventario.Text = "0";
            txtCantidadTransferir.Text = "0";

            long idBodegaSaliente = 0;
            Bodega _tbBodega = (Bodega)cmbBodegaSaliente.SelectedItem;
            if (_tbBodega != null) idBodegaSaliente = _tbBodega.idBodega;

            long idProducto = 0;
            Producto _tbProducto = (Producto)cmbProducto.SelectedItem;
            if (_tbProducto != null) idProducto = _tbProducto.idProducto;

            var _tbInventario = edmCosolemFunctions.getInventario(idEmpresa, idBodegaSaliente, idProducto);
            if (_tbInventario != null)
            {
                txtFisicoDisponible.Text = _tbInventario.fisicoDisponible.ToString();
                txtReservado.Text = _tbInventario.reservado.ToString();
                txtInventario.Text = _tbInventario.inventario.ToString();
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmTransferencias_Load(null, null);
        }

        private void tsbGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                long cantidadTransferir = 0;
                if (!String.IsNullOrEmpty(txtCantidadTransferir.Text.Trim())) cantidadTransferir = Convert.ToInt64(txtCantidadTransferir.Text.Trim());

                cmbProducto_SelectionChangeCommitted(null, null);

                txtCantidadTransferir.Text = cantidadTransferir.ToString();

                string mensaje = String.Empty;

                long idBodegaSaliente = ((Bodega)cmbBodegaSaliente.SelectedItem).idBodega;
                long idBodegaEntrante = ((Bodega)cmbBodegaEntrante.SelectedItem).idBodega;
                Producto _tbProducto = (Producto)cmbProducto.SelectedItem;

                long inventario = 0;
                if (!String.IsNullOrEmpty(txtInventario.Text.Trim())) inventario = Convert.ToInt64(txtInventario.Text.Trim());
                if (((Tienda)cmbTiendaSaliente.SelectedItem).idTienda == 0) mensaje += "Seleccione tienda saliente\n";
                if (idBodegaSaliente == 0) mensaje += "Seleccione bodega saliente\n";
                if (((Tienda)cmbTiendaEntrante.SelectedItem).idTienda == 0) mensaje += "Seleccione tienda entrante\n";
                if (idBodegaEntrante == 0) mensaje += "Seleccione bodega entrante\n";
                if (_tbProducto.idProducto == 0) mensaje += "Seleccione producto\n";
                if (cantidadTransferir == 0) mensaje += "Ingrese cantidad a transferir\n";
                if (idBodegaSaliente == idBodegaEntrante) mensaje += "Bodega saliente y entrante no pueden ser iguales\n";
                if (cantidadTransferir > inventario) mensaje += "Cantidad a transferir mayor a la del inventario\n";

                tbCosto _tbCosto = edmCosolemFunctions.getCosto(idEmpresa, _tbProducto.idProducto);
                if (_tbCosto == null || _tbCosto.costo == 0) mensaje += "Producto no tiene costo\n";

                if (String.IsNullOrEmpty(mensaje.Trim()))
                {
                    tbTransaccionInventario _tbTransaccionInventario = null;

                    _tbTransaccionInventario = new tbTransaccionInventario();
                    _tbTransaccionInventario.tipoTransaccion = "Transferencia de inventario";
                    _tbTransaccionInventario.idBodega = idBodegaSaliente;
                    _tbTransaccionInventario.idProducto = _tbProducto.idProducto;
                    _tbTransaccionInventario.cantidad = -cantidadTransferir;
                    _tbTransaccionInventario.estadoRegistro = true;
                    _tbTransaccionInventario.fechaHoraIngreso = Program.fechaHora;
                    _tbTransaccionInventario.idUsuarioIngreso = idUsuario;
                    _tbTransaccionInventario.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbTransaccionInventario.AddObject(_tbTransaccionInventario);

                    _tbTransaccionInventario = new tbTransaccionInventario();
                    _tbTransaccionInventario.tipoTransaccion = "Transferencia de inventario";
                    _tbTransaccionInventario.idBodega = idBodegaEntrante;
                    _tbTransaccionInventario.idProducto = _tbProducto.idProducto;
                    _tbTransaccionInventario.cantidad = cantidadTransferir;
                    _tbTransaccionInventario.estadoRegistro = true;
                    _tbTransaccionInventario.fechaHoraIngreso = Program.fechaHora;
                    _tbTransaccionInventario.idUsuarioIngreso = idUsuario;
                    _tbTransaccionInventario.terminalIngreso = Program.terminal;
                    _dbCosolemEntities.tbTransaccionInventario.AddObject(_tbTransaccionInventario);

                    tbInventario _tbInventario = null;

                    _tbInventario = (from I in _dbCosolemEntities.tbInventario where I.idBodega == idBodegaSaliente && I.idProducto == _tbTransaccionInventario.idProducto && I.estadoRegistro select I).FirstOrDefault();
                    if (_tbInventario == null)
                    {
                        _tbInventario = new tbInventario();
                        _tbInventario.idBodega = idBodegaSaliente;
                        _tbInventario.idProducto = _tbProducto.idProducto;
                        _tbInventario.fisicoDisponible = cantidadTransferir;
                        _tbInventario.reservado = 0;
                        _tbInventario.estadoRegistro = true;
                        _tbInventario.fechaHoraIngreso = Program.fechaHora;
                        _tbInventario.idUsuarioIngreso = idUsuario;
                        _tbInventario.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbInventario.AddObject(_tbInventario);
                    }
                    else
                    {
                        _tbInventario.fisicoDisponible = _tbInventario.fisicoDisponible - cantidadTransferir;
                        _tbInventario.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbInventario.idUsuarioUltimaModificacion = idUsuario;
                        _tbInventario.terminalUltimaModificacion = Program.terminal;
                    }

                    _tbInventario = (from I in _dbCosolemEntities.tbInventario where I.idBodega == idBodegaEntrante && I.idProducto == _tbTransaccionInventario.idProducto && I.estadoRegistro select I).FirstOrDefault();
                    if (_tbInventario == null)
                    {
                        _tbInventario = new tbInventario();
                        _tbInventario.idBodega = idBodegaEntrante;
                        _tbInventario.idProducto = _tbProducto.idProducto;
                        _tbInventario.fisicoDisponible = cantidadTransferir;
                        _tbInventario.reservado = 0;
                        _tbInventario.estadoRegistro = true;
                        _tbInventario.fechaHoraIngreso = Program.fechaHora;
                        _tbInventario.idUsuarioIngreso = idUsuario;
                        _tbInventario.terminalIngreso = Program.terminal;
                        _dbCosolemEntities.tbInventario.AddObject(_tbInventario);
                    }
                    else
                    {
                        _tbInventario.fisicoDisponible = _tbInventario.fisicoDisponible + cantidadTransferir;
                        _tbInventario.fechaHoraUltimaModificacion = Program.fechaHora;
                        _tbInventario.idUsuarioUltimaModificacion = idUsuario;
                        _tbInventario.terminalUltimaModificacion = Program.terminal;
                    }

                    long fisicoDisponible = edmCosolemFunctions.getFisicoDisponible(idEmpresa, idBodegaSaliente, _tbProducto.idProducto);
                    if (cantidadTransferir > fisicoDisponible)
                        MessageBox.Show("Cantidad a transferir mayor a la del inventario", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        _dbCosolemEntities.SaveChanges();
                        MessageBox.Show("Registro grabado satisfactoriamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmTransferencias_Load(null, null);
                    }
                }
                else
                    MessageBox.Show(mensaje, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Util.MostrarException(this.Text, ex);
            }
        }
    }
}