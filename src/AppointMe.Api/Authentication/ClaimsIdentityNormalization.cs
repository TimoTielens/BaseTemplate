using System.Security.Claims;

namespace AppointMe.Api.Authentication;

internal static class ClaimsIdentityNormalization
{
    extension(ClaimsIdentity identity)
    {
        /// <summary>
        /// Adds <paramref name="targetClaimType"/> from the first non-empty source claim, unless the
        /// target claim already exists. Used to map provider-specific claims onto the standard types.
        /// </summary>
        public void NormalizeFirstAvailable(string targetClaimType, IReadOnlyList<string> sourceClaimTypes)
        {
            if (identity.HasClaim(claim => claim.Type == targetClaimType))
            {
                return;
            }

            foreach (var source in sourceClaimTypes)
            {
                var value = identity.FindFirst(source)?.Value;
                if (!string.IsNullOrEmpty(value))
                {
                    identity.AddClaim(new Claim(targetClaimType, value));
                    return;
                }
            }
        }

        /// <summary>
        /// Composes a display name from the <c>given_name</c> / <c>family_name</c> claims,
        /// or null when neither is present.
        /// </summary>
        public string? ComposeName()
        {
            var given = identity.FindFirst("given_name")?.Value;
            var family = identity.FindFirst("family_name")?.Value;

            return (given, family) switch
            {
                (not null, not null) => $"{given} {family}",
                (not null, null) => given,
                (null, not null) => family,
                _ => null
            };
        }
    }
}
