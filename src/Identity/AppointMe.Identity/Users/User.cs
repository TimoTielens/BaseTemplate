using AppointMe.Shared.Domain;
using AppointMe.Shared.Users;

namespace AppointMe.Identity.Users;

public sealed record User : AggregateRoot
{
    public required UserId Id { get; init; }
    public required IdentityProviderUserId IdentityProviderUserId { get; init; }
    public required PersonName Name { get; init; }
    public required Email Email { get; init; }
    public required DateTimeOffset RegisteredAt { get; init; }
    public required bool IsDeleted { get; init; }
}
