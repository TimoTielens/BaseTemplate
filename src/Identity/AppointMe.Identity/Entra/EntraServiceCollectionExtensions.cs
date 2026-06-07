using Microsoft.Extensions.DependencyInjection;

namespace AppointMe.Identity.Entra;

public static class EntraServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddEntraIdentityProvider()
        {
            services.AddOptions<EntraIdentityOptions>()
                .BindConfiguration("EntraIdentity")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services.AddScoped<IIdentityProvider, EntraIdentityProvider>();
        }
    }
}
