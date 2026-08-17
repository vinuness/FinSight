using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Calculos.API.Entities.GoalCalculation
{
    public class MetaResponse
    {
        public bool MetaAtingida { get; set; }

        public decimal PatrimonioEstimado { get; set; }

        public decimal DiferencaParaMeta { get; set; }

        public decimal AporteNecessario { get; set; }
    }
}