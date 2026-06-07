using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace AppointMe.Identity.Signup;

internal sealed class SignupEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/signup", Signup).WithName(nameof(Signup)).AllowAnonymous();
    }

    private static async Task<Ok> Signup([FromBody] SignupRequest request, IMessageBus bus,
        CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new SignupCommand
        {
            Name = request.Name,
            Email = request.Email
        }, cancellationToken);
        return TypedResults.Ok();
    }
}
