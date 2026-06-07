namespace AppointMe.Organizations.Tests.Permissions.DefaultGrants;

public class when_resolving_for_a_single_role : behaves_like_resolving_permissions
{
    protected override IReadOnlySet<Role> Roles => new HashSet<Role> { Role.Manager };

    [Fact]
    public void should_return_the_roles_default_grants() => Assert.Equivalent(new[] { Read }, Result, strict: true);
}
