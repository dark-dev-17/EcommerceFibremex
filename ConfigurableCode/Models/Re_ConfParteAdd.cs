using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurableCode.Models
{
    public class Re_ConfParteAdd
    {
        public string NameFile { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }

    public class Re_ConfParteUpd
    {
        public string NameFile { get; set; }
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }
}
