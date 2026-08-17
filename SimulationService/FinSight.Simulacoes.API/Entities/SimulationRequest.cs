using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Simulacoes.API.Entities
{
    public class SimulationRequest
    {
        public decimal ValorInicial { get; set; }

        public decimal AporteMensal { get; set; }

        public int PrazoEmMeses { get; set; }
    }

    public class SimulationResponse
    {
        public decimal ValorInicial { get; set; }

        public decimal AporteMensal { get; set; }

        public int PrazoEmMeses { get; set; }

        public decimal TotalInvestido { get; set; }

        public List<CenarioResultado> Cenários { get; set; } = new();
    }
}