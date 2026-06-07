namespace AppointMe.Organizations.Settings.Permissions.Database;

public static class RolePermissionOverrideQueries
{
    extension(IQueryable<RolePermissionOverride> overrides)
    {
        public Task<Dictionary<(string PermissionCode, Role Role), RolePermissionOverride>> LoadAsync(
            CompanyId companyId, CancellationToken cancellationToken)
        {
            return overrides
                .Where(permissionOverride => permissionOverride.CompanyId == companyId)
                .ToDictionaryAsync(
                    permissionOverride => (permissionOverride.PermissionCode, permissionOverride.Role),
                    cancellationToken);
        }
    }
}
