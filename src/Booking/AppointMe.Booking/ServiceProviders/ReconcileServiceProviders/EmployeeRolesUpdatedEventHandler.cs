using AppointMe.Booking.Database;
using AppointMe.Organizations.Contracts.Employees;
using AppointMe.Organizations.Contracts.Employees.Events;

namespace AppointMe.Booking.ServiceProviders.ReconcileServiceProviders;

public class EmployeeRolesUpdatedEventHandler(BookingDbContext dbContext, ServiceProviderSynchronizer synchronizer)
{
    public async Task Handle(EmployeeRolesUpdated @event, CancellationToken cancellationToken)
    {
        var snapshot = new EmployeeSnapshot(
            EmployeeId: @event.EmployeeId,
            CompanyId: new CompanyId(@event.CompanyId),
            FirstName: @event.FirstName,
            LastName: @event.LastName,
            Roles: @event.Roles);

        var existing = await dbContext.ServiceProviders
            .IgnoreQueryFilters()
            .SingleOrDefaultAsync(provider => provider.Id == new ServiceProviderId(snapshot.EmployeeId),
                cancellationToken);

        await synchronizer.Apply(existing, snapshot, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
