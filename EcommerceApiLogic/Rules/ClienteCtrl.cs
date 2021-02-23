using DbManagerDark.Exceptions;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.Resquest;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EcommerceApiLogic.Rules
{
    public class ClienteCtrl
    {
        #region Atributos
        private DarkDev darkDev;
        private Usuario Cliente_re;
        public int IdCliente { get; set; }
        public DarkDev DarkDev { get { return darkDev; } }
        private readonly IConfiguration configuration;
        public List<ExtrasValidation> Extras { get; internal set; }
        #endregion

        #region Constructor
        public ClienteCtrl(IConfiguration configuration)
        {
            this.configuration = configuration;
            darkDev = new DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();

            darkDev.LoadObject(MysqlObject.Usuario);
            darkDev.LoadObject(MysqlObject.Cliente);

        }
        
        public Responses.SplittelRespData<TokenInformation> RegisterNewB2C(RegisterB2C registerB2C)
        {
            try
            {
                if(registerB2C.Password != registerB2C.PasswordConfirm)
                {
                    throw new DarkException { 
                        Code = 100,
                        Mensaje = "Tus contraseñas no coinciden",
                        TipoErro = TypeError.User,
                        TipoMensaje = TypeException.Alerta,
                        IdAux = ""
                    };
                }

                var Result = darkDev.Usuario.GetOpenquerys($"Where email = '{registerB2C.Email}'");

                if (Result != null)
                {
                    throw new DarkException
                    {
                        Code = 130,
                        Mensaje = $"Ya existe una cuenta registrada con el correo {registerB2C.Email}",
                        TipoErro = TypeError.User,
                        TipoMensaje = TypeException.Alerta,
                        IdAux = ""
                    };
                }
                Extras = new List<ExtrasValidation> {
                    new ExtrasValidation { Color = Validators.Herramientas.ValidSentence(registerB2C.Password,@"(?=.*?[A-Z])") ? "success": "danger", Descripcion = "al menos 1 letra mayúscula" },
                    new ExtrasValidation { Color = Validators.Herramientas.ValidSentence(registerB2C.Password,@"(?=.*?[a-z])") ? "success": "danger", Descripcion = "al menos 1 letra minúscula" },
                    new ExtrasValidation { Color = Validators.Herramientas.ValidSentence(registerB2C.Password,@"(?=.*?[0-9])") ? "success": "danger", Descripcion = "al menos 1 carácter numérico" },
                    new ExtrasValidation { Color = Validators.Herramientas.ValidSentence(registerB2C.Password,@"(?=.*?[^\w\s])") ? "success": "danger", Descripcion = "al menos 1 carácter especial" },
                    new ExtrasValidation { Color = Validators.Herramientas.ValidSentence(registerB2C.Password,@".{8,}$") ? "success": "danger", Descripcion = "al menos 8 caracteres" }

                };

                if (Extras.Count(a => a.Color == "danger") > 0)
                {
                    throw new DarkException
                    {
                        Code = 110,
                        Mensaje = "Tus contraseña no cumple con la seguridad requerida",
                        TipoErro = TypeError.User,
                        TipoMensaje = TypeException.Alerta,
                        IdAux = ""
                    };

                }

                Cliente NewCliente = new Cliente();
                NewCliente.Nombre = registerB2C.Nombre;
                NewCliente.Apellidos = registerB2C.Apellidos;
                NewCliente.Telefono = registerB2C.Telefono;
                NewCliente.Email = registerB2C.Email;
                NewCliente.Password = Validators.EncryptData.Encrypt("password", registerB2C.Password);
                NewCliente.Registro = DateTime.Now;
                NewCliente.UltimoLogin = DateTime.Now;
                NewCliente.Activo = "si";
                NewCliente.TipoCliente = "B2C";
                NewCliente.CodigoCliente = "";
                NewCliente.Sociedad = "";
                NewCliente.Pass_b2b = "";
                //NewCliente.Ingreso = DateTime.Parse();
                //NewCliente.Groupcode =
                //NewCliente.Segmento =
                //NewCliente.Update_pass =
                //NewCliente.Email_ejecutivo =
                NewCliente.Descuento = 0;
                //NewCliente.Dias_credito =

                darkDev.Cliente.Element = NewCliente;
                if (!darkDev.Cliente.Add(true))
                {
                    throw new DarkException
                    {
                        Code = 110,
                        Mensaje = "No fue registrada tu información",
                        TipoErro = TypeError.User,
                        TipoMensaje = TypeException.Error,
                        IdAux = ""
                    };
                }
                Result = darkDev.Usuario.GetOpenquerys($"Where email = '{registerB2C.Email}'");
                return new Responses.SplittelRespData<TokenInformation>
                {
                    Code = 0,
                    Data = new TokenInformation
                    {
                        Token = darkDev.tokenValidationAction.GenerateToken(Result),
                        Expiration = DateTime.Now.AddMinutes(Int32.Parse(configuration["Jwt:TokenExpirationInMinutes"])),
                        TipoCliente = Result.TipoCliente,
                        NombreCliente = Result.Nombre,
                    },
                    Message = "Datos correctos, Bienvenido a E-commerce Splittel"
                };
            }
            catch (DarkException ex)
            {
                throw ex;
            }
        }


        public Responses.SplittelRespData<TokenInformation> ValidateLogin(Login Login)
        {
            var Result = darkDev.Usuario.GetOpenquerys($"Where email = '{Login.User}'");

            if(Result is null)
            {
                throw new DarkException
                {
                    Code = 110,
                    Mensaje = "No existe ningun usuario registrado con el correo dado",
                    TipoErro = TypeError.User,
                    TipoMensaje = TypeException.Alerta,
                    IdAux = ""
                };
            }
            string pass_real = Validators.EncryptData.Decrypt("password", Result.Password);
            if (pass_real.Trim() == Login.Password.Trim())
            {
                return new Responses.SplittelRespData<TokenInformation>
                { 
                    Code = 0,
                    Data = new TokenInformation {
                        Token = darkDev.tokenValidationAction.GenerateToken(Result),
                        Expiration = DateTime.Now.AddDays(Int32.Parse(configuration["Jwt:TokenExpirationInMinutes"])),
                        TipoCliente = Result.TipoCliente,
                        NombreCliente = Result.Nombre,
                    },
                    Message = "Datos correctos, Bienvenido a E-commerce Splittel"
                };
            }
            else
            {
                return new Responses.SplittelRespData<TokenInformation>
                {
                    Code = 200,
                    Message = "Datos correctos, Bienvenido a E-commerce Splittel"
                };
            }
        }
        /// <summary>
        /// Terminar controller
        /// </summary>
        public void Finish()
        {
            darkDev.CloseConnection();
        }
        #endregion
    }
}
