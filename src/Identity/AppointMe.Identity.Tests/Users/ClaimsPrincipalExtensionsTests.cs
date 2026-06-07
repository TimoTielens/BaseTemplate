using System.Security.Claims;

namespace AppointMe.Identity.Tests.Users;

public class ClaimsPrincipalExtensionsTests
{
    private static ClaimsPrincipal PrincipalFrom(params (string Type, string Value)[] claims) =>
        new(new ClaimsIdentity(claims.Select(claim => new Claim(claim.Type, claim.Value))));

    [Fact]
    public void should_build_authenticated_user_from_subject_name_and_email_claims()
    {
        var principal = PrincipalFrom(
            (ClaimTypes.NameIdentifier, "kc-user-id"),
            (ClaimTypes.Name, "Jane Doe"),
            (ClaimTypes.Email, "jane@example.com"));

        var authenticatedUser = principal.ToAuthenticatedUser();

        Assert.Equal("kc-user-id", authenticatedUser.IdentityProviderUserId.Value);
        Assert.Equal(PersonName.FromFullName("Jane Doe"), authenticatedUser.Name);
        Assert.Equal("jane@example.com", authenticatedUser.Email.Value);
    }

    [Fact]
    public void should_throw_access_denied_when_subject_claim_is_missing()
    {
        var principal = PrincipalFrom(
            (ClaimTypes.Name, "Jane Doe"),
            (ClaimTypes.Email, "jane@example.com"));

        var exception = Assert.Throws<AccessDeniedException>(principal.ToAuthenticatedUser);
        Assert.Contains("subject", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void should_throw_access_denied_when_name_claim_is_missing()
    {
        var principal = PrincipalFrom(
            (ClaimTypes.NameIdentifier, "kc-user-id"),
            (ClaimTypes.Email, "jane@example.com"));

        var exception = Assert.Throws<AccessDeniedException>(principal.ToAuthenticatedUser);
        Assert.Contains("name", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void should_throw_access_denied_when_email_claim_is_missing()
    {
        var principal = PrincipalFrom(
            (ClaimTypes.NameIdentifier, "kc-user-id"),
            (ClaimTypes.Name, "Jane Doe"));

        var exception = Assert.Throws<AccessDeniedException>(principal.ToAuthenticatedUser);
        Assert.Contains("email", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void should_throw_validation_exception_when_email_claim_is_malformed()
    {
        var principal = PrincipalFrom(
            (ClaimTypes.NameIdentifier, "kc-user-id"),
            (ClaimTypes.Name, "Jane Doe"),
            (ClaimTypes.Email, "not-an-email"));

        Assert.Throws<ValidationException>(principal.ToAuthenticatedUser);
    }

    [Fact]
    public void should_throw_validation_exception_when_name_claim_is_whitespace()
    {
        var principal = PrincipalFrom(
            (ClaimTypes.NameIdentifier, "kc-user-id"),
            (ClaimTypes.Name, "   "),
            (ClaimTypes.Email, "jane@example.com"));

        Assert.Throws<ValidationException>(principal.ToAuthenticatedUser);
    }

    [Fact]
    public void should_pick_first_subject_claim_when_multiple_are_present()
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, "primary-subject"),
            new Claim(ClaimTypes.NameIdentifier, "shadow-subject"),
            new Claim(ClaimTypes.Name, "Jane Doe"),
            new Claim(ClaimTypes.Email, "jane@example.com")
        ]));

        var authenticatedUser = principal.ToAuthenticatedUser();

        Assert.Equal("primary-subject", authenticatedUser.IdentityProviderUserId.Value);
    }

    [Fact]
    public void should_lowercase_and_trim_email_from_claim()
    {
        var principal = PrincipalFrom(
            (ClaimTypes.NameIdentifier, "kc-user-id"),
            (ClaimTypes.Name, "Jane Doe"),
            (ClaimTypes.Email, "  JANE@Example.COM  "));

        var authenticatedUser = principal.ToAuthenticatedUser();

        Assert.Equal("jane@example.com", authenticatedUser.Email.Value);
    }
}
