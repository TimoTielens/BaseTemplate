using AppointMe.Shared.Domain;

namespace AppointMe.Organizations.Contracts.Invitations.Events;

public record EmployeeInvited(Guid InvitationId, Guid CompanyId, string Email) : IDomainEvent;
