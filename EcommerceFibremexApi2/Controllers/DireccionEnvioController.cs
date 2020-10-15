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
    public class DireccionEnvioController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public DireccionEnvioController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();
            darkDev.LoadObject(MysqlObject.DireccionEnvio);
        }

        // GET: api/<DireccionEnvioController>
        /// <summary>
        /// Listado de direcciones del cliente B2C en sessioón(token)
        /// </summary>
        /// <returns></returns>
        /// <response code="200">dirección encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<DireccionEnvio>> GetB2C()
        {
            try
            {
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                return Ok(darkDev.DireccionEnvio.Get("" + idCliente, darkDev.DireccionEnvio.ColumName(nameof(darkDev.DireccionEnvio.Element.IdCliente))));
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

        // GET api/<DireccionEnvioController>/5
        /// <summary>
        /// Ver direccion del cliente B2C en sessioón(token) 
        /// </summary>
        /// <param name="id">id de la dirección a consultar</param>
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
        public ActionResult<DireccionEnvio> GetB2C(int id)
        {

            try
            {
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_re = darkDev.DireccionEnvio.Get(id + "");
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

        // POST api/<DireccionEnvioController>
        /// <summary>
        /// Crear direccion del cliente B2C en sessioón(token)
        /// </summary>
        /// <param name="DireccionEnvio"></param>
        /// <returns></returns>
        /// <response code="200">dirección creada</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult CreateB2C([FromBody] DireccionEnvio DireccionEnvio)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }

                darkDev.DireccionEnvio.Element = DireccionEnvio;
                darkDev.DireccionEnvio.Element.IdCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                if (darkDev.DireccionEnvio.Add())
                {
                    return Ok(new { error = false, message = "Dirección agregada", data = DireccionEnvio });
                }
                else
                {
                    return BadRequest("Dirección no agregada");
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

        // POST api/<DireccionEnvioController>
        /// <summary>
        /// Editar dirección del cliente B2C en sessión(token)
        /// </summary>
        /// <param name="DireccionEnvio"></param>
        /// <returns></returns>
        /// <response code="200">dirección actualizada</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult EditB2C([FromBody] DireccionEnvio DireccionEnvio)
        {
            try
            {
                var Direccion_re = darkDev.DireccionEnvio.Get(DireccionEnvio.IdDireccionEnvio);
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                if (Direccion_re == null)
                {
                    throw new DarkExceptionUser("La dirección requerida no fue encontrada");
                }

                if (Direccion_re.IdCliente != idCliente)
                {
                    throw new DarkExceptionUser("No puedes actualizar esta direccion");
                }

                darkDev.DireccionEnvio.Element = DireccionEnvio;
                darkDev.DireccionEnvio.Element.IdCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                if (darkDev.DireccionEnvio.Update())
                {
                    return Ok(new { error = false, message = "Dirección actualizada", data = DireccionEnvio });
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