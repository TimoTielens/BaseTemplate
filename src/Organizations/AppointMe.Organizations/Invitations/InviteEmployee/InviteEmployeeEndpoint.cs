using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Organizations.Invitations.InviteEmployee;

internal sealed class InviteEmployeeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/invitations", InviteEmployee)
            .WithName(nameof(InviteEmployee));
    }

    private static async Task<Created<InviteEmployeeResponse>> InviteEmployee([FromBody] InviteEmployeeRequest request,
        IMessageBus bus, CancellationToken cancellationToken)
    {
        var result = await bus.InvokeAsync<InviteEmployeeResponse>(new InviteEmployeeCommand
        {
            Email = request.Email,
            Roles = request.Roles,
        }, cancellationToken);
        return TypedResults.Created($"/invitations/{result.Id}", result);
    }
}
