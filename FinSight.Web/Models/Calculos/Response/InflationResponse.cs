using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Calculos.Response
{
    public class InflationResponse
    {
        public decimal ValorAtual { get; set; }
        public decimal ValorFuturo { get; set; }
        public decimal Diferenca { get; set; }
    }
}