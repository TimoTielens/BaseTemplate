using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AppointMe.Api.Authentication.Keycloak;

public sealed class KeycloakClaimsTransformer : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
        {
            return Task.FromResult(principal);
        }

        var identity = (ClaimsIdentity)principal.Identity;

        identity.NormalizeFirstAvailable(ClaimTypes.NameIdentifier, ["sub"]);
        NormalizeName(identity);
        identity.NormalizeFirstAvailable(ClaimTypes.Email, ["email"]);

        return Task.FromResult(principal);
    }

    private static void NormalizeName(ClaimsIdentity identity)
    {
        if (identity.HasClaim(claim => claim.Type == ClaimTypes.Name))
        {
            return;
        }

        var name = identity.FindFirst("name")?.Value
                   ?? identity.ComposeName();

        if (name is not null)
        {
            identity.AddClaim(new Claim(ClaimTypes.Name, name));
        }
    }
}
