namespace AppointMe.Organizations.Invitations.GetPendingInvitations;

public sealed class GetPendingInvitationsResponse
{
    public required IReadOnlyList<PendingInvitationDto> Invitations { get; init; }
}

public sealed class PendingInvitationDto
{
    public required Guid Id { get; init; }
    public required Guid CompanyId { get; init; }
    public required string CompanyName { get; init; }
    public required IReadOnlyList<Role> Roles { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
}
