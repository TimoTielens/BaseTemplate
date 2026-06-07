using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Booking.Appointments.RescheduleAppointment;

internal sealed class RescheduleAppointmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPut("/appointments/{id:guid}/reschedule", RescheduleAppointment)
            .WithName(nameof(RescheduleAppointment));
    }

    private static async Task<NoContent> RescheduleAppointment(Guid id, [FromBody] RescheduleAppointmentRequest request,
        IMessageBus bus, CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(request.ToCommand(id), cancellationToken);
        return TypedResults.NoContent();
    }
}
