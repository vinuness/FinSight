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

            cliente.HasIndex(c => c.CPF)
                .IsUnique();

            cliente.HasIndex(c => c.Email)
                .IsUnique();
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
