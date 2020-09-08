using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "cotizacion_encabezado", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Pedido
    {
        [DarkColumn(Name = "id",IsMapped = true, IsKey = true)]
        public int IdPedido { get; set; }

        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = false)]
        public int IdCliente { get; set; }

        [DarkColumn(Name = "subtotal", IsMapped = true, IsKey = false)]
        public float SubTotal { get; set; }

        [DarkColumn(Name = "iva", IsMapped = true, IsKey = false)]
        public float Iva { get; set; }

        [DarkColumn(Name = "total", IsMapped = true, IsKey = false)]
        public float Total { get; set; }

        [DarkColumn(Name = "fecha", IsMapped = true, IsKey = false)]
        public DateTime Fecha { get; set; }

        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [DarkColumn(Name = "estatus", IsMapped = true, IsKey = false)]
        public string Estatus { get; set; }

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

        [DarkColumn(Name = "tipoCambio", IsMapped = true, IsKey = false)]
        public float TipoCambio { get; set; }

        [DarkColumn(Name = "DiasExtraCredito", IsMapped = true, IsKey = false)]
        public int DiasCreditoExtra { get; set; }

        [DarkColumn(Name = "CFDIUser", IsMapped = true, IsKey = false)]
        public string UsoCFDI { get; set; }

        [DarkColumn(Name = "aprobada", IsMapped = true, IsKey = false)]
        public int Aprobada { get; set; }

        [DarkColumn(Name = "numero_guia_estatus", IsMapped = true, IsKey = false)]
        public string NumGuiaEstatus { get; set; }

        [DarkColumn(Name = "nombre_paqueteria", IsMapped = true, IsKey = false)]
        public string PaqueteriaDescripcion { get; set; }

        [DarkColumn(Name = "fecha_recibio_paquete", IsMapped = true, IsKey = false)]
        public string FechaReciboPaquete { get; set; }

        [DarkColumn(Name = "recibio", IsMapped = true, IsKey = false)]
        public string NombreRecibio { get; set; }

        [DarkColumn(Name = "ip", IsMapped = true, IsKey = false)]
        public string IpSource { get; set; }

        [DarkColumn(Name = "tipo_pedido", IsMapped = true, IsKey = false)]
        public string TipoPedido { get; set; }

        [DarkColumn(Name = "estatus_puntos", IsMapped = true, IsKey = false)]
        public int EstatusPuntos { get; set; }

        [DarkColumn(Name = "estatus_puntos", IsMapped = false, IsKey = false)]
        public List<DetallePedido> DetallePedidos { get; set; }



    }
}
