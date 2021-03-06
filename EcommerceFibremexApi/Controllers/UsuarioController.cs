﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using System.Threading.Tasks;
using DbManagerDark.Exceptions;
using EcommerceApiLogic.Herramientas;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.Validators;
using EcommerceFibremexApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceFibremexApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private EcommerceApiLogic.DarkDev darkDev;
        private readonly IConfiguration configuration;
        public UsuarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
            darkDev = new EcommerceApiLogic.DarkDev(configuration, DbManagerDark.DarkMode.Ecommerce);
            darkDev.OpenConnection();
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.Usuario);
            darkDev.LoadObject(EcommerceApiLogic.MysqlObject.Cliente);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            return darkDev.Usuario.Get();
        }

        // GET api/values/5
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<Usuario> Get([FromBody] Login Login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Usuario o contraseña vacias");
                }

                var Result = darkDev.Usuario.Get(
                    darkDev.Usuario.ColumName(nameof(darkDev.Usuario.Element.Email)), Login.User.Trim(),
                    darkDev.Usuario.ColumName(nameof(darkDev.Usuario.Element.Password)), Login.Password.Trim()
                );

                if(Result == null)
                {
                    return Unauthorized();
                }
                var response = new {
                    token = darkDev.tokenValidationAction.GenerateToken(Result),
                    Expiration = DateTime.Now.AddMinutes(Int32.Parse(configuration["Jwt:TokenExpirationInMinutes"])),
                    TipoCliente = darkDev.Cliente.GetByColumn(Result.IdCliente+"", darkDev.Cliente.ColumName(nameof(darkDev.Cliente.Element.IdCliente))).TipoCliente
                };
                //Herramientas.EscribeLogError(JsonSerializer.Serialize(response));
                return Ok(response);
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

        // GET api/values/5
        [HttpGet]
        [Route("{token}")]
        [AllowAnonymous]
        public ActionResult<string> Vertoken(string token)
        {
            Herramientas.EscribeLogError(token);

            return token;
        }

        // POST api/values
        [HttpPost]
        [EnableCors("AllowAllHeaders")]
        //[ApiExplorerSettings(GroupName = "v1")]
        public ActionResult Login([FromBody] Login Login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("usuario o contraseña vacias");
                }

                var Result = darkDev.Usuario.GetByColumn(
                    Login.User.Trim(), darkDev.Usuario.ColumName(nameof(darkDev.Usuario.Element.Email))
                );
                
                if (Result == null)
                {
                    return Unauthorized();
                }
                string pass_real = "";
                using (EncrypData encrypData = new EncrypData("password"))
                {
                    pass_real = encrypData.Decrypt(Result.Password);
                }
                if (pass_real.Trim() == Login.Password.Trim())
                {
                    var Cliente_re = darkDev.Cliente.GetByColumn(Result.IdCliente + "", darkDev.Cliente.ColumName(nameof(darkDev.Cliente.Element.IdCliente)));
                    var response = new
                    {
                        token = darkDev.tokenValidationAction.GenerateToken(Result),
                        Expiration = DateTime.Now.AddMinutes(Int32.Parse(configuration["Jwt:TokenExpirationInMinutes"])),
                        TipoCliente = Cliente_re.TipoCliente,
                        NombreCliente = Cliente_re.Nombre,
                    };
                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }
                
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
