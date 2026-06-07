using AppointMe.Organizations.Contracts.Companies;
using AppointMe.Organizations.Database;

namespace AppointMe.Organizations.Companies;

internal sealed class CompanyRehydrationSource(OrganizationsDbContext dbContext) : ICompanyRehydrationSource
{
    public async Task<IReadOnlyList<CompanySnapshot>> GetAll(CancellationToken cancellationToken)
    {
        return await dbContext.Companies
            .AsNoTracking()
            .Select(company => new CompanySnapshot(
                CompanyId: company.Id.Value,
                Name: company.Name.Value,
                TimeZone: company.TimeZone.Id))
            .ToListAsync(cancellationToken);
    }
}
