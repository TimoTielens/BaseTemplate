using AppointMe.Booking.Contracts.Attendees.Events;

namespace AppointMe.Booking.Attendees.CreateAttendee;

public static class CreateAttendee
{
    extension(Attendee)
    {
        public static Attendee Create(AttendeeId id, CompanyId companyId, PersonName name, DateOfBirth? dateOfBirth,
            Email? email)
        {
            var attendee = new Attendee
            {
                Id = id,
                CompanyId = companyId,
                Name = name,
                DateOfBirth = dateOfBirth,
                Email = email,
                IsDeleted = false
            };
            attendee.Raise(new AttendeeProjected(id.Value, companyId.Value));
            return attendee;
        }
    }
}
