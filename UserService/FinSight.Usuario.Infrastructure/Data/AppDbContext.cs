using FinSight.Usuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinSight.Usuario.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cliente = modelBuilder.Entity<UsuarioModel>();
            var endereco = modelBuilder.Entity<EnderecoModel>();

            cliente.HasIndex(c => c.CPF)
                .IsUnique();

            cliente.HasIndex(c => c.Email)
                .IsUnique();

            cliente.HasMany(c => c.Enderecos)
                .WithMany(e => e.Usuarios);

            endereco.HasMany(e => e.Usuarios)
                .WithMany(c => c.Enderecos);
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
    }
}
