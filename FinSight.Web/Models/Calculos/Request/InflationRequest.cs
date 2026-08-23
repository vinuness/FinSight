using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Calculos.Request
{
    public class InflationRequest
    {
        public decimal Valor { get; set; }
        public int PrazoEmMeses { get; set; }
    }
}