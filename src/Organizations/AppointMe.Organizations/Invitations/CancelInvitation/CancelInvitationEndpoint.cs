namespace AppointMe.Organizations.Invitations.CancelInvitation;

internal sealed class CancelInvitationEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/invitations/{id:guid}", CancelInvitation)
            .WithName(nameof(CancelInvitation));
    }

    private static async Task<IResult> CancelInvitation(Guid id, IMessageBus bus, CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new CancelInvitationCommand { Id = id }, cancellationToken);
        return Results.NoContent();
    }
}
