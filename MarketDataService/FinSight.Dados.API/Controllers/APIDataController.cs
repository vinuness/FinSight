using FinSight.Dados.API.DTOs;
using FinSight.Dados.API.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinSight.Dados.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIDataController : ControllerBase
    {
        private readonly IAPIData _data;

        public APIDataController(IAPIData data)
        {
            _data = data;
        }

        [HttpGet("get/data/USDBRL")]
        public async Task<APIResponseDTO> GetUSDBRL()
        {
            return await _data.GetUSDBRL();
        }

        [HttpGet("get/data/EURBRL")]
        public async Task<APIResponseDTO> GetEURBRL()
        {
            return await _data.GetEURBRL();
        }

        [HttpGet("get/data/BTCBRL")]
        public async Task<APIResponseDTO> GetBTCBRL()
        {
            return await _data.GetBTCBRL();
        }

        [HttpGet("get/data/Selic")]
        public async Task<BCBDataDTO> GetSelicData()
        {
            return await _data.GetSelicData();
        }

        [HttpGet("get/data/CDI")]
        public async Task<BCBDataDTO> GetCDIData()
        {
            return await _data.GetCDIData();
        }

        [HttpGet("get/data/IPCA")]
        public async Task<BCBDataDTO> GetIPCAData()
        {
            return await _data.GetIPCAData();
        }
    }
}
