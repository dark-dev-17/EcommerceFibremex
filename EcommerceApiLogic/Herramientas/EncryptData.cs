using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EcommerceApiLogic.Herramientas
{
    public class EncrypData : IDisposable
    {

        #region Propiedades
        private string Key { get; set; }
        private string Salt { get; set; }
        #endregion

        #region Constructor
        public EncrypData(string codigoBase)
        {
            this.Key = CreateKey(codigoBase);
            this.Salt = CreateSalt(codigoBase);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CreateKey(string codigoBase)
        {
            string result = "";
            result = string.Format("{0}56{1}A9HHh", codigoBase, Herramienta.ReverseCadena(codigoBase));

            return result;
        }
        public string CreateSalt(string codigoBase)
        {
            string result = "";
            result = string.Format("{0}12{1}4576sdv", codigoBase, Herramienta.ReverseCadena(codigoBase));

            return result;
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

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~EncrypData() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
