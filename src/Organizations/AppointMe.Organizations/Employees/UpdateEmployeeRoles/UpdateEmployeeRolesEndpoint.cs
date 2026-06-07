using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Organizations.Employees.UpdateEmployeeRoles;

internal sealed class UpdateEmployeeRolesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPut("/employees/{id:guid}/roles", UpdateRoles)
            .WithName(nameof(UpdateRoles));
    }

    private static async Task UpdateRoles(Guid id, [FromBody] UpdateEmployeeRolesRequest request,
        IMessageBus bus, CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new UpdateEmployeeRolesCommand { Id = id, Roles = request.Roles }, cancellationToken);
    }
}
