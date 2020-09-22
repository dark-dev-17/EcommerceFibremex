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

namespace EcommerceFibremexApi2.Controllers
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
        /// <summary>
        /// Obtiene el listado de sub categorias ordenadas por descripción
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<SubCategoria>> Get()
        {
            var Result = darkDev.SubCategoria.Get();
            return Ok(Result.OrderBy(a => a.SubDescripcion));
        }

        // GET api/<SubCategoriaController>/5
        /// <summary>
        /// Obtiene el detalle de una sub categoria por id
        /// </summary>
        /// <param name="id">Id de la subcategoria</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<SubCategoria> Get(string id)
        {
            var Result = darkDev.SubCategoria.Get(id, darkDev.SubCategoria.ColumName(nameof(darkDev.SubCategoria.Element.IdSubcategoria)));
            return Ok(Result);
        }

        // GET api/<SubCategoriaController>/5
        /// <summary>
        /// Obtiene las sub categorias por familia(Categoria)
        /// </summary>
        /// <param name="id">Id de la familia</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<SubCategoria>> GetByCategoria(string id)
        {
            var Result = darkDev.SubCategoria.Get(id, darkDev.SubCategoria.ColumName(nameof(darkDev.SubCategoria.Element.IdFamilia)));
            return Ok(Result);
        }
    }
}
