using AppointMe.Booking.Attendees.CreateAttendee;
using AppointMe.Booking.Attendees.UpdateAttendee;

namespace AppointMe.Booking.Attendees.ReconcileAttendees;

public static class UpsertAttendee
{
    extension(DbSet<Attendee> attendees)
    {
        public async Task UpsertAsync(
            Guid customerId,
            Guid companyId,
            PersonName name,
            DateOfBirth? dateOfBirth,
            Email? email,
            CancellationToken cancellationToken)
        {
            var attendee = await attendees
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(attendee => attendee.Id == new AttendeeId(customerId), cancellationToken);

            if (attendee is null)
            {
                attendee = Attendee.Create(
                    id: new AttendeeId(customerId),
                    companyId: new CompanyId(companyId),
                    name: name,
                    dateOfBirth: dateOfBirth,
                    email: email);

                await attendees.AddAsync(attendee, cancellationToken);
            }
            else
            {
                attendee.Update(name, dateOfBirth, email);
            }
        }
    }
}
