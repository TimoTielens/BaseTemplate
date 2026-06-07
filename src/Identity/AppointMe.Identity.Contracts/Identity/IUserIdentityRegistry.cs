using AppointMe.Shared.Authentication;

namespace AppointMe.Identity.Contracts.Identity;

public interface IUserIdentityRegistry
{
    Task<UserIdentity> GetOrRegister(AuthenticatedUser authenticatedUser, CancellationToken cancellationToken);
}
