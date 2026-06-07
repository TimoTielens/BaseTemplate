using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AppointMe.Api.Authentication.EntraExternalId;

public sealed class EntraExternalIdClaimsTransformer : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
        {
            return Task.FromResult(principal);
        }

        var identity = (ClaimsIdentity)principal.Identity;

        // Subject — Entra also emits `oid` (object id, stable across sign-ins). Prefer `sub` for
        // OIDC interop; fall back to `oid` if the user flow strips `sub`.
        identity.NormalizeFirstAvailable(ClaimTypes.NameIdentifier, ["sub", "oid"]);

        NormalizeName(identity);

        // External ID often emits `emails` as an array (B2C-style) instead of a scalar `email`.
        // Fall back through email → emails[0] → preferred_username.
        identity.NormalizeFirstAvailable(ClaimTypes.Email, ["email", "emails", "preferred_username"]);

        return Task.FromResult(principal);
    }

    private static void NormalizeName(ClaimsIdentity identity)
    {
        if (identity.HasClaim(claim => claim.Type == ClaimTypes.Name))
        {
            return;
        }

        var name = identity.FindFirst("name")?.Value
                   ?? identity.ComposeName()
                   ?? identity.FindFirst("preferred_username")?.Value;

        if (name is not null)
        {
            identity.AddClaim(new Claim(ClaimTypes.Name, name));
        }
    }
}
