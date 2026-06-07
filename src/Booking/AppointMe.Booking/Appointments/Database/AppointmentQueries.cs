
namespace AppointMe.Booking.Appointments.Database;

internal static class AppointmentQueries
{
    extension(IQueryable<Appointment> appointments)
    {
        public async Task<Appointment> LoadAsync(AppointmentId id, CancellationToken cancellationToken)
        {
            var appointment = await appointments
                .SingleOrDefaultAsync(appointment => appointment.Id == id, cancellationToken);

            return appointment ?? throw new NotFoundException($"Appointment with id='{id.Value}' not found");
        }
    }
}
