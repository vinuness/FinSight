using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Usuario
{
    public class RegisterModel
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string ConfirmaSenha { get; set; }
        public required string CPF { get; set; }
    }
}