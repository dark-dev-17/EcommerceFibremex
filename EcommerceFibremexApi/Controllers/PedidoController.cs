using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EcommerceFibremexApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public PedidoController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ecommerce);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.Pedido);
        }

        // GET api/values
        [HttpGet]
        [Route("[action]")]
        [EnableCors("AllowAllHeaders")]
        [Authorize]
        public ActionResult<IEnumerable<Pedido>> Get()
        {
            try
            {
                return Ok(darkDev.Pedido.Get("" + darkDev.tokenValidationAction.GetIdClienteToken(HttpContext), darkDev.Pedido.ColumName(nameof(darkDev.Pedido.Element.IdCliente))));
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
        [HttpGet("{id}")]
        [Route("[action]/{id}")]
        [Authorize]
        public ActionResult<Pedido> Get(int id)
        {
            try
            {
                var result = darkDev.Pedido.GetByColumn("" + id, darkDev.Pedido.ColumName(nameof(darkDev.Pedido.Element.IdPedido)));
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

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            int IdCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
