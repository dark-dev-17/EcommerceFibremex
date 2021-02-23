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
        private ConfigurableCode.Controllers.MainV1Ctrl MainV1Ctrl;
        public ConfiguradorController()
        {
            
        }

        #region General
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateConfiguration(string Nombre)
        {
            try
            {
                MainV1Ctrl = new ConfigurableCode.Controllers.MainV1Ctrl(Nombre);
                MainV1Ctrl.Create();
                return Ok(MainV1Ctrl.Configurable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult OpenConfiguration(string Nombre)
        {
            try
            {
                MainV1Ctrl = new ConfigurableCode.Controllers.MainV1Ctrl(Nombre);
                MainV1Ctrl.Open();
                return Ok(MainV1Ctrl.Configurable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Elemento
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateParte([FromBody] Re_ConfParteAdd re_ConfParteAdd)
        {
            try
            {
                MainV1Ctrl = new ConfigurableCode.Controllers.MainV1Ctrl(re_ConfParteAdd.NameFile);
                MainV1Ctrl.Open();
                MainV1Ctrl.PartesCrl.Add(re_ConfParteAdd.Tipo, re_ConfParteAdd.Descripcion);
                MainV1Ctrl.Save();
                return Ok(MainV1Ctrl.Configurable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult IpdateParte([FromBody] Re_ConfParteUpd re_ConfParteAdd)
        {
            try
            {
                MainV1Ctrl = new ConfigurableCode.Controllers.MainV1Ctrl(re_ConfParteAdd.NameFile);
                MainV1Ctrl.Open();
                MainV1Ctrl.PartesCrl.Update(re_ConfParteAdd.Id, re_ConfParteAdd.Tipo, re_ConfParteAdd.Descripcion);
                MainV1Ctrl.Save();
                return Ok(MainV1Ctrl.Configurable);
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
        public IActionResult CreateOpcion(int Id, string clave, string Title, string NameFile)
        {
            try
            {
                MainV1Ctrl = new ConfigurableCode.Controllers.MainV1Ctrl(NameFile);
                MainV1Ctrl.Open();
                MainV1Ctrl.OpcionesCtrl.AddOpcion(Id, clave, Title);
                MainV1Ctrl.Save();
                return Ok(MainV1Ctrl.Configurable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult GetJson(string NameFile)
        {
            try
            {
                var man = new Manager().GetSon(NameFile);
                return Ok(man);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
