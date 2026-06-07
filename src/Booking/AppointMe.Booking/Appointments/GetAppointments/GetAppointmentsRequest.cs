namespace AppointMe.Booking.Appointments.GetAppointments;

public sealed class GetAppointmentsRequest
{
    public required DateTimeOffset From { get; init; }
    public required DateTimeOffset To { get; init; }
}

public static class GetAppointmentsRequestExtensions
{
    extension(GetAppointmentsRequest request)
    {
        public GetAppointmentsQuery ToQuery()
        {
            return new GetAppointmentsQuery
            {
                Start = request.From,
                End = request.To
            };
        }
    }
}
