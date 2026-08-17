using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Calculos.API.Entities.Juros
{
    public class JurosCompostoResponse
    {
        public decimal PatrimonioFinal { get; set; }

        public decimal TotalInvestido { get; set; }

        public decimal Rendimentos { get; set; }
    }
}