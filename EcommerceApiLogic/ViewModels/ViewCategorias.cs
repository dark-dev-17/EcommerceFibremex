using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.ViewModels
{
    [DarkTable(Name = "listar_categorias", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class ViewCategorias
    {
        [DarkColumn(Name = "id_categoria", IsMapped = true, IsKey = false)]
        public int IdCategoria { get; set; }
        [DarkColumn(Name = "id_codigo", IsMapped = true, IsKey = false)]
        public string Codigo { get; set; }
        [DarkColumn(Name = "desc_familia", IsMapped = true, IsKey = false)]
        public string DescripcionFam { get; set; }
        [DarkColumn(Name = "id_imagen", IsMapped = true, IsKey = false)]
        public string  ImagenPath { get; set; }
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }
        [DarkColumn(Name = "folder_name_categoria", IsMapped = true, IsKey = false)]
        public string FolderName { get; set; }
        [DarkColumn(Name = "identificador_subcategoria", IsMapped = true, IsKey = false)]
        public int IdentificadorCat { get; set; }
        [DarkColumn(Name = "t1_desc_subcategoria", IsMapped = true, IsKey = false)]
        public string DescrSubcategoria { get; set; }
        [DarkColumn(Name = "subnivel", IsMapped = true, IsKey = false)]
        public string Subnivel { get; set; }
        [DarkColumn(Name = "folder_name_subcategoria", IsMapped = true, IsKey = false)]
        public string FolderSubcategoria { get; set; }
        [DarkColumn(Name = "id_subcategoria_1", IsMapped = true, IsKey = false)]
        public int IdSubcategoria1 { get; set; }
        [DarkColumn(Name = "t2_id_subcategoria", IsMapped = true, IsKey = false)]
        public string idSubcategoria2 { get; set; }
        [DarkColumn(Name = "codigo", IsMapped = true, IsKey = false)]
        public string CodigoCod { get; set; }
        [DarkColumn(Name = "t2_desc_subcategoria", IsMapped = true, IsKey = false)]
        public string DescSubcategoria { get; set; }
        [DarkColumn(Name = "cable_codigo", IsMapped = true, IsKey = false)]
        public string CableCode { get; set; }
        [DarkColumn(Name = "configuracion_mobile", IsMapped = true, IsKey = false)]
        public int ConfigMobile { get; set; }
        [DarkColumn(Name = "activo_subcategorias_n1", IsMapped = true, IsKey = false)]
        public string ActtivoSubcat1 { get; set; }
    }
}
