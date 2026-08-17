namespace FinSight.Metas.Domain.Entities
{
    public class Meta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Nova Meta";
        public string? Description { get; set; }
        public double ValorAlcancado { get; set; }
        public double ValorDesejado { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class MetaDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double ValorAlcancado {get;set;}
        public double ValorDesejado {get; set;}
        public required Guid UsuarioId { get; set; }
    }

    public class MetaUpdate
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double ValorAlcancado {get;set;}
        public double ValorDesejado {get; set;}
    }
}
