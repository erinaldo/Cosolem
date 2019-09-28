using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cosolem
{
    public class rptGeneracionContrasena
    {
        public string razonSocial { get; set; }
        public string nombreCompleto { get; set; }
        public string descripcionCargo { get; set; }
        public string descripcionDepartamento { get; set; }
        public string nombreUsuario { get; set; }
        public string correoElectronico { get; set; }
        public string contrasena { get; set; }
        public string numeroIdentificacion { get; set; }
        public string usuario { get; set; }
        public string terminal { get; set; }
    }

    public class rptCotizacion
    {
        public long idOrdenVentaCabecera { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string usuario { get; set; }
        public string tipoIdentificacion { get; set; }
        public string numeroIdentificacion { get; set; }
        public string formaPago { get; set; }
        public string cliente { get; set; }
        public string direccion { get; set; }
        public string referencia { get; set; }
        public string telefonos { get; set; }
        public string producto { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal subTotal { get; set; }
        public decimal descuento { get; set; }
        public decimal subTotalBruto { get; set; }
        public string etiquetaIVA { get; set; }
        public decimal IVA { get; set; }
        public decimal totalNeto { get; set; }
    }

    public class rptMovimientoInventario
    {
        public string tipoMovimiento { get; set; }
        public long idBodega { get; set; }
        public string descripcionBodega { get; set; }
        public long idProducto { get; set; }
        public string codigoProducto { get; set; }
        public string descripcionProducto { get; set; }
        public long cantidadIngreso { get; set; }
        public long cantidadEgreso { get; set; }
        public long cantidad { get; set; }
        public DateTime fechaHoraMovimiento { get; set; }
        public string usuarioGeneroMovimiento { get; set; }
    }

    public class rptIngresoInventario
    {
        public string concepto { get; set; }
        public DateTime fechaFactura { get; set; }
        public string cliente { get; set; }
        public string comentarios { get; set; }
        public string nombreUsuario { get; set; }
        public string nombreCompleto { get; set; }
        public string codigoProducto { get; set; }
        public string descripcionProducto { get; set; }
        public long inventario { get; set; }
        public long cantidad { get; set; }
        public string descripcionBodega { get; set; }
    }
    
    public class clsInventarioGeneral
    {
        public long idTienda { get; set; }
        public string descripcionTienda { get; set; }
        public long idBodega { get; set; }
        public string descripcionBodega { get; set; }
        public long fisicoDisponible { get; set; }
        public long reservado { get; set; }
        public long inventario { get; set; }
    }

    public partial class tbDireccion
    {
        public string descripcionProvincia { get; set; }
        public string descripcionCanton { get; set; }
        public string descripcionTipoDireccion { get; set; }
    }

    public partial class tbHorarioLaboral
    {
        public string descripcionDiaSemana { get; set; }
    }

    public partial class tbFormacionAcademica
    {
        public string descripcionProvincia { get; set; }
        public string descripcionCanton { get; set; }
        public string descripcionTipoFormacionAcademica { get; set; }
    }

    public partial class tbProducto
    {
        public bool seleccionado { get; set; }
        public string descripcionTipoProducto { get; set; }
        public string producto { get; set; }
        public long idCosto { get; set; }
        public decimal costo { get; set; }
        public long idIndiceComercial { get; set; }
        public decimal indiceComercial { get; set; }
        public long idIndiceFinanciero { get; set; }
        public decimal indiceFinanciero { get; set; }
        public long idIndiceInformativo { get; set; }
        public decimal indiceInformativo { get; set; }
        public DateTime fechaHasta { get; set; }
        public long idProcesarPrecio { get; set; }
    }

    public partial class tbProductoComplementario
    {
        public string producto { get; set; }
        public string caracteristicas { get; set; }
    }

    public partial class tbBodega
    {
        public bool seleccionado { get; set; }
    }

    public partial class tbProcesarPrecio
    {
        public bool seleccionado { get; set; }
        public string marca { get; set; }
        public string linea { get; set; }
        public string grupo { get; set; }
        public string subgrupo { get; set; }
        public string modelo { get; set; }
        public string producto { get; set; }
        public decimal costo { get; set; }
        public decimal indiceComercial { get; set; }
        public decimal indiceFinanciero { get; set; }
        public decimal indiceInformativo { get; set; }
    }

    public partial class tbOrdenVentaCabecera
    {
        public bool seleccionado { get; set; }
        public string usuarioIngreso { get; set; }
        public string descripcionFormaPago { get; set; }
        public string descripcionCliente { get; set; }
        public string descripcionEstadoOrdenVenta { get; set; }
    }

    public partial class tbOrdenVentaDetalle
    {
        public string codigoProducto { get; set; }
        public string descripcionProducto { get; set; }
        public string descripcionBodegaSalida { get; set; }
        public long idBodegaEntrada { get; set; }
        public string descripcionBodegaEntrada { get; set; }
    }

    public partial class tbInventario
    {
        public string concepto { get; set; }
        public DateTime fechaFactura { get; set; }
        public string cliente { get; set; }
        public string comentarios { get; set; }
        public string codigoProducto { get; set; }
        public string descripcionProducto { get; set; }
        public long cantidad { get; set; }
    }

    public partial class tbSeguimientoCotizacionDetalle
    {
        public long idOrdenVentaCabecera { get; set; }
        public string cliente { get; set; }
        public int idEstadoOrdenVenta { get; set; }
        public DateTime fechaSeguimiento { get; set; }
    }

    public partial class tbOrdenPedidoDetalle
    {
        public string descripcionProducto { get; set; }
        public long fisicoDisponible { get; set; }
    }

    public partial class tbOrdenCompraDetalle
    {
        public string descripcionProducto { get; set; }
    }

    public partial class tbOrdenTrabajo
    {
        public bool seleccionado { get; set; }
        public string estadoAgendamiento { get; set; }
        public string tecnicoAsignado { get; set; }
    }

    public partial class clsPrecio
    {
        public string marca { get; set; }
        public string linea { get; set; }
        public string grupo { get; set; }
        public string subgrupo { get; set; }
        public string modelo { get; set; }
        public string producto { get; set; }
        public decimal precioOferta { get; set; }
        public decimal precioVentaPublico { get; set; }
        public decimal precioInformativo { get; set; }
    }
}