namespace AppointMe.Organizations.Invitations.ResendInvitation;

internal sealed class ResendInvitationEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/invitations/{id:guid}/resend", ResendInvitation)
            .WithName(nameof(ResendInvitation));
    }

    private static async Task ResendInvitation(Guid id, IMessageBus bus, CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new ResendInvitationCommand { Id = id }, cancellationToken);
    }
}
