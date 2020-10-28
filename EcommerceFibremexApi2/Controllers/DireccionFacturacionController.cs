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
    public class DireccionFacturacionController : ControllerBase
    {
        private DireccionesCtrl DireccionesCtrl;

        public DireccionFacturacionController(IConfiguration configuration)
        {
            DireccionesCtrl = new DireccionesCtrl(configuration);
        }

        #region Direcciones de factuacion B2B
        /// <summary>
        /// Obtiene direccion de facturacion seleccionada
        /// </summary>
        /// <param name="NombreDireccion">Direccion facturacion a mostrar</param>
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
                var Direccion_Re = DireccionesCtrl.GetB2B_fact(NombreDireccion);
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
        /// Obtiene listado de direcciones de facturacion del cliente en sesion (B2B)
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
                var Direccion_Re = DireccionesCtrl.GetB2B_dirFact();
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
        /// Obtiene direccion de facturación por default
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
                var Direccion_Re = DireccionesCtrl.GetB2B_factDef();
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

        #region Direcciones de factuacion B2C
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
        public ActionResult<DireccionFacturacion> GetDefaultB2C()
        {
            try
            {

                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_Re = DireccionesCtrl.GetB2C_factDef();
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
                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_Re = DireccionesCtrl.GetB2C_dirFact();
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
                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_Re = DireccionesCtrl.GetB2C_facturacion(id);
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

        // POST api/<DireccionFacturacionController>
        /// <summary>
        /// Listado de direcciones del cliente B2C en session(token)
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

                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                DireccionesCtrl.AddB2C_fact(DireccionFacturacion);
                return Ok(new { error = false, message = "Dirección agregada", data = DireccionFacturacion });
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
        /// Detalle de dirección del cliente B2C en session(token)
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
        public ActionResult EditB2C([FromBody] DireccionFacturacion DireccionFacturacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }
                DireccionesCtrl.IdCliente = DireccionesCtrl.DarkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                DireccionesCtrl.UpdB2C_fact(DireccionFacturacion);
                return Ok(new { error = false, message = "Dirección agregada", data = DireccionFacturacion });
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
