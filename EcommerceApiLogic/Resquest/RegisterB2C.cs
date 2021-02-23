using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceApiLogic.Resquest
{
    public class RegisterB2C
    {
        /// <summary>
        /// Nombre del cliente
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        //[MinLength(5)]
        public string Nombre { get; set; }
        /// <summary>
        /// Apellidos del cliente
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        //[MinLength(5)]
        public string Apellidos { get; set; }
        /// <summary>
        /// telefono del cliente
        /// </summary>
        [MaxLength(10)]
        [MinLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefono invalido")]
        public string Telefono { get; set; }

        /// <summary>
        /// Correo del cliente
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo invalido")]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Contraseña del cliente
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(5)]
        public string Password { get; set; }
        /// <summary>
        /// Confirmacion de contraseña
        /// </summary>
        [Required(ErrorMessage = "Este campo es requerido")]
        //[MinLength(5)]
        public string PasswordConfirm { get; set; }

        
    }

    public class ExtrasValidation
    {
        public string Color { get; set; }
        public string Descripcion { get; set; }
    }
}
