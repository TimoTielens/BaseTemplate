namespace AppointMe.Booking.Appointments.ScheduleAppointment;

public sealed class ScheduleAppointmentCommand
{
    public required Guid ProviderId { get; init; }
    public required Guid AttendeeId { get; init; }
    public required DateTimeOffset Start { get; init; }
    public required DateTimeOffset End { get; init; }
    public string? Notes { get; init; }
}
