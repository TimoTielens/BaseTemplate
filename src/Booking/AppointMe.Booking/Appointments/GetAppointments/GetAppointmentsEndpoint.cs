
namespace AppointMe.Booking.Appointments.GetAppointments;

internal sealed class GetAppointmentsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/appointments", GetAppointments).WithName(nameof(GetAppointments));
    }

    private static async Task<IReadOnlyList<AppointmentDto>> GetAppointments(
        [AsParameters] GetAppointmentsRequest request, IMessageBus bus, CancellationToken cancellationToken)
    {
        return await bus.InvokeAsync<IReadOnlyList<AppointmentDto>>(request.ToQuery(), cancellationToken);
    }
}
