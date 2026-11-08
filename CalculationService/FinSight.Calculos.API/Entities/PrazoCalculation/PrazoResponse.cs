using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Calculos.API.Entities.TimeCalculation
{
    public class PrazoResponse
    {
        public CenarioPrazo SELIC {get;set;}
        public CenarioPrazo CDI {get;set;}
    }

    public class CenarioPrazo
    {
        public int MesesNecessarios { get; set; }

        public decimal PatrimonioFinal { get; set; }
    }
}