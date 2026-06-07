using System.Globalization;
using AppointMe.Booking.Attendees.CreateAttendee;
using Microsoft.Extensions.Time.Testing;

namespace AppointMe.Booking.Tests.Attendees.CreateAttendee;

public class CreateAttendeeTests
{
    [Fact]
    public void should_create_active_attendee_with_supplied_state()
    {
        var timeProvider = new FakeTimeProvider(
            DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo));
        var id = new AttendeeId(NewId());
        var companyId = new CompanyId(NewId());
        var name = PersonName.Create("Jane", "Doe");
        var dateOfBirth = DateOfBirth.Create(DateOnly.Parse("1990-05-12", DateTimeFormatInfo.InvariantInfo), timeProvider);
        var email = Email.Create("jane.doe@example.com");

        var attendee = Attendee.Create(id, companyId, name, dateOfBirth, email);

        Assert.Equal(id, attendee.Id);
        Assert.Equal(companyId, attendee.CompanyId);
        Assert.Equal(name, attendee.Name);
        Assert.Equal(dateOfBirth, attendee.DateOfBirth);
        Assert.Equal(email, attendee.Email);
        Assert.False(attendee.IsDeleted);
    }
}
