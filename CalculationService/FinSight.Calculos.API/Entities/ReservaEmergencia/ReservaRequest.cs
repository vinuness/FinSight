using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Calculos.API.Entities.EmergencyReserve
{
    public class ReservaRequest
    {
        public decimal DespesasMensais { get; set; }

        public int MesesDeReserva { get; set; }
    }
}