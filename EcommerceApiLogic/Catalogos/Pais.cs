using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Catalogos
{
    public class Pais
    {
        public string NombrePais { get { return "Mexico"; } }
        public List<Estado> Estados { get { 
                return new List<Estado> {
                     new Estado{ Value = "", Label = "Selecciona tu estado" },
                    new Estado{ Value = "AGS", Label = "Aguascalientes" },
                    new Estado{ Value = "BC", Label = "Baja California" },
                    new Estado{ Value = "BCS", Label = "Baja California Sur" },
                    new Estado{ Value = "CAM", Label = "Campeche" },
                    new Estado{ Value = "CHS", Label = "Chiapas" },
                    new Estado{ Value = "CHI", Label = "Chihuahua" },
                    new Estado{ Value = "COA", Label = "Coahuila" },
                    new Estado{ Value = "COL", Label = "Colima" },
                    new Estado{ Value = "CMX", Label = "Ciudad de México" },
                    new Estado{ Value = "DUR", Label = "Durango" },
                    new Estado{ Value = "MEX", Label = "México" },
                    new Estado{ Value = "GTO", Label = "Guanajuato" },
                    new Estado{ Value = "GRO", Label = "Guerrero" },
                    new Estado{ Value = "HID", Label = "Hidalgo" },
                    new Estado{ Value = "JAL", Label = "Jalisco" },
                    new Estado{ Value = "MCH", Label = "Michoacán" },
                    new Estado{ Value = "MOR", Label = "Morelos" },
                    new Estado{ Value = "NAY", Label = "Nayarit" },
                    new Estado{ Value = "NL", Label = "Nuevo León" },
                    new Estado{ Value = "OAX", Label = "Oaxaca" },
                    new Estado{ Value = "PUE", Label = "Puebla" },
                    new Estado{ Value = "QUE", Label = "Querétaro" },
                    new Estado{ Value = "QR", Label = "Quintana Roo" },
                    new Estado{ Value = "SLP", Label = "San Luis Potosí"},
                    new Estado{ Value = "SIN", Label = "Sinaloa" },
                    new Estado{ Value = "SON", Label = "Sonora" },
                    new Estado{ Value = "TAB", Label = "Tabasco" },
                    new Estado{ Value = "TAM", Label = "Tamaulipas" },
                    new Estado{ Value = "TLA", Label = "Tlaxcala" },
                    new Estado{ Value = "VER", Label = "Veracruz" },
                    new Estado{ Value = "YUC", Label = "Yucatán" },
                    new Estado{ Value = "ZAC", Label = "Zacatecas" },
                }; 
            } 
        }
    }

    public class Estado
    {
        public string Value { get; set; }
        public string Label { get; set; }
    }
}
