using AppointMe.Booking.Tests.Appointments;
using AppointMe.Booking.Tests.Attendees;
using AppointMe.Booking.Tests.BookingCompanies;
using AppointMe.Booking.Tests.ServiceProviders;

namespace AppointMe.Booking.Tests;

public static class Create
{
    public static AppointmentBuilder Appointment => new();
    public static AttendeeBuilder Attendee => new();
    public static BookingCompanyBuilder BookingCompany => new();
    public static ServiceProviderBuilder ServiceProvider => new();
}
