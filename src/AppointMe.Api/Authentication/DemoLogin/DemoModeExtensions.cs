using AppointMe.Shared.Configuration;
using AppointMe.Shared.Endpoints;

namespace AppointMe.Api.Authentication.DemoLogin;

internal static class DemoModeExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDemoMode(IConfiguration configuration)
        {
            services.AddOptions<DemoOptions>()
                .BindConfiguration("Demo")
                .ValidateOnStart();

            services.AddEndpoints(typeof(DemoLoginEndpoint).Assembly);

            var provider = configuration["Authentication:Provider"] ?? "Keycloak";
            switch (provider)
            {
                case "Keycloak":
                    services.AddHttpClient<IDemoUserAuthenticator, KeycloakDemoUserAuthenticator>();
                    break;
                case "EntraExternalId":
                    services.AddHttpClient<IDemoUserAuthenticator, EntraExternalIdDemoUserAuthenticator>();
                    break;
            }

            return services;
        }
    }
}
