using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EcommerceFibremexApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductoComentController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        private IConfiguration configuration;
        public ProductoComentController(IConfiguration configuration)
        {
            this.configuration = configuration;
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ecommerce);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.ProductoComent);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.ProductoComentProm);
        }
        /// <summary>
        /// Listar comentarios del producto seleccionado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<ProductoComent>> GetComentarios(string id)
        {
            try
            {
                var Result = darkDev.ProductoComent.GetOpenquery($"where IdProducto = '{id}'", $"");
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
        /// promedio de comentarios por producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<SplittelRespData<ProductoComentProm>> GetPromedioProd(string id)
        {
            try
            {
                var Result = darkDev.ProductoComentProm.GetUnicSpecialStat($"SELECT " +
                        $"t01.t01_f003, " +
                        $"COUNT(*) AS TotalComentarios, " +
                        $"ROUND(SUM(t01.t01_f003) / COUNT(*), 2) AS Promedio, " +
                        $"IF(IdCliente = 0, 'Anonimo', CONCAT(t02.nombre, ' ', t02.apellidos)) AS Usuario " +
                    $"FROM t01_comentarios_producto AS t01 " +
                    $"LEFT JOIN login_cliente AS t02 " +
                    $"ON t01.IdCliente = t02.id_cliente " +
                    $"where t01.IdProducto = '{id}'");
                if (Result is null)
                    return BadRequest(new SplittelRespData<ProductoComentProm>
                    {
                        Code = 1000,
                        Message = "No fue encontrada la dirección de envio solicitada",
                    });
                else
                    return Ok(new SplittelRespData<ProductoComentProm>
                    {
                        Code = 0,
                        Message = "Solicitud exitosa",
                        Data = Result
                    });
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest(new SplittelRespData<ProductoComentProm>
                {
                    Code = 2000,
                    Message = ex.Message,
                });
            }
            catch (DarkExceptionUser ex)
            {
                return BadRequest(new SplittelRespData<ProductoComentProm>
                {
                    Code = 3000,
                    Message = ex.Message,
                });
            }
            finally
            {
                darkDev.CloseConnection();
            }
        }
    }
}
