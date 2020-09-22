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
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceFibremexApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DireccionFacturacionController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public DireccionFacturacionController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();
            darkDev.LoadObject(MysqlObject.DireccionFacturacion);
        }

        // GET: api/<DireccionFacturacionController>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(typeof(DireccionFacturacion))]
        [SwaggerResponse(200,Type = typeof(DireccionFacturacion))]
        [Authorize]
        public ActionResult<IEnumerable<DireccionFacturacion>> GetB2C()
        {
            try
            {
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                return Ok(darkDev.DireccionFacturacion.Get("" + idCliente, darkDev.DireccionFacturacion.ColumName(nameof(darkDev.DireccionFacturacion.Element.IdCliente))));
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

        // GET api/<DireccionFacturacionController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<DireccionFacturacion> GetB2C(int id)
        {
            
            try
            {
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                var Direccion_re = darkDev.DireccionFacturacion.Get(id + "");
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

        // POST api/<DireccionFacturacionController>
        [HttpPost]
        public ActionResult CreateB2C([FromBody] DireccionFacturacion DireccionFacturacion)
        {
            try
            {
                darkDev.DireccionFacturacion.Element = DireccionFacturacion;
                darkDev.DireccionFacturacion.Element.IdCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                if (darkDev.DireccionFacturacion.Add())
                {
                    return Ok(new { error = false, message = "Dirección agregada", data = DireccionFacturacion });
                }
                else
                {
                    return BadRequest("Dirección no guardada");
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

        [HttpPost]
        public ActionResult EditB2B([FromBody] DireccionFacturacion DireccionFacturacion)
        {
            try
            {
                var Direccion_re = darkDev.DireccionFacturacion.Get(DireccionFacturacion.IdDireccionFacturacion);
                int idCliente = darkDev.tokenValidationAction.GetIdClienteToken(HttpContext);
                if (Direccion_re == null)
                {
                    throw new DarkExceptionUser("La dirección requerida no fue encontrada");
                }

                if (Direccion_re.IdCliente != idCliente)
                {
                    throw new DarkExceptionUser("No puedes actualizar esta direccion");
                }

                darkDev.DireccionFacturacion.Element = DireccionFacturacion;
                if (darkDev.DireccionFacturacion.Update())
                {
                    return Ok(new { error = false, message = "Dirección actualizada", data = DireccionFacturacion });
                }
                else
                {
                    return BadRequest("Dirección no actualizada");
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

    }
}
