using AppointMe.Shared.Domain;

namespace AppointMe.Organizations.Contracts.Employees.Events;

public record EmployeeDeleted(Guid EmployeeId, Guid CompanyId) : IDomainEvent;
