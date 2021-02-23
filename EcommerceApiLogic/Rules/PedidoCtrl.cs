using EcommerceApiLogic.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Rules
{
    public class PedidoCtrl
    {
        private Usuario Cliente_re;
        public int IdCliente { get; set; }
        public DarkDev darkDev { get; internal set; }


        public PedidoCtrl(IConfiguration configuration)
        {
            darkDev = new DarkDev(configuration, DbManagerDark.DarkMode.Ambos);
            darkDev.OpenConnection();

            darkDev.LoadObject(MysqlObject.Usuario);

        }

        public PedidoCtrl(DarkDev DarkDev)
        {
            darkDev = DarkDev;


        }

        #region Metodoss
        /// <summary>
        /// Obtener lista de documentos en proceso
        /// </summary>
        /// <param name="CardCode"></param>
        /// <returns></returns>
        public List<ModelsSap.DocumentReportSap> GetEnProcesoHide(string CardCode = "")
        {
            var List = new List<ModelsSap.DocumentReportSap>();
            var Data = CardCode == "" ? darkDev.ConnectionSqlSever.GetDataReader($"exec [Eco_GetOrderInProcessEcommerceAll] @CardCode = 'get'") : darkDev.ConnectionSqlSever.GetDataReader($"exec [Eco_GetOrderInProcessEcommerce] @CardCode = '{CardCode}'");
            if (CardCode == "")
            {
                while (Data.Read())
                {
                    List.Add(new ModelsSap.DocumentReportSap
                    {
                        DocNum = Data.GetInt32(1) + "",
                        DocEntry = Data.GetInt32(2) + "",
                        DocDate = Data.GetDateTime(3),
                        CardCode = Data.GetString(0),
                        DocType = Data.GetString(5),
                        DocTotal = double.Parse(Data.GetDecimal(6) + ""),
                        DocNumEcommerce = Data.IsDBNull(4) ? "" : Data.GetInt32(4) + "",
                        DocCur = Data.GetString(7),
                        Status = Data.GetInt32(9) + "",
                        TrackNo = Data.IsDBNull(8) ? "" : Data.GetString(8) + "",
                        Cardname = Data.IsDBNull(10) ? "" : Data.GetString(10) + ""
                    });
                }
                Data.Close();
            }
            else
            {
                while (Data.Read())
                {
                    List.Add(new ModelsSap.DocumentReportSap
                    {
                        DocNum = Data.GetInt32(1) + "",
                        DocEntry = Data.GetInt32(2) + "",
                        DocDate = Data.GetDateTime(3),
                        CardCode = Data.GetString(0),
                        DocType = Data.GetString(5),
                        DocTotal = double.Parse(Data.GetDecimal(6) + ""),
                        DocNumEcommerce = Data.IsDBNull(4) ? "" : Data.GetInt32(4) + "",
                        DocCur = Data.GetString(7),
                        Status = Data.GetInt32(9) + "",
                        TrackNo = Data.IsDBNull(8) ? "" : Data.GetString(8) + "",
                        //Cardname = Data.IsDBNull(10) ? "" : Data.GetString(10) + ""
                    });
                }
                Data.Close();
            }
            return List;
        }
        /// <summary>
        /// Obtener historico de pedidos
        /// </summary>
        /// <param name="CardCode"></param>
        /// <returns></returns>
        public List<ModelsSap.DocumentReportSap> GetHistoricoHide(string CardCode = "")
        {
            var List = new List<ModelsSap.DocumentReportSap>();
            var Data = CardCode == "" ? darkDev.ConnectionSqlSever.GetDataReader($"exec [Eco_GetOrdersAll] @CardCode = 'get'") : darkDev.ConnectionSqlSever.GetDataReader($"exec [Eco_GetOrderInProcessEcommerce] @CardCode = '{CardCode}'");
            if (CardCode == "")
            {
                while (Data.Read())
                {
                    List.Add(new ModelsSap.DocumentReportSap
                    {
                        DocNum = Data.GetInt32(2) + "",
                        DocEntry = Data.GetInt32(3) + "",
                        DocDate = Data.GetDateTime(4),
                        CardCode = Data.GetString(0),
                        Cardname = Data.GetString(1),
                        DocType = Data.GetString(5),
                        DocCur = Data.GetString(6),
                        DocTotal = double.Parse(Data.GetDecimal(7) + ""),
                        DocNumEcommerce = Data.IsDBNull(8) ? "" : Data.GetInt32(8) + ""
                    });
                }
                Data.Close();
            }
            else
            {
                while (Data.Read())
                {
                    List.Add(new ModelsSap.DocumentReportSap
                    {
                        DocNum = Data.GetInt32(1) + "",
                        DocEntry = Data.GetInt32(2) + "",
                        DocDate = Data.GetDateTime(3),
                        CardCode = Data.GetString(0),
                        DocType = Data.GetString(5),
                        DocTotal = double.Parse(Data.GetDecimal(6) + ""),
                        DocNumEcommerce = Data.IsDBNull(4) ? "" : Data.GetInt32(4) + "",
                        DocCur = Data.GetString(7),
                        Status = Data.GetInt32(9) + "",
                        TrackNo = Data.IsDBNull(8) ? "" : Data.GetString(8) + "",
                        //Cardname = Data.IsDBNull(10) ? "" : Data.GetString(10) + ""
                    });
                }
                Data.Close();
            }
            return List;
        }
        /// <summary>
        /// Obtener lsta de documentos en rechazados
        /// </summary>
        /// <param name="CardCode"></param>
        /// <returns></returns>
        public List<ModelsSap.DocumentReportSap> GetRechazadosHide(string CardCode = "")
        {
            var List = new List<ModelsSap.DocumentReportSap>();
            var Data = CardCode == "" ? darkDev.ConnectionSqlSever.GetDataReader($"exec Eco_GetOrdersRejectedAll @DocDate = '2020-01-01'") : darkDev.ConnectionSqlSever.GetDataReader($"exec Eco_GetOrdersRejected @CardCode = '{CardCode}', @DocDate = '2020-01-01'");
            if (CardCode == "")
            {
                while (Data.Read())
                {
                    List.Add(new ModelsSap.DocumentReportSap
                    {
                        DocNum = Data.GetInt32(1) + "",
                        DocDate = DateTime.Parse(Data.GetString(2) + ""),
                        Status = Data.GetString(3),
                        DocType = Data.GetString(0),
                        Remarks = Data.IsDBNull(4) ? "" : Data.GetString(4),
                        DocNumEcommerce = Data.IsDBNull(5) ? "" : Data.GetInt32(5) + "",
                        CardCode = Data.IsDBNull(6) ? "" : Data.GetString(6) + "",
                        Cardname = Data.IsDBNull(7) ? "" : Data.GetString(7) + "",
                    });
                }
                Data.Close();
            }
            else
            {
                while (Data.Read())
                {
                    List.Add(new ModelsSap.DocumentReportSap
                    {
                        DocNum = Data.GetInt32(1) + "",
                        DocDate = DateTime.Parse(Data.GetString(2) + ""),
                        Status = Data.GetString(3),
                        DocType = Data.GetString(0),
                        Remarks = Data.IsDBNull(4) ? "" : Data.GetString(4),
                        DocNumEcommerce = Data.IsDBNull(5) ? "" : Data.GetInt32(5) + "",
                        CardCode = CardCode,
                        //Cardname = Data.IsDBNull(7) ? "" : Data.GetInt32(7) + "",
                    });
                }
                Data.Close();
            }
            return List;
        }

        /// <summary>
        /// libera objetos usados
        /// </summary>
        public void Terminar()
        {
            darkDev.CloseConnection();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
