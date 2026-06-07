using AppointMe.Organizations.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace AppointMe.Api.Authorization;

public static class AuthorizationServiceCollectionExtensions
{
    public static IServiceCollection AddAppointMeAuthorization(this IServiceCollection services)
    {
        services
            .AddSingleton<ICurrentPrincipal, CurrentPrincipal>()
            .AddScoped<ICurrentPrincipalResolver, CurrentPrincipalResolver>()
            .AddScoped<IAuthorizationHandler, RegisteredUserAuthorizationHandler>()
            .AddSingleton<PermissionRegistry>();

        services
            .AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new RegisteredUserRequirement())
                .Build());

        return services;
    }
}
