using AppointMe.Booking.Database;
using AppointMe.Crm.Contracts.Customers;

namespace AppointMe.Booking.Attendees.ReconcileAttendees;

public sealed class AttendeeSynchronizer(BookingDbContext dbContext, TimeProvider timeProvider)
{
    public async Task Apply(CustomerSnapshot snapshot, CancellationToken cancellationToken)
    {
        await dbContext.Attendees.UpsertAsync(
            customerId: snapshot.CustomerId,
            companyId: snapshot.CompanyId,
            name: PersonName.Create(snapshot.FirstName, snapshot.LastName),
            dateOfBirth: DateOfBirth.CreateOrNull(snapshot.DateOfBirth, timeProvider),
            email: Email.CreateOrNull(snapshot.Email),
            cancellationToken: cancellationToken);
    }
}
