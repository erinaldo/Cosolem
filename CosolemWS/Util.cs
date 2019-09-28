using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CosolemWS
{
    public static class Util
    {
        public static string EncriptaValor(string valor, string clave)
        {
            byte[] value = Encoding.UTF8.GetBytes(valor);
            byte[] key = Encoding.UTF8.GetBytes(clave);
            key = SHA256.Create().ComputeHash(key);
            byte[] bytesEncrypted = Encrypt(value, key);
            return Convert.ToBase64String(bytesEncrypted);
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
    }
}