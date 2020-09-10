using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "listar_detalle_pedido", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class ViewDetallePedido
    {
        [DarkColumn(Name = "pedidokey", IsMapped = true, IsKey = true)]
        public int IdPedido { get; set; }

        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = false)]
        public int IdCliente { get; set; }

        [DarkColumn(Name = "pedidoSubtotal", IsMapped = true, IsKey = false)]
        public float SubTotal { get; set; }

        [DarkColumn(Name = "pedidoIva", IsMapped = true, IsKey = false)]
        public float Iva { get; set; }

        [DarkColumn(Name = "pedidoTotal", IsMapped = true, IsKey = false)]
        public float Total { get; set; }

        [DarkColumn(Name = "pedidoSubtotalMXN", IsMapped = true, IsKey = false)]
        public float SubTotalMXN { get; set; }

        [DarkColumn(Name = "pedidoIvaMXN", IsMapped = true, IsKey = false)]
        public float IvaMXN { get; set; }

        [DarkColumn(Name = "pedidoTotalMXN", IsMapped = true, IsKey = false)]
        public float TotalMXN { get; set; }

        [DarkColumn(Name = "moneda_pago", IsMapped = true, IsKey = false)]
        public string MonedaPago { get; set; }

        [DarkColumn(Name = "tipoCambio", IsMapped = true, IsKey = false)]
        public float TipoCambio { get; set; }

        [DarkColumn(Name = "detallekey", IsMapped = true, IsKey = false)]
        public int DetalleKey { get; set; }

        [DarkColumn(Name = "id_cotizacion", IsMapped = true, IsKey = false)]
        public int IdCotizacion { get; set; }

        [DarkColumn(Name = "detalle_codigo", IsMapped = true, IsKey = false)]
        public string CodigoProducto { get; set; }

        [DarkColumn(Name = "cantidad", IsMapped = true, IsKey = false)]
        public int Cantidad { get; set; }

        [DarkColumn(Name = "descuento", IsMapped = true, IsKey = false)]
        public float Descuento { get; set; }

        [DarkColumn(Name = "detalleSubtotal", IsMapped = true, IsKey = false)]
        public float DetalleSubtotal { get; set; }

        [DarkColumn(Name = "detalleSubtotalSinDescuento", IsMapped = true, IsKey = false)]
        public float DetalleSubtotanSinDesc { get; set; }

        [DarkColumn(Name = "detallePrecioUnidad", IsMapped = true, IsKey = false)]
        public float DetallePrecioUnidad { get; set; }

        [DarkColumn(Name = "detalleIva", IsMapped = true, IsKey = false)]
        public float DetalleIva { get; set; }

        [DarkColumn(Name = "detalleTotal", IsMapped = true, IsKey = false)]
        public float DetalleTotal { get; set; }

        [DarkColumn(Name = "detalleSubtotalMXN", IsMapped = true, IsKey = false)]
        public float DetalleSubtotalMXN { get; set; }

        [DarkColumn(Name = "detalleSubtotalSinDescuentoMXN", IsMapped = true, IsKey = false)]
        public float DetalleSubtotanSinDescMXN { get; set; }

        [DarkColumn(Name = "detallePrecioUnidadMXN", IsMapped = true, IsKey = false)]
        public float DetallePrecioUnidadMXN { get; set; }

        [DarkColumn(Name = "detalleIvaMXN", IsMapped = true, IsKey = false)]
        public float DetalleIvaMXn { get; set; }

        [DarkColumn(Name = "detalleTotalMXN", IsMapped = true, IsKey = false)]
        public float DetalleTotalMXN { get; set; }

        [DarkColumn(Name = "detalle_code_configurable", IsMapped = true, IsKey = false)]
        public string CodigoConfigurable { get; set; }

        [DarkColumn(Name = "detalle_activo", IsMapped = true, IsKey = false)]
        public string DetalleActivo { get; set; }

        [DarkColumn(Name = "producto_codigo", IsMapped = true, IsKey = false)]
        public string CodigoProducto2 { get; set; }

        [DarkColumn(Name = "desc_producto", IsMapped = true, IsKey = false)]
        public string DescripcionProducto { get; set; }

        [DarkColumn(Name = "precio", IsMapped = true, IsKey = false)]
        public float Precio { get; set; }

        [DarkColumn(Name = "existencia", IsMapped = true, IsKey = false)]
        public float Existencia { get; set; }

        [DarkColumn(Name = "descuento_producto", IsMapped = true, IsKey = false)]
        public float DescuentoProducto { get; set; }

        [DarkColumn(Name = "producto_codigo_configurable", IsMapped = true, IsKey = false)]
        public string ProductoCodigoConfigurable { get; set; }

        [DarkColumn(Name = "producto_activo", IsMapped = true, IsKey = false)]
        public string ProductoActivo { get; set; }

        [DarkColumn(Name = "img_principal", IsMapped = true, IsKey = false)]
        public string ImagePrincipal { get; set; }

        [DarkColumn(Name = "costo_envio", IsMapped = true, IsKey = false)]
        public string CostoEnvio { get; set; }

        [DarkColumn(Name = "almacen", IsMapped = true, IsKey = false)]
        public string Almacen { get; set; }

        [DarkColumn(Name = "categoria", IsMapped = true, IsKey = false)]
        public string Categoria { get; set; }

        [DarkColumn(Name = "t17_f001", IsMapped = true, IsKey = false)]
        public string CodigoProdConfigurable { get; set; }

        [DarkColumn(Name = "t17_f003", IsMapped = true, IsKey = false)]
        public string NombreDinamicoConf { get; set; }

        [DarkColumn(Name = "t36_f097", IsMapped = true, IsKey = false)]
        public string TiempoEntregaIDConfig { get; set; }

        [DarkColumn(Name = "TiempoEntrega", IsMapped = true, IsKey = false)]
        public string TiempoEntrega { get; set; }
    }
}
