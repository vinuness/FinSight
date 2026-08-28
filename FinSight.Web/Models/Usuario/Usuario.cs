using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Usuario
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? CPF { get; set; }

        [JsonIgnore]
        public List<Endereco> Enderecos = new();
    }
}