using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Crm.Customers.UpdateCustomer;

internal sealed class UpdateCustomerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPut("/customers/{id:guid}", UpdateCustomer).WithName(nameof(UpdateCustomer));
    }

    private static async Task<IResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerRequest request,
        IMessageBus bus, CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(request.ToCommand(id), cancellationToken);
        return Results.NoContent();
    }
}
