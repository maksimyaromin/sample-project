using Linnworks.Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Linnworks.DataSeederUtility
{
    public class LinnworksDbContextFactory : IDesignTimeDbContextFactory<LinnworksDbContext>
    {
        public LinnworksDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Seeder.json", true, true)
                .Build();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LinnworksDbContext>();
            dbContextOptionsBuilder
                .UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(LinnworksDbContextFactory).Assembly.FullName));

            return new LinnworksDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
