using AppointMe.Shared.Authorization.Permissions;
using AppointMe.Shared.Authorization.Principals;
using AppointMe.Shared.Authorization.Roles;

namespace AppointMe.Shared.Tests.Authorization;

public class PrincipalAuthorizationExtensionsTests
{
    private static readonly Permission Read = new("Customers", "Read");
    private static readonly Permission Write = new("Customers", "Write");

    [Fact]
    public void should_not_throw_when_principal_has_all_required_permissions()
    {
        var principal = new StubPrincipal(Read, Write);

        principal.Require(Read, Write);
    }

    [Fact]
    public void should_throw_when_any_required_permission_is_missing()
    {
        var principal = new StubPrincipal(Read);

        var error = Assert.Throws<AccessDeniedException>(() => principal.Require(Read, Write));

        Assert.Contains(Write.Code, error.Message);
    }

    [Fact]
    public void should_list_all_required_permission_codes_in_message_when_denied()
    {
        var principal = new StubPrincipal();

        var error = Assert.Throws<AccessDeniedException>(() => principal.Require(Read, Write));

        Assert.Contains(Read.Code, error.Message);
        Assert.Contains(Write.Code, error.Message);
    }

    [Fact]
    public void should_throw_when_no_permissions_specified()
    {
        var principal = new StubPrincipal();

        Assert.Throws<ArgumentException>(() => principal.Require());
    }

    private sealed class StubPrincipal(params Permission[] granted) : IPrincipal
    {
        public bool HasRole(Role role) => false;

        public bool HasPermission(Permission permission) => granted.Contains(permission);
    }
}
