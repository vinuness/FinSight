using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSight.Web.Interfaces;
using static FinSight.Web.Models.Calculos.Response.DataDTO;

namespace FinSight.Web.Services
{
    public class DataService : IDataService
    {

        private readonly HttpClient _http;

        public DataService(HttpClient http)
        {
            _http = http;
        }

        public async Task<double> GetUSDBRL()
        {
            var response = await _http.GetAsync("get/data/USDBRL");
            response.EnsureSuccessStatusCode();
            APIResponseDTO? USDBRL = await response.Content.ReadFromJsonAsync<APIResponseDTO>();
            return USDBRL?.Valor ?? throw new Exception("Não foi possível obter o valor do USDBRL.");
        }

        public async Task<double> GetEURBRL()
        {
            var response = await _http.GetAsync("get/data/EURBRL");
            response.EnsureSuccessStatusCode();
            APIResponseDTO? EURBRL = await response.Content.ReadFromJsonAsync<APIResponseDTO>();
            return EURBRL?.Valor ?? throw new Exception("Não foi possível obter o valor do EURBRL.");
        }

        public async Task<double> GetBTCBRL()
        {
            var response = await _http.GetAsync("get/data/BTCBRL");
            response.EnsureSuccessStatusCode();
            APIResponseDTO? BTCBRL = await response.Content.ReadFromJsonAsync<APIResponseDTO>();
            return BTCBRL?.Valor ?? throw new Exception("Não foi possível obter o valor do BTCBRL.");
        }

        public async Task<double> GetSelicData()
        {
            var response = await _http.GetAsync("get/data/Selic");
            response.EnsureSuccessStatusCode();
            BCBDataDTO? Selic = await response.Content.ReadFromJsonAsync<BCBDataDTO>();
            return Selic?.Valor ?? throw new Exception("Não foi possível obter o valor da Selic.");
        }

        public async Task<double> GetCDIData()
        {
            var response = await _http.GetAsync("get/data/CDI");
            response.EnsureSuccessStatusCode();
            BCBDataDTO? CDI = await response.Content.ReadFromJsonAsync<BCBDataDTO>();
            return CDI?.Valor ?? throw new Exception("Não foi possível obter o valor do CDI.");
        }

        public async Task<double> GetIPCAData()
        {
            var response = await _http.GetAsync("get/data/IPCA");
            response.EnsureSuccessStatusCode();
            BCBDataDTO? IPCA = await response.Content.ReadFromJsonAsync<BCBDataDTO>();
            return IPCA?.Valor ?? throw new Exception("Não foi possível obter o valor do IPCA.");
        }
    }
}