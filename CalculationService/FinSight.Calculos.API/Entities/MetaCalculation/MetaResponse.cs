using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Calculos.API.Entities.GoalCalculation
{
    public class MetaResponse
    {
        public decimal ValorMeta { get; set; }
        public CenarioMetaResponse SELIC {get;set;}
        public CenarioMetaResponse CDI {get;set;}
    }

    public class CenarioMetaResponse
    {
        public string Nome { get; set; }

        public decimal TaxaAnual { get; set; }

        public bool MetaAtingida { get; set; }

        public decimal Diferenca { get; set; }

        public decimal PatrimonioEstimado { get; set; }
    }
}