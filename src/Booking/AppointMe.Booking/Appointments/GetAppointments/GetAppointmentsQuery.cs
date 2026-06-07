namespace AppointMe.Booking.Appointments.GetAppointments;

public sealed class GetAppointmentsQuery
{
    public required DateTimeOffset Start { get; init; }
    public required DateTimeOffset End { get; init; }
}
