using Microsoft.AspNetCore.Authentication;

namespace AppointMe.Api.Authentication.EntraExternalId;

internal static class EntraExternalIdAuthenticationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddEntraExternalIdAuthOptions(IConfiguration configuration)
        {
            services.AddOptions<EntraExternalIdOptions>()
                .BindConfiguration("Authentication:EntraExternalId")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var options = configuration.GetRequiredSection("Authentication:EntraExternalId")
                              .Get<EntraExternalIdOptions>()
                          ?? throw new InvalidOperationException(
                              "Authentication:EntraExternalId configuration is missing.");

            services.AddSingleton(new IdentityProviderOptions
            {
                Authority = options.Authority,
                OidcClientId = options.ClientId,
                OidcClientSecret = options.ClientSecret,
                ApiAudience = options.ApiAudience,
            });

            services.AddTransient<IClaimsTransformation, EntraExternalIdClaimsTransformer>();

            return services;
        }
    }
}
