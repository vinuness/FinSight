using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Calculos.API.Entities.Inflation
{
    public class InflationRequest
    {
        public decimal Valor { get; set; }
        public int PrazoEmMeses { get; set; }
    }
}