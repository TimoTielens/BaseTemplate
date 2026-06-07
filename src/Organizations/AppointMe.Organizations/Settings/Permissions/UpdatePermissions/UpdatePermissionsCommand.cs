namespace AppointMe.Organizations.Settings.Permissions.UpdatePermissions;

public sealed class UpdatePermissionsCommand
{
    public required List<RolePermissionGrantDto> Grants { get; init; }
}
