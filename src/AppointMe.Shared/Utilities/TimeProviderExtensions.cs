namespace AppointMe.Shared.Utilities;

public static class TimeProviderExtensions
{
    extension(TimeProvider timeProvider)
    {
        public DateOnly Today()
        {
            return DateOnly.FromDateTime(timeProvider.GetLocalNow().Date);
        }
    }
}
