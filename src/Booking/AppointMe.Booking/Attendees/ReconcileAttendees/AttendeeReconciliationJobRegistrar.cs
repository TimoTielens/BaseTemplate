using AppointMe.Shared.Jobs;

namespace AppointMe.Booking.Attendees.ReconcileAttendees;

internal sealed class AttendeeReconciliationJobRegistrar : IRecurringJobRegistrar
{
    private const string JobId = "booking:attendee-reconciliation";
    private const string CronExpression = "0 3 * * *"; // daily at 03:00

    public void Register(IRecurringJobScheduler scheduler)
    {
        scheduler.AddOrUpdate<AttendeeReconciliationJob>(
            JobId,
            job => job.Run(CancellationToken.None),
            CronExpression);
    }
}
