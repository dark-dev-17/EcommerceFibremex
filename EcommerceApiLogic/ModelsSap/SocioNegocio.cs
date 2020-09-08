using DbManagerDark.DbManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EcommerceApiLogic.ModelsSap
{
    public class SocioNegocio
    {
        public string CardCode { set; get; }
        public string CardName { set; get; }
        public string ExtraDays { set; get; }
        public string DescriptPayment { set; get; }
        public double CreditLine { set; get; }
        public double Balance { set; get; }
        public string Phone2 { set; get; }
        public string E_Mail { set; get; }
        public string E_MailL_invoice { set; get; }
        public string E_MailL_account { set; get; }
        public string SlpName { set; get; }
        public string Email_employeSales { set; get; }
        public string Section { set; get; }
        public string MonexUSD { set; get; }
        public string MonexMXP { set; get; }
        public string Currency { set; get; }

        protected DarkConnectionSqlSever ConnectionSqlSever;

        public SocioNegocio(DarkConnectionSqlSever ConnectionSqlSever)
        {
            this.ConnectionSqlSever = ConnectionSqlSever;
        }

        public SocioNegocio Get(string CardCode)
        {
            string sqlStatement = string.Format("EXEC Eco_GetInfoGeneralCustomer @CardCode = '{0}'", CardCode);
            DataTable data = ConnectionSqlSever.GetData(sqlStatement);
            if (data.Rows.Count <= 0)
            {
                return null;
            }
            this.CardCode = data.Rows[0].ItemArray[0].ToString();
            CardName = data.Rows[0].ItemArray[1].ToString();
            ExtraDays = data.Rows[0].ItemArray[2].ToString();
            DescriptPayment = data.Rows[0].ItemArray[3].ToString();
            CreditLine = double.Parse(data.Rows[0].ItemArray[4].ToString());
            Balance = double.Parse(data.Rows[0].ItemArray[5].ToString());
            Phone2 = data.Rows[0].ItemArray[6].ToString();
            E_Mail = data.Rows[0].ItemArray[7].ToString();
            E_MailL_invoice = data.Rows[0].ItemArray[8].ToString();
            E_MailL_account = data.Rows[0].ItemArray[9].ToString();
            SlpName = data.Rows[0].ItemArray[10].ToString();
            Email_employeSales = data.Rows[0].ItemArray[11].ToString();
            Section = data.Rows[0].ItemArray[12].ToString();
            MonexUSD = data.Rows[0].ItemArray[13].ToString();
            MonexMXP = data.Rows[0].ItemArray[14].ToString();
            Currency = data.Rows[0].ItemArray[15].ToString();
            data.Dispose();
            return this;
        }

    }
}
