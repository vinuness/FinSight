using System.Text.Json.Serialization;

namespace FinSight.Usuario.Domain.Entities
{
    public class EnderecoModel
    {
        public int Id { get; set; }
        public int CEP { get; set; }
        public string? Bairro { get; set; }
        public string? Rua { get; set; }
        public int Numero { get; set; }
        public string? Localidade { get; set; }
        public string? Estado { get; set; }

        [JsonIgnore]
        public List<UsuarioModel> Usuarios = new();
    }

    public class EnderecoDTO
    {
        public int CEP { get; set; }
        public string? Bairro { get; set; }
        public string? Rua { get; set; }
        public int Numero { get; set; }
        public string? Localidade { get; set; }
        public string? Estado { get; set; }
    }
}
