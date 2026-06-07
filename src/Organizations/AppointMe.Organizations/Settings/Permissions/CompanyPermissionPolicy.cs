namespace AppointMe.Organizations.Settings.Permissions;

public sealed record CompanyPermissionPolicy
{
    public required CompanyId CompanyId { get; init; }
    public required Dictionary<(string PermissionCode, Role Role), RolePermissionOverride> Overrides { get; init; }
    public List<RolePermissionOverride> Added { get; } = [];
    public List<RolePermissionOverride> Removed { get; } = [];
}

public static class CompanyPermissionPolicyFactory
{
    extension(CompanyPermissionPolicy)
    {
        public static CompanyPermissionPolicy Load(CompanyId companyId,
            Dictionary<(string PermissionCode, Role Role), RolePermissionOverride> overrides)
            => new()
            {
                CompanyId = companyId,
                Overrides = overrides,
            };
    }
}
