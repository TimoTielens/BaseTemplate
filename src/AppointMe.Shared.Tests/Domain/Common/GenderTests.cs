using System.Text.Json;

namespace AppointMe.Shared.Tests.Domain.Common;

public class GenderTests
{
    [Theory]
    [InlineData("Male", Gender.Male)]
    [InlineData("Female", Gender.Female)]
    [InlineData("Other", Gender.Other)]
    [InlineData("male", Gender.Male)]
    [InlineData("fEmAle", Gender.Female)]
    [InlineData("OTHER", Gender.Other)]
    public void should_parse_valid_values_case_insensitive(string input, Gender expected)
    {
        var result = Gender.Parse(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void should_return_null_when_input_is_null()
    {
        var result = Gender.ParseOrNull(null);
        Assert.Null(result);
    }

    [Theory]
    [InlineData("unknown")]
    [InlineData("man")]
    public void should_throw_validation_exception_for_invalid_value(string input)
    {
        var ex = Assert.Throws<ValidationException>(() => Gender.Parse(input));
        Assert.StartsWith("Invalid gender value:", ex.Message);
        Assert.Contains("Valid values are: Male, Female, Other", ex.Message);
    }

    [Theory]
    [InlineData(Gender.Male, "\"Male\"")]
    [InlineData(Gender.Female, "\"Female\"")]
    [InlineData(Gender.Other, "\"Other\"")]
    public void should_serialize_gender_to_string(Gender value, string expectedJson)
    {
        var json = JsonSerializer.Serialize(value);
        Assert.Equal(expectedJson, json);
    }

    [Theory]
    [InlineData("\"Male\"", Gender.Male)]
    [InlineData("\"female\"", Gender.Female)]
    [InlineData("\"OTHER\"", Gender.Other)]
    public void should_deserialize_string_to_enum(string json, Gender expected)
    {
        var result = JsonSerializer.Deserialize<Gender>(json);
        Assert.Equal(expected, result);
    }
}
