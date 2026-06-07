using AppointMe.Organizations.Employees;
using AppointMe.Shared.Domain;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Companies;

public record Company : AggregateRoot
{
    public required CompanyId Id { get; init; }
    public required CompanyName Name { get; init; }
    public required TimeZoneInfo TimeZone { get; init; }
    public required UserId RegisteredBy { get; init; }
    public required DateTimeOffset RegistrationDate { get; init; }
    public required EmployeeId? PrimaryOwnerEmployeeId { get; set; }
}
