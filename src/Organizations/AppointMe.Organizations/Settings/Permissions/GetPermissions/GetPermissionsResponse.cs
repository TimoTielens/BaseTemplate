namespace AppointMe.Organizations.Settings.Permissions.GetPermissions;

public sealed class GetPermissionsResponse
{
    public required IReadOnlyList<RoleDefinitionDto> Roles { get; init; }
    public required IReadOnlyList<PermissionGroupDto> Groups { get; init; }
}

public sealed class RoleDefinitionDto
{
    public required Role Role { get; init; }
    public required bool AllowsPermissionOverrides { get; init; }
}

public sealed class PermissionGroupDto
{
    public required string Name { get; init; }
    public required IReadOnlyList<PermissionDto> Permissions { get; init; }
}

public sealed class PermissionDto
{
    public required string Key { get; init; }

    public required Dictionary<Role, PermissionGrantDto> Grants { get; init; }
}

public sealed class PermissionGrantDto
{
    public required bool IsGranted { get; init; }
    public required bool IsOverridden { get; set; }
}
