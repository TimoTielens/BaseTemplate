using AppointMe.Shared.Authorization.Roles;
using AppointMe.Shared.Authorization.Permissions;

namespace AppointMe.Shared.Authorization.Principals;

public interface IPrincipal
{
    public bool HasRole(Role role);
    public bool HasPermission(Permission permission);
}
