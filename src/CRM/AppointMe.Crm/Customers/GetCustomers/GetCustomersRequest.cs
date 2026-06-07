using AppointMe.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Crm.Customers.GetCustomers;

public sealed class GetCustomersRequest : PaginationRequest
{
    [FromQuery(Name = "search")]
    public string? Search { get; init; }
}
