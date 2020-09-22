using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ModelsSap;
using EcommerceApiLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceFibremexApi2.Controllers
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
        /// <summary>
        /// Listado de categorias
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Detalle de pedido encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
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
        /// <summary>
        /// Ver categoria con sus subcategorias
        /// </summary>
        /// <param name="id">Id de la categoria(familia)</param>
        /// <returns></returns>
        /// <response code="200">Detalle de pedido encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
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
    }
}