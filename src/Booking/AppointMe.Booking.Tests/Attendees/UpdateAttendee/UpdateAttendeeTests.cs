using System.Globalization;
using AppointMe.Booking.Attendees.UpdateAttendee;
using Microsoft.Extensions.Time.Testing;

namespace AppointMe.Booking.Tests.Attendees.UpdateAttendee;

public class UpdateAttendeeTests
{
    [Fact]
    public void should_update_name_date_of_birth_and_email_and_preserve_identity()
    {
        var attendee = Create.Attendee.Build();
        var originalId = attendee.Id;
        var originalCompanyId = attendee.CompanyId;
        var originalIsDeleted = attendee.IsDeleted;

        var timeProvider = new FakeTimeProvider(
            DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo));
        var newName = PersonName.Create("Janet", "Smith");
        var newDateOfBirth = DateOfBirth.Create(DateOnly.Parse("1992-08-20", DateTimeFormatInfo.InvariantInfo), timeProvider);
        var newEmail = Email.Create("janet.smith@example.com");

        attendee.Update(newName, newDateOfBirth, newEmail);

        Assert.Equal(newName, attendee.Name);
        Assert.Equal(newDateOfBirth, attendee.DateOfBirth);
        Assert.Equal(newEmail, attendee.Email);
        Assert.Equal(originalId, attendee.Id);
        Assert.Equal(originalCompanyId, attendee.CompanyId);
        Assert.Equal(originalIsDeleted, attendee.IsDeleted);
    }
}
