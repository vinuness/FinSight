using FinSight.Usuario.Infrastructure.Utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FinSight.Usuario.Infrastructure.Data
{
    public class AppDbFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var ConfigPath = config["connection:ConfigPath"] ?? "";

            var constants = new Constants();
            constants.ConfigPath = ConfigPath;

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(Constants.Connection, ServerVersion.AutoDetect(Constants.Connection));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
