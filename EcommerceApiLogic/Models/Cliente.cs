using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "admin_clientes", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class Cliente
    {
        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = true)]
        public int IdCliente { get; set; }
        [DarkColumn(Name = "nombre", IsMapped = true, IsKey = false)]
        public string Nombre { get; set; }
        [DarkColumn(Name = "apellidos", IsMapped = true, IsKey = false)]
        public string Apellidos { get; set; }
        [DarkColumn(Name = "telefono", IsMapped = true, IsKey = false)]
        public string Telefono { get; set; }
        [DarkColumn(Name = "email", IsMapped = true, IsKey = false)]
        public string Email { get; set; }
        [DarkColumn(Name = "fecha_registro", IsMapped = true, IsKey = false)]
        public DateTime Registro { get; set; }
        [DarkColumn(Name = "last_login", IsMapped = true, IsKey = false)]
        public DateTime UltimoLogin { get; set; }
        [DarkColumn(Name = "tipo_cliente", IsMapped = true, IsKey = false)]
        public string TipoCliente { get; set; }
        [DarkColumn(Name = "cardcode", IsMapped = true, IsKey = false)]
        public string CodigoCliente { get; set; }
        [DarkColumn(Name = "sociedad", IsMapped = true, IsKey = false)]
        public string Sociedad { get; set; }
    }
}
