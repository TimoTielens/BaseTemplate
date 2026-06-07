using AppointMe.Shared.Authorization.Roles;

namespace AppointMe.Shared.Authorization.Permissions.DefaultGrants;

public sealed record RolePermissionGrant(Role Role, params IReadOnlyCollection<Permission> Permissions);
