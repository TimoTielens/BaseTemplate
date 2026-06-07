using AppointMe.Booking.BookingCompanies.CreateBookingCompany;

namespace AppointMe.Booking.Tests.BookingCompanies.CreateBookingCompany;

public class CreateBookingCompanyTests
{
    [Fact]
    public void should_create_booking_company_with_supplied_state()
    {
        var id = new CompanyId(NewId());
        const string name = "Acme Clinic";
        var timeZone = TimeZoneInfo.Utc;

        var bookingCompany = BookingCompany.Create(id, name, timeZone);

        Assert.Equal(id, bookingCompany.Id);
        Assert.Equal(name, bookingCompany.Name);
        Assert.Equal(timeZone, bookingCompany.TimeZone);
    }
}
