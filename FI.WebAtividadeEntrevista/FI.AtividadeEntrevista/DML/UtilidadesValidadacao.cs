using System;

namespace FI.AtividadeEntrevista.DML.Utilidades
{
    public static class UtilidadesValidadacao
    {
        private const string CPF0 = "00000000000";
        private const string CPF1 = "11111111111";
        private const string CPF2 = "22222222222";
        private const string CPF3 = "33333333333";
        private const string CPF4 = "44444444444";
        private const string CPF5 = "55555555555";
        private const string CPF6 = "66666666666";
        private const string CPF7 = "77777777777";
        private const string CPF8 = "88888888888";
        private const string CPF9 = "99999999999";

        public static bool CPFEhValido(string CPF)
        {
            if (CPF != null)
            {
                CPF = CPF.Trim();
                string CPFLimpo = string.Empty;
                for (int i = 0; i < CPF.Length; i++)
                {
                    if (Char.IsNumber(CPF[i]))
                        CPFLimpo += CPF[i];
                }
                CPF = CPFLimpo;
            }
            if (string.IsNullOrEmpty(CPF) || CPF.Length != 11) return false;
            if (CPF0.Equals(CPF)) return false;
            if (CPF1.Equals(CPF)) return false;
            if (CPF2.Equals(CPF)) return false;
            if (CPF3.Equals(CPF)) return false;
            if (CPF4.Equals(CPF)) return false;
            if (CPF5.Equals(CPF)) return false;
            if (CPF6.Equals(CPF)) return false;
            if (CPF7.Equals(CPF)) return false;
            if (CPF8.Equals(CPF)) return false;
            if (CPF9.Equals(CPF)) return false;

            int[] CPFNum = new int[11];
            for (int i = 0; i < 11; i++)
            {
                CPFNum[i] = int.Parse(CPF[i].ToString());
            }

            int d1, d2;

            //Note: compute 1st verification digit.
            d1 = 10 * CPFNum[0] + 9 * CPFNum[1] + 8 * CPFNum[2];
            d1 += 7 * CPFNum[3] + 6 * CPFNum[4] + 5 * CPFNum[5];
            d1 += 4 * CPFNum[6] + 3 * CPFNum[7] + 2 * CPFNum[8];
            d1 = 11 - d1 % 11;
            d1 = (d1 >= 10) ? 0 : d1;

            //Note: compute 2nd verification digit.
            d2 = 11 * CPFNum[0] + 10 * CPFNum[1] + 9 * CPFNum[2];
            d2 += 8 * CPFNum[3] + 7 * CPFNum[4] + 6 * CPFNum[5];
            d2 += 5 * CPFNum[6] + 4 * CPFNum[7] + 3 * CPFNum[8];
            d2 += 2 * d1;
            d2 = 11 - d2 % 11;
            d2 = (d2 >= 10) ? 0 : d2;

            //Note: True if verification digits are as expected.
            return (d1 == CPFNum[9]) && (d2 == CPFNum[10]);
        }
    }
}
