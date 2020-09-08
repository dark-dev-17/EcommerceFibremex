using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.ModelsSap;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceFibremexApi.Controllers
{
    [EnableCors("SplittelPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        private readonly IConfiguration configuration;
        public GeneralController(IConfiguration configuration)
        {
            this.configuration = configuration;
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.SSQLObject.TipoCambio);
        }

        // GET: api/<GeneralController>
        [HttpGet("{Moneda}")]
        public ActionResult<TipoCambio> GetTipoCambio(string Moneda)
        {
            try
            {
                if (string.IsNullOrEmpty(Moneda))
                {
                    throw new DarkExceptionUser("Moneda faltante");
                }

                DateTime Hoy = DateTime.Now;
                var Result = darkDev.TipoCambio.Get(
                        darkDev.TipoCambio.ColumName(nameof(darkDev.TipoCambio.Element.Currency)), Moneda.Trim(),
                        darkDev.TipoCambio.ColumName(nameof(darkDev.TipoCambio.Element.RateDate)), Hoy.ToString("yyyy-MM-dd")
                    );
                if(Result == null)
                {
                    throw new DarkExceptionUser("No se encontro el tipo de cambio");
                }
                return Ok(Result);
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

        // GET api/<GeneralController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GeneralController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GeneralController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GeneralController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
