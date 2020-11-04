using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Models;
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
    public class PedidoController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public PedidoController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ecommerce);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.ViewPedido);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.PedidoB2C);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.PedidoB2B);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.ViewDetallePedido);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.ViewPedidoB2C_);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.LogErrorsOpenPay);
        }

        #region Pedidos generales
        // GET api/values
        /// <summary>
        /// Listado de pedidos sin filtro B2B y B2C
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Lista de pedidos</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Pedido>> Get()
        {
            try
            {
                return Ok(darkDev.ViewPedido.Get("" + darkDev.tokenValidationAction.GetIdClienteToken(HttpContext), darkDev.ViewPedido.ColumName(nameof(darkDev.ViewPedido.Element.IdCliente))));
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

        // GET api/values/5
        /// <summary>
        /// Ver detalle de pedido sin importar division B2B y B2C
        /// </summary>
        /// <param name="id">Folio del pedido</param>
        /// <returns></returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<ViewPedido> Get(int id)
        {
            try
            {
                var result = darkDev.ViewPedido.GetByColumn("" + id, darkDev.ViewPedido.ColumName(nameof(darkDev.ViewPedido.Element.IdPedido)));
                if (!darkDev.tokenValidationAction.Validation(result.IdCliente, HttpContext, EcommerceApiLogic.Validators.TokenValidationType.Pedido))
                {
                    return Unauthorized();
                }
                return Ok(result);
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
        #endregion

        #region Pedidos b2c
        /// <summary>
        /// obtener logs open pay 
        /// </summary>
        /// <param name="IdCotizacion">Folio ecomerce</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<LogErrorsOpenPay>> GetLogOpenPay(int IdCotizacion)
        {
            try
            {
                if (IdCotizacion == 0)
                {
                    throw new DarkExceptionUser("NFolio no valido");
                }
                string Query = string.Format("WHERE t16_f005 = '{0}'", IdCotizacion);
                return Ok(darkDev.LogErrorsOpenPay.GetOpenquery(Query, "ORDER BY t16_pk01 DESC LIMIT 1"));
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
                darkDev.CloseConnection();
                darkDev = null;
            }
        }
        /// <summary>
        /// Obtiene lista de cotizaciones
        /// </summary>
        /// <returns></returns>
        /// <response code="200">cotizaciones encontrados</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<ViewPedido>> GetB2C_Cotizaciones()
        {
            try
            {
                if (darkDev.tokenValidationAction.GetTipoCliente(HttpContext).Trim() != "B2C")
                {
                    throw new DarkExceptionUser("No puedes acceder a esta sección");
                }
                string Query = string.Format("WHERE id_cliente = '{0}' AND estatus = 'C' AND total > 0 AND fecha >= DATE_ADD(CURDATE(), INTERVAL -10 DAY)", darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                return Ok(darkDev.ViewPedido.GetOpenquery(Query, "order by fecha desc"));
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
                darkDev.CloseConnection();
                darkDev = null;
            }
        }
        /// <summary>
        /// Obtiene lista de pedidos pendientes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Pedidos encontrados</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<PedidoB2C>> GetB2C_pendientes()
        {
            try
            {
                if (darkDev.tokenValidationAction.GetTipoCliente(HttpContext).Trim() != "B2C")
                {
                    throw new DarkExceptionUser("No puedes acceder a esta sección");
                }

                string Query = string.Format("WHERE id_cliente = '{0}' AND t05_f008 <> 0 AND (t12_f001 = 'charge.succeeded' AND t12_f004 = 'completed' || t12_f001 = 'charge.created' && t12_f004 = 'in_progress' )", darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                return Ok(darkDev.PedidoB2C.GetOpenquery(Query, "order by fecha desc"));
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
                darkDev.CloseConnection();
                darkDev = null;
            }
        }
        /// <summary>
        /// Obtiene pedidos terminados 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Pedidos encontrados</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<PedidoB2C>> GetB2C_Terminados()
        {
            try
            {
                if (darkDev.tokenValidationAction.GetTipoCliente(HttpContext).Trim() != "B2C")
                {
                    throw new DarkExceptionUser("No puedes acceder a esta sección");
                }
                string Query = string.Format("WHERE id_cliente = '{0}' AND t05_f008 = 0 AND t12_f004 = 'completed'", darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                return Ok(darkDev.PedidoB2C.GetOpenquery(Query, "order by fecha desc"));
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
                darkDev.CloseConnection();
                darkDev = null;
            }
        }
        /// <summary>
        /// Listado de pedidos B2C
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Pedidos encontrados</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        // GET api/values
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<PedidoB2C>> GetB2C()
        {
            try
            {
                if (darkDev.tokenValidationAction.GetTipoCliente(HttpContext).Trim() != "B2C")
                {
                    throw new DarkExceptionUser("No puedes acceder a esta sección");
                }
                return Ok(darkDev.PedidoB2C.Get("" + darkDev.tokenValidationAction.GetIdClienteToken(HttpContext), darkDev.PedidoB2C.ColumName(nameof(darkDev.PedidoB2C.Element.IdCliente))));
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
                darkDev.CloseConnection();
                darkDev = null;
            }
        }

        // GET api/values/5
        /// <summary>
        /// Detalle de pedido B2C
        /// </summary>
        /// <param name="id">Folio del pedido</param>
        /// <returns></returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<PedidoB2C> GetB2C(int id)
        {
            try
            {
                if (darkDev.tokenValidationAction.GetTipoCliente(HttpContext).Trim() != "B2C")
                {
                    throw new DarkExceptionUser("No puedes acceder a esta sección");
                }
                var result = darkDev.PedidoB2C.GetByColumn("" + id, darkDev.PedidoB2C.ColumName(nameof(darkDev.PedidoB2C.Element.IdPedido)));
                if (result == null)
                {
                    throw new DarkExceptionUser("El pedido no fue encontrado");
                }

                result.Detalle = darkDev.ViewDetallePedido.Get("" + result.IdPedido, darkDev.ViewDetallePedido.ColumName(nameof(darkDev.ViewDetallePedido.Element.IdPedido)));

                if (!darkDev.tokenValidationAction.Validation(result.IdCliente, HttpContext, EcommerceApiLogic.Validators.TokenValidationType.Pedido))
                {
                    return Unauthorized();
                }
                return Ok(result);
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
                darkDev.CloseConnection();
                darkDev = null;
            }
        }
        #endregion

        #region Pedidos b2b
        // GET api/values
        /// <summary>
        /// Listado de pedidos B2C
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Listado de pedidos</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<PedidoB2B>> GetB2B()
        {
            try
            {
                if (darkDev.tokenValidationAction.GetTipoCliente(HttpContext).Trim() != "B2B")
                {
                    throw new DarkExceptionUser("No puedes acceder a esta sección");
                }

                return Ok(darkDev.PedidoB2B.Get("" + darkDev.tokenValidationAction.GetIdClienteToken(HttpContext), darkDev.PedidoB2B.ColumName(nameof(darkDev.PedidoB2B.Element.IdCliente))));
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
                darkDev.CloseConnection();
            }
        }

        // GET api/values/5
        /// <summary>
        /// Obtener detalle de un pedido de un cliente B2B
        /// </summary>
        /// <param name="id">Folio del pedido</param>
        /// <returns></returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<PedidoB2B> GetB2B(int id)
        {
            try
            {
                if (darkDev.tokenValidationAction.GetTipoCliente(HttpContext).Trim() != "B2B")
                {
                    throw new DarkExceptionUser("No puedes acceder a esta sección");
                }

                var result = darkDev.PedidoB2B.GetByColumn("" + id, darkDev.PedidoB2B.ColumName(nameof(darkDev.PedidoB2B.Element.IdPedido)));
                if (result == null)
                {
                    throw new DarkExceptionUser("El pedido no fue encontrado");
                }

                result.Detalle = darkDev.ViewDetallePedido.Get("" + result.IdPedido, darkDev.ViewDetallePedido.ColumName(nameof(darkDev.ViewDetallePedido.Element.IdPedido)));

                if (!darkDev.tokenValidationAction.Validation(result.IdCliente, HttpContext, EcommerceApiLogic.Validators.TokenValidationType.Pedido))
                {
                    return Unauthorized();
                }
                return Ok(result);
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
                darkDev.CloseConnection();
            }
        }
        #endregion
    }
}
