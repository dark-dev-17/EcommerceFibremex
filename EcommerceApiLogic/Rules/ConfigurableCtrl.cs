using EcommerceApiLogic.Models;
using EcommerceApiLogic.Resquest;
using EcommerceApiLogic.Responses;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Rules
{
    public class ConfigurableCtrl
    {
        #region Atributos
        private DarkDev darkDev;
        private Usuario Cliente_re;
        public int IdCliente { get; set; }
        public DarkDev DarkDev { get { return darkDev; } }
        #endregion

        #region Constructor
        public ConfigurableCtrl(IConfiguration configuration)
        {
            darkDev = new DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();

            darkDev.LoadObject(MysqlObject.Usuario);
            darkDev.LoadObject(MysqlObject.Configu_nombre);

        }
        public SplittelRespData<Configu_nombre> Crear(ConfNombre ConfNombre)
        {
            var Data_re = darkDev.Configu_nombre.GetOpenquerys($"WHERE t17_f001 = '{ConfNombre.Codigo}' AND t17_f002 = '{ConfNombre.CodCodfigurable}'");

            if (Data_re is null)
            {
                return new SplittelRespData<Configu_nombre>
                {
                    Code = 0,
                    Message = "Ya existe un nombre para el configurable",
                    Data = Data_re
                };
            }
            else
            {
                Configu_nombre configu_Nombre = new Configu_nombre
                {
                    Codigo = ConfNombre.Codigo,
                    CodCodfigurable = ConfNombre.CodCodfigurable,
                    Descripcion = ConfNombre.Descripcion,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                };

                darkDev.Configu_nombre.Element = configu_Nombre;
                if (!darkDev.Configu_nombre.Add())
                {
                    return new SplittelRespData<Configu_nombre>
                    {
                        Code = 100,
                        Message = "Error, el nombre no pudo guardarse",
                        Data = Data_re
                    };
                }

                return new SplittelRespData<Configu_nombre>
                {
                    Code = 0,
                    Message = "Guardado con exitoso!!",
                    Data = configu_Nombre
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
