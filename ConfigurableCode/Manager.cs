using ConfigurableCode.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConfigurableCode
{
    public class Manager
    {
        private readonly string Path = @"C:\Splittel\Ecommerce\Configuraciones\";
        public Configurable Configurable { private set; get; }

        public Manager() 
        {
            
        }

        #region Valores CRUD
        public void AddValor(string IdElement, string IdValor, string Label)
        {
            if (string.IsNullOrEmpty(IdElement))
                throw new Exception("IdElement elemento is null");
            if (string.IsNullOrEmpty(IdValor))
                throw new Exception("IdValor elemento is null");
            if (string.IsNullOrEmpty(Label))
                throw new Exception("Label elemento is null");

            var Old_element = Configurable.Elementos.FirstOrDefault(a => a.IdElemClave == IdElement);
            if (Old_element == null)
            {
                throw new Exception(string.Format("Elemento '{0}' not found in the '{1}' configration", IdElement, Configurable.Nombre));
            }

            if(Old_element.Valores.FirstOrDefault(a => a.IdValor == IdValor) != null)
            {
                throw new Exception(string.Format("Elemento '{0}' has already the value '{1}'", IdElement, IdValor));
            }

            Valores valores = new Valores
            {
                Descripcion = Label,
                IdValor = IdValor,
            };

            Old_element.Valores.Add(valores);
        }
        public void UpdateValor(string IdElement, string IdValor, string Label)
        {

        }
        public void DeleteValor(string IdElement, string IdValor)
        {

        }
        #endregion

        #region Elemento CRUD
        public void AddElement(string Nombre)
        {
            if(string.IsNullOrEmpty(Nombre))
                throw new Exception("Nombre elemento is null");
            var elemento = new Elemento
            {
                IdElemento = Configurable.Elementos.Count() + 1,
                Descripcion = Nombre,
                Valores = new List<Valores>()
            };

            Configurable.Elementos.Add(elemento);
        }
        public void UpdateElement(string Nombre, string IdElemento)
        {
            if (string.IsNullOrEmpty(Nombre))
                throw new Exception("Nombre elemento is null");
            if (string.IsNullOrEmpty(IdElemento))
                throw new Exception("IdElemento elemento is null");

            var Old_element = Configurable.Elementos.FirstOrDefault(a => a.IdElemClave == IdElemento);
            if (Old_element == null)
            {
                throw new Exception(string.Format("Elemento '{0}' not found in the '{1}' configration", IdElemento, Configurable.Nombre));
            }
            Old_element.Descripcion = Nombre;
        }
        public void DeleteElement(string IdElemento)
        {
            if (string.IsNullOrEmpty(IdElemento))
                throw new Exception("IdElemento elemento is null");

            var Old_element = Configurable.Elementos.FirstOrDefault(a => a.IdElemClave == IdElemento);
            if (Old_element == null)
            {
                throw new Exception(string.Format("Elemento '{0}' not found in the '{1}' configration", IdElemento, Configurable.Nombre));
            }

            int index = Configurable.Elementos.FindIndex(element => element.IdElemClave == IdElemento);
            Configurable.Elementos.RemoveAt(index);
        }
        #endregion

        #region Generales
        public string CreateNewConfiguracion(string NombreConfiguraion, string Descripción)
        {
            Configurable = new Configurable();
            Configurable.Nombre = NombreConfiguraion;
            Configurable.Descripción = Descripción;
            Configurable.Elementos = new List<Elemento>();

            string path = string.Format(@"{0}Confi_{1}.json", Path, NombreConfiguraion + "");
            // Create the file, or overwrite if the file exists.
            using (FileStream fs = File.Create(path, 1024))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(Configurable));
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }

            return string.Format(@"Confi_{0}.json", NombreConfiguraion + "");
        }

        public void SaveChanges(string NombreConfiguraion)
        {
            string path = string.Format(@"{0}{1}", Path, NombreConfiguraion + "");
            var json = JsonConvert.SerializeObject(Configurable);
            if (File.Exists(path))
            {
                File.WriteAllText(path, json);
            }
            else
            {
                throw new Exception(string.Format("el archivo: {0} no fue encontrado", path));
            }
        }

        public void LoadConfiguration(string configuracionName)
        {
            string path = string.Format(@"{0}{1}", Path, configuracionName);
            if (File.Exists(path))
            {
                Configurable = JsonConvert.DeserializeObject<Configurable>(File.ReadAllText(path));
            }
            else
            {
                throw new Exception(string.Format("el archivo: {0} no fue encontrado", path));
            }
        }
        #endregion
    }

    public enum ActionElement
    {
        Add = 1,
        Edit = 2,
        Delete = 3
    }
}
