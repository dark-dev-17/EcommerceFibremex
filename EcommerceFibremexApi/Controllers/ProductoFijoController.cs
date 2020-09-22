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

namespace EcommerceFibremexApi.Controllers
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
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ProductoFijo>> Get()
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get().Where(a => a.CodigoConfigurable == "" && a.ProductoActivo == "si");
                return Ok(Result.OrderBy(a => a.Descripcion ));
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
        [HttpGet("{id}")]
        [Authorize]
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
        [HttpGet("{id}")]
        [Authorize]
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
        [HttpGet("{id}")]
        [Authorize]
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

        // POST api/<ProductoFijoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductoFijoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductoFijoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
