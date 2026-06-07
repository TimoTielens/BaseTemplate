using AppointMe.Shared.Domain;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Employees;

public record Employee : AggregateRoot
{
    public required EmployeeId Id { get; init; }
    public required CompanyId CompanyId { get; init; }
    public required PersonName Name { get; init; }
    public required Email Email { get; init; }
    public required IReadOnlyList<Role> Roles { get; set; }
    public required UserId UserId { get; init; }
    public required DateTimeOffset RegistrationDate { get; init; }
    public required bool IsDeleted { get; set; }
}
