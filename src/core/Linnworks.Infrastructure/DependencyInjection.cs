using Linnworks.Core.Application.Common.Interfaces;
using Linnworks.Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linnworks.Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<LinnworksDbContext>(options =>
                    options.UseInMemoryDatabase("LinnworksTestDatabase"));
            }
            else
            {
                services.AddDbContext<LinnworksDbContext>(options =>
                    options
                        .UseSqlite(
                            configuration.GetConnectionString("DefaultConnection"))
                        .EnableSensitiveDataLogging());
            }

            services.AddScoped<ILinnworksDbContext>(provider => provider.GetService<LinnworksDbContext>());

            return services;
        }
    }
}
