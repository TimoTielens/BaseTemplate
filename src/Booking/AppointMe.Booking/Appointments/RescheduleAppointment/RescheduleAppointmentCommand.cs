namespace AppointMe.Booking.Appointments.RescheduleAppointment;

public sealed class RescheduleAppointmentCommand
{
    public required Guid AppointmentId { get; init; }
    public required Guid ProviderId { get; init; }
    public required DateTimeOffset Start { get; init; }
    public required DateTimeOffset End { get; init; }
}
