using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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

        public static bool ValidSentence(string Value, string Regex) {
            bool errorStatus = false;
            Regex re = new Regex(Regex.Trim());
            if (!re.IsMatch(Value.Trim()))
                errorStatus = false;
            else
            {
                errorStatus = true;
            }
            return errorStatus;

        } 

        public static string ValidateEmail(string dataset)
        {
            string Malo = "";
            foreach (var a in GetEmails(dataset))
            {
                if (!IsValidEmail(a))
                {
                    Malo = a;
                    break;
                }
            }
            return Malo;
        }

        private static bool IsValidEmail(string email)
        {
            bool errorStatus = false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (!re.IsMatch(email.Trim()))
                errorStatus = false;
            else
            {
                errorStatus = true;
            }
            return errorStatus;
        }
        private static List<string> GetEmails(string dataset)
        {
            List<string> list = new List<string>();
            string[] allAddresses = dataset.Split(";,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string emailAddress in allAddresses)
            {
                list.Add(emailAddress);
            }

            return list;
        }
    }
}
