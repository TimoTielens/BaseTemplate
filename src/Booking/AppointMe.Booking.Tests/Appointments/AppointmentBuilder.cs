using System.Globalization;

namespace AppointMe.Booking.Tests.Appointments;

public sealed class AppointmentBuilder
{
    private static readonly DateTimeOffset Anchor =
        DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo);

    private AppointmentStatus _status = AppointmentStatus.Scheduled;

    public AppointmentBuilder WithStatus(AppointmentStatus status)
    {
        _status = status;
        return this;
    }

    public Appointment Build() => new()
    {
        Id = new AppointmentId(NewId()),
        CompanyId = new CompanyId(NewId()),
        ProviderId = new ServiceProviderId(NewId()),
        AttendeeId = new AttendeeId(NewId()),
        Period = DateTimeOffsetPeriod.Create(Anchor, Anchor.AddHours(1)),
        Notes = LongString.Create("Initial visit"),
        Status = _status,
        ScheduledAt = Anchor.AddDays(-1)
    };
}
