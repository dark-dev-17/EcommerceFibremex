using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceApiLogic.Resquest
{
    public class Login
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
