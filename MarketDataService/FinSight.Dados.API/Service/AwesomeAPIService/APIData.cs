using FinSight.Dados.API.DTOs;
using FinSight.Dados.API.Service.Interface;
using System.Globalization;

namespace FinSight.Dados.API.Service.AwesomeAPIService
{
    public class APIData : IAPIData
    {
        private readonly HttpClient _client;

        public APIData(HttpClient client)
        {
            _client = client;
        }

        public async Task<APIResponseDTO> GetUSDBRL()
        {
            var currencies = await _client.GetFromJsonAsync<Dictionary<string, AwesomeAPICurrency>>
                ("https://economia.awesomeapi.com.br/json/last/USD-BRL,EUR-BRL,BTC-BRL");

            if (currencies == null)
            {
                return null;
            }

            var dolar = currencies["USDBRL"];
            double valor = Convert.ToDouble(dolar.Bid, CultureInfo.InvariantCulture);

            return new APIResponseDTO
            {
                Nome = "USDBRL",
                Valor = Math.Round(valor, 2)
            };
        }

        public async Task<APIResponseDTO> GetEURBRL()
        {
            var currencies = await _client.GetFromJsonAsync<Dictionary<string, AwesomeAPICurrency>>
                ("https://economia.awesomeapi.com.br/json/last/USD-BRL,EUR-BRL,BTC-BRL");

            if (currencies == null)
            {
                return null;
            }

            var dolar = currencies["EURBRL"];
            double valor = Convert.ToDouble(dolar.Bid, CultureInfo.InvariantCulture);

            return new APIResponseDTO
            {
                Nome = "EURBRL",
                Valor = Math.Round(valor, 2)
            };
        }

        public async Task<APIResponseDTO> GetBTCBRL()
        {
            var currencies = await _client.GetFromJsonAsync<Dictionary<string, AwesomeAPICurrency>>
                ("https://economia.awesomeapi.com.br/json/last/USD-BRL,EUR-BRL,BTC-BRL");

            if (currencies == null)
            {
                return null;
            }

            var dolar = currencies["BTCBRL"];
            double valor = Convert.ToDouble(dolar.Bid, CultureInfo.InvariantCulture);

            return new APIResponseDTO
            {
                Nome = "BTCBRL",
                Valor = Math.Round(valor, 2)
            };
        }

        public async Task<BCBDataModel> GetSelicData()
        {
            var DataInicial = DateOnly
            .FromDateTime(DateTime.Now)
            .AddMonths(-1)
            .ToString();

            List<BCBDataModel> valores = await _client.GetFromJsonAsync<List<BCBDataModel>>(
                $"https://api.bcb.gov.br/dados/serie/bcdata.sgs.11/dados?formato=json&dataInicial={DataInicial}");

            var indice = valores.Count()-1;
            return valores[indice];
        }

        public async Task<BCBDataModel> GetCDIData()
        {
            var DataInicial = DateOnly
            .FromDateTime(DateTime.Now)
            .AddMonths(-1)
            .ToString();

            List<BCBDataModel> valores = await _client.GetFromJsonAsync<List<BCBDataModel>>(
                $"https://api.bcb.gov.br/dados/serie/bcdata.sgs.12/dados?formato=json&dataInicial={DataInicial}");

            var indice = valores.Count() - 1;
            return valores[indice];
        }

        public async Task<BCBDataModel> GetIPCAData()
        {

            List<BCBDataModel> valores = await _client.GetFromJsonAsync<List<BCBDataModel>>(
                $"https://api.bcb.gov.br/dados/serie/bcdata.sgs.433/dados?formato=json");

            var indice = valores.Count() - 1;
            return valores[indice];
        }
    }
}
