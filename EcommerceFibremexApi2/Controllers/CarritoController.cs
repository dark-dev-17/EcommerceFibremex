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
using EcommerceFibremexApi2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceFibremexApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public CarritoController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();
        }

        // GET: api/<CarritoController>
        /// <summary>
        /// Lista de carritos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Lista de carritos</response>
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
                PedidoRule pedidoRule = new PedidoRule(darkDev, darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                return Ok(pedidoRule.GetPedidos("c"));
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema: " + ex.Message);
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

        // GET api/<CarritoController>/5
        /// <summary>
        /// Obetener un pedido
        /// </summary>
        /// <param name="id">Folio de un pedido</param>
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
        public ActionResult<Pedido> Get(int id)
        {
            try
            {
                PedidoRule pedidoRule = new PedidoRule(darkDev, darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                return Ok(pedidoRule.GetPedido(id));
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema: " + ex.Message);
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
        /// Obtiene el numero de partidas en el carrito
        /// </summary>
        /// <param name="id"></param>
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
        public ActionResult<int> CountItems(int id)
        {
            try
            {
                PedidoRule pedidoRule = new PedidoRule(darkDev, darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                return Ok(pedidoRule.GetPedido(id).DetallePedidos.Count);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema: " + ex.Message);
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

        // POST api/<CarritoController>
        /// <summary>
        /// Agregar nuevo articulo a carrito
        /// </summary>
        /// <param name="nuevoArticulo">Producto a agregar</param>
        /// <returns></returns>
        /// <response code="200">producto agregado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult Agregar([FromBody] NuevoArticulo nuevoArticulo)
        {
            try
            {
                PedidoRule pedidoRule = new PedidoRule(darkDev, darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                int IdPedido_re = pedidoRule.AddCarrito(nuevoArticulo.IdPedido, nuevoArticulo.CodigoProducto, nuevoArticulo.Cantidad);
                return Ok(new { IdPedido = IdPedido_re, Mensaje = "Producto agregado", error = false });
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema: " + ex.Message);
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

        // PUT api/<CarritoController>/5
        /// <summary>
        /// Actualizar cantidad de producto solicitado de un carrito
        /// </summary>
        /// <param name="nuevoArticulo">Producto a modificar</param>
        /// <returns></returns>
        /// <response code="200">Producto actualizado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult Update([FromBody] NuevoArticulo nuevoArticulo)
        {
            try
            {
                PedidoRule pedidoRule = new PedidoRule(darkDev, darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                pedidoRule.CambiarCantidad(nuevoArticulo.CodigoProducto, nuevoArticulo.Cantidad, nuevoArticulo.IdPedido);
                return Ok(new { IdPedido = nuevoArticulo.IdPedido, Mensaje = "Producto actualizado", error = false });
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema: " + ex.Message);
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

        // DELETE api/<CarritoController>/5
        /// <summary>
        /// Eliminar un producto de un carrito
        /// </summary>
        /// <param name="IdPedido">Id del carrito</param>
        /// <param name="ProductoCode">Producto</param>
        /// <returns></returns>
        /// <response code="200">Producto eliminado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpPost("{IdPedido}/{ProductoCode}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult Delete(int IdPedido, string ProductoCode)
        {
            try
            {
                PedidoRule pedidoRule = new PedidoRule(darkDev, darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                pedidoRule.EliminarProducto(ProductoCode, IdPedido);
                return Ok(new { IdPedido = IdPedido, Mensaje = "Producto eliminado", error = false });
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema: " + ex.Message);
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
        /// Pagar pedido con tarjeta de credido
        /// </summary>
        /// <param name="requestPedido">Folio del pedido</param>
        /// <returns></returns>
        /// <response code="200">Pedido pagado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult PagarTarjetaCredido(RequestPedido requestPedido)
        {
            try
            {
                OpenPayRule OpenPayRule = new OpenPayRule(darkDev, darkDev.tokenValidationAction.GetIdClienteToken(HttpContext), requestPedido.IdPedido);
                OpenPayRule.CheateChargeCard(requestPedido);
                return Ok(new { IdPedido = requestPedido.IdPedido, Mensaje = "Pedido pagado exitosamente!", error = false });
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema: " + ex.Message);
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