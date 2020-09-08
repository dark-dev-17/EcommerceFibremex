using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "cotizacion_detalle", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class DetallePedido
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdDetalle { get; set; }

        [DarkColumn(Name = "id_cotizacion", IsMapped = true, IsKey = false)]
        public int IdPedido { get; set; }

        [DarkColumn(Name = "codigo", IsMapped = true, IsKey = false)]
        public string CodigoProducto { get; set; }

        [DarkColumn(Name = "cantidad", IsMapped = true, IsKey = false)]
        public int Cantidad { get; set; }

        [DarkColumn(Name = "descuento", IsMapped = true, IsKey = false)]
        public float Descuento { get; set; }

        [DarkColumn(Name = "subtotal", IsMapped = true, IsKey = false)]
        public float SubTotal { get; set; }

        [DarkColumn(Name = "subtotal_sin_descuento", IsMapped = true, IsKey = false)]
        public float SubTotalSinDesc { get; set; }

        [DarkColumn(Name = "iva", IsMapped = true, IsKey = false)]
        public float Iva { get; set; }

        [DarkColumn(Name = "total", IsMapped = true, IsKey = false)]
        public float Total { get; set; }

        [DarkColumn(Name = "code_configurable", IsMapped = true, IsKey = false)]
        public string CodigoConfigurable { get; set; }

        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [DarkColumn(Name = "fecha_registro", IsMapped = true, IsKey = false)]
        public DateTime FechaRegistro { get; set; }
    }
}
