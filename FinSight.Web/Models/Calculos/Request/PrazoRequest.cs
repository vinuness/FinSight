using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Calculos.Request
{
    public class PrazoRequest
    {
        public decimal Meta { get; set; }

        public decimal ValorInicial { get; set; }

        public decimal AporteMensal { get; set; }
    }
}