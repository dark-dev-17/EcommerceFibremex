using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Herramientas
{
    public static class Herramienta
    {
        public static string ReverseCadena(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
