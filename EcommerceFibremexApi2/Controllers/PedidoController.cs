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
        [EnableCors("AllowAllHeaders")]
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
        /// Listado de pedidos B2C
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Pedidos encontrados</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        // GET api/values
        [HttpGet]
        [EnableCors("AllowAllHeaders")]
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
        [EnableCors("AllowAllHeaders")]
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
