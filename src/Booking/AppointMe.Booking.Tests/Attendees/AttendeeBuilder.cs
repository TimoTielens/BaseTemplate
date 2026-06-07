using System.Globalization;
using Microsoft.Extensions.Time.Testing;

namespace AppointMe.Booking.Tests.Attendees;

public sealed class AttendeeBuilder
{
    private readonly FakeTimeProvider _timeProvider =
        new(DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo));

    public Attendee Build() => new()
    {
        Id = new AttendeeId(NewId()),
        CompanyId = new CompanyId(NewId()),
        Name = PersonName.Create("Jane", "Doe"),
        DateOfBirth = DateOfBirth.Create(DateOnly.Parse("1990-05-12", DateTimeFormatInfo.InvariantInfo), _timeProvider),
        Email = Email.Create("jane.doe@example.com"),
        IsDeleted = false
    };
}
