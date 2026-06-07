using Microsoft.AspNetCore.Authentication;

namespace AppointMe.Api.Authentication.Keycloak;

internal static class KeycloakAuthenticationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddKeycloakAuthOptions(IConfiguration configuration)
        {
            services.AddOptions<KeycloakOptions>()
                .BindConfiguration("Authentication:Keycloak")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var options = configuration.GetRequiredSection("Authentication:Keycloak").Get<KeycloakOptions>()
                          ?? throw new InvalidOperationException("Authentication:Keycloak configuration is missing.");

            services.AddSingleton(new IdentityProviderOptions
            {
                Authority = options.Authority,
                OidcClientId = options.FrontendClientId,
                OidcClientSecret = options.FrontendClientSecret,
                ApiAudience = options.ApiAudience,
                CustomizeOidc = oidc =>
                {
                    var previous = oidc.Events.OnRedirectToIdentityProvider;
                    oidc.Events.OnRedirectToIdentityProvider = async context =>
                    {
                        if (context.Properties.Items.TryGetValue("provider", out var idp))
                        {
                            context.ProtocolMessage.SetParameter("kc_idp_hint", idp);
                        }

                        await previous(context);
                    };
                }
            });

            services.AddTransient<IClaimsTransformation, KeycloakClaimsTransformer>();

            return services;
        }
    }
}
