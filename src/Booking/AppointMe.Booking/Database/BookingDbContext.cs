using AppointMe.Booking.Appointments;
using AppointMe.Booking.Attendees;
using AppointMe.Booking.BookingCompanies;
using AppointMe.Booking.ServiceProviders;

namespace AppointMe.Booking.Database;

public sealed class BookingDbContext(DbContextOptions<BookingDbContext> options, ICurrentCompany currentCompany)
    : DbContext(options)
{
    public const string Schema = "booking";

    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Attendee> Attendees => Set<Attendee>();
    public DbSet<ServiceProvider> ServiceProviders => Set<ServiceProvider>();
    public DbSet<BookingCompany> BookingCompanies => Set<BookingCompany>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(Infrastructure.BookingModuleAssembly.Instance);

        modelBuilder.Entity<Appointment>()
            .HasQueryFilter(BookingFilters.CompanyId, appointment => appointment.CompanyId == currentCompany.CompanyId);

        modelBuilder.Entity<Attendee>()
            .HasQueryFilter(BookingFilters.SoftDelete, attendee => !attendee.IsDeleted)
            .HasQueryFilter(BookingFilters.CompanyId, attendee => attendee.CompanyId == currentCompany.CompanyId);

        modelBuilder.Entity<ServiceProvider>()
            .HasQueryFilter(BookingFilters.SoftDelete, provider => !provider.IsDeleted)
            .HasQueryFilter(BookingFilters.CompanyId, provider => provider.CompanyId == currentCompany.CompanyId);
    }
}
