using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurableCode;
using ConfigurableCode.Models;
using EcommerceFibremexApi2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceFibremexApi2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfiguradorController : ControllerBase
    {
        private ConfigurableCode.Manager manager;
        public ConfiguradorController()
        {
            manager = new ConfigurableCode.Manager();
        }

        #region General
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateConfiguration(string Nombre)
        {
            return Ok(manager.CreateNewConfiguracion(Nombre, Nombre));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetConfiguration(string Nombre)
        {
            manager.LoadConfiguration(Nombre);
            return Ok(manager.Configurable);
        }
        #endregion

        #region Elemento
        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddElement(string Nombre, string DescripcionElemento)
        {
            try
            {
                manager.LoadConfiguration(Nombre);
                manager.AddElement(DescripcionElemento);
                manager.SaveChanges(Nombre);
                return Ok(manager.Configurable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Valores
        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddElementValor([FromBody] ElementoValor elementoValor)
        {
            try
            {
                manager.LoadConfiguration(elementoValor.Nombre);
                manager.AddValor(elementoValor.IdElement, elementoValor.IdValor, elementoValor.Descripcion);
                manager.SaveChanges(elementoValor.Nombre);
                return Ok(manager.Configurable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
