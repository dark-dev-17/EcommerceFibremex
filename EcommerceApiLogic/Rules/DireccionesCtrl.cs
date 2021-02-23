using DbManagerDark.Exceptions;
using EcommerceApiLogic.Catalogos;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ModelsSap;
using EcommerceApiLogic.Resquest;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EcommerceApiLogic.Rules
{
    public class DireccionesCtrl
    {
        private DarkDev darkDev;
        private Usuario Cliente_re;
        public int IdCliente { get; set; }
        public DarkDev DarkDev { get { return darkDev; } }


        public DireccionesCtrl(IConfiguration configuration) 
        {
            darkDev = new DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();

            darkDev.LoadObject(MysqlObject.Usuario);
            darkDev.LoadObject(MysqlObject.DireccionEnvio);
            darkDev.LoadObject(MysqlObject.DireccionFacturacion);

            darkDev.LoadObject(SSQLObject.DireccionPedido);
        }

        #region Cliente B2B

        #region Envio
        /// <summary>
        /// Obtiene direcciones de envio del cliente B2B
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DireccionPedido> GetB2B_dirEnv()
        {
            GetUsuarioEcommerce("B2B");
            return darkDev.DireccionPedido.GetSpecialStat(string.Format("exec Eco_GetAddressByCustomer @CardCode = '{0}', @AdresType = 'S'", Cliente_re.CodigoCliente.Trim())).OrderBy(a => a.IsDefault);
        }
        /// <summary>
        /// Obtiene direccion de envio por default del cliente B2B
        /// </summary>
        /// <returns></returns>
        public DireccionPedido GetB2B_envioDef()
        {
            GetUsuarioEcommerce("B2B");
            return darkDev.DireccionPedido.GetSpecialStat(string.Format("exec Eco_GetAddressByCustomer @CardCode = '{0}', @AdresType = 'S'", Cliente_re.CodigoCliente.Trim())).Find(a => a.Default == "default");
        }
        /// <summary>
        /// Obtiene direccion de envio de acuerdo al nombre enviado como parametro del cliente B2B
        /// </summary>
        /// <param name="NombreDir"></param>
        /// <returns></returns>
        public DireccionPedido GetB2B_envio(string NombreDir)
        {
            GetUsuarioEcommerce("B2B");
            return darkDev.DireccionPedido.GetSpecialStat(string.Format("exec Eco_GetAddressByCustomer @CardCode = '{0}', @AdresType = 'S'", Cliente_re.CodigoCliente.Trim())).Find(a => a.Nombre == NombreDir);
        }
        #endregion

        #region facturacion
        /// <summary>
        /// Obtiene direcciones de facturacion del cliente B2B
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DireccionPedido> GetB2B_dirFact()
        {
            GetUsuarioEcommerce("B2B");
            return darkDev.DireccionPedido.GetSpecialStat(string.Format("exec Eco_GetAddressByCustomer @CardCode = '{0}', @AdresType = 'B'", Cliente_re.CodigoCliente.Trim())).OrderBy(a => a.IsDefault);
        }
        /// <summary>
        /// Obtiene direccion de facturacion por default del cliente B2B
        /// </summary>
        /// <returns></returns>
        public DireccionPedido GetB2B_factDef()
        {
            GetUsuarioEcommerce("B2B");
            return darkDev.DireccionPedido.GetSpecialStat(string.Format("exec Eco_GetAddressByCustomer @CardCode = '{0}', @AdresType = 'B'", Cliente_re.CodigoCliente.Trim())).Find(a => a.Default == "default");
        }
        /// <summary>
        /// obtiene direccion de facruracion  de acuerdo al nombre enviado como parametro del cliente B2B
        /// </summary>
        /// <param name="NombreDir"></param>
        /// <returns></returns>
        public DireccionPedido GetB2B_fact(string NombreDir)
        {

            GetUsuarioEcommerce("B2B");
            return darkDev.DireccionPedido.GetSpecialStat(string.Format("exec Eco_GetAddressByCustomer @CardCode = '{0}', @AdresType = 'B'", Cliente_re.CodigoCliente.Trim())).Find(a => a.Nombre == NombreDir);
        }
        #endregion

        public void AddDireccion(DireccionEnvFac DireccionEnvFac)
        {

            if(DireccionEnvFac is null)
            {
                throw new DarkExceptionUser("Los datos no son validos o son nulos");
            }
            if(string.IsNullOrEmpty(DireccionEnvFac.TipoDireccion) || DireccionEnvFac.TipoDireccion.Trim() != "B" && DireccionEnvFac.TipoDireccion.Trim() != "S")
                throw new DarkExceptionUser($"El tipo de dirección '{DireccionEnvFac.TipoDireccion}' no es valida");

            if(new Pais().Estados.Find(a => a.Value == DireccionEnvFac.Estado.Trim()) == null)
            {
                throw new DarkExceptionUser($"No es valido el estado '{DireccionEnvFac.Estado.Trim()}'");
            }

            GetUsuarioEcommerce("B2B");

            if(DireccionEnvFac.TipoDireccion.Trim() == "B")
                if(GetB2B_dirFact().Count(a => a.Nombre == DireccionEnvFac.NombreDireccion.Trim()) >= 1)
                    throw new DarkExceptionUser($"Ya existe una direccion de facturacion con el nombre '{DireccionEnvFac.NombreDireccion.Trim()}'");
            
            if (DireccionEnvFac.TipoDireccion.Trim() == "S")
                if (GetB2B_dirEnv().Count(a => a.Nombre == DireccionEnvFac.NombreDireccion.Trim()) >= 1)
                    throw new DarkExceptionUser($"Ya existe una direccion de envio con el nombre '{DireccionEnvFac.NombreDireccion.Trim()}'");

            WS_BussinesPartner.WS_BussinesPartnerSoapClient.EndpointConfiguration endpoint = new WS_BussinesPartner.WS_BussinesPartnerSoapClient.EndpointConfiguration();
            WS_BussinesPartner.WS_BussinesPartnerSoapClient wS_BussinesPartnerSoap = new WS_BussinesPartner.WS_BussinesPartnerSoapClient(endpoint);
            var responseString = wS_BussinesPartnerSoap.AddNewAddressBussinesPartnerAsync(
                new WS_BussinesPartner.Usuario
                {
                    Password = new Validators.EncriptDataEcomUsers(Cliente_re.CodigoCliente.Trim()).Encrypt(Cliente_re.PasswordB2B.Trim()),
                    Society = "FIBREMEX",
                    UserKey = Cliente_re.CodigoCliente.Trim()
                },
                new WS_BussinesPartner.BussinessPartnerAdresses
                {
                    Street = DireccionEnvFac.Calle,
                    StreetNo = DireccionEnvFac.NumeroExterior,
                    Block = DireccionEnvFac.Colonia,
                    County = DireccionEnvFac.Municipio,
                    ZipCode = DireccionEnvFac.CP,
                    State = DireccionEnvFac.Estado,
                    City = DireccionEnvFac.Ciudad,
                    AddressName = DireccionEnvFac.NombreDireccion,
                    AddressType = DireccionEnvFac.TipoDireccion == "B" ? "BillTo" : "ShipTo",
                    Default = DireccionEnvFac.Default,
                }
            ).Result;

            if(responseString.AddNewAddressBussinesPartnerResult.ErrorCode != 0)
            {
                throw new DarkExceptionUser("Error al agregar la dirección");
            }

        }
        public void UpdDireccion(DireccionEnvFac DireccionEnvFac)
        {

            if (DireccionEnvFac is null)
            {
                throw new DarkExceptionUser("Los datos no son validos o son nulos");
            }
            if (string.IsNullOrEmpty(DireccionEnvFac.TipoDireccion) || DireccionEnvFac.TipoDireccion.Trim() != "B" && DireccionEnvFac.TipoDireccion.Trim() != "S")
                throw new DarkExceptionUser($"El tipo de dirección '{DireccionEnvFac.TipoDireccion}' no es valida");

            if (new Pais().Estados.Find(a => a.Value == DireccionEnvFac.Estado.Trim()) == null)
            {
                throw new DarkExceptionUser($"No es valido el estado '{DireccionEnvFac.Estado.Trim()}'");
            }

            GetUsuarioEcommerce("B2B");
            string TipoDireccion = "";
            if (DireccionEnvFac.TipoDireccion.Trim() == "B")
            { 
                var data = GetB2B_dirFact().ToList();
                if (data.Count(a => a.Nombre == DireccionEnvFac.NombreDireccion.Trim()) != 1)
                    throw new DarkExceptionSystem($"No existe una direccion de facturacion con el nombre '{DireccionEnvFac.NombreDireccion.Trim()}'");


                //TipoDireccion = data.Find(a => a.Nombre == DireccionEnvFac.NombreDireccion.Trim()).Nombre;
            }
                

            if (DireccionEnvFac.TipoDireccion.Trim() == "S")
            {
                var data = GetB2B_dirEnv().ToList();
                if (data.Count(a => a.Nombre == DireccionEnvFac.NombreDireccion.Trim()) != 1)
                    throw new DarkExceptionSystem($"No existe una direccion de envio con el nombre '{DireccionEnvFac.NombreDireccion.Trim()}'");

                //TipoDireccion = data.Find(a => a.Nombre == DireccionEnvFac.NombreDireccion.Trim()).;
            }
                

            WS_BussinesPartner.WS_BussinesPartnerSoapClient.EndpointConfiguration endpoint = new WS_BussinesPartner.WS_BussinesPartnerSoapClient.EndpointConfiguration();
            WS_BussinesPartner.WS_BussinesPartnerSoapClient wS_BussinesPartnerSoap = new WS_BussinesPartner.WS_BussinesPartnerSoapClient(endpoint);
            var responseString = wS_BussinesPartnerSoap.UpdateAddressBussinesPartnerAsync(
                new WS_BussinesPartner.Usuario
                {
                    Password = new Validators.EncriptDataEcomUsers(Cliente_re.CodigoCliente.Trim()).Encrypt(Cliente_re.PasswordB2B.Trim()),
                    Society = "FIBREMEX",
                    UserKey = Cliente_re.CodigoCliente.Trim()
                },
                new WS_BussinesPartner.BussinessPartnerAdresses
                {
                    Street = DireccionEnvFac.Calle,
                    StreetNo = DireccionEnvFac.NumeroExterior,
                    Block = DireccionEnvFac.Colonia,
                    County = DireccionEnvFac.Municipio,
                    ZipCode = DireccionEnvFac.CP,
                    State = DireccionEnvFac.Estado,
                    City = DireccionEnvFac.Ciudad,
                    AddressName = DireccionEnvFac.NombreDireccion,
                    AddressType = DireccionEnvFac.TipoDireccion == "B" ? "BillTo": "ShipTo",
                    Default = DireccionEnvFac.Default,
                }
            ).Result;

            if (responseString.UpdateAddressBussinesPartnerResult.ErrorCode != 0)
            {
                throw new DarkExceptionUser("Error al agregar la dirección");
            }
        }

        #endregion

        #region Cliente B2C

        #region envio
        /// <summary>
        /// Obtener direcciones de envio para clientes B2C
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DireccionEnvio> GetB2C_dirEnv()
        {
            var direcciones_re = darkDev.DireccionEnvio.Get("" + this.IdCliente, darkDev.DireccionEnvio.ColumName(nameof(darkDev.DireccionEnvio.Element.IdCliente)));
            return direcciones_re.Where(a => a.Activo == "si").ToList();
        }
        /// <summary>
        /// Obtener direccion de envio default
        /// </summary>
        /// <returns></returns>
        public DireccionEnvio GetB2C_envioDef()
        {
            int IdDireccion = darkDev.DireccionEnvio.GetLastId(darkDev.DireccionEnvio.ColumName(nameof(darkDev.DireccionEnvio.Element.IdCliente)), "" + this.IdCliente);
            var Direccion_Re = darkDev.DireccionEnvio.Get(IdDireccion);
            if (Direccion_Re == null)
                return null;
            return Direccion_Re.Activo == "si" ? Direccion_Re : null;
        }

        /// <summary>
        /// Obtener direccion envio seleccionada
        /// </summary>
        /// <param name="IdDireccionenvio"></param>
        /// <returns></returns>
        public DireccionEnvio GetB2C_envio(int IdDireccionenvio)
        {
            var Direccion_Re = darkDev.DireccionEnvio.Get(IdDireccionenvio);
            if (Direccion_Re == null)
                return null;
            return Direccion_Re.IdCliente == this.IdCliente && Direccion_Re.Activo == "si" ? Direccion_Re : null ;
        }
        /// <summary>
        /// Actualizar direccion de envio
        /// </summary>
        /// <param name="DireccionEnvio"></param>
        public void UpdB2C_envio(DireccionEnvio DireccionEnvio)
        {
            darkDev.StartTransaction();
            try
            {
                var Direccion_re = darkDev.DireccionEnvio.Get(DireccionEnvio.IdDireccionEnvio);

                if (DireccionEnvio == null)
                    throw new DarkExceptionUser("Los datos no son validos o son nulos");

                if (Direccion_re == null)
                    throw new DarkExceptionUser("La dirección requerida no fue encontrada");

                if (Direccion_re.IdCliente != this.IdCliente)
                    throw new DarkExceptionUser("No puedes actualizar esta direccion");

                darkDev.DireccionEnvio.Element = DireccionEnvio;
                darkDev.DireccionEnvio.Element.IdCliente = this.IdCliente;
                if (!darkDev.DireccionEnvio.Update())
                {
                    throw new DarkExceptionUser("Error al actualizar dirección de envio");
                }

                darkDev.Commit();
            }
            catch (Exception)
            {
                darkDev.RolBack();
                throw;
            }
        }
        /// <summary>
        /// agregar direccion de envio
        /// </summary>
        /// <param name="DireccionEnvio"></param>
        public void AddB2C_envio(DireccionEnvio DireccionEnvio)
        {
            darkDev.StartTransaction();
            try
            {

                if (DireccionEnvio == null)
                    throw new DarkExceptionUser("Los datos no son validos o son nulos");

                darkDev.DireccionEnvio.Element = DireccionEnvio;
                darkDev.DireccionEnvio.Element.IdCliente = this.IdCliente;
                if (!darkDev.DireccionEnvio.Add())
                {
                    throw new DarkExceptionUser("Error al registrar la dirección de envio");
                }

                darkDev.Commit();
            }
            catch (Exception)
            {
                darkDev.RolBack();
                throw;
            }
        }
        #endregion

        #region facturacion
        /// <summary>
        /// obtiene direcciones de facturacion por default
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DireccionFacturacion> GetB2C_dirFact()
        {
            var direcciones_re = darkDev.DireccionFacturacion.Get("" + this.IdCliente, darkDev.DireccionFacturacion.ColumName(nameof(darkDev.DireccionFacturacion.Element.IdCliente)));
            return direcciones_re.Where(a => a.Activo == "si").ToList();
        }
        /// <summary>
        /// Obtiene direccion de facturacion por default
        /// </summary>
        /// <returns></returns>
        public DireccionFacturacion GetB2C_factDef()
        {
            int IdDireccion = darkDev.DireccionFacturacion.GetLastId(darkDev.DireccionFacturacion.ColumName(nameof(darkDev.DireccionFacturacion.Element.IdCliente)), "" + this.IdCliente);
            var Direccion_Re = darkDev.DireccionFacturacion.Get(IdDireccion);
            if (Direccion_Re == null)
                return null;
            return  Direccion_Re.Activo == "si" ? Direccion_Re : null;
        }
        /// <summary>
        /// Obtener direccion facturacion seleccionada
        /// </summary>
        /// <param name="IdDireccionFacturacion"></param>
        /// <returns></returns>
        public DireccionFacturacion GetB2C_facturacion(int IdDireccionFacturacion)
        {
            var Direccion_Re = darkDev.DireccionFacturacion.Get(IdDireccionFacturacion);

            if (Direccion_Re == null)
                return null;

            return Direccion_Re.IdCliente == this.IdCliente && Direccion_Re.Activo == "si" ? Direccion_Re : null;
        }
        /// <summary>
        /// Actualizar direccion de facturacion
        /// </summary>
        /// <param name="DireccionFacturacion"></param>
        public void UpdB2C_fact(DireccionFacturacion DireccionFacturacion)
        {
            darkDev.StartTransaction();
            try
            {
                var Direccion_re = darkDev.DireccionFacturacion.Get(DireccionFacturacion.IdDireccionFacturacion);

                if (DireccionFacturacion == null)
                    throw new DarkExceptionUser("Los datos no son validos o son nulos");

                if (Direccion_re == null)
                    throw new DarkExceptionUser("La dirección requerida no fue encontrada");

                if (Direccion_re.IdCliente != this.IdCliente)
                    throw new DarkExceptionUser("No puedes actualizar esta direccion");

                darkDev.DireccionFacturacion.Element = DireccionFacturacion;
                darkDev.DireccionFacturacion.Element.IdCliente = this.IdCliente;
                if (!darkDev.DireccionFacturacion.Update())
                {
                    throw new DarkExceptionUser("Error al actualizar dirección de facturación");
                }

                darkDev.Commit();
            }
            catch (Exception)
            {
                darkDev.RolBack();
                throw;
            }
        }
        /// <summary>
        /// Agregar direccion de facturacion
        /// </summary>
        /// <param name="DireccionFacturacion"></param>
        public void AddB2C_fact(DireccionFacturacion DireccionFacturacion)
        {
            darkDev.StartTransaction();
            try
            {

                if (DireccionFacturacion == null)
                    throw new DarkExceptionUser("Los datos no son validos o son nulos");

                darkDev.DireccionFacturacion.Element = DireccionFacturacion;
                darkDev.DireccionFacturacion.Element.IdCliente = this.IdCliente;
                if (!darkDev.DireccionFacturacion.Add())
                {
                    throw new DarkExceptionUser("Error al registrar dirección de facturación");
                }

                darkDev.Commit();
            }
            catch (Exception)
            {
                darkDev.RolBack();
                throw;
            }
        }
        #endregion

        #endregion

        #region Generales
        public void Finish()
        {
            darkDev.CloseConnection();
        }
        private  void GetUsuarioEcommerce(string TypeBp)
        {
            if(Cliente_re == null || Cliente_re != null && Cliente_re.IdCliente != this.IdCliente)
            {
                Cliente_re = darkDev.Usuario.Get(this.IdCliente);
                if (Cliente_re == null)
                {
                    throw new DarkExceptionUser("Cliente no encontrado");
                }
            }

            if (TypeBp.Trim() != Cliente_re.TipoCliente.Trim())
            {
                throw new DarkExceptionUser("No puedes extraer información de esta seccion, solo aplica para clientes " + TypeBp);
            }
        }
        #endregion


    }
}
