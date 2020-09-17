using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class SubCategoriaController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        public SubCategoriaController(IConfiguration configuration)
        {
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ecommerce);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.SubCategoria);
        }

        // GET: api/<SubCategoriaController>
        [HttpGet]
        public ActionResult<IEnumerable<SubCategoria>> Get()
        {
            var Result = darkDev.SubCategoria.Get();
            return Ok(Result.OrderBy(a => a.SubDescripcion ));
        }

        // GET api/<SubCategoriaController>/5
        [HttpGet("{id}")]
        public ActionResult<SubCategoria> Get(string id)
        {
            var Result = darkDev.SubCategoria.Get(id, darkDev.SubCategoria.ColumName(nameof(darkDev.SubCategoria.Element.IdSubcategoria)));
            return Ok(Result);
        }
        // GET api/<SubCategoriaController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<SubCategoria>> GetByCategoria(string id)
        {
            var Result = darkDev.SubCategoria.Get(id, darkDev.SubCategoria.ColumName(nameof(darkDev.SubCategoria.Element.IdFamilia)));
            return Ok(Result);
        }

        // POST api/<SubCategoriaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SubCategoriaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SubCategoriaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
