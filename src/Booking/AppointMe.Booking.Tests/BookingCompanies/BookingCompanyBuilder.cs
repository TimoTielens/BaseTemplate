namespace AppointMe.Booking.Tests.BookingCompanies;

public sealed class BookingCompanyBuilder
{
    public BookingCompany Build() => new()
    {
        Id = new CompanyId(NewId()),
        Name = "Acme Clinic",
        TimeZone = TimeZoneInfo.Utc
    };
}
