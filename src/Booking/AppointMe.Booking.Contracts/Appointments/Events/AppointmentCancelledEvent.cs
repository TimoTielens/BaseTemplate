using AppointMe.Shared.Domain;

namespace AppointMe.Booking.Contracts.Appointments.Events;

public sealed record AppointmentCancelledEvent(Guid AppointmentId, Guid CompanyId) : IDomainEvent;
