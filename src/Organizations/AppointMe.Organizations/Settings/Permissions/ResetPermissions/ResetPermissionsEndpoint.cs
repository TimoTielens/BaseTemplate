namespace AppointMe.Organizations.Settings.Permissions.ResetPermissions;

internal sealed class ResetPermissionsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/settings/permissions/overrides", ResetPermissions)
            .WithName(nameof(ResetPermissions));
    }

    private static async Task ResetPermissions(IMessageBus bus, CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new ResetPermissionsCommand(), cancellationToken);
    }
}
