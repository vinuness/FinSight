namespace FinSight.Metas.Domain.Entities
{
    public class Meta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Nova Meta";
        public string? Description { get; set; }
        public double ValorAlcancado { get; set; } = 0;
        public string? Email { get; set; }
    }

    public class MetaDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public required string Email { get; set; }
    }

    public class MetaUpdate
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
