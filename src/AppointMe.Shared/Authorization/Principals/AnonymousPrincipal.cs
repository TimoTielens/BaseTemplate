using AppointMe.Shared.Authorization.Roles;
using AppointMe.Shared.Authorization.Permissions;

namespace AppointMe.Shared.Authorization.Principals;

public sealed record AnonymousPrincipal : IPrincipal
{
    public bool HasRole(Role role) => false;
    public bool HasPermission(Permission permission) => false;
}
