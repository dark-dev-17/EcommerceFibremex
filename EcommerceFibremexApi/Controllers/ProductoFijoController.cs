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
        public ProductoFijoController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ecommerce);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.ProductoFijo);
        }

        // GET: api/<ProductoFijoController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductoFijo>> Get()
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get();
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
        public ActionResult<IEnumerable<ProductoFijo>> Categoria(string id)
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get(id, darkDev.ProductoFijo.ColumName(nameof(darkDev.ProductoFijo.Element.IdCategoria)));
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
        public ActionResult<IEnumerable<ProductoFijo>> SubCategoria(string id)
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get(id, darkDev.ProductoFijo.ColumName(nameof(darkDev.ProductoFijo.Element.IdSubcategoria)));
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
        public ActionResult<ProductoFijo> Get(string id)
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get(id);
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
