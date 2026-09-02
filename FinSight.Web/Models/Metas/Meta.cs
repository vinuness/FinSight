using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSight.Web.Models.Metas
{
    public class Meta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = "Nova Meta";
        public string? Descricao { get; set; }
        public decimal ValorAtual { get; set; }
        public decimal ValorDesejado { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class MetaDTO
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal ValorAtual {get;set;}
        public decimal ValorDesejado {get; set;}
        public required Guid UsuarioId { get; set; }
    }

    public class MetaUpdate
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? ValorAtual {get;set;}
        public decimal? ValorDesejado {get; set;}
    }
}