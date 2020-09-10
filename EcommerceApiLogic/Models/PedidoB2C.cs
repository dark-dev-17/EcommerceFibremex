using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "listar_pedido_b2c", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class PedidoB2C
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdPedido { get; set; }

        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = false)]
        public int IdCliente { get; set; }

        [DarkColumn(Name = "subtotal", IsMapped = true, IsKey = false)]
        public float SubTotal { get; set; }

        [DarkColumn(Name = "iva", IsMapped = true, IsKey = false)]
        public float Iva { get; set; }

        [DarkColumn(Name = "total", IsMapped = true, IsKey = false)]
        public float Total { get; set; }

        [DarkColumn(Name = "pedidoSubtotalMXN", IsMapped = true, IsKey = false)]
        public float SubTotalMXN { get; set; }

        [DarkColumn(Name = "pedidoIvaMXN", IsMapped = true, IsKey = false)]
        public float IvaMXN { get; set; }

        [DarkColumn(Name = "pedidoTotalMXN", IsMapped = true, IsKey = false)]
        public float TotalMXN { get; set; }

        [DarkColumn(Name = "fecha", IsMapped = true, IsKey = false)]
        public DateTime Fecha { get; set; }

        [DarkColumn(Name = "metodo_pago", IsMapped = true, IsKey = false)]
        public string MetodoPago { get; set; }

        [DarkColumn(Name = "moneda_pago", IsMapped = true, IsKey = false)]
        public string MonedaPago { get; set; }

        [DarkColumn(Name = "datos_envio", IsMapped = true, IsKey = false)]
        public string DatosEnvio { get; set; }

        [DarkColumn(Name = "datos_facturacion", IsMapped = true, IsKey = false)]
        public string DatosFacturacion { get; set; }

        [DarkColumn(Name = "numero_guia", IsMapped = true, IsKey = false)]
        public string NumeroGuia { get; set; }

        [DarkColumn(Name = "paqueteria", IsMapped = true, IsKey = false)]
        public string Paqueteria { get; set; }

        [DarkColumn(Name = "tipo_cambio", IsMapped = true, IsKey = false)]
        public float TipoCambio { get; set; }

        [DarkColumn(Name = "dias_extra_credito", IsMapped = true, IsKey = false)]
        public int DiasExtraCredito { get; set; }

        [DarkColumn(Name = "CFDI_user", IsMapped = true, IsKey = false)]
        public string UsoCFDI { get; set; }

        [DarkColumn(Name = "estatus", IsMapped = true, IsKey = false)]
        public string Estatus { get; set; }

        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [DarkColumn(Name = "estatus_puntos", IsMapped = true, IsKey = false)]
        public int EstatusPuntos { get; set; }

        [DarkColumn(Name = "tipo_pedido", IsMapped = true, IsKey = false)]
        public string TipoPedido { get; set; }

        [DarkColumn(Name = "numero_guia_estatus", IsMapped = true, IsKey = false)]
        public string NumeroGuiaEstatus { get; set; }

        [DarkColumn(Name = "nombre_paqueteria", IsMapped = true, IsKey = false)]
        public string NombrePaqueteria { get; set; }

        [DarkColumn(Name = "t05_f004", IsMapped = true, IsKey = false)]
        public string FacturaSAP { get; set; }

        [DarkColumn(Name = "t05_f005", IsMapped = true, IsKey = false)]
        public string PagoSAP { get; set; }

        [DarkColumn(Name = "t05_f006", IsMapped = true, IsKey = false)]
        public string CotizacionSAP { get; set; }

        [DarkColumn(Name = "t05_f007", IsMapped = true, IsKey = false)]
        public string RequiereFactura { get; set; }

        [DarkColumn(Name = "t05_f008", IsMapped = true, IsKey = false)]
        public int EstatusSAP { get; set; }

        [DarkColumn(Name = "t05_f009", IsMapped = true, IsKey = false)]
        public string IdTransactionOPP { get; set; }

        [DarkColumn(Name = "t05_f010", IsMapped = true, IsKey = false)]
        public string ComentariosPaqueteria { get; set; }

        [DarkColumn(Name = "t05_f011", IsMapped = true, IsKey = false)]
        public int t05_f011 { get; set; }

        [DarkColumn(Name = "t05_f013", IsMapped = true, IsKey = false)]
        public int t05_f013 { get; set; }

        [DarkColumn(Name = "t12_pk01", IsMapped = true, IsKey = false)]
        public int IdWebHook { get; set; }

        [DarkColumn(Name = "t12_f001", IsMapped = true, IsKey = false)]
        public string WHTitulo { get; set; }

        [DarkColumn(Name = "t12_f002", IsMapped = true, IsKey = false)]
        public int WHNumeroPedido { get; set; }

        [DarkColumn(Name = "t12_f004", IsMapped = true, IsKey = false)]
        public string WHEstatus { get; set; }

        [DarkColumn(Name = "t12_f005", IsMapped = true, IsKey = false)]
        public string WHResponseData { get; set; }

        [DarkColumn(Name = "detalle", IsMapped = false, IsKey = false)]
        public List<ViewDetallePedido> Detalle { get; set; }
    }
}
