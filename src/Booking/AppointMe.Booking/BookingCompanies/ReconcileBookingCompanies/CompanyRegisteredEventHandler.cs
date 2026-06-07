using AppointMe.Booking.Database;
using AppointMe.Organizations.Contracts.Companies;
using AppointMe.Organizations.Contracts.Companies.Events;

namespace AppointMe.Booking.BookingCompanies.ReconcileBookingCompanies;

public class CompanyRegisteredEventHandler(BookingDbContext dbContext, BookingCompanySynchronizer synchronizer)
{
    public async Task Handle(CompanyRegistered @event, CancellationToken cancellationToken)
    {
        var snapshot = new CompanySnapshot(
            CompanyId: @event.CompanyId,
            Name: @event.Name,
            TimeZone: @event.TimeZone);

        await synchronizer.Apply(snapshot, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
