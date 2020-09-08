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
    public class CategoriaController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public CategoriaController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ecommerce);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.Categoria);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.SubCategoria);
        }

        // GET: api/<CategoriaController>
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var Result = darkDev.Categoria.Get();
                Result.ForEach(a =>
                {
                    a.SubCategorias = darkDev.SubCategoria.Get(a.Codigo, darkDev.SubCategoria.ColumName(nameof(darkDev.SubCategoria.Element.IdFamilia)));
                });
                return Ok(Result.OrderBy(a => a.Orden));
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

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public ActionResult<Categoria> Get(string id)
        {
            try
            {
                var Result = darkDev.Categoria.GetByColumn(id, darkDev.Categoria.ColumName(nameof(darkDev.Categoria.Element.Codigo)));

                Result.SubCategorias = darkDev.SubCategoria.Get(Result.Codigo, darkDev.SubCategoria.ColumName(nameof(darkDev.SubCategoria.Element.IdFamilia)));
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

        // POST api/<CategoriaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
