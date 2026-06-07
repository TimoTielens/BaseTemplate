using AppointMe.Shared.Authorization.Permissions;

namespace AppointMe.Organizations.Settings.Permissions;

public static class PermissionPermissions
{
    public static readonly Permission View = new("permissions", "view");
    public static readonly SystemPermission Manage = new("permissions", "manage");
}
