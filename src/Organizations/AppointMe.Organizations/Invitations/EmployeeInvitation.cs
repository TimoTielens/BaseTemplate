using AppointMe.Shared.Domain;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Invitations;

public record EmployeeInvitation : AggregateRoot
{
    public required EmployeeInvitationId Id { get; init; }
    public required CompanyId CompanyId { get; init; }
    public required Email Email { get; init; }
    public required IReadOnlyList<Role> Roles { get; init; }
    public required InvitationStatus Status { get; set; }
    public required DateTimeOffset ExpiresAt { get; init; }
    public required UserId InvitedBy { get; init; }
    public required DateTimeOffset InvitedAt { get; init; }
    public DateTimeOffset? AcceptedAt { get; set; }
}
