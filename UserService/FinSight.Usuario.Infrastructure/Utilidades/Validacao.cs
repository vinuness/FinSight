using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Usuario.Infrastructure.Utilidades
{
    public static class Validacao
    {
        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = {10,9,8,7,6,5,4,3,2};
            int[] multiplicador2 = {11,10,9,8,7,6,5,4,3,2};
            string tempCpf;
            string digito;
            int soma = 0;
            int resto;

            cpf = cpf.Trim(); //remove espacos em branco
            cpf = cpf.Replace(".", "").Replace("-", ""); //troca '.' por espacos em branco e faz o mesmo com '-'

            if(cpf.Length != 11) return false;

            tempCpf = cpf.Substring(0, 9); //pega os digitos do cpf do indice 0 até 9

            for(int i = 0; i < 9; i++) soma += int.Parse(tempCpf[i].ToString())*multiplicador1[i];
            
            resto = soma % 11;

            if(resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for(int i = 0; i < 10; i++) soma += int.Parse(tempCpf[i].ToString())*multiplicador2[i];

            resto = soma % 11;
            if(resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}