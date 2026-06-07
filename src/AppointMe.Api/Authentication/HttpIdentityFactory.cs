using AppointMe.Identity.Contracts.Identity;
using AppointMe.Identity.Users;

namespace AppointMe.Api.Authentication;

public sealed class HttpIdentityFactory(
    IHttpContextAccessor httpContextAccessor,
    IUserIdentityRegistry userIdentityRegistry
)
{
    public async ValueTask<IIdentity> Create(CancellationToken cancellationToken)
    {
        var principal = httpContextAccessor.HttpContext?.User;
        if (principal?.Identity?.IsAuthenticated != true)
        {
            return new AnonymousIdentity();
        }

        var authenticatedUser = principal.ToAuthenticatedUser();
        return await userIdentityRegistry.GetOrRegister(authenticatedUser, cancellationToken);
    }
}
