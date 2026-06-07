using AppointMe.Shared.Authorization.Permissions.DefaultGrants;

namespace AppointMe.Organizations.Invitations;

public static class InvitationRolesGrants
{
    public static readonly IReadOnlyCollection<RolePermissionGrant> DefaultGrants =
    [
        new(Role.Owner,
            InvitationPermissions.Resend,
            InvitationPermissions.Cancel
        ),
        new(Role.Manager,
            InvitationPermissions.Resend,
            InvitationPermissions.Cancel
        )
    ];
}
