using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Booking.Appointments.ScheduleAppointment;

internal sealed class ScheduleAppointmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/appointments", ScheduleAppointment).WithName(nameof(ScheduleAppointment));
    }

    private static async Task<Created<ScheduleAppointmentResponse>> ScheduleAppointment(
        [FromBody] ScheduleAppointmentRequest request, IMessageBus bus, CancellationToken cancellationToken)
    {
        var result = await bus.InvokeAsync<ScheduleAppointmentResponse>(request.ToCommand(), cancellationToken);
        return TypedResults.Created($"/appointments/{result.Id}", result);
    }
}
