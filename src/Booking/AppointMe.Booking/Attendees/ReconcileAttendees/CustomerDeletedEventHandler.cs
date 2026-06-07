using AppointMe.Booking.Attendees.DeleteAttendee;
using AppointMe.Booking.Database;
using AppointMe.Crm.Contracts.Customers.Events;

namespace AppointMe.Booking.Attendees.ReconcileAttendees;

public class CustomerDeletedEventHandler(BookingDbContext dbContext)
{
    public async Task Handle(CustomerDeletedEvent @event, CancellationToken cancellationToken)
    {
        var attendee = await dbContext.Attendees
            .IgnoreQueryFilters()
            .SingleOrDefaultAsync(attendee => attendee.Id == new AttendeeId(@event.CustomerId), cancellationToken);

        if (attendee is null || attendee.IsDeleted)
        {
            return;
        }

        attendee.Delete();
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
