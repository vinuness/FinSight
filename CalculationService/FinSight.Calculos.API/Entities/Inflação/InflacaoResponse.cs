using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Calculos.API.Entities.Inflation
{
    public class InflationResponse
    {
        public decimal ValorFuturoEquivalente { get; set; }

        public decimal PerdaPoderDeCompra { get; set; }
    }
}