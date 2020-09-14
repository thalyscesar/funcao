using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DML
{
    public static class UtilidadesFormatacao
    {
        public static string SomenteNumeros(this string texto)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in texto)
            {
                if (char.IsDigit(ch))
                    sb.Append(ch);
            }
           return sb.ToString();
        }
    }
}
