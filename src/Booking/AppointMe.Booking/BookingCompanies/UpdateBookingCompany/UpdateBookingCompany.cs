namespace AppointMe.Booking.BookingCompanies.UpdateBookingCompany;

public static class UpdateBookingCompany
{
    extension(BookingCompany bookingCompany)
    {
        public void Update(string name, TimeZoneInfo timeZone)
        {
            bookingCompany.Name = name;
            bookingCompany.TimeZone = timeZone;
        }
    }
}
