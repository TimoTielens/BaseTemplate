using AppointMe.Booking.Attendees;
using AppointMe.Booking.ServiceProviders;

namespace AppointMe.Booking.Appointments;

public sealed record Appointment : AggregateRoot
{
    public required AppointmentId Id { get; init; }
    public required CompanyId CompanyId { get; init; }
    public required ServiceProviderId ProviderId { get; set; }
    public required AttendeeId AttendeeId { get; init; }
    public required DateTimeOffsetPeriod Period { get; set; }
    public required LongString? Notes { get; init; }
    public required AppointmentStatus Status { get; set; }
    public required DateTimeOffset ScheduledAt { get; init; }
}
