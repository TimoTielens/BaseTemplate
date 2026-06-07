using AppointMe.Booking.ServiceProviders.UpdateServiceProvider;

namespace AppointMe.Booking.Tests.ServiceProviders.UpdateServiceProvider;

public class UpdateServiceProviderTests
{
    [Fact]
    public void should_update_name_and_preserve_other_fields()
    {
        var serviceProvider = Create.ServiceProvider.Build();
        var originalId = serviceProvider.Id;
        var originalCompanyId = serviceProvider.CompanyId;
        var originalIsDeleted = serviceProvider.IsDeleted;

        var newName = PersonName.Create("Robin", "Hale");

        serviceProvider.Update(newName);

        Assert.Equal(newName, serviceProvider.Name);
        Assert.Equal(originalId, serviceProvider.Id);
        Assert.Equal(originalCompanyId, serviceProvider.CompanyId);
        Assert.Equal(originalIsDeleted, serviceProvider.IsDeleted);
    }
}
