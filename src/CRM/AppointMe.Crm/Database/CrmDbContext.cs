using AppointMe.Crm.Customers;
using AppointMe.Crm.Customers.Database;

namespace AppointMe.Crm.Database;

public sealed class CrmDbContext(DbContextOptions<CrmDbContext> options, ICurrentCompany currentCompany)
    : DbContext(options)
{
    public const string DefaultSchema = "crm";

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(ModuleAssembly.Instance);

        modelBuilder.Entity<Customer>()
            .HasQueryFilter(CustomerFilters.SoftDelete, customer => !customer.IsDeleted)
            .HasQueryFilter(CustomerFilters.CompanyId, customer => customer.CompanyId == currentCompany.CompanyId);
    }
}
