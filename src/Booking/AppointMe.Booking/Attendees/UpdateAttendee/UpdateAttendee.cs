namespace AppointMe.Booking.Attendees.UpdateAttendee;

public static class UpdateAttendee
{
    extension(Attendee attendee)
    {
        public void Update(PersonName name, DateOfBirth? dateOfBirth, Email? email)
        {
            attendee.Name = name;
            attendee.DateOfBirth = dateOfBirth;
            attendee.Email = email;
        }
    }
}
