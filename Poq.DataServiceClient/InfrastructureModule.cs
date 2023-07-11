using Microsoft.Extensions.DependencyInjection;
using Poq.Application.Interfaces;
using Poq.Application.Services;
using Poq.DataSourceClient.Clients;

namespace Poq.DataSourceClient
{
    public static class InfrastructureModule
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductClient, ProductClient>();
            services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHttpClient();

            return services;
        }
    }
}
