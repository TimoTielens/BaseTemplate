using AppointMe.Shared.Pagination;

namespace AppointMe.Crm.Contracts.Customers;

public interface ICustomerRehydrationSource
{
    Task<PagedResult<CustomerSnapshot>> GetByCompanyAsync(
        Guid companyId, PaginationFilter pagination, CancellationToken cancellationToken);
}
