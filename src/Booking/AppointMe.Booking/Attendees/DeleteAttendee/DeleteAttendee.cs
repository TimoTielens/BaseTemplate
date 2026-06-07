namespace AppointMe.Booking.Attendees.DeleteAttendee;

public static class DeleteAttendee
{
    extension(Attendee attendee)
    {
        public void Delete()
        {
            attendee.IsDeleted = true;
        }
    }
}
