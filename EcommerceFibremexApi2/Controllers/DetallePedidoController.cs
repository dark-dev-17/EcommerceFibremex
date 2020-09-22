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
    public class DetallePedidoController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public DetallePedidoController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.ViewDetallePedido);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.Pedido);
        }

        // GET api/<DetallePedidoController>/5
        /// <summary>
        /// Obtener partidas del pedido o cotización seleccionada
        /// </summary>
        /// <param name="id">Folio del pedido</param>
        /// <returns></returns>
        /// <response code="200">Detalle de pedido encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<ViewDetallePedido>> Get(int id)
        {
            try
            {
                var Pedido_re = darkDev.Pedido.Get(id + "");
                if (!darkDev.tokenValidationAction.Validation(Pedido_re.IdCliente, HttpContext, EcommerceApiLogic.Validators.TokenValidationType.Pedido))
                {
                    return Unauthorized();
                }
                var Detalle_re = darkDev.ViewDetallePedido.Get("" + id, darkDev.ViewDetallePedido.ColumName(nameof(darkDev.ViewDetallePedido.Element.IdPedido)));
                return Ok(Detalle_re.Where(a => a.DetalleActivo == "si"));
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
    }
}