using DbManagerDark.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace EcommerceApiLogic.Herramientas
{
    public class FtpFiles
    {
        private string Server { set; get; }
        private string User { set; get; }
        private string Password { set; get; }
        public string Dominio { get; internal set; }
        /// <summary>
        /// url del sitio https://fibremex.com/fibra-optica/public/
        /// </summary>
        public string Site { get; internal set; }
        public string FtpSitebase { get; internal set; }
        public FtpFiles(string Server, string User, string Password, string Dominio, string Site, string FtpSitebase)
        {
            this.Server = Server;
            this.User = User;
            this.Password = Password;
            this.Dominio = Dominio;
            this.Site = Site;
            this.FtpSitebase = FtpSitebase;
        }
        [Obsolete]
        public List<FileFtp> GetFiles(string pattern, string publicRoute, string Permit = "")
        {
            string Uri = "ftp://" + Server + pattern;
            //var dataGet = ListDirectory(pattern, false);
            //Stream responseStream = null;
            //StreamReader reader = null;
            //FtpWebResponse response = null;
            List<FileFtp> lista = new List<FileFtp>();
            try
            {
                var dataGet = ListDirectory(pattern, false);
                dataGet.Contenido.ForEach(a =>
                {
                    if (Permit != "")
                    {
                        if (Permit.Trim().ToLower() == a.Extension.Trim().ToLower())
                        {
                            lista.Add(new FileFtp
                            {
                                Ruta = Site + publicRoute + a.Name,
                                Name = a.Name
                            });
                        }
                    }
                    else
                    {
                        lista.Add(new FileFtp
                        {
                            Ruta = Site + publicRoute + a.Name,
                            Name = a.Name
                        });
                    }
                });


                #region basura
                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(Uri, false));
                //request.UseBinary = false;
                ////request.UsePassive = true;
                //request.Method = WebRequestMethods.Ftp.ListDirectory;
                //request.Credentials = new NetworkCredential(FTP_user, FTP_password);
                //response = (FtpWebResponse)request.GetResponse();
                //responseStream = response.GetResponseStream();
                //reader = new StreamReader(responseStream);
                //while (reader.Peek() >= 0)
                //{
                //    string nameFile = reader.ReadLine();
                //    //if(Permit != "")
                //    //{
                //    //    if(nameFile.Substring(nameFile.Length, nameFile.Le))
                //    //}
                //    //else
                //    //{
                //    //    lista.Add(new FileFtp { Ruta = Site + publicRoute + nameFile, Name = nameFile });
                //    //}
                //    lista.Add(new FileFtp { Ruta = Site + publicRoute + nameFile, Name = nameFile });

                //}
                #endregion

                return lista;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    //return lista;
                    throw new DarkExceptionSystem(string.Format("Exception FTP - {0}", ex.Message));
                else
                    throw new DarkExceptionSystem(string.Format("Exception FTP - {0}", ex.Message));
            }
            catch (Exception ex)
            {
                throw new DarkExceptionSystem(string.Format("Exception FTP - {0}", ex.Message));
            }
            finally
            {
                //if (reader != null)
                //{
                //    reader.Close();
                //}
                //if (response != null)
                //{
                //    response.Close();
                //}
            }
        }
        [Obsolete]
        public FTPDirectorio ListDirectory(string pattern, bool isroot)
        {
            string Uri = "ftp://" + Server + pattern;

            Stream responseStream = null;
            StreamReader reader = null;
            FtpWebResponse response = null;
            FTPDirectorio FTPDirectorio = new FTPDirectorio();
            FTPDirectorio.PathFolder = $"{pattern}";
            FTPDirectorio.Contenido = new List<FtpContenido>();
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(Uri, false));
                request.UseBinary = false;
                //request.UsePassive = false;
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(User, Password);
                response = (FtpWebResponse)request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);

                string regex =
                    @"^" +                          //# Start of line
                    @"(?<dir>[\-ld])" +             //# File size          
                    @"(?<permission>[\-rwx]{9})" +  //# Whitespace          \n
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<filecode>\d+)" +
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<owner>\w+)" +
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<group>\w+)" +
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<size>\d+)" +
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<month>\w{3})" +            //# Month (3 letters)   \n
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<day>\d{1,2})" +            //# Day (1 or 2 digits) \n
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<timeyear>[\d:]{4,5})" +    //# Time or year        \n
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<filename>(.*))" +          //# Filename            \n
                    @"$";                           //# End of line
                while (reader.Peek() >= 0)
                {
                    var split = new Regex(regex).Match(reader.ReadLine());
                    string dir = split.Groups["dir"].ToString();
                    string filename = split.Groups["filename"].ToString();
                    bool isDirectory = !string.IsNullOrWhiteSpace(dir) && dir.Equals("d", StringComparison.OrdinalIgnoreCase);
                    if (filename != "." && filename != ".ftpquota" && filename != "Thumbs.db")
                    {
                        if (filename == "..")
                        {
                            if (isroot)
                            {
                                string[] allAddresses = pattern.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                string Patrea = string.Join("/", allAddresses.Take(allAddresses.Count() - 1).ToArray());
                                FTPDirectorio.PathLast = dir;
                                FTPDirectorio.Contenido.Add(new FtpContenido
                                {
                                    IsDirectorio = isDirectory,
                                    IsFile = (isDirectory ? false : true),
                                    Extension = (isDirectory ? false : true) ? filename.Split('.')[1] : "",
                                    Name = filename,
                                    PathServer = $"{Patrea}"
                                });
                            }
                        }
                        else
                        {
                            FTPDirectorio.PathLast = dir;
                            FTPDirectorio.Contenido.Add(new FtpContenido
                            {
                                IsDirectorio = isDirectory,
                                IsFile = (isDirectory ? false : true),
                                Extension = (isDirectory ? false : true) ? filename.Split('.')[1] : "",
                                Name = filename,
                                PathServer = $"{pattern}{filename}"
                            });
                        }
                    }
                }
                FTPDirectorio.Contenido = FTPDirectorio.Contenido.OrderByDescending(a => a.IsDirectorio).ToList();
                return FTPDirectorio;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    //return FTPDirectorio;
                    throw new DarkExceptionSystem(string.Format("Exception FTP - {0}", ex.Message));
                else
                    throw new DarkExceptionSystem(string.Format("Exception FTP - {0}", ex.Message));
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
        public List<FileFtp> GetFiless(string Path, string publicRoute)
        {
            string Uri = "ftp://" + Server + Path;

            Stream responseStream = null;
            StreamReader reader = null;
            FtpWebResponse response = null;
            //StreamWriter writeStream = null;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(Uri));
                //request.UsePassive = true;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(User, Password);
                response = (FtpWebResponse)request.GetResponse();
                List<FileFtp> lista = new List<FileFtp>();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                while (reader.Peek() >= 0)
                {
                    string nameFile = reader.ReadLine();
                    lista.Add(new FileFtp { Ruta = Site + publicRoute + nameFile, Name = nameFile });
                }
                return lista;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    return new List<FileFtp>();
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
    public class FileFtp
    {
        /// <summary>
        /// Ruta 
        /// </summary>
        public string Ruta { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Seccion
        /// </summary>
        public string Seccion { get; set; }
    }
    public class FTPDirectorio
    {
        /// <summary>
        /// Nombre del folder contenerdor
        /// </summary>
        public string PathFolder { get; set; }
        /// <summary>
        /// Path anterior
        /// </summary>
        public string PathLast { get; set; }
        /// <summary>
        /// Nombre del folder actual
        /// </summary>
        public string ActualFoder { get; set; }
        /// <summary>
        /// Descripcion corta del path
        /// </summary>
        public string ShortPath { get; set; }
        /// <summary>
        /// Lista de arcchivos
        /// </summary>
        public List<FtpContenido> Contenido { get; set; }
        /// <summary>
        /// Es folder raiz
        /// </summary>
        public bool IsPathRoot { get; set; }
    }

    public class FtpContenido
    {
        /// <summary>
        /// Es una carpeta
        /// </summary>
        public bool IsDirectorio { get; set; }
        /// <summary>
        /// Es un archivo
        /// </summary>
        public bool IsFile { get; set; }
        /// <summary>
        /// Extension del archivo
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// Nombre del directorio o archivo
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PathServer { get; set; }

        public object Datos { get; set; }
    }
}
