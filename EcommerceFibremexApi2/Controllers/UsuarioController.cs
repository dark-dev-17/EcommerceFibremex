using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Herramientas;
using EcommerceFibremexApi2.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceFibremexApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        private readonly IConfiguration configuration;

        public UsuarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ecommerce);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.Usuario);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.Cliente);
        }

        /// <summary>
        /// Iniciar sesión para aceder a los diferentes metodos
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        /// <response code="200">Credenciales correctas</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Credenciales incorrectas</response>
        [HttpPost]
        [EnableCors("AllowAllHeaders")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        //[ApiExplorerSettings(GroupName = "v1")]
        public ActionResult Login([FromBody] Login Login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("usuario o contraseña vacias");
                }

                var Result = darkDev.Usuario.GetByColumn(
                    Login.User.Trim(), darkDev.Usuario.ColumName(nameof(darkDev.Usuario.Element.Email))
                );

                if (Result == null)
                {
                    return Unauthorized();
                }
                string pass_real = "";
                using (EncrypData encrypData = new EncrypData("password"))
                {
                    pass_real = encrypData.Decrypt(Result.Password);
                }
                if (pass_real.Trim() == Login.Password.Trim())
                {
                    var Cliente_re = darkDev.Cliente.GetByColumn(Result.IdCliente + "", darkDev.Cliente.ColumName(nameof(darkDev.Cliente.Element.IdCliente)));
                    var response = new
                    {
                        token = darkDev.tokenValidationAction.GenerateToken(Result),
                        Expiration = DateTime.Now.AddMinutes(Int32.Parse(configuration["Jwt:TokenExpirationInMinutes"])),
                        TipoCliente = Cliente_re.TipoCliente,
                        NombreCliente = Cliente_re.Nombre,
                    };
                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest("Error usuario");
            }
            finally
            {
                darkDev.CloseConnection();
            }
        }
    }
}
