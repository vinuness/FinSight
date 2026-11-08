using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Calculos.Response
{
    public class DataDTO
    {
        
        public class APIResponseDTO
        {
            public string? Nome { get; set; }
            public double Valor { get; set; }
        }
        public class BCBDataDTO
        {
            public string Data { get; set;}
            public double Valor { get; set; }
        }
    }
}