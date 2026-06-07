using Microsoft.Extensions.Logging;

namespace AppointMe.Booking.BookingCompanies.ReconcileBookingCompanies;

public sealed class BookingCompanyReconciliationJob(
    IMessageBus bus,
    ILogger<BookingCompanyReconciliationJob> logger)
{
    public async Task Run(CancellationToken cancellationToken)
    {
        await bus.InvokeAsync(new ReconcileBookingCompaniesCommand(), cancellationToken);
        logger.LogInformation("Booking company reconciliation scheduled.");
    }
}
