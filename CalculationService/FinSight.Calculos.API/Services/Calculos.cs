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
using FinSight.Calculos.API.Entities;

namespace FinSight.Calculos.API.Services
{
    public class Calculos : ICalculos
    {
        private const string url = "http://localhost:5058/api/APIData/";
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

        public async Task<InflationResponse> CalcularInflação(InflationRequest inflacao)
        {

            APIIPCAResponse response = await _http.GetFromJsonAsync<APIIPCAResponse>($"{url}get/data/IPCA");

            if (response == null) throw new Exception("Não foi possível obter o IPCA");

            inflacao.IPCA = (decimal)response.Valor;

            double anos = inflacao.PrazoEmMeses / 12.0;
            double taxa = (double)inflacao.IPCA / 100;
            decimal valorFuturo = inflacao.Valor * (decimal)Math.Pow(1 + taxa, anos);

            return new InflationResponse
            {
                ValorAtual = inflacao.Valor,
                ValorFuturo = valorFuturo,
                Diferenca = valorFuturo - inflacao.Valor
            };
        }

        public async Task<MetaResponse> CalcularMeta(MetaRequest meta)
        {

            APIIPCAResponse selicRes = await _http.GetFromJsonAsync<APIIPCAResponse>($"{url}get/data/Selic");
            decimal SELIC = (decimal)selicRes.Valor;

            APIIPCAResponse cdiRes = await _http.GetFromJsonAsync<APIIPCAResponse>($"{url}get/data/CDI");
            decimal CDI = (decimal)cdiRes.Valor;

            if (selicRes == null || cdiRes == null)
            {
                throw new InvalidOperationException("Não foi possível obter SELIC ou CDI.");
            }

            var jurosSelic = new JurosCompostoRequest
            {
                ValorInicial = meta.ValorInicial,
                AporteMensal = meta.AporteMensal,
                PrazoEmMeses = meta.PrazoEmMeses,
                TaxaAnual = SELIC
            };

            var resultadoSelic = CalculoJurosComposto(jurosSelic);

            var jurosCdi = new JurosCompostoRequest
            {
                ValorInicial = meta.ValorInicial,
                AporteMensal = meta.AporteMensal,
                PrazoEmMeses = meta.PrazoEmMeses,
                TaxaAnual = CDI
            };

            var resultadoCdi = CalculoJurosComposto(jurosCdi);

            return new MetaResponse
            {
                ValorMeta = meta.ValorMeta,
                SELIC =
                {
                    Nome = "SELIC",
                    TaxaAnual = SELIC,
                    MetaAtingida = resultadoSelic.PatrimonioFinal >= meta.ValorMeta,
                    PatrimonioEstimado = resultadoSelic.PatrimonioFinal,
                    Diferenca = Math.Abs(meta.ValorMeta - resultadoSelic.PatrimonioFinal)
                },
                CDI =
                {
                    Nome = "CDI",
                    TaxaAnual = CDI,
                    MetaAtingida = resultadoCdi.PatrimonioFinal >= meta.ValorMeta,
                    PatrimonioEstimado = resultadoCdi.PatrimonioFinal,
                    Diferenca = Math.Abs(meta.ValorMeta - resultadoCdi.PatrimonioFinal)
                }
            };
        }

        public PrazoResponse CalcularPrazo(PrazoRequest prazo)
        {
            return new PrazoResponse{};
        }

        public ReservaResponse CalcularReserva(ReservaRequest reserva)
        {
            decimal valorReserva = reserva.DespesasMensais * reserva.MesesDeReserva;
            return new ReservaResponse
            {
                ValorReserva = valorReserva
            };
        }
    }
}