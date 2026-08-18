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

        [HttpGet("calcular/meta")]
        public async Task<ActionResult<MetaResponse>> CalcularMeta([FromBody] MetaRequest meta)
        {
            MetaResponse response = await _calc.CalcularMeta(meta);
            return Ok(response);
        }

        [HttpGet("calcular/prazo")]
        public async Task<ActionResult<PrazoResponse>> CalcularPrazo([FromBody] PrazoRequest prazo)
        {
            PrazoResponse response = await _calc.CalcularPrazo(prazo);
            return Ok(response);
        }
        
        [HttpGet("calcular/inflacao")]
        public async Task<ActionResult<InflationResponse>> CalcularInflação([FromBody] InflationRequest inflacao)
        {
            InflationResponse response = await _calc.CalcularInflação(inflacao);
            return Ok(response);
        }

        [HttpGet("calcular/reserva")]
        public async Task<ActionResult<ReservaResponse>> CalcularReservaAsync([FromBody] ReservaRequest reserva)
        {
            ReservaResponse response = _calc.CalcularReserva(reserva);
            return Ok(response);
        }
        
    }
}