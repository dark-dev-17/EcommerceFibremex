using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Resquest
{
    public class TokenInformation
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string TipoCliente { get; set; }
        public string NombreCliente { get; set; }
        public List<ExtrasValidation> Extras { get; set; }
    }
}
