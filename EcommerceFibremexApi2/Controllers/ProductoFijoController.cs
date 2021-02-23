using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Enums;
using EcommerceApiLogic.Herramientas;
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
        /// <summary>
        /// Obtiene los articulos fijos de acuerdo a la categoria seleccionada, y muestra el limite seleccionado en forma random
        /// </summary>
        /// <param name="Categoria">Categoria</param>
        /// <param name="Limit">Número de productos a mostrar</param>
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
        public ActionResult<IEnumerable<ProductoFijo>> GetRandomByCate(string Categoria, int Limit)
        {
            try
            {
                string where = string.Format("WHERE subcategoria='{0}' AND (codigo_configurable = '' OR codigo_configurable IS NULL ) ", Categoria);
                string order = string.Format("ORDER BY RAND() LIMIT {0} ", Limit);
                var Result = darkDev.ProductoFijo.GetOpenquery(where, order).Where(a => a.CodigoConfigurable == "" && a.ProductoActivo == "si");
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

        /// <summary>
        /// Obtener archivos del producto seleccionado
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="Tipo">0.- Producto, 1.- Descripcion, 2.- InfoAdicional, 3.- Miniatura</param>
        /// <returns></returns>
        /// <response code="200">Detalle del producto</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet()]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<List<FileFtp>> GetFiles(string Codigo, ProductoTipoFile Tipo)
        {
            try
            {
                Codigo = EcommerceApiLogic.Validators.EncryptData.DecryptProd(Codigo);
                // Limpiar codigo de caracteres como [/] remplazar por  [-]
                string CodigoReal = Codigo;
                Codigo = Codigo.Replace('-', '/');
                string Path = "";
                string RutePublic = "";
                if (Tipo == ProductoTipoFile.Producto)
                {
                    Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/*.jpg";
                    RutePublic = string.Format(@"images/img_spl/productos/{0}/", Codigo);
                }
                else if (Tipo == ProductoTipoFile.Descripcion)
                {
                    Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/descripcion/*.jpg";
                    RutePublic = string.Format(@"images/img_spl/productos/{0}/descripcion/", Codigo);
                }
                else if (Tipo == ProductoTipoFile.InfoAdicional)
                {
                    Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/adicional/*.jpg";
                    RutePublic = string.Format(@"images/img_spl/productos/{0}/adicional/", Codigo);
                }
                else if (Tipo == ProductoTipoFile.Miniatura)
                {
                    Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/thumbnail/*.jpg";
                    RutePublic = string.Format(@"images/img_spl/productos/{0}/thumbnail/", Codigo);
                }

                var result = darkDev.FtpFiles.GetFiles(Path.Replace("*.jpg", ""), RutePublic, "jpg");
                
                return Ok(result);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest(ex.Message);
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

                id = EcommerceApiLogic.Validators.EncryptData.DecryptProd(id);


                var Result = darkDev.ProductoFijo.Get(id);
                string Domain = configuration.GetSection("Ftp").GetSection("Domain").Value;

                Result.SlideImg = new List<string>();
                Result.SlideImgAdicionales = new List<string>();
                Result.SlideImgDescripcion = new List<string>();


                darkDev.FtpFiles.GetFiles(
                    $"public_html/fibra-optica/public/images/img_spl/productos/{id}/",
                    string.Format(@"images/img_spl/productos/{0}/", id), "jpg")
                .ForEach(a => {
                    Result.SlideImg.Add(a.Ruta);
                });

                return Ok(Result);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest(ex.Message);
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
        /// <summary>
        /// Obtener artiulo en lista
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
        public ActionResult<IEnumerable<ProductoFijo>> GetInList(string id)
        {
            try
            {
                var Result = darkDev.ProductoFijo.Get(id);

                var Images = darkDev.FtpFiles.GetFiles($"public_html/fibra-optica/public/images/img_spl/productos/{id}/", string.Format(@"images/img_spl/productos/{0}/", id), "jpg");

                string Domain = configuration.GetSection("Ftp").GetSection("Domain").Value;

                Result.SlideImg = new List<string>();

                Images.ForEach(a => {
                    Result.SlideImg.Add(a.Ruta);
                });
                return Ok(new List<ProductoFijo> { Result });
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
