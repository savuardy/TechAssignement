using Poq.Application.Configurations;
using Poq.DataSourceClient.Configurations;

namespace Poq.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration
            services.AddOptions();
            services.Configure<MockyConfiguration>(configuration.GetRequiredSection(MockyConfiguration.Configuration));
            services.Configure<FilterLogicConfiguration>(configuration.GetRequiredSection(FilterLogicConfiguration.Configuration));
            return services;
        }
    }
}
