namespace AppointMe.Organizations.Me;

internal sealed class GetCurrentUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/me", GetCurrentUser)
            .WithName(nameof(GetCurrentUser))
            .AllowAnonymous();
    }

    private static async Task<GetCurrentUserResponse> GetCurrentUser(
        IMessageBus bus, CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<GetCurrentUserResponse>(new GetCurrentUserQuery(), cancellationToken);
    }
}
