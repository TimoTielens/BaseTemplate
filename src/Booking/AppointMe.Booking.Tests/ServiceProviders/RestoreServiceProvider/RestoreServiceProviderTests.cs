using AppointMe.Booking.ServiceProviders.DeleteServiceProvider;
using AppointMe.Booking.ServiceProviders.RestoreServiceProvider;

namespace AppointMe.Booking.Tests.ServiceProviders.RestoreServiceProvider;

public class RestoreServiceProviderTests
{
    [Fact]
    public void should_clear_is_deleted_flag()
    {
        var serviceProvider = Create.ServiceProvider.Build();
        serviceProvider.Delete();

        serviceProvider.Restore();

        Assert.False(serviceProvider.IsDeleted);
    }
}
