using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.Responses;
using EcommerceApiLogic.Resquest;
using EcommerceApiLogic.Rules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EcommerceFibremexApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigurableController : ControllerBase
    {
        private ConfigurableCtrl ConfigurableCtrl;

        public ConfigurableController(IConfiguration configuration)
        {
            ConfigurableCtrl = new ConfigurableCtrl(configuration);
        }

        /// <summary>
        /// agregar nombre y descripcion a configurable seleccionado
        /// </summary>
        /// <param name="ConfNombre"></param>
        /// <returns></returns>
        /// <response code="200">Proceso completo</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<SplittelRespData<Configu_nombre>> CrearNombre([FromBody] ConfNombre ConfNombre)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }

                ConfigurableCtrl.IdCliente = ConfigurableCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Data = ConfigurableCtrl.Crear(ConfNombre);

                return Ok(Data);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest(new SplittelRespData<Configu_nombre>
                {
                    Code = 2000,
                    Message = ex.Message,
                });
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(new SplittelRespData<Configu_nombre>
                {
                    Code = 3000,
                    Message = ex.Message,
                });
            }
            finally
            {
                ConfigurableCtrl.Finish();
            }
        }
    }
}
