using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Resquest;
using EcommerceApiLogic.Rules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EcommerceFibremexApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrecioConfController : ControllerBase
    {

        private PreciosConfCtrl PreciosConfCtrl;
        public PrecioConfController(IConfiguration configuration)
        {
            PreciosConfCtrl = new PreciosConfCtrl(configuration);
        }
        /// <summary>
        /// Calcular precio pigtail
        /// </summary>
        /// <param name="pigtailPrecioCal"></param>
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
        public ActionResult PigtailCal([FromBody] PigtailPrecioCal pigtailPrecioCal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }

                PreciosConfCtrl.IdCliente = PreciosConfCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var result = PreciosConfCtrl.PigtailCal(pigtailPrecioCal);
                //dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(result, new ExpandoObjectConverter());
                return Ok(new { error = false, message = "Dirección agregada", data = result });
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                PreciosConfCtrl.Finish();
            }
        }
    }
}
