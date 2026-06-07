using Microsoft.Extensions.DependencyInjection;

namespace AppointMe.Identity.Keycloak;

public static class KeycloakServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddKeycloakIdentityProvider()
        {
            services.AddOptions<KeycloakAdminOptions>()
                .BindConfiguration("KeycloakAdmin")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services.AddScoped<IIdentityProvider, KeycloakIdentityProvider>();
        }
    }
}
