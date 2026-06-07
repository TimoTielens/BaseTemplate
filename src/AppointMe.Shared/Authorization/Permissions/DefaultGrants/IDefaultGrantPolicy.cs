namespace AppointMe.Shared.Authorization.Permissions.DefaultGrants;

public interface IDefaultGrantPolicy
{
    IReadOnlyCollection<RolePermissionGrant> DefaultGrants { get; }
}
