using AutoMapper;
using Linnworks.Core.Application.Services;
using Linnworks.Core.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Linnworks.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IRegionsService, RegionsService>();
            services.AddTransient<IItemsService, ItemsService>();
            services.AddTransient<IOrderPrioritiesService, OrderPrioritiesService>();
            services.AddTransient<IOrdersService, OrdersService>();

            return services;
        }
    }
}
