using AppointMe.Shared.Domain;

namespace AppointMe.Crm.Contracts.Customers.Events;

public sealed record CustomerDeletedEvent(Guid CustomerId, Guid CompanyId) : IDomainEvent;
