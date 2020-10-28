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

namespace EcommerceFibremexApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public HomeController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();
            darkDev.LoadObject(MysqlObject.HomeSlide);
        }

        // GET: api/<Get>
        /// <summary>
        /// Lista de slides 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Lista de slides</response>
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
                var imagenes = darkDev.HomeSlide.GetSpecialStat(string.Format("call Ecom_HomeAnuncio_({0});", darkDev.tokenValidationAction.GetIdClienteToken(HttpContext)));
                imagenes.ForEach(a => a.PathImg1 = string.Format(@"http://fibremex.co/fibra-optica/public/images/img_spl/slide/img1/{0}", a.PathImg1));
                imagenes.ForEach(a => a.PathImg2 = string.Format(@"http://fibremex.co/fibra-optica/public/images/img_spl/slide/img2/{0}", a.PathImg2));
                return Ok(imagenes);
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
