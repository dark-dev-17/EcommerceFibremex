using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "t05_factura_b2c", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class SeguimientB2C
    {
        [DarkColumn(Name = "t05_pk01", IsMapped = true, IsKey = true)]
        public int IdSeguimientoB2C { get; set; }

        [DarkColumn(Name = "t05_f009", IsMapped = true, IsKey = false)]
        public string OpenPayReferencia { get; set; }

        [DarkColumn(Name = "t05_f004", IsMapped = true, IsKey = false)]
        public string FacturaSAP { get; set; }

        [DarkColumn(Name = "t05_f005", IsMapped = true, IsKey = false)]
        public string PagoSAP { get; set; }

        [DarkColumn(Name = "t06_f006", IsMapped = true, IsKey = false)]
        public string IdPedido { get; set; }

        [DarkColumn(Name = "t05_f007", IsMapped = true, IsKey = false)]
        public string RequiereFactura { get; set; }

        [DarkColumn(Name = "t05_f008", IsMapped = true, IsKey = false)]
        public string Estatus { get; set; }

        [DarkColumn(Name = "t05_f010", IsMapped = true, IsKey = false)]
        public string ReferenciaFactura { get; set; }

        [DarkColumn(Name = "t05_f011", IsMapped = true, IsKey = false)]
        public int Intentos { get; set; }

        [DarkColumn(Name = "t05_f098", IsMapped = true, IsKey = false)]
        public DateTime Registro { get; set; }

        [DarkColumn(Name = "t05_f099", IsMapped = true, IsKey = false)]
        public DateTime Actualizacion { get; set; }

    }
}
