using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.Rules;
using EcommerceFibremexApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceFibremexApi.Controllers
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
        [HttpGet]
        [Authorize]
        [ApiExplorerSettings(GroupName = "v1")]
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
        [HttpGet("{id}")]
        [Authorize]
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

        // POST api/<CarritoController>
        [HttpPost]
        [Authorize]
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
                return BadRequest("Error: " +  ex.Message);
            }
            finally
            {
                darkDev.CloseConnection();
            }
        }

        // PUT api/<CarritoController>/5
        [HttpPut]
        [Authorize]
        public ActionResult Update([FromBody] NuevoArticulo nuevoArticulo)
        {
            try
            {
                PedidoRule pedidoRule = new PedidoRule(darkDev, darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                pedidoRule.CambiarCantidad(nuevoArticulo.CodigoProducto, nuevoArticulo.Cantidad, nuevoArticulo.IdPedido);
                return Ok("Producto actualizado");
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
        [HttpDelete("{IdPedido}/{ProductoCode}")]
        [Authorize]
        public ActionResult Delete(int IdPedido, string ProductoCode)
        {
            try
            {
                PedidoRule pedidoRule = new PedidoRule(darkDev, darkDev.tokenValidationAction.GetIdClienteToken(HttpContext));
                pedidoRule.EliminarProducto(ProductoCode, IdPedido);
                return Ok("Producto eliminado");
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
