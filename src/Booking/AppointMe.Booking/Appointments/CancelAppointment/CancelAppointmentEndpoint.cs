using Microsoft.AspNetCore.Http.HttpResults;

namespace AppointMe.Booking.Appointments.CancelAppointment;

internal sealed class CancelAppointmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/appointments/{id:guid}/cancel", CancelAppointment)
            .WithName(nameof(CancelAppointment));
    }

    private static async Task<NoContent> CancelAppointment(Guid id, IMessageBus bus,
        CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new CancelAppointmentCommand { AppointmentId = id }, cancellationToken);
        return TypedResults.NoContent();
    }
}
