using AppointMe.Booking.ServiceProviders.CreateServiceProvider;

namespace AppointMe.Booking.Tests.ServiceProviders.CreateServiceProvider;

public class CreateServiceProviderTests
{
    [Fact]
    public void should_create_active_service_provider()
    {
        var id = new ServiceProviderId(NewId());
        var companyId = new CompanyId(NewId());
        var name = PersonName.Create("Alex", "Stone");

        var serviceProvider = ServiceProvider.Create(id, companyId, name);

        Assert.Equal(id, serviceProvider.Id);
        Assert.Equal(companyId, serviceProvider.CompanyId);
        Assert.Equal(name, serviceProvider.Name);
        Assert.False(serviceProvider.IsDeleted);
    }
}
