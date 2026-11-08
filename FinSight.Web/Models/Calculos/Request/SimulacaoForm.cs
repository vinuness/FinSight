using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Web.Models.Calculos.Response;

namespace FinSight.Web.Models.Calculos.Request
{
    public class SimulacaoForm
    {
        public MetaRequest Meta { get; set; } = new();

        public decimal DespesasMensais { get; set; }

        public int MesesReserva { get; set; } = 6;

        public SimulacaoResultado? Resultado { get; set; }
    }

    public class SimulacaoResultado
{
    public MetaResponse Meta { get; set; } 

    public PrazoResponse Prazo { get; set; }

    public InflationResponse Inflacao { get; set; }

    public ReservaResponse Reserva { get; set; }
}
}