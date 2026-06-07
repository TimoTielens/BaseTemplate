using AppointMe.Booking.BookingCompanies.CreateBookingCompany;
using AppointMe.Booking.BookingCompanies.UpdateBookingCompany;

namespace AppointMe.Booking.BookingCompanies.ReconcileBookingCompanies;

public static class UpsertBookingCompany
{
    extension(DbSet<BookingCompany> bookingCompanies)
    {
        public async Task UpsertAsync(
            CompanyId id,
            string name,
            TimeZoneInfo timeZone,
            CancellationToken cancellationToken)
        {
            var bookingCompany = await bookingCompanies
                .SingleOrDefaultAsync(company => company.Id == id, cancellationToken);

            if (bookingCompany is null)
            {
                await bookingCompanies.AddAsync(BookingCompany.Create(id, name, timeZone), cancellationToken);
            }
            else
            {
                bookingCompany.Update(name, timeZone);
            }
        }
    }
}
