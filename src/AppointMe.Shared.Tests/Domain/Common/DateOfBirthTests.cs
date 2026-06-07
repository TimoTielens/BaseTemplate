using System.Globalization;
using Microsoft.Extensions.Time.Testing;

namespace AppointMe.Shared.Tests.Domain.Common;

public sealed class DateOfBirthTests
{
    private readonly FakeTimeProvider _timeProvider = new(DateTimeOffset.Parse("5 Oct 2025 12:00Z", DateTimeFormatInfo.InvariantInfo));

    [Fact]
    public void should_create_date_of_birth_when_date_is_valid()
    {
        var validDate = _timeProvider.Today().AddYears(-30);
        var dob = DateOfBirth.Create(validDate, _timeProvider);

        Assert.Equal(validDate, dob.Value);
    }

    [Fact]
    public void should_create_date_of_birth_when_date_is_today()
    {
        var today = _timeProvider.Today();
        var dob = DateOfBirth.Create(today, _timeProvider);

        Assert.Equal(today, dob.Value);
    }

    [Fact]
    public void should_throw_validation_exception_when_date_is_in_the_future()
    {
        var futureDate = _timeProvider.Today().AddDays(1);
        var error = Assert.Throws<ValidationException>(() => DateOfBirth.Create(futureDate, _timeProvider));

        Assert.Equal("Date of birth cannot be in the future.", error.Message);
    }

    [Fact]
    public void should_throw_validation_exception_when_date_is_more_than_150_years_in_the_past()
    {
        var pastDate = _timeProvider.Today().AddYears(-150).AddDays(-1);
        var error = Assert.Throws<ValidationException>(() => DateOfBirth.Create(pastDate, _timeProvider));

        Assert.Equal("Date of birth is not valid.", error.Message);
    }

    [Fact]
    public void should_create_date_of_birth_when_date_150_years_in_the_past()
    {
        var exactlyMaxAge = _timeProvider.Today().AddYears(-150);
        var dob = DateOfBirth.Create(exactlyMaxAge, _timeProvider);

        Assert.Equal(exactlyMaxAge, dob.Value);
    }

    [Fact]
    public void should_return_null_for_optional_date_when_null()
    {
        var dob = DateOfBirth.CreateOrNull(null, _timeProvider);

        Assert.Null(dob);
    }

    [Theory]
    [InlineData("1995-10-05", 30)] // Born today, 30 years ago
    [InlineData("2025-10-05", 0)] // Born today
    [InlineData("1995-10-04", 30)] // Born yesterday (in 1995), already had birthday
    [InlineData("1995-10-06", 29)] // Born tomorrow (in 1995), birthday not yet
    [InlineData("2000-01-01", 25)] // Born in 2000, birthday passed
    public void should_calculate_age_on_various_dates(string birthDateString, int expectedAge)
    {
        var birthDate = DateOnly.Parse(birthDateString, DateTimeFormatInfo.InvariantInfo);
        var dob = DateOfBirth.Create(birthDate, _timeProvider);

        var actualAge = dob.GetAge(_timeProvider);

        Assert.Equal(expectedAge, actualAge);
    }

    [Fact]
    public void should_calculate_age_when_leap_year_and_birthday_is_february29()
    {
        // Born on leap day 2000
        var birthDate = DateOnly.Parse("2000-02-29", DateTimeFormatInfo.InvariantInfo);
        var dob = DateOfBirth.Create(birthDate, _timeProvider);

        var actualAge = dob.GetAge(_timeProvider);

        Assert.Equal(25, actualAge);
    }
}
