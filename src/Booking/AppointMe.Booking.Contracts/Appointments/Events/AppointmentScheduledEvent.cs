using AppointMe.Shared.Domain;

namespace AppointMe.Booking.Contracts.Appointments.Events;

public sealed record AppointmentScheduledEvent(
    Guid AppointmentId,
    Guid CompanyId,
    Guid ProviderId,
    Guid AttendeeId,
    DateTimeOffset Start,
    DateTimeOffset End,
    string? Notes) : IDomainEvent;
