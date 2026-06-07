
namespace AppointMe.Booking.ServiceProviders.Database;

internal static class ServiceProviderQueries
{
    extension(IQueryable<ServiceProvider> serviceProviders)
    {
        public async Task<ServiceProvider> LoadAsync(ServiceProviderId id, CancellationToken cancellationToken)
        {
            var serviceProvider = await serviceProviders
                .SingleOrDefaultAsync(serviceProvider => serviceProvider.Id == id, cancellationToken);

            return serviceProvider ?? throw new NotFoundException($"Service provider with id='{id.Value}' not found");
        }
    }
}
