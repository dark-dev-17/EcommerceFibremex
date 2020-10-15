using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ModelsSap;
using EcommerceApiLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceFibremexApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DireccionFacturacionController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public DireccionFacturacionController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();
            darkDev.LoadObject(MysqlObject.DireccionFacturacion);
        }

        // GET: api/<DireccionFacturacionController>
        /// <summary>
        /// Listado de direcciones del cliente B2C en session(token)
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Produces(typeof(DireccionFacturacion))]
        [Authorize]
        //[Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<DireccionFacturacion>> GetB2C()
        {
            try
            {
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                return Ok(darkDev.DireccionFacturacion.Get("" + idCliente, darkDev.DireccionFacturacion.ColumName(nameof(darkDev.DireccionFacturacion.Element.IdCliente))));
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            finally
            {
                darkDev.CloseConnection();
            }
        }

        // GET api/<DireccionFacturacionController>/5
        /// <summary>
        /// Detalle de dirección del cliente B2C en session(token)
        /// </summary>
        /// <param name="id">id de la direccion</param>
        /// <returns></returns>
        /// <response code="200">dirección encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<DireccionFacturacion> GetB2C(int id)
        {

            try
            {
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_re = darkDev.DireccionFacturacion.Get(id + "");
                if (Direccion_re.IdCliente == idCliente)
                {
                    return Ok(Direccion_re);
                }
                else
                {
                    return BadRequest("Dirección no encontrada");
                }
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            finally
            {
                darkDev.CloseConnection();
            }
        }

        // POST api/<DireccionFacturacionController>
        /// <summary>
        /// Listado de direcciones del cliente B2B en session(token)
        /// </summary>
        /// <param name="DireccionFacturacion"></param>
        /// <returns></returns>
        /// <response code="200">Dirección encontrada</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult CreateB2C([FromBody] DireccionFacturacion DireccionFacturacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }
                darkDev.DireccionFacturacion.Element = DireccionFacturacion;
                darkDev.DireccionFacturacion.Element.IdCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                if (darkDev.DireccionFacturacion.Add())
                {
                    return Ok(new { error = false, message = "Dirección agregada", data = DireccionFacturacion });
                }
                else
                {
                    return BadRequest("Dirección no guardada");
                }
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            finally
            {
                darkDev.CloseConnection();
            }
        }

        /// <summary>
        /// Detalle de dirección del cliente B2B en session(token)
        /// </summary>
        /// <param name="DireccionFacturacion"></param>
        /// <returns></returns>
        /// <response code="200">Dirección encontrada</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult EditB2B([FromBody] DireccionFacturacion DireccionFacturacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }
                var Direccion_re = darkDev.DireccionFacturacion.Get(DireccionFacturacion.IdDireccionFacturacion);
                darkDev.DireccionFacturacion.Element.IdCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                if (Direccion_re == null)
                {
                    throw new DarkExceptionUser("La dirección requerida no fue encontrada");
                }

                if (Direccion_re.IdCliente != idCliente)
                {
                    throw new DarkExceptionUser("No puedes actualizar esta direccion");
                }

                darkDev.DireccionFacturacion.Element = DireccionFacturacion;
                if (darkDev.DireccionFacturacion.Update())
                {
                    return Ok(new { error = false, message = "Dirección actualizada", data = DireccionFacturacion });
                }
                else
                {
                    return BadRequest("Dirección no actualizada");
                }
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
            finally
            {
                darkDev.CloseConnection();
            }
        }

    }
}
