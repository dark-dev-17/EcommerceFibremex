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

        public Manager() 
        {
            
        }
    }

    public enum ActionElement
    {
        Add = 1,
        Edit = 2,
        Delete = 3
    }
}
