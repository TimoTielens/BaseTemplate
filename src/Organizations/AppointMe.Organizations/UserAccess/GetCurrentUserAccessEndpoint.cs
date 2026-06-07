namespace AppointMe.Organizations.UserAccess;

internal sealed class GetCurrentUserAccessEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/me/access", GetCurrentUserAccess)
            .WithName(nameof(GetCurrentUserAccess));
    }

    private static async Task<GetCurrentUserAccessResponse> GetCurrentUserAccess(IMessageBus bus,
        CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<GetCurrentUserAccessResponse>(
            new GetCurrentUserAccessQuery(), cancellationToken);
    }
}
