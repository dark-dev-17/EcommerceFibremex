using ConfigurableCode.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConfigurableCode.Controllers
{
    public class MainV1Ctrl
    {
        private readonly string Path = @"C:\Splittel\Ecommerce\Configuraciones\";
        private Configurable configurable;
        private string NameFile;

        public Configurable Configurable { get { return configurable; } }
        public PartesCrl PartesCrl { get; internal set; }
        public OpcionesCtrl OpcionesCtrl { get; internal set; }
        public MainV1Ctrl(string NameFile)
        {
            this.NameFile = NameFile;
        }

        public void Save()
        {
            string path = string.Format(@"{0}{1}", Path, NameFile);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(configurable);
            if (File.Exists(path))
            {
                File.WriteAllText(path, json);
            }
            else
            {
                throw new Exception(string.Format("el archivo: {0} no fue encontrado", path));
            }
        }

        public void Open()
        {
            if (string.IsNullOrEmpty(NameFile))
            {
                throw new Exception(string.Format("Archivo '{0}' de archivo no valido", NameFile));
            }
            string Result = "";
            string path = string.Format(@"{0}{1}", Path, NameFile);
            if (File.Exists(path))
            {
                Result = File.ReadAllText(path);
            }
            else
            {
                throw new Exception(string.Format("el archivo: {0} no fue encontrado", path));
            }
            configurable = JsonConvert.DeserializeObject<Configurable>(Result);

            PartesCrl = new PartesCrl(configurable);
            OpcionesCtrl = new OpcionesCtrl(configurable);
        }

        public void Create()
        {
            string path = string.Format(@"{0}{1}.json", Path, NameFile);

            if (File.Exists(path))
            {
                throw new Exception(string.Format("el archivo: {0} fue encontrado, intenta con otro nombre", path));
            }

            configurable = new Configurable() {
                CodeExample = "",
                Id = 1,
                Nombre = NameFile,
                ExpresionRegular = "",
                Partes = new List<Parte>()
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(configurable);
            // Create the file, or overwrite if the file exists.
            using (FileStream fs = File.Create(path, 1024))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(json);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }

    }
}
