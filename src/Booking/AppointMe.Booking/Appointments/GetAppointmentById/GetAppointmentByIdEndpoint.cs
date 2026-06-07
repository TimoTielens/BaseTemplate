
namespace AppointMe.Booking.Appointments.GetAppointmentById;

internal sealed class GetAppointmentByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/appointments/{id:guid}", GetAppointment)
            .WithName(nameof(GetAppointment));
    }

    private static async Task<AppointmentDto> GetAppointment(Guid id, IMessageBus bus,
        CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<AppointmentDto>(new GetAppointmentByIdQuery(id), cancellationToken);
    }
}
