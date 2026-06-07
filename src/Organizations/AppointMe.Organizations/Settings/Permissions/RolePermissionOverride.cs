namespace AppointMe.Organizations.Settings.Permissions;

public sealed class RolePermissionOverride
{
    public required CompanyId CompanyId { get; init; }
    public required Role Role { get; init; }
    public required string PermissionCode { get; init; }
    public required bool IsGranted { get; set; }
}
