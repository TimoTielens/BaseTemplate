namespace AppointMe.Shared.Tests.Domain.Common;

public class PersonNameTests
{
    [Fact]
    public void should_create_person_name_when_valid()
    {
        var name = PersonName.Create("John", "Doe");

        Assert.Equal("John", name.FirstName);
        Assert.Equal("Doe", name.LastName);
    }

    [Fact]
    public void should_allow_optional_last_name()
    {
        var name = PersonName.Create("John", null);

        Assert.Equal("John", name.FirstName);
        Assert.Null(name.LastName);
    }

    [Fact]
    public void should_throw_validation_exception_when_first_name_is_empty()
    {
        var error = Assert.Throws<ValidationException>(() => PersonName.Create("", null));

        Assert.Equal("firstName must not be empty.", error.Message);
    }

    [Fact]
    public void should_throw_validation_exception_when_first_name_is_too_long()
    {
        var error = Assert.Throws<ValidationException>(() => PersonName.Create(new string('A', 101), null));

        Assert.Equal("firstName must be 100 chars at max.", error.Message);
    }

    [Fact]
    public void should_throw_validation_exception_when_last_name_is_too_long()
    {
        var error = Assert.Throws<ValidationException>(() => PersonName.Create("John", new string('B', 101)));

        Assert.Equal("lastName must be 100 chars at max.", error.Message);
    }

    [Theory]
    [InlineData("john", "doe")]
    [InlineData("joHN", "doE")]
    [InlineData("JOHN", "DOE")]
    public void should_capitalize_name(string firstName, string lastName)
    {
        var name = PersonName.Create(firstName, lastName);

        Assert.Equal("John", name.FirstName);
        Assert.Equal("Doe", name.LastName);
    }

    [Theory]
    [InlineData("John", "Doe", "John Doe")]
    [InlineData("John", null, "John")]
    public void should_generate_full_name(string firstName, string? lastName, string expected)
    {
        var name = PersonName.Create(firstName, lastName);

        Assert.Equal(expected, name.FullName);
    }

    [Theory]
    [InlineData("John", "Doe", "JD")]
    [InlineData("John", null, "J")]
    [InlineData("John", "", "J")]
    public void should_generate_initials(string firstName, string? lastName, string expected)
    {
        var name = PersonName.Create(firstName, lastName);

        Assert.Equal(expected, name.Initials);
    }

    [Fact]
    public void should_create_person_name_from_full_name_with_first_and_last()
    {
        var name = PersonName.FromFullName("John Doe");

        Assert.Equal("John", name.FirstName);
        Assert.Equal("Doe", name.LastName);
    }

    [Fact]
    public void should_create_person_name_from_full_name_with_only_first_name()
    {
        var name = PersonName.FromFullName("John");

        Assert.Equal("John", name.FirstName);
        Assert.Null(name.LastName);
    }

    [Fact]
    public void should_trim_and_ignore_extra_spaces_for_full_name()
    {
        var name = PersonName.FromFullName("   John    Doe   ");

        Assert.Equal("John", name.FirstName);
        Assert.Equal("Doe", name.LastName);
    }

    [Fact]
    public void should_assign_all_remaining_parts_to_last_name()
    {
        var name = PersonName.FromFullName("John Ronald Reuel Tolkien");

        Assert.Equal("John", name.FirstName);
        Assert.Equal("Ronald Reuel Tolkien", name.LastName);
    }

    [Theory]
    [InlineData("john doe")]
    [InlineData("JOHN DOE")]
    [InlineData("joHN doE")]
    public void should_capitalize_names_when_created_from_full_name(string fullName)
    {
        var name = PersonName.FromFullName(fullName);

        Assert.Equal("John", name.FirstName);
        Assert.Equal("Doe", name.LastName);
    }

    [Fact]
    public void should_throw_validation_exception_when_full_name_is_empty()
    {
        var error = Assert.Throws<ValidationException>(() => PersonName.FromFullName(""));

        Assert.Equal("Full name must not be empty.", error.Message);
    }

    [Fact]
    public void should_throw_validation_exception_when_full_name_is_whitespace()
    {
        var error = Assert.Throws<ValidationException>(() => PersonName.FromFullName("   "));

        Assert.Equal("Full name must not be empty.", error.Message);
    }
}
