using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Web.Models.Calculos.Request;
using FinSight.Web.Models.Calculos.Response;

namespace FinSight.Web.Interfaces
{
    public interface ICalculoService
    {
        public Task<MetaResponse> CalcularMeta(MetaRequest meta);
        public Task<PrazoResponse> CalcularPrazo(PrazoRequest prazo);
        public Task<InflationResponse> CalcularInflação(InflationRequest inflacao);
        public Task<ReservaResponse> CalcularReserva(ReservaRequest reserva);
        public Task<SimulacaoResultado> CalcularSimulacao(SimulacaoForm simulacao);
    }
}