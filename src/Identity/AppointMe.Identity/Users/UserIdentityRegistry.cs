using AppointMe.Identity.Database;
using AppointMe.Identity.Users.RegisterUser;
using AppointMe.Shared.Authentication;

namespace AppointMe.Identity.Users;

public sealed class UserIdentityRegistry(
    IdentityDbContext dbContext,
    TimeProvider timeProvider
) : IUserIdentityRegistry
{
    public async Task<UserIdentity> GetOrRegister(AuthenticatedUser authenticatedUser,
        CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.IdentityProviderUserId == authenticatedUser.IdentityProviderUserId,
                cancellationToken);

        user ??= await RegisterUser(authenticatedUser, cancellationToken);

        return new UserIdentity(user.Id, user.Name, user.Email);
    }

    private async Task<User> RegisterUser(AuthenticatedUser authenticatedUser, CancellationToken cancellationToken)
    {
        var user = User.Register(
            identityProviderUserId: authenticatedUser.IdentityProviderUserId,
            name: authenticatedUser.Name,
            email: authenticatedUser.Email,
            registrationDate: timeProvider.GetUtcNow());

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }
}
