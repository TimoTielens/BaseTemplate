using AppointMe.Shared.Jobs;

namespace AppointMe.Booking.ServiceProviders.ReconcileServiceProviders;

internal sealed class ServiceProviderReconciliationJobRegistrar : IRecurringJobRegistrar
{
    private const string JobId = "booking:service-provider-reconciliation";
    private const string CronExpression = "0 * * * *"; // hourly

    public void Register(IRecurringJobScheduler scheduler)
    {
        scheduler.AddOrUpdate<ServiceProviderReconciliationJob>(
            JobId,
            job => job.Run(CancellationToken.None),
            CronExpression);
    }
}
