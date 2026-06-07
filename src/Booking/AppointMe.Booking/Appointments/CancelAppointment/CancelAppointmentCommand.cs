namespace AppointMe.Booking.Appointments.CancelAppointment;

public sealed class CancelAppointmentCommand
{
    public required Guid AppointmentId { get; init; }
}
