using AppointMe.Organizations.Contracts.Invitations.Events;
using AppointMe.Shared.Configuration;
using Microsoft.Extensions.Options;

namespace AppointMe.Identity.Users.InviteUser;

public sealed class EmployeeInvitationResentHandler(
    IIdentityProvider identityProvider,
    IOptions<FrontendOptions> frontendOptions)
{
    public async Task HandleAsync(EmployeeInvitationResent @event, CancellationToken cancellationToken)
    {
        await identityProvider.ResendInvitationEmail(
            @event.Email,
            redirectUri: frontendOptions.Value.InvitationUrl,
            cancellationToken);
    }
}
