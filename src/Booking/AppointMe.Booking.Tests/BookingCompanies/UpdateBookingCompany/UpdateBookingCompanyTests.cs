using AppointMe.Booking.BookingCompanies.UpdateBookingCompany;

namespace AppointMe.Booking.Tests.BookingCompanies.UpdateBookingCompany;

public class UpdateBookingCompanyTests
{
    [Fact]
    public void should_update_name_and_time_zone_and_preserve_id()
    {
        var bookingCompany = Create.BookingCompany.Build();
        var originalId = bookingCompany.Id;

        const string newName = "Acme Wellness";
        var newTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/London");

        bookingCompany.Update(newName, newTimeZone);

        Assert.Equal(newName, bookingCompany.Name);
        Assert.Equal(newTimeZone, bookingCompany.TimeZone);
        Assert.Equal(originalId, bookingCompany.Id);
    }
}
