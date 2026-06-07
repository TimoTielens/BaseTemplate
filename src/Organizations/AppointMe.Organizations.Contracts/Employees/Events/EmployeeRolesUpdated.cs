using AppointMe.Shared.Authorization.Roles;
using AppointMe.Shared.Domain;

namespace AppointMe.Organizations.Contracts.Employees.Events;

public record EmployeeRolesUpdated(
    Guid EmployeeId,
    Guid CompanyId,
    string FirstName,
    string? LastName,
    Role[] Roles) : IDomainEvent;
