using AppointMe.Organizations.Companies;
using AppointMe.Organizations.Employees;
using AppointMe.Organizations.Employees.Database;
using AppointMe.Organizations.Invitations;
using AppointMe.Organizations.Invitations.Database;
using AppointMe.Organizations.Settings.Permissions;
using AppointMe.Organizations.Settings.Permissions.Database;

namespace AppointMe.Organizations.Database;

public class OrganizationsDbContext(DbContextOptions<OrganizationsDbContext> options, ICurrentCompany currentCompany)
    : DbContext(options)
{
    public const string DefaultSchema = "organizations";

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeeInvitation> Invitations => Set<EmployeeInvitation>();
    public DbSet<RolePermissionOverride> RolePermissionOverrides => Set<RolePermissionOverride>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Role>().HaveConversion<RoleValueConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(ModuleAssembly.Instance);

        modelBuilder.Entity<Employee>()
            .HasQueryFilter(EmployeeFilters.SoftDelete, employee => !employee.IsDeleted)
            .HasQueryFilter(EmployeeFilters.CompanyId, employee => employee.CompanyId == currentCompany.CompanyId);

        modelBuilder.Entity<EmployeeInvitation>()
            .HasQueryFilter(EmployeeInvitationFilters.CompanyId, invitation => invitation.CompanyId == currentCompany.CompanyId);

        modelBuilder.Entity<RolePermissionOverride>()
            .HasQueryFilter(RolePermissionOverrideFilters.CompanyId, o => o.CompanyId == currentCompany.CompanyId);
    }
}
