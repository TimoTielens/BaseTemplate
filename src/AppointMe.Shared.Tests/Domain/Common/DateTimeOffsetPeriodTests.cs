namespace AppointMe.Shared.Tests.Domain.Common;

public class DateTimeOffsetPeriodTests
{
    private static readonly DateTimeOffset Start = new(2026, 4, 26, 10, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset End = new(2026, 4, 26, 11, 0, 0, TimeSpan.Zero);

    [Fact]
    public void should_create_period_when_both_endpoints_are_utc_and_end_is_after_start()
    {
        var period = DateTimeOffsetPeriod.Create(Start, End);

        Assert.Equal(Start, period.Start);
        Assert.Equal(End, period.End);
    }

    [Fact]
    public void should_throw_validation_exception_when_start_has_non_zero_offset()
    {
        var localStart = new DateTimeOffset(2026, 4, 26, 12, 0, 0, TimeSpan.FromHours(2));

        var error = Assert.Throws<ValidationException>(() => DateTimeOffsetPeriod.Create(localStart, End));

        Assert.Equal("Start and end must be in UTC.", error.Message);
    }

    [Fact]
    public void should_throw_validation_exception_when_end_has_non_zero_offset()
    {
        var localEnd = new DateTimeOffset(2026, 4, 26, 13, 0, 0, TimeSpan.FromHours(2));

        var error = Assert.Throws<ValidationException>(() => DateTimeOffsetPeriod.Create(Start, localEnd));

        Assert.Equal("Start and end must be in UTC.", error.Message);
    }

    [Fact]
    public void should_throw_validation_exception_when_end_equals_start()
    {
        var error = Assert.Throws<ValidationException>(() => DateTimeOffsetPeriod.Create(Start, Start));

        Assert.Equal("End time must be after start time.", error.Message);
    }

    [Fact]
    public void should_throw_validation_exception_when_end_is_before_start()
    {
        var error = Assert.Throws<ValidationException>(() => DateTimeOffsetPeriod.Create(End, Start));

        Assert.Equal("End time must be after start time.", error.Message);
    }

    [Fact]
    public void should_validate_offset_before_chronology_when_both_invariants_are_violated()
    {
        var localEnd = new DateTimeOffset(2026, 4, 26, 9, 0, 0, TimeSpan.FromHours(2));

        var error = Assert.Throws<ValidationException>(() => DateTimeOffsetPeriod.Create(Start, localEnd));

        Assert.Equal("Start and end must be in UTC.", error.Message);
    }
}
