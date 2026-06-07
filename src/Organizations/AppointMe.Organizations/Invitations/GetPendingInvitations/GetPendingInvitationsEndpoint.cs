namespace AppointMe.Organizations.Invitations.GetPendingInvitations;

internal sealed class GetPendingInvitationsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/invitations/pending", GetPendingInvitations)
            .WithName(nameof(GetPendingInvitations))
            .RequireAuthorization();
    }

    private static async Task<GetPendingInvitationsResponse> GetPendingInvitations(
        IMessageBus bus, CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<GetPendingInvitationsResponse>(new GetPendingInvitationsQuery(), cancellationToken);
    }
}
