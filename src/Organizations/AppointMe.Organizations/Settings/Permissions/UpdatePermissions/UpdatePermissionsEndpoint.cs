using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Organizations.Settings.Permissions.UpdatePermissions;

internal sealed class UpdatePermissionsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch("/settings/permissions", UpdatePermissions)
            .WithName(nameof(UpdatePermissions));
    }

    private static async Task UpdatePermissions([FromBody] UpdatePermissionsRequest request,
        IMessageBus bus, CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new UpdatePermissionsCommand
        {
            Grants = request.Grants
        }, cancellationToken);
    }
}
