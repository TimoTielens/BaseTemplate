using AppointMe.Organizations.Contracts.Settings.Permissions.Events;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Settings.Permissions.ResetPermissions;

public static class ResetPermissions
{
    extension(CompanyPermissionPolicy policy)
    {
        public CompanyPermissionsChanged? Reset(UserId? changedBy)
        {
            if (policy.Overrides.Count == 0)
            {
                return null;
            }

            policy.Removed.AddRange(policy.Overrides.Values);
            policy.Overrides.Clear();
            return new CompanyPermissionsChanged(policy.CompanyId.Value, changedBy?.Value);
        }
    }
}
