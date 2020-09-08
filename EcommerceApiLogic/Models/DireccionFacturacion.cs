using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "datos_facturacion", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class DireccionFacturacion
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdDireccionFacturacion { get; set; }

        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = false)]
        public int IdCliente { get; set; }

        [DarkColumn(Name = "razon_social", IsMapped = true, IsKey = false)]
        public string RazonSocial { get; set; }

        [DarkColumn(Name = "tipo_facturacion", IsMapped = true, IsKey = false)]
        public string TipoFacturacion { get; set; }

        [DarkColumn(Name = "rfc", IsMapped = true, IsKey = false)]
        public string RFC { get; set; }

        [DarkColumn(Name = "calle", IsMapped = true, IsKey = false)]
        public string Calle { get; set; }

        [DarkColumn(Name = "n_ext", IsMapped = true, IsKey = false)]
        public string NoExterior { get; set; }

        [DarkColumn(Name = "n_int", IsMapped = true, IsKey = false)]
        public string NoInterior { get; set; }

        [DarkColumn(Name = "cp", IsMapped = true, IsKey = false)]
        public string CP { get; set; }

        [DarkColumn(Name = "estado", IsMapped = true, IsKey = false)]
        public string Estado { get; set; }

        [DarkColumn(Name = "ciudad", IsMapped = true, IsKey = false)]
        public string Ciudad { get; set; }

        [DarkColumn(Name = "delegacion", IsMapped = true, IsKey = false)]
        public string Delegacion { get; set; }

        [DarkColumn(Name = "colonia", IsMapped = true, IsKey = false)]
        public string Colonia { get; set; }

        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }
    }
}
