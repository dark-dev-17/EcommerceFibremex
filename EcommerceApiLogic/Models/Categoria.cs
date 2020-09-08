using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "menu_categorias", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Categoria
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdCategoria { get; set; }

        [DarkColumn(Name = "id_codigo", IsMapped = true, IsKey = false)]
        public string Codigo { get; set; }

        [DarkColumn(Name = "desc_familia", IsMapped = true, IsKey = false)]
        public string DescripcionFamilia { get; set; }

        [DarkColumn(Name = "descripcion", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        [DarkColumn(Name = "orden", IsMapped = true, IsKey = false)]
        public int Orden { get; set; }

        [DarkColumn(Name = "orden", IsMapped = false, IsKey = false)]
        public List<SubCategoria> SubCategorias { get; set; }

    }
}
