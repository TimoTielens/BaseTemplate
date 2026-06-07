namespace AppointMe.Organizations.Tests.Permissions.DefaultGrants;

public class when_resolving_for_a_role_with_no_registered_defaults : behaves_like_resolving_permissions
{
    protected override IReadOnlySet<Role> Roles => new HashSet<Role> { Role.Staff };

    [Fact]
    public void should_return_empty() => Assert.Empty(Result);
}
