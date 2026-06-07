using AppointMe.Shared.Domain;

namespace AppointMe.Identity.Users.RegisterUser;

public sealed record UserRegisteredEvent(Guid UserId) : IDomainEvent;
