using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurableCode.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public List<string> Detonantes { get; set; }
        public string AplicaElemento { get; set; }

    }
}
