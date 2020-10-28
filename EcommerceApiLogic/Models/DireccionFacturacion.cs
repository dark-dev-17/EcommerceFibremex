using DbManagerDark.Attributes;
using EcommerceApiLogic.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "datos_facturacion", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class DireccionFacturacion
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdDireccionFacturacion { get; set; }

        [Required]
        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = false)]
        public int IdCliente { get; set; }

        [Required]
        [DarkColumn(Name = "razon_social", IsMapped = true, IsKey = false)]
        public string RazonSocial { get; set; }

        [Required]
        [DarkColumn(Name = "tipo_facturacion", IsMapped = true, IsKey = false)]
        public string TipoFacturacion { get; set; }

        [Required]
        [DarkColumn(Name = "rfc", IsMapped = true, IsKey = false)]
        public string RFC { get; set; }

        [Required]
        [DarkColumn(Name = "calle", IsMapped = true, IsKey = false)]
        public string Calle { get; set; }

        [Required]
        [DarkColumn(Name = "n_ext", IsMapped = true, IsKey = false)]
        public string NoExterior { get; set; }

        [Required]
        [DarkColumn(Name = "n_int", IsMapped = true, IsKey = false)]
        public string NoInterior { get; set; }

        [Required]
        [MaxLength(5)]
        [MinLength(5)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe de ser un número")]
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

        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [DarkColumn(Name = "activo", IsMapped = false, IsKey = false)]
        public string EstadoName { get { return new Pais().Estados.Find(a => a.Value == Estado) == null ? "--" : new Pais().Estados.Find(a => a.Value == Estado).Label; } }
    }
}
