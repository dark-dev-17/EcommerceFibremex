using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Responses
{
    /// <summary>
    /// Informacion de respuesta
    /// </summary>
    /// <typeparam name="T">Tipo de dato</typeparam>
    public class SplittelRespData
    {
        /// <summary>
        /// Codigo de respuesta 
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Descripción de la respuesta
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Fecha de respuesta
        /// </summary>
        public DateTime Fecha { get { return DateTime.Now; } }
        /// <summary>
        /// Informacion
        /// </summary>
    }
    /// <summary>
    /// Informacion de respuesta
    /// </summary>
    /// <typeparam name="T">Tipo de dato</typeparam>
    public class SplittelRespData<T>
    {
        /// <summary>
        /// Codigo de respuesta 
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Descripción de la respuesta
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Fecha de respuesta
        /// </summary>
        public DateTime Fecha { get { return DateTime.Now; } }
        /// <summary>
        /// Informacion
        /// </summary>
        public T Data { get; set; }
    }
    /// <summary>
    /// Informacion de respuesta
    /// </summary>
    /// <typeparam name="T">Tipo de dato</typeparam>
    public class SplittelRespDatas<T>
    {
        /// <summary>
        /// Codigo de respuesta 
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Descripción de la respuesta
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Fecha de respuesta
        /// </summary>
        public DateTime Fecha { get { return DateTime.Now; } }
        /// <summary>
        /// Informacion
        /// </summary>
        public List<T> Datas { get; set; }
    }
}
