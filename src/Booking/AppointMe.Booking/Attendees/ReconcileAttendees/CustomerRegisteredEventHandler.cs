using AppointMe.Booking.Database;
using AppointMe.Crm.Contracts.Customers;
using AppointMe.Crm.Contracts.Customers.Events;

namespace AppointMe.Booking.Attendees.ReconcileAttendees;

public class CustomerRegisteredEventHandler(BookingDbContext dbContext, AttendeeSynchronizer synchronizer)
{
    public async Task Handle(CustomerRegisteredEvent @event, CancellationToken cancellationToken)
    {
        var snapshot = new CustomerSnapshot(
            CustomerId: @event.CustomerId,
            CompanyId: @event.CompanyId,
            FirstName: @event.FirstName,
            LastName: @event.LastName,
            DateOfBirth: @event.DateOfBirth,
            Email: @event.Email
        );

        await synchronizer.Apply(snapshot, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
