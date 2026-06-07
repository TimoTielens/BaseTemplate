
namespace AppointMe.Crm.Customers.GetCustomerById;

internal sealed class GetCustomerByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/customers/{id:guid}", GetCustomer).WithName(nameof(GetCustomer));
    }

    private static async Task<CustomerDto> GetCustomer(Guid id, IMessageBus bus,
        CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<CustomerDto>(new GetCustomerByIdQuery(id), cancellationToken);
    }
}
