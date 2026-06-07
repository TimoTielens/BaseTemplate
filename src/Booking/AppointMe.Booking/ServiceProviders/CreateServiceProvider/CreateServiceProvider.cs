using AppointMe.Booking.Contracts.ServiceProviders.Events;

namespace AppointMe.Booking.ServiceProviders.CreateServiceProvider;

public static class CreateServiceProvider
{
    extension(ServiceProvider)
    {
        public static ServiceProvider Create(ServiceProviderId id, CompanyId companyId, PersonName name)
        {
            var serviceProvider = new ServiceProvider
            {
                Id = id,
                CompanyId = companyId,
                Name = name,
                IsDeleted = false
            };
            serviceProvider.Raise(new ServiceProviderProjected(id.Value, companyId.Value));
            return serviceProvider;
        }
    }
}
