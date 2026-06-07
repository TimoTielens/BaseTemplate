using AppointMe.Shared.Pagination;

namespace AppointMe.Crm.Customers.GetCustomers;

public class GetCustomersResponse
{
    public required PagedResult<CustomerDto> Customers { get; init; }
}
