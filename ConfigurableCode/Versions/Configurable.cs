using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurableCode.Versions
{
    public class ConfigurableJ
    {
        public string Configurable { get; set; }
        public string ItemCodeexample { get; set; }
        public string ItemCode { get; set; }
        public string Expresion { get; set; }
        public List<ElementCode> Blocks { get; set; }
        public List<RestriccionElemento> Rectrictions { get; set; }
        public List<RestriccionCampoUsuario> FieldsFree { get; set; }
    }
    public class RestriccionCampoUsuario
    {
        public string BlockKey { get; set; }
        public string Type { get; set; }
        public bool IsRange { get; set; }
        public double RangeFrom { get; set; }
        public double RangeTo { get; set; }
        public bool HasCerosMask { get; set; }
        public int NumberCeros { get; set; }
        public string UnitMesureUser { get; set; }
        public int NumeroMult { get; set; }
    }
    public class ElementCode
    {
        public string Block { get; set; }
        public string Key { get; set; }
        public bool IsFixed { get; set; }
        public bool IsOptional { get; set; }
        public bool IsOpenUser { get; set; }
        public string Type { get; set; }
        public string FixedValue { get; set; }
        public List<OpcionesSelect> Options { get; set; }
    }
    public class OpcionesSelect
    {
        public string Option { get; set; }
        public string Key { get; set; }
    }
    public class RestriccionElemento
    {
        public string Restriccion { get; set; }
        public string Block { get; set; }
        public List<Regla> Rules { get; set; }
    }
    public class Regla
    {
        public List<string> BlockValues { get; set; }
        public string Type { get; set; }
        public string BlockApply { get; set; }
        public List<string> ValuesAcepted { get; set; }
    }
}
