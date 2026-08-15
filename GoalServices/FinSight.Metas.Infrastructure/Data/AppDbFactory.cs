using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FinSight.Metas.Infrastructure.Utilidades;
using Microsoft.Extensions.Configuration.Json;

namespace FinSight.Metas.Infrastructure.Data
{
    public class AppDbFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var ConfigPath = Environment.ExpandEnvironmentVariables(config["connection:ConfigPath"]);
            Constants.ConfigPath = ConfigPath;

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(Constants.Connection, ServerVersion.AutoDetect(Constants.Connection));
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
