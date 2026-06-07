using AppointMe.Shared.Domain;

namespace AppointMe.Booking.Contracts.Appointments.Events;

public sealed record AppointmentRescheduledEvent(
    Guid AppointmentId,
    Guid CompanyId,
    Guid ProviderId,
    DateTimeOffset Start,
    DateTimeOffset End) : IDomainEvent;
