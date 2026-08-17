using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Simulacoes.API.Entities
{
    public class Cenario
    {
        public string Nome { get; set; } = string.Empty;

        public decimal TaxaAnual { get; set; }
    }

    public class CenarioResultado
    {
        public string Nome { get; set; } = string.Empty;

        public decimal TaxaAnual { get; set; }

        public decimal TotalInvestido { get; set; }

        public decimal PatrimonioFinal { get; set; }

        public decimal Rendimentos { get; set; }
    }
}