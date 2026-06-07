using AppointMe.Shared.Domain;

namespace AppointMe.Organizations.Contracts.Invitations.Events;

public record EmployeeInvitationCancelled(Guid InvitationId, Guid CompanyId) : IDomainEvent;
