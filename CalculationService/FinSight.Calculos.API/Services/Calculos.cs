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
            double taxaMensal = Math.Pow(1 + ((double)juros.TaxaAnual / 100), 1.0 / 12) - 1;
            double valorInicialComJuros = (double)juros.ValorInicial * Math.Pow(1 + taxaMensal, juros.PrazoEmMeses);
            double valorAportes;

            if (taxaMensal > 0)
            {
                valorAportes = (double)juros.AporteMensal * ((Math.Pow(1 + taxaMensal, juros.PrazoEmMeses) - 1) / taxaMensal);
            }
            else
            {
                valorAportes = (double)juros.AporteMensal * juros.PrazoEmMeses;
            }
            decimal patrimonioFinal = Math.Round((decimal)(valorInicialComJuros + valorAportes), 2);
            decimal totalInvestido = Math.Round(juros.ValorInicial + (juros.AporteMensal * juros.PrazoEmMeses), 2);
            decimal rendimentos = Math.Round(patrimonioFinal - totalInvestido, 2);

            return new JurosCompostoResponse
            {
                TotalInvestido = totalInvestido,
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
            if(selicRes == null) throw new Exception("Resposta da Selic nula");
            decimal SELIC = (decimal)selicRes.Valor;

            APIIPCAResponse cdiRes = await _http.GetFromJsonAsync<APIIPCAResponse>($"{url}get/data/CDI");
            if(cdiRes == null) throw new Exception("Resposta do CDI nulo");
            decimal CDI = (decimal)cdiRes.Valor;

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
                SELIC = new CenarioMetaResponse
                {
                    Nome = "SELIC",
                    TaxaAnual = SELIC,
                    MetaAtingida = resultadoSelic.PatrimonioFinal >= meta.ValorMeta,
                    PatrimonioEstimado = resultadoSelic.PatrimonioFinal,
                    Diferenca = Math.Abs(meta.ValorMeta - resultadoSelic.PatrimonioFinal)
                },
                CDI = new CenarioMetaResponse
                {
                    Nome = "CDI",
                    TaxaAnual = CDI,
                    MetaAtingida = resultadoCdi.PatrimonioFinal >= meta.ValorMeta,
                    PatrimonioEstimado = resultadoCdi.PatrimonioFinal,
                    Diferenca = Math.Abs(meta.ValorMeta - resultadoCdi.PatrimonioFinal)
                }
            };
        }

        private CenarioPrazo CalcularCenarioPrazo(PrazoRequest prazo, decimal TaxaAnual)
        {
            decimal patrimonio = prazo.ValorInicial;
            int meses = 0;

            double taxaMensal = Math.Pow(1 + ((double)TaxaAnual/100), 1.0/12) - 1;
            while(patrimonio < prazo.Meta)
            {
                decimal jurosMensal = patrimonio * (decimal)taxaMensal;
                patrimonio += jurosMensal + prazo.AporteMensal;
                meses++;
            }
            return new CenarioPrazo
            {
                MesesNecessarios = meses,
                PatrimonioFinal = patrimonio
            };
        }

        public async Task<PrazoResponse> CalcularPrazo(PrazoRequest prazo)
        {

            if(prazo.ValorInicial <= 0 && prazo.AporteMensal <= 0)
                throw new Exception("Informe um valor inicial ou aporte mensal");
            
            APIIPCAResponse selicRes = await _http.GetFromJsonAsync<APIIPCAResponse>($"{url}get/data/Selic");
            if(selicRes == null) throw new Exception("Resposta da Selic nula");
            decimal SELIC = (decimal)selicRes.Valor;

            APIIPCAResponse cdiRes = await _http.GetFromJsonAsync<APIIPCAResponse>($"{url}get/data/CDI");
            if(cdiRes == null) throw new Exception("Resposta do CDI nulo");
            decimal CDI = (decimal)cdiRes.Valor;

            var prazoSelic = CalcularCenarioPrazo(prazo, SELIC);
            var prazoCdi = CalcularCenarioPrazo(prazo, CDI);

            return new PrazoResponse
            {
                SELIC = prazoSelic,
                CDI = prazoCdi
            };
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