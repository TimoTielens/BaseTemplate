using AppointMe.Crm.Database;
using Microsoft.Extensions.Logging;

namespace AppointMe.Crm.Customers.SeedDemoCustomers;

public sealed class SeedDemoCustomersCommandHandler(
    CrmDbContext dbContext,
    SeedDemoCustomers seeder,
    ILogger<SeedDemoCustomersCommandHandler> logger)
{
    public async Task HandleAsync(SeedDemoCustomersCommand command, CompanyId companyId,
        CancellationToken cancellationToken)
    {
        if (await dbContext.Customers.AnyAsync(cancellationToken))
        {
            return;
        }

        var customers = seeder.Generate(companyId, command.Count);

        dbContext.Customers.AddRange(customers);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Seeded {Count} demo customers for company {CompanyId}.",
            command.Count, companyId.Value);
    }
}
