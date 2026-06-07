using AppointMe.Shared.Domain;

namespace AppointMe.Organizations.Contracts.Invitations.Events;

public record EmployeeInvitationResent(Guid InvitationId, Guid CompanyId, string Email) : IDomainEvent;
