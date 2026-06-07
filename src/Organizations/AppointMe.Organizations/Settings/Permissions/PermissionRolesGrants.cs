using AppointMe.Shared.Authorization.Permissions.DefaultGrants;

namespace AppointMe.Organizations.Settings.Permissions;

public static class PermissionRolesGrants
{
    public static readonly IReadOnlyCollection<RolePermissionGrant> DefaultGrants =
    [
        new(Role.Owner,
            PermissionPermissions.View,
            PermissionPermissions.Manage
        ),
        new(Role.Manager,
            PermissionPermissions.View
        ),
    ];
}
