using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceFibremexApi.Models
{
    public class NuevoArticulo
    {
        public int IdPedido { get; set; }
        public string CodigoProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
