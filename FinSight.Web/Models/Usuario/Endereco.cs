using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Usuario
{
    public class Endereco
    {
        public int Id { get; set; }
        public int CEP { get; set; }
        public string? Bairro { get; set; }
        public string? Rua { get; set; }
        public int Numero { get; set; }
        public string? Localidade { get; set; }
        public string? Estado { get; set; }

        [JsonIgnore]
        public List<Usuario> Usuarios = new();
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