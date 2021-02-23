using System;
using System.Collections.Generic;
using System.Text;

namespace DbManagerDark.Exceptions
{
    public class DarkException : Exception
    {
        public int Code { get; set; }
        public string Mensaje { get; set; }
        public TypeException TipoMensaje { get; set; }
        public TypeError TipoErro { get; set; }
        public string IdAux { get; set; }

        public override string Message
        {
            get
            {
                if(TipoErro == TypeError.User)
                {
                    return $"{TipoMensaje.ToString()}, {Mensaje}";
                }
                else
                {
                    return $"{TipoMensaje.ToString()}, Codigo: {Code} Descripcion:{Mensaje}";
                }
            }
        }
    }

    public enum TypeException
    {
        Info = 1,
        Exito = 2,
        Alerta = 3,
        Error = 3,
    }

    public enum TypeError
    {
        User = 1,
        System = 2,
    }
}