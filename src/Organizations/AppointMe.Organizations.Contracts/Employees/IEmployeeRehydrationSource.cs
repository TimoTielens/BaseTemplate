using AppointMe.Shared.Authorization.Roles;
using AppointMe.Shared.Companies;

namespace AppointMe.Organizations.Contracts.Employees;

public sealed record EmployeeSnapshot(
    Guid EmployeeId,
    CompanyId CompanyId,
    string FirstName,
    string? LastName,
    IReadOnlyList<Role> Roles);

public interface IEmployeeRehydrationSource
{
    Task<IReadOnlyList<EmployeeSnapshot>> GetAllByCompany(CompanyId companyId, CancellationToken cancellationToken);
}
