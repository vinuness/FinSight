using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Usuario.Domain.Entities
{
    public class UpdatePass
    {
        public string? Password {get; set; }
        public string? ConfirmPass {get;set;}
    }
}