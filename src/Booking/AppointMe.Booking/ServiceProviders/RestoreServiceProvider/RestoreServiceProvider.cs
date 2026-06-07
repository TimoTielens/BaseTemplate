namespace AppointMe.Booking.ServiceProviders.RestoreServiceProvider;

public static class RestoreServiceProvider
{
    extension(ServiceProvider serviceProvider)
    {
        public void Restore()
        {
            serviceProvider.IsDeleted = false;
        }
    }
}
