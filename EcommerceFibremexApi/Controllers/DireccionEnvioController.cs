using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic;
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
        [HttpGet]
        [Authorize]
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
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<DireccionEnvio> GetB2C(int id)
        {
            
            try
            {
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_re = darkDev.DireccionEnvio.Get(id + "");
                if(Direccion_re.IdCliente == idCliente)
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
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DireccionEnvioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DireccionEnvioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
