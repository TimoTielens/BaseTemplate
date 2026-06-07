namespace AppointMe.Organizations.Invitations.AcceptInvitation;

internal sealed class AcceptInvitationEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/invitations/{id:guid}/accept", AcceptInvitation)
            .WithName(nameof(AcceptInvitation))
            .RequireAuthorization();
    }

    private static async Task AcceptInvitation(Guid id, IMessageBus bus,
        CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new AcceptInvitationCommand { Id = id }, cancellationToken);
    }
}
