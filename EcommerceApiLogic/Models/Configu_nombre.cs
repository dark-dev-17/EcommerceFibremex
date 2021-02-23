using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "t17_nombres_productos_configurables", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Configu_nombre
    {
        /// <summary>
        /// id
        /// </summary>
        [DarkColumn(Name = "t17_pk01", IsMapped = true, IsKey = true)]
        public int IdConfigu_nombre { get; set; }

        /// <summary>
        /// Codigo 
        /// </summary>
        [DarkColumn(Name = "t17_f001", IsMapped = true, IsKey = false)]
        public string Codigo { get; set; }

        /// <summary>
        /// Codigo del configurable
        /// </summary>
        [DarkColumn(Name = "t17_f002", IsMapped = true, IsKey = false)]
        public string CodCodfigurable { get; set; }

        /// <summary>
        /// Descripcion del configurable
        /// </summary>
        [DarkColumn(Name = "t17_f003", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Creado
        /// </summary>
        [DarkColumn(Name = "t17_f098", IsMapped = true, IsKey = false)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Actualizado
        /// </summary>
        [DarkColumn(Name = "t17_f099", IsMapped = true, IsKey = false)]
        public DateTime Updated { get; set; }
    }
}
