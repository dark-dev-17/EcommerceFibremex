using EcommerceApiLogic.Diccionarios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EcommerceApiLogic.Rules
{
    public class MunicipioCtrl
    {
        private readonly string Path = @"C:\Splittel\EcommerceApi\Diccionarios\";

        public MunicipioCtrl()
        {

        }

        public Municipio GetMunicipios(string Estado)
        {
            return LoadData().Find(a => a.Estado == Estado);
        }
        public List<Municipio> GetMunicipios()
        {
            return LoadData();
        }

        private List<Municipio> LoadData()
        {
            string path = string.Format(@"{0}Municipios.json", Path);
            if (!File.Exists(path))
            {
                throw new Exception(string.Format("el archivo: {0} no fue encontrado", path));
            }

            return JsonConvert.DeserializeObject<List<Municipio>>(File.ReadAllText(path));
        }
    }
}
