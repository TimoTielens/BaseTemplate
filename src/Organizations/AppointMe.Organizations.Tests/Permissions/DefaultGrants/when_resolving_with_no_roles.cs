namespace AppointMe.Organizations.Tests.Permissions.DefaultGrants;

public class when_resolving_with_no_roles : behaves_like_resolving_permissions
{
    [Fact]
    public void should_return_empty() => Assert.Empty(Result);
}
