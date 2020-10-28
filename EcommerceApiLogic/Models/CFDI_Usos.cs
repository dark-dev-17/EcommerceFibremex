using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "t08_comprobantes_CFDI", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class CFDI_Usos
    {
        [DarkColumn(Name = "t08_pk01", IsMapped = true, IsKey = true)]
        public int IdUsoCFDI { get; set; }

        [DarkColumn(Name = "t08_f001", IsMapped = true, IsKey = false)]
        public string Clave { get; set; }

        [DarkColumn(Name = "t08_f002", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }
    }
}
