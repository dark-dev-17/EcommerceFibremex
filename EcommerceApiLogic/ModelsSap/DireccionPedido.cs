using DbManagerDark.Attributes;
using DbManagerDark.DbManager;
using EcommerceApiLogic.Catalogos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EcommerceApiLogic.ModelsSap
{
    [DarkTable(Name = "menu_categorias", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class DireccionPedido
    {
        [DarkColumn(Name = "Address", IsMapped = true, IsKey = true)]
        public string Nombre { get; set; }

        [DarkColumn(Name = "Street", IsMapped = true, IsKey = false)]
        public string Calle { get; set; }

        [DarkColumn(Name = "StreetNo", IsMapped = true, IsKey = false)]
        public string CalleNo { get; set; }

        [DarkColumn(Name = "Block", IsMapped = true, IsKey = false)]
        public string Colonia { get; set; }

        [DarkColumn(Name = "County", IsMapped = true, IsKey = false)]
        public string Municipio { get; set; }

        [DarkColumn(Name = "ZipCode", IsMapped = true, IsKey = false)]
        public string CP { get; set; }

        [DarkColumn(Name = "State", IsMapped = true, IsKey = false)]
        public string Estado { get; set; }

        [DarkColumn(Name = "LicTradNum", IsMapped = true, IsKey = false)]
        public string RFC { get; set; }

        [DarkColumn(Name = "City", IsMapped = true, IsKey = false)]
        public string Ciudad { get; set; }

        [DarkColumn(Name = "CardName", IsMapped = true, IsKey = false)]
        public string NombreCliente { get; set; }

        [DarkColumn(Name = "DefaultAddress", IsMapped = true, IsKey = false)]
        public string Default { get; set; }

        [DarkColumn(Name = "Name", IsMapped = true, IsKey = false)]
        public string ContactoNombre { get; set; }

        [DarkColumn(Name = "Tel1", IsMapped = true, IsKey = false)]
        public string ContactoTelefono { get; set; }

        [DarkColumn(Name = "E_MailL", IsMapped = true, IsKey = false)]
        public string ContactoCorreo { get; set; }

        [DarkColumn(Name = "E_MailL", IsMapped = false, IsKey = false)]
        public bool IsDefault { get { return Default.Trim() == "default" ? true : false; } }

        [DarkColumn(Name = "E_MailL", IsMapped = false, IsKey = false)]
        public string EstadoName { get { return new Pais().Estados.Find(a => a.Value == Estado) == null ? "--" : new Pais().Estados.Find(a => a.Value == Estado).Label; } }
    }
}
