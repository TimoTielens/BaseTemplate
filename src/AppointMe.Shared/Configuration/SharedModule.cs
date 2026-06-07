using AppointMe.Shared.Database.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointMe.Shared.Configuration;

public static class SharedModule
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddSharedModule(IConfiguration configuration)
        {
            DapperTypeHandlerRegistration.Register();

            services.AddOptions<FrontendOptions>()
                .BindConfiguration("Frontend")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddHybridCache();

            return services
                .AddSingleton(TimeProvider.System)
                .AddSingleton(new ConnectionStrings(configuration));
        }
    }
}
