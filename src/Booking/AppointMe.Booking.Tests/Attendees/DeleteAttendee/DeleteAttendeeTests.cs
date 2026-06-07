using AppointMe.Booking.Attendees.DeleteAttendee;

namespace AppointMe.Booking.Tests.Attendees.DeleteAttendee;

public class DeleteAttendeeTests
{
    [Fact]
    public void should_mark_attendee_as_deleted()
    {
        var attendee = Create.Attendee.Build();
        var originalName = attendee.Name;
        var originalEmail = attendee.Email;

        attendee.Delete();

        Assert.True(attendee.IsDeleted);
        Assert.Equal(originalName, attendee.Name);
        Assert.Equal(originalEmail, attendee.Email);
    }
}
