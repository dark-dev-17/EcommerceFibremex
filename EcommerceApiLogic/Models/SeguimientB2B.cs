using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "t06_b2b", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class SeguimientB2B
    {
        [DarkColumn(Name = "t06_pk01", IsMapped = true, IsKey = true)]
        public int IdSeguimientoB2B { get; set; }

        [DarkColumn(Name = "t06_f001", IsMapped = true, IsKey = false)]
        public string OfertaVenta { get; set; }

        [DarkColumn(Name = "t06_f002", IsMapped = true, IsKey = false)]
        public string OrdenVentaBorrador { get; set; }

        [DarkColumn(Name = "t06_f003", IsMapped = true, IsKey = false)]
        public string OrdenVenta { get; set; }

        [DarkColumn(Name = "t06_f004", IsMapped = true, IsKey = false)]
        public string ListaPicking { get; set; }

        [DarkColumn(Name = "t06_f009", IsMapped = true, IsKey = false)]
        public string Pago { get; set; }

        [DarkColumn(Name = "t06_f005", IsMapped = true, IsKey = false)]
        public string IdPedido { get; set; }

        [DarkColumn(Name = "t06_f006", IsMapped = true, IsKey = false)]
        public int EstatusPedido { get; set; }

        [DarkColumn(Name = "t06_f007", IsMapped = true, IsKey = false)]
        public string ReferenciaFactura { get; set; }

        [DarkColumn(Name = "t06_f008", IsMapped = true, IsKey = false)]
        public string OpenPayReferencia { get; set; }

        [DarkColumn(Name = "t06_f010", IsMapped = true, IsKey = false)]
        public int Intentos { get; set; }

        [DarkColumn(Name = "t06_f011", IsMapped = true, IsKey = false)]
        public string ContactoNombre { get; set; }

        [DarkColumn(Name = "t06_f012", IsMapped = true, IsKey = false)]
        public string ContactoTelefono { get; set; }

        [DarkColumn(Name = "t06_f013", IsMapped = true, IsKey = false)]
        public string ContactoCorreo { get; set; }

        [DarkColumn(Name = "t06_f098", IsMapped = true, IsKey = false)]
        public DateTime Registro { get; set; }

        [DarkColumn(Name = "t06_f099", IsMapped = true, IsKey = false)]
        public DateTime Actualizacion { get; set; }
    }
}
