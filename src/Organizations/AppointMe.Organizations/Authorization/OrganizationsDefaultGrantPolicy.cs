using AppointMe.Organizations.Employees;
using AppointMe.Organizations.Invitations;
using AppointMe.Organizations.Settings.Permissions;
using AppointMe.Shared.Authorization.Permissions.DefaultGrants;

namespace AppointMe.Organizations.Authorization;

public sealed class OrganizationsDefaultGrantPolicy : IDefaultGrantPolicy
{
    public IReadOnlyCollection<RolePermissionGrant> DefaultGrants =>
    [
        ..EmployeeRolesGrants.DefaultGrants,
        ..InvitationRolesGrants.DefaultGrants,
        ..PermissionRolesGrants.DefaultGrants,
    ];
}
