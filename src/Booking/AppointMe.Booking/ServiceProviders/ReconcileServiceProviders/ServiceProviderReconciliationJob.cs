using AppointMe.Booking.Database;
using Microsoft.Extensions.Logging;

namespace AppointMe.Booking.ServiceProviders.ReconcileServiceProviders;

public sealed class ServiceProviderReconciliationJob(
    BookingDbContext dbContext,
    IMessageBus bus,
    ILogger<ServiceProviderReconciliationJob> logger)
{
    public async Task Run(CancellationToken cancellationToken)
    {
        var companies = await dbContext.BookingCompanies.ToListAsync(cancellationToken);
        foreach (var company in companies)
        {
            await bus.InvokeForTenantAsync(company.Id.Value.ToString(), new ReconcileServiceProvidersCommand(),
                cancellationToken);
        }

        logger.LogInformation("Service providers reconciliation scheduled across {Companies} companies.",
            companies.Count);
    }
}
