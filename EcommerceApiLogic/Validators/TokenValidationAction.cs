using DbManagerDark.Exceptions;
using EcommerceApiLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EcommerceApiLogic.Validators
{
    public class TokenValidationAction
    {
        public string Mensaje = "";
        private readonly IConfiguration configuration;

        public TokenValidationAction()
        {

        }

        public TokenValidationAction(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool Validation(int id, HttpContext httpContext, TokenValidationType tokenValidationType)
        {
            try
            {
                var currentUser = httpContext.User;

                if (!currentUser.HasClaim(c => c.Type == "IdCliente"))
                {
                    throw new DarkExceptionSystem("Error, el token ha sido destruido o no se ha generadó correctamente");
                }
                int IdCliente = Int32.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "IdCliente").Value);

                if (IdCliente == id)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No tienes autorización para esta información";
                    return false;
                }
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
                return false;
            }
        }

        public int GetIdClienteToken(HttpContext httpContext)
        {
            var currentUser = httpContext.User;

            if (!currentUser.HasClaim(c => c.Type == "IdCliente"))
            {
                throw new DarkExceptionSystem("Error, el token ha sido destruido o no se ha generadó correctamente");
            }
            int IdCliente = Int32.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "IdCliente").Value);
            return IdCliente;
        }

        public string GetTipoCliente(HttpContext httpContext)
        {
            var currentUser = httpContext.User;

            if (!currentUser.HasClaim(c => c.Type == "TipoCliente"))
            {
                throw new DarkExceptionSystem("Error, el token ha sido destruido o no se ha generadó correctamente");
            }
            string IdCliente = currentUser.Claims.FirstOrDefault(c => c.Type == "TipoCliente").Value;


            return IdCliente;
        }
        public string GetTipoClienteCard(HttpContext httpContext)
        {
            var currentUser = httpContext.User;

            if (!currentUser.HasClaim(c => c.Type == "CodigoCliente"))
            {
                throw new DarkExceptionSystem("Error, el token ha sido destruido o no se ha generadó correctamente");
            }
            string IdCliente = currentUser.Claims.FirstOrDefault(c => c.Type == "CodigoCliente").Value;


            return IdCliente;
        }

        public string GenerateToken(Usuario usuarioInfo)
        {
            if(usuarioInfo == null)
            {
                throw new DarkExceptionSystem("El objeto usuario esta vacio, no se puede generar token");
            }
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:FibremexKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, usuarioInfo.IdCliente.ToString()),
                new Claim("nombre", usuarioInfo.Nombre),
                new Claim("apellidos", usuarioInfo.Apellidos),
                new Claim("rol", "cliente"),
                new Claim("IdCliente", usuarioInfo.IdCliente.ToString()),
                new Claim("TipoCliente", usuarioInfo.TipoCliente),
                new Claim("CodigoCliente", usuarioInfo.CodigoCliente),
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            int MinutesExpired = Int32.Parse(configuration["Jwt:TokenExpirationInMinutes"]);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(MinutesExpired),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public enum TokenValidationType
    {
        Pedido = 1
    }
}
