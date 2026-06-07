namespace AppointMe.Organizations.Tests.Permissions;

public sealed class RolePermissionOverrideBuilder
{
    private Role _role = Role.Manager;
    private Permission _permission = new("test", "test");
    private bool _isGranted = true;

    public RolePermissionOverrideBuilder WithRole(Role role)
    {
        _role = role;
        return this;
    }

    public RolePermissionOverrideBuilder WithPermission(Permission permission)
    {
        _permission = permission;
        return this;
    }

    public RolePermissionOverrideBuilder Denied()
    {
        _isGranted = false;
        return this;
    }

    public RolePermissionOverride Build() => new()
    {
        CompanyId = new CompanyId(Guid.NewGuid()),
        Role = _role,
        PermissionCode = _permission.Code,
        IsGranted = _isGranted
    };
}
