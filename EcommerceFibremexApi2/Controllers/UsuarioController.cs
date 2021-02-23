using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Herramientas;
using EcommerceApiLogic.Responses;
using EcommerceApiLogic.Resquest;
using EcommerceApiLogic.Rules;
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
        private ClienteCtrl ClienteCtrl;

        public UsuarioController(IConfiguration configuration)
        {
            ClienteCtrl = new ClienteCtrl(configuration);
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
        public ActionResult<SplittelRespData<TokenInformation>> Login([FromBody] Login Login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("usuario o contraseña vacias");
                }

                var data = ClienteCtrl.ValidateLogin(Login);
                if (data.Code == 0)
                {
                    return Ok(data);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (DarkException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                ClienteCtrl.Finish();
            }
        }

        /// <summary>
        /// register new usuario B2C
        /// </summary>
        /// <param name="registerB2C"></param>
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
        public ActionResult<SplittelRespData<TokenInformation>> RegisterNewB2C([FromBody] RegisterB2C registerB2C)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }

                var data = ClienteCtrl.RegisterNewB2C(registerB2C);
                if (data.Code == 0)
                {
                    return Ok(data);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (DarkException ex)
            {
                return BadRequest(new SplittelRespData<TokenInformation> { 
                    Code = ex.Code,
                        Message = ex.Message,
                        Data = new TokenInformation { 
                            Extras = ClienteCtrl.Extras
                        }
                });
            }
            finally
            {
                ClienteCtrl.Finish();
            }
        }
    }
}
