using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ModelsSap;
using EcommerceApiLogic.Rules;
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
        private DireccionesCtrl DireccionesCtrl;

        public DireccionEnvioController(IConfiguration configuration)
        {
            DireccionesCtrl = new DireccionesCtrl(configuration);
        }

        #region Direcciones de envio para el tipo de cliente B2B
        /// <summary>
        /// Obtiene direccion de envio seleccionada
        /// </summary>
        /// <param name="NombreDireccion">Direccion envio a mostrar</param>
        /// <returns></returns>
        /// <response code="200">Ultima dirección agregada</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{NombreDireccion}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<DireccionPedido> GetB2B(string NombreDireccion)
        {
            try
            {
                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_Re = DireccionesCtrl.GetB2B_envio(NombreDireccion);
                if (Direccion_Re is null)
                    return BadRequest("No tiene una dirección");
                else
                    return Ok(Direccion_Re);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DireccionesCtrl.Finish();
            }
        }
        /// <summary>
        /// Obtiene listado de direcciones de envio del cliente en sesion (B2B)
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ultima dirección agregada</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<DireccionPedido>> GetB2B()
        {
            try
            {
                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_Re = DireccionesCtrl.GetB2B_dirEnv();
                if (Direccion_Re is null)
                    return BadRequest("No tiene una dirección");
                else
                    return Ok(Direccion_Re);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DireccionesCtrl.Finish();
            }
        }
        /// <summary>
        /// Obtiene direccion de envio por default
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ultima dirección agregada</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</respons
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<DireccionPedido> GetDefaultB2B()
        {
            try
            {
                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_Re = DireccionesCtrl.GetB2B_envioDef();
                if (Direccion_Re is null)
                    return BadRequest("No tiene una dirección");
                else
                    return Ok(Direccion_Re);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DireccionesCtrl.Finish();
            }
        }

        #endregion

        #region direcciones de envio para tipo de cliente B2C
        /// <summary>
        /// Ultima dirección agregada
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ultima dirección agregada</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<DireccionEnvio> GetDefaultB2C()
        {
            try
            {

                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_Re = DireccionesCtrl.GetB2C_envioDef();
                if (Direccion_Re is null)
                    return BadRequest("No tiene una dirección");
                else
                    return Ok(Direccion_Re);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DireccionesCtrl.Finish();
            }
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
                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_Re = DireccionesCtrl.GetB2C_dirEnv();
                return Ok(Direccion_Re);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DireccionesCtrl.Finish();
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
                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_re = DireccionesCtrl.GetB2C_envio(id);
                if (Direccion_re != null)
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
                return BadRequest( ex.Message);
            }
            finally
            {
                DireccionesCtrl.Finish();
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

                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                DireccionesCtrl.AddB2C_envio(DireccionEnvio);

                return Ok(new { error = false, message = "Dirección agregada", data = DireccionEnvio });
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DireccionesCtrl.Finish();
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

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }

                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                DireccionesCtrl.UpdB2C_envio(DireccionEnvio);

                return Ok(new { error = false, message = "Dirección actualizada", data = DireccionEnvio });
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DireccionesCtrl.Finish();
            }
        }
        #endregion

    }
}