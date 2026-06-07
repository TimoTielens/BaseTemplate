using AppointMe.Booking.Database;
using Microsoft.Extensions.Logging;

namespace AppointMe.Booking.Attendees.ReconcileAttendees;

public sealed class AttendeeReconciliationJob(
    BookingDbContext dbContext,
    IMessageBus bus,
    ILogger<AttendeeReconciliationJob> logger)
{
    public async Task Run(CancellationToken cancellationToken)
    {
        var companies = await dbContext.BookingCompanies.ToListAsync(cancellationToken);
        foreach (var company in companies)
        {
            await bus.InvokeForTenantAsync(company.Id.Value.ToString(), new ReconcileAttendeesCommand(),
                cancellationToken);
        }

        logger.LogInformation("Attendee reconciliation scheduled across {CompaniesCount} companies.",
            companies.Count);
    }
}
