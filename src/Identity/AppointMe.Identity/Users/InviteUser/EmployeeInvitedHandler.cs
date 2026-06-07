using AppointMe.Organizations.Contracts.Invitations.Events;
using AppointMe.Shared.Configuration;
using Microsoft.Extensions.Options;

namespace AppointMe.Identity.Users.InviteUser;

public sealed class EmployeeInvitedHandler(
    IIdentityProvider identityProvider,
    IOptions<FrontendOptions> frontendOptions)
{
    public async Task HandleAsync(EmployeeInvited @event, CancellationToken cancellationToken)
    {
        await identityProvider.CreateUserWithPasswordSetup(
            email: @event.Email,
            name: null,
            redirectUri: frontendOptions.Value.InvitationUrl,
            cancellationToken: cancellationToken);
    }
}
