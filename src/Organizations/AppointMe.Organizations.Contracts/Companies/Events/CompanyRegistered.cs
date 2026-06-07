using AppointMe.Shared.Domain;

namespace AppointMe.Organizations.Contracts.Companies.Events;

public record CompanyRegistered(Guid CompanyId, string Name, string TimeZone, Guid RegisteredByUserId) : IDomainEvent;
