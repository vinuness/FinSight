using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Calculos.API.Entities.Juros;
using FinSight.Calculos.API.Entities.Inflation;
using FinSight.Calculos.API.Interface;
using FinSight.Calculos.API.Entities.GoalCalculation;
using FinSight.Calculos.API.Entities.TimeCalculation;
using FinSight.Calculos.API.Entities.EmergencyReserve;

namespace FinSight.Calculos.API.Services
{
    public class Calculos : ICalculos
    {

        private readonly HttpClient _http;

        public Calculos(HttpClient http)
        {
            _http = http;
        }

        public JurosCompostoResponse CalculoJurosComposto(JurosCompostoRequest juros)
        {
            //taxaMensal = (1 + taxa/100^periodo) - 1
            double taxaMensal = Math.Pow(1 + ((double)juros.TaxaAnual / 100), 1.0 / 12) - 1;

            //valorInicialComJuros = valorInicial * (1 + taxa)^prazo
            double valorInicialComJuros = (double)juros.ValorInicial * Math.Pow(1 + taxaMensal,juros.PrazoEmMeses);

            double valorAportes;

            if (taxaMensal > 0)
            {
                valorAportes = (double)juros.AporteMensal * ((Math.Pow(1 + taxaMensal,juros.PrazoEmMeses) - 1)/ taxaMensal);
            }
            else
            {
                valorAportes = (double)juros.AporteMensal * juros.PrazoEmMeses;
            }

            decimal patrimonioFinal = (decimal)(valorInicialComJuros + valorAportes);
            decimal TotalInvestido = juros.ValorInicial + (juros.AporteMensal * juros.PrazoEmMeses);
            decimal rendimentos = patrimonioFinal - TotalInvestido;


            return new JurosCompostoResponse
            {
                TotalInvestido = TotalInvestido,
                PatrimonioFinal = patrimonioFinal,
                Rendimentos = rendimentos
            };
        }

        public InflationResponse CalcularInflação(InflationRequest inflacao)
        {
            return new InflationResponse{};
        }

        public MetaResponse CalcularMeta(MetaRequest meta)
        {
            throw new NotImplementedException();
        }

        public PrazoResponse CalcularPrazo(PrazoRequest prazo)
        {
            throw new NotImplementedException();
        }

        public ReservaResponse CalcularReserva(ReservaRequest reserva)
        {
            throw new NotImplementedException();
        }
    }
}