using AppointMe.Shared.Pagination;

namespace AppointMe.Crm.Customers.GetCustomers;

public class GetCustomerQuery
{
    public required string? Search { get; init; }
    public required PaginationFilter Pagination { get; init; }
}
