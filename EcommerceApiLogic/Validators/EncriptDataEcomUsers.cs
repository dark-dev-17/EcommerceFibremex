using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace EcommerceApiLogic.Validators
{
    public class EncriptDataEcomUsers
    {
        #region Propiedades
        private string Key { get; set; }
        private string Salt { get; set; }
        #endregion

        #region Constructor
        public EncriptDataEcomUsers(string codigoBase)
        {
            this.Key = CreateKey(codigoBase);
            this.Salt = CreateSalt(codigoBase);
        }
        #endregion

        #region Metodos
        private static string CreateKey(string codigoBase)
        {
            string result = "";
            result = string.Format("{0}56{1}A9HHh", codigoBase, ReverseCadena(codigoBase));

            return result;
        }
        private static string CreateSalt(string codigoBase)
        {
            string result = "";
            result = string.Format("{0}12{1}4576sdv", codigoBase, ReverseCadena(codigoBase));

            return result;
        }

        private static string ReverseCadena(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public string Encrypt(string value)
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(Key, Encoding.Unicode.GetBytes(Salt));
            SymmetricAlgorithm algorithm = new TripleDESCryptoServiceProvider();
            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);
            ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);
            using (MemoryStream buffer = new MemoryStream())
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }
                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        public string Decrypt(string value)
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(Key, Encoding.Unicode.GetBytes(Salt));
            SymmetricAlgorithm algorithm = new TripleDESCryptoServiceProvider();
            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);
            ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, rgbIV);
            using (MemoryStream buffer = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
        #endregion
    }
}