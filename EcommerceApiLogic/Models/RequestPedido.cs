using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    public class RequestPedido
    {
        public string TokenId { get; set; }
        public string SessionIdDevice { get; set; }
        public int IdPedido { get; set; }
        public string MonedaPago { get; set; }
        public string DatosEnvio { get; set; }
        public string DatosFacturacion { get; set; }
        public string ReferenciasPaqueteria { get; set; }
        public RequestB2B B2B { get; set; }
        public RequestB2C B2C { get; set; }
        public string UsoCFDI { get;  set; }
        public string ReferenciaDoc { get; set; }
    }

    public class RequestB2B
    {
        
        public string ContactoNombre { get; set; }
        public string ContactoTelefono { get; set; }
        public string ContactoCorreo { get; set; }
    }
    public class RequestB2C
    {
        public bool RequiereFactura { get; set; }
    }
}
