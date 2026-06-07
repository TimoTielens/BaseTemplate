
namespace AppointMe.Booking.Attendees.Database;

internal static class AttendeeQueries
{
    extension(IQueryable<Attendee> attendees)
    {
        public async Task<Attendee> LoadAsync(AttendeeId id, CancellationToken cancellationToken)
        {
            var attendee = await attendees
                .SingleOrDefaultAsync(attendee => attendee.Id == id, cancellationToken);

            return attendee ?? throw new NotFoundException($"Attendee with id='{id.Value}' not found");
        }
    }
}
