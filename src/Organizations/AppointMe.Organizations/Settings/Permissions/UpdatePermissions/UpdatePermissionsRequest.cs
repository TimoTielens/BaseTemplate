namespace AppointMe.Organizations.Settings.Permissions.UpdatePermissions;

public sealed class UpdatePermissionsRequest
{
    public required List<RolePermissionGrantDto> Grants { get; init; }
}

public sealed class RolePermissionGrantDto
{
    public required string PermissionKey { get; init; }
    public required Role Role { get; init; }
    public required bool IsGranted { get; init; }
}
