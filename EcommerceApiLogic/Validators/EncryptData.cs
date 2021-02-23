using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EcommerceApiLogic.Validators
{
    public static class EncryptData
    {
        public static string Encrypt(string KeyCod, string value)
        {
            string Key = CreateKey(KeyCod);
            string Salt = CreateSalt(KeyCod);

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
                string oculto = Convert.ToBase64String(buffer.ToArray());
                return oculto;
            }
        }

        public static string Decrypt(string KeyCod, string value)
        {
            try
            {
                string Key = CreateKey(KeyCod);
                string Salt = CreateSalt(KeyCod);

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
            catch (FormatException ex)
            {
                throw new DbManagerDark.Exceptions.DarkExceptionSystem("Error al obtener clave real");
            }

        }

        public static string EncryptProd(string value)
        {
            string Key = CreateKey("JALPJCJ_2o2o");
            string Salt = CreateSalt("JALPJCJ_2o2osss");

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
                string oculto = Convert.ToBase64String(buffer.ToArray()); ;
                return oculto.Replace("+", "PLUS").Replace("-", "LESS").Replace("=", "EQQ").Replace("/", "SLA");
            }
        }

        public static string DecryptProd(string value)
        {
            try
            {
                string Key = CreateKey("JALPJCJ_2o2o");
                string Salt = CreateSalt("JALPJCJ_2o2osss");

                value = value.Replace("PLUS", "+").Replace("LESS", "-").Replace("EQQ", "=").Replace("SLA", "/");


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
            catch (FormatException ex)
            {
                throw new DbManagerDark.Exceptions.DarkExceptionSystem("Error al obtener clave real");
            }

        }

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
    }
}
