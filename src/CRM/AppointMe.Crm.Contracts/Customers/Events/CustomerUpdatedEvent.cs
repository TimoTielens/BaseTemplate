using AppointMe.Shared.Domain;

namespace AppointMe.Crm.Contracts.Customers.Events;

public sealed record CustomerUpdatedEvent(
    Guid CustomerId,
    Guid CompanyId,
    string FirstName,
    string? LastName,
    DateOnly? DateOfBirth,
    string? Email) : IDomainEvent;
