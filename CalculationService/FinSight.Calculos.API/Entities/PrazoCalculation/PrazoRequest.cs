using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Calculos.API.Entities.TimeCalculation
{
    public class PrazoRequest
    {
        public decimal Meta { get; set; }

        public decimal ValorInicial { get; set; }

        public decimal AporteMensal { get; set; }
    }
}