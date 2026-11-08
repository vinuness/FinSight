using FinSight.Metas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinSight.Metas.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Meta> Metas { get; set; }
    }
}
