using AppointMe.Shared.Domain;

namespace AppointMe.Booking.Contracts.ServiceProviders.Events;

public sealed record ServiceProviderProjected(Guid ServiceProviderId, Guid CompanyId) : IDomainEvent;
