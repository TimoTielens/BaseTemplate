
namespace AppointMe.Booking.ServiceProviders;

public sealed record ServiceProvider : AggregateRoot
{
    public required ServiceProviderId Id { get; init; }
    public required CompanyId CompanyId { get; init; }
    public required PersonName Name { get; set; }
    public required bool IsDeleted { get; set; }
}
