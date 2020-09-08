using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EcommerceApiLogic.Validators
{
    public static class Herramientas
    {
        public static void EscribeLogError(string mensaje)
        {
            try
            {
                if (!Directory.Exists("C:\\Splittel\\EcommerceApi\\Log\\Tokens\\"))
                {
                    Directory.CreateDirectory("C:\\Splittel\\EcommerceApi\\Log\\Tokens\\");
                }

                StreamWriter sw = new StreamWriter("C:\\Splittel\\EcommerceApi\\Log\\Tokens\\Log_" + DateTime.Today.ToString("MM_dd_yyyy") + ".log", true);
                sw.WriteLine(DateTime.Now.ToString() + " - " + mensaje);
                sw.Close();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
    }
}
