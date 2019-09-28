using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace Cosolem
{
    static class Program
    {
        public static char decimalPoint;

        public static DateTime fechaHora;
        
        public static string terminal
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

        public static tbUsuario tbUsuario = null;

        [STAThread]
        static void Main()
        {
            Application.CurrentCulture = new System.Globalization.CultureInfo("es-EC");
            decimalPoint = Convert.ToChar(Application.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            fechaHora = edmCosolemFunctions.getFechaHora();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new frmInicioSesion().Show();
            Application.Run();
        }
    }
}
