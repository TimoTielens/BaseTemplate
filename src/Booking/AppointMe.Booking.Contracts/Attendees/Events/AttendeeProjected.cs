using AppointMe.Shared.Domain;

namespace AppointMe.Booking.Contracts.Attendees.Events;

public sealed record AttendeeProjected(Guid AttendeeId, Guid CompanyId) : IDomainEvent;
