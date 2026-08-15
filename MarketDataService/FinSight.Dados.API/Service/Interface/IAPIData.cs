using FinSight.Dados.API.DTOs;

namespace FinSight.Dados.API.Service.Interface
{
    public interface IAPIData
    {
        Task<APIResponseDTO> GetUSDBRL();
        Task<APIResponseDTO> GetEURBRL();
        Task<APIResponseDTO> GetBTCBRL();
        Task<BCBDataModel> GetSelicData();
        Task<BCBDataModel> GetCDIData();
        Task<BCBDataModel> GetIPCAData();
    }
}
