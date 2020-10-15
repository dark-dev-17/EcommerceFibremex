using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurableCode.Models
{
    public class Elemento
    {
        public int IdElemento { get; set; }
        public string IdElemClave { get { return "elem_" + IdElemento; } }
        public string Descripcion { get; set; }
        public TipoElemento TipoElemento { get; set; }
        public List<Valores> Valores { get; set; }
        public string ValorFijo { get; set; }
        public bool Requerido { get; set; }
        public RangoMask RangoMask { get; set; }
    }
    public class RangoMask
    {
        public string Unidad { get; set; }
        public string Mascara { get; set; }
        public int EquivalenciaUnidad { get; set; }
    }
}
