using AppointMe.Shared.Domain;

namespace AppointMe.Organizations.Contracts.Invitations.Events;

public record EmployeeInvitationAccepted(Guid InvitationId, Guid CompanyId, Guid UserId) : IDomainEvent;
