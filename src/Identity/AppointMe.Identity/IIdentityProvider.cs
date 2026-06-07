namespace AppointMe.Identity;

public interface IIdentityProvider
{
    Task<IdentityProviderUserId> CreateUserWithPasswordSetup(string email, PersonName? name, string redirectUri,
        CancellationToken cancellationToken);

    Task ResendInvitationEmail(string email, string redirectUri, CancellationToken cancellationToken);
}
