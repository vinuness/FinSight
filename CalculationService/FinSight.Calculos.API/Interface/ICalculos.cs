using FinSight.Calculos.API.Entities.Juros;
using FinSight.Calculos.API.Entities.Inflation;
using FinSight.Calculos.API.Entities.TimeCalculation;
using FinSight.Calculos.API.Entities.GoalCalculation;
using FinSight.Calculos.API.Entities.EmergencyReserve;

namespace FinSight.Calculos.API.Interface
{
    public interface ICalculos
    {
        JurosCompostoResponse CalculoJurosComposto(JurosCompostoRequest juros);
        Task<InflationResponse> CalcularInflação(InflationRequest inflacao);
        Task<MetaResponse> CalcularMeta(MetaRequest meta);
        PrazoResponse CalcularPrazo(PrazoRequest prazo);
        ReservaResponse CalcularReserva(ReservaRequest reserva);
    }
}