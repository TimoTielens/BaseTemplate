
namespace AppointMe.Booking.BookingCompanies;

public sealed record BookingCompany
{
    public required CompanyId Id { get; init; }
    public required string Name { get; set; }
    public required TimeZoneInfo TimeZone { get; set; }
}
