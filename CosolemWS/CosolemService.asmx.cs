using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net.NetworkInformation;

namespace CosolemWS
{
    public class CosolemService : System.Web.Services.WebService
    {
        string terminal
        {
            get
            {
                string _terminal = String.Empty;
                foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        foreach (UnicastIPAddressInformation unicastIPAddressInformation in networkInterface.GetIPProperties().UnicastAddresses)
                        {
                            if (unicastIPAddressInformation.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                _terminal = unicastIPAddressInformation.Address.ToString();
                        }
                    }
                }
                return _terminal;
            }
        }


        [WebMethod]
        public string IniciarSesion(string nombreUsuario, string contrasena)
        {
            using (dbCosolemEntities _dbCosolemEntities = new dbCosolemEntities())
            {
                long idUsuario = 0;
                tbUsuario usuario = _dbCosolemEntities.tbUsuario.Where(x => x.nombreUsuario == nombreUsuario).FirstOrDefault();
                if (usuario != null) idUsuario = usuario.idUsuario;
                contrasena = Util.EncriptaValor(contrasena, idUsuario.ToString());
                usuario = _dbCosolemEntities.tbUsuario.Include("tbEmpleado.tbPersona").Include("tbEmpleado.tbEmpresa").Include("tbEmpleado.tbTienda").Include("tbUsuarioOpcion.tbOpcion.tbModulo").Where(x => x.nombreUsuario == nombreUsuario && x.contrasena == contrasena).FirstOrDefault();
                if (usuario != null)
                {
                    if (!usuario.fechaHoraPrimerAcceso.HasValue && usuario.terminalPrimerAcceso == null)
                    {
                        usuario.fechaHoraPrimerAcceso = edmCosolemFunctions.getFechaHora();
                        usuario.terminalPrimerAcceso = terminal;
                    }
                    _dbCosolemEntities.SaveChanges();
                    _dbCosolemEntities.Dispose();
                    return "Ok";
                }
                else
                    return "Usuario y/o contraseña incorrectos, favor verificar";
            }
        }
    }
}
