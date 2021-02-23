using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceApiLogic.Resquest
{
    /// <summary>
    /// solicitud de agregar o editar direccion de envio o facturacion de un cliente B2B
    /// </summary>
    public class DireccionEnvFac
    {
        /// <summary>
        /// Nombre de la direccion
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public string NombreDireccion { get; set; }
        /// <summary>
        /// Número exterior
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public string NumeroExterior { get; set; }
        /// <summary>
        /// Nombre de la calle
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Calle { get; set; }
        /// <summary>
        /// Colonia
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Colonia { get; set; }
        /// <summary>
        /// Municipio
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Municipio { get; set; }
        /// <summary>
        /// Codigo postal
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public string CP { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Estado { get; set; }
        /// <summary>
        /// Ciudad
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Ciudad { get; set; }
        /// <summary>
        /// Direccion default 
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public bool Default { get; set; }
        /// <summary>
        /// Tipo direccion, S = Envio, B = Facturacion
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        public string TipoDireccion { get; set; }
    }
}
