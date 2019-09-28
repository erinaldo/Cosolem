using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Cosolem
{
    static class edmCosolemFunctions
    {
        public static DateTime getFechaHora()
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
                return _dbCosolemEntities.ExecuteStoreQuery<DateTime>("SELECT GETDATE()").First();
        }

        public static List<tbProcesarPrecio> getProcesarPrecios(long idEmpresa)
        {
            List<tbProcesarPrecio> getProcesarPrecios = new List<tbProcesarPrecio>();
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                getProcesarPrecios = (from PP in _dbCosolemEntities.tbProcesarPrecio where PP.idEmpresa == idEmpresa && PP.estadoRegistro && PP.procesarPrecio select PP).ToList();
            }
            getProcesarPrecios.ForEach(x =>
            {
                var _tbProducto = getProducto(x.idProducto);
                if (_tbProducto != null)
                {
                    x.marca = _tbProducto.marca;
                    x.linea = _tbProducto.linea;
                    x.grupo = _tbProducto.grupo;
                    x.subgrupo = _tbProducto.subgrupo;
                    x.modelo = _tbProducto.modelo;
                    x.producto = _tbProducto.producto;
                }
                tbCosto _tbCosto = getCosto(idEmpresa, x.idProducto);
                x.costo = _tbCosto == null ? 0 : _tbCosto.costo;
                tbIndiceComercial _tbIndiceComercial = getIndiceComercial(idEmpresa, x.idProducto);
                x.indiceComercial = _tbIndiceComercial == null ? 0 : _tbIndiceComercial.indiceComercial;
                tbIndiceFinanciero _tbIndiceFinanciero = getIndiceFinanciero(idEmpresa, x.idProducto);
                x.indiceFinanciero = _tbIndiceFinanciero == null ? 0 : _tbIndiceFinanciero.indiceFinanciero;
                tbIndiceInformativo _tbIndiceInformativo = getIndiceInformativo(idEmpresa, x.idProducto);
                x.indiceInformativo = _tbIndiceInformativo == null ? 0 : _tbIndiceInformativo.indiceInformativo;
            });
            return getProcesarPrecios;
        }

        public static bool isProductoInventariable(long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return _dbCosolemEntities.tbProducto.Where(x => x.idProducto == idProducto).FirstOrDefault().tbTipoProducto.esInventariable;
            }
        }

        static dynamic getProducto(long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return (from P in _dbCosolemEntities.tbProducto
                        where P.idProducto == idProducto && P.estadoRegistro
                        select new
                        {
                            marca = P.tbMarca.descripcion,
                            linea = P.tbModelo.tbSubGrupo.tbGrupo.tbLinea.descripcion,
                            grupo = P.tbModelo.tbSubGrupo.tbGrupo.descripcion,
                            subgrupo = P.tbModelo.tbSubGrupo.descripcion,
                            modelo = P.tbModelo.descripcion,
                            producto = P.codigoProducto + " - " + P.descripcion
                        }).FirstOrDefault();
            }
        }

        public static dynamic getProductos(string codigoProducto = null, bool productosQueTenganComplementarios = false)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                IQueryable<tbProducto> productos = (from P in _dbCosolemEntities.tbProducto where P.estadoRegistro select P);
                if (codigoProducto != null)
                    productos = productos.Where(x => x.codigoProducto == codigoProducto);
                if (productosQueTenganComplementarios)
                    productos = productos.Where(x => x.tbProductoComplementario.Count > 0);
                return (from P in productos
                        select new
                        {
                            marca = P.tbMarca.descripcion,
                            linea = P.tbModelo.tbSubGrupo.tbGrupo.tbLinea.descripcion,
                            grupo = P.tbModelo.tbSubGrupo.tbGrupo.descripcion,
                            subgrupo = P.tbModelo.tbSubGrupo.descripcion,
                            modelo = P.tbModelo.descripcion,
                            codigoProducto = P.codigoProducto,
                            descripcion = P.descripcion,
                            caracteristicas = P.caracteristicas,
                            fechaRegistro = P.fechaHoraIngreso,
                            producto = P
                        }).ToList();
            }
        }

        public static tbProcesarPrecio getProcesarPrecio(long idEmpresa, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return (from PP in _dbCosolemEntities.tbProcesarPrecio where PP.idEmpresa == idEmpresa && PP.idProducto == idProducto && PP.estadoRegistro select PP).FirstOrDefault();
            }
        }

        public static tbCosto getCosto(long idEmpresa, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                DateTime fechaActual = Program.fechaHora.Date;
                return (from C in _dbCosolemEntities.tbCosto where C.idEmpresa == idEmpresa && C.idProducto == idProducto && C.estadoRegistro && EntityFunctions.TruncateTime(C.fechaHasta) >= EntityFunctions.TruncateTime(fechaActual) select C).FirstOrDefault();
            }
        }

        public static tbIndiceComercial getIndiceComercial(long idEmpresa, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                DateTime fechaActual = Program.fechaHora.Date;
                return (from IC in _dbCosolemEntities.tbIndiceComercial where IC.idEmpresa == idEmpresa && IC.idProducto == idProducto && IC.estadoRegistro && EntityFunctions.TruncateTime(IC.fechaHasta) >= EntityFunctions.TruncateTime(fechaActual) select IC).FirstOrDefault();
            }
        }

        public static tbIndiceFinanciero getIndiceFinanciero(long idEmpresa, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                DateTime fechaActual = Program.fechaHora.Date;
                return (from IF in _dbCosolemEntities.tbIndiceFinanciero where IF.idEmpresa == idEmpresa && IF.idProducto == idProducto && IF.estadoRegistro && EntityFunctions.TruncateTime(IF.fechaHasta) >= EntityFunctions.TruncateTime(fechaActual) select IF).FirstOrDefault();
            }
        }

        public static tbIndiceInformativo getIndiceInformativo(long idEmpresa, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                DateTime fechaActual = Program.fechaHora.Date;
                return (from II in _dbCosolemEntities.tbIndiceInformativo where II.idEmpresa == idEmpresa && II.idProducto == idProducto && II.estadoRegistro && EntityFunctions.TruncateTime(II.fechaHasta) >= EntityFunctions.TruncateTime(fechaActual) select II).FirstOrDefault();
            }
        }

        public static long getFisicoDisponible(long idEmpresa, long idBodega, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return (from I in _dbCosolemEntities.tbInventario where I.tbBodega.tbTienda.estadoRegistro && I.tbBodega.estadoRegistro && I.tbBodega.esFacturable && I.estadoRegistro && I.tbBodega.tbTienda.idEmpresa == idEmpresa && I.idBodega == idBodega && I.idProducto == idProducto select I.fisicoDisponible - I.reservado).FirstOrDefault();
            }
        }

        public static long getFisicoDisponible(long idEmpresa, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return (from I in _dbCosolemEntities.tbInventario where I.tbBodega.tbTienda.estadoRegistro && I.tbBodega.estadoRegistro && I.tbBodega.esFacturable && I.estadoRegistro && I.tbBodega.tbTienda.idEmpresa == idEmpresa && I.idProducto == idProducto select I.fisicoDisponible - I.reservado).FirstOrDefault();
            }
        }

        public static long getFisicoDisponible(long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return (from I in _dbCosolemEntities.tbInventario where I.tbBodega.tbTienda.estadoRegistro && I.tbBodega.estadoRegistro && I.tbBodega.esFacturable && I.estadoRegistro && I.idProducto == idProducto select I.fisicoDisponible - I.reservado).FirstOrDefault();
            }
        }

        public static dynamic getInventario(long idEmpresa, long idBodega, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return (from I in _dbCosolemEntities.tbInventario where I.tbBodega.tbTienda.estadoRegistro && I.tbBodega.estadoRegistro && I.estadoRegistro && I.tbBodega.tbTienda.idEmpresa == idEmpresa && I.idBodega == idBodega && I.idProducto == idProducto select new { fisicoDisponible = I.fisicoDisponible, reservado = I.reservado, inventario = I.fisicoDisponible - I.reservado }).FirstOrDefault();
            }
        }

        public static List<clsInventarioGeneral> getInventarioGeneral(long idEmpresa, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return (from I in _dbCosolemEntities.tbInventario
                        where I.tbBodega.tbTienda.estadoRegistro && I.tbBodega.estadoRegistro && I.tbBodega.esFacturable && I.estadoRegistro && I.tbBodega.tbTienda.idEmpresa == idEmpresa && I.idProducto == idProducto
                        select new clsInventarioGeneral
                        {
                            idTienda = I.tbBodega.idTienda,
                            descripcionTienda = I.tbBodega.tbTienda.descripcion,
                            idBodega = I.idBodega,
                            descripcionBodega = I.tbBodega.descripcion,
                            fisicoDisponible = I.fisicoDisponible,
                            reservado = I.reservado,
                            inventario = I.fisicoDisponible - I.reservado
                        }).ToList();
            }
        }

        public static List<clsPrecio> getPrecios(long idEmpresa)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return (from P in _dbCosolemEntities.tbPrecio
                        join PR in _dbCosolemEntities.tbProducto on P.idProducto equals PR.idProducto
                        where P.idEmpresa == idEmpresa && P.estadoRegistro && PR.estadoRegistro
                        select new clsPrecio
                        {
                            marca = PR.tbMarca.descripcion,
                            linea = PR.tbModelo.tbSubGrupo.tbGrupo.tbLinea.descripcion,
                            grupo = PR.tbModelo.tbSubGrupo.tbGrupo.descripcion,
                            subgrupo = PR.tbModelo.tbSubGrupo.descripcion,
                            modelo = PR.tbModelo.descripcion,
                            producto = PR.codigoProducto + " - " + PR.descripcion,
                            precioOferta = P.precioOferta,
                            precioVentaPublico = P.precioVentaPublico,
                            precioInformativo = P.precioInformativo
                        }).ToList();
            }
        }

        public static tbPrecio getPrecio(long idEmpresa, long idProducto)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                return _dbCosolemEntities.tbPrecio.Where(x => x.idEmpresa == idEmpresa && x.idProducto == idProducto && x.estadoRegistro).FirstOrDefault();
            }
        }

        public static decimal getPorcentajeIVA(long idProducto, long idTienda)
        {
            decimal porcentajeIVA = 0;
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                if (_dbCosolemEntities.tbProducto.Where(x => x.idProducto == idProducto).FirstOrDefault().tbTipoProducto.aplicaIVA)
                    porcentajeIVA = _dbCosolemEntities.tbTienda.Where(x => x.idTienda == idTienda).FirstOrDefault().porcentajeIVA;
            }
            return porcentajeIVA / 100;
        }

        public static string getNombreUsuario(long idUsuario)
        {
            string nombreUsuario = String.Empty;
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                tbUsuario usuario = _dbCosolemEntities.tbUsuario.Where(x => x.idUsuario == idUsuario).FirstOrDefault();
                if (usuario != null) nombreUsuario = usuario.nombreUsuario;
            }
            return nombreUsuario;
        }

        public static string getLineas(List<long> listIdProductos)
        {
            string lineas = String.Empty;
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                lineas = String.Join(", ", _dbCosolemEntities.tbProducto.Where(x => listIdProductos.Contains(x.idProducto)).Select(x => x.tbModelo.tbSubGrupo.tbGrupo.tbLinea.descripcion).ToList());
            }
            return lineas;
        }

        public static bool existNumeroFactura(long idEmpresa, long idTienda, long numeroFactura)
        {
            bool exist = false;
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                exist = _dbCosolemEntities.tbOrdenVentaCabecera.Where(x => x.idEmpresaFactura == idEmpresa && x.idTiendaFactura == idTienda && x.numeroFactura == numeroFactura).Any();
            }
            return exist;
        }
    }
}
