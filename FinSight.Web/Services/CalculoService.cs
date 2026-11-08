using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Web.Interfaces;
using FinSight.Web.Models.Calculos.Request;
using FinSight.Web.Models.Calculos.Response;

namespace FinSight.Web.Services
{
    public class CalculoService : ICalculoService
    {
        private readonly HttpClient _http;

        public CalculoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<InflationResponse> CalcularInflação(InflationRequest inflacao)
        {
            var response = await _http.PostAsJsonAsync("calcular/inflacao", inflacao);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<InflationResponse>() 
                ?? throw new Exception("Não foi possível obter o resultado do cálculo de inflação.");
        }

        public async Task<MetaResponse> CalcularMeta(MetaRequest meta)
        {
            var response = await _http.PostAsJsonAsync("calcular/meta", meta);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MetaResponse>() 
                ?? throw new Exception("Não foi possível obter o resultado do cálculo de meta.");
        }

        public async Task<PrazoResponse> CalcularPrazo(PrazoRequest prazo)
        {
            var response = await _http.PostAsJsonAsync("calcular/prazo", prazo);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PrazoResponse>() 
                ?? throw new Exception("Não foi possível obter o resultado do cálculo de prazo.");
        }

        public async Task<ReservaResponse> CalcularReserva(ReservaRequest reserva)
        {
            var response = await _http.PostAsJsonAsync("calcular/reserva", reserva);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReservaResponse>() 
                ?? throw new Exception("Não foi possível obter o resultado do cálculo de reserva.");
        }

        public async Task<SimulacaoResultado> CalcularSimulacao(SimulacaoForm simulacao)
        {
            ReservaRequest reservaRequest = new ReservaRequest
            {
                DespesasMensais = simulacao.DespesasMensais,
                MesesDeReserva = simulacao.MesesReserva
            };

            PrazoRequest prazoRequest = new PrazoRequest
            {
                Meta = simulacao.Meta.ValorMeta,
                AporteMensal = simulacao.Meta.AporteMensal,
                ValorInicial = simulacao.Meta.ValorInicial
            };

            InflationRequest inflacao = new InflationRequest
            {
                Valor = simulacao.Meta.ValorMeta,
                PrazoEmMeses = simulacao.Meta.PrazoEmMeses
            };

            var MetaRes = CalcularMeta(simulacao.Meta);
            var PrazoRes = CalcularPrazo(prazoRequest);
            var InflacaoRes = CalcularInflação(inflacao);
            var ReservaRes = CalcularReserva(reservaRequest);

            await Task.WhenAll(MetaRes, PrazoRes, InflacaoRes, ReservaRes);

            return new SimulacaoResultado{
                Meta = await MetaRes,
                Prazo = await PrazoRes,
                Inflacao = await InflacaoRes,
                Reserva = await ReservaRes
            };
        }
    }
}