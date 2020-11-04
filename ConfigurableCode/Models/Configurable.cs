using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurableCode.Models
{
    public class Configurable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ExpresionRegular { get; set; }
        public string CodeExample { get; set; }
        public List<Parte> Partes { get; set; }
    }

    public class Parte
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public Mascara Mascara { get; set; }
        public List<Opcion> Opciones { get; set; }
        public List<Restriccion> Restricciones { get; set; }
    }

    public class Mascara
    {
        public int Id { get; set; }
        public int Longitud { get; set; }
        public string UnidadMedida { get; set; }
        public Equivalencia Equivalencia { get; set; }
    }

    public class Equivalencia
    {
        public int Base { get; set; }
        public int Igualdad { get; set; }
    }

    public class Opcion
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public int Posicion { get; set; }
    }

    public class Restriccion
    {
        public int Id { get; set; }
        public List<string> Detonantes { get; set; }
        public string Operador { get; set; }
        public string Accion { get; set; }
        public List<string> Valores { get; set; }
        public int ParteAplicar { get; set; }
    }
}
