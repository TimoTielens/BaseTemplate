using AppointMe.Shared.Domain;

namespace AppointMe.Organizations.Contracts.Settings.Permissions.Events;

public record CompanyPermissionsChanged(Guid CompanyId, Guid? ChangedByUserId) : IDomainEvent;
