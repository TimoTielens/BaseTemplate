using AppointMe.Identity.Users;
using AppointMe.Identity.Users.Database;

namespace AppointMe.Identity.Database;

public sealed class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public const string DefaultSchema = "identity";

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(ModuleAssembly.Instance);

        modelBuilder.Entity<User>()
            .HasQueryFilter(UserFilters.SoftDelete, user => !user.IsDeleted);
    }
}
