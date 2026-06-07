using AppointMe.Shared.Pagination;

namespace AppointMe.Crm.Customers.GetCustomers;

internal sealed class GetCustomersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/customers", GetCustomers).WithName(nameof(GetCustomers));
    }

    private static async Task<GetCustomersResponse> GetCustomers([AsParameters] GetCustomersRequest request,
        IMessageBus bus, CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<GetCustomersResponse>(new GetCustomerQuery
        {
            Search = request.Search,
            Pagination = request.ToPaginationFilter()
        }, cancellationToken);
    }
}
