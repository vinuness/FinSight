using FinSight.Calculos.API.Entities.EmergencyReserve;
using FinSight.Calculos.API.Entities.GoalCalculation;
using FinSight.Calculos.API.Entities.Inflation;
using FinSight.Calculos.API.Entities.TimeCalculation;
using FinSight.Calculos.API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinSight.Calculos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculoController : ControllerBase
    {
        private readonly ICalculos _calc;

        public CalculoController(ICalculos calc)
        {
            _calc = calc;
        }

        [HttpPost("calcular/meta")]
        public async Task<ActionResult<MetaResponse>> CalcularMeta(MetaRequest meta)
        {
            MetaResponse response = await _calc.CalcularMeta(meta);
            return Ok(response);
        }

        [HttpPost("calcular/prazo")]
        public async Task<ActionResult<PrazoResponse>> CalcularPrazo(PrazoRequest prazo)
        {
            PrazoResponse response = await _calc.CalcularPrazo(prazo);
            return Ok(response);
        }
        
        [HttpPost("calcular/inflacao")]
        public async Task<ActionResult<InflationResponse>> CalcularInflação(InflationRequest inflacao)
        {
            InflationResponse response = await _calc.CalcularInflação(inflacao);
            return Ok(response);
        }

        [HttpPost("calcular/reserva")]
        public async Task<ActionResult<ReservaResponse>> CalcularReserva(ReservaRequest reserva)
        {
            ReservaResponse response = _calc.CalcularReserva(reserva);
            return Ok(response);
        }
        
    }
}