using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Calculos.Request
{
    public class MetaRequest
    {
        public decimal ValorMeta { get; set; }
        public decimal ValorInicial { get; set; }
        public int PrazoEmMeses { get; set; }
        public decimal AporteMensal { get; set; }
    }
}