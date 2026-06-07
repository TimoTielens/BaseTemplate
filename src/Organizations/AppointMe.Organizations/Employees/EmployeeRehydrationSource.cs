using AppointMe.Organizations.Contracts.Employees;
using AppointMe.Organizations.Database;
using AppointMe.Organizations.Employees.Database;

namespace AppointMe.Organizations.Employees;

internal sealed class EmployeeRehydrationSource(OrganizationsDbContext dbContext) : IEmployeeRehydrationSource
{
    public async Task<IReadOnlyList<EmployeeSnapshot>> GetAllByCompany(CompanyId companyId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Employees
            .IgnoreQueryFilters([EmployeeFilters.CompanyId])
            .AsNoTracking()
            .Where(employee => employee.CompanyId == companyId)
            .Select(employee => new EmployeeSnapshot(
                EmployeeId: employee.Id.Value,
                CompanyId: employee.CompanyId,
                FirstName: employee.Name.FirstName,
                LastName: employee.Name.LastName,
                Roles: employee.Roles))
            .ToListAsync(cancellationToken);
    }
}
