namespace AppointMe.Booking.ServiceProviders.UpdateServiceProvider;

public static class UpdateServiceProvider
{
    extension(ServiceProvider serviceProvider)
    {
        public void Update(PersonName name)
        {
            serviceProvider.Name = name;
        }
    }
}
