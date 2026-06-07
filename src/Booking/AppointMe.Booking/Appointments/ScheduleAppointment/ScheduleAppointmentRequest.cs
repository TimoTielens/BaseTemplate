namespace AppointMe.Booking.Appointments.ScheduleAppointment;

public sealed class ScheduleAppointmentRequest
{
    public required Guid ProviderId { get; init; }
    public required Guid AttendeeId { get; init; }
    public required DateTimeOffset Start { get; init; }
    public required DateTimeOffset End { get; init; }
    public string? Notes { get; init; }
}

public static class ScheduleAppointmentRequestExtensions
{
    extension(ScheduleAppointmentRequest request)
    {
        public ScheduleAppointmentCommand ToCommand()
        {
            return new ScheduleAppointmentCommand
            {
                ProviderId = request.ProviderId,
                AttendeeId = request.AttendeeId,
                Start = request.Start,
                End = request.End,
                Notes = request.Notes
            };
        }
    }
}
