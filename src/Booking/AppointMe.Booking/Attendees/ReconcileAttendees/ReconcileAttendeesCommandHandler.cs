using System.Runtime.CompilerServices;
using AppointMe.Booking.Database;
using AppointMe.Crm.Contracts.Customers;
using AppointMe.Shared.Pagination;
using Microsoft.Extensions.Logging;

namespace AppointMe.Booking.Attendees.ReconcileAttendees;

public class ReconcileAttendeesCommandHandler(
    BookingDbContext dbContext,
    ICustomerRehydrationSource source,
    AttendeeSynchronizer synchronizer,
    ILogger<ReconcileAttendeesCommandHandler> logger)
{
    private const int PageSize = 100;

    public async Task HandleAsync(ReconcileAttendeesCommand _, CompanyId companyId,
        CancellationToken cancellationToken)
    {
        await foreach (var snapshot in GetCustomerSnapshots(companyId, cancellationToken))
        {
            await ReconcileAttendee(snapshot, companyId, cancellationToken);
        }

        logger.LogInformation("Finished attendee reconciliation for company {CompanyId}.", companyId);
    }

    private async IAsyncEnumerable<CustomerSnapshot> GetCustomerSnapshots(CompanyId companyId,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var pageNumber = 1;
        var hasMore = true;

        while (hasMore)
        {
            var page = await source.GetByCompanyAsync(
                companyId.Value,
                new PaginationFilter(limit: PageSize, page: pageNumber),
                cancellationToken);

            foreach (var snapshot in page.Items)
            {
                yield return snapshot;
            }

            hasMore = pageNumber < page.TotalPages;
            pageNumber++;
        }
    }

    private async Task ReconcileAttendee(CustomerSnapshot snapshot, CompanyId companyId,
        CancellationToken cancellationToken)
    {
        try
        {
            await synchronizer.Apply(snapshot, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            logger.LogError(exception,
                "Failed to reconcile attendee {CustomerId} in company {CompanyId}.",
                snapshot.CustomerId,
                companyId);

            dbContext.ChangeTracker.Clear();
        }
    }
}
