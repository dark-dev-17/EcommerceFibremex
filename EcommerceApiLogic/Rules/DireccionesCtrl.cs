using DbManagerDark.Exceptions;
using EcommerceApiLogic.Models;
using EcommerceApiLogic.ModelsSap;
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
            return darkDev.DireccionPedido.GetSpecialStat(string.Format("exec Eco_GetAddressByCustomer @CardCode = '{0}', @AdresType = 'S'", Cliente_re.CodigoCliente.Trim())).Find( a => a.Default == "default");
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

        #region Clinete B2C

        #region envio
        public IEnumerable<DireccionEnvio> GetB2C_dirEnv()
        {
            var direcciones_re = darkDev.DireccionEnvio.Get("" + this.IdCliente, darkDev.DireccionEnvio.ColumName(nameof(darkDev.DireccionEnvio.Element.IdCliente)));
            return direcciones_re;
        }

        public DireccionEnvio GetB2C_envioDef()
        {
            int IdDireccion = darkDev.DireccionEnvio.GetLastId(darkDev.DireccionEnvio.ColumName(nameof(darkDev.DireccionEnvio.Element.IdCliente)), "" + this.IdCliente);
            var Direccion_Re = darkDev.DireccionEnvio.Get(IdDireccion);

            return Direccion_Re;
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
            return Direccion_Re.IdCliente == this.IdCliente ? Direccion_Re : null ;
        }
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
            return direcciones_re;
        }
        /// <summary>
        /// Obtiene direccion de facturacion por default
        /// </summary>
        /// <returns></returns>
        public DireccionFacturacion GetB2C_factDef()
        {
            int IdDireccion = darkDev.DireccionFacturacion.GetLastId(darkDev.DireccionFacturacion.ColumName(nameof(darkDev.DireccionFacturacion.Element.IdCliente)), "" + this.IdCliente);
            var Direccion_Re = darkDev.DireccionFacturacion.Get(IdDireccion);

            return Direccion_Re;
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

            return Direccion_Re.IdCliente == this.IdCliente ? Direccion_Re : null;
        }

        public void UpdB2C_fact(DireccionFacturacion DireccionFacturacion)
        {
            darkDev.StartTransaction();
            try
            {
                var Direccion_re = darkDev.DireccionEnvio.Get(DireccionFacturacion.IdDireccionFacturacion);

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
