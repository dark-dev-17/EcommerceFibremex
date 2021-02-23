using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "listar_comentarios", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class ProductoComent
    {
        [DarkColumn(Name = "t01_pk01", IsMapped = true, IsKey = false)]
        public int IdProductoComent { get; set; }

        [DarkColumn(Name = "t01_f001", IsMapped = true, IsKey = false)]
        public string Titulo { get; set; }

        [DarkColumn(Name = "t01_f002", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        [DarkColumn(Name = "t01_f003", IsMapped = true, IsKey = false)]
        public int Estrellas { get; set; }

        [DarkColumn(Name = "t01_f004", IsMapped = true, IsKey = false)]
        public int Activo { get; set; }

        [DarkColumn(Name = "IdCliente", IsMapped = true, IsKey = false)]
        public int IdCliente { get; set; }

        [DarkColumn(Name = "IdProducto", IsMapped = true, IsKey = false)]
        public string CodigoProd { get; set; }

        [DarkColumn(Name = "IdCategoria", IsMapped = true, IsKey = false)]
        public string IdCategoria { get; set; }

        [DarkColumn(Name = "Usuario", IsMapped = true, IsKey = false)]
        public string Usuario { get; set; }
    }

    [DarkTable(Name = "", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class ProductoComentProm
    {
        public int IdProductoComentProm { get; set; }
        public int TotalComents { get; set; }
        public double Promedio { get; set; }
        public string Usuario { get; set; }

    }
}
