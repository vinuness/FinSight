using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Calculos.Request
{
    public class ReservaRequest
    {
        public decimal DespesasMensais { get; set; }

        public int MesesDeReserva { get; set; }
    }
}