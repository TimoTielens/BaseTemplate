
namespace AppointMe.Booking.BookingCompanies.CreateBookingCompany;

public static class CreateBookingCompany
{
    extension(BookingCompany)
    {
        public static BookingCompany Create(CompanyId id, string name, TimeZoneInfo timeZone)
        {
            return new BookingCompany
            {
                Id = id,
                Name = name,
                TimeZone = timeZone
            };
        }
    }
}
