using ConfigurableCode.Models;
using ConfigurableCode.Versions;
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

        public Manager() 
        {
            
        }
        public ConfigurableJ GetSon(string name)
        {
            return JsonConvert.DeserializeObject<ConfigurableJ>(Open(name));
        }
        public string Open(string Name)
        {
            string Result = "";
            string path = string.Format(@"{0}{1}", Path, Name);
            if (File.Exists(path))
            {
                Result = File.ReadAllText(path);
            }
            else
            {
                throw new Exception(string.Format("el archivo: {0} no fue encontrado", path));
            }
            return Result;
        }
    }

    public enum ActionElement
    {
        Add = 1,
        Edit = 2,
        Delete = 3
    }
}
