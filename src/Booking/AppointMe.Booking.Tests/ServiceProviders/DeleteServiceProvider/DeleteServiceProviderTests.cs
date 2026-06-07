using AppointMe.Booking.ServiceProviders.DeleteServiceProvider;

namespace AppointMe.Booking.Tests.ServiceProviders.DeleteServiceProvider;

public class DeleteServiceProviderTests
{
    [Fact]
    public void should_mark_service_provider_as_deleted()
    {
        var serviceProvider = Create.ServiceProvider.Build();
        var originalName = serviceProvider.Name;

        serviceProvider.Delete();

        Assert.True(serviceProvider.IsDeleted);
        Assert.Equal(originalName, serviceProvider.Name);
    }
}
