using AppointMe.Shared.Domain.Common;

namespace AppointMe.Identity.Contracts.Identity;

public sealed record AuthenticatedUser
{
    public required IdentityProviderUserId IdentityProviderUserId { get; init; }
    public required PersonName Name { get; init; }
    public required Email Email { get; init; }
}
