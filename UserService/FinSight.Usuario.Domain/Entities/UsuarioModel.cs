using System.Text.Json.Serialization;

namespace FinSight.Usuario.Domain.Entities
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public int CPF { get; set; }
        public string Role { get; set; } = "Usuario";

        [JsonIgnore]
        public List<EnderecoModel> Enderecos = new();
    }
}
