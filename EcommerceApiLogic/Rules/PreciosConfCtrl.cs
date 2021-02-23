using EcommerceApiLogic.Models;
using EcommerceApiLogic.Resquest;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Rules
{
    public class PreciosConfCtrl
    {
        private DarkDev darkDev;
        private Usuario Cliente_re;
        public int IdCliente { get; set; }
        public DarkDev DarkDev { get { return darkDev; } }


        public PreciosConfCtrl(IConfiguration configuration)
        {
            darkDev = new DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();
        }

        public string PigtailCal(PigtailPrecioCal pigtailPrecioCal)
        {
            darkDev.GetDarkConnectionMySQL().SimpleProcedure("PrecioPigtail", new List<DbManagerDark.DbManager.ProcedureModel>()
            {
                new DbManagerDark.DbManager.ProcedureModel { Namefield = "Longitud_" ,value = pigtailPrecioCal.Longitud_},
                new DbManagerDark.DbManager.ProcedureModel { Namefield = "NumeroHilos_" ,value = pigtailPrecioCal.NumeroHilos},
                new DbManagerDark.DbManager.ProcedureModel { Namefield = "Conector_" ,value = pigtailPrecioCal.Conector_},
                new DbManagerDark.DbManager.ProcedureModel { Namefield = "Fibra_" ,value = pigtailPrecioCal.Fibra_},
                new DbManagerDark.DbManager.ProcedureModel { Namefield = "Diametro_" ,value = pigtailPrecioCal.Diametro_},
                new DbManagerDark.DbManager.ProcedureModel { Namefield = "Pulido_" ,value = pigtailPrecioCal.Pulido_},
                new DbManagerDark.DbManager.ProcedureModel { Namefield = "Codigo_" ,value = pigtailPrecioCal.Codigo_},
                new DbManagerDark.DbManager.ProcedureModel { Namefield = "SubcategoriesN1Code" ,value = pigtailPrecioCal.SubCategoriaN1Code},
                new DbManagerDark.DbManager.ProcedureModel { Namefield = "ClientId" ,value = pigtailPrecioCal.ClientId},
            });
            return darkDev.GetDarkConnectionMySQL().RESULT;
        }
        public void Finish()
        {
            darkDev.CloseConnection();
        }
    }
}
