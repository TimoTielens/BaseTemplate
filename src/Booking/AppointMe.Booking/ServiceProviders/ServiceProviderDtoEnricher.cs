namespace AppointMe.Booking.ServiceProviders;

internal static class ServiceProviderDtoEnricher
{
    extension(ServiceProviderDto provider)
    {
        public ServiceProviderDto Enrich()
        {
            var personName = PersonName.CreateOrNull(provider.FirstName, provider.LastName);
            return provider with
            {
                FullName = personName?.FullName
            };
        }
    }
}
