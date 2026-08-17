using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Calculos.API.Entities.Inflation
{
    public class InflationRequest
    {
        public decimal ValorAtual { get; set; }

        public decimal TaxaInflacaoAnual { get; set; }

        public int Anos { get; set; }
    }
}