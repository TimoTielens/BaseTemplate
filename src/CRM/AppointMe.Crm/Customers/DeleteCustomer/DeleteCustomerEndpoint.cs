
namespace AppointMe.Crm.Customers.DeleteCustomer;

internal sealed class DeleteCustomerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/customers/{id:guid}", DeleteCustomer).WithName(nameof(DeleteCustomer));
    }

    private static async Task DeleteCustomer(Guid id, IMessageBus bus, CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new DeleteCustomerCommand(id), cancellationToken);
    }
}
