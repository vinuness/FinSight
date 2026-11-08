using System.ComponentModel.DataAnnotations;

namespace FinSight.Usuario.Domain.Entities
{
    public class RegisterModel
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string ConfirmaSenha { get; set; }
        [StringLength(14, MinimumLength = 11)]
        public required string CPF { get; set; }
    }   
}
