using AppointMe.Crm.Contracts.Customers;
using AppointMe.Crm.Customers.Database;
using AppointMe.Crm.Database;
using AppointMe.Shared.Pagination;

namespace AppointMe.Crm.Customers;

internal sealed class CustomerRehydrationSource(CrmDbContext dbContext) : ICustomerRehydrationSource
{
    public async Task<PagedResult<CustomerSnapshot>> GetByCompanyAsync(
        Guid companyId, PaginationFilter pagination, CancellationToken cancellationToken)
    {
        var customers = dbContext.Customers
            .IgnoreQueryFilters([CustomerFilters.CompanyId])
            .AsNoTracking()
            .Where(customer => customer.CompanyId == new CompanyId(companyId));

        var totalCount = await customers.CountAsync(cancellationToken);

        var items = await customers
            .OrderBy(customer => customer.Id)
            .Skip(pagination.Offset)
            .Take(pagination.Limit)
            .Select(customer => new CustomerSnapshot(
                customer.Id.Value,
                customer.CompanyId.Value,
                customer.Name.FirstName,
                customer.Name.LastName,
                customer.DateOfBirth != null ? customer.DateOfBirth.Value : null,
                customer.Email != null ? customer.Email.Value : null))
            .ToListAsync(cancellationToken);

        return items.ToPagedResult(pagination, totalCount);
    }
}
