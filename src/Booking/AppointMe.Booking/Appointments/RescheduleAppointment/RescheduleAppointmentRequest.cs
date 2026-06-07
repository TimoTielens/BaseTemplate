namespace AppointMe.Booking.Appointments.RescheduleAppointment;

public sealed class RescheduleAppointmentRequest
{
    public required Guid ProviderId { get; init; }
    public required DateTimeOffset Start { get; init; }
    public required DateTimeOffset End { get; init; }
}

public static class RescheduleAppointmentRequestExtensions
{
    extension(RescheduleAppointmentRequest request)
    {
        public RescheduleAppointmentCommand ToCommand(Guid appointmentId)
        {
            return new RescheduleAppointmentCommand
            {
                AppointmentId = appointmentId,
                ProviderId = request.ProviderId,
                Start = request.Start,
                End = request.End
            };
        }
    }
}
