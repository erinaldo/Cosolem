using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Cosolem
{
    public static class Util
    {
        public static List<Control> ObtenerControles(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(x => ObtenerControles(x, type)).Concat(controls).Where(y => y.GetType() == type).ToList();
        }

        public static IEnumerable<dynamic> ObtenerHorarioLaboral()
        {
            List<TimeSpan> horarioLaboral = new List<TimeSpan>();
            for (int hour = 0; hour < 24; hour++)
            {
                horarioLaboral.Add(new TimeSpan(hour, 0, 0));
                horarioLaboral.Add(new TimeSpan(hour, 30, 0));
            }
            return horarioLaboral.Select(x => new { hora = x });
        }

        public static string GeneraContrasena()
        {
            const int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = 2;
            const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
            const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMERIC_CHARACTERS = "0123456789";
            const string SPECIAL_CHARACTERS = @"!#$%&*@\";
            string characterSet = "";
            characterSet += LOWERCASE_CHARACTERS;
            characterSet += UPPERCASE_CHARACTERS;
            characterSet += NUMERIC_CHARACTERS;
            characterSet += SPECIAL_CHARACTERS;
            char[] password = new char[18];
            int characterSetLength = characterSet.Length;
            System.Random random = new System.Random();
            for (int characterPosition = 0; characterPosition < 18; characterPosition++)
            {
                password[characterPosition] = characterSet[random.Next(characterSetLength - 1)];
                bool moreThanTwoIdenticalInARow = characterPosition > MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS && password[characterPosition] == password[characterPosition - 1] && password[characterPosition - 1] == password[characterPosition - 2];
                if (moreThanTwoIdenticalInARow) characterPosition--;
            }
            return string.Join(null, password);
        }

        public static bool ValidaEmail(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", RegexOptions.Compiled);
            return regex.IsMatch(email);
        }

        public static Image ObtenerImagen(byte[] imagen)
        {
            MemoryStream memoryStream = new MemoryStream(imagen);
            return Image.FromStream(memoryStream);
        }

        public static string FormatoMoneda(decimal value, int cantidadDecimales)
        {
            return value.ToString("c" + cantidadDecimales.ToString());
        }

        public static string FormatoNumero(decimal value, int cantidadDecimales)
        {
            return value.ToString("n" + cantidadDecimales.ToString());
        }

        public static string FormatoPorcentaje(decimal value, int cantidadDecimales)
        {
            return value.ToString("p" + cantidadDecimales.ToString());
        }

        private static string getUnidades(string numero)
        {
            string resultado = String.Empty;
            string[] unidades = { "", "un ", "dos ", "tres ", "cuatro ", "cinco ", "seis ", "siete ", "ocho ", "nueve " };
            string num = numero.Substring(numero.Length - 1);

            resultado = unidades[int.Parse(num)];

            return resultado;
        }

        private static string getDecenas(string numero)
        {
            string resultado = String.Empty;
            string[] decenas = { "diez ", "once ", "doce ", "trece ", "catorce ", "quince ", "dieciseis ", "diecisiete ", "dieciocho ", "diecinueve", "veinte ", "treinta ", "cuarenta ", "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa " };
            int n = int.Parse(numero);

            if (n < 10) resultado = getUnidades(numero);
            else if (n > 19)
            {
                string u = getUnidades(numero);
                if (u.Equals("")) resultado = decenas[int.Parse(numero.Substring(0, 1)) + 8];
                else resultado = decenas[int.Parse(numero.Substring(0, 1)) + 8] + "y " + u;
            }
            else resultado = decenas[n - 10];

            return resultado;
        }

        private static string getCentenas(string numero)
        {
            string resultado = String.Empty;
            string[] centenas = { "", "ciento ", "doscientos ", "trecientos ", "cuatrocientos ", "quinientos ", "seiscientos ", "setecientos ", "ochocientos ", "novecientos " };
            
            if (int.Parse(numero) > 99)
            {
                if (int.Parse(numero) == 100) resultado = " cien ";
                else resultado = centenas[int.Parse(numero.Substring(0, 1))] + getDecenas(numero.Substring(1));
            }
            else resultado = getDecenas(int.Parse(numero) + "");

            return resultado;
        }

        private static string getMiles(string numero)
        {
            string resultado = String.Empty;
            string c = numero.Substring(numero.Length - 3);
            string m = numero.Substring(0, numero.Length - 3);
            string n = String.Empty;

            if (int.Parse(m) > 0)
            {
                n = getCentenas(m);
                resultado = n + "mil " + getCentenas(c);
            }
            else resultado = "" + getCentenas(c);
            
            return resultado;
        }

        private static string getMillones(string numero)
        {
            string resultado = String.Empty;
            string miles = numero.Substring(numero.Length - 6);
            string millon = numero.Substring(0, numero.Length - 6);
            string n = String.Empty;

            if (millon.Length > 1) n = getCentenas(millon) + "millones ";
            else n = getUnidades(millon) + "millon ";

            resultado = n + getMiles(miles);

            return resultado;
        }

        public static string ConvertirNumeroALetras(string monto, bool enMayusculas, string moneda)
        {
            string resultado = String.Empty;

            Regex regex = new Regex(@"\d{1,9},\d{1,2}");

            string literal = String.Empty;
            string parteDecimal = String.Empty;

            monto = monto.Replace(".", ",");
            if (monto.IndexOf(",") == -1) monto = monto + ",00";

            MatchCollection matchCollection = regex.Matches(monto);
            if (matchCollection.Count > 0)
            {
                string[] numero = monto.Split(',');

                parteDecimal = moneda + " " + numero[1] + "/100";

                if (int.Parse(numero[0]) == 0) literal = "cero ";
                else if (int.Parse(numero[0]) > 999999) literal = getMillones(numero[0]);
                else if (int.Parse(numero[0]) > 999) literal = getMiles(numero[0]);
                else if (int.Parse(numero[0]) > 99) literal = getCentenas(numero[0]);
                else if (int.Parse(numero[0]) > 9) literal = getDecenas(numero[0]);
                else literal = getUnidades(numero[0]);
            }
            else
                literal = String.Empty;
            resultado = literal + parteDecimal;
            return enMayusculas ? resultado.ToUpper() : resultado;
        }

        public static string setFormatoNumeroFactura(long idEmpresa, long idTienda, long numeroFactura)
        {
            string formatoNumeroFactura = String.Empty;
            formatoNumeroFactura += new String('0', 3 - idEmpresa.ToString().Trim().Length) + idEmpresa.ToString().Trim();
            formatoNumeroFactura += new String('0', 3 - idTienda.ToString().Trim().Length) + idTienda.ToString().Trim();
            formatoNumeroFactura += new String('0', 9 - numeroFactura.ToString().Trim().Length) + numeroFactura.ToString().Trim();
            return formatoNumeroFactura;
        }

        public static void MostrarException(string titulo, Exception exception)
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(exception, true);
            System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrames()[stackTrace.FrameCount - 1];
            string innerExceptionMessage = "\n\nInnerException: Ninguna referencia";
            if (exception.InnerException != null) innerExceptionMessage = "\n\nInnerException: " + exception.InnerException.Message;
            System.Windows.Forms.MessageBox.Show("Mensaje de error: " + exception.Message + "\nError en línea " + stackFrame.GetFileLineNumber().ToString() + "\nArchivo: " + stackFrame.GetFileName() + "\nClase: " + stackFrame.GetMethod().DeclaringType.ToString() + "\nMétodo: " + stackFrame.GetMethod().Name + innerExceptionMessage, titulo, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public static string EncriptaValor(string valor, string clave)
        {
            byte[] value = Encoding.UTF8.GetBytes(valor);
            byte[] key = Encoding.UTF8.GetBytes(clave);
            key = SHA256.Create().ComputeHash(key);
            byte[] bytesEncrypted = Encrypt(value, key);
            return Convert.ToBase64String(bytesEncrypted);
        }

        public static string DesencriptaValor(string valor, string clave)
        {
            byte[] value = Convert.FromBase64String(valor);
            byte[] key = Encoding.UTF8.GetBytes(clave);
            key = SHA256.Create().ComputeHash(key);
            byte[] bytesDecrypted = Decrypt(value, key);
            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        static byte[] Encrypt(byte[] value, byte[] password)
        {
            byte[] Encrypt = null;
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged { KeySize = 256, BlockSize = 128, Mode = CipherMode.CBC })
                {
                    Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 1000);
                    rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(value, 0, value.Length);
                        cryptoStream.Close();
                    }
                    Encrypt = memoryStream.ToArray();
                }
            }
            return Encrypt;
        }

        static byte[] Decrypt(byte[] value, byte[] password)
        {
            byte[] Decrypt = null;
            var salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged { KeySize = 256, BlockSize = 128, Mode = CipherMode.CBC })
                {
                    Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 1000);
                    rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(value, 0, value.Length);
                        cryptoStream.Close();
                    }
                    Decrypt = memoryStream.ToArray();
                }
            }
            return Decrypt;
        }
    }
}
