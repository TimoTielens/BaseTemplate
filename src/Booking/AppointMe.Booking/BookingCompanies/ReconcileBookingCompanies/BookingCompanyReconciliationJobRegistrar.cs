using AppointMe.Shared.Jobs;

namespace AppointMe.Booking.BookingCompanies.ReconcileBookingCompanies;

internal sealed class BookingCompanyReconciliationJobRegistrar : IRecurringJobRegistrar
{
    private const string JobId = "booking:company-reconciliation";
    private const string CronExpression = "0 * * * *"; // hourly

    public void Register(IRecurringJobScheduler scheduler)
    {
        scheduler.AddOrUpdate<BookingCompanyReconciliationJob>(
            JobId,
            job => job.Run(CancellationToken.None),
            CronExpression);
    }
}
