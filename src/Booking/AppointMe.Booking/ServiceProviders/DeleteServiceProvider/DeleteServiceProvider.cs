namespace AppointMe.Booking.ServiceProviders.DeleteServiceProvider;

public static class DeleteServiceProvider
{
    extension(ServiceProvider serviceProvider)
    {
        public void Delete()
        {
            serviceProvider.IsDeleted = true;
        }
    }
}
