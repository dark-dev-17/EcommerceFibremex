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

        // GET: api/<DetallePedidoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DetallePedidoController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ViewDetallePedido>> Get(int id)
        {
            try
            {
                var Pedido_re = darkDev.Pedido.Get(id + "");
                if (!darkDev.tokenValidationAction.Validation(Pedido_re.IdCliente, HttpContext, EcommerceApiLogic.Validators.TokenValidationType.Pedido))
                {
                    return Unauthorized();
                }
                var Detalle_re = darkDev.ViewDetallePedido.Get(""+ id, darkDev.ViewDetallePedido.ColumName(nameof(darkDev.ViewDetallePedido.Element.IdPedido)));
                return Ok(Detalle_re);
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

        // POST api/<DetallePedidoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DetallePedidoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DetallePedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
