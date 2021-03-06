﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Catalogos;
using EcommerceApiLogic.Diccionarios;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ModelsSap;
using EcommerceApiLogic.Rules;
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
    public class GeneralController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        private readonly IConfiguration configuration;
        public GeneralController(IConfiguration configuration)
        {
            this.configuration = configuration;
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.SSQLObject.TipoCambio);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.OpenPayKeys);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.CFDI_Usos);
        }

        /// <summary>
        /// Extraer lista de usos de CFDI
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Lista de CFDI</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<Pais> GetUsosCFDI()
        {
            return Ok(darkDev.CFDI_Usos.Get());
        }
        /// <summary>
        /// Informacion de contacto de la empresa
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Lista de CFDI</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<SplittelContacto> GetSplittelContact()
        {
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.SplittelContacto);
            return Ok(darkDev.SplittelContacto.Get(1));
        }
        /// <summary>
        /// Extraer lista de estados 
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Lista de estados</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public ActionResult<Pais> GetEstados()
        {
            return Ok(new Pais());
        }
        // GET: api/<GeneralController>
        /// <summary>
        /// Extraer tipo de cambio del ecommerce
        /// </summary>
        /// <param name="Moneda">Moneda(USD)</param>
        /// <returns></returns>
        /// <response code="200">Tipo de cambio</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet("{Moneda}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<TipoCambio> GetTipoCambio(string Moneda)
        {
            try
            {
                if (string.IsNullOrEmpty(Moneda))
                {
                    throw new DarkExceptionUser("Moneda faltante");
                }

                DateTime Hoy = DateTime.Now;
                var Result = darkDev.TipoCambio.Get(
                        darkDev.TipoCambio.ColumName(nameof(darkDev.TipoCambio.Element.Currency)), Moneda.Trim(),
                        darkDev.TipoCambio.ColumName(nameof(darkDev.TipoCambio.Element.RateDate)), Hoy.ToString("yyyy-MM-dd")
                    );
                if (Result == null)
                {
                    throw new DarkExceptionUser("No se encontro el tipo de cambio");
                }
                return Ok(Result);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
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
        // GET: api/<GeneralController>
        /// <summary>
        /// Extraer claves de conexion a OpenPay
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Claves de OpenPay</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<TipoCambio> GetOpenPay()
        {
            try
            {
                DateTime Hoy = DateTime.Now;
                var Result = darkDev.OpenPayKeys.Get("1");
                if (Result == null)
                {
                    throw new DarkExceptionUser("No se encontraron los datos maestros");
                }
                return Ok(Result);
            }
            catch (DarkExceptionSystem ex)
            {
                return BadRequest("Error sistema");
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
        /// Obtiene la lista de municipios del estado seleccionado
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        /// <response code="200">Claves de OpenPay</response>
        /// <response code="400">Errores de sistema y errores de usuario</response>
        /// <response code="401">Sin autorizacion(token caducado)</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult<Municipio> GetMunicipios(string Estado)
        {
            try
            {
                MunicipioCtrl municipioCtrl = new MunicipioCtrl();
                return Ok(municipioCtrl.GetMunicipios(Estado));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {

            }
            
        }
    }
}
