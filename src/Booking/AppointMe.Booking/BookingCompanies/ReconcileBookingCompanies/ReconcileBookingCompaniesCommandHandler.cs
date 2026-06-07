using AppointMe.Booking.Database;
using AppointMe.Organizations.Contracts.Companies;
using Microsoft.Extensions.Logging;

namespace AppointMe.Booking.BookingCompanies.ReconcileBookingCompanies;

public class ReconcileBookingCompaniesCommandHandler(
    BookingDbContext dbContext,
    ICompanyRehydrationSource source,
    BookingCompanySynchronizer synchronizer,
    ILogger<ReconcileBookingCompaniesCommandHandler> logger)
{
    public async Task HandleAsync(ReconcileBookingCompaniesCommand command, CancellationToken cancellationToken)
    {
        var snapshots = await source.GetAll(cancellationToken);

        var processed = 0;
        foreach (var snapshot in snapshots)
        {
            try
            {
                await synchronizer.Apply(snapshot, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                processed++;
            }
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                logger.LogError(exception,
                    "Failed to reconcile booking company {CompanyId}.", snapshot.CompanyId);
                dbContext.ChangeTracker.Clear();
            }
        }

        logger.LogInformation("Reconciled {Count} booking companies.", processed);
    }
}
