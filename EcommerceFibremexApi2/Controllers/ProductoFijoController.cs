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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceFibremexApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductoFijoController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        private IConfiguration configuration;
        public ProductoFijoController(IConfiguration configuration)
        {
            this.configuration = configuration;
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ecommerce);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.ProductoFijo);
        }

        // GET: api/<ProductoFijoController>
        /// <summary>
        /// Listado de productos fijos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Lista de productos</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<ProductoFijo>> Get()
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get().Where(a => a.CodigoConfigurable == "" && a.ProductoActivo == "si");
                return Ok(Result.OrderBy(a => a.Descripcion));
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

        // GET: api/<ProductoFijoController>
        /// <summary>
        /// Listado de productos por categoria
        /// </summary>
        /// <param name="id">Id de la categoria</param>
        /// <returns></returns>
        /// <response code="200">Listado de productos por categoria</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<ProductoFijo>> Categoria(string id)
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get(id, darkDev.ProductoFijo.ColumName(nameof(darkDev.ProductoFijo.Element.IdCategoria)))
                    .Where(a => a.CodigoConfigurable == "" && a.ProductoActivo == "si");
                return Ok(Result.OrderBy(a => a.Descripcion));
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

        // GET: api/<ProductoFijoController>
        /// <summary>
        /// Listado de productos por sub categoria
        /// </summary>
        /// <param name="id">Id de la sub categoria</param>
        /// <returns></returns>
        /// <response code="200">Listado de producto por categoria</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<ProductoFijo>> SubCategoria(string id)
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get(id, darkDev.ProductoFijo.ColumName(nameof(darkDev.ProductoFijo.Element.IdSubcategoria)))
                    .Where(a => a.CodigoConfigurable == "" && a.ProductoActivo == "si"); ;
                return Ok(Result.OrderBy(a => a.Descripcion));
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

        // GET api/<ProductoFijoController>/5
        /// <summary>
        /// Detalle del producto seleccionado, imagenes
        /// </summary>
        /// <param name="id">Codigo del articulo</param>
        /// <returns></returns>
        /// <response code="200">Detalle del producto</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<ProductoFijo> Get(string id)
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get(id);

                var Images = darkDev.FtpFiles.GetFiles(string.Format("public_html/fibra-optica/public/images/img_spl/productos/{0}/*.jpg", id));

                string Domain = configuration.GetSection("Ftp").GetSection("Domain").Value;

                Result.SlideImg = new List<string>();

                Images.ForEach(a => {
                    Result.SlideImg.Add(string.Format(@"{0}/fibra-optica/public/images/img_spl/productos/{1}/{2}", Domain, id, a));
                });
                return Ok(Result);
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
    }
}
