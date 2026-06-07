using System.Reflection;

namespace AppointMe.Booking.Infrastructure;

public static class BookingModuleAssembly
{
    public static Assembly Instance => typeof(BookingModuleAssembly).Assembly;
}
