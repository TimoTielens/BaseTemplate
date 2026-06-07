using AppointMe.Booking.Database;
using AppointMe.Booking.ServiceProviders.CreateServiceProvider;
using AppointMe.Booking.ServiceProviders.DeleteServiceProvider;
using AppointMe.Booking.ServiceProviders.RestoreServiceProvider;
using AppointMe.Booking.ServiceProviders.UpdateServiceProvider;
using AppointMe.Organizations.Contracts.Employees;
using AppointMe.Shared.Authorization.Roles;

namespace AppointMe.Booking.ServiceProviders.ReconcileServiceProviders;

public sealed class ServiceProviderSynchronizer(BookingDbContext dbContext)
{
    public async Task Apply(ServiceProvider? existing, EmployeeSnapshot? snapshot, CancellationToken cancellationToken)
    {
        var isStaff = snapshot?.Roles.Contains(Role.Staff) == true;

        await ((existing, snapshot, isStaff) switch
        {
            // Doesn't exist, needs to be created
            (existing: null, snapshot: not null, isStaff: true) =>
                CreateServiceProvider(snapshot, cancellationToken),

            // Exists, needs update (and potential restoration)
            (existing: not null, snapshot: not null, isStaff: true) =>
                UpdateServiceProvider(existing, snapshot, cancellationToken),

            // Exists but no longer has role, needs deletion
            (existing: not null, snapshot: _, isStaff: false) =>
                DeleteServiceProvider(existing, cancellationToken),

            _ => Task.CompletedTask
        });
    }

    private Task DeleteServiceProvider(ServiceProvider existing, CancellationToken _)
    {
        if (existing.IsDeleted)
        {
            return Task.CompletedTask;
        }

        existing.Delete();
        return Task.CompletedTask;
    }

    private Task UpdateServiceProvider(ServiceProvider existing, EmployeeSnapshot snapshot,
        CancellationToken _)
    {
        if (existing.IsDeleted)
        {
            existing.Restore();
        }

        existing.Update(PersonName.Create(snapshot.FirstName, snapshot.LastName));
        return Task.CompletedTask;
    }

    private async Task CreateServiceProvider(EmployeeSnapshot snapshot, CancellationToken cancellationToken)
    {
        var serviceProvider = ServiceProvider.Create(
            id: new ServiceProviderId(snapshot.EmployeeId),
            companyId: snapshot.CompanyId,
            name: PersonName.Create(snapshot.FirstName, snapshot.LastName));
        await dbContext.ServiceProviders.AddAsync(serviceProvider, cancellationToken);
    }
}
