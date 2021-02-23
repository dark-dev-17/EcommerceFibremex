using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "listar_productos_fijos", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class ProductoFijo
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = false)]
        public int Id { get; set; }

        [DarkColumn(Name = "codigo", IsMapped = true, IsKey = true)]
        public string Codigo { get; set; }

        [DarkColumn(Name = "desc_producto", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        [DarkColumn(Name = "desc_ceo", IsMapped = true, IsKey = false)]
        public string DescCEO { get; set; }

        [DarkColumn(Name = "producto_id_desc_larga", IsMapped = true, IsKey = false)]
        public string IdDescripcionLarga { get; set; }

        [DarkColumn(Name = "subcategoria", IsMapped = true, IsKey = false)]
        public string SubCategoria { get; set; }

        [DarkColumn(Name = "producto_id_marca", IsMapped = true, IsKey = false)]
        public string IdMarca { get; set; }

        [DarkColumn(Name = "precio", IsMapped = true, IsKey = false)]
        public float Precio { get; set; }

        [DarkColumn(Name = "descuento_producto", IsMapped = true, IsKey = false)]
        public float Descuento { get; set; }

        [DarkColumn(Name = "existencia", IsMapped = true, IsKey = false)]
        public float Existencia { get; set; }

        [DarkColumn(Name = "info_tecnica", IsMapped = true, IsKey = false)]
        public string InfoTecnica { get; set; }

        [DarkColumn(Name = "pesos_dimensiones", IsMapped = true, IsKey = false)]
        public string PesoDimensional { get; set; }

        [DarkColumn(Name = "info_adicional", IsMapped = true, IsKey = false)]
        public string InfoAdicional { get; set; }

        [DarkColumn(Name = "productos_relacionados", IsMapped = true, IsKey = false)]
        public string ProductosRelacionados { get; set; }

        [DarkColumn(Name = "codigo_configurable", IsMapped = true, IsKey = false)]
        public string CodigoConfigurable { get; set; }

        [DarkColumn(Name = "producto_activo", IsMapped = true, IsKey = false)]
        public string ProductoActivo { get; set; }

        [DarkColumn(Name = "manejado_por", IsMapped = true, IsKey = false)]
        public string ManejadoPor { get; set; }

        [DarkColumn(Name = "img_principal", IsMapped = true, IsKey = false)]
        public string ImgPrincipal { get; set; }

        [DarkColumn(Name = "costo_envio", IsMapped = true, IsKey = false)]
        public string CostoEnvio { get; set; }

        [DarkColumn(Name = "desc_larga", IsMapped = true, IsKey = false)]
        public string DescripcionLarga { get; set; }

        [DarkColumn(Name = "id_codigo", IsMapped = true, IsKey = false)]
        public string IdCategoria { get; set; }

        [DarkColumn(Name = "desc_familia", IsMapped = true, IsKey = false)]
        public string DescripcionCategoria { get; set; }

        [DarkColumn(Name = "id_imagen", IsMapped = true, IsKey = false)]
        public string IdImagen { get; set; }

        [DarkColumn(Name = "categoria_activo", IsMapped = true, IsKey = false)]
        public string CategoriaActiva { get; set; }

        [DarkColumn(Name = "menu1", IsMapped = true, IsKey = false)]
        public string Menu1 { get; set; }

        [DarkColumn(Name = "menu2", IsMapped = true, IsKey = false)]
        public string Menu2 { get; set; }

        [DarkColumn(Name = "folder_name", IsMapped = true, IsKey = false)]
        public string FolderName { get; set; }

        [DarkColumn(Name = "categoria_descripcion", IsMapped = true, IsKey = false)]
        public string CategoriaDescripcion { get; set; }

        [DarkColumn(Name = "id_subcategoria", IsMapped = true, IsKey = false)]
        public string IdSubcategoria { get; set; }

        [DarkColumn(Name = "desc_subcategoria", IsMapped = true, IsKey = false)]
        public string DescripcionSubcategoria { get; set; }

        [DarkColumn(Name = "subnivel", IsMapped = true, IsKey = false)]
        public string Subnivel { get; set; }

        [DarkColumn(Name = "subcategoria_activo", IsMapped = true, IsKey = false)]
        public string SubcategoriaActiva { get; set; }

        [DarkColumn(Name = "subcategoria_descripcion", IsMapped = true, IsKey = false)]
        public string SubcategoriaDescripcion { get; set; }

        [DarkColumn(Name = "desc_marca", IsMapped = true, IsKey = false)]
        public string MarcaDescripcion { get; set; }

        [DarkColumn(Name = "id_producto", IsMapped = true, IsKey = false)]
        public string ProductoFicha { get; set; }

        [DarkColumn(Name = "id_ficha", IsMapped = true, IsKey = false)]
        public string FichaProducto { get; set; }

        [DarkColumn(Name = "ficha_id", IsMapped = true, IsKey = false)]
        public string FichaID { get; set; }

        [DarkColumn(Name = "ruta", IsMapped = true, IsKey = false)]
        public string RutaFicha { get; set; }

        [DarkColumn(Name = "ImagenesSlide", IsMapped = false, IsKey = false)]
        public List<string> SlideImg { get; set; }
        [DarkColumn(Name = "ImagenesSlide", IsMapped = false, IsKey = false)]
        public List<string> SlideImgAdicionales { get; set; }
        [DarkColumn(Name = "ImagenesSlide", IsMapped = false, IsKey = false)]
        public List<string> SlideImgDescripcion { get; set; }


        [DarkColumn(Name = "ruta", IsMapped = false, IsKey = false)]
        public string EncryptID { get { return Validators.EncryptData.EncryptProd(Codigo); } }

    }
}
