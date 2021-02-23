using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "login_cliente", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
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

        [DarkColumn(Name = "password", IsMapped = true, IsKey = false)]
        public string Password { get; set; }

        [DarkColumn(Name = "fecha_registro", IsMapped = true, IsKey = false)]
        public DateTime Registro { get; set; }

        [DarkColumn(Name = "last_login", IsMapped = true, IsKey = false)]
        public DateTime UltimoLogin { get; set; }

        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [DarkColumn(Name = "tipo_cliente", IsMapped = true, IsKey = false)]
        public string TipoCliente { get; set; }

        [DarkColumn(Name = "cardcode", IsMapped = true, IsKey = false)]
        public string CodigoCliente { get; set; }

        [DarkColumn(Name = "sociedad", IsMapped = true, IsKey = false)]
        public string Sociedad { get; set; }

        [DarkColumn(Name = "pass_b2b", IsMapped = true, IsKey = false)]
        public string Pass_b2b { get; set; }

        [DarkColumn(Name = "ingreso", IsMapped = true, IsKey = false)]
        public int Ingreso { get; set; }

        [DarkColumn(Name = "groupcode", IsMapped = true, IsKey = false)]
        public int Groupcode { get; set; }

        [DarkColumn(Name = "segmento", IsMapped = true, IsKey = false)]
        public string Segmento { get; set; }

        [DarkColumn(Name = "update_pass", IsMapped = true, IsKey = false)]
        public int Update_pass { get; set; }

        [DarkColumn(Name = "email_ejecutivo", IsMapped = true, IsKey = false)]
        public string Email_ejecutivo { get; set; }

        [DarkColumn(Name = "descuento", IsMapped = true, IsKey = false)]
        public int Descuento { get; set; }

        [DarkColumn(Name = "dias_credito", IsMapped = true, IsKey = false)]
        public int Dias_credito { get; set; }
    }
    
}
