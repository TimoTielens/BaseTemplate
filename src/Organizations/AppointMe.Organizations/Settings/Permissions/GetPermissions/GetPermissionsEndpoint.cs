namespace AppointMe.Organizations.Settings.Permissions.GetPermissions;

internal sealed class GetPermissionsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/settings/permissions", GetPermissions)
            .WithName(nameof(GetPermissions));
    }

    private static async Task<GetPermissionsResponse> GetPermissions(
        IMessageBus bus, CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<GetPermissionsResponse>(new GetPermissionsQuery(), cancellationToken);
    }
}
