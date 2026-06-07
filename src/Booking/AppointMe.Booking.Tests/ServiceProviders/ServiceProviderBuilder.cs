namespace AppointMe.Booking.Tests.ServiceProviders;

public sealed class ServiceProviderBuilder
{
    public ServiceProvider Build() => new()
    {
        Id = new ServiceProviderId(NewId()),
        CompanyId = new CompanyId(NewId()),
        Name = PersonName.Create("Alex", "Stone"),
        IsDeleted = false
    };
}
