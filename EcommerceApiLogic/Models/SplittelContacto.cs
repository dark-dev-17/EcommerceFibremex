using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;


namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "datos_generales_contacto", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class SplittelContacto
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdSplittelContacto { get; set; }

        [DarkColumn(Name = "ubicacion", IsMapped = true, IsKey = false)]
        public string Ubicacion { get; set; }

        [DarkColumn(Name = "telefono", IsMapped = true, IsKey = false)]
        public string Telfono { get; set; }

        [DarkColumn(Name = "email", IsMapped = true, IsKey = false)]
        public string Email { get; set; }

        [DarkColumn(Name = "horario", IsMapped = true, IsKey = false)]
        public string Horario { get; set; }

        [DarkColumn(Name = "pagina", IsMapped = true, IsKey = false)]
        public string Pagina { get; set; }

        [DarkColumn(Name = "texto", IsMapped = true, IsKey = false)]
        public string Texto { get; set; }

        [DarkColumn(Name = "telefono1", IsMapped = true, IsKey = false)]
        public string Telefono1 { get; set; }

        [DarkColumn(Name = "webservice", IsMapped = true, IsKey = false)]
        public string WebService { get; set; }
    }
}
