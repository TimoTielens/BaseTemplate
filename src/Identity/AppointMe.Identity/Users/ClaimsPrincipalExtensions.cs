using System.Security.Claims;

namespace AppointMe.Identity.Users;

public static class ClaimsPrincipalExtensions
{
    extension(ClaimsPrincipal claimsPrincipal)
    {
        public AuthenticatedUser ToAuthenticatedUser()
        {
            var subject = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)
                          ?? throw new AccessDeniedException("Missing subject claim");

            var name = claimsPrincipal.FindFirstValue(ClaimTypes.Name)
                       ?? throw new AccessDeniedException("Missing name claim");

            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email)
                        ?? throw new AccessDeniedException("Missing email claim");

            return new AuthenticatedUser
            {
                IdentityProviderUserId = new IdentityProviderUserId(subject),
                Name = PersonName.FromFullName(name),
                Email = Email.Create(email)
            };
        }
    }
}
