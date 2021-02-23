using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Resquest
{
    public class ConfNombre
    {
        /// <summary>
        /// Codigo 
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Codigo del configurable
        /// </summary>
        public string CodCodfigurable { get; set; }

        /// <summary>
        /// Descripcion del configurable
        /// </summary>
        public string Descripcion { get; set; }
    }
}
