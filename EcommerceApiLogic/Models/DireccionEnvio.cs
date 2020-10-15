using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "datos_envio", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class DireccionEnvio
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdDireccionEnvio { get; set; }

        [Required]
        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = false)]
        public int IdCliente { get; set; }

        [Required]
        [DarkColumn(Name = "nombre", IsMapped = true, IsKey = false)]
        public string Nombre { get; set; }

        [Required]
        [DarkColumn(Name = "apellido", IsMapped = true, IsKey = false)]
        public string Apellido { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe de ser un número")]
        [Required]
        [DarkColumn(Name = "celular", IsMapped = true, IsKey = false)]
        public string Celular { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe de ser un número")]
        [Required]
        [DarkColumn(Name = "telefono", IsMapped = true, IsKey = false)]
        public string Telefono { get; set; }

        [Required]
        [DarkColumn(Name = "calle", IsMapped = true, IsKey = false)]
        public string Calle { get; set; }

        //[MaxLength(5)]
        //[MinLength(1)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Debe de ser un número")]
        [DarkColumn(Name = "n_ext", IsMapped = true, IsKey = false)]
        public string NoExterior { get; set; }

        //[MaxLength(5)]
        //[MinLength(1)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Debe de ser un número")]
        [DarkColumn(Name = "n_int", IsMapped = true, IsKey = false)]
        public string NoInterior { get; set; }

        [MaxLength(5)]
        [MinLength(5)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe de ser un número")]
        [Required]
        [DarkColumn(Name = "cp", IsMapped = true, IsKey = false)]
        public string CP { get; set; }

        [Required]
        [DarkColumn(Name = "estado", IsMapped = true, IsKey = false)]
        public string Estado { get; set; }

        [Required]
        [DarkColumn(Name = "ciudad", IsMapped = true, IsKey = false)]
        public string Ciudad { get; set; }

        [Required]
        [DarkColumn(Name = "delegacion", IsMapped = true, IsKey = false)]
        public string Delegacion { get; set; }

        [Required]
        [DarkColumn(Name = "colonia", IsMapped = true, IsKey = false)]
        public string Colonia { get; set; }

        [Required]
        [DarkColumn(Name = "referencia", IsMapped = true, IsKey = false)]
        public string Referencia { get; set; }

        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

    }
}
