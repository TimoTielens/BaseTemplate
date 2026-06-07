namespace AppointMe.Shared.Tests.Domain.Common;

public class EmailTests
{
    [Theory]
    [InlineData("test@example.com")]
    [InlineData("User.Name+alias@domain.co.uk")]
    [InlineData("  Trimmed@Domain.Com  ")]
    public void should_create_valid_email_and_normalize_case(string input)
    {
        var email = Email.Create(input);

        Assert.Equal(input.Trim().ToLowerInvariant(), email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void should_throw_when_email_is_empty(string input)
    {
        var error = Assert.Throws<ValidationException>(() => Email.Create(input));

        Assert.Equal("Email address is required.", error.Message);
    }

    [Fact]
    public void should_return_null_when_email_is_optional()
    {
        var option = Email.CreateOrNull(null);

        Assert.Null(option);
    }

    [Fact]
    public void should_throw_when_email_exceeds_max_length()
    {
        var longEmail = new string('a', 190) + "@example.com";
        var error = Assert.Throws<ValidationException>(() => Email.Create(longEmail));

        Assert.Equal("Email must be 200 chars at max.", error.Message);
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("no-at-symbol.com")]
    [InlineData("user@")]
    [InlineData("@domain.com")]
    public void should_throw_validation_exception_when_email_has_invalid_format(string input)
    {
        var error = Assert.Throws<ValidationException>(() => Email.Create(input));

        Assert.Equal($"'{input}' is not a valid email address.", error.Message);
    }
}
