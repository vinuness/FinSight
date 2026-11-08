using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FinSight.Web.Models.Calculos.Response.DataDTO;

namespace FinSight.Web.Interfaces
{
    public interface IDataService
    {
        Task<double> GetUSDBRL();
        Task<double> GetEURBRL();
        Task<double> GetBTCBRL();
        Task<double> GetSelicData();
        Task<double> GetCDIData();
        Task<double> GetIPCAData();
    }
}