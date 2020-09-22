using DbManagerDark.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace EcommerceApiLogic.Herramientas
{
    public class FtpFiles
    {
        private string Server { set; get; }
        private string User { set; get; }
        private string Password { set; get; }

        public FtpFiles(string Server, string User, string Password)
        {
            this.Server = Server;
            this.User = User;
            this.Password = Password;
        }

        public List<string> GetFiles(string Path)
        {
            string Uri = "ftp://" + Server + Path;

            Stream responseStream = null;
            StreamReader reader = null;
            FtpWebResponse response = null;
            //StreamWriter writeStream = null;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(Uri));
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(User, Password);
                response = (FtpWebResponse)request.GetResponse();
                List<string> lista = new List<string>();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                while (reader.Peek() >= 0)
                {
                    string nameFile = reader.ReadLine();
                    lista.Add(nameFile);
                }
                return lista;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    throw new DarkExceptionSystem(string.Format("No se encontraron archivos"));
                else
                    throw new DarkExceptionSystem(string.Format("WebException FTP - {0}", ex.Message));
            }
            catch (Exception ex)
            {
                throw new DarkExceptionSystem(string.Format("Exception FTP - {0}", ex.Message));
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
        }
    }
}
