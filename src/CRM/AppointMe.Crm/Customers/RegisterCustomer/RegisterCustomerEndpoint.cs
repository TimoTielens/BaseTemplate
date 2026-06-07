using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Crm.Customers.RegisterCustomer;

internal sealed class RegisterCustomerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/customers", RegisterCustomer).WithName(nameof(RegisterCustomer));
    }

    private static async Task<Created<RegisterCustomerResponse>> RegisterCustomer(
        [FromBody] RegisterCustomerRequest request, IMessageBus bus, CancellationToken cancellationToken)
    {
        var result = await bus.InvokeAsync<RegisterCustomerResponse>(request.ToCommand(), cancellationToken);
        return TypedResults.Created($"/customers/{result.Id}", result);
    }
}
