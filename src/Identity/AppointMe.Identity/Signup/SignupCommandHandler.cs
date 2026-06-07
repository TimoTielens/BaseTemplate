using AppointMe.Identity.Database;
using AppointMe.Identity.Users;
using AppointMe.Identity.Users.RegisterUser;
using AppointMe.Shared.Configuration;
using Microsoft.Extensions.Options;

namespace AppointMe.Identity.Signup;

public sealed class SignupCommandHandler(
    IIdentityProvider identityProvider,
    IOptions<FrontendOptions> frontendOptions,
    TimeProvider timeProvider,
    IdentityDbContext dbContext
)
{
    public async Task HandleAsync(SignupCommand command, CancellationToken cancellationToken)
    {
        var personName = PersonName.FromFullName(command.Name);
        var email = Email.Create(command.Email);

        var redirectUri = new UriBuilder(frontendOptions.Value.InvitationUrl)
        {
            Query = "returnUrl=/onboarding"
        }.Uri.ToString();

        var identityProviderUserId = await identityProvider.CreateUserWithPasswordSetup(
            email: email.Value,
            name: personName,
            redirectUri: redirectUri,
            cancellationToken: cancellationToken
        );

        var user = User.Register(
            identityProviderUserId: identityProviderUserId,
            name: personName,
            email: email,
            registrationDate: timeProvider.GetUtcNow()
        );

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
