
namespace AppointMe.Booking.Attendees;

public sealed record Attendee : AggregateRoot
{
    public required AttendeeId Id { get; init; }
    public required CompanyId CompanyId { get; init; }
    public required PersonName Name { get; set; }
    public required DateOfBirth? DateOfBirth { get; set; }
    public required Email? Email { get; set; }
    public required bool IsDeleted { get; set; }
}
