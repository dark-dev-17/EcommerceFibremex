using System;
using System.Collections.Generic;
using System.Text;
using DbManagerDark.Attributes;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "menu_subcategorias", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class SubCategoria
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public string Id { get; set; }

        [DarkColumn(Name = "id_subcategoria", IsMapped = true, IsKey = false)]
        public string IdSubcategoria { get; set; }

        [DarkColumn(Name = "desc_subcategoria", IsMapped = true, IsKey = false)]
        public string SubDescripcion { get; set; }

        [DarkColumn(Name = "id_familia", IsMapped = true, IsKey = false)]
        public string IdFamilia { get; set; }

        [DarkColumn(Name = "subnivel", IsMapped = true, IsKey = false)]
        public string SubNivel { get; set; }

        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [DarkColumn(Name = "descripcion", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }
    }
}
