namespace AppointMe.Organizations.Tests.Permissions.OverrideInteraction;

public class when_a_grant_override_is_set_for_a_permission_not_in_defaults : behaves_like_resolving_permissions
{
    // Staff has no defaults; a grant override should extend their permissions
    protected override IReadOnlySet<Role> Roles => new HashSet<Role> { Role.Staff };

    protected override IEnumerable<RolePermissionOverride> Overrides =>
    [
        Create.Override.WithRole(Role.Staff).WithPermission(Write).Build()
    ];

    [Fact]
    public void should_add_the_permission() => Assert.Equivalent(new[] { Write }, Result, strict: true);
}
